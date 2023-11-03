Imports Core

Public Class frmFilter
    Public Property Filter As String

    Private Sub frmFilter_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Dim iLeft As Integer, iTop As Integer
        iLeft = Cursor.Position.X
        iTop = Cursor.Position.Y
        Dim iALtezza As Integer = Me.Height

        Do While (iLeft + Me.Width) > Screen.PrimaryScreen.Bounds.Width
            iLeft -= 100
        Loop
        Me.Left = iLeft

        Do While (iTop + iALtezza) > Screen.PrimaryScreen.Bounds.Height
            iTop -= 100
        Loop
        Me.Top = iTop

        txtFilter.Focus()
        txtFilter.SelectAll()

    End Sub

    Private Sub frmFilter_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.Text = My.Application.Info.AssemblyName & " - " & xGlobals.Release & " - Filtri"
        Me.Icon = New System.Drawing.Icon(xGlobals.PicturePath & "OreLavoro.ico")

        txtFilter.Text = Filter

        KeyPreview = True

    End Sub

    Private Sub frmFilter_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress

        If e.KeyChar = vbCr Then
            plsConfirm_Click(sender, New EventArgs)
        End If
    End Sub

    Private Sub plsConfirm_Click(sender As Object, e As EventArgs) Handles plsConfirm.Click
        DialogResult = DialogResult.OK
        Filter = txtFilter.Text
    End Sub

    Private Sub plsCancel_Click(sender As Object, e As EventArgs) Handles plsCancel.Click
        DialogResult = DialogResult.Cancel
    End Sub


End Class