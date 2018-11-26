' Class that apart from the normal EventArgs type also contain a week object
'Author: Björn Fyrvall

Namespace Trinity
    Public Class WeekEventArgs
        Inherits EventArgs

        ' The week that called the event
        Public ReadOnly week As Trinity.cWeek

        Public Sub New(ByVal prmWeek As Trinity.cWeek)
            Me.week = prmWeek
        End Sub
    End Class
End Namespace

