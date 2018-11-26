Imports System.IO
Imports System
Imports System.Text

Namespace Trinity

    Public Class cBlockIniReader
        Public ID As New Collection
        Public General As New Collection
        Public Marathon As New Collection
        Public Contacts As New Collection
        Public Paths As New Collection
        Public NetworkDB As New Collection
        Public LocalDB As New Collection
        Public Spotlistcolumns As New Collection
        Public Columns As New Collection
        Public ColumnWidth As New Collection
        Public PlannedColumns As New Collection
        Public ActualColumns As New Collection
        Public ConfirmedColumns As New Collection
        Public Booking As New Collection
        Public Preferences As New Collection
        Public Settings As New Collection
        Public SpotlistColumnWidth As New Collection
        Public ConfirmedColumnWidth As New Collection
        Public PrintColumns As New Collection
        Public ActualColumnWidth As New Collection
        Public LastCampaigns As New Collection
        Public BillingAddresses As New Collection
        Public OrderPlacerAddresses As New Collection
        Public DefaultContract As New Collection
        Public PrintBookingColumns As New Collection

        Sub ReadIni()

            Dim FileName As String = Trinity.Helper.GetSpecialFolder(Trinity.Helper.CSIDLEnum.UserProfile) & "Trinity 4.0\Trinity.ini"
            Dim MyFile As New FileStream(FileName, FileMode.Open)
            Dim MyStream As New StreamReader(MyFile, Encoding.Default)

            Dim FileContents() As String = Strings.Split(MyStream.ReadToEnd(), vbNewLine)
            MyFile.Close()
            Dim LastSection As New Collection
            For Each line As String In FileContents
                Select Case line
                    Case "[ID]"
                        LastSection = ID
                    Case "[General]"
                        LastSection = General
                    Case "[Marathon]"
                        LastSection = Marathon
                    Case "[Contacts]"
                        LastSection = Contacts
                    Case "[Paths]"
                        LastSection = Paths
                    Case "[NetworkDB]"
                        LastSection = NetworkDB
                    Case "[DefaultContract]"
                        LastSection = DefaultContract
                    Case "[LocalDB]"
                        LastSection = LocalDB
                    Case "[SpotlistColumns]"
                        LastSection = Spotlistcolumns
                    Case "[Columns]"
                        LastSection = Columns
                    Case "[ConfirmedColumns]"
                        LastSection = ConfirmedColumns
                    Case "[PlannedColumns]"
                        LastSection = PlannedColumns
                    Case "[PrintBookingColumns]"
                        LastSection = PrintBookingColumns
                    Case "[ActualColumns]"
                        LastSection = ActualColumns
                    Case "[Booking]"
                        LastSection = Booking
                    Case "[Preferences]"
                        LastSection = Preferences
                    Case "[Settings]"
                        LastSection = Settings
                    Case "[SpotlistColumnWidth]"
                        LastSection = SpotlistColumnWidth
                    Case "[Contacts]"
                        LastSection = ID
                    Case "[ConfirmedColumnWidth]"
                        LastSection = ConfirmedColumnWidth
                    Case "[ColumnWidth]"
                        LastSection = ColumnWidth
                    Case "[PrintColumns]"
                        LastSection = PrintColumns
                    Case "[ActualColumnWidth]"
                        LastSection = ActualColumnWidth
                    Case "[LastCampaigns]"
                        LastSection = LastCampaigns
                    Case "[ConfirmedColumns]"
                        LastSection = ConfirmedColumns
                    Case "[BillingAddresses]"
                        LastSection = BillingAddresses
                    Case "[OrderPlacerAddresses]"
                        LastSection = OrderPlacerAddresses
                    Case Else


                        If Not line.Contains(";") Then
                            If Strings.Split(line, "=").Length > 1 Then
                                LastSection.Add(Strings.Split(line, "=")(1), Strings.Split(line, "=")(0))
                            Else
                                Dim s As String = line
                            End If
                        End If


                End Select



            Next

        End Sub

    End Class

End Namespace

