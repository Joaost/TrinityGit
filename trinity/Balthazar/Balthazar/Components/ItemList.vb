Public Class ItemList
    Public Items As New ItemListItemCollection(Me)

    Protected Overrides Sub OnInvalidated(ByVal e As System.Windows.Forms.InvalidateEventArgs)
        MyBase.OnInvalidated(e)
    End Sub

    Friend Sub Redraw()
        Dim MaxValueWidth As Integer
        Dim MaxTextWidth As Integer = 0
        Dim LastBottom As Integer = 0

        If pnlText.Controls.Count <= Items.Count Then
            Dim i As Integer = 0
            While i < pnlText.Controls.Count
                DirectCast(pnlText.Controls.Item("lblText" & i), Label).Text = Items(i).Text
                DirectCast(pnlValue.Controls.Item("lblValue" & i), Label).Text = Items(i).FormattedValue

                If DirectCast(pnlText.Controls.Item("lblText" & i), Label).Width > MaxTextWidth Then MaxTextWidth = DirectCast(pnlText.Controls.Item("lblText" & i), Label).Width
                If DirectCast(pnlValue.Controls.Item("lblValue" & i), Label).Width > MaxValueWidth Then MaxValueWidth = DirectCast(pnlValue.Controls.Item("lblValue" & i), Label).Width

                pnlValue.Left = Me.Width - pnlValue.Width - 3
                pnlText.Width = pnlValue.Left - 9
                LastBottom = DirectCast(pnlText.Controls.Item("lblText" & i), Label).Bottom

                i += 1
            End While
            While i < Items.Count
                pnlValue.AutoSizeMode = Windows.Forms.AutoSizeMode.GrowAndShrink
                Dim TmpValue As New Label
                TmpValue.AutoSize = True
                TmpValue.Name = "lblValue" & i
                TmpValue.Text = Items(i).FormattedValue
                TmpValue.Visible = True
                '                If TmpValue.Width > MaxValueWidth Then MaxValueWidth = TmpValue.Width
                pnlValue.Controls.Add(TmpValue)
                TmpValue.Top = LastBottom + 3
                pnlValue.Left = Me.Width - pnlValue.Width - 3

                pnlText.Width = pnlValue.Left - 9
                Dim TmpText As New Label
                TmpText.Width = pnlText.Width
                TmpText.AutoEllipsis = True
                TmpText.Name = "lblText" & i
                TmpText.Text = Items(i).Text
                TmpText.Visible = True
                TmpText.Anchor = AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Top
                '                If TmpText.Width > MaxTextWidth Then MaxTextWidth = TmpText.Width
                pnlText.Controls.Add(TmpText)
                TmpText.Top = LastBottom + 3

                LastBottom = TmpText.Bottom

                i += 1
            End While
        ElseIf pnlText.Controls.Count > Items.Count Then
            For i As Integer = 0 To Items.Count - 1
                DirectCast(pnlText.Controls.Item("lblText" & i), Label).Text = Items(i).Text
                DirectCast(pnlValue.Controls.Item("lblValue" & i), Label).Text = Items(i).FormattedValue

                If DirectCast(pnlText.Controls.Item("lblText" & i), Label).Width > MaxTextWidth Then MaxTextWidth = DirectCast(pnlText.Controls.Item("lblText" & i), Label).Width
                If DirectCast(pnlValue.Controls.Item("lblValue" & i), Label).Width > MaxValueWidth Then MaxValueWidth = DirectCast(pnlValue.Controls.Item("lblValue" & i), Label).Width

                pnlValue.Left = Me.Width - pnlValue.Width - 3
                pnlText.Width = pnlValue.Left - 9
                LastBottom = DirectCast(pnlText.Controls.Item("lblText" & i), Label).Bottom
            Next
            For i As Integer = Items.Count To pnlText.Controls.Count - 1
                pnlText.Controls.RemoveByKey("lblText" & i)
                pnlValue.Controls.RemoveByKey("lblValue" & i)
            Next
        End If
    End Sub

End Class

Public Class ItemListItem
    Public Text As String
    Public Value As Single
    Public ValueFormat As String

    Public ReadOnly Property FormattedValue() As String
        Get
            Return Format(Value, ValueFormat)
        End Get
    End Property
End Class

Public Class ItemListItemCollection
    Implements ICollection(Of ItemListItem)

    Private Base As ItemList
    Private mCol As New List(Of ItemListItem)

    Public Sub Add(ByVal item As ItemListItem) Implements System.Collections.Generic.ICollection(Of ItemListItem).Add
        mCol.Add(item)

        Base.Redraw()
    End Sub

    Public Sub Add(ByVal Text As String, ByVal Value As Single, Optional ByVal ValueFormat As String = "N1")
        Dim TmpItem As New ItemListItem
        TmpItem.Text = Text
        TmpItem.Value = Value
        TmpItem.ValueFormat = ValueFormat
        Add(TmpItem)
    End Sub

    Public Sub Clear() Implements System.Collections.Generic.ICollection(Of ItemListItem).Clear
        mCol.Clear()
    End Sub

    Public Function Contains(ByVal item As ItemListItem) As Boolean Implements System.Collections.Generic.ICollection(Of ItemListItem).Contains
        Return mCol.Contains(item)
    End Function

    Public Sub CopyTo(ByVal array() As ItemListItem, ByVal arrayIndex As Integer) Implements System.Collections.Generic.ICollection(Of ItemListItem).CopyTo
        mCol.CopyTo(array, arrayIndex)
    End Sub

    Public ReadOnly Property Count() As Integer Implements System.Collections.Generic.ICollection(Of ItemListItem).Count
        Get
            Return mCol.Count
        End Get
    End Property

    Public ReadOnly Property IsReadOnly() As Boolean Implements System.Collections.Generic.ICollection(Of ItemListItem).IsReadOnly
        Get

        End Get
    End Property

    Public Function Remove(ByVal item As ItemListItem) As Boolean Implements System.Collections.Generic.ICollection(Of ItemListItem).Remove
        mCol.Remove(item)
    End Function

    Public Function GetEnumerator() As System.Collections.Generic.IEnumerator(Of ItemListItem) Implements System.Collections.Generic.IEnumerable(Of ItemListItem).GetEnumerator
        Return mCol.GetEnumerator
    End Function

    Public Function GetEnumerator1() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
        Return mCol.GetEnumerator
    End Function

    Default Public ReadOnly Property Item(ByVal index As Integer) As ItemListItem
        Get
            Return mCol(index)
        End Get
    End Property

    Public Sub New(ByVal BaseClass As ItemList)
        Base = BaseClass
    End Sub
End Class