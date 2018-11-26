Imports System.Windows.Forms
Imports System.Drawing
Imports System.Xml.Linq
Imports System.Threading
Public Class frmReadChannelSplit

    Dim localChan As Trinity.cChannel
    Dim localBT As Trinity.cBookingType
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        
        For each tmpchan As Trinity.cChannel in Campaign.Channels
            cmbChannel.Items.Add(tmpchan)
        Next
    End Sub

    Private Sub cmbBookingtype_Enter(sender As Object, e As EventArgs) Handles cmbBookingtype.Enter
        If Not cmbChannel.Text = ""
            cmbBookingtype.Items.Clear()
            localChan = cmbChannel.SelectedItem
            For each tmpBT As Trinity.cBookingType in localChan.BookingTypes
                cmbBookingtype.Items.Add(tmpBT)
            Next
        End If
    End Sub

    Private Sub cmbChannel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbChannel.SelectedIndexChanged
        cmbBookingtype.SelectedItem = Nothing
    End Sub

    Private Sub cmbBookingtype_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbBookingtype.SelectedIndexChanged        
        grdChannelSplitview.Rows.Clear()
        grdChannelSplitview.Columns.Clear()
        If localChan.ChannelName = "TV3"
            localBT = cmbBookingtype.SelectedItem
            If localBT.Name = "3-6-8-10-MTV-CC-NGC-FOX"
                'Create columns for this specific booking type, not optimal but a quick fix

                if grdChannelSplitview.ColumnCount < 1
                    grdChannelSplitview.ColumnCount = 7
                    grdChannelSplitview.Columns(0).Name = "Target"
                    grdChannelSplitview.Columns(1).Name = "TV3"
                    grdChannelSplitview.Columns(2).Name = "TV6"
                    grdChannelSplitview.Columns(3).Name = "TV8"
                    grdChannelSplitview.Columns(4).Name = "TV10"
                    grdChannelSplitview.Columns(5).Name = "MTV"
                    grdChannelSplitview.Columns(6).Name = "CC"
                    grdChannelSplitview.Columns(7).Name = "NGC"
                    grdChannelSplitview.Columns(8).Name = "FOX"
                End If

                For i As Integer = 0 To 23
                    Dim row As String() = New String() {"0", "0", "0", "0", "0", "0", "0", "0", "0"}
                    grdChannelSplitview.Rows.Add(row)
                Next
            ElseIf localBT.Name = "Full flex 3-6-8-10"                
                if grdChannelSplitview.ColumnCount < 1
                    grdChannelSplitview.ColumnCount = 5
                    grdChannelSplitview.Columns(0).Name = "Target"
                    grdChannelSplitview.Columns(1).Name = "TV3"
                    grdChannelSplitview.Columns(2).Name = "TV6"
                    grdChannelSplitview.Columns(3).Name = "TV8"
                    grdChannelSplitview.Columns(4).Name = "TV10"
                End If
                For i As Integer = 0 To 23
                    Dim row As String() = New String() {"0", "0", "0", "0", "0"}
                    grdChannelSplitview.Rows.Add(row)
                Next
            ElseIf localBT.Name = "Full flex 3-6-8"                
                if grdChannelSplitview.ColumnCount < 1
                    grdChannelSplitview.ColumnCount = 4
                    grdChannelSplitview.Columns(0).Name = "Target"
                    grdChannelSplitview.Columns(1).Name = "TV3"
                    grdChannelSplitview.Columns(2).Name = "TV6"
                    grdChannelSplitview.Columns(3).Name = "TV8"
                End If
                For i As Integer = 0 To 23
                    Dim row As String() = New String() {"0", "0", "0", "0"}
                    grdChannelSplitview.Rows.Add(row)
                Next
            ElseIf localBT.Name = "Full flex 3-6"                
                if grdChannelSplitview.ColumnCount < 1
                    grdChannelSplitview.ColumnCount = 3
                    grdChannelSplitview.Columns(0).Name = "Target"
                    grdChannelSplitview.Columns(1).Name = "TV3"
                    grdChannelSplitview.Columns(2).Name = "TV6"
                End If
                For i As Integer = 0 To 23
                    Dim row As String() = New String() {"0", "0", "0"}
                    grdChannelSplitview.Rows.Add(row)
                Next
            End If
        End If
    End Sub

    Private Sub cmdSaveAs_Click(sender As Object, e As EventArgs) Handles cmdSaveAs.Click
        Dim _dlg As New Windows.Forms.SaveFileDialog

        _dlg.FileName = localBT.Name & ".xml"
        _dlg.Filter = "Pricelists|*.xml"
        _dlg.InitialDirectory = TrinitySettings.ActiveDataPath
        
        Dim path As String = ""
        
        If _dlg.ShowDialog = Windows.Forms.DialogResult.OK Then
            
            path = _dlg.FileName
            Dim XMLDoc As New XmlDocument
                
            Using writer As XmlWriter = XmlWriter.Create(path)
                'Dim TargetsNode As XmlElement                
                'Dim tmpTargetNode As XmlElement
                'TargetsNode = XMLDoc.CreateElement("Targets")

                writer.WriteStartDocument()
                writer.WriteStartElement("Targets")
                For each tmprow As DataGridViewRow In grdChannelSplitview.Rows
                    
                    writer.WriteStartElement("Target")
                    For i As Integer = 0 to grdChannelSplitview.Columns.Count - 1 
                        writer.WriteElementString(grdChannelSplitview.Columns(i).Name, Replace(tmprow.Cells(i).Value, "%", ""))
                    Next
                    'tmpTargetNode = XMLDoc.CreateElement("Target", )

                    'For i As Integer = 0 to grdChannelSplitview.Columns.Count - 1  
                    '    Dim col As Integer = 0                  
                    '    tmpTargetNode = XMLDoc.CreateElement(grdChannelSplitview.Columns(i).Name, Replace(tmprow.Cells(i).Value, "%", ""))
                    'Next
                    'TargetsNode.AppendChild(tmpTargetNode)
                    writer.WriteEndElement()      
                Next       
                writer.WriteEndElement()         
                writer.WriteEndDocument()
                'XMLDoc.AppendChild(TargetsNode)
                'XMLDoc.Save(path)
            End Using
        End If
    End Sub
    
    Private Sub grdChannelSplitview_CellValueNeeded(sender As Object, e As DataGridViewCellValueEventArgs) Handles grdChannelSplitview.CellValueNeeded
    End Sub

    Private Sub grdChannelSplitview_KeyUp_1(sender As Object, e As KeyEventArgs) Handles grdChannelSplitview.KeyUp
        
        
        If e.Control AndAlso e.KeyCode = Keys.V Then
            Dim _rows() As String = Clipboard.GetText().Split(vbNewLine)
            Dim _r As Integer = grdChannelSplitview.SelectedRows(0).Index
            For Each _row As String In _rows
                Dim _cells() As String = _row.Split(vbTab)
                Dim _c As Integer = 0
                For Each _cell As String In _cells
                    If _cell.Trim <> "" Then
                        While Not grdChannelSplitview.Columns(_c).Visible
                            _c += 1
                        End While
                        grdChannelSplitview.Rows(_r).Cells(_c).Value = _cell.Replace(vbLf, "")

                    End If
                    _c += 1
                Next
                _r += 1
            Next
        End If
    End Sub

    Private Sub grdChannelSplitview_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles grdChannelSplitview.CellFormatting
        If e.ColumnIndex <> 0
            e.Value = Replace(e.Value, "%", "")
        End If
    End Sub

    'If e.ColumnIndex <> 0
    '    e.Value = Replace(e.Value.ToString, "%", "")
    'End If
End Class