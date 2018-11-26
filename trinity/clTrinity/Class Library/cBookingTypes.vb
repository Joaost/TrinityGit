Imports System.Runtime.InteropServices
Imports System
Imports System.Text
Imports System.Collections
Imports Microsoft.VisualBasic

Namespace Trinity
    Public Class cBookingTypes
        Implements Collections.IEnumerable

        'local variable to hold collection
        Private mCol As Collection
        Private Main As cKampanj
        Private mvarParentChannel As cChannel


        Public Event TRPChanged(ByVal sender As Object, ByVal e As WeekEventArgs)
        Private Sub _trpChanged(ByVal sender As Object, ByVal e As WeekEventArgs)
            RaiseEvent TRPChanged(sender, e)
        End Sub

        Public Event FilmChanged(Film As cFilm)
        Public Event WeekChanged(Week As cWeek)
        Public Event BookingtypeChanged(Bookingtype As cBookingType)

        Private Sub _filmChanged(Film As cFilm)
            RaiseEvent FilmChanged(Film)
        End Sub

        Private Sub _weekChanged(Week As cWeek)
            RaiseEvent WeekChanged(Week)
        End Sub

        Private Sub _bookingtypeChanged(Bookingtype As cBookingType)
            RaiseEvent BookingtypeChanged(Bookingtype)
        End Sub

        Public Function GetXML(ByRef colXml As Xml.XmlElement, ByRef errorMessege As List(Of String), ByVal xmlDoc As Xml.XmlDocument) As Boolean
            'this function saves the collection to the xml provided
            'it will return True of succeded and false if failed

            On Error GoTo On_Error

            Dim XMLBookingType As Xml.XmlElement

            For Each TmpBookingType As Trinity.cBookingType In Me
                XMLBookingType = xmlDoc.CreateElement("BookingType")
                colXml.AppendChild(XMLBookingType)
            Next TmpBookingType

            Exit Function

On_Error:
            colXml.AppendChild(xmlDoc.CreateComment("ERROR (" & Err.Number & "): " & Err.Description))
            errorMessege.Add("Error saving the bookingtype collection")
            Resume Next
        End Function

        Friend Property ParentChannel() As cChannel
            '---------------------------------------------------------------------------------------
            ' Procedure : ParentChannel
            ' DateTime  : 2003-08-15 16:18
            ' Author    : joho
            ' Purpose   :
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo ParentChannel_Error

                ParentChannel = mvarParentChannel

                On Error GoTo 0
                Exit Property

ParentChannel_Error:

                Err.Raise(Err.Number, "cBookingType: ParentChannel", Err.Description)
            End Get
            Set(ByVal value As cChannel)
                On Error GoTo ParentChannel_Error

                mvarParentChannel = value
                Dim TmpBookingType As cBookingType

                For Each TmpBookingType In mCol
                    If TmpBookingType.ParentChannel Is Nothing Then
                        TmpBookingType.ParentChannel = value
                    End If
                Next

                On Error GoTo 0
                Exit Property

