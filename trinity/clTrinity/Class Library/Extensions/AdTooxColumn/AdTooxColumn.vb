Imports Microsoft.VisualBasic.Compatibility
Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports System.Drawing.Drawing2D
Imports System.Runtime.CompilerServices


Public Class AdTooxColumn
    Inherits DataGridViewColumn

    Public Sub New()
        MyBase.New(New AdTooxCell)
    End Sub

    Public Overrides Property CellTemplate() As System.Windows.Forms.DataGridViewCell
        Get
            Return MyBase.CellTemplate
        End Get
        Set(ByVal value As System.Windows.Forms.DataGridViewCell)
            If Not (value Is Nothing) AndAlso Not value.GetType().IsAssignableFrom(GetType(AdTooxCell)) Then
                Throw New InvalidCastException("Must be an AdTooxCell")
            End If
            MyBase.CellTemplate = value
        End Set
    End Property
End Class
