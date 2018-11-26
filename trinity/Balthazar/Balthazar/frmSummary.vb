Public Class frmSummary

    Private _questionaireId As Integer

    Private Sub frmSummary_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim _table As DataTable
        _table = Database.GetAnswersForEvent(MyEvent.DatabaseID)

        grdSummary.Columns.Clear()
        grdSummary.Columns.Add("", "")
        For Each _row As DataRow In _table.Rows
            Dim xmlDoc As New Xml.XmlDocument
            xmlDoc.LoadXml(_row!xml)
            Dim TmpRow As DataGridViewRow = grdSummary.Rows(grdSummary.Rows.Add)
            For Each _node As Xml.XmlElement In xmlDoc.SelectSingleNode("questionaire").ChildNodes
                If _node.Name = "input" Then
                    If Not grdSummary.Columns.Contains(_node.GetAttribute("name")) Then
                        grdSummary.Columns.Add(_node.GetAttribute("name"), _node.GetAttribute("text"))
                    End If
                    TmpRow.Cells(_node.GetAttribute("name")).Value = _node.GetAttribute("answer")
                End If
            Next
        Next
        grdSummary.Columns.RemoveAt(0)
    End Sub

    Private Sub cmdExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExcel.Click
        Dim Excel As Object = CreateObject("excel.application")
        Dim WB As Object = Excel.Workbooks.Add

        Excel.ScreenUpdating = False
        With WB.Sheets(1)
            For r As Integer = 0 To grdSummary.RowCount - 1
                For c As Integer = 0 To grdSummary.ColumnCount - 1
                    .cells(r + 1, c + 1) = grdSummary.Rows(r).Cells(c).Value
                Next
            Next
        End With
        Excel.ScreenUpdating = True
        Excel.Visible = True

    End Sub
End Class