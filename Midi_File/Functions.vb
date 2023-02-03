Imports System.Text
Partial Public Class CMidiFile

    Public Function GetTrackName(TrackNumber As Integer) As String

        If TrackNumber >= TrackList.Count Then Return ""        ' if invalid TrackNumber

        Dim ev As CMidiFile.TrackEvent

        Dim ascii As Encoding = Encoding.ASCII
        Dim chr As Char()

        For i = 1 To TrackList(TrackNumber).EventList.Count
            ev = TrackList(TrackNumber).EventList(i - 1)


            If ev.Type = EventType.MetaEvent Then
                If ev.Data1 = MetaEventType.SequenceOrTrackName Then

                    If ev.DataX.Length > 0 Then
                        chr = ascii.GetChars(ev.DataX)
                        Return chr
                    Else
                        Return ""
                    End If

                End If
            End If
        Next

        Return "[no Name]"
    End Function
End Class
