Imports System
Imports System.Globalization
Imports System.Threading
Imports System.Resources
Imports System.Reflection

Public Class ProgressBar

    Private _progress As Integer
    Public Property Progress() As Integer
        Get
            Return _progress
        End Get
        Set(ByVal value As Integer)
            _progress = value
            Dim idx As Integer = (_progress / 100) * 54 + 1
            PictureBox1.Image = My.Resources.ResourceManager.GetObject("rör" & idx)
        End Set
    End Property


End Class
