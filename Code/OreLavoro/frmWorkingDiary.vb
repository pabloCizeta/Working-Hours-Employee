Imports Core

Public Class frmWorkingDiary

    Private Const cOrderSeparator = "----------"

    Private dtOreLavoro As New DataTable

    Private LastDateShown As Date = Now

    Private CheckInputData As Core.CheckInputData

    Private dtpDateChanging As Boolean = False

    Private orders As New List(Of String)

    Private Sub frmWorkingDiary_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dtpDate.Value = New Date(DateTime.Now.Year, DateTime.Now.Month, 1)

        Me.Text = My.Application.Info.AssemblyName & " - " & xGlobals.Release & " - Ore lavoro"
        Me.Icon = New System.Drawing.Icon(xGlobals.PicturePath & "OreLavoro.ico")

        Call ShowMenu()

        CheckInputData = New Core.CheckInputData()

        LoadOrdersComboBox("%")
        LoadActivityComboBox()
        LoadSectorComboBox()

        lblEmployeeName.Text = xUsers.UserLogged.Name

        LoadData()

    End Sub



    Private Sub LoadOrdersComboBox(Filter As String)
        LoadOrdersComboBox(Filter, False)

    End Sub
    Private Sub LoadOrdersComboBox(Filter As String, ShowValue As Boolean)
        cboOrderName.Items.Clear()
        cboOrderName.Items.Add("Ferie")
        cboOrderName.Items.Add("Mutua")
        cboOrderName.Items.Add(cOrderSeparator)

        orders = xDiario.GetOrderList(Filter)
        For Each str As String In orders
            cboOrderName.Items.Add(str)
        Next
        If ShowValue Then
            If orders.Count > 0 Then
                cboOrderName.Text = orders(0)
            End If
        End If
    End Sub
    Private Sub LoadActivityComboBox()
        cboActivity.Items.Clear()
        For Each str As String In xConfig.App.Params.Activities
            cboActivity.Items.Add(str.ToUpper)
        Next
    End Sub
    Private Sub LoadSectorComboBox()
        cboSector.Items.Clear()
        For Each str As String In xConfig.App.Params.Sectors
            cboSector.Items.Add(str.ToUpper)
        Next
    End Sub

    Private Sub LoadData()

        Dim DaysInMonth As Integer = Date.DaysInMonth(dtpDate.Value.Year, dtpDate.Value.Month)
        Dim EndDate As Date = New Date(dtpDate.Value.Year, dtpDate.Value.Month, DaysInMonth)
        Dim BeginDate As Date = New Date(dtpDate.Value.Year, dtpDate.Value.Month, 1)

        Dim Filter As New Diario.udtFilterOreLavoro
        Filter.EmployeeName = lblEmployeeName.Text   'xUsers.UserLogged.Name
        Filter.OrderName = "%"
        Filter.Activity = "%"
        Filter.Sector = "%"
        Filter.BeginDate = BeginDate
        Filter.EndDate = EndDate
        'dtOreLavoro = xDatabase.GetWorkedHoursMonthly(Filter)
        xDiario.GetWorkedHoursMonthly(Filter)
        Try
            dgvOreLavoro.DataSource = Nothing
            dgvOreLavoro.Rows.Clear()

            If IsNothing(xDiario.WorkedHoursMonthly) Or xDiario.WorkedHoursMonthly.Count = 0 Then
                ' dgvOreLavoro.DataSource = Nothing
                ' dgvOreLavoro.Rows.Clear()
            Else
                dgvOreLavoro.DataSource = xDiario.WorkedHoursMonthly  '  dtOreLavoro 'bsTrace
                dgvOreLavoro.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells)
                'dgvOreLavoro.RowHeadersWidth = 10
                dgvOreLavoro.RowHeadersVisible = False
                If dgvOreLavoro.ColumnCount > 1 Then
                    dgvOreLavoro.Columns(0).Width = 50
                    dgvOreLavoro.Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    dgvOreLavoro.Columns(1).Width = 230
                    dgvOreLavoro.Columns(2).Width = 120
                    dgvOreLavoro.Columns(3).Width = 100
                    dgvOreLavoro.Columns(4).Width = 60
                    dgvOreLavoro.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    dgvOreLavoro.Columns(5).Width = 60
                    dgvOreLavoro.Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                End If


                Dim ore As udtOre = GetOre(dgvOreLavoro, 4, 5)
                lblTotaleOreLavoro.Text = TimeSpanToText(ore.Lavoro)
                lblTotaleOreViaggio.Text = TimeSpanToText(ore.Viaggio)

                Dim wh As TimeSpan = GetWorkingHours(BeginDate, EndDate)
                ore.Straordinario = ore.Lavoro.Add(ore.Viaggio)
                ore.Straordinario = ore.Straordinario.Subtract(wh)
                lblTotaleOreStraordinario.Text = TimeSpanToText(ore.Straordinario)
            End If

        Catch ex As Exception

        End Try
    End Sub



