Imports System.Windows.Forms
Imports System.Drawing

Public Class eMenu

    Private WithEvents mForm As System.Windows.Forms.Form
    Private WithEvents tsButton1 As ToolStripButton
    Private WithEvents tsButton2 As ToolStripButton
    Private WithEvents tsButton3 As ToolStripButton
    Private WithEvents tsButton4 As ToolStripButton
    Private WithEvents tsButton5 As ToolStripButton
    Private WithEvents tsButton6 As ToolStripButton
    Private WithEvents tsButton7 As ToolStripButton
    Private WithEvents tsButton8 As ToolStripButton
    Private WithEvents tsButton9 As ToolStripButton
    Private WithEvents tsButton10 As ToolStripButton
    Private WithEvents tsButton11 As ToolStripButton
    Private WithEvents tsButton12 As ToolStripButton
    Private IdActiveMenu As Byte

    Public Property IdMenuPrec As Byte

    ' Private WithEvents Autologout As czControlLibrary.czAutoLogout

    Public Enum enumUserRole
        Unknown = 0
        User = 1
        Administrator = 2
        Maintenance = 3
        Master = 4
    End Enum


    Private Structure udtOpzMenu
        Friend Name As String
        Friend Text As String
        Friend ToolTipText As String
        Friend Image As System.Drawing.Image
        Friend Visible As Boolean
        Friend Enabled As Boolean
        Friend EnabledBeforeSuspended As Boolean
        Friend SwitchButton As Boolean
        Friend SwitchStatus As Boolean
        Friend UserRoleEnabled As enumUserRole
    End Structure
    Private Structure udtMenu
        Friend Title As String
        Friend QtyOpz As Byte
        Friend Opz() As udtOpzMenu

    End Structure

    Private menu() As udtMenu
    Public Event TextButtonChoosed(ByVal IdActiveMenu As Byte, ByVal sNameButton As String, ByVal IdMenuPrecedente As Byte)
    Public Event IdButtonChoosed(ByVal IdActiveMenu As Byte, ByVal IdButtom As Byte, ByVal IdMenuPrecedente As Byte)


#Region "Proprieta"

    Property FunctionKeyEnabled As Boolean = True

    Property TitleMenu(ByVal index As Byte) As String
        Get
            Return menu(index).Title
        End Get
        Set(ByVal value As String)
            menu(index).Title = GetText(value)
        End Set
    End Property

    Public Property IdMenuSuspended As Integer = 0

    Public ReadOnly Property IsButtonPressed(ByVal IdMenu As Byte, ByVal ButtonName As String) As Boolean
        Get
            Dim iOpz As Byte
            iOpz = GetIndexOpzMenu(IdMenu, ButtonName)
            If iOpz <> 0 Then
                Return menu(IdMenu).Opz(iOpz).SwitchStatus
            End If
        End Get
    End Property


#End Region

