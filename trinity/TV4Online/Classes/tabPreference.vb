Imports TrinityPlugin
Imports System.Windows.Forms
Imports System.Runtime.Serialization
Imports System.Xml.Serialization
Imports System.ServiceModel

Public Class tabPreference
    Inherits pluginPreferencesTab

    Dim _binding As Object
    Dim _endpoint As Object

    Private txtUsername As New TextBox With {.Left = 6, .Top = 30, .Width = 260}
    Private txtToken As New TextBox With {.Left = 6, .Top = 70, .Width = 260}

    'Private txtToken As New TextBox With {.Left = 6, .Top = 130, .Width = 260}
    'Private cmdPasswordEye As New Button With {.Left = txtPassword.Width + 20, .Top = 70, .Width = 30, .Image = TV4Online.My.Resources.Resources.preview_2_24x24}

    Dim _plugin As TV4OnlinePlugin

    Public Property Username As String
        Get
            Return txtUsername.Text
        End Get
        Set(value As String)
            txtUsername.Text = value
        End Set
    End Property

    'Public Property Password As String
    '    Get
    '        Return txtPassword.Text
    '    End Get
    '    Set(value As String)
    '        txtPassword.Text = value
    '    End Set
    'End Property
    Public Property Token As String
        Get
            Return txtToken.Text
        End Get
        Set(value As String)
            txtToken.Text = value
        End Set
    End Property

    Private Sub InitializeComponent()
        Me.SuspendLayout()

        Me.Text = "TV4 Spotlight"

        Me.Controls.Add(New Label With {.Text = "Username", .Left = 6, .Top = 13, .AutoSize = True})
        Me.Controls.Add(txtUsername)

        Me.Controls.Add(New Label With {.Text = "Token", .Left = 6, .Top = 53, .AutoSize = True})
        Me.Controls.Add(txtToken)

        'Me.Controls.Add(New Label With {.Text = "Token", .Left = 6, .Top = 110, .AutoSize = True})
        'Me.Controls.Add(txtToken)

        'Me.Controls.Add(cmdPasswordEye)

        'AddHandler cmdPasswordEye.Click, AddressOf showHidePassword

        'Me.Controls.Add(New Button With {.Text = "", .left = txtPassword.Width + 20, .Top = 53, .AutoSize = True})

        Me.ResumeLayout(False)
    End Sub

    Public Overrides Sub SaveData()
        Dim _ser As New XmlSerializer(GetType(Preferences))
        Dim _preferences As New Preferences
        _preferences.Username = txtUsername.Text
        '_preferences.SetPlainTextPassword(txtPassword.Text)
        _preferences.Token = txtToken.Text
        Using _stream As New IO.FileStream(IO.Path.Combine(_plugin.Application.GetUserDataPath, "TV4Online.xml"), IO.FileMode.Create)
            _ser.Serialize(_stream, _preferences)
        End Using
        _plugin.Preferences.Username = txtUsername.Text
        '_plugin.Preferences.SetPlainTextPassword(txtPassword.Text)

        _plugin.Preferences.Token = txtToken.Text
    End Sub

    Public Sub New(Plugin As TV4OnlinePlugin)
        _plugin = Plugin
        Me.InitializeComponent()
    End Sub
    'Private sub showHidePassword
    '    If txtPassword.UseSystemPasswordChar = True
    '        txtPassword.UseSystemPasswordChar = False
    '    Else
    '        txtPassword.UseSystemPasswordChar = True
    '    End If
    'End sub
End Class
