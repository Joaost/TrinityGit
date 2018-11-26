Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports System.Drawing.Drawing2D

Public Class ExtendedComboboxColumn
    Inherits DataGridViewColumn

    Public Sub New()
        MyBase.New(New ExtendedComboBoxCell())
    End Sub

    Public Overrides Property CellTemplate() As DataGridViewCell
        Get
            Return MyBase.CellTemplate
        End Get
        Set(ByVal value As DataGridViewCell)

            ' Ensure that the cell used for the template is a ExtendedComboBoxCell.
            If Not (value Is Nothing) AndAlso _
                Not value.GetType().IsAssignableFrom(GetType(ExtendedComboBoxCell)) _
                Then
                Throw New InvalidCastException("Must be an ExtendedComboBoxCell")
            End If
            MyBase.CellTemplate = value
        End Set
    End Property

End Class

Public Class ExtendedComboBoxCell
    Inherits DataGridViewTextBoxCell

    Private myCombo As New ComboboxEditingControl

    Public Overloads Property Value() As Object
        Get
            Return ComboBox.SelectedItem
        End Get
        Set(ByVal value As Object)
            ComboBox.SelectedItem = value
        End Set
    End Property

    Protected Overrides Function GetFormattedValue(ByVal value As Object, ByVal rowIndex As Integer, ByRef cellStyle As System.Windows.Forms.DataGridViewCellStyle, ByVal valueTypeConverter As System.ComponentModel.TypeConverter, ByVal formattedValueTypeConverter As System.ComponentModel.TypeConverter, ByVal context As System.Windows.Forms.DataGridViewDataErrorContexts) As Object
        Return MyBase.GetFormattedValue(value, rowIndex, cellStyle, valueTypeConverter, formattedValueTypeConverter, context)
    End Function

    Protected Overrides Function SetValue(ByVal rowIndex As Integer, ByVal value As Object) As Boolean
        Return MyBase.SetValue(rowIndex, value)
    End Function

    Public Overrides Function ParseFormattedValue(ByVal formattedValue As Object, ByVal cellStyle As System.Windows.Forms.DataGridViewCellStyle, ByVal formattedValueTypeConverter As System.ComponentModel.TypeConverter, ByVal valueTypeConverter As System.ComponentModel.TypeConverter) As Object
        'Stop
        Return formattedValue
    End Function

    Public Overrides Sub InitializeEditingControl(ByVal rowIndex As Integer, _
    ByVal initialFormattedValue As Object, _
    ByVal dataGridViewCellStyle As DataGridViewCellStyle)

        ' Set the value of the editing control to the current cell value.
        MyBase.InitializeEditingControl(rowIndex, initialFormattedValue, _
            dataGridViewCellStyle)

        Dim ctl As ComboboxEditingControl = _
            CType(DataGridView.EditingControl, ComboboxEditingControl)
        With ctl
            .DropDownStyle = myCombo.DropDownStyle
            .AutoCompleteSource = AutoCompleteSource.ListItems
            .AutoCompleteMode = AutoCompleteMode.Append
            .DisplayMember = myCombo.DisplayMember
            .Items.Clear()
            For Each o As Object In myCombo.Items
                .Items.Add(o)
            Next
            .SelectedItem = MyBase.GetValue(rowIndex)
            AddHandler .SelectedValueChanged, AddressOf SelectedValueChanged
            AddHandler .TextChanged, AddressOf SelectedValueChanged
        End With

    End Sub

    Public Property DropDownStyle() As ComboBoxStyle
        Get
            Return myCombo.DropDownStyle
        End Get
        Set(ByVal value As ComboBoxStyle)
            myCombo.DropDownStyle = value
        End Set
    End Property

    Friend Sub SelectedValueChanged(ByVal sender As Object, ByVal e As EventArgs)
        myCombo.SelectedItem = sender.selecteditem
        myCombo.Text = sender.text
    End Sub

    Public ReadOnly Property Items() As Windows.Forms.ComboBox.ObjectCollection
        Get
            Return CType(myCombo, ComboboxEditingControl).Items
        End Get
    End Property

    Public ReadOnly Property SelectedItem()
        Get
            Return CType(myCombo, ComboboxEditingControl).SelectedItem
        End Get
    End Property

    Protected Overrides Function GetValue(ByVal rowIndex As Integer) As Object
        Return MyBase.GetValue(rowIndex)
    End Function

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

    Public ReadOnly Property ComboBox() As ComboboxEditingControl
        Get
            Return myCombo
        End Get
    End Property
