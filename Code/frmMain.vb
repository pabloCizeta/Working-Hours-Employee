Imports Core

Public Class frmMain


#Region "Private data"

    Private Const cOrderSeparator = "----------"

    Private WithEvents tmrVisual As Windows.Forms.Timer
    Private WithEvents tmrVisualFast As Windows.Forms.Timer

    Private dtOreLavoro As New DataTable

    Private LastDateShown As Date = Now

    Private CheckInputData As Core.CheckInputData

    Private dtpDateChanging As Boolean = False


#End Region


#Region " Form events "

    Private Sub frmMain_Activated(sender As Object, e As EventArgs) Handles Me.Activated
    End Sub

    Private Sub frmMain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        End
    End Sub

    Private Sub frmMain_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint

    End Sub
    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim EndProgramForced As Boolean = False

        xGlobals = New Globals

        'Nota: Build output path : ..\..\App\

        Dim DiskDrive As String = System.IO.Path.GetPathRoot(System.Windows.Forms.Application.StartupPath)
        'xGlobals.DataPath = DiskDrive & System.Windows.Forms.Application.ProductName & "\Data\"
        'xGlobals.PicturePath = DiskDrive & System.Windows.Forms.Application.ProductName & "\Data\Pictures\"

        'If Not System.Diagnostics.Debugger.IsAttached Then
        xGlobals.DataPath = Application.StartupPath & "\..\Data\"
        xGlobals.PicturePath = Application.StartupPath & "\..\Data\Pictures\"
        ' End If



        xGlobals.Release = String.Format("Rev. {0}.{1}.{2}", My.Application.Info.Version.Major, My.Application.Info.Version.Minor, My.Application.Info.Version.Build)
        lblRelease.Text = xGlobals.Release
        Me.Text = My.Application.Info.AssemblyName & " - " & xGlobals.Release

        'Config
        xConfig = New Configuration
        If Not xConfig.ReadAppConfig(xGlobals.DataPath & "myConfig.xml") Then EndProgramForced = True
        If Not xConfig.ReadMenusConfig(xGlobals.DataPath & "Menus.xml") Then EndProgramForced = True
        If EndProgramForced Then
            MsgBox("Lettura configurazione fallita", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
            End
        End If




        'Users
        xUsers = New Users()
        xUsers.LevelsForUserRole = "1"
        xUsers.LevelsForMaintenanceRole = "1"
        xUsers.LevelsForAdministratorRole = "1"

        xUsers.UserLogged.Name = xConfig.App.Params.NomeDipendente
        xUsers.UserLogged.Role = Users.enumUserRole.Administrator

        Me.lblLoginName.Text = xUsers.UserLogged.Name
        'Me.lblUserRole.Text = xUsers.UserLogged.Role.ToString
        If My.Computer.FileSystem.FileExists(xGlobals.DataPath & "Foto.png") Then
            picUserLogged.Image = Image.FromFile(xGlobals.DataPath & "Foto.png")
        End If

        xDiario = New Diario(xUsers.UserLogged.Name)
        xDiario.ReadDataFromFile()

        'update dall'xml in \import
        If My.Computer.FileSystem.FileExists(xGlobals.DataPath & "import\" & "Diario" & xUsers.UserLogged.Name.Replace(" ", "") & ".xml") Then
            UpdateDiaryFromAdmin()
        End If



        KeyPreview = True

        'Start chaos
        tmrVisual = New Windows.Forms.Timer
        tmrVisual.Enabled = True
        tmrVisual.Interval = 2000
        tmrVisualFast = New Windows.Forms.Timer
        tmrVisualFast.Enabled = True
        tmrVisualFast.Interval = 50


        panMenu.Enabled = True

        mnu.CreateMenu(Me, xConfig.GetFormMenus(MyBase.Name))
        mnu.ShowMenu("Main")

    End Sub




#End Region


#Region "Private"



#End Region


#Region "Timers"

    Private Sub tmrVisual_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrVisual.Tick




    End Sub

    Private Sub tmrVisualFast_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrVisualFast.Tick




    End Sub


#End Region

#Region "Menu"

    Private Sub mnu_ButtonClick(MenuName As String, ButtonName As String, Checked As CheckState) Handles mnu.ButtonClick
        Select Case MenuName
            Case "Main"
                MenuMainButtonPressed(ButtonName)

            Case "Config"
                MenuConfigButtonPressed(ButtonName)

        End Select

    End Sub

    Private Sub MenuMainButtonPressed(ButtonName As String)

        Select Case ButtonName
            Case "OreLavoro"
                mnu.SuspendMenu()
                Dim frm As New frmWorkingDiary
                frm.Show(Me)
                'frm.Dispose()
                'frm = Nothing
                mnu.ActivateMenuSuspended()

            Case "Refound"
                mnu.SuspendMenu()
                Dim frm As New frmExpenses
                frm.Show(Me)
                'frm.Dispose()
                'frm = Nothing
                mnu.ActivateMenuSuspended()

            Case "Orders"

            Case "Config"
                mnu.ShowMenu("Config")

            Case "End"
                Me.Close()
        End Select



    End Sub

    Private Sub MenuConfigButtonPressed(ButtonName As String)

        Select Case ButtonName
            Case "Back", "Escape", "Close"
                mnu.ShowMenu("Main")

            Case "Employees"


            Case "Parametri"


        End Select



    End Sub




#End Region



#Region "Check data input"

    Private Sub SelectOnGotFocus(ByVal sender As Object, ByVal e As System.EventArgs)

        'sender.SelectAll()
    End Sub
    Private Sub OnlyTimeOnKeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        sender.backcolor = Color.White
        e.KeyChar = CheckInputData.OnlyTime(sender, e.KeyChar)
    End Sub
    Private Sub OnlyRealOnKeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        sender.backcolor = Color.White
        e.KeyChar = CheckInputData.OnlyNumber(sender, e.KeyChar, "+")
    End Sub


    Private Function CheckData() As Boolean

        'If Not CheckInputData.IsTime(txtOreLavoro) Then Return False
        'If Not CheckInputData.IsTime(txtOreViaggio) Then Return False

        'If cboOrderName.Text = cOrderSeparator Then
        '    CheckInputData.NokResult = CheckInputData.enumNokResult.ValoreErrato
        '    cboOrderName.Focus()
        '    cboOrderName.BackColor = Color.Coral
        '    Return False
        'End If

        Return True

    End Function

    'Private Sub cboOrderName_SelectedIndexChanged(sender As Object, e As EventArgs)
    '    cboOrderName.BackColor = Color.White
    'End Sub



    'Private Sub cboOrderName_DropDown(sender As Object, e As EventArgs)
    '    cboOrderName.BackColor = Color.White
    'End Sub


#End Region
#Region "Update Xml"
    Private Sub UpdateDiaryFromAdmin()

        Dim diarioAdmin As Diario = New Diario(xUsers.UserLogged.Name, True)
        diarioAdmin.ReadDataFromFile()
        Dim ListaNuoveOre
        Dim ListaNuoveSpese
        If diarioAdmin.Diario.OreLavoro.Count > 0 Then

            'inserisco record nuovi nel diario aggiornato
            ListaNuoveOre = xDiario.Diario.OreLavoro.FindAll(Function(c) c.Data > diarioAdmin.Diario.OreLavoro.Last.Data)
            For Each item In ListaNuoveOre
                diarioAdmin.AddOreLavoro(item)
            Next

            ListaNuoveSpese = xDiario.Diario.Spese.FindAll(Function(c) c.Data > diarioAdmin.Diario.Spese.Last.Data)
            For Each item In ListaNuoveSpese
                diarioAdmin.AddExpenses(item)
            Next

            'elimino xml vecchio, sposto quello aggiornato e chiudo l'app dopo aver notificato l'utente
            My.Computer.FileSystem.DeleteFile(xGlobals.DataPath & "Diario" & xUsers.UserLogged.Name.Replace(" ", "") & ".xml")
            My.Computer.FileSystem.MoveFile(xGlobals.DataPath & "import\" & "Diario" & xUsers.UserLogged.Name.Replace(" ", "") & ".xml", xGlobals.DataPath & "Diario" & xUsers.UserLogged.Name.Replace(" ", "") & ".xml")

            MsgBox("File .xml aggiornato." & vbCrLf & "Riavviare OreLavoro", MsgBoxStyle.Information + MsgBoxStyle.DefaultButton2)
            Application.Exit()

        End If
    End Sub
#End Region





End Class
