Imports System.IO.Ports
Imports System.Net
Imports System.Threading


Public Class ModBusClassRs232

    Private Const RIP_COM As Integer = 3     'Esegui 3 tentativi di ritrasmissione messaggio

    Private Const fctReadCoil As Byte = 1
    Private Const fctReadDiscreteInputs As Byte = 2
    Private Const fctReadHoldingRegister As Byte = 3
    Private Const fctReadInputRegister As Byte = 4
    Private Const fctWriteSingleCoil As Byte = 5
    Private Const fctWriteSingleRegister As Byte = 6
    Private Const fctWriteMultipleCoils As Byte = 15
    Private Const fctWriteMultipleRegister As Byte = 16

    Private sp As SerialPort
    Private myCrono As Cronometro

    Private mPort As String
    Private mBaudRate As Int32
    Private mParity As DataParity
    Private mStopBit As DataStopBit
    Private mDataBit As Int32
    Private mBufferSize As Int32
    Private mTimeout As Int32

    Private CRCHi(255) As Byte               'Tabella per calcolo byte alto CRC16
    Private CRCLo(255) As Byte               'Tabella per calcolo byte basso CRC16


    Private TimeSilent35 As Long           'Tempo di interframe 3.5 caratteri
    Private TimeSilent15 As Long           'Tempo di intercarattere 1.5 caratteri
    Private TimeoutAnswer As Long          'Tempo di timeout attesa risposta da slave
    Private TimeLastChar As Long           'Tempo alla ricezione dell'ultimo carattere


    Private mStatus As String
    Private mErrorCode As Integer
    Private mErrorMsg As String

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


    Public Enum DataParity
        Parity_None = 0
        Parity_Odd
        Parity_Even
        Parity_Mark
    End Enum
    Public Enum DataStopBit
        StopBit_1 = 1
        StopBit_2
    End Enum


#Region "Proprietà"

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
            Return sp.IsOpen
        End Get
    End Property

    Public ReadOnly Property IsDataAvailable() As Boolean
        Get
            Return IIf(sp.BytesToRead > 0, True, False)
        End Get
    End Property

    Public Property Port() As String
        Get
            Return mPort
        End Get
        Set(ByVal Value As String)
            mPort = Value
        End Set
    End Property
    Public Property Parity() As DataParity
        Get
            Return mParity
        End Get
        Set(ByVal Value As DataParity)
            mParity = Value
        End Set
    End Property
    Public Property StopBit() As DataStopBit
        Get
            Return mStopBit
        End Get
        Set(ByVal Value As DataStopBit)
            mStopBit = Value
        End Set
    End Property
    Public Property BaudRate() As Integer
        Get
            Return mBaudRate
        End Get
        Set(ByVal Value As Integer)
            mBaudRate = Value
        End Set
    End Property
    Public Property DataBit() As Integer
        Get
            Return mDataBit
        End Get
        Set(ByVal Value As Integer)
            mDataBit = Value
        End Set
    End Property
    Public Property BufferSize() As Integer
        Get
            Return mBufferSize
        End Get
        Set(ByVal Value As Integer)
            mBufferSize = Value
        End Set
    End Property
#End Region

