Imports System.Globalization
Imports Microsoft.Office.Interop

Namespace CultureSafeExcel
    Public Class Interior

        Private _interior As Excel.Interior
        Private _ci As CultureInfo = New System.Globalization.CultureInfo("en-US")

        Public Sub New(Interior As Excel.Interior)
            _interior = Interior            
        End Sub

        Public Property Color As Integer
            Get
                Try
                    Return _interior.GetType.InvokeMember("Color", Reflection.BindingFlags.GetProperty, Nothing, _interior, Nothing, _ci)
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Get
            Set(value As Integer)
                Try
                    _interior.GetType.InvokeMember("Color", Reflection.BindingFlags.SetProperty, Nothing, _interior, New Object() {value}, _ci)
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Set
        End Property

        Public WriteOnly Property ColorIndex As Integer
            Set(value As Integer)
                Try
                    _interior.GetType.InvokeMember("ColorIndex", Reflection.BindingFlags.SetProperty, Nothing, _interior, New Object() {value}, _ci)
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Set
        End Property

    End Class
End Namespace
