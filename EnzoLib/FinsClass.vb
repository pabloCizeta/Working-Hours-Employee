Imports System.Net
Imports System.Threading

Public Class FinsClassUdp

    Private myCrono As Cronometro

    Private MyUdp As UdpClass

    Private mLocalIP As String
    Private mRemoteIP As String
    Private mLocalPort As Integer
    Private mRemotePort As Integer
    Private MsgFromTcp As String

    Private mStatus As String
    Private mErrorCode As Integer
    Private mErrorMsg As String

    Private mTimeoutAnswer As Long          'Tempo di timeout attesa risposta da slave

    Private mDNA As Integer
    Private mDA1 As Integer
    Private mDA2 As Integer
    Private mSNA As Integer
    Private mSA1 As Integer
    Private mSA2 As Integer

    Private mConnected As Boolean

    

    Public Class MyErrorException
        Inherits Exception

        Private pMessage As String
        Public Overrides ReadOnly Property Message() As String
            Get
                Return pMessage
            End Get
        End Property
        Public Sub New(ByVal msg As String)
            Me.pMessage = msg
        End Sub
    End Class


#Region "Properties"

    Public ReadOnly Property ErrorCode() As Integer
        Get
            Return mErrorCode
        End Get
    End Property
    Public ReadOnly Property ErrorMsg() As String
        Get
            Return mErrorMsg
        End Get
    End Property
    Public ReadOnly Property IsOpen() As Boolean
        Get
            Return mConnected
        End Get
    End Property

    Public ReadOnly Property IsDataAvailable() As Boolean
        Get
            Return MyUdp.IsDataAvailable
        End Get
    End Property

    Public Property LocalIp() As String
        Get
            Return mLocalIP
        End Get
        Set(ByVal Value As String)
            mLocalIP = Value
        End Set
    End Property
    Public Property RemoteIp() As String
        Get
            Return mRemoteIP
        End Get
        Set(ByVal Value As String)
            mRemoteIP = Value
        End Set
    End Property

    Public Property Port() As Integer
        Get
            Return mLocalPort
        End Get
        Set(ByVal Value As Integer)
            mLocalPort = Value
            mRemotePort = Value
        End Set
    End Property
    Public Property TimeoutAnswer() As Integer
        Get
            Return mTimeoutAnswer
        End Get
        Set(ByVal Value As Integer)
            mTimeoutAnswer = Value
        End Set
    End Property
    Public Property DestinationNetworkAddress() As Byte
        Get
            Return mDNA
        End Get
        Set(ByVal Value As Byte)
            mDNA = Value
        End Set
    End Property
    Public Property DestinationNodeAddress() As Byte
        Get
            Return mDA1
        End Get
        Set(ByVal Value As Byte)
            mDA1 = Value
        End Set
    End Property
    Public Property DestinationUnitAddress() As Byte
        Get
            Return mDA2
        End Get
        Set(ByVal Value As Byte)
            mDA2 = Value
        End Set
    End Property
    Public Property SourceNetworkAddress() As Byte
        Get
            Return mSNA
        End Get
        Set(ByVal Value As Byte)
            mSNA = Value
        End Set
    End Property
    Public Property SourceNodeAddress() As Byte
        Get
            Return mSA1
        End Get
        Set(ByVal Value As Byte)
            mSA1 = Value
        End Set
    End Property
    Public Property SourceUnitAddress() As Byte
        Get
            Return mSA2
        End Get
        Set(ByVal Value As Byte)
            mSA2 = Value
        End Set
    End Property


#End Region

#Region "Costruttori"

    Public Sub New(ByVal sRemoteIP As String, ByVal iPort As Integer, ByVal sLocalIP As String)

        mLocalIP = sLocalIP
        mLocalPort = iPort
        mRemoteIP = sRemoteIP
        mRemotePort = iPort

        mTimeoutAnswer = 1000

        mStatus = ""
        mErrorCode = 0
        mErrorMsg = ""

        mDNA = 0
        mDA1 = 0
        mDA2 = 0
        mSNA = 0
        mSA1 = 99
        mSA2 = 0

        MyUdp = New UdpClass(sRemoteIP, iPort, mTimeoutAnswer)
        mConnected = False

        myCrono = New Cronometro
        myCrono.Start()

    End Sub

