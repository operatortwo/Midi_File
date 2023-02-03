Partial Public Class Form1

    Private Sub UpdateClbMute()

        clbMute.Items.Clear()

        For i = 1 To Mid.TrackList.Count
            clbMute.Items.Add("Track " & CStr(i) & " - " & Mid.GetTrackName(i - 1))
        Next

    End Sub

    Private Sub clbMuteAllTracks()
        For i = 1 To clbMute.Items.Count
            clbMute.SetItemChecked(i - 1, True)
        Next
    End Sub

    Private Sub clbUnMuteAllTracks()

        For i = 1 To clbMute.Items.Count
            clbMute.SetItemChecked(i - 1, False)
        Next

    End Sub

End Class
