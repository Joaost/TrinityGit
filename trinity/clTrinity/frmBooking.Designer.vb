<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBooking
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBooking))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim CWrapper1 As clTrinity.Trinity.cWrapper = New clTrinity.Trinity.cWrapper()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle16 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle14 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle15 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle17 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle18 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle19 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle20 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle21 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle22 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle23 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle24 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle25 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle26 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle27 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle28 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle29 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle30 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle31 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle32 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle33 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle34 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle35 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle36 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle37 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle38 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle39 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle40 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.sptBooking = New System.Windows.Forms.SplitContainer()
        Me.sptSpotlist = New System.Windows.Forms.SplitContainer()
        Me.TabSchedule = New clTrinity.ExtendedTabControl()
        Me.tpNumeric = New System.Windows.Forms.TabPage()
        Me.stpSchedule = New System.Windows.Forms.ToolStrip()
        Me.cmdScheduleFilter = New System.Windows.Forms.ToolStripButton()
        Me.cmdScheduleColumns = New System.Windows.Forms.ToolStripButton()
        Me.cmdEstimate = New System.Windows.Forms.ToolStripButton()
        Me.cmdTweak = New System.Windows.Forms.ToolStripButton()
        Me.cmdRFEst = New System.Windows.Forms.ToolStripButton()
        Me.cmdSolus = New System.Windows.Forms.ToolStripButton()
        Me.cmbSolusFreq = New System.Windows.Forms.ToolStripComboBox()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripLabel2 = New System.Windows.Forms.ToolStripLabel()
        Me.txtCustomPeriod = New System.Windows.Forms.ToolStripTextBox()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripLabel1 = New System.Windows.Forms.ToolStripLabel()
        Me.txtProgramNameFilter = New System.Windows.Forms.ToolStripTextBox()
        Me.lblStatus = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator()
        Me.imgOn = New System.Windows.Forms.PictureBox()
        Me.imgOff = New System.Windows.Forms.PictureBox()
        Me.grdSchedule = New System.Windows.Forms.DataGridView()
        Me.tpGfxSchedule = New System.Windows.Forms.TabPage()
        Me.cmbWeeks = New System.Windows.Forms.ComboBox()
        Me.GfxSchedule2 = New clTrinity.gfxSchedule2()
        Me.pbLast = New System.Windows.Forms.PictureBox()
        Me.pbNext = New System.Windows.Forms.PictureBox()
        Me.pbPrevious = New System.Windows.Forms.PictureBox()
        Me.pbFirst = New System.Windows.Forms.PictureBox()
        Me.tabSpotlist = New System.Windows.Forms.TabControl()
        Me.tpSpotlist = New System.Windows.Forms.TabPage()
        Me.grdSpotlist = New System.Windows.Forms.DataGridView()
        Me.stpSpotlist = New System.Windows.Forms.ToolStrip()
        Me.cmdSpotlistFilter = New System.Windows.Forms.ToolStripButton()
        Me.cmdSpotlistColumns = New System.Windows.Forms.ToolStripButton()
        Me.cmdExcel = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.cmdUpdatePrices = New System.Windows.Forms.ToolStripButton()
        Me.cmdOptimize = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.cmdSpotlistAV = New System.Windows.Forms.ToolStripButton()
        Me.cmdFilm = New System.Windows.Forms.ToolStripButton()
        Me.cmdNotes = New System.Windows.Forms.ToolStripButton()
        Me.cmdSpotlistDelete = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.cmdLoadOtherSpots = New System.Windows.Forms.ToolStripButton()
        Me.cmdExportToAllocate = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.cmdCheckForChanges = New System.Windows.Forms.ToolStripButton()
        Me.cmdLock = New System.Windows.Forms.ToolStripButton()
        Me.cmdReadK2Spotlist = New System.Windows.Forms.ToolStripButton()
        Me.tpCrossTab = New System.Windows.Forms.TabPage()
        Me.mnuPanes = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.PlannedToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BookedToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LeftToBookToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FilmsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DaypartsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ReachToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EstimatedTargetProfileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PictureToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.InfoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DetailsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ProgsInOtherChannelsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TrendToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TargetProfileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PositionInBreakToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.grpInfo = New System.Windows.Forms.GroupBox()
        Me.pnlScore = New System.Windows.Forms.TableLayoutPanel()
        Me.PictureBox13 = New System.Windows.Forms.PictureBox()
        Me.PictureBox12 = New System.Windows.Forms.PictureBox()
        Me.PictureBox11 = New System.Windows.Forms.PictureBox()
        Me.PictureBox10 = New System.Windows.Forms.PictureBox()
        Me.PictureBox9 = New System.Windows.Forms.PictureBox()
        Me.PictureBox8 = New System.Windows.Forms.PictureBox()
        Me.PictureBox7 = New System.Windows.Forms.PictureBox()
        Me.PictureBox6 = New System.Windows.Forms.PictureBox()
        Me.PictureBox5 = New System.Windows.Forms.PictureBox()
        Me.PictureBox4 = New System.Windows.Forms.PictureBox()
        Me.cmdImdb = New System.Windows.Forms.Button()
        Me.lblInfo = New System.Windows.Forms.Label()
        Me.grpPicture = New System.Windows.Forms.GroupBox()
        Me.picPicture = New System.Windows.Forms.PictureBox()
        Me.grpLeftToBook = New System.Windows.Forms.GroupBox()
        Me.grdLeftToBook = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn25 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn26 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn27 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn28 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lblLeftNetHeadline = New System.Windows.Forms.Label()
        Me.lblLeftBudgetHeadline = New System.Windows.Forms.Label()
        Me.lblLeftPercentHeadline = New System.Windows.Forms.Label()
        Me.lblLeftEstHeadline = New System.Windows.Forms.Label()
        Me.lblLeftTRPHeadline = New System.Windows.Forms.Label()
        Me.lblLeftChanHeadline = New System.Windows.Forms.Label()
        Me.grpPeak = New System.Windows.Forms.GroupBox()
        Me.chtPrimePeak = New clTrinity.PrimePeakChart()
        Me.grpEstProfile = New System.Windows.Forms.GroupBox()
        Me.chtEstProfile = New clTrinity.ProfileChart()
        Me.grpGender = New System.Windows.Forms.GroupBox()
        Me.chtGender = New clTrinity.GenderChart()
        Me.grpPIB = New System.Windows.Forms.GroupBox()
        Me.chtPIB = New clTrinity.PIBChart()
        Me.grpDayparts = New System.Windows.Forms.GroupBox()
        Me.grdDayparts = New System.Windows.Forms.DataGridView()
        Me.colDaypart = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colPlannedDP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colBookedDP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colNetDP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.grpPlannedTRP = New System.Windows.Forms.GroupBox()
        Me.lblNetHeadline = New System.Windows.Forms.Label()
        Me.lblBudgetHeadline = New System.Windows.Forms.Label()
        Me.lblPercentHeadline = New System.Windows.Forms.Label()
        Me.lblEstHeadline = New System.Windows.Forms.Label()
        Me.lblTRPHeadline = New System.Windows.Forms.Label()
        Me.lblChanHeadline = New System.Windows.Forms.Label()
        Me.grdPlannedTRP = New System.Windows.Forms.DataGridView()
        Me.colPlannedChan = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colPlannedEst = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colPlannedPercent = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colPlannedNet = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.grpBookedTRP = New System.Windows.Forms.GroupBox()
        Me.grdBookedTRP = New System.Windows.Forms.DataGridView()
        Me.colBookedChan = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colBookedEst = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colBookedPercent = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colBookedNet = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lblBookedNetHeadline = New System.Windows.Forms.Label()
        Me.lblBookedBudgetHeadline = New System.Windows.Forms.Label()
        Me.lblBookedPercentHeadline = New System.Windows.Forms.Label()
        Me.lblBookedEstHeadline = New System.Windows.Forms.Label()
        Me.lblBookedTRPHeadline = New System.Windows.Forms.Label()
        Me.lblBookedChanHeadline = New System.Windows.Forms.Label()
        Me.grpFilms = New System.Windows.Forms.GroupBox()
        Me.grdFilms = New System.Windows.Forms.DataGridView()
        Me.colFilm = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colPlanned = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colPlanPerc = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colBooked = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colBookedPerc = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colNet = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.grpProfile = New System.Windows.Forms.GroupBox()
        Me.chtProfile = New clTrinity.ProfileChart()
        Me.grpTrend = New System.Windows.Forms.GroupBox()
        Me.chtTrend = New clTrinity.TrendChart()
        Me.grpOther = New System.Windows.Forms.GroupBox()
        Me.grdOther = New System.Windows.Forms.DataGridView()
        Me.grpDetails = New System.Windows.Forms.GroupBox()
        Me.grdDetails = New System.Windows.Forms.DataGridView()
        Me.colDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colTime = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colProgBefore = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colProgAfter = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colTarget = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colBuyTarget = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.grpReach = New System.Windows.Forms.GroupBox()
        Me.grdReach = New System.Windows.Forms.DataGridView()
        Me.grpGeneral = New System.Windows.Forms.GroupBox()
        Me.cmbFilm = New System.Windows.Forms.ComboBox()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.cmbChannel = New System.Windows.Forms.ComboBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.cmbDatabase = New System.Windows.Forms.ComboBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.tmrFilter = New System.Windows.Forms.Timer(Me.components)
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn9 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn10 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn11 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn12 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn13 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn14 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn15 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn16 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn17 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn18 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn19 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn20 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn21 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn22 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn23 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn24 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colEstTime = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colEstProgAfter = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colEstMainTarget = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colEstChanTarget = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colChan = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colProgram = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.sptBooking.Panel1.SuspendLayout()
        Me.sptBooking.Panel2.SuspendLayout()
        Me.sptBooking.SuspendLayout()
        Me.sptSpotlist.Panel1.SuspendLayout()
        Me.sptSpotlist.Panel2.SuspendLayout()
        Me.sptSpotlist.SuspendLayout()
        Me.TabSchedule.SuspendLayout()
        Me.tpNumeric.SuspendLayout()
        Me.stpSchedule.SuspendLayout()
        CType(Me.imgOn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgOff, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdSchedule, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpGfxSchedule.SuspendLayout()
        CType(Me.pbLast, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbNext, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbPrevious, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbFirst, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabSpotlist.SuspendLayout()
        Me.tpSpotlist.SuspendLayout()
        CType(Me.grdSpotlist, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.stpSpotlist.SuspendLayout()
        Me.mnuPanes.SuspendLayout()
        Me.grpInfo.SuspendLayout()
        Me.pnlScore.SuspendLayout()
        CType(Me.PictureBox13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpPicture.SuspendLayout()
        CType(Me.picPicture, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpLeftToBook.SuspendLayout()
        CType(Me.grdLeftToBook, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpPeak.SuspendLayout()
        Me.grpEstProfile.SuspendLayout()
        Me.grpGender.SuspendLayout()
        Me.grpPIB.SuspendLayout()
        Me.grpDayparts.SuspendLayout()
        CType(Me.grdDayparts, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpPlannedTRP.SuspendLayout()
        CType(Me.grdPlannedTRP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpBookedTRP.SuspendLayout()
        CType(Me.grdBookedTRP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpFilms.SuspendLayout()
        CType(Me.grdFilms, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpProfile.SuspendLayout()
        Me.grpTrend.SuspendLayout()
        Me.grpOther.SuspendLayout()
        CType(Me.grdOther, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpDetails.SuspendLayout()
        CType(Me.grdDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpReach.SuspendLayout()
        CType(Me.grdReach, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpGeneral.SuspendLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'sptBooking
        '
        Me.sptBooking.Dock = System.Windows.Forms.DockStyle.Fill
        Me.sptBooking.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.sptBooking.Location = New System.Drawing.Point(0, 0)
        Me.sptBooking.Name = "sptBooking"
        '
        'sptBooking.Panel1
        '
        Me.sptBooking.Panel1.Controls.Add(Me.sptSpotlist)
        '
        'sptBooking.Panel2
        '
        Me.sptBooking.Panel2.AutoScroll = True
        Me.sptBooking.Panel2.ContextMenuStrip = Me.mnuPanes
        Me.sptBooking.Panel2.Controls.Add(Me.grpInfo)
        Me.sptBooking.Panel2.Controls.Add(Me.grpPicture)
        Me.sptBooking.Panel2.Controls.Add(Me.grpLeftToBook)
        Me.sptBooking.Panel2.Controls.Add(Me.grpPeak)
        Me.sptBooking.Panel2.Controls.Add(Me.grpEstProfile)
        Me.sptBooking.Panel2.Controls.Add(Me.grpGender)
        Me.sptBooking.Panel2.Controls.Add(Me.grpPIB)
        Me.sptBooking.Panel2.Controls.Add(Me.grpDayparts)
        Me.sptBooking.Panel2.Controls.Add(Me.grpPlannedTRP)
        Me.sptBooking.Panel2.Controls.Add(Me.grpBookedTRP)
        Me.sptBooking.Panel2.Controls.Add(Me.grpFilms)
        Me.sptBooking.Panel2.Controls.Add(Me.grpProfile)
        Me.sptBooking.Panel2.Controls.Add(Me.grpTrend)
        Me.sptBooking.Panel2.Controls.Add(Me.grpOther)
        Me.sptBooking.Panel2.Controls.Add(Me.grpDetails)
        Me.sptBooking.Panel2.Controls.Add(Me.grpReach)
        Me.sptBooking.Panel2.Controls.Add(Me.grpGeneral)
        Me.sptBooking.Size = New System.Drawing.Size(969, 630)
        Me.sptBooking.SplitterDistance = 702
        Me.sptBooking.TabIndex = 0
        '
        'sptSpotlist
        '
        Me.sptSpotlist.Dock = System.Windows.Forms.DockStyle.Fill
        Me.sptSpotlist.Location = New System.Drawing.Point(0, 0)
        Me.sptSpotlist.Name = "sptSpotlist"
        Me.sptSpotlist.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'sptSpotlist.Panel1
        '
        Me.sptSpotlist.Panel1.Controls.Add(Me.TabSchedule)
        '
        'sptSpotlist.Panel2
        '
        Me.sptSpotlist.Panel2.Controls.Add(Me.tabSpotlist)
        Me.sptSpotlist.Size = New System.Drawing.Size(702, 630)
        Me.sptSpotlist.SplitterDistance = 313
        Me.sptSpotlist.TabIndex = 0
        '
        'TabSchedule
        '
        Me.TabSchedule.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabSchedule.Controls.Add(Me.tpNumeric)
        Me.TabSchedule.Controls.Add(Me.tpGfxSchedule)
        Me.TabSchedule.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed
        Me.TabSchedule.ItemSize = New System.Drawing.Size(200, 19)
        Me.TabSchedule.Location = New System.Drawing.Point(3, 3)
        Me.TabSchedule.Name = "TabSchedule"
        Me.TabSchedule.SelectedIndex = 0
        Me.TabSchedule.Size = New System.Drawing.Size(694, 305)
        Me.TabSchedule.TabIndex = 4
        '
        'tpNumeric
        '
        Me.tpNumeric.Controls.Add(Me.stpSchedule)
        Me.tpNumeric.Controls.Add(Me.imgOn)
        Me.tpNumeric.Controls.Add(Me.imgOff)
        Me.tpNumeric.Controls.Add(Me.grdSchedule)
        Me.tpNumeric.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tpNumeric.Location = New System.Drawing.Point(4, 23)
        Me.tpNumeric.Name = "tpNumeric"
        Me.tpNumeric.Padding = New System.Windows.Forms.Padding(3)
        Me.tpNumeric.Size = New System.Drawing.Size(686, 278)
        Me.tpNumeric.TabIndex = 0
        Me.tpNumeric.Text = "Numeric Schedule"
        Me.tpNumeric.UseVisualStyleBackColor = True
        '
        'stpSchedule
        '
        Me.stpSchedule.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.stpSchedule.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmdScheduleFilter, Me.cmdScheduleColumns, Me.cmdEstimate, Me.cmdTweak, Me.cmdRFEst, Me.cmdSolus, Me.cmbSolusFreq, Me.ToolStripSeparator6, Me.ToolStripLabel2, Me.txtCustomPeriod, Me.ToolStripSeparator3, Me.ToolStripLabel1, Me.txtProgramNameFilter, Me.lblStatus, Me.ToolStripSeparator7})
        Me.stpSchedule.Location = New System.Drawing.Point(3, 3)
        Me.stpSchedule.Name = "stpSchedule"
        Me.stpSchedule.Size = New System.Drawing.Size(680, 25)
        Me.stpSchedule.Stretch = True
        Me.stpSchedule.TabIndex = 2
        Me.stpSchedule.Text = "ToolStrip1"
        '
        'cmdScheduleFilter
        '
        Me.cmdScheduleFilter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdScheduleFilter.Image = CType(resources.GetObject("cmdScheduleFilter.Image"), System.Drawing.Image)
        Me.cmdScheduleFilter.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdScheduleFilter.Name = "cmdScheduleFilter"
        Me.cmdScheduleFilter.Size = New System.Drawing.Size(23, 22)
        Me.cmdScheduleFilter.ToolTipText = "Filter schedule"
        '
        'cmdScheduleColumns
        '
        Me.cmdScheduleColumns.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdScheduleColumns.Image = CType(resources.GetObject("cmdScheduleColumns.Image"), System.Drawing.Image)
        Me.cmdScheduleColumns.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdScheduleColumns.Name = "cmdScheduleColumns"
        Me.cmdScheduleColumns.Size = New System.Drawing.Size(23, 22)
        Me.cmdScheduleColumns.Text = "ToolStripButton2"
        Me.cmdScheduleColumns.ToolTipText = "Define columns for schedule"
        '
        'cmdEstimate
        '
        Me.cmdEstimate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdEstimate.Image = Global.clTrinity.My.Resources.Resources.magic_wand_2_16_x16
        Me.cmdEstimate.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdEstimate.Name = "cmdEstimate"
        Me.cmdEstimate.Size = New System.Drawing.Size(23, 22)
        Me.cmdEstimate.ToolTipText = "Estimate schedule"
        '
        'cmdTweak
        '
        Me.cmdTweak.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdTweak.Enabled = False
        Me.cmdTweak.Image = CType(resources.GetObject("cmdTweak.Image"), System.Drawing.Image)
        Me.cmdTweak.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdTweak.Name = "cmdTweak"
        Me.cmdTweak.Size = New System.Drawing.Size(23, 22)
        Me.cmdTweak.Text = "ToolStripButton1"
        Me.cmdTweak.ToolTipText = "Tweak estimate"
        Me.cmdTweak.Visible = False
        '
        'cmdRFEst
        '
        Me.cmdRFEst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdRFEst.Image = Global.clTrinity.My.Resources.Resources.light_bulb_2_12x20
        Me.cmdRFEst.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdRFEst.Name = "cmdRFEst"
        Me.cmdRFEst.Size = New System.Drawing.Size(23, 22)
        Me.cmdRFEst.ToolTipText = "Turn on reach-estimation"
        '
        'cmdSolus
        '
        Me.cmdSolus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdSolus.Enabled = False
        Me.cmdSolus.Image = CType(resources.GetObject("cmdSolus.Image"), System.Drawing.Image)
        Me.cmdSolus.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdSolus.Name = "cmdSolus"
        Me.cmdSolus.Size = New System.Drawing.Size(23, 22)
        Me.cmdSolus.ToolTipText = "Calculate solus"
        '
        'cmbSolusFreq
        '
        Me.cmbSolusFreq.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSolusFreq.Enabled = False
        Me.cmbSolusFreq.Items.AddRange(New Object() {"1+", "2+", "3+", "4+", "5+"})
        Me.cmbSolusFreq.Name = "cmbSolusFreq"
        Me.cmbSolusFreq.Size = New System.Drawing.Size(75, 25)
        Me.cmbSolusFreq.ToolTipText = "Solus frequency"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripLabel2
        '
        Me.ToolStripLabel2.Name = "ToolStripLabel2"
        Me.ToolStripLabel2.Size = New System.Drawing.Size(110, 22)
        Me.ToolStripLabel2.Text = "Custom est. period:"
        '
        'txtCustomPeriod
        '
        Me.txtCustomPeriod.Name = "txtCustomPeriod"
        Me.txtCustomPeriod.Size = New System.Drawing.Size(60, 25)
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripLabel1
        '
        Me.ToolStripLabel1.Name = "ToolStripLabel1"
        Me.ToolStripLabel1.Size = New System.Drawing.Size(86, 22)
        Me.ToolStripLabel1.Text = "Program name"
        '
        'txtProgramNameFilter
        '
        Me.txtProgramNameFilter.Name = "txtProgramNameFilter"
        Me.txtProgramNameFilter.Size = New System.Drawing.Size(100, 25)
        '
        'lblStatus
        '
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(0, 22)
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        Me.ToolStripSeparator7.Size = New System.Drawing.Size(6, 25)
        '
        'imgOn
        '
        Me.imgOn.Image = CType(resources.GetObject("imgOn.Image"), System.Drawing.Image)
        Me.imgOn.Location = New System.Drawing.Point(228, 71)
        Me.imgOn.Name = "imgOn"
        Me.imgOn.Size = New System.Drawing.Size(24, 24)
        Me.imgOn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.imgOn.TabIndex = 3
        Me.imgOn.TabStop = False
        Me.imgOn.Visible = False
        '
        'imgOff
        '
        Me.imgOff.Image = CType(resources.GetObject("imgOff.Image"), System.Drawing.Image)
        Me.imgOff.Location = New System.Drawing.Point(198, 71)
        Me.imgOff.Name = "imgOff"
        Me.imgOff.Size = New System.Drawing.Size(24, 24)
        Me.imgOff.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.imgOff.TabIndex = 2
        Me.imgOff.TabStop = False
        Me.imgOff.Visible = False
        '
        'grdSchedule
        '
        Me.grdSchedule.AllowUserToAddRows = False
        Me.grdSchedule.AllowUserToDeleteRows = False
        Me.grdSchedule.AllowUserToOrderColumns = True
        Me.grdSchedule.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ControlLight
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.grdSchedule.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.grdSchedule.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdSchedule.BackgroundColor = System.Drawing.Color.Silver
        Me.grdSchedule.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdSchedule.Location = New System.Drawing.Point(3, 29)
        Me.grdSchedule.Name = "grdSchedule"
        Me.grdSchedule.ReadOnly = True
        Me.grdSchedule.RowHeadersVisible = False
        Me.grdSchedule.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdSchedule.Size = New System.Drawing.Size(680, 248)
        Me.grdSchedule.TabIndex = 0
        Me.grdSchedule.VirtualMode = True
        '
        'tpGfxSchedule
        '
        Me.tpGfxSchedule.Controls.Add(Me.cmbWeeks)
        Me.tpGfxSchedule.Controls.Add(Me.GfxSchedule2)
        Me.tpGfxSchedule.Controls.Add(Me.pbLast)
        Me.tpGfxSchedule.Controls.Add(Me.pbNext)
        Me.tpGfxSchedule.Controls.Add(Me.pbPrevious)
        Me.tpGfxSchedule.Controls.Add(Me.pbFirst)
        Me.tpGfxSchedule.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tpGfxSchedule.Location = New System.Drawing.Point(4, 23)
        Me.tpGfxSchedule.Name = "tpGfxSchedule"
        Me.tpGfxSchedule.Padding = New System.Windows.Forms.Padding(3)
        Me.tpGfxSchedule.Size = New System.Drawing.Size(686, 278)
        Me.tpGfxSchedule.TabIndex = 1
        Me.tpGfxSchedule.Text = "Graphic Schedule"
        Me.tpGfxSchedule.UseVisualStyleBackColor = True
        '
        'cmbWeeks
        '
        Me.cmbWeeks.FormattingEnabled = True
        Me.cmbWeeks.Location = New System.Drawing.Point(237, 5)
        Me.cmbWeeks.Name = "cmbWeeks"
        Me.cmbWeeks.Size = New System.Drawing.Size(121, 22)
        Me.cmbWeeks.TabIndex = 7
        '
        'GfxSchedule2
        '
        Me.GfxSchedule2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GfxSchedule2.BackColor = System.Drawing.SystemColors.Menu
        Me.GfxSchedule2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.GfxSchedule2.DayWidths = 100.0!
        Me.GfxSchedule2.EndDate = New Date(2006, 12, 11, 11, 23, 25, 95)
        Me.GfxSchedule2.ExtendedInfos = CWrapper1
        Me.GfxSchedule2.HeadlineBackcolor = System.Drawing.Color.AliceBlue
        Me.GfxSchedule2.HeadlineFont = New System.Drawing.Font("Arial", 8.0!)
        Me.GfxSchedule2.LeftsideBackcolor = System.Drawing.Color.AliceBlue
        Me.GfxSchedule2.Location = New System.Drawing.Point(2, 29)
        Me.GfxSchedule2.Margin = New System.Windows.Forms.Padding(0)
        Me.GfxSchedule2.MinuteHeights = 2.0!
        Me.GfxSchedule2.Name = "GfxSchedule2"
        Me.GfxSchedule2.ProgramBackcolor = System.Drawing.Color.Snow
        Me.GfxSchedule2.ProgramFont = New System.Drawing.Font("Arial", 6.0!)
        Me.GfxSchedule2.Size = New System.Drawing.Size(682, 247)
        Me.GfxSchedule2.StartDate = New Date(2006, 12, 11, 11, 23, 25, 95)
        Me.GfxSchedule2.TabIndex = 2
        Me.GfxSchedule2.tabThread = Nothing
        Me.GfxSchedule2.TopMaM = 360
        '
        'pbLast
        '
        Me.pbLast.Image = CType(resources.GetObject("pbLast.Image"), System.Drawing.Image)
        Me.pbLast.Location = New System.Drawing.Point(384, 8)
        Me.pbLast.Name = "pbLast"
        Me.pbLast.Size = New System.Drawing.Size(14, 14)
        Me.pbLast.TabIndex = 6
        Me.pbLast.TabStop = False
        Me.ToolTip1.SetToolTip(Me.pbLast, "Last")
        '
        'pbNext
        '
        Me.pbNext.Image = CType(resources.GetObject("pbNext.Image"), System.Drawing.Image)
        Me.pbNext.Location = New System.Drawing.Point(364, 8)
        Me.pbNext.Name = "pbNext"
        Me.pbNext.Size = New System.Drawing.Size(14, 14)
        Me.pbNext.TabIndex = 5
        Me.pbNext.TabStop = False
        Me.ToolTip1.SetToolTip(Me.pbNext, "Next")
        '
        'pbPrevious
        '
        Me.pbPrevious.Image = CType(resources.GetObject("pbPrevious.Image"), System.Drawing.Image)
        Me.pbPrevious.Location = New System.Drawing.Point(217, 8)
        Me.pbPrevious.Name = "pbPrevious"
        Me.pbPrevious.Size = New System.Drawing.Size(14, 14)
        Me.pbPrevious.TabIndex = 4
        Me.pbPrevious.TabStop = False
        Me.ToolTip1.SetToolTip(Me.pbPrevious, "Previous")
        '
        'pbFirst
        '
        Me.pbFirst.Image = CType(resources.GetObject("pbFirst.Image"), System.Drawing.Image)
        Me.pbFirst.Location = New System.Drawing.Point(197, 8)
        Me.pbFirst.Name = "pbFirst"
        Me.pbFirst.Size = New System.Drawing.Size(14, 14)
        Me.pbFirst.TabIndex = 3
        Me.pbFirst.TabStop = False
        Me.ToolTip1.SetToolTip(Me.pbFirst, "First")
        '
        'tabSpotlist
        '
        Me.tabSpotlist.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tabSpotlist.Controls.Add(Me.tpSpotlist)
        Me.tabSpotlist.Controls.Add(Me.tpCrossTab)
        Me.tabSpotlist.Location = New System.Drawing.Point(3, 3)
        Me.tabSpotlist.Name = "tabSpotlist"
        Me.tabSpotlist.SelectedIndex = 0
        Me.tabSpotlist.Size = New System.Drawing.Size(694, 307)
        Me.tabSpotlist.TabIndex = 0
        '
        'tpSpotlist
        '
        Me.tpSpotlist.Controls.Add(Me.grdSpotlist)
        Me.tpSpotlist.Controls.Add(Me.stpSpotlist)
        Me.tpSpotlist.Location = New System.Drawing.Point(4, 22)
        Me.tpSpotlist.Name = "tpSpotlist"
        Me.tpSpotlist.Padding = New System.Windows.Forms.Padding(3)
        Me.tpSpotlist.Size = New System.Drawing.Size(686, 281)
        Me.tpSpotlist.TabIndex = 0
        Me.tpSpotlist.Text = "Spotlist"
        Me.tpSpotlist.UseVisualStyleBackColor = True
        '
        'grdSpotlist
        '
        Me.grdSpotlist.AllowUserToAddRows = False
        Me.grdSpotlist.AllowUserToDeleteRows = False
        Me.grdSpotlist.AllowUserToOrderColumns = True
        Me.grdSpotlist.AllowUserToResizeRows = False
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.ControlLight
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.grdSpotlist.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle2
        Me.grdSpotlist.BackgroundColor = System.Drawing.Color.Silver
        Me.grdSpotlist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdSpotlist.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdSpotlist.Location = New System.Drawing.Point(3, 28)
        Me.grdSpotlist.Name = "grdSpotlist"
        Me.grdSpotlist.ReadOnly = True
        Me.grdSpotlist.RowHeadersVisible = False
        Me.grdSpotlist.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdSpotlist.Size = New System.Drawing.Size(680, 250)
        Me.grdSpotlist.TabIndex = 1
        Me.grdSpotlist.VirtualMode = True
        '
        'stpSpotlist
        '
        Me.stpSpotlist.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.stpSpotlist.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmdSpotlistFilter, Me.cmdSpotlistColumns, Me.cmdExcel, Me.ToolStripSeparator2, Me.cmdUpdatePrices, Me.cmdOptimize, Me.ToolStripSeparator1, Me.cmdSpotlistAV, Me.cmdFilm, Me.cmdNotes, Me.cmdSpotlistDelete, Me.ToolStripSeparator5, Me.cmdLoadOtherSpots, Me.cmdExportToAllocate, Me.ToolStripSeparator4, Me.cmdCheckForChanges, Me.cmdLock, Me.cmdReadK2Spotlist})
        Me.stpSpotlist.Location = New System.Drawing.Point(3, 3)
        Me.stpSpotlist.Name = "stpSpotlist"
        Me.stpSpotlist.Size = New System.Drawing.Size(680, 25)
        Me.stpSpotlist.TabIndex = 0
        Me.stpSpotlist.Text = "ToolStrip2"
        '
        'cmdSpotlistFilter
        '
        Me.cmdSpotlistFilter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdSpotlistFilter.Image = CType(resources.GetObject("cmdSpotlistFilter.Image"), System.Drawing.Image)
        Me.cmdSpotlistFilter.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdSpotlistFilter.Name = "cmdSpotlistFilter"
        Me.cmdSpotlistFilter.Size = New System.Drawing.Size(23, 22)
        Me.cmdSpotlistFilter.Text = "ToolStripButton4"
        Me.cmdSpotlistFilter.ToolTipText = "Filter spotlist"
        '
        'cmdSpotlistColumns
        '
        Me.cmdSpotlistColumns.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdSpotlistColumns.Image = CType(resources.GetObject("cmdSpotlistColumns.Image"), System.Drawing.Image)
        Me.cmdSpotlistColumns.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdSpotlistColumns.Name = "cmdSpotlistColumns"
        Me.cmdSpotlistColumns.Size = New System.Drawing.Size(23, 22)
        Me.cmdSpotlistColumns.Text = "ToolStripButton5"
        Me.cmdSpotlistColumns.ToolTipText = "Define columns for spotlist"
        '
        'cmdExcel
        '
        Me.cmdExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdExcel.Image = Global.clTrinity.My.Resources.Resources.excel_2
        Me.cmdExcel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdExcel.Name = "cmdExcel"
        Me.cmdExcel.Size = New System.Drawing.Size(23, 22)
        Me.cmdExcel.ToolTipText = "Export to excel"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'cmdUpdatePrices
        '
        Me.cmdUpdatePrices.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdUpdatePrices.Image = Global.clTrinity.My.Resources.Resources.money_bag_2
        Me.cmdUpdatePrices.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdUpdatePrices.Name = "cmdUpdatePrices"
        Me.cmdUpdatePrices.Size = New System.Drawing.Size(23, 22)
        Me.cmdUpdatePrices.Text = "ToolStripButton7"
        Me.cmdUpdatePrices.ToolTipText = "Update prices"
        '
        'cmdOptimize
        '
        Me.cmdOptimize.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdOptimize.Image = Global.clTrinity.My.Resources.Resources.change_film_2_20x20
        Me.cmdOptimize.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdOptimize.Name = "cmdOptimize"
        Me.cmdOptimize.Size = New System.Drawing.Size(23, 22)
        Me.cmdOptimize.Text = "ToolStripButton1"
        Me.cmdOptimize.ToolTipText = "Optimize filmsplit"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'cmdSpotlistAV
        '
        Me.cmdSpotlistAV.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdSpotlistAV.Image = Global.clTrinity.My.Resources.Resources.added_value_3_15x16
        Me.cmdSpotlistAV.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdSpotlistAV.Name = "cmdSpotlistAV"
        Me.cmdSpotlistAV.Size = New System.Drawing.Size(23, 22)
        Me.cmdSpotlistAV.Text = "ToolStripButton8"
        Me.cmdSpotlistAV.ToolTipText = "Change Added Values on spot"
        '
        'cmdFilm
        '
        Me.cmdFilm.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdFilm.Image = Global.clTrinity.My.Resources.Resources.film_3_small
        Me.cmdFilm.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdFilm.Name = "cmdFilm"
        Me.cmdFilm.Size = New System.Drawing.Size(23, 22)
        Me.cmdFilm.Text = "ToolStripButton9"
        Me.cmdFilm.ToolTipText = "Change film for spot"
        '
        'cmdNotes
        '
        Me.cmdNotes.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdNotes.Image = Global.clTrinity.My.Resources.Resources.notes_small_16x16
        Me.cmdNotes.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdNotes.Name = "cmdNotes"
        Me.cmdNotes.Size = New System.Drawing.Size(23, 22)
        Me.cmdNotes.Text = "ToolStripButton10"
        Me.cmdNotes.ToolTipText = "Change notes on spot"
        '
        'cmdSpotlistDelete
        '
        Me.cmdSpotlistDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdSpotlistDelete.Image = Global.clTrinity.My.Resources.Resources.delete_2_small
        Me.cmdSpotlistDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdSpotlistDelete.Name = "cmdSpotlistDelete"
        Me.cmdSpotlistDelete.Size = New System.Drawing.Size(23, 22)
        Me.cmdSpotlistDelete.Text = "ToolStripButton11"
        Me.cmdSpotlistDelete.ToolTipText = "Delete spot"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(6, 25)
        '
        'cmdLoadOtherSpots
        '
        Me.cmdLoadOtherSpots.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdLoadOtherSpots.Image = Global.clTrinity.My.Resources.Resources.open_2
        Me.cmdLoadOtherSpots.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdLoadOtherSpots.Name = "cmdLoadOtherSpots"
        Me.cmdLoadOtherSpots.Size = New System.Drawing.Size(23, 22)
        Me.cmdLoadOtherSpots.Text = "ToolStripButton1"
        Me.cmdLoadOtherSpots.ToolTipText = "Load Booked Spots from another Campaign"
        '
        'cmdExportToAllocate
        '
        Me.cmdExportToAllocate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdExportToAllocate.Image = Global.clTrinity.My.Resources.Resources.transfer_2_16x16
        Me.cmdExportToAllocate.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdExportToAllocate.Name = "cmdExportToAllocate"
        Me.cmdExportToAllocate.Size = New System.Drawing.Size(23, 22)
        Me.cmdExportToAllocate.Text = "ToolStripButton1"
        Me.cmdExportToAllocate.ToolTipText = "Export budget and ratings to Allocate"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 25)
        '
        'cmdCheckForChanges
        '
        Me.cmdCheckForChanges.Checked = True
        Me.cmdCheckForChanges.CheckOnClick = True
        Me.cmdCheckForChanges.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cmdCheckForChanges.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdCheckForChanges.Image = Global.clTrinity.My.Resources.Resources.attention_2
        Me.cmdCheckForChanges.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdCheckForChanges.Name = "cmdCheckForChanges"
        Me.cmdCheckForChanges.Size = New System.Drawing.Size(23, 22)
        Me.cmdCheckForChanges.Text = "ToolStripButton1"
        Me.cmdCheckForChanges.ToolTipText = "Check for changes to your booked spots"
        '
        'cmdLock
        '
        Me.cmdLock.CheckOnClick = True
        Me.cmdLock.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdLock.Image = Global.clTrinity.My.Resources.Resources.lock_2_16x16
        Me.cmdLock.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdLock.Name = "cmdLock"
        Me.cmdLock.Size = New System.Drawing.Size(23, 22)
        Me.cmdLock.Text = "ToolStripButton1"
        Me.cmdLock.ToolTipText = "Lock spot"
        '
        'cmdReadK2Spotlist
        '
        Me.cmdReadK2Spotlist.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdReadK2Spotlist.Image = CType(resources.GetObject("cmdReadK2Spotlist.Image"), System.Drawing.Image)
        Me.cmdReadK2Spotlist.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdReadK2Spotlist.Name = "cmdReadK2Spotlist"
        Me.cmdReadK2Spotlist.Size = New System.Drawing.Size(23, 22)
        Me.cmdReadK2Spotlist.Text = "ToolStripButton1"
        '
        'tpCrossTab
        '
        Me.tpCrossTab.Location = New System.Drawing.Point(4, 22)
        Me.tpCrossTab.Name = "tpCrossTab"
        Me.tpCrossTab.Padding = New System.Windows.Forms.Padding(3)
        Me.tpCrossTab.Size = New System.Drawing.Size(686, 281)
        Me.tpCrossTab.TabIndex = 1
        Me.tpCrossTab.Text = "Cross Tab"
        Me.tpCrossTab.UseVisualStyleBackColor = True
        '
        'mnuPanes
        '
        Me.mnuPanes.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PlannedToolStripMenuItem, Me.BookedToolStripMenuItem, Me.LeftToBookToolStripMenuItem, Me.FilmsToolStripMenuItem, Me.DaypartsToolStripMenuItem, Me.ReachToolStripMenuItem, Me.EstimatedTargetProfileToolStripMenuItem, Me.PictureToolStripMenuItem, Me.InfoToolStripMenuItem, Me.DetailsToolStripMenuItem, Me.ProgsInOtherChannelsToolStripMenuItem, Me.TrendToolStripMenuItem, Me.TargetProfileToolStripMenuItem, Me.PositionInBreakToolStripMenuItem})
        Me.mnuPanes.Name = "mnuPanes"
        Me.mnuPanes.ShowCheckMargin = True
        Me.mnuPanes.ShowImageMargin = False
        Me.mnuPanes.Size = New System.Drawing.Size(199, 312)
        '
        'PlannedToolStripMenuItem
        '
        Me.PlannedToolStripMenuItem.Checked = True
        Me.PlannedToolStripMenuItem.CheckOnClick = True
        Me.PlannedToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.PlannedToolStripMenuItem.Name = "PlannedToolStripMenuItem"
        Me.PlannedToolStripMenuItem.Size = New System.Drawing.Size(198, 22)
        Me.PlannedToolStripMenuItem.Text = "Planned"
        '
        'BookedToolStripMenuItem
        '
        Me.BookedToolStripMenuItem.Checked = True
        Me.BookedToolStripMenuItem.CheckOnClick = True
        Me.BookedToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.BookedToolStripMenuItem.Name = "BookedToolStripMenuItem"
        Me.BookedToolStripMenuItem.Size = New System.Drawing.Size(198, 22)
        Me.BookedToolStripMenuItem.Text = "Booked"
        '
        'LeftToBookToolStripMenuItem
        '
        Me.LeftToBookToolStripMenuItem.Checked = True
        Me.LeftToBookToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.LeftToBookToolStripMenuItem.Name = "LeftToBookToolStripMenuItem"
        Me.LeftToBookToolStripMenuItem.Size = New System.Drawing.Size(198, 22)
        Me.LeftToBookToolStripMenuItem.Text = "Left to book"
        '
        'FilmsToolStripMenuItem
        '
        Me.FilmsToolStripMenuItem.Checked = True
        Me.FilmsToolStripMenuItem.CheckOnClick = True
        Me.FilmsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.FilmsToolStripMenuItem.Name = "FilmsToolStripMenuItem"
        Me.FilmsToolStripMenuItem.Size = New System.Drawing.Size(198, 22)
        Me.FilmsToolStripMenuItem.Text = "Films"
        '
        'DaypartsToolStripMenuItem
        '
        Me.DaypartsToolStripMenuItem.Checked = True
        Me.DaypartsToolStripMenuItem.CheckOnClick = True
        Me.DaypartsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.DaypartsToolStripMenuItem.Name = "DaypartsToolStripMenuItem"
        Me.DaypartsToolStripMenuItem.Size = New System.Drawing.Size(198, 22)
        Me.DaypartsToolStripMenuItem.Text = "Dayparts"
        '
        'ReachToolStripMenuItem
        '
        Me.ReachToolStripMenuItem.Checked = True
        Me.ReachToolStripMenuItem.CheckOnClick = True
        Me.ReachToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ReachToolStripMenuItem.Name = "ReachToolStripMenuItem"
        Me.ReachToolStripMenuItem.Size = New System.Drawing.Size(198, 22)
        Me.ReachToolStripMenuItem.Text = "Reach"
        '
        'EstimatedTargetProfileToolStripMenuItem
        '
        Me.EstimatedTargetProfileToolStripMenuItem.Checked = True
        Me.EstimatedTargetProfileToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.EstimatedTargetProfileToolStripMenuItem.Name = "EstimatedTargetProfileToolStripMenuItem"
        Me.EstimatedTargetProfileToolStripMenuItem.Size = New System.Drawing.Size(198, 22)
        Me.EstimatedTargetProfileToolStripMenuItem.Text = "Estimated target profile"
        '
        'PictureToolStripMenuItem
        '
        Me.PictureToolStripMenuItem.Checked = True
        Me.PictureToolStripMenuItem.CheckOnClick = True
        Me.PictureToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.PictureToolStripMenuItem.Name = "PictureToolStripMenuItem"
        Me.PictureToolStripMenuItem.Size = New System.Drawing.Size(198, 22)
        Me.PictureToolStripMenuItem.Text = "Picture"
        '
        'InfoToolStripMenuItem
        '
        Me.InfoToolStripMenuItem.Checked = True
        Me.InfoToolStripMenuItem.CheckOnClick = True
        Me.InfoToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.InfoToolStripMenuItem.Name = "InfoToolStripMenuItem"
        Me.InfoToolStripMenuItem.Size = New System.Drawing.Size(198, 22)
        Me.InfoToolStripMenuItem.Text = "Info"
        '
        'DetailsToolStripMenuItem
        '
        Me.DetailsToolStripMenuItem.Checked = True
        Me.DetailsToolStripMenuItem.CheckOnClick = True
        Me.DetailsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.DetailsToolStripMenuItem.Name = "DetailsToolStripMenuItem"
        Me.DetailsToolStripMenuItem.Size = New System.Drawing.Size(198, 22)
        Me.DetailsToolStripMenuItem.Text = "Details"
        '
        'ProgsInOtherChannelsToolStripMenuItem
        '
        Me.ProgsInOtherChannelsToolStripMenuItem.Checked = True
        Me.ProgsInOtherChannelsToolStripMenuItem.CheckOnClick = True
        Me.ProgsInOtherChannelsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ProgsInOtherChannelsToolStripMenuItem.Name = "ProgsInOtherChannelsToolStripMenuItem"
        Me.ProgsInOtherChannelsToolStripMenuItem.Size = New System.Drawing.Size(198, 22)
        Me.ProgsInOtherChannelsToolStripMenuItem.Text = "Progs in other channels"
        '
        'TrendToolStripMenuItem
        '
        Me.TrendToolStripMenuItem.Checked = True
        Me.TrendToolStripMenuItem.CheckOnClick = True
        Me.TrendToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.TrendToolStripMenuItem.Name = "TrendToolStripMenuItem"
        Me.TrendToolStripMenuItem.Size = New System.Drawing.Size(198, 22)
        Me.TrendToolStripMenuItem.Text = "Trend"
        '
        'TargetProfileToolStripMenuItem
        '
        Me.TargetProfileToolStripMenuItem.Checked = True
        Me.TargetProfileToolStripMenuItem.CheckOnClick = True
        Me.TargetProfileToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.TargetProfileToolStripMenuItem.Name = "TargetProfileToolStripMenuItem"
        Me.TargetProfileToolStripMenuItem.Size = New System.Drawing.Size(198, 22)
        Me.TargetProfileToolStripMenuItem.Text = "Target profile"
        '
        'PositionInBreakToolStripMenuItem
        '
        Me.PositionInBreakToolStripMenuItem.Checked = True
        Me.PositionInBreakToolStripMenuItem.CheckOnClick = True
        Me.PositionInBreakToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.PositionInBreakToolStripMenuItem.Name = "PositionInBreakToolStripMenuItem"
        Me.PositionInBreakToolStripMenuItem.Size = New System.Drawing.Size(198, 22)
        Me.PositionInBreakToolStripMenuItem.Text = "Position in break"
        '
        'grpInfo
        '
        Me.grpInfo.ContextMenuStrip = Me.mnuPanes
        Me.grpInfo.Controls.Add(Me.pnlScore)
        Me.grpInfo.Controls.Add(Me.cmdImdb)
        Me.grpInfo.Controls.Add(Me.lblInfo)
        Me.grpInfo.Font = New System.Drawing.Font("Arial", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpInfo.Location = New System.Drawing.Point(3, 3)
        Me.grpInfo.Name = "grpInfo"
        Me.grpInfo.Size = New System.Drawing.Size(232, 76)
        Me.grpInfo.TabIndex = 28
        Me.grpInfo.TabStop = False
        Me.grpInfo.Text = "Info"
        '
        'pnlScore
        '
        Me.pnlScore.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlScore.ColumnCount = 10
        Me.pnlScore.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.pnlScore.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.pnlScore.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.pnlScore.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.pnlScore.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.pnlScore.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.pnlScore.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.pnlScore.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.pnlScore.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.pnlScore.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.pnlScore.Controls.Add(Me.PictureBox13, 9, 0)
        Me.pnlScore.Controls.Add(Me.PictureBox12, 8, 0)
        Me.pnlScore.Controls.Add(Me.PictureBox11, 7, 0)
        Me.pnlScore.Controls.Add(Me.PictureBox10, 6, 0)
        Me.pnlScore.Controls.Add(Me.PictureBox9, 5, 0)
        Me.pnlScore.Controls.Add(Me.PictureBox8, 4, 0)
        Me.pnlScore.Controls.Add(Me.PictureBox7, 3, 0)
        Me.pnlScore.Controls.Add(Me.PictureBox6, 2, 0)
        Me.pnlScore.Controls.Add(Me.PictureBox5, 1, 0)
        Me.pnlScore.Controls.Add(Me.PictureBox4, 0, 0)
        Me.pnlScore.Location = New System.Drawing.Point(10, 54)
        Me.pnlScore.Name = "pnlScore"
        Me.pnlScore.RowCount = 1
        Me.pnlScore.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.pnlScore.Size = New System.Drawing.Size(215, 17)
        Me.pnlScore.TabIndex = 2
        Me.pnlScore.Visible = False
        '
        'PictureBox13
        '
        Me.PictureBox13.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBox13.Image = Global.clTrinity.My.Resources.Resources.star_grey
        Me.PictureBox13.Location = New System.Drawing.Point(192, 3)
        Me.PictureBox13.Name = "PictureBox13"
        Me.PictureBox13.Size = New System.Drawing.Size(20, 11)
        Me.PictureBox13.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox13.TabIndex = 9
        Me.PictureBox13.TabStop = False
        '
        'PictureBox12
        '
        Me.PictureBox12.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBox12.Image = Global.clTrinity.My.Resources.Resources.star_grey
        Me.PictureBox12.Location = New System.Drawing.Point(171, 3)
        Me.PictureBox12.Name = "PictureBox12"
        Me.PictureBox12.Size = New System.Drawing.Size(15, 11)
        Me.PictureBox12.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox12.TabIndex = 8
        Me.PictureBox12.TabStop = False
        '
        'PictureBox11
        '
        Me.PictureBox11.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBox11.Image = Global.clTrinity.My.Resources.Resources.star_grey
        Me.PictureBox11.Location = New System.Drawing.Point(150, 3)
        Me.PictureBox11.Name = "PictureBox11"
        Me.PictureBox11.Size = New System.Drawing.Size(15, 11)
        Me.PictureBox11.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox11.TabIndex = 7
        Me.PictureBox11.TabStop = False
        '
        'PictureBox10
        '
        Me.PictureBox10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBox10.Image = Global.clTrinity.My.Resources.Resources.star_grey
        Me.PictureBox10.Location = New System.Drawing.Point(129, 3)
        Me.PictureBox10.Name = "PictureBox10"
        Me.PictureBox10.Size = New System.Drawing.Size(15, 11)
        Me.PictureBox10.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox10.TabIndex = 6
        Me.PictureBox10.TabStop = False
        '
        'PictureBox9
        '
        Me.PictureBox9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBox9.Image = Global.clTrinity.My.Resources.Resources.star_grey
        Me.PictureBox9.Location = New System.Drawing.Point(108, 3)
        Me.PictureBox9.Name = "PictureBox9"
        Me.PictureBox9.Size = New System.Drawing.Size(15, 11)
        Me.PictureBox9.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox9.TabIndex = 5
        Me.PictureBox9.TabStop = False
        '
        'PictureBox8
        '
        Me.PictureBox8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBox8.Image = Global.clTrinity.My.Resources.Resources.star_grey
        Me.PictureBox8.Location = New System.Drawing.Point(87, 3)
        Me.PictureBox8.Name = "PictureBox8"
        Me.PictureBox8.Size = New System.Drawing.Size(15, 11)
        Me.PictureBox8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox8.TabIndex = 4
        Me.PictureBox8.TabStop = False
        '
        'PictureBox7
        '
        Me.PictureBox7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBox7.Image = Global.clTrinity.My.Resources.Resources.star_grey
        Me.PictureBox7.Location = New System.Drawing.Point(66, 3)
        Me.PictureBox7.Name = "PictureBox7"
        Me.PictureBox7.Size = New System.Drawing.Size(15, 11)
        Me.PictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox7.TabIndex = 3
        Me.PictureBox7.TabStop = False
        '
        'PictureBox6
        '
        Me.PictureBox6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBox6.Image = Global.clTrinity.My.Resources.Resources.star_grey
        Me.PictureBox6.Location = New System.Drawing.Point(45, 3)
        Me.PictureBox6.Name = "PictureBox6"
        Me.PictureBox6.Size = New System.Drawing.Size(15, 11)
        Me.PictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox6.TabIndex = 2
        Me.PictureBox6.TabStop = False
        '
        'PictureBox5
        '
        Me.PictureBox5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBox5.Image = Global.clTrinity.My.Resources.Resources.star_grey
        Me.PictureBox5.Location = New System.Drawing.Point(24, 3)
        Me.PictureBox5.Name = "PictureBox5"
        Me.PictureBox5.Size = New System.Drawing.Size(15, 11)
        Me.PictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox5.TabIndex = 1
        Me.PictureBox5.TabStop = False
        '
        'PictureBox4
        '
        Me.PictureBox4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBox4.Image = Global.clTrinity.My.Resources.Resources.star_grey
        Me.PictureBox4.Location = New System.Drawing.Point(3, 3)
        Me.PictureBox4.Name = "PictureBox4"
        Me.PictureBox4.Size = New System.Drawing.Size(15, 11)
        Me.PictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox4.TabIndex = 0
        Me.PictureBox4.TabStop = False
        '
        'cmdImdb
        '
        Me.cmdImdb.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdImdb.Image = Global.clTrinity.My.Resources.Resources.imdb16x16
        Me.cmdImdb.Location = New System.Drawing.Point(200, 8)
        Me.cmdImdb.Name = "cmdImdb"
        Me.cmdImdb.Size = New System.Drawing.Size(25, 23)
        Me.cmdImdb.TabIndex = 1
        Me.cmdImdb.UseVisualStyleBackColor = True
        '
        'lblInfo
        '
        Me.lblInfo.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblInfo.AutoSize = True
        Me.lblInfo.Location = New System.Drawing.Point(8, 13)
        Me.lblInfo.Name = "lblInfo"
        Me.lblInfo.Size = New System.Drawing.Size(0, 12)
        Me.lblInfo.TabIndex = 0
        '
        'grpPicture
        '
        Me.grpPicture.ContextMenuStrip = Me.mnuPanes
        Me.grpPicture.Controls.Add(Me.picPicture)
        Me.grpPicture.Font = New System.Drawing.Font("Segoe UI", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpPicture.Location = New System.Drawing.Point(10, 123)
        Me.grpPicture.Name = "grpPicture"
        Me.grpPicture.Size = New System.Drawing.Size(225, 45)
        Me.grpPicture.TabIndex = 27
        Me.grpPicture.TabStop = False
        Me.grpPicture.Text = "Picture"
        '
        'picPicture
        '
        Me.picPicture.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.picPicture.Location = New System.Drawing.Point(10, 16)
        Me.picPicture.Name = "picPicture"
        Me.picPicture.Size = New System.Drawing.Size(209, 21)
        Me.picPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picPicture.TabIndex = 0
        Me.picPicture.TabStop = False
        '
        'grpLeftToBook
        '
        Me.grpLeftToBook.ContextMenuStrip = Me.mnuPanes
        Me.grpLeftToBook.Controls.Add(Me.grdLeftToBook)
        Me.grpLeftToBook.Controls.Add(Me.lblLeftNetHeadline)
        Me.grpLeftToBook.Controls.Add(Me.lblLeftBudgetHeadline)
        Me.grpLeftToBook.Controls.Add(Me.lblLeftPercentHeadline)
        Me.grpLeftToBook.Controls.Add(Me.lblLeftEstHeadline)
        Me.grpLeftToBook.Controls.Add(Me.lblLeftTRPHeadline)
        Me.grpLeftToBook.Controls.Add(Me.lblLeftChanHeadline)
        Me.grpLeftToBook.Font = New System.Drawing.Font("Arial", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpLeftToBook.Location = New System.Drawing.Point(10, 156)
        Me.grpLeftToBook.Name = "grpLeftToBook"
        Me.grpLeftToBook.Size = New System.Drawing.Size(225, 23)
        Me.grpLeftToBook.TabIndex = 26
        Me.grpLeftToBook.TabStop = False
        Me.grpLeftToBook.Text = "Left to book"
        '
        'grdLeftToBook
        '
        Me.grdLeftToBook.AllowUserToAddRows = False
        Me.grdLeftToBook.AllowUserToDeleteRows = False
        Me.grdLeftToBook.AllowUserToResizeColumns = False
        Me.grdLeftToBook.AllowUserToResizeRows = False
        Me.grdLeftToBook.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdLeftToBook.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdLeftToBook.ColumnHeadersVisible = False
        Me.grdLeftToBook.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn25, Me.DataGridViewTextBoxColumn26, Me.DataGridViewTextBoxColumn27, Me.DataGridViewTextBoxColumn28})
        Me.grdLeftToBook.ContextMenuStrip = Me.mnuPanes
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Arial", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdLeftToBook.DefaultCellStyle = DataGridViewCellStyle3
        Me.grdLeftToBook.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.grdLeftToBook.Location = New System.Drawing.Point(32, 45)
        Me.grdLeftToBook.Name = "grdLeftToBook"
        Me.grdLeftToBook.ReadOnly = True
        Me.grdLeftToBook.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken
        Me.grdLeftToBook.RowHeadersVisible = False
        Me.grdLeftToBook.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.grdLeftToBook.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.grdLeftToBook.ShowEditingIcon = False
        Me.grdLeftToBook.ShowRowErrors = False
        Me.grdLeftToBook.Size = New System.Drawing.Size(186, 63)
        Me.grdLeftToBook.TabIndex = 16
        '
        'DataGridViewTextBoxColumn25
        '
        Me.DataGridViewTextBoxColumn25.HeaderText = ""
        Me.DataGridViewTextBoxColumn25.Name = "DataGridViewTextBoxColumn25"
        Me.DataGridViewTextBoxColumn25.ReadOnly = True
        Me.DataGridViewTextBoxColumn25.Width = 40
        '
        'DataGridViewTextBoxColumn26
        '
        Me.DataGridViewTextBoxColumn26.HeaderText = ""
        Me.DataGridViewTextBoxColumn26.Name = "DataGridViewTextBoxColumn26"
        Me.DataGridViewTextBoxColumn26.ReadOnly = True
        Me.DataGridViewTextBoxColumn26.Width = 40
        '
        'DataGridViewTextBoxColumn27
        '
        Me.DataGridViewTextBoxColumn27.HeaderText = ""
        Me.DataGridViewTextBoxColumn27.Name = "DataGridViewTextBoxColumn27"
        Me.DataGridViewTextBoxColumn27.ReadOnly = True
        Me.DataGridViewTextBoxColumn27.Width = 40
        '
        'DataGridViewTextBoxColumn28
        '
        Me.DataGridViewTextBoxColumn28.HeaderText = ""
        Me.DataGridViewTextBoxColumn28.Name = "DataGridViewTextBoxColumn28"
        Me.DataGridViewTextBoxColumn28.ReadOnly = True
        Me.DataGridViewTextBoxColumn28.Width = 50
        '
        'lblLeftNetHeadline
        '
        Me.lblLeftNetHeadline.ContextMenuStrip = Me.mnuPanes
        Me.lblLeftNetHeadline.Font = New System.Drawing.Font("Arial", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLeftNetHeadline.Location = New System.Drawing.Point(148, 28)
        Me.lblLeftNetHeadline.Name = "lblLeftNetHeadline"
        Me.lblLeftNetHeadline.Size = New System.Drawing.Size(45, 15)
        Me.lblLeftNetHeadline.TabIndex = 15
        Me.lblLeftNetHeadline.Text = "Net"
        Me.lblLeftNetHeadline.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblLeftBudgetHeadline
        '
        Me.lblLeftBudgetHeadline.ContextMenuStrip = Me.mnuPanes
        Me.lblLeftBudgetHeadline.Font = New System.Drawing.Font("Segoe UI", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLeftBudgetHeadline.Location = New System.Drawing.Point(110, 13)
        Me.lblLeftBudgetHeadline.Name = "lblLeftBudgetHeadline"
        Me.lblLeftBudgetHeadline.Size = New System.Drawing.Size(72, 15)
        Me.lblLeftBudgetHeadline.TabIndex = 14
        Me.lblLeftBudgetHeadline.Text = "Budget"
        Me.lblLeftBudgetHeadline.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblLeftPercentHeadline
        '
        Me.lblLeftPercentHeadline.ContextMenuStrip = Me.mnuPanes
        Me.lblLeftPercentHeadline.Font = New System.Drawing.Font("Arial", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLeftPercentHeadline.Location = New System.Drawing.Point(110, 28)
        Me.lblLeftPercentHeadline.Name = "lblLeftPercentHeadline"
        Me.lblLeftPercentHeadline.Size = New System.Drawing.Size(34, 15)
        Me.lblLeftPercentHeadline.TabIndex = 13
        Me.lblLeftPercentHeadline.Text = "%"
        Me.lblLeftPercentHeadline.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblLeftEstHeadline
        '
        Me.lblLeftEstHeadline.ContextMenuStrip = Me.mnuPanes
        Me.lblLeftEstHeadline.Font = New System.Drawing.Font("Arial", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLeftEstHeadline.Location = New System.Drawing.Point(63, 28)
        Me.lblLeftEstHeadline.Name = "lblLeftEstHeadline"
        Me.lblLeftEstHeadline.Size = New System.Drawing.Size(41, 15)
        Me.lblLeftEstHeadline.TabIndex = 12
        Me.lblLeftEstHeadline.Text = "Est"
        Me.lblLeftEstHeadline.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblLeftTRPHeadline
        '
        Me.lblLeftTRPHeadline.ContextMenuStrip = Me.mnuPanes
        Me.lblLeftTRPHeadline.Font = New System.Drawing.Font("Segoe UI", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLeftTRPHeadline.Location = New System.Drawing.Point(32, 13)
        Me.lblLeftTRPHeadline.Name = "lblLeftTRPHeadline"
        Me.lblLeftTRPHeadline.Size = New System.Drawing.Size(72, 15)
        Me.lblLeftTRPHeadline.TabIndex = 11
        Me.lblLeftTRPHeadline.Text = "TRP"
        Me.lblLeftTRPHeadline.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblLeftChanHeadline
        '
        Me.lblLeftChanHeadline.ContextMenuStrip = Me.mnuPanes
        Me.lblLeftChanHeadline.Font = New System.Drawing.Font("Arial", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLeftChanHeadline.Location = New System.Drawing.Point(32, 28)
        Me.lblLeftChanHeadline.Name = "lblLeftChanHeadline"
        Me.lblLeftChanHeadline.Size = New System.Drawing.Size(34, 15)
        Me.lblLeftChanHeadline.TabIndex = 10
        Me.lblLeftChanHeadline.Text = "Chan"
        Me.lblLeftChanHeadline.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'grpPeak
        '
        Me.grpPeak.ContextMenuStrip = Me.mnuPanes
        Me.grpPeak.Controls.Add(Me.chtPrimePeak)
        Me.grpPeak.Font = New System.Drawing.Font("Segoe UI", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpPeak.Location = New System.Drawing.Point(10, 229)
        Me.grpPeak.Name = "grpPeak"
        Me.grpPeak.Size = New System.Drawing.Size(225, 19)
        Me.grpPeak.TabIndex = 25
        Me.grpPeak.TabStop = False
        Me.grpPeak.Text = "Prime profile"
        '
        'chtPrimePeak
        '
        Me.chtPrimePeak.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chtPrimePeak.BackColor = System.Drawing.SystemColors.HighlightText
        Me.chtPrimePeak.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.chtPrimePeak.Cursor = System.Windows.Forms.Cursors.Default
        Me.chtPrimePeak.Location = New System.Drawing.Point(6, 16)
        Me.chtPrimePeak.Name = "chtPrimePeak"
        Me.chtPrimePeak.OffPeak = New Decimal(New Integer() {0, 0, 0, 0})
        Me.chtPrimePeak.Peak = New Decimal(New Integer() {0, 0, 0, 0})
        Me.chtPrimePeak.Size = New System.Drawing.Size(213, 65)
        Me.chtPrimePeak.TabIndex = 0
        Me.chtPrimePeak.Text = "PrimePeakChart1"
        '
        'grpEstProfile
        '
        Me.grpEstProfile.ContextMenuStrip = Me.mnuPanes
        Me.grpEstProfile.Controls.Add(Me.chtEstProfile)
        Me.grpEstProfile.Font = New System.Drawing.Font("Segoe UI", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpEstProfile.Location = New System.Drawing.Point(10, 292)
        Me.grpEstProfile.Name = "grpEstProfile"
        Me.grpEstProfile.Size = New System.Drawing.Size(225, 86)
        Me.grpEstProfile.TabIndex = 24
        Me.grpEstProfile.TabStop = False
        Me.grpEstProfile.Text = "Estimated target profile on campaign"
        '
        'chtEstProfile
        '
        Me.chtEstProfile.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chtEstProfile.AverageRating = 0!
        Me.chtEstProfile.BackColor = System.Drawing.Color.White
        Me.chtEstProfile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.chtEstProfile.ContextMenuStrip = Me.mnuPanes
        Me.chtEstProfile.Cursor = System.Windows.Forms.Cursors.WaitCursor
        Me.chtEstProfile.Font = New System.Drawing.Font("Arial", 6.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chtEstProfile.Location = New System.Drawing.Point(6, 16)
        Me.chtEstProfile.Name = "chtEstProfile"
        Me.chtEstProfile.ShowAverageRating = False
        Me.chtEstProfile.Size = New System.Drawing.Size(212, 63)
        Me.chtEstProfile.TabIndex = 1
        Me.chtEstProfile.Target = Nothing
        Me.chtEstProfile.Text = "ProfileChart1"
        '
        'grpGender
        '
        Me.grpGender.ContextMenuStrip = Me.mnuPanes
        Me.grpGender.Controls.Add(Me.chtGender)
        Me.grpGender.Font = New System.Drawing.Font("Arial", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpGender.Location = New System.Drawing.Point(12, 570)
        Me.grpGender.Name = "grpGender"
        Me.grpGender.Size = New System.Drawing.Size(225, 86)
        Me.grpGender.TabIndex = 23
        Me.grpGender.TabStop = False
        Me.grpGender.Text = "Gender profile"
        '
        'chtGender
        '
        Me.chtGender.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chtGender.BackColor = System.Drawing.Color.White
        Me.chtGender.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.chtGender.Cursor = System.Windows.Forms.Cursors.Default
        Me.chtGender.Location = New System.Drawing.Point(8, 16)
        Me.chtGender.Men = New Decimal(New Integer() {0, 0, 0, 0})
        Me.chtGender.Name = "chtGender"
        Me.chtGender.Size = New System.Drawing.Size(210, 64)
        Me.chtGender.TabIndex = 0
        Me.chtGender.Text = "GenderChart1"
        Me.chtGender.Women = New Decimal(New Integer() {0, 0, 0, 0})
        '
        'grpPIB
        '
        Me.grpPIB.ContextMenuStrip = Me.mnuPanes
        Me.grpPIB.Controls.Add(Me.chtPIB)
        Me.grpPIB.Font = New System.Drawing.Font("Segoe UI", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpPIB.Location = New System.Drawing.Point(12, 662)
        Me.grpPIB.Name = "grpPIB"
        Me.grpPIB.Size = New System.Drawing.Size(225, 86)
        Me.grpPIB.TabIndex = 10
        Me.grpPIB.TabStop = False
        Me.grpPIB.Text = "Position in break"
        '
        'chtPIB
        '
        Me.chtPIB.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chtPIB.Average = 0!
        Me.chtPIB.BackColor = System.Drawing.Color.White
        Me.chtPIB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.chtPIB.ContextMenuStrip = Me.mnuPanes
        Me.chtPIB.Cursor = System.Windows.Forms.Cursors.Default
        Me.chtPIB.First = 0!
        Me.chtPIB.Last = 0!
        Me.chtPIB.Location = New System.Drawing.Point(8, 18)
        Me.chtPIB.Name = "chtPIB"
        Me.chtPIB.Size = New System.Drawing.Size(210, 63)
        Me.chtPIB.TabIndex = 0
        Me.chtPIB.Text = "PibChart1"
        '
        'grpDayparts
        '
        Me.grpDayparts.ContextMenuStrip = Me.mnuPanes
        Me.grpDayparts.Controls.Add(Me.grdDayparts)
        Me.grpDayparts.Font = New System.Drawing.Font("Segoe UI", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpDayparts.Location = New System.Drawing.Point(10, 219)
        Me.grpDayparts.Name = "grpDayparts"
        Me.grpDayparts.Size = New System.Drawing.Size(225, 9)
        Me.grpDayparts.TabIndex = 22
        Me.grpDayparts.TabStop = False
        Me.grpDayparts.Text = "Dayparts"
        '
        'grdDayparts
        '
        Me.grdDayparts.AllowUserToAddRows = False
        Me.grdDayparts.AllowUserToDeleteRows = False
        Me.grdDayparts.AllowUserToResizeColumns = False
        Me.grdDayparts.AllowUserToResizeRows = False
        Me.grdDayparts.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdDayparts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdDayparts.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colDaypart, Me.colPlannedDP, Me.colBookedDP, Me.colNetDP})
        Me.grdDayparts.ContextMenuStrip = Me.mnuPanes
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Segoe UI", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdDayparts.DefaultCellStyle = DataGridViewCellStyle8
        Me.grdDayparts.Location = New System.Drawing.Point(6, 16)
        Me.grdDayparts.Name = "grdDayparts"
        Me.grdDayparts.ReadOnly = True
        Me.grdDayparts.RowHeadersVisible = False
        Me.grdDayparts.Size = New System.Drawing.Size(213, 19)
        Me.grdDayparts.TabIndex = 0
        Me.grdDayparts.VirtualMode = True
        '
        'colDaypart
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.colDaypart.DefaultCellStyle = DataGridViewCellStyle4
        Me.colDaypart.Frozen = True
        Me.colDaypart.HeaderText = "Daypart"
        Me.colDaypart.Name = "colDaypart"
        Me.colDaypart.ReadOnly = True
        Me.colDaypart.Width = 50
        '
        'colPlannedDP
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle5.Format = "N1"
        DataGridViewCellStyle5.NullValue = Nothing
        Me.colPlannedDP.DefaultCellStyle = DataGridViewCellStyle5
        Me.colPlannedDP.Frozen = True
        Me.colPlannedDP.HeaderText = "Planned"
        Me.colPlannedDP.Name = "colPlannedDP"
        Me.colPlannedDP.ReadOnly = True
        Me.colPlannedDP.Width = 50
        '
        'colBookedDP
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle6.Format = "N1"
        Me.colBookedDP.DefaultCellStyle = DataGridViewCellStyle6
        Me.colBookedDP.Frozen = True
        Me.colBookedDP.HeaderText = "Booked"
        Me.colBookedDP.Name = "colBookedDP"
        Me.colBookedDP.ReadOnly = True
        Me.colBookedDP.Width = 50
        '
        'colNetDP
        '
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle7.Format = "N0"
        Me.colNetDP.DefaultCellStyle = DataGridViewCellStyle7
        Me.colNetDP.Frozen = True
        Me.colNetDP.HeaderText = "Net"
        Me.colNetDP.Name = "colNetDP"
        Me.colNetDP.ReadOnly = True
        Me.colNetDP.Width = 50
        '
        'grpPlannedTRP
        '
        Me.grpPlannedTRP.ContextMenuStrip = Me.mnuPanes
        Me.grpPlannedTRP.Controls.Add(Me.lblNetHeadline)
        Me.grpPlannedTRP.Controls.Add(Me.lblBudgetHeadline)
        Me.grpPlannedTRP.Controls.Add(Me.lblPercentHeadline)
        Me.grpPlannedTRP.Controls.Add(Me.lblEstHeadline)
        Me.grpPlannedTRP.Controls.Add(Me.lblTRPHeadline)
        Me.grpPlannedTRP.Controls.Add(Me.lblChanHeadline)
        Me.grpPlannedTRP.Controls.Add(Me.grdPlannedTRP)
        Me.grpPlannedTRP.Font = New System.Drawing.Font("Arial", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpPlannedTRP.Location = New System.Drawing.Point(10, 123)
        Me.grpPlannedTRP.Name = "grpPlannedTRP"
        Me.grpPlannedTRP.Size = New System.Drawing.Size(225, 11)
        Me.grpPlannedTRP.TabIndex = 14
        Me.grpPlannedTRP.TabStop = False
        Me.grpPlannedTRP.Text = "Planned"
        '
        'lblNetHeadline
        '
        Me.lblNetHeadline.ContextMenuStrip = Me.mnuPanes
        Me.lblNetHeadline.Font = New System.Drawing.Font("Arial", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNetHeadline.Location = New System.Drawing.Point(148, 28)
        Me.lblNetHeadline.Name = "lblNetHeadline"
        Me.lblNetHeadline.Size = New System.Drawing.Size(45, 15)
        Me.lblNetHeadline.TabIndex = 15
        Me.lblNetHeadline.Text = "Net"
        Me.lblNetHeadline.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblBudgetHeadline
        '
        Me.lblBudgetHeadline.ContextMenuStrip = Me.mnuPanes
        Me.lblBudgetHeadline.Font = New System.Drawing.Font("Arial", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBudgetHeadline.Location = New System.Drawing.Point(110, 13)
        Me.lblBudgetHeadline.Name = "lblBudgetHeadline"
        Me.lblBudgetHeadline.Size = New System.Drawing.Size(72, 15)
        Me.lblBudgetHeadline.TabIndex = 14
        Me.lblBudgetHeadline.Text = "Budget"
        Me.lblBudgetHeadline.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblPercentHeadline
        '
        Me.lblPercentHeadline.ContextMenuStrip = Me.mnuPanes
        Me.lblPercentHeadline.Font = New System.Drawing.Font("Arial", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPercentHeadline.Location = New System.Drawing.Point(110, 28)
        Me.lblPercentHeadline.Name = "lblPercentHeadline"
        Me.lblPercentHeadline.Size = New System.Drawing.Size(34, 15)
        Me.lblPercentHeadline.TabIndex = 13
        Me.lblPercentHeadline.Text = "%"
        Me.lblPercentHeadline.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblEstHeadline
        '
        Me.lblEstHeadline.ContextMenuStrip = Me.mnuPanes
        Me.lblEstHeadline.Font = New System.Drawing.Font("Arial", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEstHeadline.Location = New System.Drawing.Point(63, 28)
        Me.lblEstHeadline.Name = "lblEstHeadline"
        Me.lblEstHeadline.Size = New System.Drawing.Size(41, 15)
        Me.lblEstHeadline.TabIndex = 12
        Me.lblEstHeadline.Text = "Est"
        Me.lblEstHeadline.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblTRPHeadline
        '
        Me.lblTRPHeadline.ContextMenuStrip = Me.mnuPanes
        Me.lblTRPHeadline.Font = New System.Drawing.Font("Arial", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTRPHeadline.Location = New System.Drawing.Point(32, 13)
        Me.lblTRPHeadline.Name = "lblTRPHeadline"
        Me.lblTRPHeadline.Size = New System.Drawing.Size(72, 15)
        Me.lblTRPHeadline.TabIndex = 11
        Me.lblTRPHeadline.Text = "TRP"
        Me.lblTRPHeadline.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblChanHeadline
        '
        Me.lblChanHeadline.ContextMenuStrip = Me.mnuPanes
        Me.lblChanHeadline.Font = New System.Drawing.Font("Arial", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChanHeadline.Location = New System.Drawing.Point(32, 28)
        Me.lblChanHeadline.Name = "lblChanHeadline"
        Me.lblChanHeadline.Size = New System.Drawing.Size(34, 15)
        Me.lblChanHeadline.TabIndex = 10
        Me.lblChanHeadline.Text = "Chan"
        Me.lblChanHeadline.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'grdPlannedTRP
        '
        Me.grdPlannedTRP.AllowUserToAddRows = False
        Me.grdPlannedTRP.AllowUserToDeleteRows = False
        Me.grdPlannedTRP.AllowUserToResizeColumns = False
        Me.grdPlannedTRP.AllowUserToResizeRows = False
        Me.grdPlannedTRP.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdPlannedTRP.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdPlannedTRP.ColumnHeadersVisible = False
        Me.grdPlannedTRP.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colPlannedChan, Me.colPlannedEst, Me.colPlannedPercent, Me.colPlannedNet})
        Me.grdPlannedTRP.ContextMenuStrip = Me.mnuPanes
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle9.Font = New System.Drawing.Font("Arial", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdPlannedTRP.DefaultCellStyle = DataGridViewCellStyle9
        Me.grdPlannedTRP.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.grdPlannedTRP.Location = New System.Drawing.Point(32, 43)
        Me.grdPlannedTRP.Name = "grdPlannedTRP"
        Me.grdPlannedTRP.ReadOnly = True
        Me.grdPlannedTRP.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken
        Me.grdPlannedTRP.RowHeadersVisible = False
        Me.grdPlannedTRP.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.grdPlannedTRP.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.grdPlannedTRP.ShowEditingIcon = False
        Me.grdPlannedTRP.ShowRowErrors = False
        Me.grdPlannedTRP.Size = New System.Drawing.Size(187, 63)
        Me.grdPlannedTRP.TabIndex = 9
        '
        'colPlannedChan
        '
        Me.colPlannedChan.HeaderText = ""
        Me.colPlannedChan.Name = "colPlannedChan"
        Me.colPlannedChan.ReadOnly = True
        Me.colPlannedChan.Width = 40
        '
        'colPlannedEst
        '
        Me.colPlannedEst.HeaderText = ""
        Me.colPlannedEst.Name = "colPlannedEst"
        Me.colPlannedEst.ReadOnly = True
        Me.colPlannedEst.Width = 40
        '
        'colPlannedPercent
        '
        Me.colPlannedPercent.HeaderText = ""
        Me.colPlannedPercent.Name = "colPlannedPercent"
        Me.colPlannedPercent.ReadOnly = True
        Me.colPlannedPercent.Width = 40
        '
        'colPlannedNet
        '
        Me.colPlannedNet.HeaderText = ""
        Me.colPlannedNet.Name = "colPlannedNet"
        Me.colPlannedNet.ReadOnly = True
        Me.colPlannedNet.Width = 50
        '
        'grpBookedTRP
        '
        Me.grpBookedTRP.ContextMenuStrip = Me.mnuPanes
        Me.grpBookedTRP.Controls.Add(Me.grdBookedTRP)
        Me.grpBookedTRP.Controls.Add(Me.lblBookedNetHeadline)
        Me.grpBookedTRP.Controls.Add(Me.lblBookedBudgetHeadline)
        Me.grpBookedTRP.Controls.Add(Me.lblBookedPercentHeadline)
        Me.grpBookedTRP.Controls.Add(Me.lblBookedEstHeadline)
        Me.grpBookedTRP.Controls.Add(Me.lblBookedTRPHeadline)
        Me.grpBookedTRP.Controls.Add(Me.lblBookedChanHeadline)
        Me.grpBookedTRP.Font = New System.Drawing.Font("Arial", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpBookedTRP.Location = New System.Drawing.Point(10, 139)
        Me.grpBookedTRP.Name = "grpBookedTRP"
        Me.grpBookedTRP.Size = New System.Drawing.Size(225, 11)
        Me.grpBookedTRP.TabIndex = 15
        Me.grpBookedTRP.TabStop = False
        Me.grpBookedTRP.Text = "Booked"
        '
        'grdBookedTRP
        '
        Me.grdBookedTRP.AllowUserToAddRows = False
        Me.grdBookedTRP.AllowUserToDeleteRows = False
        Me.grdBookedTRP.AllowUserToResizeColumns = False
        Me.grdBookedTRP.AllowUserToResizeRows = False
        Me.grdBookedTRP.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdBookedTRP.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdBookedTRP.ColumnHeadersVisible = False
        Me.grdBookedTRP.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colBookedChan, Me.colBookedEst, Me.colBookedPercent, Me.colBookedNet})
        Me.grdBookedTRP.ContextMenuStrip = Me.mnuPanes
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle10.Font = New System.Drawing.Font("Arial", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdBookedTRP.DefaultCellStyle = DataGridViewCellStyle10
        Me.grdBookedTRP.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.grdBookedTRP.Location = New System.Drawing.Point(32, 45)
        Me.grdBookedTRP.Name = "grdBookedTRP"
        Me.grdBookedTRP.ReadOnly = True
        Me.grdBookedTRP.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken
        Me.grdBookedTRP.RowHeadersVisible = False
        Me.grdBookedTRP.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.grdBookedTRP.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.grdBookedTRP.ShowEditingIcon = False
        Me.grdBookedTRP.ShowRowErrors = False
        Me.grdBookedTRP.Size = New System.Drawing.Size(186, 63)
        Me.grdBookedTRP.TabIndex = 16
        '
        'colBookedChan
        '
        Me.colBookedChan.HeaderText = ""
        Me.colBookedChan.Name = "colBookedChan"
        Me.colBookedChan.ReadOnly = True
        Me.colBookedChan.Width = 40
        '
        'colBookedEst
        '
        Me.colBookedEst.HeaderText = ""
        Me.colBookedEst.Name = "colBookedEst"
        Me.colBookedEst.ReadOnly = True
        Me.colBookedEst.Width = 40
        '
        'colBookedPercent
        '
        Me.colBookedPercent.HeaderText = ""
        Me.colBookedPercent.Name = "colBookedPercent"
        Me.colBookedPercent.ReadOnly = True
        Me.colBookedPercent.Width = 40
        '
        'colBookedNet
        '
        Me.colBookedNet.HeaderText = ""
        Me.colBookedNet.Name = "colBookedNet"
        Me.colBookedNet.ReadOnly = True
        Me.colBookedNet.Width = 50
        '
        'lblBookedNetHeadline
        '
        Me.lblBookedNetHeadline.ContextMenuStrip = Me.mnuPanes
        Me.lblBookedNetHeadline.Font = New System.Drawing.Font("Arial", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBookedNetHeadline.Location = New System.Drawing.Point(148, 28)
        Me.lblBookedNetHeadline.Name = "lblBookedNetHeadline"
        Me.lblBookedNetHeadline.Size = New System.Drawing.Size(45, 15)
        Me.lblBookedNetHeadline.TabIndex = 15
        Me.lblBookedNetHeadline.Text = "Net"
        Me.lblBookedNetHeadline.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblBookedBudgetHeadline
        '
        Me.lblBookedBudgetHeadline.ContextMenuStrip = Me.mnuPanes
        Me.lblBookedBudgetHeadline.Font = New System.Drawing.Font("Arial", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBookedBudgetHeadline.Location = New System.Drawing.Point(110, 13)
        Me.lblBookedBudgetHeadline.Name = "lblBookedBudgetHeadline"
        Me.lblBookedBudgetHeadline.Size = New System.Drawing.Size(72, 15)
        Me.lblBookedBudgetHeadline.TabIndex = 14
        Me.lblBookedBudgetHeadline.Text = "Budget"
        Me.lblBookedBudgetHeadline.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblBookedPercentHeadline
        '
        Me.lblBookedPercentHeadline.ContextMenuStrip = Me.mnuPanes
        Me.lblBookedPercentHeadline.Font = New System.Drawing.Font("Arial", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBookedPercentHeadline.Location = New System.Drawing.Point(110, 28)
        Me.lblBookedPercentHeadline.Name = "lblBookedPercentHeadline"
        Me.lblBookedPercentHeadline.Size = New System.Drawing.Size(34, 15)
        Me.lblBookedPercentHeadline.TabIndex = 13
        Me.lblBookedPercentHeadline.Text = "%"
        Me.lblBookedPercentHeadline.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblBookedEstHeadline
        '
        Me.lblBookedEstHeadline.ContextMenuStrip = Me.mnuPanes
        Me.lblBookedEstHeadline.Font = New System.Drawing.Font("Arial", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBookedEstHeadline.Location = New System.Drawing.Point(63, 28)
        Me.lblBookedEstHeadline.Name = "lblBookedEstHeadline"
        Me.lblBookedEstHeadline.Size = New System.Drawing.Size(41, 15)
        Me.lblBookedEstHeadline.TabIndex = 12
        Me.lblBookedEstHeadline.Text = "Est"
        Me.lblBookedEstHeadline.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblBookedTRPHeadline
        '
        Me.lblBookedTRPHeadline.ContextMenuStrip = Me.mnuPanes
        Me.lblBookedTRPHeadline.Font = New System.Drawing.Font("Arial", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBookedTRPHeadline.Location = New System.Drawing.Point(32, 13)
        Me.lblBookedTRPHeadline.Name = "lblBookedTRPHeadline"
        Me.lblBookedTRPHeadline.Size = New System.Drawing.Size(72, 15)
        Me.lblBookedTRPHeadline.TabIndex = 11
        Me.lblBookedTRPHeadline.Text = "TRP"
        Me.lblBookedTRPHeadline.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblBookedChanHeadline
        '
        Me.lblBookedChanHeadline.ContextMenuStrip = Me.mnuPanes
        Me.lblBookedChanHeadline.Font = New System.Drawing.Font("Arial", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBookedChanHeadline.Location = New System.Drawing.Point(32, 28)
        Me.lblBookedChanHeadline.Name = "lblBookedChanHeadline"
        Me.lblBookedChanHeadline.Size = New System.Drawing.Size(34, 15)
        Me.lblBookedChanHeadline.TabIndex = 10
        Me.lblBookedChanHeadline.Text = "Chan"
        Me.lblBookedChanHeadline.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'grpFilms
        '
        Me.grpFilms.ContextMenuStrip = Me.mnuPanes
        Me.grpFilms.Controls.Add(Me.grdFilms)
        Me.grpFilms.Font = New System.Drawing.Font("Segoe UI", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpFilms.Location = New System.Drawing.Point(10, 251)
        Me.grpFilms.Name = "grpFilms"
        Me.grpFilms.Size = New System.Drawing.Size(225, 12)
        Me.grpFilms.TabIndex = 21
        Me.grpFilms.TabStop = False
        Me.grpFilms.Text = "Films"
        '
        'grdFilms
        '
        Me.grdFilms.AllowUserToAddRows = False
        Me.grdFilms.AllowUserToDeleteRows = False
        Me.grdFilms.AllowUserToResizeColumns = False
        Me.grdFilms.AllowUserToResizeRows = False
        Me.grdFilms.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdFilms.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdFilms.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colFilm, Me.colPlanned, Me.colPlanPerc, Me.colBooked, Me.colBookedPerc, Me.colNet})
        DataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle16.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle16.Font = New System.Drawing.Font("Segoe UI", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle16.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle16.SelectionBackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle16.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdFilms.DefaultCellStyle = DataGridViewCellStyle16
        Me.grdFilms.Location = New System.Drawing.Point(6, 16)
        Me.grdFilms.Name = "grdFilms"
        Me.grdFilms.ReadOnly = True
        Me.grdFilms.RowHeadersVisible = False
        Me.grdFilms.Size = New System.Drawing.Size(215, 34)
        Me.grdFilms.TabIndex = 0
        Me.grdFilms.VirtualMode = True
        '
        'colFilm
        '
        Me.colFilm.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colFilm.HeaderText = "Film"
        Me.colFilm.MinimumWidth = 50
        Me.colFilm.Name = "colFilm"
        Me.colFilm.ReadOnly = True
        '
        'colPlanned
        '
        Me.colPlanned.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.colPlanned.DefaultCellStyle = DataGridViewCellStyle11
        Me.colPlanned.HeaderText = "Planned"
        Me.colPlanned.Name = "colPlanned"
        Me.colPlanned.ReadOnly = True
        Me.colPlanned.Width = 40
        '
        'colPlanPerc
        '
        Me.colPlanPerc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.colPlanPerc.DefaultCellStyle = DataGridViewCellStyle12
        Me.colPlanPerc.HeaderText = "%"
        Me.colPlanPerc.Name = "colPlanPerc"
        Me.colPlanPerc.ReadOnly = True
        Me.colPlanPerc.Width = 35
        '
        'colBooked
        '
        Me.colBooked.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        DataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.colBooked.DefaultCellStyle = DataGridViewCellStyle13
        Me.colBooked.HeaderText = "Booked"
        Me.colBooked.Name = "colBooked"
        Me.colBooked.ReadOnly = True
        Me.colBooked.Width = 40
        '
        'colBookedPerc
        '
        Me.colBookedPerc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        DataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.colBookedPerc.DefaultCellStyle = DataGridViewCellStyle14
        Me.colBookedPerc.HeaderText = "%"
        Me.colBookedPerc.Name = "colBookedPerc"
        Me.colBookedPerc.ReadOnly = True
        Me.colBookedPerc.Width = 35
        '
        'colNet
        '
        Me.colNet.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        DataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.colNet.DefaultCellStyle = DataGridViewCellStyle15
        Me.colNet.HeaderText = "Net"
        Me.colNet.Name = "colNet"
        Me.colNet.ReadOnly = True
        Me.colNet.Width = 40
        '
        'grpProfile
        '
        Me.grpProfile.ContextMenuStrip = Me.mnuPanes
        Me.grpProfile.Controls.Add(Me.chtProfile)
        Me.grpProfile.Font = New System.Drawing.Font("Segoe UI", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpProfile.Location = New System.Drawing.Point(10, 478)
        Me.grpProfile.Name = "grpProfile"
        Me.grpProfile.Size = New System.Drawing.Size(225, 86)
        Me.grpProfile.TabIndex = 20
        Me.grpProfile.TabStop = False
        Me.grpProfile.Text = "Target profile for selected prog"
        '
        'chtProfile
        '
        Me.chtProfile.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chtProfile.AverageRating = 0!
        Me.chtProfile.BackColor = System.Drawing.Color.White
        Me.chtProfile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.chtProfile.ContextMenuStrip = Me.mnuPanes
        Me.chtProfile.Cursor = System.Windows.Forms.Cursors.WaitCursor
        Me.chtProfile.Font = New System.Drawing.Font("Arial", 6.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chtProfile.Location = New System.Drawing.Point(6, 18)
        Me.chtProfile.Name = "chtProfile"
        Me.chtProfile.ShowAverageRating = False
        Me.chtProfile.Size = New System.Drawing.Size(212, 63)
        Me.chtProfile.TabIndex = 0
        Me.chtProfile.Target = Nothing
        Me.chtProfile.Text = "ProfileChart1"
        '
        'grpTrend
        '
        Me.grpTrend.ContextMenuStrip = Me.mnuPanes
        Me.grpTrend.Controls.Add(Me.chtTrend)
        Me.grpTrend.Font = New System.Drawing.Font("Segoe UI", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpTrend.Location = New System.Drawing.Point(10, 386)
        Me.grpTrend.Name = "grpTrend"
        Me.grpTrend.Size = New System.Drawing.Size(225, 86)
        Me.grpTrend.TabIndex = 19
        Me.grpTrend.TabStop = False
        Me.grpTrend.Text = "Trend"
        '
        'chtTrend
        '
        Me.chtTrend.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chtTrend.BackColor = System.Drawing.Color.White
        Me.chtTrend.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.chtTrend.ContextMenuStrip = Me.mnuPanes
        Me.chtTrend.Cursor = System.Windows.Forms.Cursors.WaitCursor
        Me.chtTrend.ExtendedInfo = Nothing
        Me.chtTrend.Location = New System.Drawing.Point(8, 18)
        Me.chtTrend.Name = "chtTrend"
        Me.chtTrend.Period = Nothing
        Me.chtTrend.Size = New System.Drawing.Size(210, 63)
        Me.chtTrend.TabIndex = 0
        Me.chtTrend.Text = "TrendChart1"
        '
        'grpOther
        '
        Me.grpOther.ContextMenuStrip = Me.mnuPanes
        Me.grpOther.Controls.Add(Me.grdOther)
        Me.grpOther.Font = New System.Drawing.Font("Segoe UI", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpOther.Location = New System.Drawing.Point(10, 200)
        Me.grpOther.Name = "grpOther"
        Me.grpOther.Size = New System.Drawing.Size(225, 14)
        Me.grpOther.TabIndex = 18
        Me.grpOther.TabStop = False
        Me.grpOther.Text = "Progs in other channels"
        '
        'grdOther
        '
        Me.grdOther.AllowUserToAddRows = False
        Me.grdOther.AllowUserToDeleteRows = False
        Me.grdOther.AllowUserToResizeColumns = False
        Me.grdOther.AllowUserToResizeRows = False
        Me.grdOther.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdOther.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdOther.ContextMenuStrip = Me.mnuPanes
        DataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle17.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle17.Font = New System.Drawing.Font("Segoe UI", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle17.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle17.SelectionBackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdOther.DefaultCellStyle = DataGridViewCellStyle17
        Me.grdOther.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.grdOther.Location = New System.Drawing.Point(6, 16)
        Me.grdOther.Name = "grdOther"
        Me.grdOther.ReadOnly = True
        Me.grdOther.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken
        Me.grdOther.RowHeadersVisible = False
        Me.grdOther.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.grdOther.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.grdOther.ShowEditingIcon = False
        Me.grdOther.ShowRowErrors = False
        Me.grdOther.Size = New System.Drawing.Size(212, 91)
        Me.grdOther.TabIndex = 10
        '
        'grpDetails
        '
        Me.grpDetails.ContextMenuStrip = Me.mnuPanes
        Me.grpDetails.Controls.Add(Me.grdDetails)
        Me.grpDetails.Font = New System.Drawing.Font("Segoe UI", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpDetails.Location = New System.Drawing.Point(10, 182)
        Me.grpDetails.Name = "grpDetails"
        Me.grpDetails.Size = New System.Drawing.Size(225, 12)
        Me.grpDetails.TabIndex = 17
        Me.grpDetails.TabStop = False
        Me.grpDetails.Text = "Details"
        '
        'grdDetails
        '
        Me.grdDetails.AllowUserToAddRows = False
        Me.grdDetails.AllowUserToDeleteRows = False
        Me.grdDetails.AllowUserToResizeColumns = False
        Me.grdDetails.AllowUserToResizeRows = False
        Me.grdDetails.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdDetails.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colDate, Me.colTime, Me.colProgBefore, Me.colProgAfter, Me.colTarget, Me.colBuyTarget})
        Me.grdDetails.ContextMenuStrip = Me.mnuPanes
        DataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle18.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle18.Font = New System.Drawing.Font("Segoe UI", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle18.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle18.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        DataGridViewCellStyle18.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdDetails.DefaultCellStyle = DataGridViewCellStyle18
        Me.grdDetails.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.grdDetails.Location = New System.Drawing.Point(4, 16)
        Me.grdDetails.Name = "grdDetails"
        Me.grdDetails.ReadOnly = True
        Me.grdDetails.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken
        Me.grdDetails.RowHeadersVisible = False
        Me.grdDetails.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.grdDetails.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.grdDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdDetails.ShowEditingIcon = False
        Me.grdDetails.ShowRowErrors = False
        Me.grdDetails.Size = New System.Drawing.Size(217, 91)
        Me.grdDetails.TabIndex = 10
        '
        'colDate
        '
        Me.colDate.HeaderText = "Date"
        Me.colDate.Name = "colDate"
        Me.colDate.ReadOnly = True
        Me.colDate.Width = 50
        '
        'colTime
        '
        Me.colTime.HeaderText = "Time"
        Me.colTime.Name = "colTime"
        Me.colTime.ReadOnly = True
        Me.colTime.Width = 30
        '
        'colProgBefore
        '
        Me.colProgBefore.HeaderText = "Prog Before"
        Me.colProgBefore.Name = "colProgBefore"
        Me.colProgBefore.ReadOnly = True
        Me.colProgBefore.Width = 80
        '
        'colProgAfter
        '
        Me.colProgAfter.FillWeight = 80.0!
        Me.colProgAfter.HeaderText = "Prog After"
        Me.colProgAfter.Name = "colProgAfter"
        Me.colProgAfter.ReadOnly = True
        Me.colProgAfter.Width = 80
        '
        'colTarget
        '
        Me.colTarget.HeaderText = "W25-44"
        Me.colTarget.Name = "colTarget"
        Me.colTarget.ReadOnly = True
        Me.colTarget.Width = 40
        '
        'colBuyTarget
        '
        Me.colBuyTarget.HeaderText = "A12-59"
        Me.colBuyTarget.Name = "colBuyTarget"
        Me.colBuyTarget.ReadOnly = True
        Me.colBuyTarget.Width = 40
        '
        'grpReach
        '
        Me.grpReach.ContextMenuStrip = Me.mnuPanes
        Me.grpReach.Controls.Add(Me.grdReach)
        Me.grpReach.Font = New System.Drawing.Font("Segoe UI", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpReach.Location = New System.Drawing.Point(10, 266)
        Me.grpReach.Name = "grpReach"
        Me.grpReach.Size = New System.Drawing.Size(225, 18)
        Me.grpReach.TabIndex = 16
        Me.grpReach.TabStop = False
        Me.grpReach.Text = "Reach"
        '
        'grdReach
        '
        Me.grdReach.AllowUserToAddRows = False
        Me.grdReach.AllowUserToDeleteRows = False
        Me.grdReach.AllowUserToResizeColumns = False
        Me.grdReach.AllowUserToResizeRows = False
        Me.grdReach.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdReach.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdReach.ContextMenuStrip = Me.mnuPanes
        DataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle19.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle19.Font = New System.Drawing.Font("Segoe UI", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle19.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle19.SelectionBackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle19.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle19.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdReach.DefaultCellStyle = DataGridViewCellStyle19
        Me.grdReach.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.grdReach.Location = New System.Drawing.Point(34, 16)
        Me.grdReach.Name = "grdReach"
        Me.grdReach.ReadOnly = True
        Me.grdReach.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken
        Me.grdReach.RowHeadersVisible = False
        Me.grdReach.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.grdReach.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.grdReach.ShowEditingIcon = False
        Me.grdReach.ShowRowErrors = False
        Me.grdReach.Size = New System.Drawing.Size(185, 91)
        Me.grdReach.TabIndex = 10
        '
        'grpGeneral
        '
        Me.grpGeneral.ContextMenuStrip = Me.mnuPanes
        Me.grpGeneral.Controls.Add(Me.cmbFilm)
        Me.grpGeneral.Controls.Add(Me.PictureBox3)
        Me.grpGeneral.Controls.Add(Me.cmbChannel)
        Me.grpGeneral.Controls.Add(Me.PictureBox2)
        Me.grpGeneral.Controls.Add(Me.cmbDatabase)
        Me.grpGeneral.Controls.Add(Me.PictureBox1)
        Me.grpGeneral.Location = New System.Drawing.Point(10, 11)
        Me.grpGeneral.Name = "grpGeneral"
        Me.grpGeneral.Size = New System.Drawing.Size(225, 106)
        Me.grpGeneral.TabIndex = 13
        Me.grpGeneral.TabStop = False
        Me.grpGeneral.Text = "General"
        '
        'cmbFilm
        '
        Me.cmbFilm.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbFilm.ContextMenuStrip = Me.mnuPanes
        Me.cmbFilm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFilm.FormattingEnabled = True
        Me.cmbFilm.Location = New System.Drawing.Point(32, 73)
        Me.cmbFilm.Name = "cmbFilm"
        Me.cmbFilm.Size = New System.Drawing.Size(187, 21)
        Me.cmbFilm.TabIndex = 5
        '
        'PictureBox3
        '
        Me.PictureBox3.ContextMenuStrip = Me.mnuPanes
        Me.PictureBox3.Image = Global.clTrinity.My.Resources.Resources.film_3_small
        Me.PictureBox3.Location = New System.Drawing.Point(6, 73)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(20, 20)
        Me.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox3.TabIndex = 4
        Me.PictureBox3.TabStop = False
        '
        'cmbChannel
        '
        Me.cmbChannel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbChannel.ContextMenuStrip = Me.mnuPanes
        Me.cmbChannel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbChannel.FormattingEnabled = True
        Me.cmbChannel.Location = New System.Drawing.Point(32, 46)
        Me.cmbChannel.Name = "cmbChannel"
        Me.cmbChannel.Size = New System.Drawing.Size(187, 21)
        Me.cmbChannel.TabIndex = 3
        '
        'PictureBox2
        '
        Me.PictureBox2.ContextMenuStrip = Me.mnuPanes
        Me.PictureBox2.Image = Global.clTrinity.My.Resources.Resources.monitor_3
        Me.PictureBox2.Location = New System.Drawing.Point(6, 46)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(20, 21)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox2.TabIndex = 2
        Me.PictureBox2.TabStop = False
        '
        'cmbDatabase
        '
        Me.cmbDatabase.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbDatabase.ContextMenuStrip = Me.mnuPanes
        Me.cmbDatabase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDatabase.FormattingEnabled = True
        Me.cmbDatabase.Items.AddRange(New Object() {"Database", "Avail..."})
        Me.cmbDatabase.Location = New System.Drawing.Point(32, 19)
        Me.cmbDatabase.Name = "cmbDatabase"
        Me.cmbDatabase.Size = New System.Drawing.Size(187, 21)
        Me.cmbDatabase.TabIndex = 1
        '
        'PictureBox1
        '
        Me.PictureBox1.ContextMenuStrip = Me.mnuPanes
        Me.PictureBox1.Image = Global.clTrinity.My.Resources.Resources.db_2_18x24
        Me.PictureBox1.Location = New System.Drawing.Point(6, 19)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(20, 21)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'tmrFilter
        '
        Me.tmrFilter.Interval = 1500
        '
        'DataGridViewTextBoxColumn1
        '
        DataGridViewCellStyle20.Format = "Short Date"
        DataGridViewCellStyle20.NullValue = Nothing
        Me.DataGridViewTextBoxColumn1.DefaultCellStyle = DataGridViewCellStyle20
        Me.DataGridViewTextBoxColumn1.Frozen = True
        Me.DataGridViewTextBoxColumn1.HeaderText = ""
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.Width = 40
        '
        'DataGridViewTextBoxColumn2
        '
        DataGridViewCellStyle21.Format = "N1"
        Me.DataGridViewTextBoxColumn2.DefaultCellStyle = DataGridViewCellStyle21
        Me.DataGridViewTextBoxColumn2.Frozen = True
        Me.DataGridViewTextBoxColumn2.HeaderText = ""
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        Me.DataGridViewTextBoxColumn2.Width = 40
        '
        'DataGridViewTextBoxColumn3
        '
        DataGridViewCellStyle22.Format = "N1"
        Me.DataGridViewTextBoxColumn3.DefaultCellStyle = DataGridViewCellStyle22
        Me.DataGridViewTextBoxColumn3.Frozen = True
        Me.DataGridViewTextBoxColumn3.HeaderText = ""
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        Me.DataGridViewTextBoxColumn3.Width = 40
        '
        'DataGridViewTextBoxColumn4
        '
        DataGridViewCellStyle23.Format = "N1"
        Me.DataGridViewTextBoxColumn4.DefaultCellStyle = DataGridViewCellStyle23
        Me.DataGridViewTextBoxColumn4.Frozen = True
        Me.DataGridViewTextBoxColumn4.HeaderText = ""
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        Me.DataGridViewTextBoxColumn4.Width = 40
        '
        'DataGridViewTextBoxColumn5
        '
        DataGridViewCellStyle24.Format = "N1"
        Me.DataGridViewTextBoxColumn5.DefaultCellStyle = DataGridViewCellStyle24
        Me.DataGridViewTextBoxColumn5.HeaderText = ""
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = True
        Me.DataGridViewTextBoxColumn5.Width = 40
        '
        'DataGridViewTextBoxColumn6
        '
        DataGridViewCellStyle25.Format = "N1"
        Me.DataGridViewTextBoxColumn6.DefaultCellStyle = DataGridViewCellStyle25
        Me.DataGridViewTextBoxColumn6.HeaderText = ""
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.ReadOnly = True
        Me.DataGridViewTextBoxColumn6.Width = 40
        '
        'DataGridViewTextBoxColumn7
        '
        DataGridViewCellStyle26.Format = "N1"
        Me.DataGridViewTextBoxColumn7.DefaultCellStyle = DataGridViewCellStyle26
        Me.DataGridViewTextBoxColumn7.HeaderText = ""
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        Me.DataGridViewTextBoxColumn7.ReadOnly = True
        Me.DataGridViewTextBoxColumn7.Width = 40
        '
        'DataGridViewTextBoxColumn8
        '
        DataGridViewCellStyle27.Format = "N0"
        Me.DataGridViewTextBoxColumn8.DefaultCellStyle = DataGridViewCellStyle27
        Me.DataGridViewTextBoxColumn8.HeaderText = ""
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        Me.DataGridViewTextBoxColumn8.ReadOnly = True
        Me.DataGridViewTextBoxColumn8.Width = 50
        '
        'DataGridViewTextBoxColumn9
        '
        DataGridViewCellStyle28.Format = "Short Date"
        DataGridViewCellStyle28.NullValue = Nothing
        Me.DataGridViewTextBoxColumn9.DefaultCellStyle = DataGridViewCellStyle28
        Me.DataGridViewTextBoxColumn9.Frozen = True
        Me.DataGridViewTextBoxColumn9.HeaderText = "Date"
        Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
        Me.DataGridViewTextBoxColumn9.ReadOnly = True
        Me.DataGridViewTextBoxColumn9.Width = 50
        '
        'DataGridViewTextBoxColumn10
        '
        DataGridViewCellStyle29.Format = "Short Date"
        DataGridViewCellStyle29.NullValue = Nothing
        Me.DataGridViewTextBoxColumn10.DefaultCellStyle = DataGridViewCellStyle29
        Me.DataGridViewTextBoxColumn10.Frozen = True
        Me.DataGridViewTextBoxColumn10.HeaderText = "Date"
        Me.DataGridViewTextBoxColumn10.Name = "DataGridViewTextBoxColumn10"
        Me.DataGridViewTextBoxColumn10.ReadOnly = True
        Me.DataGridViewTextBoxColumn10.Width = 50
        '
        'DataGridViewTextBoxColumn11
        '
        DataGridViewCellStyle30.Format = "N1"
        Me.DataGridViewTextBoxColumn11.DefaultCellStyle = DataGridViewCellStyle30
        Me.DataGridViewTextBoxColumn11.Frozen = True
        Me.DataGridViewTextBoxColumn11.HeaderText = "Time"
        Me.DataGridViewTextBoxColumn11.Name = "DataGridViewTextBoxColumn11"
        Me.DataGridViewTextBoxColumn11.ReadOnly = True
        Me.DataGridViewTextBoxColumn11.Width = 30
        '
        'DataGridViewTextBoxColumn12
        '
        DataGridViewCellStyle31.Format = "N1"
        Me.DataGridViewTextBoxColumn12.DefaultCellStyle = DataGridViewCellStyle31
        Me.DataGridViewTextBoxColumn12.Frozen = True
        Me.DataGridViewTextBoxColumn12.HeaderText = ""
        Me.DataGridViewTextBoxColumn12.Name = "DataGridViewTextBoxColumn12"
        Me.DataGridViewTextBoxColumn12.ReadOnly = True
        Me.DataGridViewTextBoxColumn12.Width = 40
        '
        'DataGridViewTextBoxColumn13
        '
        Me.DataGridViewTextBoxColumn13.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        DataGridViewCellStyle32.Format = "N1"
        Me.DataGridViewTextBoxColumn13.DefaultCellStyle = DataGridViewCellStyle32
        Me.DataGridViewTextBoxColumn13.Frozen = True
        Me.DataGridViewTextBoxColumn13.HeaderText = ""
        Me.DataGridViewTextBoxColumn13.MinimumWidth = 30
        Me.DataGridViewTextBoxColumn13.Name = "DataGridViewTextBoxColumn13"
        Me.DataGridViewTextBoxColumn13.ReadOnly = True
        '
        'DataGridViewTextBoxColumn14
        '
        Me.DataGridViewTextBoxColumn14.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        DataGridViewCellStyle33.Format = "N0"
        Me.DataGridViewTextBoxColumn14.DefaultCellStyle = DataGridViewCellStyle33
        Me.DataGridViewTextBoxColumn14.Frozen = True
        Me.DataGridViewTextBoxColumn14.HeaderText = ""
        Me.DataGridViewTextBoxColumn14.Name = "DataGridViewTextBoxColumn14"
        Me.DataGridViewTextBoxColumn14.ReadOnly = True
        Me.DataGridViewTextBoxColumn14.Width = 50
        '
        'DataGridViewTextBoxColumn15
        '
        Me.DataGridViewTextBoxColumn15.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        DataGridViewCellStyle34.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewTextBoxColumn15.DefaultCellStyle = DataGridViewCellStyle34
        Me.DataGridViewTextBoxColumn15.Frozen = True
        Me.DataGridViewTextBoxColumn15.HeaderText = ""
        Me.DataGridViewTextBoxColumn15.Name = "DataGridViewTextBoxColumn15"
        Me.DataGridViewTextBoxColumn15.ReadOnly = True
        Me.DataGridViewTextBoxColumn15.Width = 40
        '
        'DataGridViewTextBoxColumn16
        '
        Me.DataGridViewTextBoxColumn16.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        DataGridViewCellStyle35.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewTextBoxColumn16.DefaultCellStyle = DataGridViewCellStyle35
        Me.DataGridViewTextBoxColumn16.Frozen = True
        Me.DataGridViewTextBoxColumn16.HeaderText = ""
        Me.DataGridViewTextBoxColumn16.Name = "DataGridViewTextBoxColumn16"
        Me.DataGridViewTextBoxColumn16.ReadOnly = True
        Me.DataGridViewTextBoxColumn16.Width = 40
        '
        'DataGridViewTextBoxColumn17
        '
        Me.DataGridViewTextBoxColumn17.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        DataGridViewCellStyle36.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewTextBoxColumn17.DefaultCellStyle = DataGridViewCellStyle36
        Me.DataGridViewTextBoxColumn17.HeaderText = ""
        Me.DataGridViewTextBoxColumn17.Name = "DataGridViewTextBoxColumn17"
        Me.DataGridViewTextBoxColumn17.ReadOnly = True
        Me.DataGridViewTextBoxColumn17.Width = 40
        '
        'DataGridViewTextBoxColumn18
        '
        Me.DataGridViewTextBoxColumn18.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        DataGridViewCellStyle37.Format = "N1"
        Me.DataGridViewTextBoxColumn18.DefaultCellStyle = DataGridViewCellStyle37
        Me.DataGridViewTextBoxColumn18.HeaderText = ""
        Me.DataGridViewTextBoxColumn18.Name = "DataGridViewTextBoxColumn18"
        Me.DataGridViewTextBoxColumn18.ReadOnly = True
        Me.DataGridViewTextBoxColumn18.Width = 50
        '
        'DataGridViewTextBoxColumn19
        '
        Me.DataGridViewTextBoxColumn19.HeaderText = ""
        Me.DataGridViewTextBoxColumn19.Name = "DataGridViewTextBoxColumn19"
        Me.DataGridViewTextBoxColumn19.ReadOnly = True
        Me.DataGridViewTextBoxColumn19.Width = 40
        '
        'DataGridViewTextBoxColumn20
        '
        Me.DataGridViewTextBoxColumn20.HeaderText = ""
        Me.DataGridViewTextBoxColumn20.Name = "DataGridViewTextBoxColumn20"
        Me.DataGridViewTextBoxColumn20.ReadOnly = True
        Me.DataGridViewTextBoxColumn20.Width = 40
        '
        'DataGridViewTextBoxColumn21
        '
        Me.DataGridViewTextBoxColumn21.FillWeight = 80.0!
        Me.DataGridViewTextBoxColumn21.HeaderText = ""
        Me.DataGridViewTextBoxColumn21.Name = "DataGridViewTextBoxColumn21"
        Me.DataGridViewTextBoxColumn21.ReadOnly = True
        Me.DataGridViewTextBoxColumn21.Width = 40
        '
        'DataGridViewTextBoxColumn22
        '
        DataGridViewCellStyle38.Format = "N0"
        Me.DataGridViewTextBoxColumn22.DefaultCellStyle = DataGridViewCellStyle38
        Me.DataGridViewTextBoxColumn22.FillWeight = 80.0!
        Me.DataGridViewTextBoxColumn22.HeaderText = ""
        Me.DataGridViewTextBoxColumn22.Name = "DataGridViewTextBoxColumn22"
        Me.DataGridViewTextBoxColumn22.ReadOnly = True
        Me.DataGridViewTextBoxColumn22.Width = 50
        '
        'DataGridViewTextBoxColumn23
        '
        Me.DataGridViewTextBoxColumn23.HeaderText = "A12-59"
        Me.DataGridViewTextBoxColumn23.Name = "DataGridViewTextBoxColumn23"
        Me.DataGridViewTextBoxColumn23.ReadOnly = True
        Me.DataGridViewTextBoxColumn23.Width = 40
        '
        'DataGridViewTextBoxColumn24
        '
        Me.DataGridViewTextBoxColumn24.HeaderText = "A12-59"
        Me.DataGridViewTextBoxColumn24.Name = "DataGridViewTextBoxColumn24"
        Me.DataGridViewTextBoxColumn24.ReadOnly = True
        Me.DataGridViewTextBoxColumn24.Width = 40
        '
        'colEstTime
        '
        Me.colEstTime.HeaderText = "Time"
        Me.colEstTime.Name = "colEstTime"
        Me.colEstTime.ReadOnly = True
        Me.colEstTime.Width = 30
        '
        'colEstProgAfter
        '
        Me.colEstProgAfter.HeaderText = "Prog After"
        Me.colEstProgAfter.Name = "colEstProgAfter"
        Me.colEstProgAfter.ReadOnly = True
        Me.colEstProgAfter.Width = 75
        '
        'colEstMainTarget
        '
        DataGridViewCellStyle39.Format = "N1"
        Me.colEstMainTarget.DefaultCellStyle = DataGridViewCellStyle39
        Me.colEstMainTarget.HeaderText = "A25-54"
        Me.colEstMainTarget.Name = "colEstMainTarget"
        Me.colEstMainTarget.ReadOnly = True
        Me.colEstMainTarget.Width = 40
        '
        'colEstChanTarget
        '
        DataGridViewCellStyle40.Format = "N1"
        Me.colEstChanTarget.DefaultCellStyle = DataGridViewCellStyle40
        Me.colEstChanTarget.HeaderText = "A12-59"
        Me.colEstChanTarget.Name = "colEstChanTarget"
        Me.colEstChanTarget.ReadOnly = true
        Me.colEstChanTarget.Width = 40
        '
        'colChan
        '
        Me.colChan.HeaderText = "Chan"
        Me.colChan.Name = "colChan"
        Me.colChan.ReadOnly = true
        Me.colChan.Width = 40
        '
        'colProgram
        '
        Me.colProgram.HeaderText = "Programme"
        Me.colProgram.Name = "colProgram"
        Me.colProgram.ReadOnly = true
        '
        'Button1
        '
        Me.Button1.Image = Global.clTrinity.My.Resources.Resources.imdb16x16
        Me.Button1.Location = New System.Drawing.Point(196, 26)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(29, 27)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = true
        '
        'frmBooking
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(969, 630)
        Me.Controls.Add(Me.sptBooking)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.Name = "frmBooking"
        Me.Text = "Booking"
        Me.sptBooking.Panel1.ResumeLayout(false)
        Me.sptBooking.Panel2.ResumeLayout(false)
        Me.sptBooking.ResumeLayout(false)
        Me.sptSpotlist.Panel1.ResumeLayout(false)
        Me.sptSpotlist.Panel2.ResumeLayout(false)
        Me.sptSpotlist.ResumeLayout(false)
        Me.TabSchedule.ResumeLayout(false)
        Me.tpNumeric.ResumeLayout(false)
        Me.tpNumeric.PerformLayout
        Me.stpSchedule.ResumeLayout(false)
        Me.stpSchedule.PerformLayout
        CType(Me.imgOn,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.imgOff,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.grdSchedule,System.ComponentModel.ISupportInitialize).EndInit
        Me.tpGfxSchedule.ResumeLayout(false)
        CType(Me.pbLast,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.pbNext,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.pbPrevious,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.pbFirst,System.ComponentModel.ISupportInitialize).EndInit
        Me.tabSpotlist.ResumeLayout(false)
        Me.tpSpotlist.ResumeLayout(false)
        Me.tpSpotlist.PerformLayout
        CType(Me.grdSpotlist,System.ComponentModel.ISupportInitialize).EndInit
        Me.stpSpotlist.ResumeLayout(false)
        Me.stpSpotlist.PerformLayout
        Me.mnuPanes.ResumeLayout(false)
        Me.grpInfo.ResumeLayout(false)
        Me.grpInfo.PerformLayout
        Me.pnlScore.ResumeLayout(false)
        CType(Me.PictureBox13,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.PictureBox12,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.PictureBox11,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.PictureBox10,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.PictureBox9,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.PictureBox8,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.PictureBox7,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.PictureBox6,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.PictureBox5,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.PictureBox4,System.ComponentModel.ISupportInitialize).EndInit
        Me.grpPicture.ResumeLayout(false)
        CType(Me.picPicture,System.ComponentModel.ISupportInitialize).EndInit
        Me.grpLeftToBook.ResumeLayout(false)
        CType(Me.grdLeftToBook,System.ComponentModel.ISupportInitialize).EndInit
        Me.grpPeak.ResumeLayout(false)
        Me.grpEstProfile.ResumeLayout(false)
        Me.grpGender.ResumeLayout(false)
        Me.grpPIB.ResumeLayout(false)
        Me.grpDayparts.ResumeLayout(false)
        CType(Me.grdDayparts,System.ComponentModel.ISupportInitialize).EndInit
        Me.grpPlannedTRP.ResumeLayout(false)
        CType(Me.grdPlannedTRP,System.ComponentModel.ISupportInitialize).EndInit
        Me.grpBookedTRP.ResumeLayout(false)
        CType(Me.grdBookedTRP,System.ComponentModel.ISupportInitialize).EndInit
        Me.grpFilms.ResumeLayout(false)
        CType(Me.grdFilms,System.ComponentModel.ISupportInitialize).EndInit
        Me.grpProfile.ResumeLayout(false)
        Me.grpTrend.ResumeLayout(false)
        Me.grpOther.ResumeLayout(false)
        CType(Me.grdOther,System.ComponentModel.ISupportInitialize).EndInit
        Me.grpDetails.ResumeLayout(false)
        CType(Me.grdDetails,System.ComponentModel.ISupportInitialize).EndInit
        Me.grpReach.ResumeLayout(false)
        CType(Me.grdReach,System.ComponentModel.ISupportInitialize).EndInit
        Me.grpGeneral.ResumeLayout(false)
        CType(Me.PictureBox3,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.PictureBox2,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.PictureBox1,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)

End Sub
    Friend WithEvents sptBooking As System.Windows.Forms.SplitContainer
    Friend WithEvents sptSpotlist As System.Windows.Forms.SplitContainer
    Friend WithEvents tabSpotlist As System.Windows.Forms.TabControl
    Friend WithEvents tpSpotlist As System.Windows.Forms.TabPage
    Friend WithEvents tpCrossTab As System.Windows.Forms.TabPage
    Friend WithEvents imgOn As System.Windows.Forms.PictureBox
    Friend WithEvents imgOff As System.Windows.Forms.PictureBox
    Friend WithEvents stpSpotlist As System.Windows.Forms.ToolStrip
    Friend WithEvents cmdSpotlistFilter As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmdSpotlistColumns As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmdExcel As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmdUpdatePrices As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmdSpotlistAV As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmdFilm As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmdNotes As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmdSpotlistDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents grdSpotlist As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colEstTime As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colEstProgAfter As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colEstMainTarget As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colEstChanTarget As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn10 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn11 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colChan As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colProgram As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents grpPIB As System.Windows.Forms.GroupBox
    Friend WithEvents chtPIB As clTrinity.PIBChart
    Friend WithEvents DataGridViewTextBoxColumn12 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn13 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn14 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn15 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn16 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn17 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn18 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents DataGridViewTextBoxColumn19 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn20 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn21 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn22 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents mnuPanes As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents BookedToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FilmsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DaypartsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ReachToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DetailsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ProgsInOtherChannelsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TrendToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TargetProfileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PlannedToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PositionInBreakToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents grpDayparts As System.Windows.Forms.GroupBox
    Friend WithEvents grdDayparts As System.Windows.Forms.DataGridView
    Friend WithEvents colDaypart As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colPlannedDP As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colBookedDP As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colNetDP As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents grpPlannedTRP As System.Windows.Forms.GroupBox
    Friend WithEvents lblNetHeadline As System.Windows.Forms.Label
    Friend WithEvents lblBudgetHeadline As System.Windows.Forms.Label
    Friend WithEvents lblPercentHeadline As System.Windows.Forms.Label
    Friend WithEvents lblEstHeadline As System.Windows.Forms.Label
    Friend WithEvents lblTRPHeadline As System.Windows.Forms.Label
    Friend WithEvents lblChanHeadline As System.Windows.Forms.Label
    Friend WithEvents grdPlannedTRP As System.Windows.Forms.DataGridView
    Friend WithEvents colPlannedChan As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colPlannedEst As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colPlannedPercent As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colPlannedNet As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colBookedChan As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colBookedEst As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colBookedPercent As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colBookedNet As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents grpBookedTRP As System.Windows.Forms.GroupBox
    Friend WithEvents grdBookedTRP As System.Windows.Forms.DataGridView
    Friend WithEvents lblBookedNetHeadline As System.Windows.Forms.Label
    Friend WithEvents lblBookedBudgetHeadline As System.Windows.Forms.Label
    Friend WithEvents lblBookedPercentHeadline As System.Windows.Forms.Label
    Friend WithEvents lblBookedEstHeadline As System.Windows.Forms.Label
    Friend WithEvents lblBookedTRPHeadline As System.Windows.Forms.Label
    Friend WithEvents lblBookedChanHeadline As System.Windows.Forms.Label
    Friend WithEvents grpFilms As System.Windows.Forms.GroupBox
    Friend WithEvents grdFilms As System.Windows.Forms.DataGridView
    Friend WithEvents grpProfile As System.Windows.Forms.GroupBox
    Friend WithEvents chtProfile As clTrinity.ProfileChart
    Friend WithEvents grpTrend As System.Windows.Forms.GroupBox
    Friend WithEvents chtTrend As clTrinity.TrendChart
    Friend WithEvents grpOther As System.Windows.Forms.GroupBox
    Friend WithEvents grdOther As System.Windows.Forms.DataGridView
    Friend WithEvents grpDetails As System.Windows.Forms.GroupBox
    Friend WithEvents grdDetails As System.Windows.Forms.DataGridView
    Friend WithEvents grpReach As System.Windows.Forms.GroupBox
    Friend WithEvents grdReach As System.Windows.Forms.DataGridView
    Friend WithEvents grpGeneral As System.Windows.Forms.GroupBox
    Friend WithEvents cmbFilm As System.Windows.Forms.ComboBox
    Friend WithEvents PictureBox3 As System.Windows.Forms.PictureBox
    Friend WithEvents cmbChannel As System.Windows.Forms.ComboBox
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents cmbDatabase As System.Windows.Forms.ComboBox
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents TabSchedule As clTrinity.ExtendedTabControl
    Friend WithEvents tpNumeric As System.Windows.Forms.TabPage
    Friend WithEvents tpGfxSchedule As System.Windows.Forms.TabPage
    Friend WithEvents stpSchedule As System.Windows.Forms.ToolStrip
    Friend WithEvents cmdScheduleFilter As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmdScheduleColumns As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmdEstimate As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmdRFEst As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmdSolus As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmbSolusFreq As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents ToolStripLabel1 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents txtCustomPeriod As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents lblStatus As System.Windows.Forms.ToolStripLabel
    Friend WithEvents GfxSchedule2 As clTrinity.gfxSchedule2
    Friend WithEvents cmdOptimize As System.Windows.Forms.ToolStripButton
    Friend WithEvents DataGridViewTextBoxColumn23 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cmdLoadOtherSpots As System.Windows.Forms.ToolStripButton
    Friend WithEvents grpGender As System.Windows.Forms.GroupBox
    Friend WithEvents chtGender As clTrinity.GenderChart
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmdLock As System.Windows.Forms.ToolStripButton
    Friend WithEvents colFilm As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colPlanned As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colPlanPerc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colBooked As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colBookedPerc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colNet As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmdExportToAllocate As System.Windows.Forms.ToolStripButton
    Friend WithEvents pbLast As System.Windows.Forms.PictureBox
    Friend WithEvents pbNext As System.Windows.Forms.PictureBox
    Friend WithEvents pbPrevious As System.Windows.Forms.PictureBox
    Friend WithEvents pbFirst As System.Windows.Forms.PictureBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents cmbWeeks As System.Windows.Forms.ComboBox
    Friend WithEvents grpEstProfile As System.Windows.Forms.GroupBox
    Friend WithEvents chtEstProfile As clTrinity.ProfileChart
    Friend WithEvents EstimatedTargetProfileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmdTweak As System.Windows.Forms.ToolStripButton
    Friend WithEvents grpPeak As System.Windows.Forms.GroupBox
    Friend WithEvents chtPrimePeak As clTrinity.PrimePeakChart
    Friend WithEvents colDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colTime As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colProgBefore As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colProgAfter As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colTarget As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colBuyTarget As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn24 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents txtProgramNameFilter As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents tmrFilter As System.Windows.Forms.Timer
    Friend WithEvents ToolStripLabel2 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator7 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents grpLeftToBook As System.Windows.Forms.GroupBox
    Friend WithEvents grdLeftToBook As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn25 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn26 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn27 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn28 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents lblLeftNetHeadline As System.Windows.Forms.Label
    Friend WithEvents lblLeftBudgetHeadline As System.Windows.Forms.Label
    Friend WithEvents lblLeftPercentHeadline As System.Windows.Forms.Label
    Friend WithEvents lblLeftEstHeadline As System.Windows.Forms.Label
    Friend WithEvents lblLeftTRPHeadline As System.Windows.Forms.Label
    Friend WithEvents lblLeftChanHeadline As System.Windows.Forms.Label
    Friend WithEvents LeftToBookToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents grdSchedule As System.Windows.Forms.DataGridView
    Friend WithEvents cmdCheckForChanges As System.Windows.Forms.ToolStripButton
    Friend WithEvents PictureToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents InfoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents grpPicture As System.Windows.Forms.GroupBox
    Friend WithEvents picPicture As System.Windows.Forms.PictureBox
    Friend WithEvents grpInfo As System.Windows.Forms.GroupBox
    Friend WithEvents lblInfo As System.Windows.Forms.Label
    Friend WithEvents cmdImdb As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents pnlScore As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents PictureBox4 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox13 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox12 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox11 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox10 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox9 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox8 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox7 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox6 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox5 As System.Windows.Forms.PictureBox
    Friend WithEvents cmdReadK2Spotlist As Windows.Forms.ToolStripButton
End Class