#Region "Costruttori"

    Public Sub New()

        mPort = "1"   '//  Default is COM1	
        mTimeout = 70   '// Timeout in ms
        mBaudRate = 9600
        mParity = DataParity.Parity_None
        mStopBit = DataStopBit.StopBit_1
        mDataBit = 8
        mBufferSize = 512

        sp = New SerialPort

        myCrono = New Cronometro
        myCrono.Start()

        'Definizione tempi t1.5, t3.5 e Timeout
        '
        'I tempi seguenti sono aumentati rispetto alle specifiche in quanto con
        'un sistema operativo come Windows e un linguaggio di programmazione come Visual Basic
        'non e' possibile gestire fedelmente tempistiche dell'ordine di 750uS !!!
        'Secondo le specifiche i tempi dovrebbero essere il tempo di trasmissione di un carattere
        'di 11 bit moltiplicato per 1.5 e 3.5 fino a baudrate di 19200. Per baudrate superiori i tempi
        'restano fissi a 750uS per l'intercarattere (t1.5) e 1.750mS per l'intermessaggio (t3.5)
        'La formula per calcolare il tempo di trasmissione di un carattere e' la seguente:
        ' Time(mS) = (1000 x Bits_per_character) / Baud_rate
        'Comunque essendo questo un Master le tempistiche non rivestono importanza ai fini della
        'comunicazione. Si allungano solo i tempi di comunicazione.
        '
        TimeSilent35 = 200   'Tempo di 3.5 caratteri tra un frame e l'altro (mS)
        'Secondo le specifiche dovrebbe essere il tempo di trasmissione di
        '3.5 caratteri da 11bit dipendente dal baud rate. Al minimo deve essere
        '1.175mS
        TimeSilent15 = 150   'Tempo di 1.5 caratteri tra un carattere e l'altro (mS)
        'Secondo le specifiche dovrebbe essere il tempo di trasmissione di
        '1.5 caratteri da 11bit dipendente dal baud rate. Al minimo deve essere
        '0.175mS
        TimeoutAnswer = 2000  'Tempo di timeout ricezione da slave (mS)
        'E' impostabile a piacere secondo le proprie esigenze. Non e' menzionato
        'nelle specifiche Modbus

        Dim aaa() As Byte = {&H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, _
                 &H40, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, &H40, &H0, &HC1, &H81, &H40, &H1, &HC0, _
                 &H80, &H41, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, &H40, &H0, &HC1, &H81, &H40, &H1, _
                 &HC0, &H80, &H41, &H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41, &H1, &HC0, &H80, &H41, _
                 &H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, &H40, &H0, &HC1, &H81, _
                 &H40, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41, &H1, &HC0, _
                 &H80, &H41, &H0, &HC1, &H81, &H40, &H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41, &H1, _
                 &HC0, &H80, &H41, &H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, &H40, _
                 &H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, _
                 &H40, &H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, &H40, &H1, &HC0, _
                 &H80, &H41, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, &H40, &H0, &HC1, &H81, &H40, &H1, _
                 &HC0, &H80, &H41, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41, _
                 &H0, &HC1, &H81, &H40, &H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, _
                 &H40, &H1, &HC0, &H80, &H41, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, &H40, &H1, &HC0, _
                 &H80, &H41, &H0, &HC1, &H81, &H40, &H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41, &H1, _
                 &HC0, &H80, &H41, &H0, &HC1, &H81, &H40, &H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41, _
                 &H0, &HC1, &H81, &H40, &H1, &HC0, &H80, &H41, &H1, &HC0, &H80, &H41, &H0, &HC1, &H81, _
                 &H40}
        For i As Integer = 0 To 255
            CRCHi(i) = aaa(i)
        Next

        Dim bbb() As Byte = {&H0, &HC0, &HC1, &H1, &HC3, &H3, &H2, &HC2, &HC6, &H6, &H7, &HC7, &H5, &HC5, &HC4, _
                 &H4, &HCC, &HC, &HD, &HCD, &HF, &HCF, &HCE, &HE, &HA, &HCA, &HCB, &HB, &HC9, &H9, _
                 &H8, &HC8, &HD8, &H18, &H19, &HD9, &H1B, &HDB, &HDA, &H1A, &H1E, &HDE, &HDF, &H1F, &HDD, _
                 &H1D, &H1C, &HDC, &H14, &HD4, &HD5, &H15, &HD7, &H17, &H16, &HD6, &HD2, &H12, &H13, &HD3, _
                 &H11, &HD1, &HD0, &H10, &HF0, &H30, &H31, &HF1, &H33, &HF3, &HF2, &H32, &H36, &HF6, &HF7, _
                 &H37, &HF5, &H35, &H34, &HF4, &H3C, &HFC, &HFD, &H3D, &HFF, &H3F, &H3E, &HFE, &HFA, &H3A, _
                 &H3B, &HFB, &H39, &HF9, &HF8, &H38, &H28, &HE8, &HE9, &H29, &HEB, &H2B, &H2A, &HEA, &HEE, _
                 &H2E, &H2F, &HEF, &H2D, &HED, &HEC, &H2C, &HE4, &H24, &H25, &HE5, &H27, &HE7, &HE6, &H26, _
                 &H22, &HE2, &HE3, &H23, &HE1, &H21, &H20, &HE0, &HA0, &H60, &H61, &HA1, &H63, &HA3, &HA2, _
                 &H62, &H66, &HA6, &HA7, &H67, &HA5, &H65, &H64, &HA4, &H6C, &HAC, &HAD, &H6D, &HAF, &H6F, _
                 &H6E, &HAE, &HAA, &H6A, &H6B, &HAB, &H69, &HA9, &HA8, &H68, &H78, &HB8, &HB9, &H79, &HBB, _
                 &H7B, &H7A, &HBA, &HBE, &H7E, &H7F, &HBF, &H7D, &HBD, &HBC, &H7C, &HB4, &H74, &H75, &HB5, _
                 &H77, &HB7, &HB6, &H76, &H72, &HB2, &HB3, &H73, &HB1, &H71, &H70, &HB0, &H50, &H90, &H91, _
                 &H51, &H93, &H53, &H52, &H92, &H96, &H56, &H57, &H97, &H55, &H95, &H94, &H54, &H9C, &H5C, _
                 &H5D, &H9D, &H5F, &H9F, &H9E, &H5E, &H5A, &H9A, &H9B, &H5B, &H99, &H59, &H58, &H98, &H88, _
                 &H48, &H49, &H89, &H4B, &H8B, &H8A, &H4A, &H4E, &H8E, &H8F, &H4F, &H8D, &H4D, &H4C, &H8C, _
                 &H44, &H84, &H85, &H45, &H87, &H47, &H46, &H86, &H82, &H42, &H43, &H83, &H41, &H81, &H80, _
                 &H40}
        For i As Integer = 0 To 255
            CRCLo(i) = bbb(i)
        Next

    End Sub

#End Region

#Region "Open/close"
    Public Function Open(ByVal portName As String, ByVal baudRate As Integer, ByVal databits As Integer, ByVal parity As Parity, ByVal stopBits As StopBits) As Boolean

        mPort = portName
        mBaudRate = baudRate
        mDataBit = databits
        mParity = parity
        mStopBit = stopBits

        'Ensure port isn't already opened:
        If Not sp.IsOpen Then
            'Assign desired settings to the serial port:
            sp.PortName = portName
            sp.BaudRate = baudRate
            sp.DataBits = databits
            sp.Parity = parity
            sp.StopBits = stopBits
            'These timeouts are default and cannot be editted through the class at this point:
            sp.ReadTimeout = 1000
            sp.WriteTimeout = 1000

            sp.RtsEnable = True

            Try
                sp.Open()
                mStatus = "Done"
                Return True

            Catch Ex As Exception
                mStatus = "Error opening " & portName & ": " & Ex.Message
                'Throw New MyErrorException(mStatus)
                mErrorCode = -1
                mErrorMsg = mStatus
                Return False
            End Try

        Else
            mStatus = portName + " already opened"
            'Throw New MyErrorException(mStatus)
            mErrorCode = -2
            mErrorMsg = mStatus
            Return False
        End If

    End Function
    Public Function Close() As Boolean

        If sp.IsOpen Then
            Try
                sp.Close()
            Catch Ex As Exception
                mStatus = "Error closing " & sp.PortName & ": " & Ex.Message
                'Throw New MyErrorException(mStatus)
                mErrorCode = -3
                mErrorMsg = mStatus
                Return False
            End Try
            mStatus = "Done"
            Return True
        Else
            mStatus = "Done"
            Return True
        End If

    End Function

#End Region


