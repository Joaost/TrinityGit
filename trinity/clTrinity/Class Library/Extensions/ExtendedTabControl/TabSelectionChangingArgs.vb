Imports Microsoft.VisualBasic.Compatibility
Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports System.Drawing.Drawing2D
Imports System.Runtime.CompilerServices


Public Class TabSelectionChangingArgs
    Inherits CancelEventArgs

    Dim m_intCurrent As Integer
    Dim m_intNew As Integer

    Public ReadOnly Property CurrentTabIndex() As Integer
        Get
            Return m_intCurrent
        End Get
    End Property

    Public ReadOnly Property NewTabIndex() As Integer
        Get
            Return m_intNew
        End Get
    End Property

    Public Sub New(ByVal CurrentTabIndex As Integer, ByVal NewTabIndex As Integer)
        MyBase.New()
        m_intCurrent = CurrentTabIndex
        m_intNew = NewTabIndex
    End Sub
End Class
