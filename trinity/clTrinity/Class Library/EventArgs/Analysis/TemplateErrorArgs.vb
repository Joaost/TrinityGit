Public Class TemplateErrorArgs : Inherits EventArgs

    Private _lstError As List(Of String)


    Public Property [Error]() As List(Of String)
        Get
            Return _lstError
        End Get
        Set(ByVal value As List(Of String))
            _lstError = value
        End Set
    End Property

    Public Sub New(ByVal errors As List(Of String))

        Me.Error = errors
    End Sub
End Class
