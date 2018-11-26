Imports System.Xml


Public Class cXml
    Private _fromDate As Date
    Private _toDate As Date

    Public Function GetAllNodesInCampaign(file As String)

        Dim XmlObj As New XmlDocument
        Dim list As List(Of Object)
        Dim result As New Object

        XmlObj.LoadXml(file)


        Dim q = From camp In XmlObj("Campaign")
                 Select camp

        list = q.ToList()

        For Each node As Object In list
            If node.ToString = "Costs" Then
                result = node
            End If
        Next


        Return list

    End Function



    Public Function GetCriteriaValue(file As String, criteriaValue As String)


        Dim XmlObj As New XmlDocument
        Dim list As List(Of Object)


        XmlObj.LoadXml(file)

        Dim query = From camp In XmlObj("Campaign").GetAttributeNode("BudgetTotalCTC")
                    Select camp

        list = query.ToList()

        Return list

    End Function



    Public Property fromDate() As Date
        Get
            Return _fromDate
        End Get
        Set(ByVal value As Date)
            _fromDate = value
        End Set
    End Property


    Public Property toDate() As Date
        Get
            Return _toDate
        End Get
        Set(ByVal value As Date)
            _toDate = value
        End Set
    End Property
End Class
