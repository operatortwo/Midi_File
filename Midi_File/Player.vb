Partial Public Class CMidiFile

    Public Event PlayerStarted()
    Public Event PlayerStopped()

    Public Event OutShortMsg(Track As Integer, Status As Byte, Data1 As Byte, Data2 As Byte)
    ' for Note-, Controller-,.. Events
    ' Track = MidiTrackNr für filtern nach Track, z.B. Mute)

    Public Event PlayerEvent(TrackEvent As TrackEvent)
    Public Property Enable_PlayerEvent As Boolean       ' save ressources if not needed      

    '--- HiRes Timer (new)
    '---  ...plan to go back to timeSetEvent (see the documentation file)

    Declare Auto Function CreateTimerQueueTimer Lib "kernel32.dll" (ByRef phNewTimer As IntPtr, hTimerQueue As UInteger, lpTimeProc As TimerProc, cbParam As UInteger, DueTime As UInteger, Period As UInteger, Flags As UInteger) As Boolean
    Declare Auto Function DeleteTimerQueueTimer Lib "kernel32.dll" (hTimerQueue As UInteger, hTimer As UInteger, CompletionEvent As Integer) As Boolean

    Delegate Sub TimerProc(lpParameter As IntPtr, TimerOrWaitFired As Boolean)
    Private ReadOnly fptrTimerProc As New TimerProc(AddressOf TickCallback)

    Private PlayerClock_Handle As IntPtr

    '--- Player

    Private PlayerStopwatch As New Stopwatch             ' for Player

    Public ReadOnly Property PlayerRunning As Boolean
    Public ReadOnly Property PlayerPaused As Boolean

    Private LastPlayerTick As Long                      ' last PlayerStopwatch.elapsedTicks
    Public PlayerPosition As Double                     ' (Song Ticks)

    Private DisablePlayer As Boolean                     ' while PlayerMoveTo

    Public Sub TickCallback(lpParameter As IntPtr, TimerOrWaitFired As Boolean)
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
        Dim tDuetime_3 As UInteger = 1                  ' ms to first event
        Dim tPeriod_3 As UInteger = 1                   ' wenn > 0 : Repetition time in ms
        Dim ret As Boolean                      ' TRUE (NonZero) if ok, FALSE if failed
        'Const WT_EXECUTEDEFAULT As UInteger = 0
        Const WT_EXECUTEINTIMERTHREAD As UInteger = &H20
        ret = CreateTimerQueueTimer(PlayerClock_Handle, 0, fptrTimerProc, 0, tDuetime_3, tPeriod_3, WT_EXECUTEINTIMERTHREAD)
        'Flags=&H20 WT_EXECUTEINTIMERTHREAD  The callback function is invoked by the timer thread itself
        ' otherwise the midi playback will not work properly (note hang-ups, etc.)        
    End Sub

    Private Sub StopPlayerTick()
        Dim ret As Boolean                      ' TRUE (NonZero) if ok, FALSE if failed
        ret = DeleteTimerQueueTimer(0, CUInt(PlayerClock_Handle), 0)
        PlayerClock_Handle = IntPtr.Zero                ' Handle is no longer valid -> set to 0
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