#Region "CRC Computation"
    Private Sub GetCRC(ByRef message As Byte(), ByRef CRClow As Byte, ByRef CRChigh As Byte)

        'Function expects a modbus message of any length as well as a 2 byte CRC array in which to 
        'return the CRC values:

        Dim CRCFull As UShort = &HFFFF
        'Dim CRCHigh As Byte = &HFF
        'Dim CRCLow As Byte = &HFF
        Dim CRCLSB As Byte

        CRClow = &HFF
        CRChigh = &HFF
        For i As Integer = 0 To message.Length - 2
            CRCFull = CType((CRCFull Xor message(i)), UShort)
            For j As Integer = 0 To 8
                CRCLSB = CByte(CRCFull And &H1)
                CRCFull = CType(((CRCFull >> 1) And &H7FFF), UShort)
                If (CRCLSB = 1) Then
                    CRCFull = CType((CRCFull Xor &HA001), UShort)
                End If
            Next
        Next
        CRChigh = CByte((CRCFull >> 8) And &HFF)
        CRClow = CByte(CRCFull And &HFF)


        'ushort CRCFull = 0xFFFF;
        'byte CRCHigh = 0xFF, CRCLow = 0xFF;
        'char CRCLSB;
        'for (int i = 0; i < (message.Length) - 2; i++)
        '{
        '    CRCFull = (ushort)(CRCFull ^ message[i]);

        '    for (int j = 0; j < 8; j++)
        '    {
        '        CRCLSB = (char)(CRCFull & 0x0001);
        '        CRCFull = (ushort)((CRCFull >> 1) & 0x7FFF);

        '        if (CRCLSB == 1)
        '            CRCFull = (ushort)(CRCFull ^ 0xA001);
        '    }
        '}
        'CRC(1) = CRCHigh = (byte)((CRCFull >> 8) & 0xFF);
        'CRC(0) = CRCLow = (byte)(CRCFull & 0xFF);


    End Sub

    Private Function CalcCRC16(ByVal messaggio() As Byte, ByRef CRClow As Byte, ByRef CRChigh As Byte) As Long
        '************************************************************
        '* scopo: Calcola CRC16 da accodare al messaggio
        '************************************************************
        '* input: Messaggio() Byte, Num_dati Integer
        '* output: CRC16 (Long)
        '* verify:
        '************************************************************

        Dim QtaDati As Integer
        Dim uCRCHi As Byte
        Dim uCRCLo As Byte
        Dim uIndex As Byte
        Dim iDati As Integer

        uCRCHi = &HFF                               'Inizializza checksum Hi
        uCRCLo = &HFF                               'Inizializza checksum Lo
        iDati = 0                                       'Inizializza indice
        QtaDati = messaggio.Length - 2

        While (iDati < QtaDati)                      'Scorri tutti i caratteri del messaggio
            uIndex = uCRCHi Xor messaggio(iDati)        'Trova l'indice per entrare nella tabella CRCHi
            uCRCHi = uCRCLo Xor CRCHi(uIndex)       'Ricava parte alta del CRC16
            uCRCLo = CRCLo(uIndex)                  'Ricava parte bassa del CRC 16
            iDati = iDati + 1                               'Incrementa indice
        End While

        CRClow = uCRCLo
        CRChigh = uCRCHi

        'Componi uCRCHi e uCRCLo in un long per la mancanza di un tipo unsigned integer
        Return (CLng(uCRCHi) * 256) + CLng(uCRCLo)  'Calcola il valore da accodare al messaggio

    End Function

#End Region

#Region "Build Message"
    Private Sub BuildMessage(ByVal address As Byte, ByVal type As Byte, ByVal start As UInt16, ByVal registers As UInt16, ByRef message() As Byte)

        'Array to receive CRC bytes:
        Dim CRClow As Byte
        Dim CRChigh As Byte

        message(0) = address
        message(1) = type
        message(2) = CByte(start >> 8)
        message(3) = CByte(start)
        message(4) = CByte(registers >> 8)
        message(5) = CByte(registers)

        'Call GetCRC(message, CRClow, CRChigh)
        Call CalcCRC16(message, CRClow, CRChigh)
        'message(6) = CRChigh
        'message(7) = CRClow
        message(message.Length - 2) = CRChigh
        message(message.Length - 1) = CRClow
    End Sub

#End Region


#Region "Check Response"
    Private Function CheckResponse(ByVal response() As Byte) As Boolean

        'Perform a basic CRC check:
        Dim CRClow As Byte
        Dim CRChigh As Byte
        If response.Length > 4 Then
            If response(0) <> &H70 Then
                'GetCRC(response, CRClow, CRChigh)
                Call CalcCRC16(response, CRClow, CRChigh)
                If CRClow = response(response.Length - 1) And CRChigh = response(response.Length - 2) Then
                    mErrorCode = 0
                    mErrorMsg = ""
                    Return True
                Else
                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    'mErrorCode = 0
                    'mErrorMsg = ""
                    'Return True
                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                    mErrorCode = -11
                    mErrorMsg = "CRC error"
                    Return False
                End If
            Else
                'Docodifica errore
                mErrorCode = response(0)
                Select Case mErrorCode
                    Case 1
                        mErrorMsg = "ILLEGAL FUNCTION"
                    Case 2
                        mErrorMsg = "ILLEGAL DATA ADDRESS"
                    Case 3
                        mErrorMsg = "ILLEGAL DATA VALUE"
                    Case 4
                        mErrorMsg = "SLAVE DEVICE FAILURE"
                    Case 5
                        mErrorMsg = "ACKNOWLEDGE"
                    Case 6
                        mErrorMsg = "SLAVE DEVICE BUSY"
                    Case 8
                        mErrorMsg = "MEMORY PARITY ERROR"
                    Case 10
                        mErrorMsg = "GATEWAY PATH UNAVAILABLE"
                    Case 11
                        mErrorMsg = "GATEWAY TARGET DEVICE FAILED TO RESPOND"
                    Case Else
                        mErrorMsg = "Unknown error"
                End Select
                Return False
            End If
        Else
            mErrorCode = -12
            mErrorMsg = "Message too short"
            Return False
        End If

    End Function
#End Region


