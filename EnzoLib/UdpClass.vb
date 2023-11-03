Imports System.Net
Imports System.Text

Public Class UdpClass

    Private udpClient As Sockets.UdpClient
    Private mRemoteIp As String
    Private mRemotePort As Integer
    Private mIsConnected As Boolean
    Private mTimeout As Integer


#Region "Properties"

    Public ReadOnly Property IsConnected() As Boolean
        Get
            Return mIsConnected
        End Get
    End Property
    Public ReadOnly Property IsDataAvailable() As Boolean
        Get
            Return IIf(udpClient.Available > 0, True, False)
        End Get
    End Property

    Public Property TimeoutAnswer() As Integer
        Get
            Return mTimeout
        End Get
        Set(ByVal Value As Integer)
            mTimeout = Value
        End Set
    End Property
    Public Property RemotPort() As Integer
        Get
            Return mRemotePort
        End Get
        Set(ByVal Value As Integer)
            mRemotePort = Value
        End Set
    End Property
    Public Property RemoteIp() As String
        Get
            Return mRemoteIp
        End Get
        Set(ByVal Value As String)
            mRemoteIp = Value
        End Set
    End Property

#End Region

    Public Sub New()
        udpClient = New Sockets.UdpClient()
        mRemoteIp = "127.0.0.1"
        mRemotePort = 8001
        mIsConnected = False
        mTimeout = 1000

    End Sub
    Public Sub New(ByVal pRemoteIp As String, ByVal pPort As Integer, ByVal pTimeout As Integer)
        udpClient = New Sockets.UdpClient()
        mRemoteIp = pRemoteIp
        mRemotePort = pPort
        mIsConnected = False
        mTimeout = pTimeout

    End Sub

#Region "Async functions"
    Public Function Open() As Boolean

        Try
            Dim RemoteIpEndPoint As IPEndPoint
            'RemoteIpEndPoint = New IPEndPoint(IPAddress.Any, 0)
            RemoteIpEndPoint = New IPEndPoint(IPAddress.Parse(mRemoteIp), 0)
            udpClient = New System.Net.Sockets.UdpClient(mRemotePort)
            ' udpClient.Connect(IPAddress.Parse(RemoteIp), mRemotePort)
            mIsConnected = True
            Return True
        Catch ex As Exception
        End Try
        Return False
    End Function
    Public Function Open(ByVal RemoteIp As String, ByVal RemotPort As Integer) As Boolean
        mRemoteIp = RemoteIp
        mRemotePort = RemotPort

        Try
            Dim RemoteIpEndPoint As IPEndPoint
            'RemoteIpEndPoint = New IPEndPoint(IPAddress.Any, 0)
            RemoteIpEndPoint = New IPEndPoint(IPAddress.Parse(mRemoteIp), 0)
            udpClient = New System.Net.Sockets.UdpClient(mRemotePort)
            ' udpClient.Connect(IPAddress.Parse(RemoteIp), mRemotePort)
            mIsConnected = True
            Return True
        Catch ex As Exception
        End Try
        Return False
    End Function

    Public Sub Close()
        Try
            udpClient.Close()
            mIsConnected = False
        Catch ex As Exception
        End Try
    End Sub
    Public Function Send(ByVal sMsg As String) As Boolean
        Dim sendBytes() As Byte
        sendBytes = Encoding.ASCII.GetBytes(sMsg)
        Try
            udpClient.Send(sendBytes, sendBytes.Length)
            Return True
        Catch ex As Exception
        End Try
        Return False
    End Function
    Public Function Send(ByVal sendBytes() As Byte) As Boolean
        Try
            udpClient.Send(sendBytes, sendBytes.Length)
            Return True
        Catch ex As Exception
        End Try
        Return False
    End Function
    Public Function Receive(ByRef BytesReceived() As Byte) As String
        Dim sMsg As String

        Dim RemoteIpEndPoint As IPEndPoint
        ''RemoteIpEndPoint = New IPEndPoint(IPAddress.Any, 0)
        RemoteIpEndPoint = New IPEndPoint(IPAddress.Parse(mRemoteIp), 0)

        sMsg = ""
        If udpClient.Available > 0 Then
            Try
                BytesReceived = udpClient.Receive(RemoteIpEndPoint)
                sMsg = Encoding.ASCII.GetString(BytesReceived)

            Catch ex As Exception
            End Try
        End If

        Return sMsg

    End Function

#End Region



