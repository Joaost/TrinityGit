Public Class cHTMLTable

    Private _cssStyle As String
    Public Property CssStyle() As String
        Get
            Return _cssStyle
        End Get
        Set(ByVal value As String)
            _cssStyle = value
        End Set
    End Property

    Private _columns As New cHTMLTableColumnCollection(Me)
    Public Property Columns() As cHTMLTableColumnCollection
        Get
            Return _columns
        End Get
        Set(ByVal value As cHTMLTableColumnCollection)
            _columns = value
        End Set
    End Property

    Private _rows As New cHTMLTableRowCollection(Me)
    Public Property Rows() As cHTMLTableRowCollection
        Get
            Return _rows
        End Get
        Set(ByVal value As cHTMLTableRowCollection)
            _rows = value
        End Set
    End Property

    Private _showHeader As Boolean = True
    Public Property ShowHeader() As Boolean
        Get
            Return _showHeader
        End Get
        Set(ByVal value As Boolean)
            _showHeader = value
        End Set
    End Property

    Private _headerCssStyle As String
    Public Property HeaderCssStyle() As String
        Get
            Return _headerCssStyle
        End Get
        Set(ByVal value As String)
            _headerCssStyle = value
        End Set
    End Property

    Function ToHTML() As String
        Dim TmpHTML As New System.Text.StringBuilder
        TmpHTML.AppendLine("Version:1.0")
        TmpHTML.AppendLine("StartHTML:00000000")
        TmpHTML.AppendLine("EndHTML:00000000")
        TmpHTML.AppendLine("StartFragment:00000000")
        TmpHTML.AppendLine("EndFragment:00000000")

        Dim StartHTML As Integer = System.Text.Encoding.GetEncoding(0).GetString(System.Text.Encoding.UTF8.GetBytes(TmpHTML.ToString)).Length

        TmpHTML.AppendLine("<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">")
        TmpHTML.AppendLine("<html><body>")
        TmpHTML.AppendLine("<!--StartFragment -->")

        Dim StartFragment As Integer = System.Text.Encoding.GetEncoding(0).GetString(System.Text.Encoding.UTF8.GetBytes(TmpHTML.ToString)).Length

        TmpHTML.AppendLine(StartTag) '<table>

        'Build headerrow
        If _showHeader Then
            Dim TmpHeader As New System.Text.StringBuilder("<tr")
            If _headerCssStyle <> "" Then
                TmpHeader.Append(" style=""" & _headerCssStyle & """")
            End If
            TmpHeader.Append(">")
            For Each TmpCol As cHTMLTableColumn In _columns
                TmpHeader.AppendLine(vbTab & TmpCol.HeaderCell.StartTag & TmpCol.HeaderCell.Text & "</td>")
            Next
            TmpHeader.AppendLine("</tr>")
            TmpHTML.AppendLine(TmpHeader.ToString)
        End If

        'Add rows
        For Each TmpRow As cHTMLTableRow In _rows
            TmpHTML.AppendLine(TmpRow.StartTag)
            For Each TmpCell As cHTMLTableCell In TmpRow.Cells
                If TmpCell.Number = 0 Then
                    TmpHTML.AppendLine(vbTab & TmpCell.StartTag & TmpCell.Text & "</td>")
                Else
                    TmpHTML.AppendLine(vbTab & TmpCell.StartTag & TmpCell.Number & "</td>")
                End If

            Next
            TmpHTML.AppendLine("</tr>")
        Next

        TmpHTML.AppendLine("</table>")

        Dim EndFragment As Integer = System.Text.Encoding.GetEncoding(0).GetString(System.Text.Encoding.UTF8.GetBytes(TmpHTML.ToString)).Length

        TmpHTML.AppendLine("<!--EndFragment--></body></html>")

        Dim EndHTML As Integer = System.Text.Encoding.GetEncoding(0).GetString(System.Text.Encoding.UTF8.GetBytes(TmpHTML.ToString)).Length

        TmpHTML.Replace("StartHTML:00000000", "StartHTML:" & Format(StartHTML))
        TmpHTML.Replace("EndHTML:00000000", "EndHTML:" & Format(EndHTML))
        TmpHTML.Replace("StartFragment:00000000", "StartFragment:" & Format(StartFragment))
        TmpHTML.Replace("EndFragment:00000000", "EndFragment:" & Format(EndFragment))

        Return System.Text.Encoding.GetEncoding(0).GetString(System.Text.Encoding.UTF8.GetBytes(TmpHTML.ToString))
    End Function

    Friend Function StartTag() As String
        If _cssStyle = "" Then
            Return "<table>"
        Else
            Return "<table style=""" & _cssStyle & """>"
        End If
    End Function
End Class

Public Class cHTMLTableColumnCollection
    Implements ICollection(Of cHTMLTableColumn)

    Private _collection As New List(Of cHTMLTableColumn)

    Public Sub Add(ByVal item As cHTMLTableColumn) Implements System.Collections.Generic.ICollection(Of cHTMLTableColumn).Add
        _collection.Add(item)
    End Sub

    Public Function Add() As cHTMLTableColumn
        Dim _tmpCol As New cHTMLTableColumn
        Add(_tmpCol)
        Return _tmpCol
    End Function

    Public Sub Clear() Implements System.Collections.Generic.ICollection(Of cHTMLTableColumn).Clear
        _collection.Clear()
    End Sub

    Default Public ReadOnly Property Item(ByVal Index As Integer) As cHTMLTableColumn
        Get
            Return _collection(Index)
        End Get
    End Property

    Public Function Contains(ByVal item As cHTMLTableColumn) As Boolean Implements System.Collections.Generic.ICollection(Of cHTMLTableColumn).Contains
        Return _collection.Contains(item)
    End Function

    Public Sub CopyTo(ByVal array() As cHTMLTableColumn, ByVal arrayIndex As Integer) Implements System.Collections.Generic.ICollection(Of cHTMLTableColumn).CopyTo
        _collection.CopyTo(array, arrayIndex)
    End Sub

    Public ReadOnly Property Count() As Integer Implements System.Collections.Generic.ICollection(Of cHTMLTableColumn).Count
        Get
            Return _collection.Count
        End Get
    End Property

    Public ReadOnly Property IsReadOnly() As Boolean Implements System.Collections.Generic.ICollection(Of cHTMLTableColumn).IsReadOnly
        Get
            Return False
        End Get
    End Property

    Public Function Remove(ByVal item As cHTMLTableColumn) As Boolean Implements System.Collections.Generic.ICollection(Of cHTMLTableColumn).Remove
        _collection.Remove(item)
    End Function

    Public Function GetEnumerator() As System.Collections.Generic.IEnumerator(Of cHTMLTableColumn) Implements System.Collections.Generic.IEnumerable(Of cHTMLTableColumn).GetEnumerator
        Return _collection.GetEnumerator
    End Function

    Public Function GetEnumerator1() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
        Return _collection.GetEnumerator
    End Function

    Private _table As cHTMLTable
    Public Sub New(ByVal table As cHTMLTable)
        _table = table
    End Sub
End Class

Public Class cHTMLTableRowCollection
    Implements ICollection(Of cHTMLTableRow)

    Private _collection As New List(Of cHTMLTableRow)

    Public Sub Add(ByVal item As cHTMLTableRow) Implements System.Collections.Generic.ICollection(Of cHTMLTableRow).Add
        _collection.Add(item)
    End Sub

    Public Function Add() As cHTMLTableRow
        Dim _tmpRow As New cHTMLTableRow
        For Each TmpCol As cHTMLTableColumn In _table.Columns
            Dim _tmpCell As New cHTMLTableCell
            _tmpCell.CSSStyle = TmpCol.CSSStyle
            _tmpRow.Cells.Add(_tmpCell)
        Next
        Add(_tmpRow)
        Return _tmpRow
    End Function

    Public Sub Clear() Implements System.Collections.Generic.ICollection(Of cHTMLTableRow).Clear
        _collection.Clear()
    End Sub

    Public Function Contains(ByVal item As cHTMLTableRow) As Boolean Implements System.Collections.Generic.ICollection(Of cHTMLTableRow).Contains
        Return _collection.Contains(item)
    End Function

    Public Sub CopyTo(ByVal array() As cHTMLTableRow, ByVal arrayIndex As Integer) Implements System.Collections.Generic.ICollection(Of cHTMLTableRow).CopyTo
        _collection.CopyTo(array, arrayIndex)
    End Sub

    Public ReadOnly Property Count() As Integer Implements System.Collections.Generic.ICollection(Of cHTMLTableRow).Count
        Get
            Return _collection.Count
        End Get
    End Property

    Public ReadOnly Property IsReadOnly() As Boolean Implements System.Collections.Generic.ICollection(Of cHTMLTableRow).IsReadOnly
        Get
            Return False
        End Get
    End Property

    Public Function Remove(ByVal item As cHTMLTableRow) As Boolean Implements System.Collections.Generic.ICollection(Of cHTMLTableRow).Remove
        _collection.Remove(item)
    End Function

    Public Function GetEnumerator() As System.Collections.Generic.IEnumerator(Of cHTMLTableRow) Implements System.Collections.Generic.IEnumerable(Of cHTMLTableRow).GetEnumerator
        Return _collection.GetEnumerator
    End Function

    Public Function GetEnumerator1() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
        Return _collection.GetEnumerator
    End Function

    Private _table As cHTMLTable
    Public Sub New(ByVal table As cHTMLTable)
        _table = table
    End Sub
End Class

Public Class cHTMLTableCellCollection
    Implements ICollection(Of cHTMLTableCell)

    Private _collection As New List(Of cHTMLTableCell)

    Public Sub Add(ByVal item As cHTMLTableCell) Implements System.Collections.Generic.ICollection(Of cHTMLTableCell).Add
        _collection.Add(item)
    End Sub

    Public Function Add() As cHTMLTableCell
        Dim _tmpCell As New cHTMLTableCell
        Add(_tmpCell)
        Return _tmpCell
    End Function

    Public Sub Clear() Implements System.Collections.Generic.ICollection(Of cHTMLTableCell).Clear
        _collection.Clear()
    End Sub

    Public Function Contains(ByVal item As cHTMLTableCell) As Boolean Implements System.Collections.Generic.ICollection(Of cHTMLTableCell).Contains
        Return _collection.Contains(item)
    End Function

    Public Sub CopyTo(ByVal array() As cHTMLTableCell, ByVal arrayIndex As Integer) Implements System.Collections.Generic.ICollection(Of cHTMLTableCell).CopyTo
        _collection.CopyTo(array, arrayIndex)
    End Sub

    Default Public ReadOnly Property Item(ByVal index As Integer) As cHTMLTableCell
        Get
            Return _collection(index)
        End Get
    End Property

    Public ReadOnly Property Count() As Integer Implements System.Collections.Generic.ICollection(Of cHTMLTableCell).Count
        Get
            Return _collection.Count
        End Get
    End Property

    Public ReadOnly Property IsReadOnly() As Boolean Implements System.Collections.Generic.ICollection(Of cHTMLTableCell).IsReadOnly
        Get
            Return False
        End Get
    End Property

    Public Function Remove(ByVal item As cHTMLTableCell) As Boolean Implements System.Collections.Generic.ICollection(Of cHTMLTableCell).Remove
        _collection.Remove(item)
    End Function

    Public Function GetEnumerator() As System.Collections.Generic.IEnumerator(Of cHTMLTableCell) Implements System.Collections.Generic.IEnumerable(Of cHTMLTableCell).GetEnumerator
        Return _collection.GetEnumerator
    End Function

    Public Function GetEnumerator1() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
        Return _collection.GetEnumerator
    End Function
End Class

Public Class cHTMLTableColumn

    Private _cssStyle As String
    Public Property CSSStyle() As String
        Get
            Return _cssStyle
        End Get
        Set(ByVal value As String)
            _cssStyle = value
        End Set
    End Property

    Private _headerCell As cHTMLTableCell
    Public Property HeaderCell() As cHTMLTableCell
        Get
            If _headerCell Is Nothing Then _headerCell = New cHTMLTableCell
            Return _headerCell
        End Get
        Set(ByVal value As cHTMLTableCell)
            _headerCell = value
        End Set
    End Property

End Class

Public Class cHTMLTableRow

    Private _cssStyle As String
    Public Property CSSStyle() As String
        Get
            Return _cssStyle
        End Get
        Set(ByVal value As String)
            _cssStyle = value
        End Set
    End Property

    Private _cells As cHTMLTableCellCollection
    Public Property Cells() As cHTMLTableCellCollection
        Get
            If _cells Is Nothing Then _cells = New cHTMLTableCellCollection
            Return _cells
        End Get
        Set(ByVal value As cHTMLTableCellCollection)
            _cells = value
        End Set
    End Property

    Friend Function StartTag() As String
        If _cssStyle = "" Then
            Return "<tr>"
        Else
            Return "<tr style=""" & _cssStyle & """>"
        End If
    End Function

End Class

Public Class cHTMLTableCell

    Private _cssStyle As String
    Public Property CSSStyle() As String
        Get
            Return _cssStyle
        End Get
        Set(ByVal value As String)
            _cssStyle = value
        End Set
    End Property

    Private _colSpan As Integer = 1
    Public Property ColumnSpan() As Integer
        Get
            Return _colSpan
        End Get
        Set(ByVal value As Integer)
            _colSpan = value
        End Set
    End Property

    Private _text As String
    Public Property Text() As String
        Get
            If _text = "" Then
                Return "&nbsp;"
            End If
            Return _text
        End Get
        Set(ByVal value As String)
            _text = value
        End Set
    End Property

    Private _number As Double
    Public Property Number() As Double
        Get
            If _number = 0 Then
                Return 0
            End If
        End Get
        Set(ByVal value As Double)
            _number = value
        End Set
    End Property

    Friend Function StartTag() As String
        Dim _tag As New System.Text.StringBuilder("<td")
        If _cssStyle <> "" Then
            _tag.Append(" style=""" & _cssStyle & """")
        End If
        If _colSpan > 1 Then
            _tag.Append(" colspan=""" & _colSpan & """")
        End If
        _tag.Append(">")
        Return _tag.ToString
    End Function
End Class