#Region "Send query and Get Response"
    Private Sub SendQuery(ByVal sQuery() As Byte)

        'Pulizia buffer di ricezione e trasmissione
        sp.DiscardInBuffer()
        sp.DiscardOutBuffer()

        '---- Invio messaggio ----
        Try

            sp.Write(sQuery, 0, sQuery.Length)
            '---- Attendi invio messaggio in seriale ----
            Do
                Threading.Thread.Sleep(10)
            Loop While (sp.BytesToWrite <> 0)

        Catch ex As Exception
        End Try


    End Sub

    Private Function WaitAnswer(ByRef sAnswer() As Byte) As String
        '************************************************************
        '* scopo: Attende la risposta dallo slave
        '************************************************************
        '* input:
        '* output:
        '* verify:
        '************************************************************
        Dim LenMsgRic As Integer                  'Byte messaggio ricevuto
        Dim MessaggioRic(260) As Byte              'Buffer messaggio in ricezione
        Dim Rit_com As Integer                   'Numero tentativi di trasmissione messaggio a slave


        '---- Azzera variabili ----
        Rit_com = 0                                         'Azzera tentativi di ritrasmissione messaggio

        '---- Ricezione messaggio ----
        Do                                                  'Loop di trasmissione messaggio a slave
            myCrono.Start()
            LenMsgRic = 0                                   'Azzera caratteri messaggio di ritorno
            Do                                                'Loop di attesa risposta da slave
                '---- Passaggio caratteri da MSComm a buffer di ricezione ----
                If (sp.BytesToRead <> 0) Then    'Se arrivati caratteri, porta in buffer
                    '---- Controllo tempo intercarattere ----
                    If (myCrono.ElapsedMilliseconds > TimeSilent15) Then  'Se arrivati caratteri dopo un tempo di pausa maggiore di 1.5 caratteri
                        LenMsgRic = 0                             'Azzera messaggio ricevuto fino a questo momento
                    End If

                    '---- Passa caratteri ricevuti in seriale sul buffer di ricezione ----
                    MessaggioRic(LenMsgRic) = sp.ReadByte()
                    LenMsgRic = LenMsgRic + 1                 'Incrementa contatore caratteri ricevuti
                    'Reimposta il tempo di arrivo dell'ultimo carattere
                    myCrono.Start()
                End If

                '---- Controllo tempo intermessaggio --------
                '---- Per far partire questo tempo devo avere almeno un carattere in buffer
                If ((LenMsgRic <> 0) And (myCrono.ElapsedMilliseconds > TimeSilent35)) Then 'Se e' arrivato almeno un carattere, controlla t35
                    Exit Do                                       'Esci dalla routine per messaggio ricevuto
                End If

                '---- Controllo timeout ricezione messaggio da slave
                If (myCrono.ElapsedMilliseconds > TimeoutAnswer) Then
                    LenMsgRic = 0                               'Azzera numero caratteri ricevuti da seriale
                    Exit Do                                       'esci dalla rutine di ricezione per timeout
                End If
            Loop                                              'Loop infinito di attesa risposta da slave

            '---- Controllo checksum messaggio ricevuto --------
            '---- e uscita per timeout da loop di ricezione ----
            If (LenMsgRic >= 4) Then                        'Al minimo devo aver ricevuto 4 caratteri dalla seriale
                If CheckResponse(MessaggioRic) Then
                    ReDim sAnswer(LenMsgRic)
                    For i As Integer = 0 To LenMsgRic
                        sAnswer(i) = MessaggioRic(i)
                    Next
                    Return System.Text.Encoding.ASCII.GetString(MessaggioRic)
                Else
                    myCrono.Start()                     'leggi tempo attuale
                    Do
                        'DoEvents                                   'Esegui eventi del computer
                    Loop While (myCrono.ElapsedMilliseconds < TimeSilent35)    'Attendi tempo 3.5 caratteri prima di ritrasmissione
                    Rit_com += 1
                End If
            Else                                              'Se uscita per timeout
                Rit_com += 1                         'Incrementa contatore ripetizioni trasmissioni
            End If

            'DoEvents                                         'Esegui eventi del sistema operativo
        Loop Until Rit_com = RIP_COM                        'prova per il numero massimo di tentativi

        '---- Errore di timeout dopo 3 tentativi di trasmissione ----
        If Rit_com = RIP_COM Then                           'se eseguiti tutti i tentativi
            'MsgBox("Errore in comunicazione !" & vbCrLf & "Controlla la connessione !", vbCritical, "Errore")
            ReDim sAnswer(2)
            sAnswer(0) = &H70
            sAnswer(1) = &H1
            Throw New MyErrorException("Communication error. Check wiring.")                                   'Esci dalla subroutine
        End If

        Return ""

    End Function

    Private Function ReadAnswer(ByRef sAnswer() As Byte) As Boolean
        '************************************************************
        '* scopo: Legge la risposta dallo slave
        '************************************************************
        Dim LenMsgRic As Integer                  'Byte messaggio ricevuto
        Dim MessaggioRic() As Byte              'Buffer messaggio in ricezione

        ReDim Preserve MessaggioRic(0)

        myCrono.Start()
        LenMsgRic = 0                                   'Azzera caratteri messaggio di ritorno
        Do                                                'Loop di attesa risposta da slave
            '---- Passaggio caratteri da MSComm a buffer di ricezione ----
            If (sp.BytesToRead <> 0) Then    'Se arrivati caratteri, porta in buffer
                '---- Controllo tempo intercarattere ----
                If (myCrono.ElapsedMilliseconds > TimeSilent15) Then  'Se arrivati caratteri dopo un tempo di pausa maggiore di 1.5 caratteri
                    LenMsgRic = 0                             'Azzera messaggio ricevuto fino a questo momento
                End If

                '---- Passa caratteri ricevuti in seriale sul buffer di ricezione ----
                ReDim Preserve MessaggioRic(LenMsgRic)
                MessaggioRic(LenMsgRic) = sp.ReadByte()
                LenMsgRic = LenMsgRic + 1                 'Incrementa contatore caratteri ricevuti
                'Reimposta il tempo di arrivo dell'ultimo carattere
                myCrono.Start()
            End If

            '---- Controllo tempo intermessaggio --------
            '---- Per far partire questo tempo devo avere almeno un carattere in buffer
            If ((LenMsgRic <> 0) And (myCrono.ElapsedMilliseconds > TimeSilent35)) Then 'Se e' arrivato almeno un carattere, controlla t35
                Exit Do                                       'Esci dalla routine per messaggio ricevuto
            End If

            '---- Controllo timeout ricezione messaggio da slave
            If (myCrono.ElapsedMilliseconds > TimeoutAnswer) Then
                LenMsgRic = 0                               'Azzera numero caratteri ricevuti da seriale
                Exit Do                                       'esci dalla rutine di ricezione per timeout
            End If
        Loop                                              'Loop infinito di attesa risposta da slave

        ReDim sAnswer(LenMsgRic - 1)
        For i As Integer = 0 To LenMsgRic - 1
            sAnswer(i) = MessaggioRic(i)
        Next
        'Return System.Text.Encoding.ASCII.GetString(MessaggioRic)
        Return True

        ''---- Controllo checksum messaggio ricevuto --------
        ''---- e uscita per timeout da loop di ricezione ----
        'If (LenMsgRic >= 4) Then                        'Al minimo devo aver ricevuto 4 caratteri dalla seriale
        '    If CheckResponse(MessaggioRic) Then
        '        ReDim sAnswer(LenMsgRic - 1)
        '        For i As Integer = 0 To LenMsgRic - 1
        '            sAnswer(i) = MessaggioRic(i)
        '        Next
        '        'Return System.Text.Encoding.ASCII.GetString(MessaggioRic)
        '        Return True
        '    End If
        'End If

        'Return False


    End Function



