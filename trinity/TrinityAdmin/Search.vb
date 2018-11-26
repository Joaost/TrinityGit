Option Strict On
Option Explicit On
Imports System.IO

Public Class FileSearch

    Private Const DefaultFileMask As String = "*.*"
    Private Const DefaultDirectoryMask As String = "*"
#Region " Member Variables "
    Private _InitialDirectory As DirectoryInfo
    Private _DirectoryMask As String
    Private _FileMask As String
    Private _Directories As New ArrayList
    Private _Files As New ArrayList

#End Region

#Region " Properites "
    Public Property InitialDirectory() As DirectoryInfo
        Get
            Return _InitialDirectory
        End Get
        Set(ByVal Value As DirectoryInfo)
            _InitialDirectory = Value
        End Set
    End Property
    Public Property DirectoryMask() As String
        Get
            Return _DirectoryMask
        End Get
        Set(ByVal Value As String)
            _DirectoryMask = Value
        End Set
    End Property
    Public Property FileMask() As String
        Get
            Return _FileMask
        End Get
        Set(ByVal Value As String)
            _FileMask = Value
        End Set
    End Property
    Public ReadOnly Property Directories() As ArrayList

        Get
            Return _Directories
        End Get
    End Property
    Public ReadOnly Property Files() As ArrayList
        Get
            Return _Files
        End Get
    End Property

#End Region

#Region " Constructors "

    Public Sub New()
    End Sub

    Public Sub New(ByVal BaseDirectory As String, _
Optional ByVal FileMask As String = DefaultFileMask, _
Optional ByVal DirectoryMask As String = DefaultDirectoryMask)

        Me.New(New IO.DirectoryInfo(BaseDirectory), FileMask, DirectoryMask)
    End Sub

    Public Sub New( _
    ByVal BaseDirectory As DirectoryInfo, _
    Optional ByVal FileMask As String = DefaultFileMask, _
    Optional ByVal DirectoryMask As String = DefaultDirectoryMask)

        _InitialDirectory = BaseDirectory
        _FileMask = FileMask
        _DirectoryMask = DirectoryMask
    End Sub

#End Region

    Protected Overrides Sub Finalize()
        _Files = Nothing
        _Directories = Nothing
        MyBase.Finalize()
    End Sub
    Public Sub Search( _
    Optional ByVal BaseDirectory As DirectoryInfo = Nothing, _
    Optional ByVal FileMask As String = Nothing, _
    Optional ByVal DirectoryMask As String = Nothing)

        If Not IsNothing(BaseDirectory) Then
            _InitialDirectory = BaseDirectory
        End If
        If IsNothing(_InitialDirectory) Then
            Throw New ArgumentException("A Directory Must be specified!", "Directory")
        End If
        If IsNothing(_FileMask) Then
            _FileMask = DefaultFileMask
        End If
        If IsNothing(DirectoryMask) Then
            _DirectoryMask = DefaultDirectoryMask
        Else
            _DirectoryMask = DirectoryMask
        End If
        DoSearch(_InitialDirectory)
    End Sub

    Private Sub DoSearch(ByVal BaseDirectory As DirectoryInfo)
        Try
            _Files.AddRange(BaseDirectory.GetFiles(_FileMask))
        Catch u As UnauthorizedAccessException
            'Siliently Ignore this error, there isnt any simple                
            'way to avoid this error.             
        End Try
        Try
            Dim Directories() As DirectoryInfo = BaseDirectory.GetDirectories(_DirectoryMask)
            _Directories.AddRange(Directories)
            For Each di As DirectoryInfo In Directories
                DoSearch(di)
            Next
        Catch u As UnauthorizedAccessException
            'Siliently Ignore this error, there isnt any simple                
            'way to avoid this error.            
        End Try
    End Sub
End Class

