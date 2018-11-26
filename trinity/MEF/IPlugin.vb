
Public Interface IPlugin
    Class PluginMenu
        Public Property AddToMenu As String = ""
        Public Property Caption As String = ""
        Public Property Items As New List(Of PluginMenuItem)
    End Class

    Class PluginMenuItem
        Public Property Text As String
        Public Property OnClickFunction As System.EventHandler
        Public Property Image As System.Drawing.Image
    End Class

    Class PropertyBundle
        Public Property Campaign As Object
    End Class

    Function Menu() As PluginMenu

End Interface

