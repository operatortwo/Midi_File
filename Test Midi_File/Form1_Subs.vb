Imports Midi_File
Imports System.IO
Partial Public Class Form1

    ''' <summary>
    ''' While playing
    ''' </summary>
    Private Sub UpdateDisplay()
        Dim pos As Integer = CInt(Mid.PlayerPosition)
        If pos <= trkbPosition.Maximum Then
            trkbPosition.Value = pos
        End If

        lblEventsCount.Text = CStr(EventFIFO.Count)
    End Sub

    Private Sub ListEvents(TrackNum As Integer)
        lvEvents.Items.Clear()

        Dim lvi As ListViewItem
        Dim ev As CMidiFile.TrackEvent

        lvEvents.BeginUpdate()          ' -----        

        For i = 1 To Mid.TrackList(TrackNum).EventList.Count

            ev = Mid.TrackList(TrackNum).EventList(i - 1)

            lvi = lvEvents.Items.Add(CStr(i))           ' add item to ListView

            lvi.SubItems.Add(CStr(ev.Time))             ' Time
            lvi.SubItems.Add(ev.Type.ToString)          ' Type
            lvi.SubItems.Add(GetSubType(ev))            ' SubType
            lvi.SubItems.Add(GetChannel(ev))            ' Channel (only Midi-Events)
            lvi.SubItems.Add(GetData(ev))               ' Data
            lvi.SubItems.Add(GetDuration(ev))           ' Duration

        Next

        lvEvents.EndUpdate()            '-----        

        lblNumOfItems.Text = CStr(Mid.TrackList(TrackNum).EventList.Count)

    End Sub


    Private Sub OpenFileDlg()

        With OpenFileDialog1
            .Title = "Midi-File load"
            '.InitialDirectory = Midi_Path              ' if not set: last used path
            '.Filter = "Data files (*.dat)|*.dat|All files (*.*)|*.*"
            .Filter = "Midi files (*.mid)|*.mid"
            .FileName = ""
        End With

        '--- Define file name for saving 
        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
            MidiFullname = OpenFileDialog1.FileName

            If Mid.ReadMidiFile(MidiFullname) = True Then
                SetForm1Title()
                MsgOut(".mid -File read.")
                Show_Header_Data()
                UpdateCmbTrackList()            ' Selection of the track for the event list
                UpdateClbMute()                 ' CheckListe Mute
            Else
                MsgOut("Error reading file" & vbCrLf & Mid.ErrorText)
                ResetForm1Title()
            End If


            '--- extract filename
            ' MidiFilename = Path.GetFileName(MidiFilename)
            ' MidiFilename = Path.GetFileNameWithoutExtension(Project_Fullname)
        End If

    End Sub

    Private Sub Show_Header_Data()

        MsgOut("Name: " & Mid.MidiName)
        MsgOut("SMF Format: " & CStr(Mid.SmfFormat))
        MsgOut("Number of Tracks: " & CStr(Mid.NumberOfTracks))
        MsgOut("Ticks per Quarter Note: " & CStr(Mid.TPQ))
        MsgOut("---")

    End Sub

    Private Sub SetForm1Title()
        Dim str As String
        str = My.Application.Info.AssemblyName          ' Form1 - Midi name
        str += " - " & " [ " & Path.GetFileName(MidiFullname) & " ]"
        Me.Text = str
    End Sub

    Private Sub ResetForm1Title()
        Me.Text = My.Application.Info.AssemblyName     ' Form1 - Midi name
    End Sub

End Class
