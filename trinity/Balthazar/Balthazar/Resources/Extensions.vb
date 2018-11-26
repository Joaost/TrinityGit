Imports System.Runtime.CompilerServices

Namespace Extensions
    Module StringExtension
        <Extension()> Public Function EndWith(ByVal MyString As String, ByVal EndString As String) As String
            If MyString.EndsWith(EndString) Then Return MyString
            Return MyString & EndString
        End Function
    End Module
End Namespace