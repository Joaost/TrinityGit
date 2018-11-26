Imports Microsoft.VisualBasic.Compatibility
Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports System.Drawing.Drawing2D
Imports System.Runtime.CompilerServices


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