#End Region


#Region "Build Message"

    Private Function CreateHeader() As Byte()
        Dim FinsHeader(8) As Byte

        'Fins header
        FinsHeader(0) = &H80       'ICF (Information Control Field):
        ' bit0: Response type (0: Response required; 1: Response not required)
        ' bit6: Kind of data (0: command; 1: response)
        FinsHeader(1) = &H0         'RSV (Reserved by System). Must be 0
        FinsHeader(2) = &H2         'GCT (Permissible Number of Gateways). Must be 2
        FinsHeader(3) = mDNA        'DNA (Destination Network Address)
        FinsHeader(4) = mDA1        'DA1 (Destination Node Address) (ultimo campo indirizzo IP)
        FinsHeader(5) = mDA2        'DA2 (Destination Unit Address)
        FinsHeader(6) = mSNA        'SNA (Source Network Address)
        FinsHeader(7) = mSA1        'SA1 (Source Node Address)
        FinsHeader(8) = mSA2        'SA2 (Source Unit Address)

        Return FinsHeader

    End Function


#End Region


#Region "Check Response"

    Private Function CheckResponse(ByVal FinsCmd() As Byte, ByVal FinsResp() As Byte, ByVal sRisposta As String) As Boolean
        'Controllo risposta
        If sRisposta.Length >= 14 Then
            If (FinsCmd(3) = FinsResp(6)) And (FinsCmd(4) = FinsResp(7)) And _
               (FinsCmd(5) = FinsResp(8)) Then
                If (FinsCmd(9) = FinsResp(9)) Then
                    If (FinsResp(12) = 0) And (FinsResp(13) = 0) Then
                        mStatus = "Write successful"
                        Return True
                    Else
                        mStatus = "Write failed: Data error"
                        'Throw New MyErrorException(mStatus)
                        Return False
                    End If
                Else
                    mStatus = "Write failed: ID error"
                    'Throw New MyErrorException(mStatus)
                    Return False
                End If
            Else
                mStatus = "Write failed: illegal source address error"
                'Throw New MyErrorException(mStatus)
                Return False
            End If
        Else
            mStatus = "Write failed: Message too short"
            'Throw New MyErrorException(mStatus)
            Return False
        End If
    End Function
#End Region


#Region "Send query and Get Response"
    'Private Sub SendMsg(ByVal sMsg() As Byte)
    '    Try
    '        'Dim sData As String = ""           
    '        'sData = System.Text.Encoding.ASCII.GetString(sMsg)
    '        MyUdp.OpenAndSend(sMsg, mRemoteIP, mRemotePort)
    '    Catch ex As Exception
    '        mStatus = "Error sending message : " & ex.Message
    '        Throw New MyErrorException(mStatus)
    '    End Try

    'End Sub

    'Private Function GetAnswer() As String
    '    Dim MessaggioRic As String = ""

    '    Try
    '        MessaggioRic = MyUdp.OpenAndReceive(mRemoteIP, mRemotePort, mTimeoutAnswer)

    '    Catch ex As Exception
    '        mStatus = "Error sending message : " & ex.Message
    '        Throw New MyErrorException(mStatus)
    '    End Try

    '    Return MessaggioRic

    'End Function

#End Region

