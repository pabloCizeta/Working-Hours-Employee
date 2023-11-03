Imports System.Net
Imports System.Net.Sockets

Public Class TelNet

    Private tnSocket As Socket

    Private MyRemoteIPAddress As IPAddress
    Private ep As IPEndPoint

#Region "Properties"


    Public Property IsOpen As Boolean = False
   

#End Region


    Public Function Open(ByVal RemoteIp As String, ByVal RemotePortNo As Integer) As Boolean

        ' Get the IP Address and the Port and create an IPEndpoint (ep)
        MyRemoteIPAddress = IPAddress.Parse(RemoteIp.Trim)
        ep = New IPEndPoint(MyRemoteIPAddress, RemotePortNo)


        Dim MyPing As System.Net.NetworkInformation.Ping
        Dim PingReply As System.Net.NetworkInformation.PingReply
        MyPing = New System.Net.NetworkInformation.Ping
        Try
            PingReply = MyPing.Send(RemoteIp, 100)
            If (PingReply.Status = System.Net.NetworkInformation.IPStatus.Success) Then
                ' Set the socket up (type etc)
                tnSocket = New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
                tnSocket.Connect(ep)
            Else
                IsOpen = False
                Return False
            End If
        Catch ex As Exception
            IsOpen = False
            Return False
        End Try


        IsOpen = tnSocket.Connected
        Return True

    End Function

    Public Sub Close()
        Try
            tnSocket.Disconnect(False)
        Catch ex As Exception

        End Try
        IsOpen = False
    End Sub


    Public Function Send(ByVal MsgToSend As String) As Boolean

        Dim SendBytes As [Byte]() = System.Text.Encoding.ASCII.GetBytes(MsgToSend)
        Try
            tnSocket.Send(SendBytes, SendBytes.Length, SocketFlags.None)
            Return True
        Catch ex As Exception

        End Try

        Return False

    End Function


    Public Function Receive() As String
        Dim tStr As String
        Dim BytesReceived(255) As Byte

        tStr = ""
        If tnSocket.Connected And (tnSocket.Available > 0) Then
            Dim NumBytes As Integer = tnSocket.Receive(BytesReceived, BytesReceived.Length, SocketFlags.None)
            tStr = tStr + System.Text.Encoding.ASCII.GetString(BytesReceived, 0, NumBytes)
        Else
            BytesReceived = Nothing
        End If
        Return tStr

    End Function


End Class