ParentChannel_Error:

                Err.Raise(Err.Number, "cBookingType: ParentChannel", Err.Description)


            End Set
        End Property

        Friend Property MainObject()
            Get
                MainObject = Main
            End Get
            Set(ByVal value)
                Dim TmpBookingType As cBookingType

                Main = value
                For Each TmpBookingType In mCol
                    TmpBookingType.MainObject = value
                Next
            End Set
        End Property

        'Default Public ReadOnly Property Item(ByVal ID As String, ByVal b As Boolean) As cChannel
        '    'used when referencing an element in the Collection
        '    'vntIndexKey contains either the Index or Key to the Collection,
        '    'this is why it is declared as a Variant
        '    'Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
        '    Get
        '        Try
        '            For i As Integer = 1 To mCol.Count
        '                If mCol.Item(i).DBID = ID Then
        '                    Return mCol.Item(i)
        '                    Exit Property
        '                End If
        '            Next
        '            Return Nothing
        '        Catch
        '            Return Nothing
        '        End Try
        '    End Get
        'End Property

        Public Overloads Function Add(ByVal Name As String, Optional ByVal ReadPricelist As Boolean = False, Optional ByVal ID As String = "") As cBookingType

            'create a new object
            Dim objNewMember As cBookingType

            ' On Error GoTo Add_Error

            objNewMember = New cBookingType(MainObject)

            'set the properties passed into the method
            'Dim Msg As String
            Helper.WriteToLogFile("cBookingTypes.Add : SetObjects")

            objNewMember.MainObject = Main
            objNewMember.ParentCollection = mCol
            objNewMember.ParentChannel = mvarParentChannel
            objNewMember.ParentChannel = mvarParentChannel

            ' Add handlers to the changed trp event
            AddHandler objNewMember.TRPChanged, AddressOf _trpChanged
            AddHandler objNewMember.BookingtypeChanged, AddressOf _bookingtypeChanged
            AddHandler objNewMember.WeekChanged, AddressOf _weekChanged
            AddHandler objNewMember.FilmChanged, AddressOf _filmChanged

            objNewMember.Name = Name

            If ReadPricelist Then
                Helper.WriteToLogFile("cBookingTypes.Add : ReadPricelist")
                objNewMember.ReadPricelist()
            End If

            Helper.WriteToLogFile("cBookingTypes.Add : Add To Collection")
            Try
                mCol.Add(objNewMember, Name)
            Catch ex As Exception
                Helper.WriteToLogFile("cBookingTypes.Add : Addition failed, already exists in collection of booking types")
            End Try


            'return the object created
            Add = objNewMember
            objNewMember = Nothing


            ' On Error GoTo 0
            Exit Function

Add_Error:

            If Err.Number = 457 Then
                Add = mCol(Name)
                Exit Function
            End If
            Helper.WriteToLogFile("ERROR: " & Err.Description)
            ' Err.Raise(Err.Number, "cBookingTypes: Add", Err.Description)


        End Function

        Public Function Contains(ByVal vntIndexKey As Object) As Boolean
            Return mCol.Contains(vntIndexKey)
        End Function

        Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As cBookingType

            'used when referencing an element in the collection
            'vntIndexKey contains either the Index or Key to the collection,
            'this is why it is declared as a Variant
            'Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
            Get
                On Error GoTo ErrHandle
                If vntIndexKey.GetType.Name = "String" AndAlso Not mCol.Contains(vntIndexKey) Then
                    Return Nothing
                End If
                Item = mCol(vntIndexKey)
                Exit Property

ErrHandle:
                If vntIndexKey = 1 And mCol.Count > 0 Then
                    Item = mCol(vntIndexKey)
                End If
                Err.Raise(Err.Number, "cBookingTypes", "Unknown Bookingtype: " & vntIndexKey)
            End Get
        End Property

        Public Overloads ReadOnly Property Count() As Long
            Get
                'used when retrieving the number of elements in the
                'collection. Syntax: Debug.Print x.Count
                Count = mCol.Count
            End Get
        End Property

        Public Overloads Sub Remove(ByVal vntIndexKey As Object)
            'used when removing an element from the collection
            'vntIndexKey contains either the Index or Key, which is why
            'it is declared as a Variant
            'Syntax: x.Remove(xyz)
            If DirectCast(mCol(vntIndexKey), cBookingType).IsUserEditable Then
                mCol.Remove(vntIndexKey)
            Else
                Windows.Forms.MessageBox.Show("This booking type cannot be removed - it is standard.")
            End If

        End Sub

        Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            GetEnumerator = mCol.GetEnumerator()
        End Function

        Public Sub New(ByVal Main As cKampanj)
            mCol = New Collection
            MainObject = Main
        End Sub

        Protected Overrides Sub Finalize()
            mCol = Nothing
            MyBase.Finalize()
        End Sub

    End Class
End Namespace