Imports System.Text
Public Module MDecode

    Public Function GetSubType(ev As CMidiFile.TrackEvent) As String

        If ev.Type = EventType.MidiEvent Then
            Return GetSubType_Midi(ev)
        ElseIf ev.Type = EventType.MetaEvent Then
            Return CType(ev.Data1, MetaEventType).ToString()
        ElseIf ev.Type = EventType.F0SysxEvent Then
            Return "F0"
        ElseIf ev.Type = EventType.F7SysxEvent Then
            Return "F7"
        ElseIf ev.Type = EventType.Unkown Then
            Return "unknown EventType"
        End If

        Return "not found"
    End Function

    Private Function GetSubType_Midi(ev As CMidiFile.TrackEvent) As String
        Dim stat As Byte = CByte(ev.Status And &HF0)
        If (stat = &H90 And ev.Data2 = 0) OrElse (stat = &H80) Then Return MidiEventType.NoteOffEvent.ToString
        Return CType(stat, MidiEventType).ToString()
    End Function

    Public Function GetChannel(ev As CMidiFile.TrackEvent) As String
        If ev.Type = EventType.MidiEvent Then
            Return Hex(ev.Status And &HF)
        Else
            Return Hex(ev.Status And &HF)
            'Return ""
        End If
    End Function

    Public Function GetData(ev As CMidiFile.TrackEvent) As String

        Select Case ev.Type
            Case EventType.MidiEvent
                Return GetData_Midi(ev)
            Case EventType.MetaEvent
                Return GetData_Meta(ev)
            Case EventType.F0SysxEvent
                ' Universal Sysx (7F=Realtime,
                ' Universal Sysx (7E=NonRealTime), 7F=Channel, 09=SubID=GM System,01=Enable
                Return Bytes_to_hex_str(ev.DataX)        ' other Sysx
            Case EventType.F7SysxEvent
                Return Bytes_to_hex_str(ev.DataX)
            Case EventType.Unkown
        End Select

        Return ""
    End Function

    Private Function GetData_Midi(ev As CMidiFile.TrackEvent) As String
        Dim stat As Byte = CByte(ev.Status And &HF0)

        If stat = MidiEventType.NoteOffEvent Then
            Return Hex(ev.Status) & " " & Hex(ev.Data1) & " " & Hex(ev.Data2) & "  -  " & NoteNr_to_NoteName(ev.Data1)

        ElseIf stat = MidiEventType.NoteOnEvent Then
            Return Hex(ev.Status) & " " & Hex(ev.Data1) & " " & Hex(ev.Data2) & "  -  " & NoteNr_to_NoteName(ev.Data1)

        ElseIf stat = MidiEventType.PolyKeyPressure Then

        ElseIf stat = MidiEventType.ControlChange Then
            Dim str As String
            str = "Ctrl: " & ev.Data1 & "  val: " & ev.Data2 & "  -  "
            str = str & GetControllerName(ev.Data1)
            Return str
        ElseIf stat = MidiEventType.ProgramChange Then
            Return CStr(ev.Data1) & " - " & GetVoiceName(ev.Data1)
        ElseIf stat = MidiEventType.ChannelPressure Then

        ElseIf stat = MidiEventType.PitchBend Then
            Return CStr(ev.Data2 * 128 + ev.Data1 - 8192)              ' center = 8192
            ' byte 2 * 128 + byte 1
        End If

        Return ""
    End Function

    Private Function GetData_Meta(ev As CMidiFile.TrackEvent) As String

        Dim type As Byte = ev.Data1

        Dim ascii As Encoding = Encoding.ASCII


        If type = MetaEventType.SequenceNumber Then
            If ev.DataX.Length > 0 Then
                Return Bytes_to_hex_str(ev.DataX)
            Else
                Return "len = 0"
            End If
        ElseIf type = MetaEventType.TextEvent Then
            Return GetMetaEventText(ev)
        ElseIf type = MetaEventType.CopyrightNotice Then
            Return GetMetaEventText(ev)
        ElseIf type = MetaEventType.SequenceOrTrackName Then
            Return GetMetaEventText(ev)
        ElseIf type = MetaEventType.InstrumentName Then
            Return GetMetaEventText(ev)
        ElseIf type = MetaEventType.Lyric Then
            Return GetMetaEventText(ev)
        ElseIf type = MetaEventType.Marker Then
            Return GetMetaEventText(ev)
        ElseIf type = MetaEventType.CuePoint Then
            Return GetMetaEventText(ev)
        ElseIf type = MetaEventType.MIDIChannelPrefix Then
            If ev.DataX.Length > 0 Then                          ' should be 1
                Return CStr(ev.DataX(0))                         ' channel (0-15)
            Else
                Return ""
            End If
        ElseIf type = MetaEventType.EndOfTrack Then
            Return ""                                           ' no data
        ElseIf type = MetaEventType.SetTempo Then
            Dim micros As Integer
            micros = ev.DataX(0) * 65536 + ev.DataX(1) * 256 + ev.DataX(2)
            Return micros.ToString & "   " & Math.Round(60 * 1000 * 1000 / micros, 2)   ' 2 Decimal places
        ElseIf type = MetaEventType.SMPTEOffset Then
            Return "not supported"
        ElseIf type = MetaEventType.TimeSignature Then
            Dim num As Byte = ev.DataX(0)                        ' numerator (Zähler)
            Dim denom As Byte = CByte(2 ^ ev.DataX(1))           ' denominator (denominator, 2 ^ denominator
            Dim mclocks_metronclick As Byte = ev.DataX(2)        ' clocks per metronome click
            Dim num32perquarter As Byte = ev.DataX(3)            ' no of 32/notes in a QuarterNote (24)            
            Return num & "/" & denom & " " & mclocks_metronclick & " " & num32perquarter
        ElseIf type = MetaEventType.KeySignature Then
            ' C  min/maj
            If ev.DataX.Length > 0 Then
                Return Bytes_to_hex_str(ev.DataX)
            Else
                Return "len = 0"
            End If
        ElseIf type = MetaEventType.SequencerSpecific Then
            If ev.DataX.Length > 0 Then
                Return Bytes_to_hex_str(ev.DataX)
            Else
                Return "len = 0"
            End If
        Else
            If ev.DataX.Length > 0 Then
                Return "unknown, " & Bytes_to_hex_str(ev.DataX)
            Else
                Return "unkown, len = 0"
            End If

        End If
        Return ""

    End Function

    Private Function GetMetaEventText(ev As CMidiFile.TrackEvent) As String
        Dim ascii As Encoding = Encoding.ASCII
        Dim chr As Char()

        If ev.DataX.Length > 0 Then
            chr = ascii.GetChars(ev.DataX)
            Return chr
        Else
            Return "len = 0"
        End If
    End Function

    Public Function GetDuration(ev As CMidiFile.TrackEvent) As String

        Return CStr(ev.Duration)

        Return ""
    End Function

    Private Class C_octave_key
        Public Property offset As Integer
        Public Property name As String          ' key name
        Public Property white As Boolean        ' is it a white key
        Public Property shape As Integer        ' shape-type (for painting)
        Public Property xbase As Integer        ' pos-x base for painting (nr of white keys from octave-start)
    End Class

    Private octave_keys As New List(Of C_octave_key) From
        {
        New C_octave_key With {.offset = 0, .name = "C", .white = True, .shape = shp_w_r, .xbase = 0},
        New C_octave_key With {.offset = 1, .name = "C#", .white = False, .shape = shp_blk, .xbase = 1},
        New C_octave_key With {.offset = 2, .name = "D", .white = True, .shape = shp_w_lr, .xbase = 1},
        New C_octave_key With {.offset = 3, .name = "D#", .white = False, .shape = shp_blk, .xbase = 2},
        New C_octave_key With {.offset = 4, .name = "E", .white = True, .shape = shp_w_l, .xbase = 2},
        New C_octave_key With {.offset = 5, .name = "F", .white = True, .shape = shp_w_r, .xbase = 3},
        New C_octave_key With {.offset = 6, .name = "F#", .white = False, .shape = shp_blk, .xbase = 4},
        New C_octave_key With {.offset = 7, .name = "G", .white = True, .shape = shp_w_lr, .xbase = 4},
        New C_octave_key With {.offset = 8, .name = "G#", .white = False, .shape = shp_blk, .xbase = 5},
        New C_octave_key With {.offset = 9, .name = "A", .white = True, .shape = shp_w_lr, .xbase = 5},
        New C_octave_key With {.offset = 10, .name = "A#", .white = False, .shape = shp_blk, .xbase = 6},
        New C_octave_key With {.offset = 11, .name = "B", .white = True, .shape = shp_w_l, .xbase = 6}
        }

    'possible key-shapes
    Private Const shp_blk = 0           ' black key                                 5*
    Private Const shp_w_lr = 1          ' white key, black on left and right        3*
    Private Const shp_w_l = 2           ' white key, black on left                  2*
    Private Const shp_w_r = 3           ' white key, black on right                 2*

    ''' <summary>
    ''' Note-number to Note Name (f.e. 60 to 'C 4')
    ''' </summary>
    ''' <param name="NoteNr"></param>
    ''' <returns></returns>
    Private Function NoteNr_to_NoteName(noteNr As Integer) As String

        If noteNr > 127 Then
            Return ""                   ' return empty String if noteNr is invalid
        End If

        Return octave_keys(noteNr Mod 12).name & " " & (noteNr \ 12) - 1

        ' A4 = 440Hz = NoteNr: 69 (Def: Middle C = C4 = NoteNr: 60)
    End Function


    Public Function Bytes_to_hex_str(ByRef src As Byte()) As String

        Dim i, p As Integer
        Dim str As String = ""

        If src.Count > 0 Then

            Dim array(src.Count() * 3) As Byte
            p = 0
            For i = 0 To src.Count - 1
                array(p) = CByte(Asc(Hex(src(i) >> 4)))
                array(p + 1) = CByte(Asc(Hex(src(i) And &HF)))
                array(p + 2) = &H20
                p = p + 3
            Next

            ' or:
            'str = BitConverter.ToString(Buffer, BufferOffset, BytesPerRow)
            'str = str.Replace("-", " ")

            str = System.Text.Encoding.ASCII.GetString(array)

        End If

        Return str
    End Function


End Module
