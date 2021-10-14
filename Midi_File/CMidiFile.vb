Imports System.IO

Public Class CMidiFile

    Public ReadOnly Property MidiFullname As String = ""
    Public ReadOnly Property MidiName As String

    ''' <summary>
    ''' TRUE if Midi-File loaded without errors and Tracks in Tracklist are filled with Track-Events
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property FileLoaded As Boolean
    Public Property ErrorCode As Integer
    Public Property ErrorText As String = ""

    Private Const MIN_MIDIFILE_SIZE = 14                    ' minimal size of a Midi file (Header)
    Private Const MAX_MIDIFILE_SIZE = 10 * 1024 * 1024      ' maximal size of a Midi file (myLimit)
    Private Const MinHeaderDataLenght = 6                   ' MinDataLength of Midi-File Header (normal = 6)
    Private Const MAX_NUMBER_OF_TRACKS = 120                ' maximal number of tracks (myLimit)

    '--- Header ---

    Private FileSize As Long
    Public ReadOnly Property SmfFormat As Integer           ' 0 or 1 or 2
    Public ReadOnly Property NumberOfTracks As Integer      ' Track count (all Tracks, including unknown)
    Public ReadOnly Property TPQ As Integer                 ' Ticks per QuaterNote

    'Private HeaderDateLength As Integer                     ' for calculating the next Track-Position

    '---

    Public ReadOnly Property TimeSignature_Num As Integer = 4           ' Numerator 4 (default TiSig.4/4)
    Public ReadOnly Property TimeSignature_Denom As Integer = 4         ' Nenner 4 (default TiSig.4/4)
    Public ReadOnly Property BPM As Single = 120

    Public ReadOnly Property LastTick As UInteger       ' greatest event time of the last event of all tracks

    '---

    Public TrackList As New List(Of TrackChunk)                    ' only Tracks with "MTrk"-Sig.

    Public Class TrackChunk
        ''' <summary>
        ''' Position of Datablock in SourceStream
        ''' </summary>
        Public DataPosition As Integer                              ' Position of Datablock in SourceStream
        ''' <summary>
        ''' Number of bytes in Datablock
        ''' </summary>
        Public DataLength As Integer                                ' Number of bytes in Datablock     
        ''' <summary>
        ''' List of TrackEvents
        ''' </summary>
        Public EventList As New List(Of TrackEvent)
        ''' <summary>
        ''' for Player (current Event)
        ''' </summary>
        Public EventPtr As Integer                                  ' for Player (current Event)
        ''' <summary>
        ''' for Player (True if end reached)
        ''' </summary>
        Public EndOfTrack As Boolean                                ' for Player (True if end reached)
        ''' <summary>
        ''' If True: skip NoteOn-Events in Player
        ''' </summary>
        Public Mute As Boolean                                      ' True = skip NoteOn-Events in Player
        ''' <summary>
        ''' Track Filter for Aux-Operations
        ''' </summary>
        Public XSelect As Boolean                                   ' Track Filter for Aux-Operations
    End Class

    <Serializable> Public Class TrackEvent
        ' For the WPF data binding, the members must be defined as properties (not as fields) 
        ' (otherwise nothing or only the type name is displayed in a ListView)
        ''' <summary>
        ''' [Ticks] for all Events
        ''' </summary>        
        Public Property Time As UInteger                 '                   for all Events        
        Public Property Type As EventType                '                   for all Events
        Public Property Status As Byte                   ' Status            for MidiEvents
        'Public Channel As Byte                  ' Channel (0-0Fh)   for MidiEvents  (needed ?)
        ''' <summary>
        ''' If MidiEvent: Data-Byte 1 / If MetaEvent: MetaEventType
        ''' </summary>
        Public Property Data1 As Byte                    ' Data 1            for MidiEvents and MetaEvents
        ''' <summary>
        ''' for MidiEvents
        ''' </summary>
        Public Property Data2 As Byte                    ' Data 2            for MidiEvents
        ''' <summary>
        ''' Data Array for MetaEvents and SysxEvents
        ''' </summary>
        Public Property DataX As Byte()                   ' Data Array        for MetaEvents and SysxEvents
        ''' <summary>
        ''' Aux for Note-On Events. Calcualted from Time until Note-off. 0 if no Note-Off.
        ''' Can be used for Graphical User Interface.
        ''' </summary>
        Public Property Duration As UInteger
        ''' <summary>
        ''' For filtering, muting, ... / TrackNumbers > 255 will be set to 255 / First Track = 0
        ''' </summary>
        Public Property TrackNumber As Byte
    End Class

    ''' <summary>
    ''' For Pattern Export
    ''' </summary>
    <Serializable> Public Class CPattern
        Public Name As String                       ' Pattern name
        Public Kategorie As String                  ' 
        Public Gruppe As String                     '
        Public Source As String                     ' f.e. the device name, MIDI, ...
        Public BPM As Integer                       ' BeatsPerMinute (Tempo)
        Public TPQ As Integer                       ' TicksPerQuarterNote (=960)
        Public Length As Integer                    ' Number of Beats
        Public EventList As List(Of TrackEvent)
        Public VoiceMap As CVoiceMap                ' for 16 Midi-Channels: Voice,BamkMSB,BankLSB
    End Class


    Public VoiceMap As New CVoiceMap
    ''' <summary>
    ''' For Pattern Export. Map for 16 Midi-Channels (0-15) Voice from ProgramChange
    '''  and BankMSB / BankLSB from ControlChange. Reflects State at current PlayerPosition
    ''' </summary>
    <Serializable>
    Public Class CVoiceMap
        Public Items As New List(Of CVoiceMapEntry)
        Public Sub New()

            For i = 1 To 16
                Items.Add(New CVoiceMapEntry)
            Next

            Reset()
        End Sub

        ''' <summary>
        ''' Set all Entries to 80h (not set)
        ''' </summary>
        Public Sub Reset()
            For i = 1 To Items.Count
                Items(i - 1).VoiceNumberGM = &H80     ' 80h = not set
                Items(i - 1).VoiceNumber = &H80       ' 80h = not set
                Items(i - 1).BankMSB = &H80           ' 80h = not set
                Items(i - 1).BankLSB = &H80           ' 80h = not set
            Next
        End Sub

        Public Function GetVoiceNumberGM(channel As Byte) As Byte
            channel = CByte(channel And &HF)        ' only valid from 0 to 0fh
            Return Items(channel).VoiceNumberGM
        End Function
        Public Function GetVoiceNumber(channel As Byte) As Byte
            channel = CByte(channel And &HF)        ' only valid from 0 to 0fh
            Return Items(channel).VoiceNumber
        End Function
        Public Function GetBankMSB(channel As Byte) As Byte
            channel = CByte(channel And &HF)        '  only valid from 0 to 0fh
            Return Items(channel).VoiceNumber
        End Function
        Public Function GetBankLSB(channel As Byte) As Byte
            channel = CByte(channel And &HF)        '  only valid from 0 to 0fh
            Return Items(channel).VoiceNumber
        End Function

        Public Sub SetVoiceNumberGM(channel As Byte, VoiceNumberGM As Byte)
            channel = CByte(channel And &HF)                ' only valid from 0 to 0fh
            VoiceNumberGM = CByte(VoiceNumberGM And &H7F)   ' only valid from 0 to 127 (7F)
            Items(channel).VoiceNumberGM = VoiceNumberGM
        End Sub
        Public Sub SetVoiceNumber(channel As Byte, VoiceNumber As Byte)
            channel = CByte(channel And &HF)            ' only valid from 0 to 0fh
            VoiceNumber = CByte(VoiceNumber And &H7F)   ' only valid from 0 to 127 (7F)
            Items(channel).VoiceNumber = VoiceNumber
        End Sub
        Public Sub SetBankMSB(channel As Byte, BankMSB As Byte)
            channel = CByte(channel And &HF)            ' only valid from 0 to 0fh
            BankMSB = CByte(BankMSB And &H7F)           ' only valid from 0 to 127 (7F)
            Items(channel).BankMSB = BankMSB
        End Sub
        Public Sub SetBankLSB(channel As Byte, BankLSB As Byte)
            channel = CByte(channel And &HF)            ' only valid from 0 to 0fh
            BankLSB = CByte(BankLSB And &H7F)           ' only valid from 0 to 127 (7F)
            Items(channel).BankLSB = BankLSB
        End Sub

        <Serializable>
        Public Class CVoiceMapEntry
            Public VoiceNumberGM As Byte
            Public VoiceNumber As Byte
            Public BankMSB As Byte
            Public BankLSB As Byte
        End Class
    End Class

    Private RunningStatusByte As Byte                                   ' cache

    Public Function ReadMidiFile(fullname As String) As Boolean

        ' Release resources if the file is already loaded

        If PlayerRunning Then
            StopPlayer()
        End If

        Initialize()                                                   ' load defaults

        If fullname Is Nothing Or fullname = "" Then
            ErrorCode = MiErr_EmptyName
            ErrorText = "No filename"
            Return False
        End If

        '--- verify header

        If Check_MidiFileHeader(fullname) = False Then
            If ErrorCode = MiErr_NoError Then
                ErrorCode = MiErr_Unknown
            End If
            Return False
        End If

        '--- verify file (data lengths)

        If Check_MidiFile(fullname) = False Then
            If ErrorCode = MiErr_NoError Then
                ErrorCode = MiErr_Unknown
            End If
            Return False
        End If

        '--- create TrackList (TrackChunks with "MTrk")

        createTrackList(fullname)

        createEventLists(fullname)

        DeltaTimeToAbs()                    ' Delta time to continuous time

        '--- calculate durations
        ' try to calculate the duration of note-on's from 'time' (for GUI)

        CalculateDurations()                ' unsuccessful events: Duration = 0

        '--- Reset Player-vars

        For i = 1 To TrackList.Count
            TrackList(i - 1).EventPtr = 0
            TrackList(i - 1).EndOfTrack = False
        Next

        '--- find LastTick

        Dim time As UInteger

        For i = 1 To TrackList.Count

            If TrackList(i - 1).EventList.Count > 0 Then
                time = TrackList(i - 1).EventList.Last().Time
                If time > LastTick Then
                    _LastTick = time
                End If
            End If
        Next

        '--- successfully loaded

        _MidiFullname = fullname
        _MidiName = Path.GetFileName(fullname)
        '_MidiName = Path.GetFileNameWithoutExtension(fullname)
        _FileLoaded = True

        Return True
    End Function

    Private Sub Initialize()
        _FileLoaded = False
        _MidiFullname = ""
        ErrorText = ""
        ErrorCode = 0
        _TimeSignature_Num = 4
        _TimeSignature_Denom = 4
        _BPM = 120
        _LastTick = 0

        VoiceMap.Reset()                    ' Reset Map (used for exporting Pattern)
    End Sub

    Private Sub DeltaTimeToAbs()

        Dim time As UInteger

        For t = 1 To TrackList.Count

            time = 0
            For e = 1 To TrackList(t - 1).EventList.Count
                time += TrackList(t - 1).EventList(e - 1).Time
                TrackList(t - 1).EventList(e - 1).Time = time
            Next

        Next
    End Sub

    Private Sub CalculateDurations()

        Dim ev As TrackEvent
        Dim stat As Byte

        For t = 1 To TrackList.Count

            For e = 1 To TrackList(t - 1).EventList.Count
                ev = TrackList(t - 1).EventList(e - 1)
                If ev.Type = EventType.MidiEvent Then
                    stat = ev.Status
                    If stat < &HA0 Then
                        If stat > &H8F Then
                            If ev.Data2 > 0 Then
                                ev.Duration = FindNoteOff(TrackList(t - 1).EventList, (e - 1))
                            End If
                        End If
                    End If
                End If
            Next
        Next

    End Sub

    Private Function FindNoteOff(elist As List(Of TrackEvent), position As Integer) As UInteger

        ' assume: current Event is Note-On

        Dim ev As TrackEvent

        Dim stat As Byte                        ' assuming 9nh
        Dim channel As Byte
        Dim noteNum As Byte
        Dim start_time As UInteger

        ' find 1: 

        '--- current event

        ev = elist(position)

        stat = ev.Status
        channel = CByte(ev.Status And &HF)
        noteNum = ev.Data1
        start_time = ev.Time

        '--- find note-off

        For e = position + 2 To elist.Count
            ev = elist(e - 1)

            If ev.Status = stat Then                        ' 9nh
                If ev.Data1 = noteNum Then                  ' same NoteNumber
                    'If ev.Data2 = 0 Then                   ' velocity = 0
                    Return ev.Time - start_time
                    'End If
                End If
            ElseIf ev.Status = stat - &H10 Then             ' 8nh
                If ev.Data1 = noteNum Then                  ' same NoteNumber
                    Return ev.Time - start_time
                End If
            End If

            '  ev.stat = stat , velo = 0            9nh
            '  ev.stat = stat -10h                  8nh
        Next

        Return 0                                    ' not found
    End Function

    Private Sub createEventLists(fullname As String)

        Dim fi As New FileInfo(fullname)
        Dim reader As New BinaryReader(fi.Open(FileMode.Open, FileAccess.Read, FileShare.Read))

        Dim el As List(Of TrackEvent)
        Dim ev As TrackEvent
        Dim endOfTrack As Boolean

        For i = 1 To TrackList.Count

            TrackList(i - 1).EventList.Clear()
            el = TrackList(i - 1).EventList
            reader.BaseStream.Position = TrackList(i - 1).DataPosition

            Do
                ev = New TrackEvent
                ev.Time = ReadDeltaTime(reader)
                endOfTrack = ReadEvent(reader, ev)      ' return TRUE if EndOfTrack reached

                If i <= 256 Then
                    ev.TrackNumber = CByte(i - 1)
                Else
                    ev.TrackNumber = 255
                End If

                el.Add(ev)                              ' add event to list

            Loop Until endOfTrack = True
        Next

        reader.Close()                                  ' release unmanaged resources (or using) 

    End Sub


    Private Function ReadEvent(reader As BinaryReader, ByRef ev As TrackEvent) As Boolean

        Dim Data0 As Byte
        Data0 = reader.ReadByte
        Dim length As Integer

        If Data0 < &HF0 Then                             ' channel event ( 8,9,A,B,C,D,E)
            ev.Type = EventType.MidiEvent

            If Data0 >= &H80 Then
                RunningStatusByte = Data0
                ev.Status = Data0
            Else                                        ' running state
                ev.Status = RunningStatusByte
                reader.BaseStream.Position -= 1         ' back to first Datenbyte
            End If

            ev.Data1 = reader.ReadByte                  ' first Databyte fo all

            Dim dat As Byte = CByte(Data0 And &HF0)     ' C + D = 1 Databyte
            If Not (dat = &HC0 Or dat = &HD0) Then
                ev.Data2 = reader.ReadByte              ' others = 2 Databytes
            End If

        ElseIf Data0 = EventType.MetaEvent Then          ' FF
            ev.Type = EventType.MetaEvent
            ev.Data1 = reader.ReadByte                  ' MetaEventType
            length = ReadVariableLength(reader)         ' Data length (can be 0)            
            ev.DataX = reader.ReadBytes(length)          ' read to Byte()

            If ev.Data1 = &H2F Then                     ' FF 2F 00 = End of Track
                Return True
            End If

        ElseIf Data0 = EventType.F0SysxEvent Then        ' F0
            ev.Type = EventType.F0SysxEvent
            length = ReadVariableLength(reader)         ' Data length            
            ev.DataX = reader.ReadBytes(length)          ' read to Byte()

        ElseIf Data0 = EventType.F7SysxEvent Then       ' F7
            ev.Type = EventType.F7SysxEvent
            length = ReadVariableLength(reader)         ' Data length             
            ev.DataX = reader.ReadBytes(length)          ' read to Byte()

        Else
            ev.Type = EventType.Unkown                  ' neither MidiEvent nor MetaEvent nor SysxEvent
            Return False
        End If

        Return False
    End Function

    Private Function ReadDeltaTime(reader As BinaryReader) As UInteger

        Dim time As UInteger
        Dim b1 As Byte

        Try
            For i = 1 To 4
                b1 = reader.ReadByte
                time = time << 7
                time = CUInt(time Or (b1 And &H7F))
                If b1 < &H80 Then Exit Try
            Next
        Catch
            Return 0
        End Try

        Return time
    End Function


    Private Function ReadVariableLength(reader As BinaryReader) As Integer

        Dim length As Integer
        Dim b1 As Byte

        Try
            For i = 1 To 4
                b1 = reader.ReadByte
                length = length << 7
                length = length Or (b1 And &H7F)
                If b1 < &H80 Then Exit Try
            Next
        Catch
            Return 0
        End Try

        Return length
    End Function

    Private Sub createTrackList(fullname As String)
        'Header und File are already checked

        TrackList.Clear()

        Dim fi As New FileInfo(fullname)

        Dim reader As New BinaryReader(fi.Open(FileMode.Open, FileAccess.Read, FileShare.Read))

        Dim chunkType As Char()
        Dim DataLength As UInteger

        ' Position = 0

        '--- Header
        chunkType = reader.ReadChars(4)
        DataLength = BeToUInt(reader.ReadUInt32)
        reader.BaseStream.Position += DataLength        ' HeaderSig(4) + DataLen(4) + DataLength

        '--- Tracks
        For i = 1 To NumberOfTracks
            chunkType = reader.ReadChars(4)
            DataLength = BeToUInt(reader.ReadUInt32)
            If chunkType = "MTrk" Then
                TrackList.Add(New TrackChunk With
                              {.DataLength = CInt(DataLength), .DataPosition = CInt(reader.BaseStream.Position)})
            End If
            reader.BaseStream.Position += DataLength    ' HeaderSig(4) + DataLen(4) + DataLength
        Next

        reader.Close()                                  ' release unmanaged resources (or using) 

    End Sub

    Private Function Check_MidiFileHeader(fullname As String) As Boolean

        Dim fi As New FileInfo(fullname)

        If fi.Exists = False Then
            ErrorCode = MiErr_FileNotExists
            ErrorText = "file not found"
            Return False
        End If

        If fi.Length < MIN_MIDIFILE_SIZE Then
            ErrorCode = MiErr_FileTooShort
            ErrorText = "invalid file size  " & CStr(fi.Length)
            Return False
        End If

        If fi.Length > MAX_MIDIFILE_SIZE Then
            ErrorCode = MiErr_FileTooLong
            ErrorText = "File is too big (application restriction)  "
            Return False
        End If


        FileSize = fi.Length                    ' Length of the file

        '----

        Dim chunkType As Char()
        Dim length As UInteger
        Dim format As UShort
        Dim ntracks As UShort
        Dim division As UShort

        Dim reader As New BinaryReader(fi.Open(FileMode.Open, FileAccess.Read, FileShare.Read))

        chunkType = reader.ReadChars(4)
        length = BeToUInt(reader.ReadUInt32)
        format = BeToUShort(reader.ReadUInt16)
        ntracks = BeToUShort(reader.ReadUInt16)
        division = BeToUShort(reader.ReadUInt16)

        reader.Close()                                  ' release unmanaged resources

        If chunkType <> "MThd" Then
            ErrorCode = MiErr_HeaderChunkSignature
            ErrorText = "Invalid header-chunk-type signature"
            Return False
        End If

        If length < MinHeaderDataLenght Then                ' normally = 6 (00 00 00 06)
            '                                               ' could get bigger in the future
            ErrorCode = MiErr_MinHeaderDataLength
            ErrorText = "Invalid header length"
            Return False
        End If

        If (division >> 15) > 0 Then
            ErrorCode = MiErr_TimeFormat_SMPTE
            ErrorText = "SMPTE format is currently not supported"
            Return False
        End If

        If format > 2 Then
            ErrorCode = MiErr_InvalidMidiFormat
            ErrorText = "Invalid midi format number"
            Return False
        End If

        If ntracks = 0 Then
            ErrorCode = MiErr_HeaderNoTracks
            ErrorText = "Number of tracks in the header is 0"
            Return False
        End If

        If format = 0 Then
            If ntracks > 1 Then
                ErrorCode = MiErr_Format0_MoreThanOneTrack
                ErrorText = "Format 0 cannot have more than 1 track"
                Return False
            End If
        ElseIf format = 1 Then
            If ntracks > MAX_NUMBER_OF_TRACKS Then
                ErrorCode = MiErr_Format1_TooManyTracks
                ErrorText = "Format 1 has too many tracks for this application"
            End If

        ElseIf format = 2 Then
            If ntracks > 1 Then
                ErrorCode = MiErr_Format0_MoreThanOneTrack
                ErrorText = "Format 2 cannot have more than 1 track"
                Return False
            End If
        End If

        If division = 0 Then
            ErrorCode = MiErr_DivisionIsNull
            ErrorText = "Division (TPQ) is null"
            Return False
        End If

        ' Header ist valid

        _SmfFormat = format
        _NumberOfTracks = ntracks
        _TPQ = division

        Return True
    End Function


    Private Function Check_MidiFile(fullname As String) As Boolean
        ' Header ist already checked

        Dim fi As New FileInfo(fullname)

        Dim reader As New BinaryReader(fi.Open(FileMode.Open, FileAccess.Read, FileShare.Read))

        Dim chunkType As Char()
        Dim DataLength As UInteger
        Dim ret As Boolean = True                       ' return value

        ' Position = 0

        Try
            '--- Header
            chunkType = reader.ReadChars(4)
            DataLength = BeToUInt(reader.ReadUInt32)
            reader.BaseStream.Position += DataLength        ' HeaderSig(4) + DataLen(4) + DataLength

            '--- Tracks
            For i = 1 To NumberOfTracks
                chunkType = reader.ReadChars(4)
                DataLength = BeToUInt(reader.ReadUInt32)
                reader.BaseStream.Position += DataLength    ' HeaderSig(4) + DataLen(4) + DataLength
            Next
        Catch
            ret = False                                 ' error
            ErrorCode = MiErr_ReadingChunkChain
            ErrorText = "Exception while reading Chunk-Chain"
        End Try

        'If Not reader.BaseStream.Position = reader.BaseStream.Length Then
        '    ret = False
        '    ErrorCode = MiErr_ReadingChunkChain
        '    ErrorText = "Wrong data-length in last Chunk"
        'End If

        reader.Close()                                  ' release unmanaged resources (or using)

        Return ret
    End Function


    ''' <summary>
    ''' Conversion from Big-Endian to Little-Endian Format. 4 Bytes to UInteger
    ''' </summary>
    ''' <param name="BigEndian"></param>
    ''' <returns></returns>
    Private Function BeToUInt(BigEndian As UInteger) As UInteger

        Dim ret As UInteger

        Dim b1 As Byte
        Dim b2 As Byte
        Dim b3 As Byte
        Dim b4 As Byte

        b1 = CByte(BigEndian >> 24 And &HFF)
        b2 = CByte(BigEndian >> 16 And &HFF)
        b3 = CByte(BigEndian >> 8 And &HFF)
        b4 = CByte(BigEndian And &HFF)

        ret = b4
        ret = (ret << 8) Or b3
        ret = (ret << 8) Or b2
        ret = (ret << 8) Or b1

        Return ret
    End Function

    Private Function BeToUShort(BigEndian As UShort) As UShort

        Dim ret As UShort

        Dim b1 As Byte
        Dim b2 As Byte

        b1 = CByte(BigEndian >> 8 And &HFF)
        b2 = CByte(BigEndian And &HFF)

        ret = b2
        ret = (ret << 8) Or b1

        Return ret
    End Function

End Class

'sysx normal
' F0 <length> <bytes to be transmitted after F0>
' sysx escape
' F7 <length> <all bytes to be transmitted>

Public Enum EventType
    Unkown = 0
    MidiEvent = 1                   ' channel message
    MetaEvent = &HFF                ' MetaEvent
    F0SysxEvent = &HF0              ' normal sysx
    F7SysxEvent = &HF7              ' escape sysx
End Enum

Public Enum MidiEventType
    NoteOffEvent = &H80
    NoteOnEvent = &H90
    PolyKeyPressure = &HA0
    ControlChange = &HB0
    ProgramChange = &HC0
    ChannelPressure = &HD0
    PitchBend = &HE0
End Enum

Public Enum MetaEventType
    SequenceNumber = 0
    TextEvent = 1
    CopyrightNotice = 2
    SequenceOrTrackName = 3
    InstrumentName = 4
    Lyric = 5
    Marker = 6
    CuePoint = 7
    MIDIChannelPrefix = &H20
    EndOfTrack = &H2F
    SetTempo = &H51
    SMPTEOffset = &H54
    TimeSignature = &H58
    KeySignature = &H59
    SequencerSpecific = &H7F
End Enum