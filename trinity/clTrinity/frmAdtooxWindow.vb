Public Class frmAdtooxWindow

    Public ReturnValue As String

    Public Event PageLoaded(ByVal sender As Object, ByVal e As EventArgs)
    Public Event LoginFailed(ByVal sender As Object, ByVal e As EventArgs)

    Private Enum WindowTypeEnum
        CreateSpotCode
        ShowFilm
    End Enum

    Private _windowType As WindowTypeEnum

    Private Sub frmAdtooxWindow_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Dim html As mshtml.HTMLDocument = bwsAdtoox.Document.DomDocument
        For Each _button As mshtml.HTMLDivElement In html.getElementsByName("bottonID")
            _button.style.display = "none"
            _button.style.visibility = "hidden"
            _button.className = ""
        Next
    End Sub

    Private Sub frmAdtooxWindow_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Enter
        Debug.Print("Display: " & bwsAdtoox.Document.DomDocument.getElementById("bottonID").style.display)
    End Sub

    Private Sub frmAdtooxWindow_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.GotFocus
        'Stop
    End Sub

    Private Sub frmAdtooxWindow_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    End Sub

    Private Sub bwsAdtoox_DocumentCompleted(ByVal sender As Object, ByVal e As System.Windows.Forms.WebBrowserDocumentCompletedEventArgs) Handles bwsAdtoox.DocumentCompleted
        Dim html As mshtml.HTMLDocument = bwsAdtoox.Document.DomDocument
        If _windowType = WindowTypeEnum.CreateSpotCode Then
            bwsAdtoox.ScrollBarsEnabled = True
            If e.Url.AbsoluteUri = bwsAdtoox.Url.AbsoluteUri Then
                If html.getElementById("loginform") IsNot Nothing Then
                    RaiseEvent LoginFailed(Me, New EventArgs)
                    Me.Close()
                    Exit Sub
                End If
                If html.getElementById("copycode.code") IsNot Nothing AndAlso html.getElementById("copycode.code").offsetHeight > 0 Then
                    Dim filmcode As String = html.getElementById("copycode.code").innerHTML.Substring(0, html.getElementById("copycode.code").innerHTML.IndexOf("&nbsp;"))
                    ReturnValue = filmcode
                    Me.DialogResult = Windows.Forms.DialogResult.OK
                Else
                    Dim product As DataTable = DBReader.getAllFromProducts(Campaign.ProductID)
                    Dim ProductType As String = ""
                    Dim BrandID As Long = 0
                    If product.Rows.Count = 1 Then
                        If Not IsDBNull(product.Rows(0)!AdTooxBrandID) Then
                            BrandID = product.Rows(0)!AdTooxBrandID
                        End If
                        If Not IsDBNull(product.Rows(0)!AdTooxProductType) Then
                            ProductType = product.Rows(0)!AdTooxProductType
                        End If
                    End If

                    html.getElementById("wrap_subMenu").style.display = "none"
                    For Each table As mshtml.HTMLTable In html.getElementsByTagName("table")
                        If table.className = "topTable" Then
                            table.style.display = "none"
                            Exit For
                        End If
                    Next
                    html.getElementById("content.commercial").checked = 1
                    html.getElementById("region" & Campaign.Area.ToLower).checked = True
                    html.parentWindow.execScript("regionSelectionChanged('" & Campaign.Area.ToLower & "');")
                    html.parentWindow.execScript("inputActionOnContentType('1')")
                    html.getElementById("inputfilm_length").value = _film.FilmLength
                    html.getElementById("inputtitle").value = _film.Name
                    html.getElementById("inputstoryboard").value = _film.Description
                    html.getElementById("inputclass_aid").value = ProductType
                    html.getElementById("inputfad").value = Format(Date.FromOADate(Campaign.StartDate), "Short date")
                    For Each TmpChan As Trinity.cChannel In Campaign.Channels
                        For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
                            If TmpBT.BookIt Then
                                For Each checkbox As mshtml.HTMLInputElement In html.getElementsByName(TmpChan.AdTooxChannelID.ToString.Trim)
                                    If checkbox.offsetHeight > 0 Then
                                        checkbox.checked = True
                                        Exit For
                                    End If
                                Next
                                Exit For
                            End If
                        Next
                    Next
                    Me.Width = html.getElementById("wrap_content").offsetWidth
                    Me.Height = html.getElementById("wrap_content").offsetHeight + 45
                    Dim AdTooxBrandID As Long
                    Dim AdTooxAdvertiserID As Long
                    Dim AdTooxDivisionID As Long
                    Dim dt As DataTable = DBReader.getAllFromProducts(Campaign.ProductID)
                    If dt.Rows.Count = 1 Then
                        If Not IsDBNull(dt.Rows(0)!AdtooxBrandID) Then
                            AdTooxBrandID = dt.Rows(0)!AdtooxBrandID
                            AdTooxAdvertiserID = dt.Rows(0)!AdTooxAdvertiserID
                            AdTooxDivisionID = dt.Rows(0)!AdTooxDivisionID
                        End If
                        DirectCast(html.getElementById("advertiser_select_bs"), mshtml.HTMLSelectElement).value = AdTooxAdvertiserID
                        html.parentWindow.execScript("loadAJAXDivisions('bs')")
                        While DirectCast(html.getElementById("division_select_bs"), mshtml.HTMLSelectElementClass).value = "" OrElse Not DirectCast(html.getElementById("division_select_bs"), mshtml.HTMLSelectElementClass).value = AdTooxDivisionID
                            Dim index As Integer
                            For Each _opt As mshtml.HTMLOptionElement In DirectCast(html.getElementById("division_select_bs"), mshtml.HTMLSelectElement).options
                                If _opt.value <> "" AndAlso _opt.value = AdTooxDivisionID Then
                                    index = _opt.index
                                End If
                            Next
                            DirectCast(html.getElementById("division_select_bs"), mshtml.HTMLSelectElement).selectedIndex = index
                        End While
                        html.parentWindow.execScript("loadAJAXBrands2('bs','link')")
                        While DirectCast(html.getElementById("inputbs"), mshtml.HTMLSelectElement).value = "" OrElse Not DirectCast(html.getElementById("inputbs"), mshtml.HTMLSelectElement).value = AdTooxBrandID
                            Dim index As Integer
                            For Each _opt As mshtml.HTMLOptionElement In DirectCast(html.getElementById("inputbs"), mshtml.HTMLSelectElement).options
                                If _opt.value <> "" AndAlso _opt.value = AdTooxBrandID Then
                                    index = _opt.index
                                End If
                            Next
                            DirectCast(html.getElementById("inputbs"), mshtml.HTMLSelectElement).selectedIndex = index
                        End While
                    End If
                End If
                lblWaitLabel.Visible = False
                bwsAdtoox.Visible = True
                For Each _button As mshtml.HTMLDivElement In html.getElementsByName("bottonID")
                    _button.style.visibility = "hidden"
                Next
                RaiseEvent PageLoaded(Me, New EventArgs)
            End If
        ElseIf _windowType = WindowTypeEnum.ShowFilm Then
            bwsAdtoox.ScrollBarsEnabled = False
            If e.Url.AbsoluteUri = bwsAdtoox.Url.AbsoluteUri Then
                bwsAdtoox.Document.InvokeScript("OnLoadActivate")
                bwsAdtoox.Visible = True
                For Each _table As Object In html.getElementById("play_popup_wrap").all
                    If _table.className = "popupPlay" Then
                        Me.Width = 675
                        Me.Height = 765
                    End If
                Next
                lblWaitLabel.Visible = False
                For Each _button As mshtml.HTMLDivElement In html.getElementsByName("bottonID")
                    _button.style.display = "none"
                    _button.style.visibility = "hidden"
                Next
                RaiseEvent PageLoaded(Me, New EventArgs)
            End If
        End If
    End Sub

    Private _film As Trinity.cFilm
    Public Sub New(ByVal Film As Trinity.cFilm)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        bwsAdtoox.Navigate("https://ecexpress.adtoox.com/ec/index.jsp?username=" & TrinitySettings.AdtooxUsername & "&password=" & TrinitySettings.AdtooxPassword & "&action=copycodegeneration&section=copycod&tmp_show_details=true&tmp_show_medias=true")
        _film = Film
        _windowType = WindowTypeEnum.CreateSpotCode
    End Sub

    Public Sub New(ByVal Filmcode As String)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        bwsAdtoox.Navigate(Campaign.AdToox.getSingleFilmCodeInfo(Filmcode).GetLinkToPlayVideo())
        _windowType = WindowTypeEnum.ShowFilm

    End Sub

    Private Sub bwsAdtoox_Invalidated(ByVal sender As Object, ByVal e As System.Windows.Forms.InvalidateEventArgs) Handles bwsAdtoox.Invalidated
    End Sub

    Private Sub bwsAdtoox_Navigated(ByVal sender As Object, ByVal e As System.Windows.Forms.WebBrowserNavigatedEventArgs) Handles bwsAdtoox.Navigated
        Dim html As mshtml.HTMLDocument = bwsAdtoox.Document.DomDocument
        For Each _button As mshtml.HTMLDivElement In html.getElementsByName("bottonID")
            _button.style.display = "none"
            _button.style.visibility = "hidden"
        Next
    End Sub

    Private Sub bwsAdtoox_Navigating(ByVal sender As Object, ByVal e As System.Windows.Forms.WebBrowserNavigatingEventArgs) Handles bwsAdtoox.Navigating
    End Sub

    Private Sub bwsAdtoox_RegionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles bwsAdtoox.RegionChanged
    End Sub
End Class