#Region "Metodi"

    Public Sub CreateMenu(ByVal xForm As Form, ByVal QtyMenu As Byte, ByVal MenuWidth As Integer)
        ReDim Preserve menu(QtyMenu)
        mForm = xForm
        mForm.KeyPreview = True
        If MenuWidth <> 0 Then
            ToolStrip1.Width = MenuWidth
        Else
            ToolStrip1.Width = Parent.Width '- 10
        End If
    End Sub
    Public Sub CreateButtonMenu(ByVal IdMenu As Byte, ByVal IdButton As Byte, _
               ByVal Name As String, ByVal Image As System.Drawing.Image, _
               ByVal Caption As String, ByVal SwitchButton As Boolean, ByVal Enabled As Boolean)

        CreateButtonMenu(IdMenu, IdButton, Name, Image, Caption, SwitchButton, Enabled, enumUserRole.Unknown)

    End Sub

    Public Sub CreateButtonMenu(ByVal IdMenu As Byte, ByVal IdButton As Byte, _
               ByVal Name As String, ByVal Image As System.Drawing.Image, _
               ByVal Caption As String, ByVal SwitchButton As Boolean, ByVal Enabled As Boolean, UserRoleToBeEnabled As enumUserRole)

        menu(IdMenu).QtyOpz = IdButton
        ReDim Preserve menu(IdMenu).Opz(IdButton)
        With menu(IdMenu).Opz(IdButton)
            .Name = Name
            .Image = Image
            If FunctionKeyEnabled Then
                If Name <> "Fine" Then
                    If Name = "Chiudi" Then
                        .Text = "(F12) " & GetText(Caption)
                    Else
                        .Text = "(F" & Format(IdButton) & ") " & GetText(Caption)
                    End If

                Else
                    .Text = "(Alt-F10) " & GetText(Caption)
                End If
            Else
                .Text = GetText(Caption)
            End If

            .ToolTipText = GetToolTipText(Caption)
            .Visible = True
            .Enabled = Enabled
            If Len(.Name) = 0 Then
                .Text = " "
                .ToolTipText = " "
            End If
            .SwitchButton = SwitchButton
            .SwitchStatus = False
            .UserRoleEnabled = UserRoleToBeEnabled
        End With
    End Sub

    Public Sub ShowMenu(ByVal IdMenu As Byte)
        ShowMenu(IdMenu, enumUserRole.Unknown)
    End Sub

    Public Sub ShowMenu(ByVal IdMenu As Byte, UserRole As enumUserRole)
        Dim iOpz As Byte

        IdMenuPrec = IdActiveMenu
        IdActiveMenu = IdMenu

        ShowActiveMenu(UserRole)


        'Title.Text = menu(IdMenu).Title
        'For iOpz = 1 To menu(IdMenu).QtyOpz
        '    CreateToolStripButton(IdMenu, iOpz, UserRole)
        'Next

        'ToolStrip1.Items.Clear()
        'If menu(IdMenu).QtyOpz = 1 Then
        '    ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() { _
        '                Title, tsButton1})
        'End If
        'If menu(IdMenu).QtyOpz = 2 Then
        '    ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() { _
        '                Title, tsButton1, tsButton2})
        'End If
        'If menu(IdMenu).QtyOpz = 3 Then
        '    ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() { _
        '                Title, tsButton1, tsButton2, tsButton3})
        'End If
        'If menu(IdMenu).QtyOpz = 4 Then
        '    ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() { _
        '                Title, tsButton1, tsButton2, tsButton3, tsButton4})
        'End If
        'If menu(IdMenu).QtyOpz = 5 Then
        '    ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() { _
        '                Title, tsButton1, tsButton2, tsButton3, tsButton4, tsButton5})
        'End If
        'If menu(IdMenu).QtyOpz = 6 Then
        '    ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() { _
        '                Title, tsButton1, tsButton2, tsButton3, tsButton4, tsButton5, _
        '                tsButton6})
        'End If
        'If menu(IdMenu).QtyOpz = 7 Then
        '    ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() { _
        '                Title, tsButton1, tsButton2, tsButton3, tsButton4, tsButton5, _
        '                tsButton6, tsButton7})
        'End If
        'If menu(IdMenu).QtyOpz = 8 Then
        '    ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() { _
        '                Title, tsButton1, tsButton2, tsButton3, tsButton4, tsButton5, _
        '                tsButton6, tsButton7, tsButton8})
        'End If
        'If menu(IdMenu).QtyOpz = 9 Then
        '    ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() { _
        '                Title, tsButton1, tsButton2, tsButton3, tsButton4, tsButton5, _
        '                tsButton6, tsButton7, tsButton8, tsButton9})
        'End If
        'If menu(IdMenu).QtyOpz = 10 Then
        '    ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() { _
        '                Title, tsButton1, tsButton2, tsButton3, tsButton4, tsButton5, _
        '                tsButton6, tsButton7, tsButton8, tsButton9, tsButton10})
        'End If
        'If menu(IdMenu).QtyOpz = 11 Then
        '    ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() { _
        '                Title, tsButton1, tsButton2, tsButton3, tsButton4, tsButton5, _
        '                tsButton6, tsButton7, tsButton8, tsButton9, tsButton10, _
        '                tsButton11})
        'End If
        'If menu(IdMenu).QtyOpz = 12 Then
        '    ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() { _
        '                Title, tsButton1, tsButton2, tsButton3, tsButton4, tsButton5, _
        '                tsButton6, tsButton7, tsButton8, tsButton9, tsButton10, _
        '                tsButton11, tsButton12})
        'End If

    End Sub

    Public Sub ShowActiveMenu()
        ShowActiveMenu(enumUserRole.Unknown)
    End Sub

    Public Sub ShowActiveMenu(UserRole As enumUserRole)
        Dim iOpz As Byte

        Title.Text = menu(IdActiveMenu).Title
        For iOpz = 1 To menu(IdActiveMenu).QtyOpz
            CreateToolStripButton(IdActiveMenu, iOpz, UserRole)
        Next

        ToolStrip1.Items.Clear()
        If menu(IdActiveMenu).QtyOpz = 1 Then
            ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() { _
                        Title, tsButton1})
        End If
        If menu(IdActiveMenu).QtyOpz = 2 Then
            ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() { _
                        Title, tsButton1, tsButton2})
        End If
        If menu(IdActiveMenu).QtyOpz = 3 Then
            ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() { _
                        Title, tsButton1, tsButton2, tsButton3})
        End If
        If menu(IdActiveMenu).QtyOpz = 4 Then
            ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() { _
                        Title, tsButton1, tsButton2, tsButton3, tsButton4})
        End If
        If menu(IdActiveMenu).QtyOpz = 5 Then
            ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() { _
                        Title, tsButton1, tsButton2, tsButton3, tsButton4, tsButton5})
        End If
        If menu(IdActiveMenu).QtyOpz = 6 Then
            ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() { _
                        Title, tsButton1, tsButton2, tsButton3, tsButton4, tsButton5, _
                        tsButton6})
        End If
        If menu(IdActiveMenu).QtyOpz = 7 Then
            ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() { _
                        Title, tsButton1, tsButton2, tsButton3, tsButton4, tsButton5, _
                        tsButton6, tsButton7})
        End If
        If menu(IdActiveMenu).QtyOpz = 8 Then
            ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() { _
                        Title, tsButton1, tsButton2, tsButton3, tsButton4, tsButton5, _
                        tsButton6, tsButton7, tsButton8})
        End If
        If menu(IdActiveMenu).QtyOpz = 9 Then
            ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() { _
                        Title, tsButton1, tsButton2, tsButton3, tsButton4, tsButton5, _
                        tsButton6, tsButton7, tsButton8, tsButton9})
        End If
        If menu(IdActiveMenu).QtyOpz = 10 Then
            ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() { _
                        Title, tsButton1, tsButton2, tsButton3, tsButton4, tsButton5, _
                        tsButton6, tsButton7, tsButton8, tsButton9, tsButton10})
        End If
        If menu(IdActiveMenu).QtyOpz = 11 Then
            ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() { _
                        Title, tsButton1, tsButton2, tsButton3, tsButton4, tsButton5, _
                        tsButton6, tsButton7, tsButton8, tsButton9, tsButton10, _
                        tsButton11})
        End If
        If menu(IdActiveMenu).QtyOpz = 12 Then
            ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() { _
                        Title, tsButton1, tsButton2, tsButton3, tsButton4, tsButton5, _
                        tsButton6, tsButton7, tsButton8, tsButton9, tsButton10, _
                        tsButton11, tsButton12})
        End If

    End Sub

    Public Sub EnableButtonMenu(ByVal IdMenu As Byte, ByVal ButtonName As String, ByVal Enable As Boolean)
        Dim iOpz As Byte
        If ButtonName = "*" Then
            For iOpz = 1 To menu(IdMenu).QtyOpz
                menu(IdMenu).Opz(iOpz).Enabled = Enable
                SetButtonParam(IdMenu, iOpz)
            Next iOpz
        Else
            iOpz = GetIndexOpzMenu(IdMenu, ButtonName)
            menu(IdMenu).Opz(iOpz).Enabled = Enable
            SetButtonParam(IdMenu, iOpz)
        End If
    End Sub

    Public Sub SuspendMenu()
        Dim iOpz As Byte
        For iOpz = 1 To menu(IdActiveMenu).QtyOpz
            menu(IdActiveMenu).Opz(iOpz).EnabledBeforeSuspended = menu(IdActiveMenu).Opz(iOpz).Enabled
            menu(IdActiveMenu).Opz(iOpz).Enabled = False
            SetButtonParam(IdActiveMenu, iOpz)
        Next iOpz
        IdMenuSuspended = IdActiveMenu
    End Sub
    Public Sub SuspendMenu(ByVal IdMenu As Byte)
        Dim iOpz As Byte
        For iOpz = 1 To menu(IdMenu).QtyOpz
            menu(IdMenu).Opz(iOpz).EnabledBeforeSuspended = menu(IdActiveMenu).Opz(iOpz).Enabled
            menu(IdMenu).Opz(iOpz).Enabled = False
            SetButtonParam(IdMenu, iOpz)
        Next iOpz
        IdMenuSuspended = IdMenu
    End Sub
    Public Sub ActivateMenuSuspended(UserRole As enumUserRole)
        If IdMenuSuspended <> 0 Then
            For iOpz As Byte = 1 To menu(IdMenuSuspended).QtyOpz
                menu(IdMenuSuspended).Opz(iOpz).Enabled = menu(IdMenuSuspended).Opz(iOpz).EnabledBeforeSuspended

                If menu(IdMenuSuspended).Opz(iOpz).Enabled Then
                    menu(IdMenuSuspended).Opz(iOpz).Enabled = EnableOpzByUser(IdMenuSuspended, iOpz, UserRole)
                End If

                SetButtonParam(IdMenuSuspended, iOpz)
            Next iOpz
            IdMenuSuspended = 0
        End If
    End Sub


    Public Sub SetSwitchStatus(ByVal IdMenu As Byte, ByVal ButtonName As String, ByVal SwitchStatus As Boolean)
        Dim iOpz As Byte
        iOpz = GetIndexOpzMenu(IdMenu, ButtonName)
        If iOpz <> 0 Then
            If SwitchStatus <> menu(IdMenu).Opz(iOpz).SwitchStatus Then
                menu(IdMenu).Opz(iOpz).SwitchStatus = SwitchStatus
                If IdActiveMenu = IdMenu Then
                    ShowMenu(IdMenu)
                End If
            End If
        End If
    End Sub


    Public Sub ButtonMenuText(ByVal IdMenu As Byte, ByVal IdButton As Byte, ByVal Caption As String)

        With menu(IdMenu).Opz(IdButton)
            If FunctionKeyEnabled Then
                If .Name <> "Fine" Then
                    If .Name = "Chiudi" Then
                        .Text = "(F12) " & GetText(Caption)
                    Else
                        .Text = "(F" & Format(IdButton) & ") " & GetText(Caption)
                    End If

                Else
                    .Text = "(Alt-F10) " & GetText(Caption)
                End If
            Else
                .Text = GetText(Caption)
            End If
            .ToolTipText = GetToolTipText(Caption)
            If Len(.Name) = 0 Then
                .Text = " "
                .ToolTipText = " "
            End If
        End With
    End Sub


    Public Sub ButtonMenuImage(ByVal IdMenu As Byte, ByVal IdButton As Byte, ByVal Image As System.Drawing.Image)
        Try
            menu(IdMenu).Opz(IdButton).Image = Image
        Catch ex As Exception

        End Try

    End Sub