#Region "Menu"

    Private Sub ShowMenu()
        Try

            mnu.CreateMenu(Me, xConfig.GetFormMenus(MyBase.Name))
            mnu.ShowMenu("Main")

        Catch ex As Exception

        End Try

    End Sub

    Private Sub mnu_ButtonClick(MenuName As String, ButtonName As String, Checked As CheckState) Handles mnu.ButtonClick
        Select Case MenuName
            Case "Main"
                Select Case ButtonName
                    Case "Close"
                        Call Shut()


                    Case "Write"
                        Write()

                    Case "Delete"
                        Call Delete()

                    Case "Print"
                        mnu.SuspendMenu()
                        Me.Cursor = Cursors.WaitCursor
                        Print()
                        mnu.ActivateMenuSuspended()
                        Me.Cursor = Cursors.Default




                End Select
        End Select



    End Sub


    Private Sub plsNext_Click(sender As Object, e As EventArgs) Handles plsNext.Click
        dtpDateChanging = True
        dtpDate.Value = DateAdd("d", 1, dtpDate.Value)
        dtpDateChanging = False
    End Sub

    Private Sub plsFilter_Click(sender As Object, e As EventArgs) Handles plsFilter.Click
        mnu.SuspendMenu()
        Using dlg As New frmFilter
            If dlg.ShowDialog() = DialogResult.OK Then
                LoadOrdersComboBox(dlg.Filter, True)
            End If
        End Using

        mnu.ActivateMenuSuspended()
    End Sub

    Private Sub dtpDate_ValueChanged(sender As Object, e As EventArgs) Handles dtpDate.ValueChanged
        If DateDiff(DateInterval.Month, LastDateShown, dtpDate.Value) <> 0 Then
            LastDateShown = dtpDate.Value
            LoadData()
        End If

        dtpDateChanging = True
        For Each r As DataGridViewRow In dgvOreLavoro.Rows
            If r.Cells("Giorno").Value = dtpDate.Value.Day Then
                r.Selected = True
            Else
                r.Selected = False
            End If
        Next
        dtpDateChanging = False
    End Sub

    Private Sub dgvOreLavoro_SelectionChanged(sender As Object, e As EventArgs) Handles dgvOreLavoro.SelectionChanged

        If dtpDateChanging Then Return

        If dgvOreLavoro.SelectedRows.Count > 0 Then
            dtpDate.Value = CDate(dtpDate.Value.Year.ToString + "-" + dtpDate.Value.Month.ToString + "-" + dgvOreLavoro.SelectedRows(0).Cells("Giorno").Value.ToString)
            txtOreLavoro.Text = dgvOreLavoro.SelectedRows(0).Cells("OreLavoro").Value
            txtOreViaggio.Text = dgvOreLavoro.SelectedRows(0).Cells("OreViaggio").Value
            cboOrderName.Text = dgvOreLavoro.SelectedRows(0).Cells("Commessa").Value
            cboActivity.Text = dgvOreLavoro.SelectedRows(0).Cells("Attività").Value
            cboSector.Text = dgvOreLavoro.SelectedRows(0).Cells("Settore").Value
        End If

    End Sub

    Private Sub Write()
        If Not CheckData() Then
            MsgBox(CheckInputData.NokResult.ToString, MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly)
            Exit Sub
        End If

        Dim rec As New Diario.udtOreLavoro
        rec.OrderName = cboOrderName.Text
        rec.Activity = cboActivity.Text
        rec.Sector = cboSector.Text
        rec.Data = dtpDate.Value
        rec.WorkingHours = txtOreLavoro.Text
        rec.JourneyHours = txtOreViaggio.Text
        If xDiario.RecordOreLavoroExists(rec) Then
            If MsgBox("Dati esistenti. Vuoi sovrascriverli?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
                Dim RowSelected As Integer = dgvOreLavoro.SelectedRows(0).Index
                xDiario.UpdateOreLavoro(rec)
                LoadData()
                dgvOreLavoro.ClearSelection()
                dgvOreLavoro.Rows(RowSelected).Selected = True
            Else

            End If
        Else
            If cboOrderName.Items.IndexOf(rec.OrderName) = -1 Then
                cboOrderName.Items.Add(rec.OrderName)
            End If
            'Add new record
            Dim day As Integer = dtpDate.Value.Day
            xDiario.AddOreLavoro(rec)
            LoadData()
            dgvOreLavoro.ClearSelection()
            'dgvOreLavoro.Rows(dgvOreLavoro.RowCount - 1).Selected = True
            For Each r As DataGridViewRow In dgvOreLavoro.Rows
                If r.Cells("Giorno").Value = day Then
                    r.Selected = True
                Else
                    r.Selected = False
                End If
            Next
        End If

        'dtpDate.Value = dtpDate.Value.AddDays(1)

    End Sub

    Private Sub Delete()
        Dim rec As New Diario.udtOreLavoro
        rec.OrderName = cboOrderName.Text
        rec.Activity = cboActivity.Text
        rec.Sector = cboSector.Text
        rec.Data = dtpDate.Value
        rec.WorkingHours = txtOreLavoro.Text
        rec.JourneyHours = txtOreViaggio.Text

        If xDiario.RecordOreLavoroExists(rec) Then
            If MsgBox("Cancello riga selezionata. Sei DAVVERO sicuro?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
                Dim RowSelected As Integer = dgvOreLavoro.SelectedRows(0).Index
                xDiario.DeleteOreLavoro(rec)
                LoadData()
            End If
        End If
    End Sub

    Private Sub Print()

        Dim DaysInMonth As Integer = Date.DaysInMonth(dtpDate.Value.Year, dtpDate.Value.Month)
        Dim EndDate As Date = New Date(dtpDate.Value.Year, dtpDate.Value.Month, DaysInMonth)
        Dim BeginDate As Date = New Date(dtpDate.Value.Year, dtpDate.Value.Month, 1)

        Dim Filter As New Diario.udtFilterOreLavoro
        Filter.EmployeeName = lblEmployeeName.Text   'xUsers.UserLogged.Name
        Filter.OrderName = "%"
        Filter.Activity = "%"
        Filter.Sector = "%"
        Filter.BeginDate = BeginDate
        Filter.EndDate = EndDate

        Dim pdf As New PrintToPDF
        pdf.PrintOreLavoro(String.Concat(Application.StartupPath, "\..\Ore.pdf"), Filter)

        Dim pdfShow As New frmShowPdf With {
            .FileToShow = String.Concat(Application.StartupPath, "\..\Ore.pdf"),
            .Size = New Size(1024, 768)
        }
        pdfShow.ShowDialog()
        pdfShow.Dispose()
        pdfShow = Nothing


    End Sub


    Private Sub Shut()
        Me.Close()
        Me.Dispose()
    End Sub


#End Region



#Region "Check data input"

    Private Sub SelectOnGotFocus(ByVal sender As Object, ByVal e As System.EventArgs)

        'sender.SelectAll()
    End Sub
    Private Sub OnlyTimeOnKeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles _
            txtOreLavoro.KeyPress, txtOreViaggio.KeyPress

        sender.backcolor = Color.White
        e.KeyChar = CheckInputData.OnlyTime(sender, e.KeyChar)
    End Sub
    Private Sub OnlyRealOnKeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        sender.backcolor = Color.White
        e.KeyChar = CheckInputData.OnlyNumber(sender, e.KeyChar, "+")
    End Sub


    Private Function CheckData() As Boolean

        If Not CheckInputData.IsTime(txtOreLavoro) Then Return False
        If Not CheckInputData.IsTime(txtOreViaggio) Then Return False

        If cboOrderName.Text = cOrderSeparator Then
            CheckInputData.NokResult = CheckInputData.enumNokResult.ValoreErrato
            cboOrderName.Focus()
            cboOrderName.BackColor = Color.Coral
            Return False
        End If

        If cboOrderName.Text = "" Then
            CheckInputData.NokResult = CheckInputData.enumNokResult.ValoreDeveEsistere
            cboOrderName.Focus()
            cboOrderName.BackColor = Color.Coral
            Return False
        End If

        If cboActivity.Text = "" Then
            CheckInputData.NokResult = CheckInputData.enumNokResult.ValoreDeveEsistere
            cboActivity.Focus()
            cboActivity.BackColor = Color.Coral
            Return False
        End If

        If cboSector.Text = "" Then
            CheckInputData.NokResult = CheckInputData.enumNokResult.ValoreDeveEsistere
            cboSector.Focus()
            cboSector.BackColor = Color.Coral
            Return False
        End If

        Return True

    End Function

    Private Sub cboOrderName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboOrderName.SelectedIndexChanged
        cboOrderName.BackColor = Color.White
    End Sub



    Private Sub cboOrderName_DropDown(sender As Object, e As EventArgs) Handles cboOrderName.DropDown
        cboOrderName.BackColor = Color.White
    End Sub


#End Region





End Class