Imports System.Globalization
Imports System.Threading
Imports Core

Public Class frmExpenses

    Private CheckInputData As Core.CheckInputData

    Private dtpDateChanging As Boolean = False
    Private LastDateShown As Date = Now

    Private dgvSelecting As Boolean = False

    Private IsLoading As Boolean = False

    Private CostoKm As Single = 0


    Private Sub frmExpenses_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dtpDate.Value = New Date(DateTime.Now.Year, DateTime.Now.Month, 1)

        UpdateDayName()

        Me.Text = My.Application.Info.AssemblyName & " - " & xGlobals.Release & " - Spese e rimborsi"
        Me.Icon = New System.Drawing.Icon(xGlobals.PicturePath & "OreLavoro.ico")

        Call ShowMenu()

        CheckInputData = New Core.CheckInputData()

        IsLoading = True
        lblEmployeeName.Text = xUsers.UserLogged.Name

        LoadOrdersComboBox("%")

        LoadData()

        ReadEmployeeData()

        IsLoading = False

    End Sub


#Region "Private"

    Private Sub LoadOrdersComboBox(Filter As String)
        LoadOrdersComboBox(Filter, False)
    End Sub
    Private Sub LoadOrdersComboBox(Filter As String, ShowValue As Boolean)
        Dim orders As New List(Of String)
        orders = xDiario.GetOrderList(Filter)
        orders.Sort()
        For Each str As String In orders
            cboOrderName.Items.Add(str)
        Next
        If ShowValue Then
            If orders.Count > 0 Then
                cboOrderName.Text = orders(0)
            End If
        End If
    End Sub

    Private Sub LoadData()

        Dim dtData As New DataTable

        Dim DaysInMonth As Integer = Date.DaysInMonth(dtpDate.Value.Year, dtpDate.Value.Month)
        Dim EndDate As Date = New Date(dtpDate.Value.Year, dtpDate.Value.Month, DaysInMonth)
        Dim BeginDate As Date = New Date(dtpDate.Value.Year, dtpDate.Value.Month, 1)

        Dim Filter As New Diario.udtFilterExpenses
        Filter.EmployeeName = lblEmployeeName.Text
        Filter.OrderName = "%"
        Filter.BeginDate = BeginDate
        Filter.EndDate = EndDate
        xDiario.GetExpensesMonthly(Filter)

        dgv.DataSource = Nothing
        dgv.Rows.Clear()

        If IsNothing(xDiario.ExpensesMonthly) Or xDiario.ExpensesMonthly.Count = 0 Then
            txtKm.Text = "0"
            txtAutostrada.Text = "0"
            txtMezziPubblici.Text = "0"
            txtVitto.Text = "0"
            txtAlloggio.Text = "0"
            txtVarie.Text = "0"
            txtCartaCredito.Text = "0"
            txtValuta.Text = "0"
            chkItaly.Checked = True
            chkEstero.Checked = False
            chkHotel.Checked = False
        Else
            dgv.DataSource = xDiario.ExpensesMonthly
            dgv.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells)
            'dgv.RowHeadersWidth = 10
            dgv.RowHeadersVisible = False
            If dgv.ColumnCount > 1 Then
                dgv.Columns(0).Width = 40       'Giorno
                dgv.Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                dgv.Columns(1).Width = 180      'Commessa
                dgv.Columns(2).Width = 120      'Localita
                dgv.Columns(3).Width = 40       'KM
                dgv.Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                dgv.Columns(4).Width = 60       'autostrada
                dgv.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                dgv.Columns(5).Width = 50       'Mezzi
                dgv.Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                dgv.Columns(6).Width = 40       'Vitto
                dgv.Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                dgv.Columns(7).Width = 50       'Alloggio
                dgv.Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                dgv.Columns(8).Width = 50       'Varie
                dgv.Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                dgv.Columns(9).Width = 50       'Carta
                dgv.Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                dgv.Columns(10).Width = 40       'Valuta
                dgv.Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                dgv.Columns(11).Width = 100       'Trasferta
                dgv.Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            Else
                txtKm.Text = "0"
                txtAutostrada.Text = "0"
                txtMezziPubblici.Text = "0"
                txtVitto.Text = "0"
                txtAlloggio.Text = "0"
                txtVarie.Text = "0"
                txtCartaCredito.Text = "0"
                txtValuta.Text = "0"
                chkItaly.Checked = True
                chkEstero.Checked = False
                chkHotel.Checked = False
            End If
        End If

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

        If dgvSelecting Then Return

        If DateDiff(DateInterval.Month, LastDateShown, dtpDate.Value) <> 0 Then
            LastDateShown = dtpDate.Value
            LoadData()
        End If

        dtpDateChanging = True
        For Each r As DataGridViewRow In dgv.Rows
            If r.Cells("Giorno").Value = dtpDate.Value.Day Then
                r.Selected = True
            Else
                r.Selected = False
            End If
        Next
        dtpDateChanging = False

        UpdateDayName()
    End Sub

    Private Sub dgv_SelectionChanged(sender As Object, e As EventArgs) Handles dgv.SelectionChanged

        If dtpDateChanging Then Return
        dgvSelecting = True

        If dgv.SelectedRows.Count > 0 Then

            cboOrderName.Text = dgv.SelectedRows(0).Cells("Commessa").Value
            txtCity.Text = dgv.SelectedRows(0).Cells("Località").Value
            txtKm.Text = dgv.SelectedRows(0).Cells("Km").Value
            txtAutostrada.Text = dgv.SelectedRows(0).Cells("Autostrada").Value
            txtMezziPubblici.Text = dgv.SelectedRows(0).Cells("Mezzi").Value
            txtVitto.Text = dgv.SelectedRows(0).Cells("Vitto").Value
            txtAlloggio.Text = dgv.SelectedRows(0).Cells("Alloggio").Value
            txtVarie.Text = dgv.SelectedRows(0).Cells("Varie").Value

            txtCartaCredito.Text = dgv.SelectedRows(0).Cells("Carta").Value
            txtValuta.Text = dgv.SelectedRows(0).Cells("Valuta").Value
            dtpDate.Value = CDate(dtpDate.Value.Year.ToString + "-" + dtpDate.Value.Month.ToString + "-" + dgv.SelectedRows(0).Cells("Giorno").Value.ToString)

            Select Case dgv.SelectedRows(0).Cells("Trasferta").Value
                Case Diario.enummTipoTrasferta.Italia.ToString
                    chkItaly.Checked = True
                    chkEstero.Checked = False
                    chkHotel.Checked = False
                Case Diario.enummTipoTrasferta.ItaliaHotel.ToString
                    chkItaly.Checked = True
                    chkEstero.Checked = False
                    chkHotel.Checked = True
                Case Diario.enummTipoTrasferta.Estero.ToString
                    chkItaly.Checked = False
                    chkEstero.Checked = True
                    chkHotel.Checked = False
                Case Diario.enummTipoTrasferta.EsteroLunga.ToString
                    chkItaly.Checked = False
                    chkEstero.Checked = True
                    chkHotel.Checked = False
                Case Else
                    chkItaly.Checked = True
                    chkEstero.Checked = False
                    chkHotel.Checked = False
            End Select
        End If

        dgvSelecting = False

    End Sub

    Private Sub chkItaly_CheckedChanged(sender As Object, e As EventArgs) Handles chkItaly.CheckedChanged
        If IsLoading Then Return
        If chkItaly.Checked Then
            chkEstero.Checked = False
        Else
            If Not chkEstero.Checked Then
            End If
        End If
    End Sub

    Private Sub chkEstero_CheckedChanged(sender As Object, e As EventArgs) Handles chkEstero.CheckedChanged
        If IsLoading Then Return
        If chkEstero.Checked Then
            chkItaly.Checked = False
            chkHotel.Checked = False
        Else
            If Not chkItaly.Checked Then
            End If
        End If
    End Sub

    Private Function SetRecordExpenses() As Diario.udtExpense
        Dim rec As New Diario.udtExpense
        rec.City = txtCity.Text
        rec.OrderName = cboOrderName.Text
        rec.Data = dtpDate.Value
        rec.Alloggio = TextToSingle(txtAlloggio.Text)
        rec.Autostrada = TextToSingle(txtAutostrada.Text)
        rec.CCA = TextToSingle(txtCartaCredito.Text)
        rec.Km = txtKm.Text
        rec.MezziPubblici = TextToSingle(txtMezziPubblici.Text)
        rec.Valuta = TextToSingle(txtValuta.Text)
        rec.Varie = TextToSingle(txtVarie.Text)
        rec.Vitto = TextToSingle(txtVitto.Text)
        rec.TipoRimborso = cTipoRimborso
        rec.Motivo = cMotivoRimborso
        If chkEstero.Checked Then
            rec.Diaria = cTrasfertaEstero
        ElseIf chkItaly.Checked Then
            If chkHotel.Checked Then
                rec.Diaria = cTrasfertaItaliaHotel
            Else
                rec.Diaria = cTrasfertaItalia
            End If
        Else
            rec.Diaria = cTrasfertaItalia
        End If

        Return rec
    End Function