#End Region

#Region "Async functions"

#Region "Function 3 - Read Registers"
    Public Function BeginSendFc3(ByVal SlaveAddress As Byte, ByVal StartAddress As UInt16, ByVal RegistersQty As UInt16) As Boolean

        'Ensure port is open:
        If (sp.IsOpen) Then
            'Clear in/out buffers:
            sp.DiscardOutBuffer()
            sp.DiscardInBuffer()

            'Function 3 request is always 8 bytes:
            Dim Message(8 - 1) As Byte
            'Function 3 response buffer:
            Dim Response(5 + (2 * RegistersQty) - 1) As Byte
            'Build outgoing modbus message:
            BuildMessage(SlaveAddress, CByte(3), StartAddress, RegistersQty, Message)
            'Send modbus message to Serial Port:
            Try
                SendQuery(Message)
                mErrorCode = 0
                mErrorMsg = ""
                Return True
            Catch ex As Exception
                mStatus = "Error in begin read event: " + ex.Message
                mErrorCode = -8
                mErrorMsg = mStatus
                Return False
            End Try
        Else
            mStatus = "Serial port not open"
            mErrorCode = -9
            mErrorMsg = mStatus
            Return False
        End If

    End Function
    Public Function EndSendFc3(ByVal SlaveAddress As Byte, ByVal StartAddress As UInt16, ByVal RegistersQty As UInt16, ByRef values() As Int32) As Boolean

        'Ensure port is open:
        If (sp.IsOpen) Then

            'Function 3 response buffer:
            Dim Response(5 + (2 * RegistersQty) - 1) As Byte

            Try
                If ReadAnswer(Response) Then
                    If (CheckResponse(Response)) Then
                        For i As Integer = 0 To ((Response.Length - 5) \ 2) - 1
                            If i >= RegistersQty Then
                                mStatus = "Wrong answer"
                                mErrorMsg = "Wrong answer"
                                Return False
                                Exit For
                            End If
                            values(i) = Response(2 * i + 3)
                            values(i) <<= 8
                            values(i) += Response(2 * i + 4)
                        Next
                        mStatus = "Read successful"
                        mErrorCode = 0
                        mErrorMsg = ""
                        Return True
                    Else
                        mStatus = mErrorMsg
                        Return False
                    End If
                End If
            Catch ex As Exception
                mStatus = "Error in end read answer: " + ex.Message
                mErrorCode = -8
                mErrorMsg = mStatus
                Return False
            End Try
        Else
            mStatus = "Serial port not open"
            mErrorCode = -9
            mErrorMsg = mStatus
            Return False
        End If

    End Function


#End Region
#Region "Function 16 - Write Multiple Registers"
    Public Function BeginSendFc16(ByVal address As Byte, ByVal start As UInt16, ByVal registers As UInt16, ByVal values() As Integer) As Boolean
        'Ensure port is open:
        If (sp.IsOpen) Then
            ''Clear in/out buffers:
            sp.DiscardOutBuffer()
            sp.DiscardInBuffer()
            ''Message is 1 addr + 1 fcn + 2 start + 2 reg + 1 count + 2 * reg vals + 2 CRC
            Dim Message(9 + (2 * registers) - 1) As Byte

            ''Add bytecount to message:
            Message(6) = CByte(registers * 2)
            ''Put write values into message prior to sending:
            For i As Integer = 0 To registers - 1
                Message(7 + 2 * i) = CByte(values(i) >> 8)
                Message(8 + 2 * i) = CByte(values(i))
            Next i
            ''Build outgoing message:
            BuildMessage(address, CByte(16), start, registers, Message)

            ''Send Modbus message to Serial Port:
            Try
                SendQuery(Message)
                mErrorCode = 0
                mErrorMsg = ""
                Return True
            Catch ex As Exception
                mStatus = "Error in begin read event: " + ex.Message
                mErrorCode = -8
                mErrorMsg = mStatus
                Return False
            End Try
        Else
            mStatus = "Serial port not open"
            mErrorCode = -9
            mErrorMsg = mStatus
            Return False
        End If
    End Function
    Public Function EndSendFc16(ByVal address As Byte, ByVal start As UInt16, ByVal registers As UInt16) As Boolean
        'Ensure port is open:
        If (sp.IsOpen) Then
            
            ''Function 16 response is fixe d at 8 bytes
            Dim Response(8 - 1) As Byte

            ''Send Modbus message to Serial Port:
            Try
                If ReadAnswer(Response) Then
                    If (CheckResponse(Response)) Then
                        mStatus = "Write successful"
                        mErrorCode = 0
                        mErrorMsg = ""
                        Return True
                    Else
                        mStatus = mErrorMsg
                        Return False
                    End If
                Else
                    mStatus = mErrorMsg
                    Return False
                End If
            Catch ex As Exception
                mStatus = "Error in end write answer: " + ex.Message
                mErrorCode = -8
                mErrorMsg = mStatus
                Return False
            End Try
            
        Else
            mStatus = "Serial port not open"
            mErrorCode = -9
            mErrorMsg = mStatus
            Return False
        End If
    End Function


#End Region
#End Region


#Region "Sync functions"

#Region "Function 3 - Read Registers"
    Public Function SendFc3(ByVal SlaveAddress As Byte, ByVal StartAddress As UInt16, ByVal RegistersQty As UInt16, ByRef values() As Int16) As Boolean

        'Ensure port is open:
        If (sp.IsOpen) Then
            'Clear in/out buffers:
            sp.DiscardOutBuffer()
            sp.DiscardInBuffer()
            'Function 3 request is always 8 bytes:
            Dim Message(8) As Byte
            'Function 3 response buffer:
            Dim Response(5 + 2 * RegistersQty) As Byte
            'Build outgoing modbus message:
            BuildMessage(SlaveAddress, CByte(3), StartAddress, RegistersQty, Message)
            'Send modbus message to Serial Port:
            Try
                SendQuery(Message)
                WaitAnswer(Response)
            Catch ex As Exception
                mStatus = "Error in read event: " + ex.Message
                Return False
            End Try

            'Evaluate message:
            If (CheckResponse(Response)) Then
                'Return requested register values:
                'for (int i = 0; i < (response.Length - 5) / 2; i++)
                For i As Integer = 0 To Response.Length - 5 / 2
                    values(i) = Response(2 * i + 3)
                    values(i) <<= 8
                    values(i) += Response(2 * i + 4)
                Next
                mStatus = "Read successful"
                Return True
            Else
                mStatus = "CRC error"
                Return False
            End If
        Else
            mStatus = "Serial port not open"
            Return False
        End If

    End Function


