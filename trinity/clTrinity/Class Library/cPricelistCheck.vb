Namespace Trinity

    Public Class cPricelistCheck

        Dim _main As Trinity.cKampanj
        Dim _channels As Trinity.cChannels
        Dim form As Object

        Delegate Sub FinishedChecking(ByVal Errors As String)


        Public Sub New(OnCampaign As Trinity.cKampanj, ByVal f As Windows.Forms.Form)
            'NOTE that the form must have a setPricelistLabel sub
            _channels = OnCampaign.Channels
            _main = OnCampaign
            form = f
        End Sub

        Public Sub checkPricelists()
            'this function check it the campaign has pricelists covering the duraton of the campaign
            Dim checkPricelists As String = ""

            Try
                Dim TmpChan As Trinity.cChannel
                Dim TmpBT As Trinity.cBookingType
                Dim reason As String

                'we go though all channels and their booked bookingtypes
                For Each TmpChan In _channels
                    For Each TmpBT In TmpChan.BookingTypes
                        If TmpBT.BookIt Then
                            Trinity.Helper.WriteToLogFile("Booking Type: " & TmpBT.ToString)

                            'reset variable
                            reason = ""

                            Trinity.Helper.WriteToLogFile("Create comparable BT")
                            'we create a comparable bookingtype
                            Dim TmpNewBT As New Trinity.cBookingType(Campaign)
                            TmpNewBT.ParentChannel = TmpBT.ParentChannel
                            TmpNewBT.Name = TmpBT.Name
                            TmpNewBT.ReadPricelist(Target:=TmpBT.BuyingTarget.TargetName, isPriceCheck:=True)
                            With TmpNewBT.Weeks.Add("")
                                .StartDate = _main.StartDate
                                .EndDate = _main.EndDate
                            End With

                            'we check and compare the bookingtypes
                            Dim TmpTarget As Trinity.cPricelistTarget = TmpBT.BuyingTarget
                            Dim TmpTarget2 As Trinity.cPricelistTarget = TmpNewBT.Pricelist.Targets(TmpTarget.TargetName)
                            If Not TmpTarget2 Is Nothing Then
                                reason = TmpTarget.CheckTargetWReason(TmpTarget2)
                            End If

                            'we make sure the bookingtype have a pricelist covering the entire campaign period
                            For z As Integer = _main.StartDate To _main.EndDate
                                If TmpBT.BuyingTarget.CalcCPP Then
                                    For x As Integer = 0 To TmpBT.Dayparts.Count - 1
                                        If Not TmpBT.BuyingTarget.hasCPPForDate(z, x) Then
                                            reason = reason & ", Incomplete pricelist"
                                            Exit For
                                        End If
                                    Next
                                Else
                                    If Not TmpBT.BuyingTarget.hasCPPForDate(z) Then
                                        reason = reason & ", Incomplete pricelist"
                                        Exit For
                                    End If
                                End If
                            Next

                            If Not reason = "" Then
                                If checkPricelists = "" Then
                                    checkPricelists = TmpBT.ToString & ": " & vbCrLf & reason
                                Else
                                    checkPricelists = checkPricelists & "," & vbCrLf & TmpBT.ToString & ": " & vbCrLf & reason
                                End If
                            End If


                        End If
                    Next
                Next
                If DirectCast(form, Windows.Forms.Form).IsHandleCreated Then
                    form.Invoke(New FinishedChecking(AddressOf form.setPricelistLabel), New Object() {checkPricelists})
                End If
            Catch ex As Exception
                Try
                    If DirectCast(form, Windows.Forms.Form).IsHandleCreated Then
                        form.Invoke(New FinishedChecking(AddressOf form.setPricelistLabel), New Object() {"Trinity was unable to check pricelists for updates"})
                    End If
                Catch ex2 As InvalidOperationException

                End Try
            End Try
        End Sub
    End Class

End Namespace
