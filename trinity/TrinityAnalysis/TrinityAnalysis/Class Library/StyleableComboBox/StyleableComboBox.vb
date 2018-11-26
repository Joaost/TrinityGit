Imports Microsoft.VisualBasic.Compatibility
Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports System.Drawing.Drawing2D
Imports System.Runtime.CompilerServices


Public Class StyleableCombobox
    Inherits ComboBox

    Private _styles As New Dictionary(Of Object, StyleableComboboxStyle)

    Public Sub SetItemStyle(ByVal Item As Object, ByVal Style As StyleableComboboxStyle)
        If _styles.ContainsKey(Item) Then
            _styles.Remove(Item)
        End If
        _styles.Add(Item, Style)
    End Sub

    Public Function GetItemStyle(ByVal Item As Object) As StyleableComboboxStyle
        If _styles.ContainsKey(Item) Then
            Return _styles(Item)
        Else
            Return Nothing
        End If
    End Function

    Protected Overrides Sub OnDrawItem(ByVal e As System.Windows.Forms.DrawItemEventArgs)
        If e.Index < 0 Then
            e.DrawBackground()
            e.DrawFocusRectangle()
            Exit Sub
        End If
        Dim Obj As Object = MyBase.Items(e.Index)
        Dim FontStyle As FontStyle = e.Font.Style
        Dim ForeColor As Color = e.ForeColor
        If Not Obj Is Nothing AndAlso _styles.ContainsKey(Obj) AndAlso Not _styles(Obj).FontStyle = Nothing Then
            FontStyle = _styles(Obj).FontStyle
        End If
        If Not Obj Is Nothing AndAlso _styles.ContainsKey(Obj) AndAlso Not _styles(Obj).ForeColor = Nothing Then
            ForeColor = _styles(Obj).ForeColor
        End If
        e.DrawBackground()
        e.Graphics.DrawString(Obj.ToString, New Font(e.Font, FontStyle), New SolidBrush(ForeColor), e.Bounds, StringFormat.GenericDefault)
        e.DrawFocusRectangle()
    End Sub
End Class
