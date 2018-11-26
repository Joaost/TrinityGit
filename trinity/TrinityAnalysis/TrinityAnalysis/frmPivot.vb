Imports System.Linq
Imports System.IO
Imports System.Xml
Imports System.Text
Imports System.Xml.XPath


Public Class frmPivot


    Dim campaignData As DataTable

    Dim availableFields As New List(Of String)
    Dim WeekDays As New List(Of String)
    Dim Months As New List(Of String)
    Dim availableUnits As New List(Of String)
    Dim FieldTranslation As New Dictionary(Of String, String)




    'Public Function GetCampaign(file As String)
    '    Dim objXML As New XmlDocument

    '    objXML.LoadXml(file)

    '    Dim xmlDoc As New XmlDocument
    '    Dim productNodes As XmlNodeList
    '    Dim productNode As XmlNode
    '    Dim baseDataNodes As XmlNodeList
    '    Dim bFirstInRow As Boolean
    '    Dim streng As String = ""

    '    productNodes = objXML.GetElementsByTagName("Campaign")

    '    For Each productNode In productNodes
    '        baseDataNodes = productNode.ChildNodes
    '        bFirstInRow = True

    '        For Each baseDataNode As XmlNode In baseDataNodes


    '            If (bFirstInRow) Then
    '                bFirstInRow = False
    '            Else
    '                If baseDataNode.ChildNodes.Item(1).Name = "BudgetTotalCTC" Then
    '                    Return baseDataNode.Name
    '                End If
    '                Console.WriteLine(", ")
    '            End If
    '            streng = (baseDataNode.Name & ": " & baseDataNode.InnerText)

    '        Next
    '    Next
    '    Return streng
    'End Function


End Class