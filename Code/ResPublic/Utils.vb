

Public Class Utils



    Public Declare Function SystemParametersInfo Lib "user32" Alias "SystemParametersInfoA" (ByVal uAction As Integer, ByVal uParam As Integer, ByVal lpvParam As String, ByVal fuWinIni As Integer) As Integer


    Public Shared Function Ping(ByVal IpAddress As String, ByVal Timeout As Integer) As Boolean
        Try
            Dim MyPing As System.Net.NetworkInformation.Ping
            Dim PingReply As System.Net.NetworkInformation.PingReply
            MyPing = New System.Net.NetworkInformation.Ping
            PingReply = MyPing.Send(IpAddress, Timeout)
            If (PingReply.Status = System.Net.NetworkInformation.IPStatus.Success) Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try

    End Function




End Class

