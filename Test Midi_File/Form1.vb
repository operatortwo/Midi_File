Imports System.Environment              ' for SpecialFolder
Imports System.IO
Public Class Form1

    Public MIO As New Midi_IO.Midi_IO

    Friend hMidiOut As UInteger
    Private hMidiIn As UInteger

    Public WithEvents Mid As New Midi_File.CMidiFile
    Public WithEvents Mkbd As New Midi_Keyboard.Main()                                  'screen Keyboard

    'Public Midi_Path As String = GetFolderPath(SpecialFolder.MyMusic) & "\Midi"
    'Public Midi_Path As String = GetFolderPath(SpecialFolder.Windows) & "\Media"          
    Public Midi_Path As String = Application.StartupPath

    Public MidiFullname As String = ""

    Public Const PatternDirectoryName = "Pattern"
    Public PatternPath As String = Application.StartupPath & "\" & PatternDirectoryName

    Private EventFIFO As New Queue(Of Midi_File.CMidiFile.TrackEvent)   ' for recording note events from the player


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cmbMidiOutDevices.Items.Clear()
        For i = 1 To MIO.MidiOutPorts.Count
            If MIO.MidiOutPorts(i - 1).invalidPort = False Then          'list only valid devices
                cmbMidiOutDevices.Items.Add(MIO.MidiOutPorts(i - 1).portName)
            End If
        Next

        '--- try to set Last Midi Output

        Dim LastMidiOut As String = My.Settings.LastMidiOutput

        If LastMidiOut <> "" Then
            If cmbMidiOutDevices.Items.Contains(LastMidiOut) Then
                cmbMidiOutDevices.SelectedItem = LastMidiOut            ' set to LastMidiOut
            Else
                ' if LastMidiOut is not available anymore, no selection, let the user select
            End If
        Else
            If cmbMidiOutDevices.Items.Count > 0 Then
                cmbMidiOutDevices.SelectedIndex = 0                     ' select first item
            End If
        End If
    End Sub

    Private Sub Form1_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown

        '--- try last Midi-File

        If My.Settings.LastMidiFile <> "" Then
            If File.Exists(My.Settings.LastMidiFile) Then
                MidiFullname = My.Settings.LastMidiFile
                MsgOut(".mid -File found.")
                LoadMidiFile_at_startup()
                Exit Sub
            End If
        End If

        '--- try first File in special Midi-Folder

        If Directory.Exists(Midi_Path) Then
            If Directory.GetFiles(Midi_Path, "*.mid").Count() > 0 Then
                MidiFullname = Directory.GetFiles(Midi_Path, "*.mid").First
                MsgOut(".mid -File found.")

                LoadMidiFile_at_startup()
            Else
                MsgOut("No *.mid -Files found in:  " & Midi_Path)
            End If
        Else
            MsgOut("Folder does not exists: " & Midi_Path)
        End If

    End Sub

    Private Sub LoadMidiFile_at_startup()

        If Mid.ReadMidiFile(MidiFullname) = True Then
            SetForm1Title()
            MsgOut(".mid -File read.")
            Show_Header_Data()
            UpdateCmbTrackList()            ' Selection of the track for the event list
            UpdateClbMute()                 ' list of checkboxes for Mute
        End If
    End Sub


    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

        If Mid.FileLoaded Then
            My.Settings.LastMidiFile = Mid.MidiFullname
        End If

        If Not IsNothing(cmbMidiOutDevices.SelectedItem) Then
            My.Settings.LastMidiOutput = CStr(cmbMidiOutDevices.SelectedItem)
        End If

        If Mid.PlayerRunning = True Then
            Mid.StopPlayer()
        End If

        MIO._end()                          ' close all MIDI-Ports

    End Sub

    Private Sub Mi_File_Open_Click(sender As Object, e As EventArgs) Handles Mi_File_Open.Click
        OpenFileDlg()
    End Sub

    Private Sub Mi_File_Close_Click(sender As Object, e As EventArgs) Handles Mi_File_Close.Click
        ' currently no action
    End Sub

    Private Sub Mi_File_Exit_Click(sender As Object, e As EventArgs) Handles Mi_File_Exit.Click
        Me.Close()
    End Sub

    Private Sub MidiOutCallback(msg As UInt32, data As UInt32, portNr As UInt32)
    End Sub

    Private Sub btnTbMessage_clear_Click(sender As Object, e As EventArgs) Handles btnTbMessage_clear.Click
        tbMessage.Clear()
    End Sub

    Private Sub MsgOut(Message As String)
        If tbMessage.Lines.Count > 1000 Then tbMessage.Clear()
        tbMessage.AppendText(Message & vbCrLf)
    End Sub

    Private Sub Tsbtn_Play_Click(sender As Object, e As EventArgs) Handles Tsbtn_Play.Click
        If cmbMidiOutDevices.SelectedItem Is Nothing Then
            MsgBox("No Output-Device selected", MsgBoxStyle.Exclamation, "Play")
        Else
            EventFIFO.Clear()

            lblPositionMBT.Text = "1:1:0"
            lblPosition.Text = "0"
            trkbPosition.Value = 0
            trkbPosition.Maximum = CInt(Mid.LastTick)
            trkbPosition.SmallChange = CInt(Mid.TPQ / 4)
            trkbPosition.LargeChange = Mid.TPQ

            Mid.StartPlayer()
        End If

    End Sub

    Private Sub TsBtnPause_Click(sender As Object, e As EventArgs) Handles TsBtnPause.Click
        If Mid.PlayerPaused = False Then
            TsBtnPause.BackColor = Color.LightGreen
            Mid.PlayerPause()
        Else
            TsBtnPause.BackColor = Color.Transparent
            Mid.PlayerContinue()
        End If
    End Sub

    Private Sub TsBtnStop_Click(sender As Object, e As EventArgs) Handles TsBtnStop.Click
        Mid.StopPlayer()
        tmDisplay.Stop()
    End Sub

    Private Sub PlayerStarted() Handles Mid.PlayerStarted
        Tsbtn_Play.BackColor = Color.LightGreen
        TsBtnPause.Enabled = True
        cmbMidiOutDevices.Enabled = False                 ' prevent changing the output port while playing
        tmDisplay.Start()
    End Sub

    Private Sub PlayerStopped() Handles Mid.PlayerStopped

        If ToolStrip1.InvokeRequired = True Then
            ToolStrip1.Invoke(New Delegate_RecordingStopped(AddressOf RecordingStopped))
        Else
            RecordingStopped()
        End If

        ' event raised from CMidi-Player

        ' InvokeRequired seems to depend on the Timer used. 
        ' no invoke required with TimerQueueTimer

        '--- for winmm timer ---        
        ' at direct 'Tsbtn_Play.BackColor = Color.Transparent':
        ' "Invalid cross-thread operation: Access to the control Tsbtn_Play
        '  came from a different thread than the thread it was created for."

        Mkbd.Set_All_Notes_OFF()
    End Sub

    Private Delegate Sub Delegate_RecordingStopped()
    Private Sub RecordingStopped()
        Tsbtn_Play.BackColor = Color.Transparent
        TsBtnPause.BackColor = Color.Transparent
        TsBtnPause.Enabled = False
        cmbMidiOutDevices.Enabled = True
        ' other actions to different controls are also possible
    End Sub

    Private Sub PlayerShortMsg(track As Integer, status As Byte, data1 As Byte, data2 As Byte) Handles Mid.OutShortMsg

        MIO.OutShortMsg(hMidiOut, status, data1, data2)

        If status < &HA0 Then                           ' only Note-On, Note-Off
            Mkbd.Midi_In(status, data1, data2)
        Else
            ' turn keys off when PlayerPause sending AllNotesOff
            If status = &HB0 Then                       ' only on channel 0
                If data1 = &H7B Then
                    ' All Notes Off (B0, 7B, 0)
                    Mkbd.Set_All_Notes_OFF()
                End If
            End If

        End If

    End Sub

    Private Sub PlayerEvent(trkev As Midi_File.CMidiFile.TrackEvent) Handles Mid.PlayerEvent
        EventFIFO.Enqueue(trkev)
    End Sub

    Private Sub cmbMidiOutDevices_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbMidiOutDevices.SelectedIndexChanged
        If Not hMidiOut = 0 Then
            MIO.CloseMidiOutPort(hMidiOut)
            hMidiOut = 0
        End If

        If cmbMidiOutDevices.SelectedItem IsNot Nothing Then
            Dim devName As String
            devName = cmbMidiOutDevices.SelectedItem.ToString

            If Not devName = Nothing Then
                MIO.OpenMidiOutPort(devName, hMidiOut, cmbMidiOutDevices.SelectedIndex)
            End If

        End If
    End Sub

    Private Sub btnGM_on_Click(sender As Object, e As EventArgs) Handles btnGM_on.Click
        Dim gm_on() As Byte = {&HF0, &H7E, &H7F, 9, 1, &HF7}
        Dim ret As Integer
        ret = MIO.OutLongMsg(hMidiOut, gm_on)               ' send GM-On message (General Midi system On)
    End Sub

    Private Sub trkbVolume_ValueChanged(sender As Object, e As EventArgs) Handles trkbVolume.ValueChanged
        ' Master Volume for GM Instruments, Universal Real Time SysEx
        'F0 7F 7F 04 01 ll mm F7        ' ll = volume LSB, mm = volume MSB

        Dim MasterVolume_sysx As Byte() = {&HF0, &H7F, &H7F, &H4, &H1, &H0, &H64, &HF7}

        MasterVolume_sysx(6) = CByte(trkbVolume.Value)

        MIO.OutLongMsg(hMidiOut, MasterVolume_sysx)

        lblVolumeValue.Text = CStr(trkbVolume.Value)
    End Sub

    Private Sub btnKeyboard_Click(sender As Object, e As EventArgs) Handles btnKeyboard.Click
        Mkbd.show()
    End Sub

    Private Sub Mkbd_kbEvent(status As Integer, channel As Integer, data1 As Integer, data2 As Integer) Handles Mkbd.kbEvent

        Dim ret As Integer                  'return-value

        ret = MIO.OutShortMsg(hMidiOut, CByte(status Or channel), data1, data2)

    End Sub

    Private Sub cmbTrackList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbTrackList.SelectedIndexChanged
        ListEvents(cmbTrackList.SelectedIndex)
    End Sub

    Private Sub tmDisplay_Tick(sender As Object, e As EventArgs) Handles tmDisplay.Tick
        UpdateDisplay()
    End Sub

    Private Sub trkbPosition_ValueChanged(sender As Object, e As EventArgs) Handles trkbPosition.ValueChanged
        lblPosition.Text = CStr(trkbPosition.Value)

        '--- Measures : Beats : Ticks ---
        Dim pos As Long = CLng(Mid.PlayerPosition)

        Dim meas As Long                            ' measure (assuming: 4/4)
        Dim beat As Integer                         ' beat
        Dim ticks As Integer
        Dim TPQ As Integer = Mid.TPQ

        meas = pos \ (4 * TPQ)                      '  \ = integer quotient
        beat = CInt((pos - meas) \ TPQ Mod 4)
        ticks = CInt(pos Mod TPQ)

        lblPositionMBT.Text = CStr(meas + 1) & " : " & CStr(beat + 1) & " : " & CStr(ticks)

    End Sub

    Private Sub trkbPosition_MouseDown(sender As Object, e As MouseEventArgs) Handles trkbPosition.MouseDown
        If Mid.PlayerRunning Then
            PauseState = Mid.PlayerPaused           ' store pause state
            Mid.PlayerPause()
            tmDisplay.Stop()
        End If
    End Sub

    Private PauseState As Boolean

    Private Sub trkbPosition_MouseUp(sender As Object, e As MouseEventArgs) Handles trkbPosition.MouseUp
        Mid.PlayerMoveTo(trkbPosition.Value)
        If Mid.PlayerRunning Then
            If PauseState = False Then
                Mid.PlayerContinue()                ' restore pause state
            End If
            tmDisplay.Start()
        End If
    End Sub

    Private Sub btnMuteAll_Click(sender As Object, e As EventArgs) Handles btnMuteAll.Click
        clbMuteAllTracks()
    End Sub

    Private Sub btnMuteNone_Click(sender As Object, e As EventArgs) Handles btnMuteNone.Click
        clbUnMuteAllTracks()
    End Sub

    Private Sub clbMute_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles clbMute.ItemCheck

        If Mid.TrackList.Count > e.Index Then                      ' only if track is present
            Mid.TrackList(e.Index).Mute = CBool(e.NewValue)
        End If

    End Sub

    Private Sub cbStoreEvents_CheckedChanged(sender As Object, e As EventArgs) Handles cbRecordEvents.CheckedChanged
        If cbRecordEvents.Checked = True Then
            Mid.Enable_PlayerEvent = True
        Else
            Mid.Enable_PlayerEvent = False
        End If
    End Sub

    Private Sub tpRecordedEvents_Enter(sender As Object, e As EventArgs) Handles tpRecordedEvents.Enter
        EnterTabRecordedEvents()
    End Sub

    Private Sub btnExportPattern_Click(sender As Object, e As EventArgs) Handles btnExportPattern.Click
        Export_Pattern()
    End Sub

    Private Sub Mi_Info_About_Click(sender As Object, e As EventArgs) Handles Mi_Info_About.Click
        AboutBox1.ShowDialog()
    End Sub

    Private Sub btnResetAllControllers_Click(sender As Object, e As EventArgs) Handles btnResetAllControllers.Click

        Dim ret As Integer
        Dim stat As Byte = &HB0
        For i = 0 To &HF
            ret = MIO.OutShortMsg(hMidiOut, stat Or i, 121, 0)
        Next

    End Sub


End Class
