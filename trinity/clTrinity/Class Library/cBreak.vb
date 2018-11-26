Imports System.Runtime.InteropServices
Imports System
Imports System.Text
Imports System.Collections
Imports Microsoft.VisualBasic

Namespace Trinity
    Public Class cBreak
        Public AirDate As Date
        Public MaM As Integer
        Public Duration As Integer
        Public ProgBefore As String
        Public ProgAfter As String
        Public BreakIdx As Long
        Public ID As String
        Public BreakList As String

        Private mvarChannel As String
        Private Main As cKampanj

        Public Property Channel() As cChannel
            Get
                Return Main.Channels(mvarChannel)
            End Get
            Set(ByVal value As cChannel)
                mvarChannel = value.ChannelName
            End Set
        End Property


        Public Sub New(ByVal MainObject As cKampanj)
            Main = MainObject
        End Sub
    End Class
End Namespace