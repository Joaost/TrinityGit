Imports Microsoft.VisualBasic.Compatibility
Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports System.Drawing.Drawing2D
Imports System.Runtime.CompilerServices


Public Class ExtendedComboBoxCell
    Inherits DataGridViewTextBoxCell

    Private myCombo As New ComboboxEditingControl

    Public Overrides Function ParseFormattedValue(ByVal formattedValue As Object, ByVal cellStyle As System.Windows.Forms.DataGridViewCellStyle, ByVal formattedValueTypeConverter As System.ComponentModel.TypeConverter, ByVal valueTypeConverter As System.ComponentModel.TypeConverter) As Object
        'Stop
        Return formattedValue
    End Function

    Public Sub SetItemStyle(ByVal Item As Object, ByVal Style As StyleableComboboxStyle)
        myCombo.SetItemStyle(Item, Style)
    End Sub

    Public Overrides Sub InitializeEditingControl(ByVal rowIndex As Integer, _
    ByVal initialFormattedValue As Object, _
    ByVal dataGridViewCellStyle As DataGridViewCellStyle)

        ' Set the value of the editing control to the current cell value.
        MyBase.InitializeEditingControl(rowIndex, initialFormattedValue, _
            dataGridViewCellStyle)

        Dim ctl As ComboboxEditingControl = _
            CType(DataGridView.EditingControl, ComboboxEditingControl)
        With ctl
            .DropDownStyle = ComboBoxStyle.DropDownList
            .Items.Clear()
            For Each o As Object In myCombo.Items
                .Items.Add(o)
                If myCombo.GetItemStyle(o) IsNot Nothing Then
                    .SetItemStyle(o, myCombo.GetItemStyle(o))
                End If
            Next
            '.SelectedItem = Me.Value
        End With

    End Sub

    Public ReadOnly Property Items()
        Get

            Return CType(myCombo, ComboboxEditingControl).Items
        End Get
    End Property

    Public ReadOnly Property SelectedItem()
        Get
            Return CType(myCombo, ComboboxEditingControl).SelectedItem
        End Get
    End Property

    Public Overrides ReadOnly Property EditType() As Type
        Get
            ' Return the type of the editing contol that CalendarCell uses.
            Return GetType(ComboboxEditingControl)
        End Get
    End Property

    Public Overrides ReadOnly Property ValueType() As Type
        Get
            ' Return the type of the value that CalendarCell contains.
            Return GetType(Object)
        End Get
    End Property

    Public Overrides ReadOnly Property DefaultNewRowValue() As Object
        Get
            ' Use the current date and time as the default value.
            Return Nothing
        End Get
    End Property

End Class
