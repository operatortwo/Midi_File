Partial Public Class Form1

    Private Sub EnterTabRecordedEvents()

        lblRecordedEventsCount.Text = CStr(EventFIFO.Count)

        FillTbRecordedEvents()

        FillTbVoiceMap()

    End Sub

    Private Sub FillTbRecordedEvents()
        '--- Font: Consolas 9pt

        Dim events As Midi_File.CMidiFile.TrackEvent()
        Dim sb As New Text.StringBuilder

        tbRecordedEvents.Clear()

        events = EventFIFO.ToArray

        sb.Append(String.Format("{0,6} {1,3} {2,12} {3,5} {4,5} {5,5} {6}",
                                "Time", "Trk", "Type", "Status", "Data1", "Data2", vbCrLf))

        For Each ev In events
            'ev.DataX
            sb.Append(String.Format("{0,6} {1,3} {2,12} {3,5:X} {4,5:X} {5,5:X} {6}",
                                    ev.Time, ev.TrackNumber, ev.Type, ev.Status, ev.Data1, ev.Data2, vbCrLf))
        Next

        tbRecordedEvents.Text = sb.ToString

    End Sub

    Private Sub FillTbVoiceMap()
        '--- Font: Consolas 9pt

        Dim voicemap As Midi_File.CMidiFile.CVoiceMap
        Dim sb As New Text.StringBuilder

        tbVoiceMap.Clear()

        voicemap = Mid.VoiceMap

        sb.Append(String.Format("{0,-4} {1,4} {2,6} {3,5} {4,5}  {5}",
                                "Ch.", "VoiceGM", "Voice", "MSB", "LSB", vbCrLf))

        Dim val1 As String
        Dim val2 As String
        Dim val3 As String
        Dim val4 As String


        For i = 1 To 16

            val1 = CStr(voicemap.Items(i - 1).VoiceNumberGM)
            val2 = CStr(voicemap.Items(i - 1).VoiceNumber)
            val3 = CStr(voicemap.Items(i - 1).BankMSB)
            val4 = CStr(voicemap.Items(i - 1).BankLSB)

            If val1 = "128" Then val1 = ""
            If val2 = "128" Then val2 = ""
            If val3 = "128" Then val3 = ""
            If val4 = "128" Then val4 = ""

            sb.Append(String.Format("{0,-4:X} {1,4} {2,8} {3,6} {4,5}  {5}",
                                    i - 1, val1, val2, val3, val4, vbCrLf))
        Next

        tbVoiceMap.Text = sb.ToString

    End Sub

    Private Sub Export_Pattern()

        If EventFIFO.Count = 0 Then
            MessageBox.Show("No data available", "Export pattern", MessageBoxButtons.OK)
            Exit Sub
        End If

        '--- Create folder if not exists

        If IO.Directory.Exists(PatternDirectoryName) = False Then
            IO.Directory.CreateDirectory(PatternDirectoryName)
        End If

        '--- Determine the filename

        Dim PatternName As String = "Pattern"

        If tbPatternName.Text <> "" Then
            PatternName = tbPatternName.Text
        End If

        Dim Fullname As String
        Fullname = PatternPath & "\" & PatternName & ".pat"
        Dim c As Integer = 1
        ' if  file exists -> Counter
        Do Until IO.File.Exists(Fullname) = False
            Fullname = PatternPath & "\" & PatternName & " (" & CStr(c) & ").pat"
            c += 1
        Loop

        '--- prepare für saving

        Dim pat As New Midi_File.CMidiFile.CPattern
        Const DstTPQ = 960

        pat.Name = PatternName
        pat.Category = ""                   '
        pat.Group = ""                      '
        pat.Source = "Midi_File"
        pat.BPM = CInt(Mid.BPM)
        pat.TPQ = DstTPQ                    ' the event times in Mid.TPQ are converted to DstTPQ below      
        pat.Length = 0                      ' Beats (init)
        pat.EventList = EventFIFO.ToList

        pat.VoiceMap = Mid.VoiceMap

        '--- trim start
        Dim TimeStart As UInteger = pat.EventList(0).Time               ' time of first event
        Dim ModStart As UInteger = CUInt(TimeStart Mod Mid.TPQ)         ' Rest
        Dim AlignedStart As UInteger

        If ModStart <> 0 Then
            'start-rest+TPQ
            AlignedStart = CUInt(TimeStart - ModStart + Mid.TPQ)
        Else
            AlignedStart = TimeStart
        End If

        '--- trim end

        Dim TimeEnd As UInteger = pat.EventList.Last.Time               ' time of last event
        Dim ModEnd As UInteger = CUInt(TimeEnd Mod Mid.TPQ)             ' remainder
        Dim AlignedEnd As UInteger

        AlignedEnd = TimeEnd - ModEnd

        '--- get length

        pat.Length = CInt((AlignedEnd - AlignedStart) / Mid.TPQ)

        '--- remove events at start

        Dim cnt As Integer

        For i = 1 To pat.EventList.Count
            If pat.EventList(i - 1).Time < AlignedStart Then
                cnt += 1
            End If
        Next

        pat.EventList.RemoveRange(0, cnt)

        '--- remove events at end

        For i = 1 To pat.EventList.Count
            If pat.EventList(i - 1).Time > AlignedEnd Then
                pat.EventList.RemoveRange(i - 1, pat.EventList.Count - (i - 1))
                Exit For
            End If
        Next

        '--- remove hanging notes

        ' if event: time + duration > AlignedEnd

        '--- Pattern(Time) move to the beginning

        For Each item In pat.EventList
            item.Time -= AlignedStart
        Next

        '--- scale time and duration to destination TPQ

        Dim time As UInteger
        Dim duration As UInteger

        For Each item In pat.EventList
            time = item.Time
            duration = item.Duration
            item.Time = CUInt(time * DstTPQ / Mid.TPQ)
            item.Duration = CUInt(duration * DstTPQ / Mid.TPQ)
        Next

        '--- save as XML (für debug)

        If cbSaveXML.Checked = True Then
            Dim FullnameX As String = System.IO.Path.ChangeExtension(Fullname, ".xml")
            Dim fsx As New IO.FileStream(FullnameX, IO.FileMode.Create)      'create or truncate / write        
            Dim seria As New Xml.Serialization.XmlSerializer(pat.GetType)
            seria.Serialize(fsx, pat)
            fsx.Close()
        End If

        '--- save as Binary
        Try
            Dim bf As New Runtime.Serialization.Formatters.Binary.BinaryFormatter()
            Dim fs As New IO.FileStream(Fullname, IO.FileMode.Create)          'create or truncate / write
            bf.Serialize(fs, pat)
            fs.Close()
            MessageBox.Show("Pattern saved", "Export pattern", MessageBoxButtons.OK)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Export pattern", MessageBoxButtons.OK)
        End Try

    End Sub

End Class
