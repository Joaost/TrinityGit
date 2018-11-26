Namespace Trinity
    Public Class cEnhancement
        Private _bookingtype As Trinity.cBookingType

        Private _ID As String = ""
        Public Amount As Decimal
        Public Name As String
        Private _index As Trinity.cIndex
        Private mvarUseThis As Boolean = True
        Private _parentColl As Collection

        Public Property ID() As String
            Get
                Return _ID
            End Get
            Set(ByVal value As String)
                If _parentColl.Contains(_ID) Then
                    _parentColl.Remove(_ID)
                    _parentColl.Add(Me, value)
                End If
                _ID = value
            End Set
        End Property
        Public Property UseThis() As Boolean
            Get
                Return mvarUseThis
            End Get
            Set(ByVal value As Boolean)
                mvarUseThis = value
            End Set
        End Property


        Public Function GetXML(ByRef colXml As Xml.XmlElement, ByRef errorMessege As List(Of String), ByVal xmlDoc As Xml.XmlDocument) As Boolean
            On Error GoTo On_Error

            colXml.SetAttribute("ID", ID)
            colXml.SetAttribute("Name", Name)
            colXml.SetAttribute("Amount", Amount)

On_Error:
            errorMessege.Add("Error saving Enhancement " & Name)
            Return False
        End Function

        Public Sub New(ByVal Index As Trinity.cIndex, ByVal ParentCollection As Collection)
            _index = Index
            _parentColl = ParentCollection
            ID = CreateGUID()
        End Sub

    End Class
End Namespace