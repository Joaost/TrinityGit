
Public Interface IPlugin

    Class PluginMenu
        ''' <summary>
        ''' Gets or sets the menu to which this menu should be added. If the string is empty the menu will be added to the main menu bar.
        ''' </summary>
        ''' <value>
        ''' The menu to add this menu to.
        ''' </value>
        Public Property AddToMenu As String = ""
        ''' <summary>
        ''' Gets or sets the caption of the added menu.
        ''' </summary>
        ''' <value>
        ''' The caption.
        ''' </value>
        Public Property Caption As String = ""
        ''' <summary>
        ''' Returns the menu items to be added to this menu.
        ''' </summary>
        ''' <value>
        ''' The items.
        ''' </value>
        Public Property Items As New List(Of PluginMenuItem)
    End Class

    Class PluginMenuItem
        ''' <summary>
        ''' Gets or sets the menu item text.
        ''' </summary>
        ''' <value>
        ''' The text.
        ''' </value>
        Public Property Text As String
        ''' <summary>
        ''' Gets or sets the function to be called when the user clicks the menu.
        ''' </summary>
        ''' <value>
        ''' The on click function.
        ''' </value>
        Public Property OnClickFunction As System.EventHandler
        ''' <summary>
        ''' Gets or sets the menu image.
        ''' </summary>
        ''' <value>
        ''' The image.
        ''' </value>
        Public Property Image As System.Drawing.Image
    End Class

    ''' <summary>
    ''' Gets the name of the plugin.
    ''' </summary>
    ''' <value>
    ''' The name of the plugin.
    ''' </value>
    ReadOnly Property PluginName As String

    ''' <summary>
    ''' Returns a menu to be added to Trinity
    ''' </summary><returns></returns>
    Function Menu() As PluginMenu

    ''' <summary>
    ''' Returns a dictionary of key/values to add to the save file of the campaign
    ''' </summary><returns></returns>
    Function GetSaveData() As XElement

    Event SaveDataAvailale(sender As Object, e As EventArgs)

    ReadOnly Property PreferencesTab As pluginPreferencesTab

End Interface