#End Region



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

    Private Sub Write()
        If Not CheckData() Then
            MsgBox(CheckInputData.NokResult.ToString, MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly)
            Exit Sub
        End If

        Dim rec As Diario.udtExpense = SetRecordExpenses()
        If xDiario.RecordExpensesExists(rec) Then
            If MsgBox("Dati esistenti. Vuoi sovrascriverli?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
                Dim RowSelected As Integer = dgv.SelectedRows(0).Index
                xDiario.UpdateExpenses(rec)
                LoadData()
                dgv.ClearSelection()
                dgv.Rows(RowSelected).Selected = True
            Else

            End If
        Else
            'Add new record
            Dim day As Integer = dtpDate.Value.Day
            xDiario.AddExpenses(rec)
            LoadData()
            dgv.ClearSelection()
            For Each r As DataGridViewRow In dgv.Rows
                If r.Cells("Giorno").Value = day Then
                    r.Selected = True
                Else
                    r.Selected = False
                End If
            Next
        End If

        dtpDate.Value = dtpDate.Value.AddDays(1)
    End Sub

    Private Sub Delete()
        Dim rec As Diario.udtExpense = SetRecordExpenses()
        If xDiario.RecordExpensesExists(rec) Then
            If MsgBox("Cancello riga selezionata. Sei DAVVERO sicuro?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
                Dim RowSelected As Integer = dgv.SelectedRows(0).Index
                xDiario.DeleteExpenses(rec)
                LoadData()
            End If
        Else
            If rec.OrderName <> "" Then
                If MsgBox("Voce presente nelle ore lavoro. Cancellazione non possibile.", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly) = MsgBoxResult.Ok Then

                End If
            End If

        End If
    End Sub


    Private Sub Print()

        Dim DaysInMonth As Integer = Date.DaysInMonth(dtpDate.Value.Year, dtpDate.Value.Month)
        Dim EndDate As Date = New Date(dtpDate.Value.Year, dtpDate.Value.Month, DaysInMonth)
        Dim BeginDate As Date = New Date(dtpDate.Value.Year, dtpDate.Value.Month, 1)

        Dim Filter As New Diario.udtFilterExpenses
        Filter.EmployeeName = lblEmployeeName.Text
        Filter.OrderName = "%"
        Filter.BeginDate = BeginDate
        Filter.EndDate = EndDate
        'dtData = xDatabase.GetUserRefound(Filter)

        Dim pdf As New PrintToPDF
        pdf.PrintExpenses(String.Concat(Application.StartupPath, "\..\RimborsoSpese.pdf"), Filter)

        Dim pdfShow As New frmShowPdf
        pdfShow.FileToShow = String.Concat(Application.StartupPath, "\..\RimborsoSpese.pdf")
        pdfShow.Size = New Size(1024, 768)

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

    Private Sub SelectOnGotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
            txtCity.KeyPress

        sender.backcolor = Color.White
    End Sub
    Private Sub OnlyTimeOnKeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles _
            txtKm.KeyPress

        sender.backcolor = Color.White
        e.KeyChar = CheckInputData.OnlyInteger(sender, e.KeyChar, "+")
    End Sub
    Private Sub OnlyRealOnKeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles _
            txtAutostrada.KeyPress, txtMezziPubblici.KeyPress, txtVitto.KeyPress, txtAlloggio.KeyPress, txtVarie.KeyPress,
            txtCartaCredito.KeyPress, txtValuta.KeyPress

        sender.backcolor = Color.White
        e.KeyChar = CheckInputData.OnlyNumber(sender, e.KeyChar, "+")
    End Sub


    Private Function CheckData() As Boolean

        If Not CheckInputData.IsValueExisting(txtCity) Then Return False

        If Not CheckInputData.IsValueNumericOk(txtKm, 0, 999999) Then Return False
        If Not CheckInputData.IsValueNumericOk(txtAutostrada, 0, 999999) Then Return False
        If Not CheckInputData.IsValueNumericOk(txtMezziPubblici, 0, 999999) Then Return False
        If Not CheckInputData.IsValueNumericOk(txtVitto, 0, 999999) Then Return False
        If Not CheckInputData.IsValueNumericOk(txtAlloggio, 0, 999999) Then Return False
        If Not CheckInputData.IsValueNumericOk(txtVarie, 0, 999999) Then Return False
        If Not CheckInputData.IsValueNumericOk(txtCartaCredito, 0, 999999) Then Return False
        If Not CheckInputData.IsValueNumericOk(txtValuta, 0, 999999) Then Return False

        If cboOrderName.Text = "" Then
            CheckInputData.NokResult = CheckInputData.enumNokResult.CommessaNonValida
            cboOrderName.Focus()
            'cboOrderName.BackColor = Color.Coral
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

    Private Sub Label11_Click(sender As Object, e As EventArgs) Handles Label11.Click

    End Sub


    Private Sub UpdateDayName()

        'lblDay.Text = dtpDate.Value.DayOfWeek.ToString
        'lblDay.Text = lblDay.Text.Remove(3)

        Dim dateNow = dtpDate.Value    'DateTime.Now
        Dim dfi = DateTimeFormatInfo.CurrentInfo
        Dim calendar = dfi.Calendar


        Dim weekOfyear = calendar.GetWeekOfYear(dateNow, dfi.CalendarWeekRule, DayOfWeek.Monday)

        Dim OSlanguage As String = Thread.CurrentThread.CurrentCulture.Name

        Select Case OSlanguage
            Case "en-GB"
                lblWY.Text = "Week " & weekOfyear.ToString
            Case "it-IT"
                lblWY.Text = "Sett " & weekOfyear.ToString
            Case Else
                lblWY.Text = "Sett " & weekOfyear.ToString
        End Select

        Select Case dtpDate.Value.DayOfWeek

            Case DayOfWeek.Sunday
                lblData.BackColor = Color.Red

            Case DayOfWeek.Saturday
                lblData.BackColor = Color.DarkOrange

            Case Else
                lblData.BackColor = Color.Transparent

        End Select


    End Sub

    Private Sub ReadEmployeeData()

        'Leggi il file per dati da importare nella versione dipendenti
        Dim FileName As String = xGlobals.DataPath & "\Data" & lblEmployeeName.Text.Replace(" ", "") & ".xml"
        Dim data As New EmployeeData(FileName)
        data.ReadDataFromFile()
        If data.Data.MoneyAttuali.Count > 0 Then
            txtRiporti.Text = data.Data.MoneyAttuali(0).Riporti
        Else
            txtRiporti.Text = "Errore File"
        End If




    End Sub


#End Region

End Class