''' <summary>
''' A form with a list of options that you specify. After it is closed, the picked option will be available in it's property "Result"
''' To have a list of the campaign channels presented, passing this will work: (From chan In Campaign.Channels Select chan).ToArray
''' A cChannel object will be returned
''' </summary>
''' <remarks></remarks>
Public Class frmMultipleChoice

    Dim stackPanel As New Windows.Forms.TableLayoutPanel

    Private _result As Object

    ''' <param name="options">A list of options that will be presented to the user. The object's ToString property will determine how its represented</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal options As Object, Optional ByVal Caption As String = "Pick an option:")

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        lblMessage.Text = Caption

        Dim widest As Integer = 0

        If DirectCast(options, IList).Count < 6 Then
            For Each opt As Object In options
                Dim tmpRadio As New Windows.Forms.RadioButton
                tmpRadio.Text = opt.ToString
                tmpRadio.Tag = opt
                tmpRadio.Padding = New System.Windows.Forms.Padding(0)
                tmpRadio.Left = 12
                stackPanel.Controls.Add(tmpRadio)
                stackPanel.Height = tmpRadio.Bottom + 6
                If stackPanel.Controls.Count Mod 2 = 1 Then tmpRadio.BackColor = Color.LightGray
                If tmpRadio.Right > widest Then widest = tmpRadio.Right
            Next

            If widest < Me.Width Then
                For Each cntrl As Windows.Forms.Control In stackPanel.Controls
                    cntrl.Width = Me.Width + 10
                Next
            Else
                Me.Width = widest + 10
            End If
            lblMessage.MaximumSize = New Size(Me.Width - 20, 0)
        Else
            Dim cmbOptions As New Windows.Forms.ComboBox
            stackPanel.Controls.Add(cmbOptions)
            cmbOptions.DropDownStyle = Windows.Forms.ComboBoxStyle.DropDownList
            cmbOptions.Left = lblMessage.Left
            stackPanel.Height = cmbOptions.Bottom + 6
            cmbOptions.Width = stackPanel.Width - 12
            For Each opt As Object In options
                cmbOptions.Items.Add(opt)
            Next
        End If
        stackPanel.Height += 30
        stackPanel.Padding = New System.Windows.Forms.Padding(0)
        stackPanel.Top = lblMessage.Bottom
        Me.Padding = New System.Windows.Forms.Padding(0)
        Me.Controls.Add(stackPanel)

    End Sub

    Public Sub New(ByVal ParamArray options() As Object)
        InitializeComponent()
    End Sub

    Public ReadOnly Property Result() As Object
        Get
            Return _result
        End Get
    End Property

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        For Each radiobutton As Windows.Forms.RadioButton In stackPanel.Controls.OfType(Of Windows.Forms.RadioButton)()
            If radiobutton.Checked Then _result = radiobutton.Tag
        Next
        For Each cmb As Windows.Forms.ComboBox In stackPanel.Controls.OfType(Of Windows.Forms.ComboBox)()
            _result = cmb.SelectedItem
        Next
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub frmMultipleChoice_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Label1_Click(sender As System.Object, e As System.EventArgs) Handles lblMessage.Click

    End Sub
End Class