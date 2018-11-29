Imports System.Xml
Imports System.Windows.Forms
Imports clTrinity.Trinity
'some changes made'

Public Class frmExportCampaignUnicorn_new

    Dim SelectDeselect As Boolean = False
    
    Dim mnuWeeks As New Windows.Forms.ContextMenuStrip

    Dim bundleTV4 As Boolean = False
    Dim bundleMTG As Boolean = False
    Dim bundleMTGSpecial As Boolean = False
    Dim printExportAsCampaign As Boolean = False
    Dim bundleSBS As Boolean = False
    Dim bundleFOX As Boolean = False
    Dim bundleCMORE As Boolean = False
    Dim bundleDisney As Boolean = False
    Dim bundleTNT As Boolean = False
    Dim ownCommission As Boolean = False
    Dim useCommissionAmount As Single = 0
    Dim weeks As new List(Of cWeek)
    Dim NotAll As Boolean = False
    Dim filteredChannels As New List(Of cChannel)

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click

        CheckCheckboxes()
        'Used for using only filtered Channels in the Unicorn export'
        For Each c As Trinity.cChannel In Campaign.Channels
            For Each tmpBook As Trinity.cBookingType In c.BookingTypes
                If tmpBook.BookIt = True Then
                    filteredChannels.Add(c)
                    Exit For
                End If

            Next
        Next

        '2018-05-15 : Added filteredChannels.First to only return the first element in the sequence
        If Not NotAll Then
            weeks.Clear()
            For Each w As cWeek In filteredChannels.First.BookingTypes(1).Weeks
                weeks.Add(w)
            Next
        End If
        Dim export As New CExportUnicornFileNew(Campaign)

        export.printUnicornFile(bundleTV4, bundleMTG, bundleMTGSpecial, bundleSBS, bundleFOX, bundleCMORE, bundleDisney, bundleTNT, ownCommission, useCommissionAmount, printExportAsCampaign, weeks, filteredChannels)
        filteredChannels.Clear()
        mnuWeeks.Items.Clear()
        mnuWeeks.Refresh()
    End Sub

    Private Sub CheckCheckboxes()

        If chkBundleTV4.Checked Then
            bundleTV4 = True
        Else
            bundleTV4 = False
        End If
        If chkBundleMTG.Checked Then
            bundleMTG = True
        Else
            bundleMTG = False
        End If
        If chkBundleMTGSpecial.Checked Then
            bundleMTGSpecial = True
        Else
            bundleMTGSpecial = False
        End If
        If chkBundleSBS.Checked Then
            bundleSBS = True
        Else
            bundleSBS = False
        End If
        If chkBundleFOX.Checked Then
            bundleFOX = True
        Else
            bundleFOX = False
        End If
        If chkBundleCMORE.Checked Then
            bundleCMORE = True
        Else
            bundleCMORE = False
        End If
        If chkBundleDisney.Checked Then
            bundleDisney = True
        Else
            bundleDisney = False
        End If
        If chkBundleTNT.Checked Then
            bundleTNT = True
        Else
            bundleTNT = False
        End If
        If chkOwnCommission.Checked Then
            ownCommission = True
            useCommissionAmount = 6
        Else
            ownCommission = False
        End If
        If chkPrintExportAsCampaign.Checked Then
            printExportAsCampaign = True
        Else
            printExportAsCampaign = False
        End If
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        mnuWeeks.Items.Clear()
    End Sub


    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.

        If TrinitySettings.UserEmail.Contains("mindshare") Then
            chkBundleMTGSpecial.Visible = True
            Dim test = 0
        End If
        If TrinitySettings.Developer Then
            btnExportToXML.Enabled = True
        End If
        mnuWeeks.Items.Clear()
        For Each TmpWeek As cWeek In Campaign.Channels(1).BookingTypes(1).Weeks
            With DirectCast(mnuWeeks.Items.Add(TmpWeek.Name), Windows.Forms.ToolStripMenuItem)
                .Tag = TmpWeek
                .Checked = True
                .Name = "mnuWeek" & TmpWeek.Name
                AddHandler .Click, AddressOf ChangeWeek
            End With
        Next

        With DirectCast(mnuWeeks.Items.Add("Invert"), Windows.Forms.ToolStripMenuItem)
            .Checked = False
            .Tag = "Invert"
            AddHandler .Click, AddressOf ChangeWeek
        End With
    End Sub
    Sub ChangeWeek(ByVal sender As Object, ByVal e As EventArgs)
        Dim TmpWeekStr As String = ""
        Dim TmpMnu As Windows.Forms.ToolStripMenuItem
        weeks.Clear()
        'mnuWeeks.Items.Clear() 'test clear Itemlist

        If sender.tag.GetType.FullName = "System.String" AndAlso sender.tag = "Invert" Then
            For Each TmpMnu In mnuWeeks.Items
                TmpMnu.Checked = Not TmpMnu.Checked
            Next
            sender.checked = True

        End If

        sender.checked = Not sender.checked

        NotAll = False
        For Each TmpMnu In mnuWeeks.Items
            If TmpMnu.Checked Then
                For Each _w As Trinity.cWeek In Campaign.Channels(1).BookingTypes(1).Weeks
                    If _w.Name = TmpMnu.Text And Not weeks.Contains(_w) Then
                        weeks.Add(_w)
                    End If
                Next
                TmpWeekStr = TmpWeekStr & TmpMnu.Text & ","
            Else
                NotAll = True
            End If
        Next
        If NotAll Then
            If TmpWeekStr = "" Then
                lblWeeks.Text = ""
            Else
                lblWeeks.Text = TmpWeekStr.TrimEnd(",")
            End If
        Else
            lblWeeks.Text = "All"
        End If

    End Sub

    Private Sub lblSelectDeselect_Click(sender As Object, e As EventArgs) Handles lblSelectDeselect.Click
        For Each tmpChkBox As Control In Me.Controls
            Try
                Dim chk As CheckBox = TryCast(tmpChkBox, CheckBox)
                If chk.Name <> "chkOwnCommission" And chk.Name <> "chkPrintExportAsCampaign" Then
                    If chk.Checked Then
                        chk.Checked = False
                    Else
                        chk.Checked = True
                    End If
                End If
            Catch ex As Exception

            End Try
        Next
    End Sub

    Private Sub chkOwnCommission_CheckedChanged(sender As Object, e As EventArgs) Handles chkOwnCommission.CheckedChanged
    End Sub

    Private Sub chkPrintExportAsCampaign_CheckedChanged(sender As Object, e As EventArgs) Handles chkPrintExportAsCampaign.CheckedChanged
        If chkPrintExportAsCampaign.Checked Then
            For Each tmpChkBox As Control In Me.Controls
                Try
                    Dim chk As CheckBox = TryCast(tmpChkBox, CheckBox)
                    If chk.Name <> "chkOwnCommission" And chk.Name <> "chkPrintExportAsCampaign" Then
                        If chk.Enabled Then
                            chk.Enabled = False
                            chk.Checked = False
                        End If
                    End If
                Catch ex As Exception

                End Try
            Next
        Else
            For Each tmpChkBox As Control In Me.Controls
                Try
                    Dim chk As CheckBox = TryCast(tmpChkBox, CheckBox)
                    If chk.Name <> "chkOwnCommission" And chk.Name <> "chkPrintExportAsCampaign" Then
                        If chk.Enabled = False Then
                            chk.Enabled = True
                            chk.Checked = True
                        End If
                    End If
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try
            Next

        End If
    End Sub

    Private Sub chkBundleMTGSpecial_CheckedChanged(sender As Object, e As EventArgs) Handles chkBundleMTGSpecial.CheckedChanged
        'If chkBundleMTGSpecial.Checked Then
        '    chkBundleMTG.Checked = False
        'End If
    End Sub

    Private Sub chkBundleMTG_CheckedChanged(sender As Object, e As EventArgs) Handles chkBundleMTG.CheckedChanged
        'If chkBundleMTG.Checked Then
        '    chkBundleMTGSpecial.Checked = False
        'End If
    End Sub

    Private Sub btnExportToXML_Click(sender As Object, e As EventArgs) Handles btnExportToXML.Click
        CheckCheckboxes()
        Dim exportAsXML As New cExportXmlToUnicorn(Campaign)

    End Sub

    Private Sub cmdWeeks_Click(sender As Object, e As EventArgs) Handles cmdWeeks.Click
        'set what week(s)
        mnuWeeks.Show(cmdWeeks, 0, cmdWeeks.Height)
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        mnuWeeks.Items.Clear()
        mnuWeeks.Refresh()
        For Each TmpWeek As cWeek In Campaign.Channels(1).BookingTypes(1).Weeks
            With DirectCast(mnuWeeks.Items.Add(TmpWeek.Name), Windows.Forms.ToolStripMenuItem)
                .Tag = TmpWeek
                .Checked = True
                .Name = "mnuWeek" & TmpWeek.Name
                AddHandler .Click, AddressOf ChangeWeek
            End With
        Next
        With DirectCast(mnuWeeks.Items.Add("Invert"), Windows.Forms.ToolStripMenuItem)
            .Checked = False
            .Tag = "Invert"
            AddHandler .Click, AddressOf ChangeWeek
        End With
    End Sub
End Class