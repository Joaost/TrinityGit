Imports System.Xml

Public Class cChannel

    Private mvarID As Long
    Private mvarDatabaseID As String = "NOT SAVED"
    Private mvarChannelSet As Integer = 0
    Private mvarChannelName As String
    Private mvarAdEdgeNames As String
    Private mvarDefaultArea As String
    Private mvarArea As String
    Private mvarFile As String
    Private mvarListNumber As Integer
    Private mvarMatrixName As String
    Private mvarMarathonName As String
    Public LogoPath As String

    Private ParentColl As Collection
    Private Main As cAnalysis
    Private _mCol As Collection


    Public Event ChannelChanged(Channel As cChannel)

    Public Sub New(ByVal Main As cAnalysis, ParentCollection As Collection)

    End Sub

    Public Property fileName As String
        Get
            Return mvarFile
        End Get
        Set(value As String)
            mvarFile = value
            RaiseEvent ChannelChanged(Me)
        End Set
    End Property

    Public Property ChannelName() As String
        Get
            Return mvarChannelName
        End Get

        Set(ByVal value As String)
            Dim TmpChannel As cChannel

            If value <> mvarChannelName And mvarChannelName <> "" Then
                TmpChannel = ParentColl(mvarChannelName)
                ParentColl.Add(TmpChannel, value)
                ParentColl.Remove(mvarChannelName)
            End If

            mvarChannelName = value

            RaiseEvent ChannelChanged(Me)


        End Set
    End Property

    Public Property Area() As String
        Get
            Area = mvarArea
        End Get
        Set(value As String)
            mvarArea = value
            RaiseEvent ChannelChanged(Me)
        End Set
    End Property


    Public Property ListNumber() As Integer
        Get
            ListNumber = mvarListNumber
        End Get
        Set(value As Integer)

            mvarListNumber = value

            RaiseEvent ChannelChanged(Me)

        End Set
    End Property

    Friend WriteOnly Property MainObject() As cAnalysis
        Set(ByVal value As cAnalysis)
            Main = value
            RaiseEvent ChannelChanged(Me)
        End Set

    End Property
End Class