#End Region



#Region "Private"

    Private Sub mForm_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles mForm.KeyUp

        If IdMenuSuspended = IdActiveMenu Then Return

        Dim iOpz As Byte
        If e.Modifiers = Keys.Alt Then
            If e.KeyCode = Keys.F10 Then
                iOpz = GetIndexOpzMenu(IdActiveMenu, "Fine")
                If iOpz <> 0 Then
                    If menu(IdActiveMenu).Opz(12).Enabled Then
                        RaiseEvent IdButtonChoosed(IdActiveMenu, iOpz, IdMenuPrec)
                        RaiseEvent TextButtonChoosed(IdActiveMenu, menu(IdActiveMenu).Opz(iOpz).Name, IdMenuPrec)
                    End If
                End If
            End If
        Else
            Select Case e.KeyCode
                Case Keys.Escape
                    RaiseEvent TextButtonChoosed(IdActiveMenu, "Escape", IdMenuPrec)
                Case Keys.Return
                    RaiseEvent TextButtonChoosed(IdActiveMenu, "Return", IdMenuPrec)
                Case Keys.Space
                    RaiseEvent TextButtonChoosed(IdActiveMenu, "Space", IdMenuPrec)
                Case Keys.F1
                    If menu(IdActiveMenu).QtyOpz >= 1 Then
                        If menu(IdActiveMenu).Opz(1).Enabled Then
                            RaiseEvent IdButtonChoosed(IdActiveMenu, 1, IdMenuPrec)
                            RaiseEvent TextButtonChoosed(IdActiveMenu, menu(IdActiveMenu).Opz(1).Name, IdMenuPrec)
                            ChangeSwitchStatus(1)
                        End If
                    End If
                Case Keys.F2
                    If menu(IdActiveMenu).QtyOpz >= 2 Then
                        If menu(IdActiveMenu).Opz(2).Enabled Then
                            RaiseEvent IdButtonChoosed(IdActiveMenu, 2, IdMenuPrec)
                            RaiseEvent TextButtonChoosed(IdActiveMenu, menu(IdActiveMenu).Opz(2).Name, IdMenuPrec)
                            ChangeSwitchStatus(2)
                        End If
                    End If
                Case Keys.F3
                    If menu(IdActiveMenu).QtyOpz >= 3 Then
                        If menu(IdActiveMenu).Opz(3).Enabled Then
                            RaiseEvent IdButtonChoosed(IdActiveMenu, 3, IdMenuPrec)
                            RaiseEvent TextButtonChoosed(IdActiveMenu, menu(IdActiveMenu).Opz(3).Name, IdMenuPrec)
                            ChangeSwitchStatus(3)
                        End If
                    End If
                Case Keys.F4
                    If menu(IdActiveMenu).QtyOpz >= 4 Then
                        If menu(IdActiveMenu).Opz(4).Enabled Then
                            RaiseEvent IdButtonChoosed(IdActiveMenu, 4, IdMenuPrec)
                            RaiseEvent TextButtonChoosed(IdActiveMenu, menu(IdActiveMenu).Opz(4).Name, IdMenuPrec)
                            ChangeSwitchStatus(4)
                        End If
                    End If
                Case Keys.F5
                    If menu(IdActiveMenu).QtyOpz >= 5 Then
                        If menu(IdActiveMenu).Opz(5).Enabled Then
                            RaiseEvent IdButtonChoosed(IdActiveMenu, 5, IdMenuPrec)
                            RaiseEvent TextButtonChoosed(IdActiveMenu, menu(IdActiveMenu).Opz(5).Name, IdMenuPrec)
                            ChangeSwitchStatus(5)
                        End If
                    End If
                Case Keys.F6
                    If menu(IdActiveMenu).QtyOpz >= 6 Then
                        If menu(IdActiveMenu).Opz(6).Enabled Then
                            RaiseEvent IdButtonChoosed(IdActiveMenu, 6, IdMenuPrec)
                            RaiseEvent TextButtonChoosed(IdActiveMenu, menu(IdActiveMenu).Opz(6).Name, IdMenuPrec)
                            ChangeSwitchStatus(6)
                        End If
                    End If
                Case Keys.F7
                    If menu(IdActiveMenu).QtyOpz >= 7 Then
                        If menu(IdActiveMenu).Opz(7).Enabled Then
                            RaiseEvent IdButtonChoosed(IdActiveMenu, 7, IdMenuPrec)
                            RaiseEvent TextButtonChoosed(IdActiveMenu, menu(IdActiveMenu).Opz(7).Name, IdMenuPrec)
                            ChangeSwitchStatus(7)
                        End If
                    End If
                Case Keys.F8
                    If menu(IdActiveMenu).QtyOpz >= 8 Then
                        If menu(IdActiveMenu).Opz(8).Enabled Then
                            RaiseEvent IdButtonChoosed(IdActiveMenu, 8, IdMenuPrec)
                            RaiseEvent TextButtonChoosed(IdActiveMenu, menu(IdActiveMenu).Opz(8).Name, IdMenuPrec)
                            ChangeSwitchStatus(8)
                        End If
                    End If
                Case Keys.F9
                    If menu(IdActiveMenu).QtyOpz >= 9 Then
                        If menu(IdActiveMenu).Opz(9).Enabled Then
                            RaiseEvent IdButtonChoosed(IdActiveMenu, 9, IdMenuPrec)
                            RaiseEvent TextButtonChoosed(IdActiveMenu, menu(IdActiveMenu).Opz(9).Name, IdMenuPrec)
                            ChangeSwitchStatus(9)
                        End If
                    End If
                Case Keys.F10
                    If menu(IdActiveMenu).QtyOpz >= 10 Then
                        If menu(IdActiveMenu).Opz(10).Enabled Then
                            RaiseEvent IdButtonChoosed(IdActiveMenu, 10, IdMenuPrec)
                            RaiseEvent TextButtonChoosed(IdActiveMenu, menu(IdActiveMenu).Opz(10).Name, IdMenuPrec)
                            ChangeSwitchStatus(10)
                        End If
                    End If
                Case Keys.F11
                    If menu(IdActiveMenu).QtyOpz >= 11 Then
                        If menu(IdActiveMenu).Opz(11).Enabled Then
                            RaiseEvent IdButtonChoosed(IdActiveMenu, 11, IdMenuPrec)
                            RaiseEvent TextButtonChoosed(IdActiveMenu, menu(IdActiveMenu).Opz(11).Name, IdMenuPrec)
                            ChangeSwitchStatus(11)
                        End If
                    End If
                Case Keys.F12
                    If menu(IdActiveMenu).QtyOpz >= 12 Then
                        iOpz = GetIndexOpzMenu(IdActiveMenu, "Fine")
                        If iOpz = 0 Then
                            If menu(IdActiveMenu).Opz(12).Enabled Then
                                RaiseEvent IdButtonChoosed(IdActiveMenu, 12, IdMenuPrec)
                                RaiseEvent TextButtonChoosed(IdActiveMenu, menu(IdActiveMenu).Opz(12).Name, IdMenuPrec)
                                ChangeSwitchStatus(12)
                            End If
                        End If
                    Else
                        iOpz = GetIndexOpzMenu(IdActiveMenu, "Chiudi")
                        If iOpz <> 0 Then
                            If menu(IdActiveMenu).Opz(iOpz).Enabled Then
                                RaiseEvent IdButtonChoosed(IdActiveMenu, iOpz, IdMenuPrec)
                                RaiseEvent TextButtonChoosed(IdActiveMenu, menu(IdActiveMenu).Opz(iOpz).Name, IdMenuPrec)
                                ChangeSwitchStatus(iOpz)
                            End If
                        End If
                    End If
            End Select
        End If

        'End If
    End Sub

    Private Function GetText(ByVal sRiga As String) As String
        Dim i As Int16
        i = InStr(sRiga, ",")
        If i <> 0 Then
            GetText = Mid(sRiga, 1, i - 1)
        Else
            GetText = sRiga
        End If
    End Function
    Private Function GetToolTipText(ByVal sRiga As String) As String
        Dim i As Int16
        i = InStr(sRiga, ",")
        If i <> 0 Then
            GetToolTipText = Mid(sRiga, i + 1)
        Else
            GetToolTipText = sRiga
        End If
    End Function
    Private Function GetIndexOpzMenu(ByVal IdMenu As Integer, ByVal Nome As String) As Byte
        'Ritorna l'indice dell'opzione del menu 'iMenu' corrispondente al nome 'Nome'
        'Ritorna 0 se non trova l'indice
        Dim iOpz As Integer
        For iOpz = 1 To menu(IdMenu).QtyOpz
            If menu(IdMenu).Opz(iOpz).Name = Nome Then
                GetIndexOpzMenu = iOpz
                Exit Function
            End If
        Next iOpz
        GetIndexOpzMenu = 0
    End Function
    Private Sub CreateToolStripButton(ByVal IdMenu As Byte, ByVal IdOpz As Byte, UserRole As enumUserRole)
        Dim mButton As ToolStripButton

        mButton = New System.Windows.Forms.ToolStripButton()
        mButton.Name = menu(IdMenu).Opz(IdOpz).Name
        mButton.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText
        mButton.Text = menu(IdMenu).Opz(IdOpz).Text
        mButton.TextAlign = ContentAlignment.MiddleCenter
        mButton.TextImageRelation = TextImageRelation.ImageAboveText
        mButton.Image = menu(IdMenu).Opz(IdOpz).Image
        mButton.ToolTipText = menu(IdMenu).Opz(IdOpz).ToolTipText
        mButton.Visible = menu(IdMenu).Opz(IdOpz).Visible
        mButton.Enabled = menu(IdMenu).Opz(IdOpz).Enabled

        If menu(IdMenu).Opz(IdOpz).Enabled Then
            If menu(IdMenu).Opz(IdOpz).UserRoleEnabled <> enumUserRole.Unknown Then
                If UserRole <> menu(IdMenu).Opz(IdOpz).UserRoleEnabled Then
                    Select Case UserRole
                        Case enumUserRole.Unknown
                            mButton.Enabled = False
                        Case enumUserRole.User
                        Case enumUserRole.Maintenance
                            If menu(IdMenu).Opz(IdOpz).UserRoleEnabled = enumUserRole.User Then
                            Else
                                mButton.Enabled = False
                            End If
                        Case enumUserRole.Administrator
                            If menu(IdMenu).Opz(IdOpz).UserRoleEnabled = enumUserRole.User Or menu(IdMenu).Opz(IdOpz).UserRoleEnabled = enumUserRole.Maintenance Then
                            Else
                                mButton.Enabled = False
                            End If
                        Case enumUserRole.Master
                            If menu(IdMenu).Opz(IdOpz).UserRoleEnabled = enumUserRole.User Or menu(IdMenu).Opz(IdOpz).UserRoleEnabled = enumUserRole.Maintenance Or
                                 menu(IdMenu).Opz(IdOpz).UserRoleEnabled = enumUserRole.Administrator Then
                            Else
                                mButton.Enabled = False
                            End If
                    End Select
                End If
            End If
        End If

        If menu(IdMenu).Opz(IdOpz).SwitchButton Then
            mButton.CheckOnClick = True
            If menu(IdMenu).Opz(IdOpz).SwitchStatus Then
                mButton.CheckState = CheckState.Checked
            Else
                mButton.CheckState = CheckState.Unchecked
            End If
        Else
            mButton.CheckOnClick = False
            mButton.CheckState = CheckState.Unchecked
        End If

        If IdOpz = 1 Then tsButton1 = mButton
        If IdOpz = 2 Then tsButton2 = mButton
        If IdOpz = 3 Then tsButton3 = mButton
        If IdOpz = 4 Then tsButton4 = mButton
        If IdOpz = 5 Then tsButton5 = mButton
        If IdOpz = 6 Then tsButton6 = mButton
        If IdOpz = 7 Then tsButton7 = mButton
        If IdOpz = 8 Then tsButton8 = mButton
        If IdOpz = 9 Then tsButton9 = mButton
        If IdOpz = 10 Then tsButton10 = mButton
        If IdOpz = 11 Then tsButton11 = mButton
        If IdOpz = 12 Then tsButton12 = mButton
    End Sub


    Private Function EnableOpzByUser(ByVal IdMenu As Byte, ByVal IdOpz As Byte, UserRole As enumUserRole) As Boolean

        If menu(IdMenu).Opz(IdOpz).Enabled Then
            If menu(IdMenu).Opz(IdOpz).UserRoleEnabled <> enumUserRole.Unknown Then
                If UserRole <> menu(IdMenu).Opz(IdOpz).UserRoleEnabled Then
                    Select Case UserRole
                        Case enumUserRole.Unknown
                            Return False
                        Case enumUserRole.User
                        Case enumUserRole.Maintenance
                            If menu(IdMenu).Opz(IdOpz).UserRoleEnabled = enumUserRole.User Then
                                Return True
                            Else
                                Return False
                            End If
                        Case enumUserRole.Administrator
                            If menu(IdMenu).Opz(IdOpz).UserRoleEnabled = enumUserRole.User Or menu(IdMenu).Opz(IdOpz).UserRoleEnabled = enumUserRole.Maintenance Then
                                Return True
                            Else
                                Return False
                            End If
                        Case enumUserRole.Master
                            If menu(IdMenu).Opz(IdOpz).UserRoleEnabled = enumUserRole.User Or menu(IdMenu).Opz(IdOpz).UserRoleEnabled = enumUserRole.Maintenance Or
                                 menu(IdMenu).Opz(IdOpz).UserRoleEnabled = enumUserRole.Administrator Then
                                Return True
                            Else
                                Return False
                            End If
                    End Select
                Else
                    Return True
                End If
            Else
                Return True
            End If
        End If

    End Function


    Private Sub SetButtonParam(ByVal IdMenu As Byte, ByVal IdOpz As Byte)
        Dim mButton As ToolStripButton

        mButton = New System.Windows.Forms.ToolStripButton()
        If IdOpz = 1 Then mButton = tsButton1
        If IdOpz = 2 Then mButton = tsButton2
        If IdOpz = 3 Then mButton = tsButton3
        If IdOpz = 4 Then mButton = tsButton4
        If IdOpz = 5 Then mButton = tsButton5
        If IdOpz = 6 Then mButton = tsButton6
        If IdOpz = 7 Then mButton = tsButton7
        If IdOpz = 8 Then mButton = tsButton8
        If IdOpz = 9 Then mButton = tsButton9
        If IdOpz = 10 Then mButton = tsButton10
        If IdOpz = 11 Then mButton = tsButton11
        If IdOpz = 12 Then mButton = tsButton12

        mButton.Name = menu(IdMenu).Opz(IdOpz).Name
        mButton.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText
        mButton.Text = menu(IdMenu).Opz(IdOpz).Text
        mButton.TextAlign = ContentAlignment.MiddleCenter
        mButton.TextImageRelation = TextImageRelation.ImageAboveText
        mButton.Image = menu(IdMenu).Opz(IdOpz).Image
        mButton.ToolTipText = menu(IdMenu).Opz(IdOpz).ToolTipText
        mButton.Visible = menu(IdMenu).Opz(IdOpz).Visible
        mButton.Enabled = menu(IdMenu).Opz(IdOpz).Enabled
        If menu(IdMenu).Opz(IdOpz).SwitchButton Then
            mButton.CheckOnClick = True
            If menu(IdMenu).Opz(IdOpz).SwitchStatus Then
                mButton.CheckState = CheckState.Checked
            Else
                mButton.CheckState = CheckState.Unchecked
            End If
        Else
            mButton.CheckOnClick = False
            mButton.CheckState = CheckState.Unchecked
        End If

    End Sub
    Private Sub ChangeSwitchStatus(ByVal IdOpz As Byte)
        If menu(IdActiveMenu).Opz(IdOpz).SwitchButton Then
            If menu(IdActiveMenu).Opz(IdOpz).SwitchStatus Then
                menu(IdActiveMenu).Opz(IdOpz).SwitchStatus = False
            Else
                menu(IdActiveMenu).Opz(IdOpz).SwitchStatus = True
            End If
        Else
            menu(IdActiveMenu).Opz(IdOpz).SwitchStatus = False
        End If
    End Sub
    Private Sub tsButton1_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles tsButton1.Click
        RaiseEvent IdButtonChoosed(IdActiveMenu, 1, IdMenuPrec)
        RaiseEvent TextButtonChoosed(IdActiveMenu, menu(IdActiveMenu).Opz(1).Name, IdMenuPrec)
        ChangeSwitchStatus(1)
    End Sub
    Private Sub tsButton2_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles tsButton2.Click
        RaiseEvent IdButtonChoosed(IdActiveMenu, 2, IdMenuPrec)
        RaiseEvent TextButtonChoosed(IdActiveMenu, menu(IdActiveMenu).Opz(2).Name, IdMenuPrec)
        ChangeSwitchStatus(2)
    End Sub
    Private Sub tsButton3_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles tsButton3.Click
        RaiseEvent IdButtonChoosed(IdActiveMenu, 3, IdMenuPrec)
        RaiseEvent TextButtonChoosed(IdActiveMenu, menu(IdActiveMenu).Opz(3).Name, IdMenuPrec)
        ChangeSwitchStatus(3)
    End Sub
    Private Sub tsButton4_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles tsButton4.Click
        RaiseEvent IdButtonChoosed(IdActiveMenu, 4, IdMenuPrec)
        RaiseEvent TextButtonChoosed(IdActiveMenu, menu(IdActiveMenu).Opz(4).Name, IdMenuPrec)
        ChangeSwitchStatus(4)
    End Sub
    Private Sub tsButton5_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles tsButton5.Click
        RaiseEvent IdButtonChoosed(IdActiveMenu, 5, IdMenuPrec)
        RaiseEvent TextButtonChoosed(IdActiveMenu, menu(IdActiveMenu).Opz(5).Name, IdMenuPrec)
        ChangeSwitchStatus(5)
    End Sub
    Private Sub tsButton6_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles tsButton6.Click
        RaiseEvent IdButtonChoosed(IdActiveMenu, 6, IdMenuPrec)
        RaiseEvent TextButtonChoosed(IdActiveMenu, menu(IdActiveMenu).Opz(6).Name, IdMenuPrec)
        ChangeSwitchStatus(6)
    End Sub
    Private Sub tsButton7_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles tsButton7.Click
        RaiseEvent IdButtonChoosed(IdActiveMenu, 7, IdMenuPrec)
        RaiseEvent TextButtonChoosed(IdActiveMenu, menu(IdActiveMenu).Opz(7).Name, IdMenuPrec)
        ChangeSwitchStatus(7)
    End Sub
    Private Sub tsButton8_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles tsButton8.Click
        RaiseEvent IdButtonChoosed(IdActiveMenu, 8, IdMenuPrec)
        RaiseEvent TextButtonChoosed(IdActiveMenu, menu(IdActiveMenu).Opz(8).Name, IdMenuPrec)
        ChangeSwitchStatus(8)
    End Sub
    Private Sub tsButton9_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles tsButton9.Click
        RaiseEvent IdButtonChoosed(IdActiveMenu, 9, IdMenuPrec)
        RaiseEvent TextButtonChoosed(IdActiveMenu, menu(IdActiveMenu).Opz(9).Name, IdMenuPrec)
        ChangeSwitchStatus(9)
    End Sub
    Private Sub tsButton10_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles tsButton10.Click
        RaiseEvent IdButtonChoosed(IdActiveMenu, 10, IdMenuPrec)
        RaiseEvent TextButtonChoosed(IdActiveMenu, menu(IdActiveMenu).Opz(10).Name, IdMenuPrec)
        ChangeSwitchStatus(10)
    End Sub
    Private Sub tsButton11_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles tsButton11.Click
        RaiseEvent IdButtonChoosed(IdActiveMenu, 11, IdMenuPrec)
        RaiseEvent TextButtonChoosed(IdActiveMenu, menu(IdActiveMenu).Opz(11).Name, IdMenuPrec)
        ChangeSwitchStatus(11)
    End Sub
    Private Sub tsButton12_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles tsButton12.Click
        RaiseEvent IdButtonChoosed(IdActiveMenu, 12, IdMenuPrec)
        RaiseEvent TextButtonChoosed(IdActiveMenu, menu(IdActiveMenu).Opz(12).Name, IdMenuPrec)
        ChangeSwitchStatus(12)
    End Sub



#End Region


#Region "Costrutturi"

    Public Sub New()

        ' Chiamata richiesta dalla finestra di progettazione.
        InitializeComponent()

        ' Aggiungere le eventuali istruzioni di inizializzazione dopo la chiamata a InitializeComponent().

    End Sub

#End Region



End Class
