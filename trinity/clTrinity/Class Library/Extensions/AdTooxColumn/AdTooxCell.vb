Imports Microsoft.VisualBasic.Compatibility
Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports System.Drawing.Drawing2D
Imports System.Runtime.CompilerServices


Public Class AdTooxCell
    Inherits DataGridViewTextBoxCell

    Protected Overrides Sub Paint(ByVal graphics As System.Drawing.Graphics, ByVal clipBounds As System.Drawing.Rectangle, ByVal cellBounds As System.Drawing.Rectangle, ByVal rowIndex As Integer, ByVal cellState As System.Windows.Forms.DataGridViewElementStates, ByVal value As Object, ByVal formattedValue As Object, ByVal errorText As String, ByVal cellStyle As System.Windows.Forms.DataGridViewCellStyle, ByVal advancedBorderStyle As System.Windows.Forms.DataGridViewAdvancedBorderStyle, ByVal paintParts As System.Windows.Forms.DataGridViewPaintParts)
        MyBase.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts)
        If (paintParts And DataGridViewPaintParts.ContentBackground) Or (paintParts And DataGridViewPaintParts.ContentForeground) Then
            graphics.FillRectangle(New SolidBrush(cellStyle.BackColor), cellBounds)
            graphics.SmoothingMode = SmoothingMode.HighQuality
            Dim status As Trinity.cAdTooxStatus = Me.Value
            Dim font As New Font("Segoe UI", 7, FontStyle.Bold)
            If status IsNot Nothing AndAlso status.MediaID IsNot Nothing Then
                For level As Integer = 1 To 8
                    Dim _rect As New Rectangle(cellBounds.X + (level - 1) * 10 + 2 * (level), cellBounds.Y + cellBounds.Height / 2 - 5, 10, 10)
                    Dim _brush As Brush
                    If status.Steps(level - 1) IsNot Nothing AndAlso status.Steps(level - 1).Contains("_green") Then
                        _brush = Brushes.Green
                    ElseIf status.Steps(level - 1) IsNot Nothing AndAlso status.Steps(level - 1).Contains("_red") Then
                        _brush = Brushes.Red
                    Else
                        _brush = Brushes.Gray
                    End If
                    If status.Steps(level - 1) IsNot Nothing AndAlso status.Steps(level - 1).Contains("_restricted") Then
                        graphics.FillEllipse(_brush, _rect)
                        graphics.DrawEllipse(Pens.Black, _rect)
                    Else
                        graphics.FillRectangle(_brush, _rect)
                        graphics.DrawRectangle(Pens.Black, _rect)
                    End If
                    graphics.DrawString(level, font, Brushes.White, _rect.Left + _rect.Width / 2 - graphics.MeasureString(level, font).Width / 2 + 1, _rect.Top + _rect.Height / 2 - graphics.MeasureString(level, font).Height / 2 + 1)
                Next
            Else
                graphics.DrawString("No status available", font, Brushes.Red, cellBounds.X + cellBounds.Width / 2 - graphics.MeasureString("No status available", font).Width / 2, cellBounds.Y + cellBounds.Height / 2 - graphics.MeasureString("No Adtoox available", font).Height / 2)
            End If
            graphics.DrawLine(Pens.Gray, cellBounds.Left, cellBounds.Bottom - 1, cellBounds.Right, cellBounds.Bottom - 1)
            graphics.DrawLine(Pens.Gray, cellBounds.Right - 1, cellBounds.Top, cellBounds.Right - 1, cellBounds.Bottom - 1)
        ElseIf paintParts And DataGridViewPaintParts.Border Then
            graphics.DrawRectangle(Pens.Red, clipBounds)
        End If
    End Sub

    Dim _tooltipLabel As Label
    Dim _tooltipLabelContainer As PictureBox
    Dim _screenLeft As Integer
    Dim _screenTop As Integer
    Protected Overrides Sub OnMouseEnter(ByVal rowIndex As Integer)
        If _tooltipLabel Is Nothing Then
            Dim _helpText As String = "E.C. Express Status:" & vbCrLf
            _helpText &= "1. Upload request sent" & vbCrLf
            _helpText &= "2. Upload request verified" & vbCrLf
            _helpText &= "3. Uploaded to E.C. Express" & vbCrLf
            _helpText &= "4. Quality approved by E.C. Express" & vbCrLf
            _helpText &= "5. Content approved by E.C. Express" & vbCrLf
            _helpText &= "6. Delivery accepted by TV Station" & vbCrLf
            _helpText &= "7. Delivered to TV Station" & vbCrLf
            _helpText &= "8. Approved by TV Station" & vbCrLf
            _helpText &= vbCrLf
            _helpText &= "Gray = Step not completed" & vbCrLf
            _helpText &= "Green = Step completed" & vbCrLf
            _helpText &= "Red = Step rejected" & vbCrLf
            _helpText &= vbCrLf
            _helpText &= "Round icons indicate the copy is" & vbCrLf
            _helpText &= "restricted in certain time slots."
            _tooltipLabel = New Label
            _tooltipLabel.Enabled = False
            _tooltipLabel.AutoSize = True
            _tooltipLabel.Text = _helpText
            _tooltipLabel.Visible = True
            _tooltipLabel.Left = 0
            _tooltipLabel.Top = 0

            _tooltipLabelContainer = New PictureBox
            _tooltipLabelContainer.Controls.Add(_tooltipLabel)
            _tooltipLabelContainer.Width = _tooltipLabel.Width + 15
            _tooltipLabelContainer.Height = _tooltipLabel.Height + 30
            _tooltipLabelContainer.BorderStyle = BorderStyle.FixedSingle
            _tooltipLabelContainer.Enabled = False
            _tooltipLabelContainer.Visible = True

            Dim _parent As Control = Me.DataGridView
            _screenLeft = Me.DataGridView.GetCellDisplayRectangle(Me.ColumnIndex, rowIndex, True).Left
            _screenTop = Me.DataGridView.GetCellDisplayRectangle(Me.ColumnIndex, rowIndex, True).Top
            While _parent.GetContainerControl IsNot _parent
                _screenLeft += _parent.Left
                _screenTop += _parent.Top
                _parent = _parent.Parent
            End While
            _parent.Controls.Add(_tooltipLabelContainer)
            _tooltipLabelContainer.BringToFront()
        End If
        _tooltipLabelContainer.Visible = True
        _tooltipLabel.Visible = True
        MyBase.OnMouseEnter(rowIndex)
    End Sub

    Protected Overrides Sub OnMouseLeave(ByVal rowIndex As Integer)
        _tooltipLabelContainer.Visible = False
        _tooltipLabel.Visible = False
        MyBase.OnMouseLeave(rowIndex)
    End Sub

    Protected Overrides Sub OnMouseMove(ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs)
        MyBase.OnMouseMove(e)
        _tooltipLabelContainer.Left = _screenLeft + e.X
        _tooltipLabelContainer.Top = _screenTop + e.Y
    End Sub

    Protected Overrides Function GetFormattedValue(ByVal value As Object, ByVal rowIndex As Integer, ByRef cellStyle As System.Windows.Forms.DataGridViewCellStyle, ByVal valueTypeConverter As System.ComponentModel.TypeConverter, ByVal formattedValueTypeConverter As System.ComponentModel.TypeConverter, ByVal context As System.Windows.Forms.DataGridViewDataErrorContexts) As Object
        Return Nothing
    End Function

    Protected Overrides Function SetValue(ByVal rowIndex As Integer, ByVal value As Object) As Boolean
        Return MyBase.SetValue(rowIndex, value)
    End Function

    Public Sub New()
        MyBase.New()
    End Sub

    Public Overrides ReadOnly Property ValueType() As Type
        Get
            ' Return the type of the value that CalendarCell contains.
            Return GetType(Trinity.cAdTooxStatus)
        End Get
    End Property

    Public Overrides ReadOnly Property DefaultNewRowValue() As Object
        Get
            ' Use the current date and time as the default value.
            Return New Trinity.cAdTooxStatus
        End Get
    End Property

    Public Overrides ReadOnly Property EditType() As Type
        Get
            ' Return the type of the editing contol that CalendarCell uses.
            Return Nothing
        End Get
    End Property

End Class
