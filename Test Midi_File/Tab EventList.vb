
Partial Public Class Form1

    Private Sub UpdateCmbTrackList()

        lvEvents.Items.Clear()
        lblNumOfItems.Text = "xx"

        cmbTrackList.Items.Clear()

        For i = 1 To Mid.TrackList.Count
            cmbTrackList.Items.Add("Track " & CStr(i) & " - " & Mid.GetTrackName(i - 1))
        Next

    End Sub

End Class
