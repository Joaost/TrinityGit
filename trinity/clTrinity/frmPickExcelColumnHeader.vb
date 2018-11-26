Imports System.Windows.Forms

Public Class frmPickExcelColumnHeader

    Public pointPicked As New Point(1, 1)

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        pointPicked.X = DataGridView1.SelectedCells(0).ColumnIndex + 1
        pointPicked.Y = DataGridView1.SelectedCells(0).RowIndex + 1
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Public Sub populateFormFromExcel(ByVal WB As Object, ByVal columnToFind As String, ByVal startRow As Integer)
        If columnToFind = "" Then
            lblColumn.Visible = False
            lblExplainingText.Text = "Trinity could not find the row containing the column headers for the spotlist"
            DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        Else
            lblColumn.Visible = True
            lblColumn.Text = columnToFind
            lblExplainingText.Text = "Trinity could not find the column: "
            DataGridView1.SelectionMode = DataGridViewSelectionMode.CellSelect
        End If


        DataGridView1.Columns.Clear()
        DataGridView1.Rows.Clear()

        For y As Integer = (startRow - 1) To (startRow + 49)
            DataGridView1.Columns.Add(y, y)
            For x As Integer = 0 To 50
                DataGridView1.Rows.Add()
                DataGridView1.Rows(x).Cells(y).Value = WB.sheets(1).cells(x + 1, y + 1).value
            Next
        Next
    End Sub

    Private Sub DataGridView1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        Dim s As String = ""
        If DataGridView1.SelectedRows.Count = 0 Then
            If DataGridView1.SelectedCells.Count = 0 Then Exit Sub
            pointPicked.X = DataGridView1.SelectedCells(0).ColumnIndex + 1
            pointPicked.Y = DataGridView1.SelectedCells(0).RowIndex + 1
        Else
            pointPicked.Y = DataGridView1.SelectedRows(0).Index + 1
        End If

        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
End Class