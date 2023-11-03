Imports Core

Public Class frmShowPdf
    Public Property FileToShow As String
    Public Overloads Property Size As Size

    Private Sub frmShowPdf_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.Text = My.Application.Info.AssemblyName & " - " & xGlobals.Release & " - Show PDF"
        Me.Icon = New System.Drawing.Icon(xGlobals.PicturePath & "OreLavoro.ico")

        AxAcroPDF1.LoadFile(FileToShow)
        MyBase.Size = New Size(Size)
        AxAcroPDF1.Size = New Size(Size.Width - 50, Size.Height - 50)

    End Sub
End Class