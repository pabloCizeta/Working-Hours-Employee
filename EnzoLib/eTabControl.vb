Imports System.Windows.Forms
Imports System.Drawing

Public Class eTabControl
    Inherits TabControl

    Private PreviousTab As New TabPage
    Private CurrentTab As New TabPage

    Private TabsTestDone As New List(Of Integer)
    Private TabsWithScrap As New List(Of Integer)
    Private MaskedIndex As Integer = -1

    Private Initialized As Boolean = False

    Public Event NewTabSelected(PageSelected As TabPage)


    Public Sub Init(Appearance As TabAppearance)

        Me.DrawMode = TabDrawMode.OwnerDrawFixed
        Me.Appearance = Appearance  'TabAppearance.Buttons
        'Me.SizeMode = TabSizeMode.FillToRight

        AddHandler Me.DrawItem, AddressOf DrawTabPage

        For Each TP As TabPage In Me.TabPages
            TP.BorderStyle = BorderStyle.Fixed3D
            TP.Text += "  "
        Next

        Initialized = True

        MyBase.Refresh()

    End Sub


    Public Sub SetResults(TabsTestDone As List(Of Integer), TabsWithScrap As List(Of Integer), MaskedIndex As Integer)

        Me.MaskedIndex = MaskedIndex

        Me.TabsTestDone = New List(Of Integer)
        For Each item As Integer In TabsTestDone
            Me.TabsTestDone.Add(item)
        Next

        Me.TabsWithScrap = New List(Of Integer)
        For Each item As Integer In TabsWithScrap
            Me.TabsWithScrap.Add(item)
        Next

        MyBase.Refresh()

    End Sub

    Private Sub DrawTabPage(tabctl As TabControl, e As DrawItemEventArgs)

        Dim g As Graphics = e.Graphics
        Dim font As Font = tabctl.Font
        Dim brush As New SolidBrush(Color.Black)
        Dim tabTextArea As RectangleF = RectangleF.op_Implicit(tabctl.GetTabRect(e.Index))
        tabctl.TabPages(e.Index).SetBounds(0, 0, 0, 0)
        ' tabctl.TabPages(e.Index).Enabled = True
        If tabctl.SelectedIndex = e.Index Then
            g.FillRectangle(Brushes.LightCyan, tabTextArea)
            'brush = New SolidBrush(Color.Blue)
            'font = New Font(font, FontStyle.Bold)
        Else
            Dim b As Drawing.Brush = New SolidBrush(SystemColors.Control)
            g.FillRectangle(b, tabTextArea)
        End If
        If TabsWithScrap.Contains(e.Index) Then
            font = New Font(font, FontStyle.Bold)
            brush = New SolidBrush(Color.Red)
            'g.FillRectangle(Brushes.LightSkyBlue, tabTextArea)
        Else
            If e.Index <> MaskedIndex Then
                If TabsTestDone.Contains(e.Index) Then
                    brush = New SolidBrush(Color.ForestGreen)
                    font = New Font(font, FontStyle.Bold)
                    'g.FillRectangle(Brushes.LightSkyBlue, tabTextArea)
                Else
                    brush = New SolidBrush(Color.FromArgb(40, Color.Gray))  'SystemColors.InactiveCaption)
                    g.FillRectangle(brush, tabTextArea)

                    brush = New SolidBrush(Color.DarkGray)
                    font = New Font(tabctl.Font, FontStyle.Italic)
                    ' tabctl.TabPages(e.Index).Enabled = False

                End If
            End If
        End If


        If Not IsNothing(Me.ImageList) Then
            g.DrawImage(ImageList.Images(e.Index), tabTextArea.X + 3, tabTextArea.Y + 3)
            g.DrawString(tabctl.TabPages(e.Index).Text, font, brush, tabTextArea.X + 20, tabTextArea.Y + 3)
        Else
            g.DrawString(tabctl.TabPages(e.Index).Text, font, brush, tabTextArea.X + 1, tabTextArea.Y + 3)
        End If

    End Sub


    Private Sub eTabControl_Selecting(sender As Object, e As TabControlCancelEventArgs) Handles Me.Selecting

        If Not Initialized Then Return

        If Not (TabsTestDone.Contains(e.TabPageIndex)) Then
            e.Cancel = True
        Else
            RaiseEvent NewTabSelected(CurrentTab)
        End If

    End Sub

    'Private Sub tabResults_Deselected(ByVal sender As Object, ByVal e As System.Windows.Forms.TabControlEventArgs) Handles Me.Deselected
    '    PreviousTab = e.TabPage
    'End Sub
    'Private Sub tabResults_Selected(ByVal sender As Object, ByVal e As System.Windows.Forms.TabControlEventArgs) Handles Me.Selected

    '    If Not Initialized Then Return

    '    Static IsActive As Boolean = False
    '    If IsActive Then Return
    '    IsActive = True
    '    CurrentTab = e.TabPage
    '    If Not IsNothing(CurrentTab) Then
    '        If (PreviousTab.Name <> CurrentTab.Name) Then
    '            If Not (TabsTestDone.Contains(Me.SelectedIndex)) Then
    '                Me.SelectedTab = PreviousTab
    '            Else
    '                RaiseEvent NewTabSelected(CurrentTab)
    '            End If
    '        End If
    '    End If
    '    IsActive = False
    'End Sub



    Private Sub TitleResize()
        Dim m_bmpw As Integer = 16
        Dim m_paddingtitolo As Integer = 3

        Dim titoloWMax As Integer = 0
        Dim titoloHMax As Integer = 0
        Dim szfTitolo As SizeF
        For Each TP As TabPage In Me.TabPages
            szfTitolo = TP.CreateGraphics.MeasureString(TP.Text, Me.Font)
            If titoloWMax < szfTitolo.Width Then titoloWMax = szfTitolo.Width
            If titoloHMax < szfTitolo.Height Then titoloHMax = szfTitolo.Height
        Next
        titoloWMax += m_bmpw + m_paddingtitolo * 2
        titoloHMax += m_paddingtitolo * 2
        Me.ItemSize = New Size(titoloWMax, titoloHMax)


    End Sub


End Class
