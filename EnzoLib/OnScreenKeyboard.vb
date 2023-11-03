Public Class OnScreenKeyboard
    Private Tastiera As Process
    Private KeyboardOn As Boolean = False

    Public Sub Activate()
        KeyboardOn = True
        Tastiera = Process.Start("C:\Windows\System32\osk.exe")

    End Sub
    Public Sub Remove()

        Try
            If KeyboardOn Then Tastiera.Kill()
        Catch ex As Exception
        End Try

        KeyboardOn = False
    End Sub


End Class
