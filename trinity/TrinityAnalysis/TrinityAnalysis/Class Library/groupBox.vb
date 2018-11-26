Public Class groupBox

    Dim newSize As New Size
    Dim _values As New cSearchValues

    Private _newLocation As Drawing.Point

    Public Property newLocation() As Drawing.Point
        Get
            Return _newLocation
        End Get
        Set(ByVal value As Drawing.Point)
            _newLocation = _newLocation
        End Set
    End Property

    Private Sub cmdAddValue_Click(sender As System.Object, e As System.EventArgs) Handles cmdAddValue.Click

        Dim _UC As New groupBox

        Dim userControl As Windows.Forms.UserControl = New Windows.Forms.UserControl
        Dim usercontrolList As New List(Of Object)
        Dim txtBoxList As List(Of Object)
        Dim location As New Drawing.Point

        For Each obj As Object In frmSetup.Controls
            If (TypeOf obj Is UserControl) Then
                usercontrolList.Add(obj)
            End If
        Next

        For Each userControl In usercontrolList
            _UC.Location = userControl.Location
            _UC.Location = _UC.Location + New Drawing.Point(0, 70)
        Next

        Controls.Add(_UC)
        frmSetup.Controls.Add(_UC)
    End Sub

    Private Sub cmdDeleteValue_Click(sender As System.Object, e As System.EventArgs) Handles cmdDeleteValue.Click

        Dim userControl As Windows.Forms.UserControl = New Windows.Forms.UserControl
        Dim usercontrolList As New List(Of Object)


        For Each obj As Object In frmSetup.Controls
            If (TypeOf obj Is UserControl) Then
                usercontrolList.Add(obj)
            End If
        Next

        If usercontrolList.Count > 1 Then
            frmSetup.Controls.Remove(Me)

            usercontrolList.Clear()

            For Each obj As Object In frmSetup.Controls
                If (TypeOf obj Is UserControl) Then
                    usercontrolList.Add(obj)
                End If
            Next
            For Each tmpGp As groupBox In usercontrolList
                tmpGp.Location = tmpGp.Location - New Drawing.Point(0, -70)
            Next

        End If


    End Sub

    Private Sub PopulateValues()

        cmbValueList.Items.Clear()

        For Each p As System.Reflection.PropertyInfo In _values.GetType().GetProperties()
            cmbValueList.Items.Add(p.Name)
        Next
    End Sub

    Public Sub New()

        ' This call is required by the designer.

        InitializeComponent()

        PopulateValues()
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub cmbValueList_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbValueList.SelectedIndexChanged

        If cmbValueList.SelectedItem = "channel" Then

            Dim dt1 As New DateTimePicker
            Dim dt2 As New DateTimePicker


            dt1.Location = New Drawing.Point(169, 35)
            dt1.Size = New Drawing.Size(171, 20)

            dt2.Location = New Drawing.Point(372, 35)
            dt2.Size = New Drawing.Size(171, 20)

            lblFrom.Text = "From date"
            lblTo.Text = "To date"

        End If

    End Sub
End Class
