Imports System.Runtime.CompilerServices
Imports System.Windows.Forms

Module DataGridViewExtension
    Private Declare Auto Function SendMessage Lib "user32.dll" ( _
        ByVal hWnd As IntPtr, _
        ByVal uMsg As Int32, _
        ByVal lParam As Int32, _
        ByVal lpData As Int32 _
    ) As IntPtr


    Private Const WM_SETREDRAW As Integer = &HB
    Private Const [FALSE] As Integer = 0
    Private Const [TRUE] As Integer = 1

    <Extension()> Public Sub TurnOffAutoRedraw(ByVal Grid As DataGridView)
        SendMessage(Grid.Handle, WM_SETREDRAW, [FALSE], 0)
    End Sub

    <Extension()> Public Sub TurnOnAutoRedraw(ByVal Grid As DataGridView)
        SendMessage(Grid.Handle, WM_SETREDRAW, [TRUE], 0)
    End Sub

End Module
