Imports clTrinity.Trinity

Public Class frmAddChannel

    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub

    Private Sub frmAddChannel_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cmbFile.Items.Clear()

        If (TrinitySettings.ConnectionStringCommon <> "") Then
            Dim table As DataTable = DBReader.getAllChannelSets()

            For Each dr As DataRow In table.Rows
                cmbFile.Items.Add(New cComboItem(dr(1), dr(0)))
            Next

        Else
            cmbFile.Items.Add(New cComboItem("Default", "Default"))
            Dim ini As New Trinity.clsIni
            ini.Filename = TrinitySettings.DataPath(cSettings.SettingsLocationEnum.locNetwork) & "/" & Campaign.Area & "/" & "Area.ini"
            Dim i As Integer = 1
            Dim s As String = ini.Text("channelfiles", "channelfile" & 1)
            While s <> ""
                cmbFile.Items.Add(New cComboItem(s.Substring(0, s.LastIndexOf(".")), s.Substring(0, s.LastIndexOf("."))))
                i += 1
                s = ini.Text("channelfiles", "channelfile" & i)
            End While
        End If

        cmbFile.SelectedIndex = 0

        If Me.Tag = "EDIT" Then
            Label3.Visible = True
            Me.Label2.Visible = False
            Me.txtName.Visible = False
            Me.Text = "Save as default"
        Else
            Label3.Visible = False
            Me.Label2.Visible = True
            Me.txtName.Visible = True
            Me.Text = "Add Channel"
        End If

    End Sub
End Class

Public Class cComboItem
    Private sDisplay As String
    Private sValue As String
    Public Property Display() As String
        Get
            Return Me.sDisplay
        End Get
        Set(ByVal value As String)
            Me.sDisplay = value
        End Set
    End Property
    Public Property Value() As String
        Get
            Return Me.sValue
        End Get
        Set(ByVal value As String)
            Me.sValue = value
        End Set
    End Property

    Public Sub New(ByVal sDisplay As String, ByVal sValue As String)
        Me.Display = sDisplay
        Me.Value = sValue
    End Sub

    Public Overrides Function ToString() As String
        Return Display
    End Function
End Class