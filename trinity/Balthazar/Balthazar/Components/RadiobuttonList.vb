Public Class RadiobuttonList

    Private _value As Integer = 3

    Public Property Value() As Integer
        Get
            Return _value
        End Get
        Set(ByVal value As Integer)
            If value < 1 Or value > 5 Then
                If Not value = Nothing Then DirectCast(Me.Controls("opt" & _value), RadioButton).Checked = False
                _value = Nothing
            Else
                DirectCast(Me.Controls("opt" & value), RadioButton).Checked = True
                _value = value
            End If
        End Set
    End Property


    Private Sub CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opt1.CheckedChanged, opt2.CheckedChanged, opt3.CheckedChanged, opt4.CheckedChanged, opt5.CheckedChanged
        _value = sender.tag
    End Sub
End Class
