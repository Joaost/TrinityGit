Namespace Trinity

    Public Class cColorScheme

        Public HeadlineColor As Color
        Public PanelBGColor As Color
        Public PanelFGColor As Color
        Public TextBGColor As Color
        Public textFont As String
        Private _name As String
        Public pbc1 As Color
        Public pbc2 As Color
        Public pbc3 As Color
        Public pbc4 As Color
        Public pbc5 As Color
        Public pbc6 As Color
        Public pbc7 As Color
        Public pbc8 As Color
        Public pbc9 As Color
        Public pbc10 As Color

        Public Property name()
            Get
                Return _name
            End Get
            Set(ByVal value)
                _name = value
            End Set
        End Property

        Public Sub New(Optional ByVal newname As String = Nothing)
            name = newname
        End Sub

    End Class

End Namespace