#Region "Async functions"
    Public Function Open(ByVal sRemoteIP As String, ByVal iPort As Integer) As Boolean

        mRemoteIP = sRemoteIP
        mRemotePort = iPort
        mLocalPort = iPort

        If MyUdp.Open(mRemoteIP, mLocalPort) Then
            mConnected = True
            Return True
        End If
        Return False
    End Function
    Public Sub Close()
        MyUdp.Close()
        mConnected = False
    End Sub
    Public Function BeginReadMemoryArea(ByVal Id As Byte, ByVal MemoryAreaCode As Byte, ByVal StartAddress As UInt16, ByVal RegistersQty As UInt16) As Boolean
        'Read register
        Dim FinsCmd() As Byte

        FinsCmd = CreateHeader()
        ReDim Preserve FinsCmd(17)

        FinsCmd(9) = Id         'SID (Service ID)
        'Fins command
        FinsCmd(10) = &H1       'MRC
        FinsCmd(11) = &H1       'SRC
        'Fins parameter
        FinsCmd(12) = MemoryAreaCode      'Tipo dati: DM=&h82,  CIO=&H80
        Dim Adr() = BitConverter.GetBytes(CType(StartAddress, UInt16))
        FinsCmd(13) = Adr(1)
        FinsCmd(14) = Adr(0)
        FinsCmd(15) = &H0
        Dim wQty() = BitConverter.GetBytes(CType(RegistersQty, UInt16))
        FinsCmd(16) = wQty(1)
        FinsCmd(17) = wQty(0)

        Try
            MyUdp.Send(FinsCmd)
            mErrorCode = 0
            mErrorMsg = ""
            mStatus = "Read successful"
            Return True

        Catch ex As Exception
            mStatus = "Error in read event: " + ex.Message
            mErrorCode = -1
            mErrorMsg = ex.Message
            'Throw New MyErrorException(mStatus)
            Return False
        End Try
        

    End Function

    Public Function EndReadMemoryArea(ByVal Id As Byte, ByVal MemoryAreaCode As Byte, ByVal StartAddress As UInt16, ByVal RegistersQty As UInt16, ByRef values() As UInt16) As Boolean
        'Read register
        Dim FinsCmd() As Byte
        Dim sRisposta As String
        Dim FinsResp() As Byte
        ReDim FinsResp(0)
        FinsResp(0) = &H0

        FinsCmd = CreateHeader()
        ReDim Preserve FinsCmd(17)

        FinsCmd(9) = Id         'SID (Service ID)
        'Fins command
        FinsCmd(10) = &H1       'MRC
        FinsCmd(11) = &H1       'SRC
        'Fins parameter
        FinsCmd(12) = MemoryAreaCode      'Tipo dati: DM=&h82,  CIO=&H80
        Dim Adr() = BitConverter.GetBytes(CType(StartAddress, UInt16))
        FinsCmd(13) = Adr(1)
        FinsCmd(14) = Adr(0)
        FinsCmd(15) = &H0
        Dim wQty() = BitConverter.GetBytes(CType(RegistersQty, UInt16))
        FinsCmd(16) = wQty(1)
        FinsCmd(17) = wQty(0)

        Try
            sRisposta = MyUdp.Receive(FinsResp)
            'Controllo risposta
            If CheckResponse(FinsCmd, FinsResp, sRisposta) Then
                For i As Integer = 0 To RegistersQty - 1
                    values(i) = FinsResp(14 + 2 * (i))
                    values(i) <<= 8
                    values(i) += FinsResp(15 + 2 * (i))
                Next
                mStatus = "Read successful"
                mErrorCode = 0
                mErrorMsg = ""
                Return True
            Else
                Return False
            End If            

        Catch ex As Exception
            mStatus = "Error in read event: " + ex.Message
            'Throw New MyErrorException(mStatus)
            mErrorCode = -1
            mErrorMsg = ex.Message
            Return False
        End Try

    End Function


    Public Function BeginWriteMemoryArea(ByVal Id As Byte, ByVal MemoryAreaCode As Byte, ByVal StartAddress As UInt16, ByVal RegistersQty As UInt16, ByRef values() As UInt16) As Boolean
        'Read register
        Dim FinsCmd(17) As Byte
        Dim FinsResp() As Byte
        ReDim FinsResp(0)
        FinsResp(0) = &H0

        'Fins header
        FinsCmd = CreateHeader()
        ReDim Preserve FinsCmd(17)

        FinsCmd(9) = Id         'SID (Service ID)
        'Fins command
        FinsCmd(10) = &H1       'MRC
        FinsCmd(11) = &H2       'SRC
        'Fins parameter
        FinsCmd(12) = MemoryAreaCode      'Tipo dati: DM=&h82,  CIO=&H80
        Dim Adr() = BitConverter.GetBytes(CType(StartAddress, UInt16))
        FinsCmd(13) = Adr(1)
        FinsCmd(14) = Adr(0)
        FinsCmd(15) = &H0
        Dim wQty() = BitConverter.GetBytes(CType(RegistersQty, UInt16))
        FinsCmd(16) = wQty(1)
        FinsCmd(17) = wQty(0)

        ReDim Preserve FinsCmd(17 + RegistersQty * 2)
        For i As Integer = 0 To RegistersQty - 1
            FinsCmd(18 + 2 * i) = CByte(Fix(values(i) / 256))
            FinsCmd(19 + 2 * i) = CByte(values(i) - (FinsCmd(18 + 2 * i) * 256))
        Next

        Try
            myUdp.Send(FinsCmd)
            mStatus = "Write request successful"
            mErrorCode = 0
            mErrorMsg = ""
            Return True

        Catch ex As Exception
            mStatus = "Error in write request: " + ex.Message
            'Throw New MyErrorException(mStatus)
            mErrorCode = -1
            mErrorMsg = ex.Message
            Return False
        End Try

        

    End Function

    Public Function EndWriteMemoryArea(ByVal Id As Byte, ByVal MemoryAreaCode As Byte, ByVal StartAddress As UInt16, ByVal RegistersQty As UInt16) As Boolean
        'Read register
        Dim FinsCmd(17) As Byte
        Dim FinsResp() As Byte
        Dim sRisposta As String
        ReDim FinsResp(0)
        FinsResp(0) = &H0

        'Fins header
        FinsCmd = CreateHeader()
        ReDim Preserve FinsCmd(17)

        FinsCmd(9) = Id         'SID (Service ID)
        'Fins command
        FinsCmd(10) = &H1       'MRC
        FinsCmd(11) = &H2       'SRC
        'Fins parameter
        FinsCmd(12) = MemoryAreaCode      'Tipo dati: DM=&h82,  CIO=&H80
        Dim Adr() = BitConverter.GetBytes(CType(StartAddress, UInt16))
        FinsCmd(13) = Adr(1)
        FinsCmd(14) = Adr(0)
        FinsCmd(15) = &H0
        Dim wQty() = BitConverter.GetBytes(CType(RegistersQty, UInt16))
        FinsCmd(16) = wQty(1)
        FinsCmd(17) = wQty(0)

        Try
            sRisposta = MyUdp.Receive(FinsResp)
            'Controllo risposta
            If CheckResponse(FinsCmd, FinsResp, sRisposta) Then
                mStatus = "Write successful"
                Return True
            Else
                Return False
            End If
            ' ''If sRisposta.Length >= 14 Then
            ' ''    If (FinsCmd(3) = FinsResp(6)) And (FinsCmd(4) = FinsResp(7)) And _
            ' ''       (FinsCmd(5) = FinsResp(8)) Then
            ' ''        If (FinsCmd(9) = FinsResp(9)) Then
            ' ''            If (FinsResp(12) = 0) And (FinsResp(13) = 0) Then
            ' ''                mStatus = "Write successful"
            ' ''                Return True
            ' ''            Else
            ' ''                mStatus = "Write failed: Data error"
            ' ''                'Throw New MyErrorException(mStatus)
            ' ''                Return False
            ' ''            End If
            ' ''        Else
            ' ''            mStatus = "Write failed: ID error"
            ' ''            'Throw New MyErrorException(mStatus)
            ' ''            Return False
            ' ''        End If
            ' ''    Else
            ' ''        mStatus = "Write failed: illegal source address error"
            ' ''        'Throw New MyErrorException(mStatus)
            ' ''        Return False
            ' ''    End If
            ' ''Else
            ' ''    mStatus = "Write failed: Message too short"
            ' ''    'Throw New MyErrorException(mStatus)
            ' ''    Return False
            ' ''End If

        Catch ex As Exception
            mStatus = "Error in Write event: " + ex.Message
            'Throw New MyErrorException(mStatus)
            Return False
        End Try
        mStatus = "Write successful"
        Return True

    End Function




