Imports System.Runtime.InteropServices
Imports System
Imports System.Text
Imports System.Collections
Imports Microsoft.VisualBasic

Namespace Trinity
    Public Class cFilms
        Implements Collections.IEnumerable
        Implements IDetectsProblems

        'local variable to hold collection
        Private mCol As Collection
        Private Main As cKampanj
        Private mvarBookingtype As cBookingType
        Private mvarWeek As cWeek

        Public Event FilmChanged(Film As cFilm)

        Friend ReadOnly Property Week() As Trinity.cWeek
            Get
                Return mvarWeek
            End Get
        End Property

        Friend WriteOnly Property Bookingtype() As cBookingType
            Set(ByVal value As cBookingType)
                Dim TmpFilm As cFilm

                mvarBookingtype = value
                For Each TmpFilm In mCol
                    TmpFilm.Bookingtype = value
                Next
            End Set

        End Property

        Friend WriteOnly Property MainObject() As cKampanj

            Set(ByVal value As cKampanj)
                Dim TmpFilm As cFilm

                Main = value
                For Each TmpFilm In mCol
                    TmpFilm.MainObject = value
                Next
            End Set

        End Property

        Public Function GetXML(ByRef colXml As Xml.XmlElement, ByRef errorMessege As List(Of String), ByVal xmlDoc As Xml.XmlDocument) As Boolean
            'this function saves the collection to the xml provided
            'it will return True of succeded and false if failed

            On Error GoTo On_Error

            Dim XMLFilm As Xml.XmlElement
            For Each TmpFilm As Trinity.cFilm In Me

                XMLFilm = xmlDoc.CreateElement("Film")
                XMLFilm.SetAttribute("Name", TmpFilm.Name)
                XMLFilm.SetAttribute("Filmcode", TmpFilm.Filmcode) ' as String
                XMLFilm.SetAttribute("FilmLength", TmpFilm.FilmLength) ' as Byte
                XMLFilm.SetAttribute("Index", TmpFilm.Index) ' as Single
                XMLFilm.SetAttribute("GrossIndex", TmpFilm.GrossIndex)
                XMLFilm.SetAttribute("Share", TmpFilm.Share) ' as Integer
                XMLFilm.SetAttribute("Description", TmpFilm.Description) ' as String
                colXml.AppendChild(XMLFilm)
            Next TmpFilm

            Return True
            Exit Function

On_Error:
            errorMessege.Add("Error saving Films for week " & Me.Week.Name)
            Return False
        End Function


        Public Function Add(ByVal Name As String) As cFilm
            'create a new object
            Dim objNewMember As cFilm
            Dim e As Long

            objNewMember = New cFilm(Main, mvarWeek)


            'set the properties passed into the method
            objNewMember.Name = Trim(Name)
            objNewMember.ParentCollection = mCol
            objNewMember.Bookingtype = mvarBookingtype
            AddHandler objNewMember.FilmChanged, AddressOf _filmChanged

            If Not mCol.Contains(Trim(Name)) Then
                mCol.Add(objNewMember, Trim(Name))
                Return objNewMember
            Else
                Return mCol(Trim(Name))
            End If
        End Function

        Private Sub _filmChanged(Film As cFilm)
            RaiseEvent FilmChanged(Film)
        End Sub

        Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As cFilm
            Get
                Dim TmpFilm As cFilm
                Dim e As Long

                '    used when referencing an element in the collection
                '    vntIndexKey contains either the Index or Key to the collection,
                '    this is why it is declared as a Variant
                '    Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
                On Error GoTo Item_Error
                If vntIndexKey Is Nothing Then
                    vntIndexKey = ""
                End If
                If vntIndexKey.GetType.Name = "String" Then
                    If mCol.Contains(vntIndexKey) Then
                        Item = mCol(vntIndexKey)
                    Else
                        Item = Nothing
                        For Each TmpFilm In mCol
                            If InStr("," & UCase(TmpFilm.Filmcode).Replace(" ", "") & ",", "," & UCase(vntIndexKey).ToString.Replace(" ", "") & ",") > 0 And ((vntIndexKey <> "" And TmpFilm.Filmcode <> "") Or (vntIndexKey = "" And TmpFilm.Filmcode = "")) Then
                                Item = TmpFilm
                                Exit Property
                            End If
                        Next
                    End If
                Else
                    Return mCol(vntIndexKey)
                End If

                On Error GoTo 0
                Exit Property

Item_Error:
                e = Err.Number

                If Err.Number = 5 Then
                    For Each TmpFilm In mCol
                        If InStr("," & UCase(TmpFilm.Filmcode) & ",", "," & UCase(vntIndexKey) & ",") > 0 And vntIndexKey <> "" Then
                            Item = TmpFilm
                            Exit Property
                        End If
                    Next
                End If
                Err.Raise(e, "cFilms", "Unknown Film (" & vntIndexKey & ")")
            End Get
        End Property

        Public ReadOnly Property Count() As Long
            Get
                'used when retrieving the number of elements in the
                'collection. Syntax: Debug.Print x.Count
                Count = mCol.Count
            End Get
        End Property

        Public Sub Remove(ByVal vntIndexKey As Object)
            'used when removing an element from the collection
            'vntIndexKey contains either the Index or Key, which is why
            'it is declared as a Variant
            'Syntax: x.Remove(xyz)


            mCol.Remove(vntIndexKey)
        End Sub

        Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            GetEnumerator = mCol.GetEnumerator
        End Function

        Public Sub New(ByVal MainObject As cKampanj, ByVal Week As Trinity.cWeek)
            Main = MainObject
            mvarWeek = Week
            mCol = New Collection
            Main.RegisterProblemDetection(Me)
        End Sub

        Protected Overrides Sub Finalize()
            mCol = Nothing
            MyBase.Finalize()
        End Sub


        Public Enum ProblemsEnum
            DoesNotSumTo100 = 1
            ValueTwo = 2
        End Enum

        Public Function DetectProblems() As System.Collections.Generic.List(Of cProblem) Implements IDetectsProblems.DetectProblems

            Dim _problems As New List(Of cProblem)

            Dim _helpText As New Text.StringBuilder

            Dim _sum As Single = 0
            For Each _film As cFilm In mCol
                _sum += _film.Share
            Next
            If mvarBookingtype.BookIt AndAlso Math.Round(_sum) <> 100 Then
                _helpText.AppendLine("<p style='font-weight: bold; font-size: 14px'>Weekly film share does not sum to 100</p>")
                _helpText.AppendLine("<p>The film share on week '" & Week.Name & "' in '" & mvarBookingtype.ToString & "' does not sum to 100</p>")
                _helpText.AppendLine("<p style='font-weight: bold'>Solution:</p>")
                _helpText.AppendLine("<p>Open the 'Allocate'-window, in the top most pane to the right select '" & mvarBookingtype.ToString & "' and review the film split on week '" & Week.Name & "'.</p>")
                Dim _problem As New cProblem(ProblemsEnum.DoesNotSumTo100, cProblem.ProblemSeverityEnum.Warning, "Weekly film share does not sum to 100", mvarBookingtype.Name & " week " & Week.Name, _helpText.ToString, Me)
                _problems.Add(_problem)
            End If

            If _problems.Count > 0 Then RaiseEvent ProblemsFound(_problems)
            Return _problems
        End Function

        Public Event ProblemsFound(ByVal problems As System.Collections.Generic.List(Of cProblem)) Implements IDetectsProblems.ProblemsFound




    End Class
End Namespace