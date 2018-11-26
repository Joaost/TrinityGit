Imports System.Runtime.InteropServices
Imports System
Imports System.Text
Imports System.Collections
Imports Microsoft.VisualBasic

Namespace Trinity
    Public Class cCompensation
        Private mvarID As String
        Private _expense As Single = 0
        Private _fromDate As Date
        Private _toDate As Date
        Private _trps As Single
        Private _comment As String
        Private _bookingType As Trinity.cBookingType
        Private ParentColl As Collection

        Public Function GetXML(ByRef colXml As Xml.XmlElement, ByRef errorMessege As List(Of String), ByVal xmlDoc As Xml.XmlDocument) As Boolean
            'this function saves the collection to the xml provided

            On Error GoTo On_Error

            colXml.SetAttribute("ID", Me.ID)
            colXml.SetAttribute("From", Me.FromDate)
            colXml.SetAttribute("To", Me.ToDate)
            colXml.SetAttribute("TRPs", Me.TRPs)
            colXml.SetAttribute("Comment", Me.Comment)

            Return True
            Exit Function

On_Error:
            errorMessege.Add("Error saving Compensation " & ID)
            Return False
        End Function

        Public ReadOnly Property Bookingtype() As Trinity.cBookingType
            Get
                Return _bookingType
            End Get
        End Property

        Public Property Comment() As String
            Get
                Return _comment
            End Get
            Set(ByVal value As String)
                _comment = value
            End Set
        End Property

        Public Property Expense() As Single
            Get
                Return _expense
            End Get
            Set(ByVal value As Single)
                _expense = value
            End Set
        End Property

        Public Property TRPs() As Single
            Get
                Return _trps
            End Get
            Set(ByVal value As Single)
                _trps = value
            End Set
        End Property

        Public Property ToDate() As Date
            Get
                Return _toDate
            End Get
            Set(ByVal value As Date)
                _toDate = value
            End Set
        End Property

        Public Property FromDate() As Date
            Get
                Return _fromDate
            End Get
            Set(ByVal value As Date)
                _fromDate = value
            End Set
        End Property

        Public Property ID() As String
            Get
                Return mvarID
            End Get
            Set(ByVal value As String)
                If ParentColl.Contains(mvarID) Then
                    ParentColl.Remove(mvarID)
                End If
                mvarID = value
                ParentColl.Add(Me, mvarID)

                mvarID = value
            End Set
        End Property

        Public Sub New(ByVal Bookingtype As Trinity.cBookingType, ByVal ParentC As Collection)
            mvarID = Guid.NewGuid.ToString
            _bookingType = Bookingtype
            ParentColl = ParentC
        End Sub

        Public Function TRPMainTarget() As Single
            Return _trps * (_bookingType.IndexMainTarget / 100) * _bookingType.BuyingTarget.UniIndex(_fromDate.ToOADate, True)
        End Function

        Public Function TRPAllAdults() As Single
            Return _trps * (_bookingType.IndexAllAdults / 100) * _bookingType.BuyingTarget.UniIndex(_fromDate.ToOADate, True)
        End Function
    End Class
End Namespace