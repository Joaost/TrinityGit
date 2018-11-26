Imports System.Runtime.InteropServices
Imports System
Imports System.Text
Imports System.Collections
Imports Microsoft.VisualBasic

Namespace Trinity
    Public Class cWeeks
        Implements Collections.IEnumerable
        'weeks is a container/collection of several week
        'the class week represents several days, not nessesary a "real" week

        'local variable to hold collection
        Private mCol As Collection
        Private Main As cKampanj
        Private mvarBookingtype As cBookingType

        'Event handler and event to raise then the TRP has been changed
        Public Event TRPChanged(ByVal sender As Object, ByVal e As WeekEventArgs)
        Private Sub _TRPChanged(ByVal sender As Object, ByVal e As WeekEventArgs)
            RaiseEvent TRPChanged(sender, e)
        End Sub

        Public Event FilmChanged(Film As cFilm)
        Public Event WeekChanged(Week As cWeek)

        Private Sub _filmChanged(Film As cFilm)
            RaiseEvent FilmChanged(Film)
        End Sub

        Private Sub _weekChanged(Week As cWeek)
            RaiseEvent WeekChanged(Week)
        End Sub

        Public Function GetXML(ByRef colXml As Xml.XmlElement, ByRef errorMessege As List(Of String), ByVal xmlDoc As Xml.XmlDocument) As Boolean
            'this function saves the collection to the xml provided
            'it will return True of succeded and false if failed

            On Error GoTo On_Error

            Dim XMLWeek As Xml.XmlElement
            Dim w As Integer = 0

            For Each TmpWeek As Trinity.cWeek In Me
                w += 1
                XMLWeek = xmlDoc.CreateElement("Week") 'as String
                TmpWeek.GetXML(XMLWeek, errorMessege, xmlDoc, w)

                'add it to the parent list
                colXml.AppendChild(XMLWeek)
            Next TmpWeek

            Return True
            Exit Function

On_Error:
            errorMessege.Add("Error saving weeks.")
            Return False
        End Function

        Friend WriteOnly Property Bookingtype()
            Set(ByVal value)
                Dim TmpWeek As cWeek

                mvarBookingtype = value
                For Each TmpWeek In mCol
                    TmpWeek.Bookingtype = value
                Next
            End Set

        End Property

        Friend WriteOnly Property MainObject()
            Set(ByVal value)
                Dim TmpWeek As cWeek

                Main = value
                For Each TmpWeek In mCol
                    TmpWeek.MainObject = value
                Next
            End Set
        End Property

        Public Function Add(ByVal Name As String) As cWeek
            'create a new object
            Dim objNewMember As cWeek
            Dim Ini As New clsIni

            objNewMember = New cWeek(Main)


            'set the properties passed into the method

            objNewMember.ParentCollection = mCol
            objNewMember.Name = Trim(Name)
            objNewMember.Bookingtype = mvarBookingtype
            objNewMember.MainObject = Main

            AddHandler objNewMember.WeekChanged, AddressOf _weekChanged
            AddHandler objNewMember.FilmChanged, AddressOf _filmChanged

            ' Add a handler for when the TRPChanged event is fired
            AddHandler objNewMember.TRPChanged, AddressOf _TRPChanged

            If Not mCol.Contains(Trim(Name)) Then
                mCol.Add(objNewMember, Trim(Name))
            End If

            'return the object created
            Add = mCol(Trim(Name))
            objNewMember = Nothing
            Ini = Nothing

        End Function

        Default Public Property Item(ByVal Key As String) As cWeek
            'used when referencing an element in the collection
            'vntIndexKey contains either the Index or Key to the collection,
            'this is why it is declared as a Variant
            'Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
            Get
                On Error GoTo ErrHandle

                If Not mCol.Contains(Key) Then
                    Return Nothing
                End If
                Return mCol(Key)
                Exit Property

ErrHandle:
                Err.Raise(Err.Number, "cWeeks", "Unknown week: " & Key)
            End Get
            Set(ByVal value As cWeek)
                mCol.Remove(Key)
                mCol.Add(value, Key)
            End Set
        End Property

        Default Public ReadOnly Property Item(ByVal Index As Integer) As cWeek
            Get
                If Index <= mCol.Count Then
                    Return mCol(Index)
                End If
            End Get
        End Property

        Public ReadOnly Property Count() As Long
            'used when retrieving the number of elements in the
            'collection. Syntax: Debug.Print x.Count
            Get
                Count = mCol.Count
            End Get
        End Property

        Public Sub Remove(ByVal vntIndexKey As Object)
            'used when removing an element from the collection
            'vntIndexKey contains either the Index or Key, which is why
            'it is declared as a Variant
            'Syntax: x.Remove(xyz)

            If mCol.Contains(vntIndexKey) Then
                mCol.Remove(vntIndexKey)
            End If

        End Sub

        Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            GetEnumerator = mCol.GetEnumerator
        End Function

        Public Sub New(ByVal MainObject As cKampanj)
            mCol = New Collection
            Main = MainObject
        End Sub

        ' Checks if the collections contains a specific key, with the possiblity to exclude a week in the search
        Public Function Contains(ByVal key As String, Optional ByVal excludeWeek As cWeek = Nothing) As Boolean

            If (Me.mCol.Contains(key)) Then

                ' Get the object
                Dim tmpWeek As cWeek = Me.mCol(key)

                If (tmpWeek Is excludeWeek) Then
                    Return False
                Else
                    Return True
                End If
            Else
                Return False
            End If

        End Function

        Protected Overrides Sub Finalize()
            mCol = Nothing
            MyBase.Finalize()
        End Sub
    End Class
End Namespace