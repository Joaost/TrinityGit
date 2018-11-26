Public Class frmOptimize

    Private Class cIndividual

        Private _fitness As Single
        Private _staffCount As Integer
        Private _roleCount As Single
        Private _eventRoleCount As New Dictionary(Of Integer, List(Of cRole))

        Public Tag As Object

        Public Schedule As cStaffSchedule

        Private _pop As cPopulation

        Sub CalculateVariables()
            Dim StaffList As New Dictionary(Of Integer, cStaff)
            Dim MaxRoles As Integer = 1
            _eventRoleCount.Clear()
            For Each TmpLoc As cStaffScheduleLocation In Schedule.Locations
                For Each TmpRole As cStaffScheduleRole In TmpLoc.Roles
                    For Each TmpDay As cStaffScheduleDay In TmpRole.Days
                        For Each TmpShift As cStaffScheduleShift In TmpDay.Shifts
                            For Each TmpStaff As cStaff In TmpShift.AssignedStaff
                                If _pop.FactorFewIndividuals > 0 Then
                                    If Not StaffList.ContainsKey(TmpStaff.DatabaseID) Then
                                        StaffList.Add(TmpStaff.DatabaseID, TmpStaff)
                                    End If
                                End If
                                If _pop.FactorEventRoleCount > 0 Then
                                    Dim TmpRoleList As List(Of cRole)
                                    If _eventRoleCount.ContainsKey(TmpStaff.DatabaseID) Then
                                        TmpRoleList = _eventRoleCount(TmpStaff.DatabaseID)
                                        If Not TmpRoleList.Contains(TmpRole.Role) Then
                                            TmpRoleList.Add(TmpRole.Role)
                                        End If
                                    Else
                                        TmpRoleList = New List(Of cRole)
                                        TmpRoleList.Add(TmpRole.Role)
                                        _eventRoleCount.Add(TmpStaff.DatabaseID, TmpRoleList)
                                    End If
                                End If
                            Next
                        Next
                    Next
                Next
            Next
            _staffCount = StaffList.Count
            If _pop.MinIndividuals > _staffCount OrElse _pop.MinIndividuals = 0 Then
                _pop.MinIndividuals = _staffCount
            End If
            If _pop.MaxIndividuals < _staffCount Then
                _pop.MaxIndividuals = _staffCount
            End If
            _roleCount = 0
            For Each kv As KeyValuePair(Of Integer, List(Of cRole)) In _eventRoleCount
                _roleCount += kv.Value.Count
            Next
            _roleCount /= _staffCount
            If _pop.MinStaffRoles > _roleCount OrElse _pop.MinStaffRoles = 0 Then
                _pop.MinStaffRoles = _roleCount
            End If
            If _pop.MaxStaffRoles < _roleCount Then
                _pop.MaxStaffRoles = _roleCount
            End If
        End Sub

        Public Sub CalculateFitness()
            Dim StaffCountFitness As Single = ((_staffCount - _pop.MinIndividuals) / (_pop.MaxIndividuals - _pop.MinIndividuals)) * _pop.FactorFewIndividuals
            If _pop.MaxIndividuals = _pop.MinIndividuals Then StaffCountFitness = 0
            Dim EventRoleCountFitness As Single = ((_roleCount - _pop.MinStaffRoles) / (_pop.MaxStaffRoles - _pop.MinStaffRoles)) * _pop.FactorEventRoleCount
            If _pop.MaxStaffRoles = _pop.MinStaffRoles Then EventRoleCountFitness = 0
            'Stop
            _fitness = StaffCountFitness + EventRoleCountFitness
            Tag = _roleCount
        End Sub

        Public ReadOnly Property StaffCount() As Integer
            Get
                Return _staffCount
            End Get
        End Property

        Public ReadOnly Property Fitness() As Single
            Get
                Return _fitness
            End Get
        End Property

        Public Sub New(ByVal Population As cPopulation)
            _pop = Population
        End Sub
    End Class

    Private Class IndComparer
        Implements IComparer(Of cIndividual)

        Public Function Compare(ByVal x As cIndividual, ByVal y As cIndividual) As Integer Implements System.Collections.Generic.IComparer(Of cIndividual).Compare
            If x.Fitness > y.Fitness Then
                Return 1
            ElseIf x.Fitness < y.Fitness Then
                Return -1
            Else
                Return 1
            End If
        End Function
    End Class

    Private Class cPopulation

        Public Individuals As New SortedList(Of cIndividual, cIndividual)(New IndComparer)

        Public FactorFewIndividuals As Integer
        Public FactorEventRoleCount As Integer
        Public MaxIndividuals As Integer
        Public MinIndividuals As Integer
        Public MaxStaffRoles As Single
        Public MinStaffRoles As Single

        Public Sub Reset()
            MaxIndividuals = 0
            MinIndividuals = 0
            MaxStaffRoles = 0
            MinStaffRoles = 0
        End Sub
    End Class

    Private Function GetCompetingShifts(ByVal Shift As cStaffScheduleShift) As List(Of cStaffScheduleShift)
        Dim TmpList As New List(Of cStaffScheduleShift)
        For Each TmpLoc As cStaffScheduleLocation In MyEvent.Schedule.Locations
            For Each TmpRole As cStaffScheduleRole In TmpLoc.Roles
                For Each TmpDay As cStaffScheduleDay In TmpRole.Days
                    If TmpDay.Day.DayDate = Shift.Day.Day.DayDate Then
                        For Each TmpShift As cStaffScheduleShift In TmpDay.Shifts
                            If TmpShift.Shift.Roles(TmpShift.Day.Role.Role.ID).Quantity > 0 AndAlso TmpShift.DatabaseID <> Shift.DatabaseID Then
                                If Not TmpList.Contains(TmpShift) Then
                                    If (TmpShift.Shift.StartMam >= Shift.Shift.StartMam AndAlso TmpShift.Shift.StartMam <= Shift.Shift.EndMam) OrElse (TmpShift.Shift.EndMam >= Shift.Shift.StartMam AndAlso TmpShift.Shift.EndMam <= Shift.Shift.EndMam) OrElse (Shift.Shift.StartMam >= TmpShift.Shift.StartMam AndAlso Shift.Shift.StartMam <= TmpShift.Shift.EndMam) OrElse (Shift.Shift.EndMam >= TmpShift.Shift.StartMam AndAlso Shift.Shift.EndMam <= TmpShift.Shift.EndMam) Then
                                        TmpList.Add(TmpShift)
                                    End If
                                End If
                            End If
                        Next
                    End If
                Next
            Next
        Next
        Return TmpList
    End Function

    

    Private Sub AddRandomStaffToShift(ByVal Individual As cIndividual, ByVal Shift As cStaffScheduleShift, ByVal CompetingShifts As List(Of cStaffScheduleShift), ByVal AvailableStaff As List(Of cStaff), ByVal StaffToBeAssigned As Integer)
        Dim CountInCompeting As Integer
        While Shift.AssignedStaff.Count < StaffToBeAssigned AndAlso Shift.AssignedStaff.Count < AvailableStaff.Count - CountInCompeting
            Dim TmpStaff As cStaff = AvailableStaff(Int(Rnd() * AvailableStaff.Count))
            Dim NotInCompeting As Boolean = True
            CountInCompeting = 0
            For Each TmpShift As cStaffScheduleShift In CompetingShifts
                If Individual.Schedule.Locations(TmpShift.Day.Role.Location.Location.ID).Roles(TmpShift.Day.Role.Role.ID).Days(CStr(TmpShift.Day.Day.DayDate)).Shifts(TmpShift.Shift.ID).AssignedStaff.Contains("DB" & TmpStaff.DatabaseID) Then
                    NotInCompeting = False
                End If
                CountInCompeting += Individual.Schedule.Locations(TmpShift.Day.Role.Location.Location.ID).Roles(TmpShift.Day.Role.Role.ID).Days(CStr(TmpShift.Day.Day.DayDate)).Shifts(TmpShift.Shift.ID).AssignedStaff.count
            Next
            If Not Shift.AssignedStaff.Contains("DB" & TmpStaff.DatabaseID) AndAlso NotInCompeting Then
                Shift.AssignedStaff.Add(TmpStaff, "DB" & TmpStaff.DatabaseID)
            End If
        End While
    End Sub

    Private Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
        ' Se cmdOK_Click_Old
        ' Gör om Optimeringen så att den först kollar vem som kan jobba flest skift. Sedan ska den i största möjliga mån sätta samma person på alla
        ' skift på samma roll.
    End Sub

    Private Sub cmdOk_Click_old(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim TmpDoc As New Xml.XmlDocument
        Dim AvailableStaffForShift As New Dictionary(Of Integer, List(Of cStaff))
        Dim CompetingShiftsForShift As New Dictionary(Of Integer, List(Of cStaffScheduleShift))

        Me.Cursor = Cursors.WaitCursor

        lblStatus.Text = "Preparing..."
        pbStatus.Maximum = MyEvent.Locations.Count
        pbStatus.Value = 0
        For Each TmpLoc As cStaffScheduleLocation In MyEvent.Schedule.Locations
            For Each TmpRole As cStaffScheduleRole In TmpLoc.Roles
                For Each TmpDay As cStaffScheduleDay In TmpRole.Days
                    For Each TmpShift As cStaffScheduleShift In TmpDay.Shifts
                        If TmpShift.Shift.Roles(TmpShift.Day.Role.Role.ID).Quantity > 0 Then
                            AvailableStaffForShift.Add(TmpShift.DatabaseID, Database.GetAvailableStaffForShiftList(TmpShift.DatabaseID))
                            Dim TmpCompShifts As List(Of cStaffScheduleShift) = GetCompetingShifts(TmpShift)
                            CompetingShiftsForShift.Add(TmpShift.DatabaseID, TmpCompShifts)
                        End If
                    Next
                Next
            Next
            pbStatus.Value += 1
            Application.DoEvents()
        Next

        'Create Base Population
        Dim Population As New cPopulation
        Dim Individuals As New List(Of cIndividual)

        Population.FactorEventRoleCount = RadiobuttonList3.Value
        Population.FactorFewIndividuals = RadiobuttonList1.Value

        lblStatus.Text = "Creating base population..."
        pbStatus.Maximum = txtIndividuals.Text
        pbStatus.Value = 0
        pnlProgress.Visible = True
        For i As Integer = 1 To txtIndividuals.Text
            Randomize(i)
            Dim TmpInd As New cIndividual(Population)
            TmpInd.Schedule = New cStaffSchedule(MyEvent)
            TmpInd.Schedule.CreateFromXML(MyEvent.Schedule.CreateXML(TmpDoc))

            For Each TmpLoc As cStaffScheduleLocation In TmpInd.Schedule.Locations
                For Each TmpRole As cStaffScheduleRole In TmpLoc.Roles
                    For Each TmpDay As cStaffScheduleDay In TmpRole.Days
                        For Each TmpShift As cStaffScheduleShift In TmpDay.Shifts
                            If TmpShift.Shift.Roles(TmpShift.Day.Role.Role.ID).Quantity > 0 Then
                                Dim StaffToBeAssigned As Integer = TmpShift.Shift.Roles(TmpRole.Role.ID).Quantity
                                AddRandomStaffToShift(TmpInd, TmpShift, CompetingShiftsForShift(TmpShift.DatabaseID), AvailableStaffForShift(TmpShift.DatabaseID), StaffToBeAssigned)
                            End If
                        Next
                    Next
                Next
            Next
            TmpInd.CalculateVariables()
            Individuals.Add(TmpInd)
            pbStatus.Value = i
            Application.DoEvents()
        Next
        Population.Individuals.Clear()
        For i As Integer = 0 To txtIndividuals.Text - 1
            Individuals(i).Tag = Guid.NewGuid.ToString
            Individuals(i).CalculateFitness()
            Population.Individuals.Add(Individuals(i), Individuals(i))
        Next
        For Generation As Integer = 1 To txtGenerations.Text
            lblStatus.Text = "Growing generation " & Generation & "..."
            pbStatus.Maximum = (0.5 * txtIndividuals.Text) - 1
            pbStatus.Value = 0
            Individuals.Clear()
            Population.Reset()
            For i As Integer = 0 To (0.5 * txtIndividuals.Text) - 1
                Population.Individuals.Keys(i).CalculateVariables()
                Individuals.Add(Population.Individuals.Keys(i))
            Next
            For i As Integer = 0 To (0.5 * txtIndividuals.Text) - 1
                Dim Parent1 As cIndividual = Population.Individuals.Keys(Int(Rnd() * (0.5 * txtIndividuals.Text)))
                Dim Parent2 As cIndividual = Population.Individuals.Keys(Int(Rnd() * (0.5 * txtIndividuals.Text)))
                While Parent1 Is Parent2
                    Parent2 = Population.Individuals.Keys(Int(Rnd() * (0.5 * txtIndividuals.Text)))
                End While
                Randomize(i)
                Dim TmpInd As New cIndividual(Population)
                TmpInd.Schedule = New cStaffSchedule(MyEvent)
                TmpInd.Schedule.CreateFromXML(Parent1.Schedule.CreateXML(TmpDoc))

                'CrossOvers

                For j As Integer = 1 To Int(Rnd() * txtCrossovers.Text) + 1
                    Dim BP As Integer = Int(Rnd() * Parent2.Schedule.ShiftList.Count)
                    While TmpInd.Schedule.ShiftList(BP).Shift.Roles(TmpInd.Schedule.ShiftList(BP).Day.Role.Role.ID).Quantity = 0
                        BP = Int(Rnd() * Parent2.Schedule.ShiftList.Count)
                    End While
                    TmpInd.Schedule.ShiftList(BP).AssignedStaff.Clear()
                    For Each TmpStaff As cStaff In Parent2.Schedule.ShiftList(BP).AssignedStaff
                        TmpInd.Schedule.ShiftList(BP).AssignedStaff.Add(TmpStaff, "DB" & TmpStaff.DatabaseID)
                    Next
                    For Each Shift As cStaffScheduleShift In CompetingShiftsForShift(TmpInd.Schedule.ShiftList(BP).DatabaseID)
                        TmpInd.Schedule.Locations(Shift.Day.Role.Location.Location.ID).Roles(Shift.Day.Role.Role.ID).Days(CStr(Shift.Day.Day.DayDate)).Shifts(Shift.Shift.ID).assignedstaff.clear()
                        For Each TmpStaff As cStaff In Parent2.Schedule.Locations(Shift.Day.Role.Location.Location.ID).Roles(Shift.Day.Role.Role.ID).Days(CStr(Shift.Day.Day.DayDate)).Shifts(Shift.Shift.ID).assignedstaff
                            TmpInd.Schedule.Locations(Shift.Day.Role.Location.Location.ID).Roles(Shift.Day.Role.Role.ID).Days(CStr(Shift.Day.Day.DayDate)).Shifts(Shift.Shift.ID).assignedstaff.add(TmpStaff, "DB" & TmpStaff.DatabaseID)
                        Next
                    Next
                Next

                'Mutate

                While Int(Rnd() * 100) < txtMutation.Text
                    Dim BP As Integer = Int(Rnd() * TmpInd.Schedule.ShiftList.Count)
                    While TmpInd.Schedule.ShiftList(BP).Shift.Roles(TmpInd.Schedule.ShiftList(BP).Day.Role.Role.ID).Quantity = 0
                        BP = Int(Rnd() * Parent2.Schedule.ShiftList.Count)
                    End While
                    Dim TmpStaffNo As Integer = Int(Rnd() * TmpInd.Schedule.ShiftList(BP).AssignedStaff.Count) + 1
                    TmpInd.Schedule.ShiftList(BP).AssignedStaff.Remove(TmpStaffNo)
                    For Each Shift As cStaffScheduleShift In CompetingShiftsForShift(TmpInd.Schedule.ShiftList(BP).DatabaseID)
                        TmpStaffNo = Int(Rnd() * TmpInd.Schedule.Locations(Shift.Day.Role.Location.Location.ID).Roles(Shift.Day.Role.Role.ID).Days(CStr(Shift.Day.Day.DayDate)).Shifts(Shift.Shift.ID).assignedstaff.Count) + 1
                        TmpInd.Schedule.Locations(Shift.Day.Role.Location.Location.ID).Roles(Shift.Day.Role.Role.ID).Days(CStr(Shift.Day.Day.DayDate)).Shifts(Shift.Shift.ID).assignedstaff.remove(TmpStaffNo)
                    Next
                    Dim StaffToBeAssigned As Integer = TmpInd.Schedule.ShiftList(BP).Shift.Roles(TmpInd.Schedule.ShiftList(BP).Day.Role.Role.ID).Quantity
                    AddRandomStaffToShift(TmpInd, TmpInd.Schedule.ShiftList(BP), CompetingShiftsForShift(TmpInd.Schedule.ShiftList(BP).DatabaseID), AvailableStaffForShift(TmpInd.Schedule.ShiftList(BP).DatabaseID), StaffToBeAssigned)
                    For Each Shift As cStaffScheduleShift In CompetingShiftsForShift(TmpInd.Schedule.ShiftList(BP).DatabaseID)
                        Dim TmpShift As cStaffScheduleShift = TmpInd.Schedule.Locations(Shift.Day.Role.Location.Location.ID).Roles(Shift.Day.Role.Role.ID).Days(CStr(Shift.Day.Day.DayDate)).Shifts(Shift.Shift.ID)
                        StaffToBeAssigned = TmpShift.Shift.Roles(TmpInd.Schedule.ShiftList(BP).Day.Role.Role.ID).Quantity
                        AddRandomStaffToShift(TmpInd, TmpShift, CompetingShiftsForShift(TmpInd.Schedule.ShiftList(BP).DatabaseID), AvailableStaffForShift(TmpInd.Schedule.ShiftList(BP).DatabaseID), StaffToBeAssigned)
                    Next
                End While

                TmpInd.CalculateVariables()
                Individuals.Add(TmpInd)
                pbStatus.Value = i
                Application.DoEvents()
            Next
            Population.Individuals.Clear()
            For i As Integer = 0 To txtIndividuals.Text - 1
                Individuals(i).Tag = Guid.NewGuid.ToString
                Individuals(i).CalculateFitness()
                Population.Individuals.Add(Individuals(i), Individuals(i))
            Next
        Next

        MyEvent.Schedule.CreateFromXML(Population.Individuals.Keys(0).Schedule.CreateXML(TmpDoc))
        Windows.Forms.MessageBox.Show("Fitness: " & Population.Individuals.Keys(0).Fitness)
        Me.Cursor = Cursors.Default
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Dispose()
    End Sub
End Class

