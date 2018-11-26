'Imports System.Runtime.InteropServices
Imports System
Imports System.Text
Imports System.Collections
Imports Microsoft.VisualBasic
Imports System.Windows.Forms

Namespace Trinity
    Public Class cPeriod
        Public Period As String
        Public Adedge As New Global.ConnectWrapper.Breaks
        Public Breaks As ArrayList
        Public BreakCount As Long
    End Class
End Namespace