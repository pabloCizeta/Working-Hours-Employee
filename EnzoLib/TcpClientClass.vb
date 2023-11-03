Imports System.Text

Public Class TcpClientClass
    Private WithEvents myClient As System.Net.Sockets.TcpClient
    Private mConnected As Boolean

    Public ReadOnly Property IsOpen() As Boolean
        Get
            Return mConnected
        End Get
    End Property

    Public Sub New()
        myClient = New System.Net.Sockets.TcpClient
        mConnected = False
    End Sub

    Public Function OpenConnection(ByVal Ip As String, ByVal Port As Integer) As Boolean

        If Not checkip(Ip) Then
            'Throw New Exception("Ip is not in the right format")
            Return False
        End If

        Try
            If EnzoLib.Utilities.Ping(Ip, 100) Then
                myClient.Connect(Ip, Port)
                mConnected = True
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
        



    End Function
    Public Function SendData(ByVal Message As String) As Boolean
        Dim Buffer() As Byte = System.Text.Encoding.Default.GetBytes(Message.ToCharArray)
        If mConnected Then
            Try
                myClient.GetStream.Write(Buffer, 0, Buffer.Length)
                Return True
            Catch ex As Exception
                'Throw New Exception("Data not sent")
                Return False
            End Try
        Else
            Return True
        End If
    End Function
    Public Function SendData(ByVal Buffer() As Byte) As Boolean
        If mConnected Then
            Try
                myClient.GetStream.Write(Buffer, 0, Buffer.Length)
                Return True
            Catch ex As Exception
                Throw New Exception("Data not sent")
                Return False
            End Try
        Else
            Return True
        End If
    End Function
    Public Function SendData(ByVal Data() As Int16) As Boolean
        Dim Buffer() As Byte
        If mConnected Then
            Try
                ReDim Buffer(Data.Length)
                For i As Integer = 0 To Data.Length - 1
                    Buffer(i) = CByte(Data(i))
                Next
                myClient.GetStream.Write(Buffer, 0, Buffer.Length)
                Return True
            Catch ex As Exception
                Throw New Exception("Data not sent")
                Return False
            End Try
        Else
            Return True
        End If
    End Function
    Public Function IsDataAvailable() As Boolean
        Return myClient.GetStream.DataAvailable()
    End Function
    Public Function ReadData(ByRef buffer() As Byte) As String
        ReDim buffer(myClient.Available)

        Try
            myClient.GetStream.Read(buffer, 0, buffer.Length)
            Return System.Text.Encoding.Default.GetString(buffer)
        Catch ex As Exception
        End Try
        Return ""

    End Function
    Public Function CloseConnection() As Boolean
        Try
            myClient.Close()
            mConnected = False
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function


    Public Function WaitData(ByRef buffer() As Byte, ByVal iTimeout As Integer) As String
        ReDim buffer(myClient.Available)

        Try
            myClient.GetStream.ReadTimeout = iTimeout
            myClient.GetStream.Read(buffer, 0, buffer.Length)
            Return System.Text.Encoding.Default.GetString(buffer)
        Catch ex As Exception
        End Try
        Return ""

    End Function

    Public Sub OpenAndSendMessage(ByVal Ip As String, ByVal Port As Integer, ByVal Message As String)
        If Not checkip(Ip) Then
            Throw New Exception("Ip is not in the right format")
            Exit Sub
        End If
        Try
            Dim client As New System.Net.Sockets.TcpClient
            Dim Buffer() As Byte = System.Text.Encoding.Default.GetBytes(Message.ToCharArray)
            client.Connect(Ip, Port)

            client.GetStream.Write(Buffer, 0, Buffer.Length)
            'Dim aaa(10) As Byte
            'client.GetStream.Read(aaa, 0, aaa.Length)
            'client.GetStream.DataAvailable()
            client.Close()

            ''Dim networkStream As Net.Sockets.NetworkStream = client.GetStream()
            ''If networkStream.CanWrite And networkStream.CanRead Then
            ''    ' Do a simple write.
            ''    Dim sendBytes As [Byte]() = Encoding.ASCII.GetBytes("Is anybody there")
            ''    networkStream.Write(sendBytes, 0, sendBytes.Length)
            ''    ' Read the NetworkStream into a byte buffer.
            ''    Dim bytes(client.ReceiveBufferSize) As Byte

            ''    client.ReceiveTimeout = 1000
            ''    networkStream.Read(bytes, 0, CInt(client.ReceiveBufferSize))
            ''    ' Output the data received from the host to the console.
            ''    Dim returndata As String = Encoding.ASCII.GetString(bytes)
            ''    'Console.WriteLine(("Host returned: " + returndata))
            ''    client.Close()
            ''    Throw New Exception("Dati letti: " & returndata)

            ''Else
            ''    If Not networkStream.CanRead Then
            ''        'Console.WriteLine("cannot not write data to this stream")
            ''        client.Close()
            ''    Else
            ''        If Not networkStream.CanWrite Then
            ''            'Console.WriteLine("cannot read data from this stream")
            ''            client.Close()
            ''        End If
            ''    End If
            ''End If
        Catch ex As Exception
            Throw New Exception("Remote IP is not reachable")
        End Try


        'Dim tcpClient As New System.Net.Sockets.TcpClient()
        'tcpClient.Connect("127.0.0.1", 8000)
        'Dim networkStream As NetworkStream = tcpClient.GetStream()
        'If networkStream.CanWrite And networkStream.CanRead Then
        '    ' Do a simple write.
        '    Dim sendBytes As [Byte]() = Encoding.ASCII.GetBytes("Is anybody there")
        '    networkStream.Write(sendBytes, 0, sendBytes.Length)
        '    ' Read the NetworkStream into a byte buffer.
        '    Dim bytes(tcpClient.ReceiveBufferSize) As Byte
        '    networkStream.Read(bytes, 0, CInt(tcpClient.ReceiveBufferSize))
        '    ' Output the data received from the host to the console.
        '    Dim returndata As String = Encoding.ASCII.GetString(bytes)
        '    Console.WriteLine(("Host returned: " + returndata))
        'Else
        '    If Not networkStream.CanRead Then
        '        Console.WriteLine("cannot not write data to this stream")
        '        tcpClient.Close()
        '    Else
        '        If Not networkStream.CanWrite Then
        '            Console.WriteLine("cannot read data from this stream")
        '            tcpClient.Close()
        '        End If
        '    End If
        'End If
        '' pause so user can view the console output
        'Console.ReadLine()

    End Sub
    Public Sub OpenAndSendMessage(ByVal Ip As String, ByVal Port As Integer, ByVal Message() As Byte)

        If Not checkip(Ip) Then
            Throw New Exception("Ip is not in the right format")
            Exit Sub
        End If
        Try
            Dim client As New System.Net.Sockets.TcpClient
            client.Connect(Ip, Port)
            client.GetStream.Write(Message, 0, Message.Length)
            client.Close()

        Catch ex As Exception
            Throw New Exception("Remote IP is not reachable")
        End Try

    End Sub


    Private Function checkip(ByVal ip As String) As Boolean
        Try
            Dim ss() As String = ip.Split(".")
            Dim bb(3) As Byte
            Dim i As Integer
            For i = 0 To 3
                bb(i) = Byte.Parse(ss(i))
            Next
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

End Class