End Class

Public Class ComboboxEditingControl
    Inherits ComboBox
    Implements IDataGridViewEditingControl

    Private dataGridViewControl As DataGridView
    Private valueIsChanged As Boolean = False
    Private rowIndexNum As Integer

    Public Overrides Property Text() As String
        Get
            Return MyBase.Text
        End Get
        Set(ByVal value As String)
            MyBase.Text = value
        End Set
    End Property

    Public Property EditingControlFormattedValue() As Object _
        Implements IDataGridViewEditingControl.EditingControlFormattedValue

        Get
            Return Me.SelectedItem
        End Get

        Set(ByVal value As Object)
            Me.SelectedItem = value
        End Set

    End Property

    Public Function GetEditingControlFormattedValue(ByVal context _
        As DataGridViewDataErrorContexts) As Object _
        Implements IDataGridViewEditingControl.GetEditingControlFormattedValue

        Return Me.Text
    End Function

    Public Sub ApplyCellStyleToEditingControl(ByVal dataGridViewCellStyle As _
        DataGridViewCellStyle) _
        Implements IDataGridViewEditingControl.ApplyCellStyleToEditingControl

        Me.Font = dataGridViewCellStyle.Font
        Me.ForeColor = dataGridViewCellStyle.ForeColor
        Me.BackColor = dataGridViewCellStyle.BackColor

    End Sub

    Public Property EditingControlRowIndex() As Integer _
        Implements IDataGridViewEditingControl.EditingControlRowIndex

        Get
            Return rowIndexNum
        End Get
        Set(ByVal value As Integer)
            rowIndexNum = value
        End Set

    End Property

    Public Function EditingControlWantsInputKey(ByVal key As Keys, _
        ByVal dataGridViewWantsInputKey As Boolean) As Boolean _
        Implements IDataGridViewEditingControl.EditingControlWantsInputKey

        ' Let the DateTimePicker handle the keys listed.
        Select Case key And Keys.KeyCode
            Case Keys.Left, Keys.Up, Keys.Down, Keys.Right, _
                Keys.Home, Keys.End, Keys.PageDown, Keys.PageUp

                Return True

            Case Else
                Return False
        End Select

    End Function

    Public Sub PrepareEditingControlForEdit(ByVal selectAll As Boolean) _
        Implements IDataGridViewEditingControl.PrepareEditingControlForEdit

        ' No preparation needs to be done.

    End Sub

    Public ReadOnly Property RepositionEditingControlOnValueChange() _
        As Boolean Implements _
        IDataGridViewEditingControl.RepositionEditingControlOnValueChange

        Get
            Return False
        End Get

    End Property

    Public Property EditingControlDataGridView() As DataGridView _
        Implements IDataGridViewEditingControl.EditingControlDataGridView

        Get
            Return dataGridViewControl
        End Get
        Set(ByVal value As DataGridView)
            dataGridViewControl = value
        End Set

    End Property

    Public Property EditingControlValueChanged() As Boolean _
        Implements IDataGridViewEditingControl.EditingControlValueChanged

        Get
            Return valueIsChanged
        End Get
        Set(ByVal value As Boolean)
            valueIsChanged = value
        End Set

    End Property

    Public ReadOnly Property EditingControlCursor() As Cursor _
        Implements IDataGridViewEditingControl.EditingPanelCursor

        Get
            Return MyBase.Cursor
        End Get

    End Property

    Protected Overrides Sub onSelectedIndexChanged(ByVal eventargs As EventArgs)

        ' Notify the DataGridView that the contents of the cell have changed.
        valueIsChanged = True
        If Me.EditingControlDataGridView IsNot Nothing Then Me.EditingControlDataGridView.NotifyCurrentCellDirty(True)
        MyBase.OnSelectedIndexChanged(eventargs)

    End Sub

    Protected Overrides Sub OnTextChanged(ByVal e As System.EventArgs)
        'Stop
        valueIsChanged = True
        If Me.EditingControlDataGridView IsNot Nothing Then Me.EditingControlDataGridView.NotifyCurrentCellDirty(True)
        MyBase.OnTextChanged(e)
    End Sub
End Class
