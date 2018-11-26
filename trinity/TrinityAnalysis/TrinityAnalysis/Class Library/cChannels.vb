Imports System.Runtime.InteropServices
Imports System
Imports System.Text
Imports System.Collections
Imports Microsoft.VisualBasic
Imports System.Xml

Public Class cChannels

    Implements Collections.IEnumerable
    Private mCol As Collection
    Private Main As cAnalysis

    Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
        GetEnumerator = mCol.GetEnumerator
    End Function

    Public Sub New(ByVal MainObject As cAnalysis)
        mCol = New Collection
        Main = MainObject
    End Sub

    'Private mvarChannels As New cChannels(Me) 'a collection of Channels
    Private mvarChannelName As String



    Public Function Add(ByVal Name As String, ByVal filename As String, Optional ByVal Area As String = "") As cChannel

        Dim objNewMember As cChannel
        Dim TmpChannel As New cChannel(Main, mCol)
        Dim i As Integer

        objNewMember = New cChannel(Main, mCol)

        objNewMember.ChannelName = Name

        TmpChannel = Nothing

        TmpChannel = Nothing
        If mCol.Count > 0 And objNewMember.ListNumber > 0 Then
            i = 1
            TmpChannel = mCol(1)
            While TmpChannel.ListNumber < objNewMember.ListNumber And i <= mCol.Count
                TmpChannel = mCol(i)
                i = i + 1
            End While
            If i >= mCol.Count And TmpChannel.ListNumber < objNewMember.ListNumber Then
                TmpChannel = Nothing
            End If
        End If



        If TmpChannel Is Nothing OrElse TmpChannel.ChannelName = "" Then
            mCol.Add(objNewMember, Name)
        ElseIf TmpChannel.ListNumber > objNewMember.ListNumber Then
            mCol.Add(objNewMember, Name, TmpChannel.ChannelName)
        Else
            mCol.Add(objNewMember, Name, , TmpChannel.ChannelName)
        End If

        Add = mCol(Name)

        objNewMember = Nothing

    End Function
    Friend WriteOnly Property MainObject() As cAnalysis
        Set(ByVal value As cAnalysis)
            Dim TmpChannel As cChannel

            Main = value
            For Each TmpChannel In mCol
                TmpChannel.MainObject = value
            Next
        End Set
    End Property

    Protected Overrides Sub Finalize()
        mCol = Nothing
        MyBase.Finalize()
    End Sub
End Class
