Imports System.Windows.Forms.Integration
Imports DevExpress.XtraPivotGrid
Imports System.ComponentModel
Imports System.Windows.Controls
Imports System.Windows.Data
Imports Microsoft.Windows.Controls
Imports System.Linq

Partial Public Class Window3



    Private Sub Window3_Initialized(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Initialized


        'grdSpots.ItemsSource = Window1.tmpSpots
        Dim myDS As New System.Data.DataSet


        myDS.Tables.Add(Window1.mydatatable.DefaultView.Table)

        pvtSpots.DataSource = myDS.Tables("Table1")
        'pvtSpots.DataSource = Window1.tmpSpots

        Dim fieldAdvertiser As PivotGridField = New PivotGridField("Advertiser", PivotArea.RowArea)

        Dim fieldProduct As PivotGridField = New PivotGridField("Product", PivotArea.RowArea)
        fieldProduct.Caption = "Product"

        Dim fieldChannel As PivotGridField = New PivotGridField("Kanal", PivotArea.ColumnArea)
        fieldChannel.Caption = "Kanal"

        Dim fieldTRP As PivotGridField = New PivotGridField("TRP", PivotArea.DataArea)
        fieldTRP.Caption = "TRP"
        fieldTRP.CellFormat.FormatType = Utils.FormatType.Numeric
        fieldTRP.CellFormat.FormatString = "#.#"

        Dim fieldRemarks As PivotGridField = New PivotGridField("Remarks", PivotArea.RowArea)
        fieldRemarks.Caption = "Remarks"

        pvtSpots.Fields.AddRange(New PivotGridField() {fieldAdvertiser, fieldProduct, fieldRemarks, fieldTRP, fieldChannel})


        fieldAdvertiser.AreaIndex = 0
        fieldProduct.AreaIndex = 1
        Me.Width = pvtSpots.Width

    End Sub

    Private Sub cmdExcel_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles cmdExcel.Click
        Select Case TabControl1.SelectedIndex
            Case 0

            Case 1
                Dim opts As New DevExpress.XtraPrinting.XlsExportOptions
                opts.SheetName = "Pivot"
                opts.ShowGridLines = True

                pvtSpots.ExportToXls(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\PivotOutput.xls", opts)
                Dim exx As New Microsoft.Office.Interop.Excel.Application
                exx.Workbooks.Open(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\PivotOutput.xls")
                exx.Visible = True
        End Select
    End Sub

    Private Sub tpSpots_GotFocus(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles tpSpots.GotFocus

    End Sub

    Private Sub tpSpots_Loaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles tpSpots.Loaded
        grdSpots.ItemsSource = Window1.mydatatable.DefaultView
        Me.Width = grdSpots.Width

        '        dim style as  DataGridTableStyle = dataGridTitles.TableStyles["titles"]
        'dim columnStyle as DataGridTextBoxColumn = style.GridColumnStyles["Price"] as DataGridTextBoxColumn
        '       columnStyle.Format = "N2"

        'grdSpots.ItemsSource = Window1.tmpSpots
    End Sub



End Class





