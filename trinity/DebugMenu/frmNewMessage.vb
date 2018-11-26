Public Class frmNewMessage

    Private Sub cmdCreate_Click(sender As System.Object, e As System.EventArgs) Handles cmdCreate.Click
        If chkSweden.Checked Then
            CreateMessage("Server=STO-APP60;Database=Trinity_sweden;uid=johanh;pwd=turbo;")
        End If
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Sub CreateMessage(ConnectionString As String)
        Using _conn As New SqlClient.SqlConnection(ConnectionString)
            _conn.Open()
            Using _cmd As New SqlClient.SqlCommand("INSERT INTO messages VALUES (@headline,@ingress,@body)", _conn)
                _cmd.Parameters.AddWithValue("@headline", txtHeadline.Text)
                _cmd.Parameters.AddWithValue("@ingress", txtIngress.Text)
                _cmd.Parameters.AddWithValue("@body", txtBody.Text)
                _cmd.ExecuteNonQuery()
            End Using
            _conn.Close()
        End Using
    End Sub

End Class