#End Region

#Region "Function 16 - Write Multiple Registers"
    Public Function SendFc16(ByVal address As Byte, ByVal start As UInt16, ByVal registers As UInt16, ByVal values() As Int16) As Boolean
        'Ensure port is open:
        If (sp.IsOpen) Then
            ''Clear in/out buffers:
            sp.DiscardOutBuffer()
            sp.DiscardInBuffer()
            ''Message is 1 addr + 1 fcn + 2 start + 2 reg + 1 count + 2 * reg vals + 2 CRC
            Dim Message(9 + 2 * registers) As Byte
            ''Function 16 response is fixe d at 8 bytes
            Dim Response(8) As Byte

            ''Add bytecount to message:
            Message(6) = CByte(registers * 2)
            ''Put write values into message prior to sending:
            For i As Integer = 0 To registers
                Message(7 + 2 * i) = CByte(values(i) >> 8)
                Message(8 + 2 * i) = CByte(values(i))
            Next i
            ''Build outgoing message:
            BuildMessage(address, CByte(16), start, registers, Message)

            ''Send Modbus message to Serial Port:
            Try
                Call SendQuery(Message)
                Call WaitAnswer(Response)
            Catch ex As Exception
                mStatus = "Error in write event: " & ex.Message
                Throw New MyErrorException(mStatus)
                Return False
            End Try
            'Evaluate message:
            If (CheckResponse(Response)) Then
                mStatus = "Write successful"
                Return True
            Else
                mStatus = "CRC error"
                Throw New MyErrorException("CRC error")
                Return False
            End If
        Else
            mStatus = "Serial port not open"
            Throw New MyErrorException("Serial port not open")
            Return False
        End If
    End Function

#End Region

#End Region

End Class


Public Class ModBusClassTcp

    
    Public Event ConnectionFailed()
    'Private thrWatchDog As New Thread(AddressOf WatchDog)
    'Private WatchDogEvent As AutoResetEvent

    Private Const fctReadCoil As Byte = 1
    Private Const fctReadDiscreteInputs As Byte = 2
    Private Const fctReadHoldingRegister As Byte = 3
    Private Const fctReadInputRegister As Byte = 4
    Private Const fctWriteSingleCoil As Byte = 5
    Private Const fctWriteSingleRegister As Byte = 6
    Private Const fctWriteMultipleCoils As Byte = 15
    Private Const fctWriteMultipleRegister As Byte = 16


    'Private WithEvents srv As TcpServerClass


    Private MyClient As TcpClientClass
    Private mRemoteIP As String
    Private mRemotePort As Integer
    Private mUnitId As Byte

    Private mStatus As String
    Private mErrorCode As Integer
    Private mErrorMsg As String

    Private mConnected As Boolean

    Private TimeoutAnswer As Long          'Tempo di timeout attesa risposta da slave


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


#Region "Proprietà"

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
            Return MyClient.IsDataAvailable
        End Get
    End Property

    Public Property RemoteIp() As String
        Get
            Return mRemoteIP
        End Get
        Set(ByVal Value As String)
            mRemoteIP = Value
        End Set
    End Property

    Public Property RemotePort() As Integer
        Get
            Return mRemotePort
        End Get
        Set(ByVal Value As Integer)
            mRemotePort = Value
        End Set
    End Property
    Public Property UnitId() As Integer
        Get
            Return mUnitId
        End Get
        Set(ByVal Value As Integer)
            mUnitId = Value
        End Set
    End Property

#End Region

#Region "Costruttori"

    Public Sub New(ByVal sRemoteIP As String, ByVal iRemotePort As Integer, ByVal UnitId As Byte)

        mRemoteIP = sRemoteIP
        mRemotePort = iRemotePort
        mUnitId = UnitId

        TimeoutAnswer = 1000

        mStatus = ""
        mErrorCode = 0
        mErrorMsg = ""


        MyClient = New TcpClientClass
        mConnected = False

    End Sub

#End Region

#Region "Open/close"

    Public Function Open() As Boolean

        'mRemoteIP = sRemoteIP
        'mRemotePort = iRemotePort

        If MyClient.OpenConnection(mRemoteIP, mRemotePort) Then
            mConnected = True
            Return True
        End If
        Return False
    End Function
    Public Sub Close()
        MyClient.CloseConnection()
        mConnected = False
    End Sub


    
#End Region


#Region "CRC Computation"

#End Region

#Region "Build Message"

    Private Function CreateReadHeader(ByVal id As Integer, ByVal startAddress As Integer, ByVal length As Byte, ByVal FunctionCode As Byte) As Byte()
        Dim data(11) As Byte

        Dim _id() = BitConverter.GetBytes(CType(id, UInt16))
        data(0) = _id(1)                'Transaction id - high byte
        data(1) = _id(0)                'Transaction id - low byte
        data(2) = 0                     'Protocol Id - high byte (always 0)
        data(3) = 0                     'Protocol Id - low byte (always 0)
        data(4) = 0                     'Message size - high byte
        data(5) = 6                     'Message size - low byte  (6 bytes to follow)
        data(6) = mUnitId               'Unit id
        data(7) = FunctionCode          'Function code
        'Dim _adr() = BitConverter.GetBytes(CType(IPAddress.HostToNetworkOrder(CType(startAddress, UInt16)), UInt16))
        Dim _adr = BitConverter.GetBytes(CType(startAddress, UInt16))
        data(8) = _adr(1)               'Start address - high byte
        data(9) = _adr(0)               'Start address - low byte
        data(10) = 0                    'Number of data to read - high byte
        data(11) = length               'Number of data to read - low byte
        Return data

    End Function


    Private Function CreateWriteHeader(ByVal id As Integer, ByVal startAddress As Integer, ByVal numData As Integer, ByVal FunctionCode As Byte) As Byte()
        Dim data(12) As Byte
        Dim mSize As Short

        mSize = CType((7 + numData * 2), UInt16)

        Dim _id() = BitConverter.GetBytes(CType(id, UInt16))
        data(0) = _id(1)                'Transaction id - high byte
        data(1) = _id(0)                'Transaction id - low byte
        data(2) = 0                     'Protocol Id - high byte (always 0)
        data(3) = 0                     'Protocol Id - low byte (always 0)
        'Dim _size() = BitConverter.GetBytes(CType(IPAddress.HostToNetworkOrder(mSize), UInt16))
        Dim _size() = BitConverter.GetBytes(CType(mSize, UInt16))
        data(4) = _size(1)              'Message size - high byte
        data(5) = _size(0)              'Message size - low byte  
        data(6) = mUnitId               'Unit id
        data(7) = FunctionCode          'Function code
        'Dim _adr = BitConverter.GetBytes(CType(IPAddress.HostToNetworkOrder(CType(startAddress, UInt16)), UInt16))
        Dim _adr = BitConverter.GetBytes(CType(startAddress, UInt16))
        data(8) = _adr(1)               'Start address - high byte
        data(9) = _adr(0)               'Start address - low byte
        If (FunctionCode >= fctWriteMultipleCoils) Then
            'Dim cnt() = BitConverter.GetBytes(CType(IPAddress.HostToNetworkOrder(CType(numData, UInt16)), UInt16))
            Dim cnt() = BitConverter.GetBytes(CType(numData, UInt16))
            data(10) = cnt(1)           ' Number of register
            data(11) = cnt(0)           ' Number of register
            data(12) = CByte(numData * 2)
        End If
        Return data

    End Function

