Namespace My

    ' The following events are availble for MyApplication:
    ' 
    ' Startup: Raised when the application starts, before the startup form is created.
    ' Shutdown: Raised after all application forms are closed.  This event is not raised if the application terminates abnormally.
    ' UnhandledException: Raised if the application encounters an unhandled exception.
    ' StartupNextInstance: Raised when launching a single-instance application and the application is already active. 
    ' NetworkAvailabilityChanged: Raised when the network connection is connected or disconnected.
    Partial Friend Class MyApplication

        Private Sub MyApplication_Startup(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.ApplicationServices.StartupEventArgs) Handles Me.Startup
            Dim UpdateMe As New cUpdateMe("Balthazar.exe", "http://instore.mecaccess.se/desktopapp/")

            UpdateMe.GetNewVersion()

            Const MINWAIT = 2
            Dim t As Long = Microsoft.VisualBasic.DateAndTime.Timer

            frmSplashscreen.Show()
            Windows.Forms.Application.DoEvents()

            frmSplashscreen.Status = "Connecting to database..."
            Select Case BalthazarSettings.DatabaseType
                'Case "Access"
                '    Database = New cDBReaderAccess
                Case "SQL"
                    Database = New cDBReaderSQL
            End Select
            Dim Connection As Object = Database.Connect(cDBReader.ConnectionMode.cmNetwork)
            If Connection Is Nothing OrElse Connection.GetType.IsSubclassOf(GetType(Exception)) Then
                NoDatabase(DirectCast(Connection, Exception).Message)
            End If
            If Database.GetType Is GetType(cDBReaderSQL) Then
                frmSplashscreen.Status = "Get Clients && products..."
                DirectCast(Database, cDBReaderSQL).GetProductsAndClients()
            End If
            frmSplashscreen.Status = "Reading stafflists..."
            Database.GetStaffList()
            frmSplashscreen.Status = "Reading bookings..."
            Database.GetAllBookings()
            frmSplashscreen.Status = "Creating objects..."
            MyEvent = New cEvent
            While Microsoft.VisualBasic.DateAndTime.Timer - t < MINWAIT
                Windows.Forms.Application.DoEvents()
            End While
            frmSplashscreen.Hide()
        End Sub

        Private Sub NoDatabase(ByVal Message As String)
            frmSplashscreen.Hide()
            Windows.Forms.MessageBox.Show("Could not connect to database." & vbCrLf & vbCrLf & "Message:" & vbCrLf & Message, "B A L T H A Z A R", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Sub

    End Class

End Namespace