#Region "Sync functions"


    Public Sub OpenAndSend(ByVal sMsg As String, ByVal RemoteIp As String, ByVal RemotPort As Integer)
        Dim sendBytes() As Byte
        Dim IpAdd As IPAddress

        mRemoteIp = RemoteIp
        mRemotePort = RemotPort

        IpAdd = IPAddress.Parse(RemoteIp)

        sendBytes = Encoding.ASCII.GetBytes(sMsg)
        Try
            udpClient.Connect(IpAdd, mRemotePort)
            udpClient.Send(sendBytes, sendBytes.Length)
            udpClient.Close()
        Catch ex As Exception
        End Try
    End Sub
    Public Sub OpenAndSend(ByVal sendBytes() As Byte, ByVal RemoteIp As String, ByVal RemotPort As Integer)
        Dim IpAdd As IPAddress
        Dim receiveBytes() As Byte

        mRemoteIp = RemoteIp
        mRemotePort = RemotPort
        IpAdd = IPAddress.Parse(RemoteIp)
        'sendBytes = Encoding.ASCII.GetBytes(sMsg)

        Dim RemoteIpEndPoint As IPEndPoint
        RemoteIpEndPoint = New IPEndPoint(IPAddress.Any, 0)

        Try
            udpClient.Connect(IpAdd, mRemotePort)
            udpClient.Send(sendBytes, sendBytes.Length)
            udpClient.Client.ReceiveTimeout = 1000
            receiveBytes = udpClient.Receive(RemoteIpEndPoint)
            Dim sMsg = Encoding.ASCII.GetString(receiveBytes)
            udpClient.Close()
        Catch ex As Exception
        End Try
    End Sub

    Public Function OpenAndReceive(ByVal IpAdds As String, ByVal RemotePort As Integer, ByVal iTimeout As Integer) As String
        Dim receiveBytes() As Byte
        Dim sMsg As String
        Dim mCrono As Cronometro

        ''IPEndPoint object will allow us to read datagrams sent from any source.
        'Dim RemoteIpEndPoint As New System.Net.IPEndPoint(System.Net.IPAddress.Any, 0)

        Dim RxClient As System.Net.Sockets.UdpClient
        'RxClient = New System.Net.Sockets.UdpClient(RemotePort)
        'receiveBytes = RxClient.Receive(RemoteIpEndPoint)
        'Dim sIp As String = RemoteIpEndPoint.Address.ToString
        'sMsg = Encoding.ASCII.GetString(receiveBytes)
        'RxClient.Close()

        'Return sMsg



        Dim RemoteIpEndPoint As IPEndPoint
        'If IpAdds = "" Then
        '    ' IPEndPoint object will allow us to read datagrams sent from any source.
        '    RemoteIpEndPoint = New IPEndPoint(IPAddress.Any, 0)
        'Else
        '    'Solo da IP scelto
        '    RemoteIpEndPoint = New IPEndPoint(IPAddress.Parse(IpAdds), 0)
        'End If
        RemoteIpEndPoint = New IPEndPoint(IPAddress.Any, 0)

        RxClient = New System.Net.Sockets.UdpClient(RemotePort)

        'RxClient.Connect(IPAddress.Parse(IpAdds), RemotePort)

        'RxClient.EnableBroadcast = True

        'udpClient.JoinMulticastGroup(IPAddress.Parse(IpAdds))


        mCrono = New Cronometro
        mCrono.Start()

        sMsg = ""
        Do
            If mCrono.ElapsedMilliseconds > iTimeout Then
                Exit Do
            End If
            If RxClient.Available > 0 Then
                Try
                    RxClient.Client.ReceiveTimeout = iTimeout
                    receiveBytes = RxClient.Receive(RemoteIpEndPoint)
                    sMsg = sMsg & Encoding.ASCII.GetString(receiveBytes)
                Catch ex As Exception
                End Try
                mCrono.Start()
            End If
            If (sMsg.Length > 0) And (mCrono.ElapsedMilliseconds > 300) Then
                Exit Do
            End If
        Loop

        RxClient.Close()
        Return sMsg

    End Function


    Public Function OpenSendReceive(ByVal BytesToSend() As Byte, ByVal RemoteIp As String, ByVal RemotPort As Integer, ByRef BytesReceived() As Byte) As String
        Dim IpAdd As IPAddress
        'Dim receiveBytes() As Byte
        Dim sMsg As String

        mRemoteIp = RemoteIp
        mRemotePort = RemotPort
        IpAdd = IPAddress.Parse(mRemoteIp)

        Dim RemoteIpEndPoint As IPEndPoint
        'RemoteIpEndPoint = New IPEndPoint(IPAddress.Any, 0)
        RemoteIpEndPoint = New IPEndPoint(IpAdd, 0)

        sMsg = ""
        Try
            udpClient.Connect(IpAdd, mRemotePort)
            udpClient.Send(BytesToSend, BytesToSend.Length)
            udpClient.Client.ReceiveTimeout = mTimeout
            BytesReceived = udpClient.Receive(RemoteIpEndPoint)
            sMsg = Encoding.ASCII.GetString(BytesReceived)
            udpClient.Close()
        Catch ex As Exception
        End Try

        Return sMsg

    End Function

    Public Function OpenSendReceive(ByVal sMsg As String, ByVal RemoteIp As String, ByVal RemotPort As Integer, ByRef BytesReceived() As Byte) As String
        Dim IpAdd As IPAddress
        Dim sendBytes() As Byte

        mRemoteIp = RemoteIp
        mRemotePort = RemotPort
        IpAdd = IPAddress.Parse(RemoteIp)
        sendBytes = Encoding.ASCII.GetBytes(sMsg)

        Dim RemoteIpEndPoint As IPEndPoint
        RemoteIpEndPoint = New IPEndPoint(IPAddress.Any, 0)

        sMsg = ""
        Try
            udpClient.Connect(IpAdd, mRemotePort)
            udpClient.Send(sendBytes, sendBytes.Length)
            udpClient.Client.ReceiveTimeout = mTimeout
            BytesReceived = udpClient.Receive(RemoteIpEndPoint)
            sMsg = Encoding.ASCII.GetString(BytesReceived)
            udpClient.Close()
        Catch ex As Exception
        End Try

        Return sMsg

    End Function
#End Region

End Class
