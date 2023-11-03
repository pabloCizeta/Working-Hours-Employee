Imports System.Windows.Forms
Imports System.Drawing
Imports core


Public Class ucMenu

    Private WithEvents mForm As System.Windows.Forms.Form

    Private Enum enumMenuStatus
        Idle
        Active
        Suspended
    End Enum

    Private Class udtButton
        Public Property Name As String = ""
        Public Property Image As System.Drawing.Image = Nothing
        Public Property Text As String = ""
        Public Property HotKey As String = ""
        Public Property Enabled As Boolean = False
        Public Property Visible As Boolean = False
        Public Property Type As Configuration.MenusConfig.enumButtonType = Configuration.MenusConfig.enumButtonType.Push
        Public Property Checked As CheckState = CheckState.Unchecked
        Public Property tsButton As New ToolStripButton
        Public Sub New()
        End Sub
        Public Overloads Function DeepCopy() As udtButton
            Dim other As udtButton = DirectCast(Me.MemberwiseClone(), udtButton)
            Return other
        End Function
    End Class

    Private Class udtMenu
        Public Property Name As String = ""
        Public Property Heading As String = ""
        Public Property FunctionKeyEnabled As Boolean = True
        Public Property Status As enumMenuStatus = enumMenuStatus.Idle
        Public Property Buttons As New List(Of udtButton)
        Public Sub New()

        End Sub
        Public Overloads Function DeepCopy() As udtMenu
            Dim other As udtMenu = DirectCast(Me.MemberwiseClone(), udtMenu)
            other.Buttons.Clear()
            For Each item As udtButton In Buttons
                other.Buttons.Add(item)
            Next
            Return other
        End Function
    End Class



    Private menus As New List(Of udtMenu)

    Private mnuActive As New udtMenu
    Private PrevMenu As New udtMenu

    Public Event ButtonClick(MenuName As String, ByVal ButtonName As String, Checked As CheckState)


