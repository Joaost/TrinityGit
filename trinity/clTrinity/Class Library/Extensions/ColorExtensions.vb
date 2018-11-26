Imports Microsoft.VisualBasic.Compatibility
Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports System.Drawing.Drawing2D
Imports System.Runtime.CompilerServices

Namespace Trinity
    Module ColorExtensions

        <Extension()> Public Function ToRGB(ByVal Col As Color) As Integer
            Return RGB(Col.R, Col.G, Col.B)
            '        Return Col.ToArgb + &HFFFFFFFE
        End Function

    End Module
End Namespace



