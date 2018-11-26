Imports System.Globalization
Imports Microsoft.Office.Interop

Namespace CultureSafeExcel
    Public Class PageSetup

        Private _pageSetup As Excel.PageSetup
        Private _ci As CultureInfo = New System.Globalization.CultureInfo("en-US")

        Public Sub New(PageSetup As Excel.PageSetup)
            _pageSetup = PageSetup
        End Sub

        Public WriteOnly Property Orientation As Excel.XlPageOrientation
            Set(value As Excel.XlPageOrientation)
                Try
                    _pageSetup.GetType.InvokeMember("Orientation", Reflection.BindingFlags.SetProperty, Nothing, _pageSetup, New Object() {value}, _ci)
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Set
        End Property

        Public WriteOnly Property PrintArea As String
            Set(value As String)
                Try
                    _pageSetup.GetType.InvokeMember("PrintArea", Reflection.BindingFlags.SetProperty, Nothing, _pageSetup, New Object() {value}, _ci)
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Set
        End Property

        Public WriteOnly Property PrintTitleRows As String
            Set(value As String)
                Try
                    _pageSetup.GetType.InvokeMember("PrintTitleRows", Reflection.BindingFlags.SetProperty, Nothing, _pageSetup, New Object() {value}, _ci)
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Set
        End Property

        Public WriteOnly Property PrintTitleColumns As String
            Set(value As String)
                Try
                    _pageSetup.GetType.InvokeMember("PrintTitleColumns", Reflection.BindingFlags.SetProperty, Nothing, _pageSetup, New Object() {value}, _ci)
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Set
        End Property

        Public WriteOnly Property Zoom As Integer
            Set(value As Integer)
                Try
                    _pageSetup.GetType.InvokeMember("Zoom", Reflection.BindingFlags.SetProperty, Nothing, _pageSetup, New Object() {value}, _ci)
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Set
        End Property

        Public WriteOnly Property TopMargin As Single
            Set(value As Single)
                Try
                    _pageSetup.GetType.InvokeMember("TopMargin", Reflection.BindingFlags.SetProperty, Nothing, _pageSetup, New Object() {value}, _ci)
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Set
        End Property

        Public WriteOnly Property RightMargin As Single
            Set(value As Single)
                Try
                    _pageSetup.GetType.InvokeMember("RightMargin", Reflection.BindingFlags.SetProperty, Nothing, _pageSetup, New Object() {value}, _ci)
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Set
        End Property

        Public WriteOnly Property LeftMargin As Single
            Set(value As Single)
                Try
                    _pageSetup.GetType.InvokeMember("LeftMargin", Reflection.BindingFlags.SetProperty, Nothing, _pageSetup, New Object() {value}, _ci)
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Set
        End Property

        Public WriteOnly Property BottomMargin As Single
            Set(value As Single)
                Try
                    _pageSetup.GetType.InvokeMember("BottomMargin", Reflection.BindingFlags.SetProperty, Nothing, _pageSetup, New Object() {value}, _ci)
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Set
        End Property

        Public WriteOnly Property HeaderMargin As Single
            Set(value As Single)
                Try
                    _pageSetup.GetType.InvokeMember("HeaderMargin", Reflection.BindingFlags.SetProperty, Nothing, _pageSetup, New Object() {value}, _ci)
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Set
        End Property

        Public WriteOnly Property FooterMargin As Single
            Set(value As Single)
                Try
                    _pageSetup.GetType.InvokeMember("FooterMargin", Reflection.BindingFlags.SetProperty, Nothing, _pageSetup, New Object() {value}, _ci)
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Set
        End Property

        Public WriteOnly Property PrintHeadings As Boolean
            Set(value As Boolean)
                Try
                    _pageSetup.GetType.InvokeMember("PrintHeadings", Reflection.BindingFlags.SetProperty, Nothing, _pageSetup, New Object() {value}, _ci)
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Set
        End Property

        Public WriteOnly Property PrintGridlines As Boolean
            Set(value As Boolean)
                Try
                    _pageSetup.GetType.InvokeMember("PrintGridlines", Reflection.BindingFlags.SetProperty, Nothing, _pageSetup, New Object() {value}, _ci)
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Set
        End Property
    End Class
End Namespace