#Region "Methods"

    Public Sub CreateMenu(ByVal xForm As Form, FormMenus As Configuration.MenusConfig.udtFormMenus)
        mForm = xForm
        mForm.KeyPreview = True


        menus.Clear()
        For Each item As Configuration.MenusConfig.udtMenu In FormMenus.Menus
            Dim buttons As New List(Of udtButton)
            For Each b As Configuration.MenusConfig.udtMenuButtonConfig In item.Buttons
                'Dim enab As Boolean = True

                Dim enab As Boolean = (xUsers.UserLogged.Role = Users.enumUserRole.Master)
                For Each u As Users.enumUserRole In b.EnabledUsers
                    If xUsers.UserLogged.Role = u Then
                        enab = True
                    End If
                Next


                Dim ImageFileName As String = ""  'b.ImageFileName
                If FormMenus.PicturesFolder <> Application.StartupPath & "\..\Data\Pictures\" Then
                    ImageFileName = Application.StartupPath & "\..\Data\Pictures\"
                Else
                    ImageFileName = Application.StartupPath
                End If
                If Not ImageFileName.EndsWith("\") Then ImageFileName += "\"
                ImageFileName += b.ImageFileName

                buttons.Add(New udtButton With {.Name = b.Name,
                                                .HotKey = b.HotKey,
                                               .Text = FormatText(b, item.FunctionKeyEnabled),
                                               .Image = Image.FromFile(ImageFileName),
                                                .Type = b.Type,
                                                .Enabled = (b.Enabled And enab And (b.Name <> "")),
                                                .Visible = True
                                               })
            Next

            Dim Heading As String = item.Heading.Ita


            menus.Add(New udtMenu With {.Name = item.Name, .Heading = Heading,
                                        .FunctionKeyEnabled = item.FunctionKeyEnabled,
                                        .Status = enumMenuStatus.Idle,
                                        .Buttons = buttons})
        Next


    End Sub



    Public Sub ShowMenu(mnuName As String)

        PrevMenu = GetMenu(mnuActive.Name)

        mnuActive = GetMenu(mnuName)

        ShowMenu()

    End Sub
    Public Sub ShowPreviousMenu()

        mnuActive = GetMenu(PrevMenu.Name)

        ShowMenu()

    End Sub

    Private Sub ShowMenu()

        ToolStrip1.Items.Clear()

        Dim Heading As New ToolStripLabel
        Heading.Text = mnuActive.Heading
        Heading.ForeColor = Color.Blue
        Heading.BackColor = Color.Transparent
        Heading.TextAlign = ContentAlignment.MiddleCenter
        ToolStrip1.Items.Add(Heading)

        Dim Separator As New ToolStripSeparator
        ToolStrip1.Items.Add(Separator)

        CreateToolStripButtons(mnuActive.Buttons)

        mnuActive.Status = enumMenuStatus.Active

    End Sub
    Public Sub SuspendMenu()
        mnuActive.Status = enumMenuStatus.Suspended

        For Each item As Object In ToolStrip1.Items
            If TypeOf (item) Is ToolStripButton Then
                item.Enabled = False
            End If
        Next
       
    End Sub
    Public Sub ActivateMenuSuspended()
        mnuActive.Status = enumMenuStatus.Active
        ShowMenu(mnuActive.Name)
    End Sub

    Public Sub EnableButton(mnuName As String, btnName As String, Enabled As Boolean)
        Dim mnu As udtMenu = GetMenu(mnuName)

        For Each item As udtButton In mnu.Buttons
            If item.Name = btnName Then
                item.Enabled = Enabled
                Exit For
            End If
        Next
    End Sub
    Public Sub EnableButton(btnName As String, Enabled As Boolean)

        For Each item As Object In ToolStrip1.Items
            If TypeOf (item) Is ToolStripButton Then
                If item.Name = btnName Then
                    item.enabled = Enabled
                    Exit For
                End If
            End If
        Next

    End Sub

    Public Function IsButtonEnabled(btnName As String) As Boolean
        For Each item As Object In ToolStrip1.Items
            If TypeOf (item) Is ToolStripButton Then
                If item.Name = btnName Then
                    Return item.enabled
                End If
            End If
        Next
        Return False
    End Function

#End Region



#Region "Private"


    Private Sub mForm_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles mForm.KeyUp
        Dim ButtonPressed As New udtButton

        If IsNothing(mnuActive) Then Return
        If mnuActive.Status = enumMenuStatus.Suspended Then Return

        Dim KeyCode As String = ""
        If e.Modifiers = Keys.Alt Then
            KeyCode = "Alt-"
        ElseIf e.Modifiers = Keys.Control Then
            KeyCode = "Ctrl-"
        End If

        Select Case e.KeyCode
            Case Keys.Escape
                RaiseEvent ButtonClick(mnuActive.Name, "Escape", False)
            Case Keys.F1, Keys.F2, Keys.F3, Keys.F4, Keys.F5, Keys.F6, Keys.F7, Keys.F8, Keys.F9, Keys.F10, Keys.F11, Keys.F12
                If mnuActive.FunctionKeyEnabled Then
                    KeyCode += e.KeyCode.ToString
                    ButtonPressed = GetButtonByHotKey(KeyCode)
                    If Not IsNothing(ButtonPressed) Then
                        RaiseEvent ButtonClick(mnuActive.Name, ButtonPressed.Name, False)
                        ChangeSwitchStatus(ButtonPressed.Name)
                    End If
                End If
        End Select

    End Sub

    'Private Function FormatText(Button As Configuration.MenusConfig.udtMenuButtonConfig, FunctionKeyEnabled As Boolean) As String

    '    If Button.Name.Length = 0 Then
    '        Return "     "
    '    End If

    '    If FunctionKeyEnabled Then
    '        If IsNothing(multiLang) Then
    '            Return "(" & Button.HotKey & ") " & multiL.GetMsgText(Button.Text)
    '        Else
    '            Return "(" & Button.HotKey & ") " & multiLang.GetMsgText(Button.Text)
    '        End If
    '    Else
    '        If IsNothing(multiLang) Then
    '            Return multiL.GetMsgText(Button.Text)
    '        Else
    '            Return multiLang.GetMsgText(Button.Text)
    '        End If
    '    End If

    'End Function
    Private Function FormatText(Button As Configuration.MenusConfig.udtMenuButtonConfig, FunctionKeyEnabled As Boolean) As String

        If Button.Name.Length = 0 Then
            Return "     "
        End If

        Dim Text As String = Button.Text.Ita


        If FunctionKeyEnabled Then
            Return "(" & Button.HotKey & ") " & Text
        Else
            Return Text
        End If


    End Function

    Private Function GetMenu(mnuName As String) As udtMenu

        For Each item As udtMenu In menus
            If item.Name = mnuName Then
                Return item
            End If
        Next

        Return Nothing

    End Function
    Private Function GetButton(mnuName As String, btnName As String) As udtButton

        Dim mnu As udtMenu = GetMenu(mnuName)

        For Each item As udtButton In mnu.Buttons
            If item.Name = btnName Then
                Return item
            End If
        Next

        Return Nothing

    End Function
    Private Function GetButtonByHotKey(HotKey As String) As udtButton

        For Each item As udtButton In mnuActive.Buttons
            If item.Enabled Then
                If item.HotKey = HotKey Then
                    Return item
                End If
            End If
        Next

        Return Nothing

    End Function

    Private Sub CreateToolStripButtons(ByVal Buttons As List(Of udtButton))
        Dim mButton As New ToolStripButton




        For Each item As udtButton In Buttons
            Dim aryStr() As String = item.Text.Split(",")
            mButton = New ToolStripButton
            mButton.Name = item.Name
            mButton.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText
            mButton.Text = aryStr(0)  ' item.Text
            If aryStr.Length > 1 Then
                mButton.ToolTipText = aryStr(1)
            Else
                mButton.ToolTipText = aryStr(0)
            End If
            mButton.TextAlign = ContentAlignment.MiddleCenter
            mButton.TextImageRelation = TextImageRelation.ImageAboveText
            mButton.Image = item.Image
            mButton.Visible = item.Visible
            mButton.Enabled = item.Enabled
            mButton.CheckState = CheckState.Unchecked

            AddHandler mButton.Click, AddressOf ButtonClickHandler

            ToolStrip1.Items.Add(mButton)

        Next


    End Sub

    Private Sub ButtonClickHandler(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim tsb As ToolStripButton = DirectCast(sender, ToolStripButton)

        Dim btn As New udtButton
        btn = GetButton(mnuActive.Name, tsb.Name)
        If btn.Type = Configuration.MenusConfig.enumButtonType.Switch Then
            btn.Checked = tsb.Checked
        End If
        RaiseEvent ButtonClick(mnuActive.Name, btn.Name, btn.Checked)

    End Sub

    Private Sub ChangeSwitchStatus(ButtonName As String)

        For Each item As udtButton In mnuActive.Buttons
            If item.Name = ButtonName Then
                If item.Checked = CheckState.Checked Then
                    item.Checked = CheckState.Unchecked
                Else
                    item.Checked = CheckState.Checked
                End If
                Exit For
            End If
        Next

    End Sub


#End Region


End Class