#End Region


#Region "Check Response"

    Private Function CheckResponse(ByVal Id As Byte, ByVal FunctionCode As Byte, ByVal RxValues() As Byte) As Boolean
        'RxValues(0)     Transaction id - high byte
        'RxValues(1)     Transaction id - low byte
        'RxValues(2)     Protocol Id - high byte (always 0)
        'RxValues(3)     Protocol Id - low byte (always 0)
        'RxValues(4)     Response size - high byte
        'RxValues(5)     Response size - high byte (4 byte to follow)
        'RxValues(6)     Unit id
        'RxValues(7)     Function code
        'RxValues(8)     Data byte count
        'RxValues(9...)  Data

        If Id = CByte((RxValues(0) * 256 + RxValues(1))) Then
            If mUnitId = RxValues(6) Then
                If FunctionCode = RxValues(7) Then
                    mErrorCode = 0
                    mErrorMsg = ""
                    Return True
                Else
                    'Docodifica errore
                    mErrorCode = RxValues(8)
                    Select Case mErrorCode
                        Case 1
                            mErrorMsg = "ILLEGAL FUNCTION"
                        Case 2
                            mErrorMsg = "ILLEGAL DATA ADDRESS"
                        Case 3
                            mErrorMsg = "ILLEGAL DATA VALUE"
                        Case 4
                            mErrorMsg = "SLAVE DEVICE FAILURE"
                        Case 5
                            mErrorMsg = "ACKNOWLEDGE"
                        Case 6
                            mErrorMsg = "SLAVE DEVICE BUSY"
                        Case 8
                            mErrorMsg = "MEMORY PARITY ERROR"
                        Case 10
                            mErrorMsg = "GATEWAY PATH UNAVAILABLE"
                        Case 11
                            mErrorMsg = "GATEWAY TARGET DEVICE FAILED TO RESPOND"
                        Case Else
                            mErrorMsg = "Unknown error"
                    End Select
                    Return False
                End If
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function

    Private Function DecodeReceivedData(ByVal RxValues() As Byte) As Short()
        Dim Values() As Short
        Dim DataQty As Byte

        'RxValues(0)     Transaction id - high byte
        'RxValues(1)     Transaction id - low byte
        'RxValues(2)     Protocol Id - high byte (always 0)
        'RxValues(3)     Protocol Id - low byte (always 0)
        'RxValues(4)     Response size - high byte
        'RxValues(5)     Response size - high byte (4 byte to follow)
        'RxValues(6)     Unit id
        'RxValues(7)     Function code
        'RxValues(8)     Data byte count
        'RxValues(9...)  Data

        DataQty = CByte(RxValues(8) \ 2)
        ReDim Values(DataQty - 1)
        For i As Integer = 0 To DataQty - 1
            Values(i) = RxValues(2 * i + 9)
            Values(i) <<= 8
            Values(i) += RxValues(2 * i + 10)
        Next
        Return Values

    End Function

#End Region


#Region "Send query and Get Response"
    'Private Sub SendQuery(ByVal sQuery() As Byte)
    '    Try
    '        Dim sData As String = ""
    '        For i As Integer = 0 To sQuery.Length
    '            sData = sData & sQuery(i)
    '        Next
    '        cli.OpenAndSendMessage(mRemoteIP, mRemotePort, sData)
    '    Catch ex As Exception
    '        'MessageBox.Show(ex.Message, "Error sending message")
    '        mStatus = "Error sending message : " & ex.Message
    '        Throw New MyErrorException(mStatus)
    '    End Try

    'End Sub


    'Private Function GetAnswer(ByRef sAnswer() As Byte) As String
    '    Dim MessaggioRic As String

    '    MessaggioRic = srv.WaitDataClient(5000)
    '    sAnswer = System.Text.Encoding.ASCII.GetBytes(MessaggioRic)
    '    Return MessaggioRic

    'End Function



#End Region

    

#Region "Async functions"

#Region "Read function"
    Public Function BeginSendFc3(ByVal Id As Byte, ByVal StartAddress As UInt16, ByVal RegistersQty As UInt16) As Boolean
        'Function 3 : Read register
        Dim Message(11) As Byte

        Try
            Message = CreateReadHeader(Id, StartAddress, RegistersQty, fctReadHoldingRegister)
            MyClient.SendData(Message)
            mStatus = "Begin read successful"
            Return True

        Catch ex As Exception
            mStatus = "Error in begin read event: " + ex.Message
            Return False
        End Try
        mStatus = "Read successful"
        Return True

    End Function

    Public Function EndSendFc3(ByVal Id As Byte, ByVal StartAddress As UInt16, ByVal RegistersQty As UInt16, ByRef values() As Int16) As Boolean
        'Function 3 : Read register
        Dim Response(10 + 2 * RegistersQty) As Byte
        Try
            MyClient.ReadData(Response)
            If CheckResponse(Id, fctReadHoldingRegister, Response) Then
                values = DecodeReceivedData(Response)
                mStatus = "Read successful"
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            mStatus = "Error in read event: " + ex.Message
            Return False
        End Try
        mStatus = "Read successful"
        Return True

    End Function


