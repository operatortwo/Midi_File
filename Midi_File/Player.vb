Partial Public Class CMidiFile

    Public Event PlayerStarted()
    Public Event PlayerStopped()

    Public Event OutShortMsg(Track As Integer, Status As Byte, Data1 As Byte, Data2 As Byte)
    ' for Note-, Controller-,.. Events
    ' Track = MidiTrackNr für filtern nach Track, z.B. Mute)

    Public Event PlayerEvent(TrackEvent As TrackEvent)
    Public Property Enable_PlayerEvent As Boolean       ' save ressources if not needed      

    '--- HiRes Timer 

    Declare Auto Function timeBeginPeriod Lib "winmm.dll" (uPeriod As UInteger) As UInteger
    Declare Auto Function timeEndPeriod Lib "winmm.dll" (uPeriod As UInteger) As UInteger

    Declare Auto Function timeSetEvent Lib "winmm.dll" (uDelay As UInteger, uResolution As UInteger, lpTimeProc As TimerProc, dwUser As IntPtr, fuEvent As UInteger) As UInteger
    Declare Auto Function timeKillEvent Lib "winmm.dll" (uTimerID As UInteger) As UInteger

    Private TimerID As UInteger

    Private Const TIME_PERIODIC = 1

    Delegate Sub TimerProc(uID As UInteger, uMsg As UInteger, dwUser As UInteger, dw1 As UInteger, dw2 As UInteger)
    Private ReadOnly fptrTimeProc As New TimerProc(AddressOf TickCallback)

    Private _TimerInterval As UInteger = 3
    ''' <summary>
    ''' Between 1 and 10 Milliseconds. Default = 3
    ''' </summary>
    ''' <returns></returns>
    Public Property TimerInterval As UInteger
        Get
            Return _TimerInterval
        End Get
        Set(value As UInteger)
            If value > 10 Then
                value = 10
            ElseIf value < 1 Then
                value = 1
            End If
            _TimerInterval = value
        End Set
    End Property

    Private _TimerResolution As UInteger = 3
    ''' <summary>
    ''' Between 0 and 10 , 0 = most accurate. Default = 3
    ''' </summary>
    ''' <returns></returns>
    Public Property TimerResolution As UInteger
        Get
            Return _TimerResolution
        End Get
        Set(value As UInteger)
            If value > 10 Then
                value = 10
            End If
            _TimerResolution = value
        End Set
    End Property

    '--- Player

    Private PlayerStopwatch As New Stopwatch             ' for Player

    Public ReadOnly Property PlayerRunning As Boolean
    Public ReadOnly Property PlayerPaused As Boolean

    Private LastPlayerTick As Long                      ' last PlayerStopwatch.elapsedTicks
    Public PlayerPosition As Double                     ' (Song Ticks)

    Private DisablePlayer As Boolean                     ' while PlayerMoveTo

    Private Sub TickCallback(uID As UInteger, uMsg As UInteger, dwUser As UInteger, dw1 As UInteger, dw2 As UInteger)
        Dim currentTick As Long = PlayerStopwatch.ElapsedTicks
        Dim DeltaTicks As Long                                      'stopwatch ticks
        Dim DeltaSongTicks As Double                                ' player ticks

        DeltaTicks = currentTick - LastPlayerTick
        LastPlayerTick = currentTick

        Dim DeltaMilliSeconds As Double = DeltaTicks / Stopwatch.Frequency * 1000

        ' Ticks = time(ms) * BPM * TPQ / 60'000
        ' Ticks = time(sec) * BPM * TPQ / 60
        ' Ticks = time(sec) * BPM * 16
        DeltaSongTicks = DeltaTicks / Stopwatch.Frequency * BPM * TPQ / 60

        PlayerPosition += DeltaSongTicks

        If DisablePlayer = False Then
            Player()                                                      ' play events
        End If
    End Sub

    Private Sub Player()

        Dim ev As TrackEvent

        For i = 1 To TrackList.Count
            'For i = 1 To 2

            If TrackList(i - 1).EndOfTrack = False Then

                ev = TrackList(i - 1).EventList(TrackList(i - 1).EventPtr)

                While ev.Time <= PlayerPosition

                    If ev.Type = EventType.MidiEvent Then

                        ' If Track Mute = True und NoteOn event skip output

                        If TrackList(i - 1).Mute = False Then
                            RaiseEvent OutShortMsg(i - 1, ev.Status, ev.Data1, ev.Data2)
                        ElseIf CByte(ev.Status And &HF0) = MidiEventType.NoteOnEvent Then

                        Else
                            RaiseEvent OutShortMsg(i - 1, ev.Status, ev.Data1, ev.Data2)
                        End If

                        If (ev.Status And &HF0) = MidiEventType.ProgramChange Then
                            VoiceMap.SetVoiceNumberGM((CByte(ev.Status And &HF)), ev.Data1)

                        ElseIf (ev.Status And &HF0) = MidiEventType.ControlChange Then
                            If ev.Data1 = 0 Then            ' BankSelect MSB
                                VoiceMap.SetBankMSB((CByte(ev.Status And &HF)), ev.Data2)
                            ElseIf ev.Data1 = &H20 Then     ' BankSelect LSB
                                VoiceMap.SetBankLSB((CByte(ev.Status And &HF)), ev.Data2)
                            End If
                        End If

                    ElseIf ev.Type = EventType.MetaEvent Then
                            If ev.Data1 = MetaEventType.EndOfTrack Then
                            TrackList(i - 1).EndOfTrack = True
                            Exit While
                        ElseIf ev.Data1 = MetaEventType.SetTempo Then
                            Dim micros As Integer
                            micros = ev.DataX(0) * 65536 + ev.DataX(1) * 256 + ev.DataX(2)
                            _BPM = CSng(Math.Round(60 * 1000 * 1000 / micros, 2))   ' 2 Decimal places
                        End If
                    End If

                    '--- default = skip, user can enable RaiseEvent if needed
                    If Enable_PlayerEvent = True Then
                        If TrackList(i - 1).Mute = False Then           ' skip Muted Tracks                            
                            RaiseEvent PlayerEvent(ev)
                        End If
                    End If
                    '---

                    TrackList(i - 1).EventPtr += 1                  ' to next event
                    ev = TrackList(i - 1).EventList(TrackList(i - 1).EventPtr)
                End While

            End If

            If PlayerPosition > LastTick Then
                StopPlayer()
            End If

        Next

    End Sub
    ''' <summary>
    ''' Begin Midi-Playback
    ''' </summary>
    Public Sub StartPlayer()
        If FileLoaded = False Then Exit Sub             ' if nothing to play
        If PlayerRunning = True Then Exit Sub           ' if already started

        For i = 1 To TrackList.Count
            TrackList(i - 1).EventPtr = 0
            TrackList(i - 1).EndOfTrack = False
        Next

        LastPlayerTick = 0
        PlayerPosition = 0
        _PlayerPaused = False
        PlayerStopwatch.Restart()

        StartPlayerTick()
        _PlayerRunning = True
        RaiseEvent PlayerStarted()
    End Sub

    ''' <summary>
    ''' pause player
    ''' </summary>
    Public Sub PlayerPause()
        If PlayerRunning = False Then Exit Sub
        If _PlayerPaused = True Then Exit Sub               ' if already paused

        _PlayerPaused = True
        PlayerStopwatch.Stop()
        AllNotesOff()
    End Sub

    ''' <summary>
    ''' continue after pause
    ''' </summary>
    Public Sub PlayerContinue()
        If PlayerRunning = False Then Exit Sub
        If _PlayerPaused = False Then Exit Sub              ' if already running

        _PlayerPaused = False
        PlayerStopwatch.Start()
    End Sub

    ''' <summary>
    ''' Toggles PlayerPaused
    ''' </summary>
    Public Sub PlayerPause_toggle()
        If PlayerRunning = False Then Exit Sub

        If PlayerPaused = True Then
            _PlayerPaused = False
            PlayerStopwatch.Start()
        Else
            _PlayerPaused = True
            PlayerStopwatch.Stop()
            AllNotesOff()
        End If

    End Sub
    ''' <summary>
    ''' Stop Midi-Playback
    ''' </summary>
    Public Sub StopPlayer()
        If PlayerRunning = False Then Exit Sub           ' if not started

        StopPlayerTick()
        _PlayerPaused = False
        _PlayerRunning = False

        AllNotesOff()

        RaiseEvent PlayerStopped()
    End Sub

    Private Sub StartPlayerTick()
        If TimerID <> 0 Then Exit Sub
        timeBeginPeriod(TimerResolution)
        TimerID = timeSetEvent(TimerInterval, TimerResolution, fptrTimeProc, IntPtr.Zero, TIME_PERIODIC)
    End Sub

    Private Sub StopPlayerTick()
        If TimerID <> 0 Then
            timeKillEvent(TimerID)
            timeEndPeriod(TimerResolution)
            TimerID = 0
        End If
    End Sub

    Private Sub AllNotesOff()

        Dim stat As Byte

        For i = 0 To &HF
            stat = CByte(i Or &HB0)
            RaiseEvent OutShortMsg(0, stat, &H7B, 0)           ' All Notes Off (B0, 7B, 0)
        Next

    End Sub

    Public Sub PlayerMoveTo(NewPosition As Double)
        If FileLoaded = True Then

            DisablePlayer = True                                ' no calls from PlayerStopwatch

            PlayerStopwatch.Reset()
            LastPlayerTick = 0

            If NewPosition > LastTick Then NewPosition = LastTick - 1

            PlayerPosition = NewPosition

            'Private LastPlayerTick As Long                      ' last PlayerStopwatch.elapsedTicks
            'PlayerStopwatch.ElapsedTicks 


            '--- Reset Player-vars

            For i = 1 To TrackList.Count
                TrackList(i - 1).EventPtr = 0
                TrackList(i - 1).EndOfTrack = False
            Next

            '---

            Dim ev As TrackEvent

            For i = 1 To TrackList.Count
                'For i = 1 To 2

                If TrackList(i - 1).EndOfTrack = False Then

                    ev = TrackList(i - 1).EventList(TrackList(i - 1).EventPtr)

                    While ev.Time <= NewPosition

                        'Form1.MIO.OutShortMsg(Form1.oTrk, stat, data1, data2)

                        If ev.Type = EventType.MidiEvent Then

                            If ev.Status > &H9F Then            ' skip NoteOn / NoteOff
                                RaiseEvent OutShortMsg(i - 1, ev.Status, ev.Data1, ev.Data2)
                            End If

                        ElseIf ev.Type = EventType.MetaEvent Then
                            If ev.Data1 = MetaEventType.EndOfTrack Then
                                TrackList(i - 1).EndOfTrack = True
                                Exit While
                            ElseIf ev.Data1 = MetaEventType.SetTempo Then
                                Dim micros As Integer
                                micros = ev.DataX(0) * 65536 + ev.DataX(1) * 256 + ev.DataX(2)
                                _BPM = CSng(Math.Round(60 * 1000 * 1000 / micros, 2))   ' 2 decimal places
                            End If
                        End If

                        TrackList(i - 1).EventPtr += 1                  ' to next event
                        ev = TrackList(i - 1).EventList(TrackList(i - 1).EventPtr)

                    End While

                End If

                ' If PlayerPosition > LastTick Then
                'StopPlayer()
                'End If

            Next

            DisablePlayer = False                               ' enable calls from PlayerStopwatch

        End If
    End Sub

End Class
