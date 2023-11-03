Public Class frmFileDamaged

    ' Private myMultiL As EnzoLib.MultiLingua



    Public Property ArchiveFolderName As String = ""
    Public Property iDay As Integer = 1
    Public Property IgnoreEnabled As Boolean = False

    Private Sub frmFileDamaged_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'myMultiL = New EnzoLib.MultiLingua(xGlobals.MultiLanguageFile, xAppConfig.SelectedLanguage, MyBase.Name)


        If iDay = 0 Then plsRestore.Visible = False
        plsRestore.Text = "Restore from " + Format(DateAdd(DateInterval.Day, -1 * iDay, Date.Now), "dddd")

        plsIgnore.Visible = IgnoreEnabled

    End Sub


    Private Sub plsRestore_Click(sender As Object, e As EventArgs) Handles plsRestore.Click
        Dim FileName As String = FileIO.FileSystem.GetName(lblFileName.Text)
        Dim dirs As System.IO.DirectoryInfo = IO.Directory.GetParent(lblFileName.Text)
        Dim FileFolder As String = dirs.Name + Format(DateAdd(DateInterval.Day, -1 * iDay, Date.Now), "ddd")
        Dim FileToRestore As String = ArchiveFolderName + FileFolder + "\" + FileName
        If My.Computer.FileSystem.FileExists(FileToRestore) Then
            Try
                FileSystem.FileCopy(FileToRestore, lblFileName.Text)
            Catch ex As Exception

            End Try
        End If

        DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub plsIgnore_Click(sender As Object, e As EventArgs) Handles plsIgnore.Click
        DialogResult = Windows.Forms.DialogResult.Ignore
        Me.Close()
    End Sub

    Private Sub plsExit_Click(sender As Object, e As EventArgs) Handles plsExit.Click
        DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

   
End Class