#End Region

#Region "Write Function "
    Public Function BeginSendFc5(ByVal Id As Integer, ByVal StartAddress As UInt16, ByVal bOn As Boolean) As Boolean
        'Function5: Write single coil
        Dim Message(12) As Byte     'NrByte + 11

        Message = CreateWriteHeader(Id, StartAddress, 1, fctWriteSingleCoil)
        If bOn Then
            Message(10) = &HFF
        Else
            Message(10) = &H0
        End If
        Try
            MyClient.SendData(Message)
            mStatus = "Begin Write successful"
            Return True
        Catch ex As Exception
            mStatus = "Error in begin write event: " + ex.Message
            Return False
        End Try

        mStatus = "Begin Write successful"
        Return True

    End Function
    Public Function EndSendFc5(ByVal Id As Integer, ByVal StartAddress As UInt16, ByVal bOn As Boolean, ByVal values() As Byte) As Boolean
        'Function5: Write single coil
        Dim Response(8) As Byte

        Try
            MyClient.ReadData(Response)
            mStatus = "Write successful"
            Return True
        Catch ex As Exception
            mStatus = "Error in write event: " + ex.Message
            Return False
        End Try

        mStatus = "Write successful"
        Return True

    End Function
    Public Function BeginSendFc16(ByVal Id As Integer, ByVal StartAddress As UInt16, ByVal numRegs As UInt16, ByVal values() As Int16) As Boolean
        'Function16: Write Multiple Registers
        Dim data(numRegs * 2 - 1) As Byte
        Dim numBytes As Byte
        Dim Message() As Byte

        'Metto i valori in un array di byte
        For i As Integer = 0 To numRegs - 1
            data(0 + 2 * i) = CByte(values(i) >> 8)
            data(1 + 2 * i) = CByte(values(i))
        Next i

        numBytes = CByte(numRegs * 2)
        Message = CreateWriteHeader(Id, StartAddress, numRegs, fctWriteMultipleRegister)
        ReDim Preserve Message(Message.Length + data.Length - 1)
        Array.Copy(data, 0, Message, 13, numBytes)
        Try
            MyClient.SendData(Message)
            mStatus = "Begin write successful"
            Return True
        Catch ex As Exception
            mStatus = "Error in begin write event: " + ex.Message
            Return False
        End Try

        mStatus = "Begin write successful"
        Return True
    End Function

    Public Function EndSendFc16(ByVal Id As Integer, ByVal StartAddress As UInt16, ByVal numRegs As UInt16) As Boolean
        'Function16: Write Multiple Registers
        Dim Response(8) As Byte

        Try
            MyClient.ReadData(Response)
            If CheckResponse(Id, fctWriteMultipleRegister, Response) Then
                mStatus = "Write successful"
                Return True
            Else
                mStatus = "Write failed"
                Return False
            End If
        Catch ex As Exception
            mStatus = "Error in write event: " + ex.Message
            Return False
        End Try

        mStatus = "Write successful"
        Return True
    End Function

#End Region

#End Region


#Region "Sync functions"

#Region "Read function"
    Public Function SendFc3(ByVal Id As Byte, ByVal StartAddress As UInt16, ByVal RegistersQty As UInt16, ByRef values() As Int16) As Boolean
        'Function 3 : Read register
        Dim cli As TcpClientClass
        Dim Message(12) As Byte
        Dim Response(10 + 2 * RegistersQty) As Byte

        cli = New TcpClientClass
        Try
            Message = CreateReadHeader(Id, StartAddress, RegistersQty, fctReadHoldingRegister)
            cli.OpenAndSendMessage(mRemoteIP, mRemotePort, Message)
            cli.WaitData(Response, TimeoutAnswer)
            For i As Integer = 0 To ((Response.Length - 10) / 2) - 1
                values(i) = Response(2 * i + 9)
                values(i) <<= 8
                values(i) += Response(2 * i + 10)
            Next
            mStatus = "Read successful"
            cli = Nothing
            Return True

        Catch ex As Exception
            mStatus = "Error in read event: " + ex.Message
            cli = Nothing
            Return False
        End Try

    End Function


#End Region

#Region "Write Function "
    Public Function SendFc5(ByVal Id As Integer, ByVal StartAddress As UInt16, ByVal bOn As Boolean, ByVal values() As Byte) As Boolean
        'Function5: Write single coil
        Dim Message(12) As Byte     'NrByte + 11
        Dim Response(8) As Byte

        Dim cli As TcpClientClass
        cli = New TcpClientClass

        Message = CreateWriteHeader(Id, StartAddress, 1, fctWriteSingleCoil)
        If bOn Then
            Message(10) = &HFF
        Else
            Message(10) = &H0
        End If
        Try
            cli.OpenAndSendMessage(mRemoteIP, mRemotePort, Message)
            cli.WaitData(Response, TimeoutAnswer)
            mStatus = "Write successful"
            cli = Nothing
            Return True

        Catch ex As Exception
            mStatus = "Error in write event: " + ex.Message
            cli = Nothing
            Return False
        End Try



    End Function
    Public Function SendFc16(ByVal Id As Integer, ByVal StartAddress As UInt16, ByVal numRegs As UInt16, ByVal values() As Int16) As Boolean
        'Function16: Write Multiple Registers
        Dim data(numRegs * 2) As Byte
        Dim numBytes As Byte
        Dim Message(numRegs + 11) As Byte
        Dim Response(8) As Byte

        Dim cli As TcpClientClass
        cli = New TcpClientClass

        'Metto i valori in un array di byte
        For i As Integer = 0 To numRegs - 1
            data(0 + 2 * i) = CByte(values(i) >> 8)
            data(1 + 2 * i) = CByte(values(i))
        Next i

        numBytes = CByte(numRegs * 2)
        Message = CreateWriteHeader(Id, StartAddress, numRegs, fctWriteMultipleRegister)
        Array.Copy(data, 0, Message, 13, numBytes)
        Try
            cli.OpenAndSendMessage(mRemoteIP, mRemotePort, Message)
            cli.WaitData(Response, TimeoutAnswer)
            mStatus = "Write successful"
            cli = Nothing
            Return True
        Catch ex As Exception
            mStatus = "Error in write event: " + ex.Message
            cli = Nothing
            Return False
        End Try


    End Function

#End Region

#End Region

End Class

