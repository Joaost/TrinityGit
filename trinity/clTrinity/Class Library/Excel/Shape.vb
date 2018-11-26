Imports System.Globalization
Imports Microsoft.Office.Interop

Namespace CultureSafeExcel
    Public Class Shape
        Private _shape As Object
        Private _ci As CultureInfo = New System.Globalization.CultureInfo("en-US")

        Public Sub New(Shape As Object)
            _shape = Shape
        End Sub

        Public Property Width As Single
            Get
                Try
                    Return _shape.GetType.InvokeMember("Width", Reflection.BindingFlags.GetProperty, Nothing, _shape, Nothing, _ci)
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Get
            Set(value As Single)
                Try
                    _shape.GetType.InvokeMember("Width", Reflection.BindingFlags.SetProperty, Nothing, _shape, New Object() {value}, _ci)
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Set
        End Property

        Public Property Height As Single
            Get
                Try
                    Return _shape.GetType.InvokeMember("Height", Reflection.BindingFlags.GetProperty, Nothing, _shape, Nothing, _ci)
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Get
            Set(value As Single)
                Try
                    _shape.GetType.InvokeMember("Height", Reflection.BindingFlags.SetProperty, Nothing, _shape, New Object() {value}, _ci)
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Set
        End Property

        Public Sub ScaleWidth(Factor As Single, RelativeToOriginalSize As Object, Scale As Object)
            Try
                Dim _sr As Object = _shape.GetType.InvokeMember("ShapeRange", Reflection.BindingFlags.GetProperty, Nothing, _shape, Nothing, _ci)
                _sr.GetType.InvokeMember("ScaleWidth", Reflection.BindingFlags.InvokeMethod, Nothing, _sr, New Object() {Factor, RelativeToOriginalSize, Scale}, _ci)
            Catch ex As Reflection.TargetInvocationException
                Throw ex.InnerException
            Catch ex As Exception
                Throw ex
            End Try
        End Sub


        Public Sub ScaleHeight(Factor As Single, RelativeToOriginalSize As Object, Scale As Object)
            Try
                Dim _sr As Object = _shape.GetType.InvokeMember("ShapeRange", Reflection.BindingFlags.GetProperty, Nothing, _shape, Nothing, _ci)
                _sr.GetType.InvokeMember("ScaleHeight", Reflection.BindingFlags.InvokeMethod, Nothing, _sr, New Object() {Factor, RelativeToOriginalSize, Scale}, _ci)
            Catch ex As Reflection.TargetInvocationException
                Throw ex.InnerException
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Property Top As Single
            Get
                Try
                    Return _shape.GetType.InvokeMember("Top", Reflection.BindingFlags.GetProperty, Nothing, _shape, Nothing, _ci)
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Get
            Set(value As Single)
                Try
                    _shape.GetType.InvokeMember("Top", Reflection.BindingFlags.SetProperty, Nothing, _shape, New Object() {value}, _ci)
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Set
        End Property

        Public Property Left As Single
            Get
                Try
                    Return _shape.GetType.InvokeMember("Left", Reflection.BindingFlags.GetProperty, Nothing, _shape, Nothing, _ci)
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Get
            Set(value As Single)
                Try
                    _shape.GetType.InvokeMember("Left", Reflection.BindingFlags.SetProperty, Nothing, _shape, New Object() {value}, _ci)
                Catch ex As Reflection.TargetInvocationException
                    Throw ex.InnerException
                Catch ex As Exception
                    Throw ex
                End Try
            End Set
        End Property
    End Class
End Namespace