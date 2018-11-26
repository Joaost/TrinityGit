Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports System.Drawing.Drawing2D


Public Class ExtendedDateTimePicker
    Inherits DateTimePicker

    Public Declare Auto Function GetWindowLong Lib "user32.dll" ( _
        ByVal hWnd As IntPtr, _
        ByVal nIndex As Int32 _
    ) As Int32

    Private Declare Auto Function SetWindowLong Lib "user32.dll" ( _
        ByVal hWnd As IntPtr, _
        ByVal nIndex As Int32, _
        ByVal dwNewLong As Int32 _
    ) As Int32

    Private Const GWL_STYLE As Int32 = (-16)

    Private Const MCM_FIRST As Int32 = &H1000
    Private Const MCM_GETMINREQRECT As Int32 = (MCM_FIRST + 9)
    Private Const MCS_WEEKNUMBERS As Int32 = &H4

    Private Const DTM_FIRST As Int32 = &H1000
    Private Const DTM_GETMONTHCAL As Int32 = (DTM_FIRST + 8)

    Private Declare Auto Function SendMessage Lib "user32.dll" ( _
        ByVal hWnd As IntPtr, _
        ByVal uMsg As Int32, _
        ByVal lParam As Int32, _
        ByVal lpData As Int32 _
    ) As IntPtr

    Private Declare Auto Function SendMessage Lib "user32.dll" ( _
        ByVal hWnd As IntPtr, _
        ByVal uMsg As Int32, _
        ByVal lParam As Int32, _
        ByRef lpData As Rectangle _
    ) As Int32

    Private Declare Function MoveWindow Lib "user32.dll" ( _
        ByVal hwnd As IntPtr, _
        ByVal x As Int32, _
        ByVal y As Int32, _
        ByVal nWidth As Int32, _
        ByVal nHeight As Int32, _
        ByVal bRepaint As Boolean _
    ) As Int32

    Private m_ShowWeekNumbers As Boolean

    < _
        Browsable(True), _
        DesignerSerializationVisibility( _
            DesignerSerializationVisibility.Visible _
        ) _
    > _
    Public Property ShowWeekNumbers() As Boolean
        Get
            Return m_ShowWeekNumbers
        End Get
        Set(ByVal Value As Boolean)
            m_ShowWeekNumbers = Value
        End Set
    End Property

    Protected Overloads Overrides Sub OnDropDown(ByVal e As EventArgs)
        Dim hMonthView As IntPtr = _
            SendMessage(Me.Handle, DTM_GETMONTHCAL, 0, 0)
        Dim dwStyle As Int32 = GetWindowLong(hMonthView, GWL_STYLE)
        If Me.ShowWeekNumbers Then
            dwStyle = dwStyle Or MCS_WEEKNUMBERS
        Else
            dwStyle = dwStyle And Not MCS_WEEKNUMBERS
        End If
        Dim rct As Rectangle
        SetWindowLong(hMonthView, GWL_STYLE, dwStyle)
        SendMessage(hMonthView, MCM_GETMINREQRECT, 0, rct)
        MoveWindow(hMonthView, 0, 0, rct.Right + 2, rct.Bottom, True)
        MyBase.OnDropDown(e)
    End Sub
End Class

Public Class CalendarColumn
    Inherits DataGridViewColumn

    Public Sub New()
        MyBase.New(New CalendarCell())
    End Sub

    Public Overrides Property CellTemplate() As DataGridViewCell
        Get
            Return MyBase.CellTemplate
        End Get
        Set(ByVal value As DataGridViewCell)

            ' Ensure that the cell used for the template is a CalendarCell.
            If Not (value Is Nothing) AndAlso _
                Not value.GetType().IsAssignableFrom(GetType(CalendarCell)) _
                Then
                Throw New InvalidCastException("Must be a CalendarCell")
            End If
            MyBase.CellTemplate = value

        End Set
    End Property

End Class

Public Class CalendarCell
    Inherits DataGridViewTextBoxCell

    Public Sub New()
        ' Use the short date format.
        Me.Style.Format = "d"
    End Sub

    Public Overrides Sub InitializeEditingControl(ByVal rowIndex As Integer, _
        ByVal initialFormattedValue As Object, _
        ByVal dataGridViewCellStyle As DataGridViewCellStyle)

        ' Set the value of the editing control to the current cell value.
        MyBase.InitializeEditingControl(rowIndex, initialFormattedValue, _
            dataGridViewCellStyle)

        Dim ctl As CalendarEditingControl = _
            CType(DataGridView.EditingControl, CalendarEditingControl)
        ctl.Value = CType(Me.Value, DateTime)

    End Sub

    Public Overrides ReadOnly Property EditType() As Type
        Get
            ' Return the type of the editing contol that CalendarCell uses.
            Return GetType(CalendarEditingControl)
        End Get
    End Property

    Public Overrides ReadOnly Property ValueType() As Type
        Get
            ' Return the type of the value that CalendarCell contains.
            Return GetType(DateTime)
        End Get
    End Property

    Public Overrides ReadOnly Property DefaultNewRowValue() As Object
        Get
            ' Use the current date and time as the default value.
            Return DateTime.Now
        End Get
    End Property

End Class

Class CalendarEditingControl
    Inherits ExtendedDateTimePicker
    Implements IDataGridViewEditingControl

    Private dataGridViewControl As DataGridView
    Private valueIsChanged As Boolean = False
    Private rowIndexNum As Integer

    Public Sub New()
        Me.Format = DateTimePickerFormat.Short
        Me.ShowWeekNumbers = True
    End Sub

    Public Property EditingControlFormattedValue() As Object _
        Implements IDataGridViewEditingControl.EditingControlFormattedValue

        Get
            Return Me.Value.ToShortDateString()
        End Get

        Set(ByVal value As Object)
            If TypeOf value Is [String] Then
                Me.Value = DateTime.Parse(CStr(value))
            End If
        End Set

    End Property

    Public Function GetEditingControlFormattedValue(ByVal context _
        As DataGridViewDataErrorContexts) As Object _
        Implements IDataGridViewEditingControl.GetEditingControlFormattedValue

        Return Me.Value.ToShortDateString()

    End Function

    Public Sub ApplyCellStyleToEditingControl(ByVal dataGridViewCellStyle As _
        DataGridViewCellStyle) _
        Implements IDataGridViewEditingControl.ApplyCellStyleToEditingControl

        Me.Font = dataGridViewCellStyle.Font
        Me.CalendarForeColor = dataGridViewCellStyle.ForeColor
        Me.CalendarMonthBackground = dataGridViewCellStyle.BackColor

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

    Protected Overrides Sub OnValueChanged(ByVal eventargs As EventArgs)

        ' Notify the DataGridView that the contents of the cell have changed.
        valueIsChanged = True
        Me.EditingControlDataGridView.NotifyCurrentCellDirty(True)
        MyBase.OnValueChanged(eventargs)

    End Sub

End Class