#End Region


#Region "Sync function"

    Public Function ReadMemoryAreaSync(ByVal Id As Byte, ByVal MemoryAreaCode As Byte, ByVal StartAddress As UInt16, ByVal RegistersQty As UInt16, ByRef values() As UInt16) As Boolean
        'Read register
        Dim FinsCmd(17) As Byte
        Dim FinsResp() As Byte
        Dim sRisposta As String

        Dim mUdp As UdpClass

        ReDim FinsResp(0)
        FinsResp(0) = &H0

        FinsCmd = CreateHeader()
        ReDim Preserve FinsCmd(17)

        FinsCmd(9) = Id         'SID (Service ID)
        'Fins command
        FinsCmd(10) = &H1       'MRC
        FinsCmd(11) = &H1       'SRC
        'Fins parameter
        FinsCmd(12) = MemoryAreaCode      'Tipo dati: DM=&h82,  CIO=&H80
        Dim Adr() = BitConverter.GetBytes(CType(StartAddress, UInt16))
        FinsCmd(13) = Adr(1)
        FinsCmd(14) = Adr(0)
        FinsCmd(15) = &H0
        Dim wQty() = BitConverter.GetBytes(CType(RegistersQty, UInt16))
        FinsCmd(16) = wQty(1)
        FinsCmd(17) = wQty(0)


        Try
            mUdp = New UdpClass
            mUdp.TimeoutAnswer = 100
            sRisposta = mUdp.OpenSendReceive(FinsCmd, mRemoteIP, mRemotePort, FinsResp)
            'myUdp.Send(FinsCmd, mRemoteIP, mRemotePort)
            'sRisposta = myUdp.Receive(mRemoteIP, mRemotePort, 1000)
            'FinsResp = System.Text.Encoding.ASCII.GetBytes(sRisposta)
            mUdp = Nothing

            'SendMsg(FinsCmd)
            'sRisposta = GetAnswer()
            'FinsResp = System.Text.Encoding.ASCII.GetBytes(sRisposta)

            'Controllo risposta
            If CheckResponse(FinsCmd, FinsResp, sRisposta) Then
                For i As Integer = 0 To RegistersQty - 1
                    values(i) = FinsResp(14 + 2 * (i))
                    values(i) <<= 8
                    values(i) += FinsResp(15 + 2 * (i))
                Next
                mStatus = "Read successful"
                Return True
            Else
                Return False
            End If
            ' ''If sRisposta.Length >= 14 Then
            ' ''    If (FinsCmd(3) = FinsResp(6)) And (FinsCmd(4) = FinsResp(7)) And _
            ' ''       (FinsCmd(5) = FinsResp(8)) Then
            ' ''        If (FinsCmd(9) = FinsResp(9)) Then
            ' ''            If (FinsResp(12) = 0) And (FinsResp(13) = 0) Then
            ' ''                For i As Integer = 0 To RegistersQty - 1
            ' ''                    values(i) = FinsResp(14 + 2 * (i))
            ' ''                    values(i) <<= 8
            ' ''                    values(i) += FinsResp(15 + 2 * (i))
            ' ''                Next
            ' ''                mStatus = "Read successful"
            ' ''                Return True
            ' ''            Else
            ' ''                mStatus = "Read failed: Data error"
            ' ''                Throw New MyErrorException(mStatus)
            ' ''                Return False
            ' ''            End If
            ' ''        Else
            ' ''            mStatus = "Read failed: ID error"
            ' ''            Throw New MyErrorException(mStatus)
            ' ''            Return False
            ' ''        End If
            ' ''    Else
            ' ''        mStatus = "Read failed: illegal source address error"
            ' ''        Throw New MyErrorException(mStatus)
            ' ''        Return False
            ' ''    End If
            ' ''Else
            ' ''    mStatus = "Read failed: Message too short"
            ' ''    Throw New MyErrorException(mStatus)
            ' ''    Return False
            ' ''End If

        Catch ex As Exception
            mStatus = "Error in read event: " + ex.Message
            Throw New MyErrorException(mStatus)
            Return False
        End Try
        mStatus = "Read successful"
        Return True

    End Function

    Public Function WriteMemoryAreaSync(ByVal Id As Byte, ByVal MemoryAreaCode As Byte, ByVal StartAddress As UInt16, ByVal RegistersQty As UInt16, ByRef values() As UInt16) As Boolean
        'Read register
        Dim FinsCmd(17) As Byte
        Dim FinsResp() As Byte
        Dim sRisposta As String

        Dim myUdp As UdpClass

        ReDim FinsResp(0)
        FinsResp(0) = &H0

        'Fins header
        FinsCmd = CreateHeader()
        ReDim Preserve FinsCmd(17)

        FinsCmd(9) = Id         'SID (Service ID)
        'Fins command
        FinsCmd(10) = &H1       'MRC
        FinsCmd(11) = &H2       'SRC
        'Fins parameter
        FinsCmd(12) = MemoryAreaCode      'Tipo dati: DM=&h82,  CIO=&H80
        Dim Adr() = BitConverter.GetBytes(CType(StartAddress, UInt16))
        FinsCmd(13) = Adr(1)
        FinsCmd(14) = Adr(0)
        FinsCmd(15) = &H0
        Dim wQty() = BitConverter.GetBytes(CType(RegistersQty, UInt16))
        FinsCmd(16) = wQty(1)
        FinsCmd(17) = wQty(0)

        ReDim Preserve FinsCmd(17 + RegistersQty * 2)
        For i As Integer = 0 To RegistersQty - 1
            FinsCmd(18 + 2 * i) = CByte(Fix(values(i) / 256))
            FinsCmd(19 + 2 * i) = CByte(values(i) - (FinsCmd(18 + 2 * i) * 256))
        Next

        Try
            myUdp = New UdpClass
            myUdp.TimeoutAnswer = 1000
            sRisposta = myUdp.OpenSendReceive(FinsCmd, mRemoteIP, mRemotePort, FinsResp)
            'myUdp.Send(FinsCmd, mRemoteIP, mRemotePort)
            'sRisposta = myUdp.Receive(mRemoteIP, mRemotePort, 1000)
            'FinsResp = System.Text.Encoding.ASCII.GetBytes(sRisposta)
            myUdp = Nothing

            'Controllo risposta
            If CheckResponse(FinsCmd, FinsResp, sRisposta) Then
                mStatus = "Write successful"
                Return True
            Else
                Return False
            End If
            ''If sRisposta.Length >= 14 Then
            ''    If (FinsCmd(3) = FinsResp(6)) And (FinsCmd(4) = FinsResp(7)) And _
            ''       (FinsCmd(5) = FinsResp(8)) Then
            ''        If (FinsCmd(9) = FinsResp(9)) Then
            ''            If (FinsResp(12) = 0) And (FinsResp(13) = 0) Then
            ''                mStatus = "Write successful"
            ''                Return True
            ''            Else
            ''                mStatus = "Read failed: Data error"
            ''                Throw New MyErrorException(mStatus)
            ''                Return False
            ''            End If
            ''        Else
            ''            mStatus = "Read failed: ID error"
            ''            Throw New MyErrorException(mStatus)
            ''            Return False
            ''        End If
            ''    Else
            ''        mStatus = "Read failed: illegal source address error"
            ''        Throw New MyErrorException(mStatus)
            ''        Return False
            ''    End If
            ''Else
            ''    mStatus = "Read failed: Message too short"
            ''    Throw New MyErrorException(mStatus)
            ''    Return False
            ''End If

        Catch ex As Exception
            mStatus = "Error in read event: " + ex.Message
            Throw New MyErrorException(mStatus)
            Return False
        End Try
        mStatus = "Read successful"
        Return True

    End Function



#End Region


End Class

