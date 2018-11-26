Imports System.Windows.Forms

Public Class frmMtrlSpec

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Dim Excel As CultureSafeExcel.Application
        Dim WB As CultureSafeExcel.Workbook
        Dim i As Integer
        Dim r As Integer
        Dim TmpChan As Trinity.cChannel
        Dim TmpBT As Trinity.cBookingType
        Dim TmpFilm As Trinity.cFilm

        Me.Cursor = Cursors.WaitCursor

        Excel = New CultureSafeExcel.Application(False)
        Excel.screenupdating = False
        WB = Excel.AddWorkbook

        With WB.Sheets(1)
            .Name = "Material Specification"
            .Columns(1).columnwidth = 17.5
            .Columns(2).columnwidth = 10.14
            .Columns(3).columnwidth = 12
            .Columns(4).columnwidth = 9
            .Columns(5).columnwidth = 10.75
            .Columns(6).columnwidth = 10.75
            .Columns(7).columnwidth = 30
            .Columns(8).columnwidth = 30
            .AllCells.Interior.Color = RGB(255, 255, 255)
            .AllCells.Font.Size = 10
            .Cells(1, 1).Value = "MATERIAL SPECIFICATION"
            .Cells(1, 4).Value = "TV"
            .Rows(1).Font.Bold = True
            .Rows(1).Font.Size = 14
            .Cells(3, 1).Value = "Client:"
            .Cells(3, 2).Value = Campaign.Client
            .Cells(4, 1).Value = "Product:"
            .Cells(4, 2).Value = Campaign.Product
            .Cells(5, 1).Value = "TV-Buyer:"
            .Cells(5, 2).Value = Campaign.Buyer
            .Cells(6, 1).Value = "TV-Planner:"
            .Cells(6, 2).Value = Campaign.Planner
            .Cells(7, 1).Value = "Creative Bureau:"
            .Cells(8, 1).Value = "Contact bureau:"
            .Rows(10).Font.Bold = True
            .Rows(10).horizontalalignment = -4108
            .Cells(10, 1).Value = "Channel"
            .Cells(10, 2).Value = "Deliv.Day"
            .Cells(10, 3).Value = "Camp.Start"
            .Cells(10, 4).Value = "Length"
            .Cells(10, 5).Value = "Filmcode"
            .Cells(10, 6).Value = "Description"
            .Cells(10, 7).Value = "Address"
            .Cells(10, 8).Value = "2nd Address"
            With .Range("A10:H10")
                .Interior.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Panel")
                .Font.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "PanelText")
                For i = 7 To 10
                    With .Borders(i)
                        .LineStyle = 1
                        .Weight = 2
                        .ColorIndex = -4105
                    End With
                Next
            End With
            With .Range("A3:A8")
                .Interior.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Panel")
                .Font.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "PanelText")
                For i = 7 To 10
                    With .Borders(i)
                        .LineStyle = 1
                        .Weight = 2
                        .ColorIndex = -4105
                    End With
                Next
            End With
            With .Range("B3:C8")
                For i = 7 To 10
                    With .Borders(i)
                        .LineStyle = 1
                        .Weight = 2
                        .ColorIndex = -4105
                    End With
                Next
            End With
            r = 11
            For Each TmpChan In Campaign.Channels
                For Each TmpBT In TmpChan.BookingTypes
                    If TmpBT.BookIt Then
                        For Each TmpFilm In TmpBT.Weeks(1).Films
                            .Cells(r, 1).Value = TmpChan.ChannelName
                            .Cells(r, 2).Value = Format(Date.FromOADate(Campaign.StartDate).AddDays(-7), "Short date")
                            .Cells(r, 3).Value = Format(Date.FromOADate(Campaign.StartDate), "Short date")
                            .Cells(r, 4).Value = TmpFilm.FilmLength
                            .Cells(r, 5).Numberformat = "@"
                            .Cells(r, 5).Value = TmpFilm.Filmcode
                            .Cells(r, 6).Value = TmpFilm.Description
                            .Cells(r, 7).Value = TmpChan.DeliveryAddress
                            .Cells(r, 8).Value = TmpChan.VHSAddress
                            .Rows(r).horizontalalignment = -4108
                            .Rows(r).verticalalignment = -4108
                            r = r + 1
                        Next
                        Exit For
                    End If
                Next
            Next
            With .Range("A11:H" & Trim(r))
                For i = 7 To 11
                    With .Borders(i)
                        .LineStyle = 1
                        .Weight = 2
                        .ColorIndex = -4105
                    End With
                Next
            End With
            If cmbLogo.SelectedIndex > 0 Then
                Dim Scal As Single
                .InsertPicture(DataPath & "Logos\" & cmbLogo.Text)
                Scal = 180 / .Pictures(1).Width
                Scal = 1
                If Scal < 1 Then
                    .Pictures(1).ScaleWidth(Scal, 0, 0)
                    .Pictures(1).ScaleHeight(Scal, 0, 0)
                End If
                .Pictures(1).Top = .Rows(2).Top
                '.Rows(1).RowHeight = .pictures(1).Height + 10
                .Pictures(1).Left = .Columns("I").Left - .Pictures(1).Width - 10
            End If
            With Excel.ActiveSheet.PageSetup
                .Orientation = 2
                .PrintArea = "$A$1:$H$" & Trim(r)
                .PrintTitleRows = "$1:$10"
            End With
            'Excel.ActiveWindow.SelectedSheets.PrintPreview()
            Excel.ActiveWindow.View = 2
            While Excel.ActiveSheet.VPageBreaks.Count > 0
                Excel.ActiveSheet.VPageBreaks(1).DragOff(Direction:=-4161, RegionIndex:=1)
            End While
            Excel.ActiveWindow.View = 1
        End With


        Excel.ActiveWindow.Zoom = 75
        Excel.screenupdating = True
        Excel.Visible = True

        Me.Cursor = Cursors.Default

        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmMtrlSpec_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        cmbLogo.Items.Clear()
        cmbLogo.Items.Add("None")
        For Each File As String In My.Computer.FileSystem.GetFiles(DataPath & "Logos\", FileIO.SearchOption.SearchAllSubDirectories, "*.bmp", "*.gif", "*.jpg")
            cmbLogo.Items.Add(My.Computer.FileSystem.GetName(File))
        Next
        cmbColorScheme.Items.Clear()
        For i As Integer = 1 To TrinitySettings.ColorSchemeCount
            cmbColorScheme.Items.Add(TrinitySettings.ColorScheme(i))
        Next
        cmbColorScheme.SelectedIndex = TrinitySettings.DefaultColorScheme
        cmbLogo.SelectedIndex = TrinitySettings.DefaultLogo

    End Sub
End Class
