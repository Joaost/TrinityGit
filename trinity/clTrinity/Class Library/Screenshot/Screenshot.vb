Public Class Screenshot

    ''' <summary>
    ''' Creates a screenshot
    ''' </summary>
    ''' <returns>The path to the generated image</returns>
    Shared Function Create() As String
        Dim tmpFile As String = My.Computer.FileSystem.GetTempFileName & ".jpg"
        Using screenImage As New Bitmap(My.Computer.Screen.Bounds.Width, My.Computer.Screen.Bounds.Height)
            Using g As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(screenImage)
                g.CopyFromScreen(New Point(0, 0), New Point(0, 0), New Size(My.Computer.Screen.Bounds.Width, My.Computer.Screen.Bounds.Height))
            End Using
            screenImage.Save(tmpFile, System.Drawing.Imaging.ImageFormat.Jpeg)
        End Using
        Return tmpfile
    End Function

End Class
