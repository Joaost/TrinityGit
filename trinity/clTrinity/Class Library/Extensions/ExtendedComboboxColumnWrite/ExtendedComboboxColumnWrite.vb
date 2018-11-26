Imports Microsoft.VisualBasic.Compatibility
Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports System.Drawing.Drawing2D
Imports System.Runtime.CompilerServices


Public Class ExtendedComboboxColumnWrite
    Inherits DataGridViewColumn

    Public Sub New()
        MyBase.New(New ExtendedComboBoxCellWrite())
    End Sub

    Public Overrides Property CellTemplate() As DataGridViewCell
        Get
            Return MyBase.CellTemplate
        End Get
        Set(ByVal value As DataGridViewCell)

            ' Ensure that the cell used for the template is a ExtendedComboBoxCellWrite.
            If Not (value Is Nothing) AndAlso _
                Not value.GetType().IsAssignableFrom(GetType(ExtendedComboBoxCellWrite)) _
                Then
                Throw New InvalidCastException("Must be an ExtendedComboBoxCellWrite")
            End If
            MyBase.CellTemplate = value

        End Set
    End Property

End Class
