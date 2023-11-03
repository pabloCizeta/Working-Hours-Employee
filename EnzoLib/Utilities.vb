
Imports System
Imports System.IO
Imports System.IO.Compression
Imports System.Xml


Public Module Utilities

    Public Function LeggiVar(ByVal sNomeFile As String, ByVal DefaultValue As String) As String
        If My.Computer.FileSystem.FileExists(sNomeFile) Then
            Dim fs As New System.IO.FileStream(sNomeFile, FileMode.Open, FileAccess.Read, FileShare.None)
            Dim s As New System.IO.StreamReader(fs)
            Dim sRiga As String
            sRiga = s.ReadLine
            s.Close()
            fs.Close()
            Return sRiga
        Else
            Return DefaultValue
        End If
    End Function
    Public Sub SalvaVar(ByVal Valore As String, ByVal sNomeFile As String)
        Dim fs As New System.IO.FileStream(sNomeFile, FileMode.Create, FileAccess.Write, FileShare.None)
        Dim s As New System.IO.StreamWriter(fs)
        Dim sRiga As String = Valore
        s.WriteLine(sRiga)
        s.Close()
        fs.Close()
    End Sub

    Public Sub CreaDir(ByVal sPath As String)
        Dim iDir As Long
        Dim sTmpPath As String

        Dim sChildPath() As String
        sChildPath = sPath.Split("\")
        sTmpPath = ""
        Try
            For iDir = 0 To sChildPath.Length - 1
                sTmpPath = sTmpPath & sChildPath(iDir)
                If Not My.Computer.FileSystem.DirectoryExists(sTmpPath) Then
                    My.Computer.FileSystem.CreateDirectory(sTmpPath)
                End If
                sTmpPath = sTmpPath & "\"
            Next iDir
        Catch
        End Try
    End Sub

    Public Function TextToInt(ByVal sValore As String) As Integer
        If IsNumeric(sValore) Then
            Return Convert.ToInt32(sValore)
        Else
            Return 0
        End If
    End Function
    Public Function TextTolong(ByVal sValore As String) As Long
        If IsNumeric(sValore) Then
            Return Convert.ToInt64(sValore)
        Else
            Return 0
        End If
    End Function

    Public Function TextToSingle(ByVal sValore As String) As Single

        Dim DecimalSeparator As String

        Dim Info1 As System.Globalization.NumberFormatInfo
        Info1 = System.Globalization.CultureInfo.CurrentUICulture.NumberFormat
        '  DecimalSeparator = Info1.CurrencyDecimalSeparator
        DecimalSeparator = Info1.NumberDecimalSeparator


        If DecimalSeparator = "," Then
            sValore = sValore.Replace(".", ",")
        Else
            sValore = sValore.Replace(",", ".")
        End If

        If IsNumeric(sValore) Then
            Return Convert.ToSingle(sValore)
        Else
            Return 0
        End If
    End Function


    Public Function IntegerToBitArray(ByVal iVal As Integer) As BitArray
        'Dim myB As BitArray
        'myB = IntegerToBitArray(65535)

        Dim myBitArray = New BitArray(System.BitConverter.GetBytes(iVal))
        Return myBitArray

        'Dim myBinaryString As String = Convert.ToString(iVal, 2)   'Convert integer to biary string.
        'Dim myBitArray As New BitArray(myBinaryString.Length)           'Initialise bit array.

        ''Store True for every "1" and False for every "0".
        'For i As Integer = 0 To myBitArray.Length - 1 Step 1
        '    myBitArray(i) = (myBinaryString.Chars(i) = "1"c)
        'Next i

        'Return myBitArray


        ''declare a test array
        'Dim testArray As Byte() = {0, 0, 0, 0}
        ''wrap it into a memory stream
        'Dim memStream As System.IO.MemoryStream = New System.IO.MemoryStream(testArray)
        ''wrap the stream in a binary reader
        'Dim bReader As System.IO.BinaryReader = New System.IO.BinaryReader(memStream)
        ''read a 32bit integer from the stream using the reader
        'Dim count As Integer = bReader.ReadInt32()



    End Function

    Public Function IntegerToByteArray(ByVal iVal As Integer) As Byte()
        Dim myByteArray() As Byte

        myByteArray = (System.BitConverter.GetBytes(iVal))
        Return myByteArray

    End Function


    Public Function BitArrayToInt(ByVal myBitArray As BitArray) As Integer
        Dim b(3) As Byte

        myBitArray.CopyTo(b, 0)
        Dim i As Integer = BitConverter.ToInt32(b, 0)

        Return i

    End Function
    'Public Function CreateZipFile(ByVal ZipFileName As String, ByVal SourceFiles() As String) As Boolean

    '    'Creazioe file zip
    '    Dim strmZipOutputStream As New ZipOutputStream(File.Create(ZipFileName))

    '    Dim objCrc32 As New Crc32

    '    'Livello compressione: 0= no compressione, 9=max
    '    strmZipOutputStream.SetLevel(7)

    '    'Loop su tutti i file da zippare
    '    For Each FileName As String In SourceFiles

    '        'Nuova entry nel file ZIP
    '        'Dim newZipEntry As ZipEntry = New ZipEntry(Path.GetFileName(FileName))
    '        Dim newZipEntry As ZipEntry = New ZipEntry(FileName)

    '        'Apertura file da leggere
    '        Dim strmFile As FileStream = File.OpenRead(FileName)

    '        Dim bytesRead As Integer

    '        Dim mBuffer(199999) As Byte

    '        'Azzera ilCRC
    '        objCrc32.Reset()

    '        'Inserisce l'entry nello ZIP
    '        newZipEntry.DateTime = File.GetCreationTime(FileName)
    '        newZipEntry.Size = strmFile.Length
    '        strmZipOutputStream.PutNextEntry(newZipEntry)

    '        'Ciclo lettura file sorgente
    '        While strmFile.Position < strmFile.Length
    '            bytesRead = strmFile.Read(mBuffer, 0, mBuffer.Length)
    '            strmZipOutputStream.Write(mBuffer, 0, bytesRead)
    '            objCrc32.Update(mBuffer, 0, bytesRead)
    '        End While

    '        'Imposta CRC del nuovo file nello ZIP
    '        newZipEntry.Crc = objCrc32.Value

    '        'Chiusura file sorgente
    '        strmFile.Close()

    '    Next

    '    'Chiude file zip
    '    strmZipOutputStream.Finish()
    '    strmZipOutputStream.Close()


    'End Function

    Public Sub SetValueParamXml(ByVal xmlDoc As XmlDocument, ByVal sSessione As String, ByVal sChiave As String, ByVal vValue As Object)
        Dim xmlNode As XmlNode
        Dim xmlNodeList As XmlNodeList
        Dim xmlElement As XmlNode

        xmlNodeList = xmlDoc.SelectNodes(sSessione)
        For Each xmlNode In xmlNodeList
            xmlElement = xmlNode.SelectSingleNode(sChiave)
            If xmlElement.NodeType = XmlNodeType.Element Then
                If xmlElement.Name = sChiave Then
                    Select Case VarType(vValue)
                        Case vbBoolean
                            xmlElement.InnerText = IIf(vValue, "True", "False")
                        Case vbString
                            xmlElement.InnerText = vValue
                        Case vbByte
                            xmlElement.InnerText = Trim(Format(vValue))
                        Case vbInteger
                            xmlElement.InnerText = Trim(Format(vValue))
                        Case vbLong
                            xmlElement.InnerText = Trim(Format(vValue))
                        Case vbDouble
                            xmlElement.InnerText = Trim(Str(vValue))
                    End Select
                    Exit Sub
                End If
            End If
        Next

    End Sub

    Public Function GetValueNodeXml(ByVal xmlDoc As XmlDocument, ByVal sSessione As String, ByVal sChiave As String, ByVal vDefault As Object) As Object
        Dim xmlNode As XmlNode
        Dim xmlNodeList As XmlNodeList
        Dim xmlElement As XmlNode

        Try
            xmlNodeList = xmlDoc.SelectNodes(sSessione)
            For Each xmlNode In xmlNodeList
                xmlElement = xmlNode.SelectSingleNode(sChiave)
                If xmlElement.NodeType = XmlNodeType.Element Then
                    If xmlElement.Name = sChiave Then
                        Dim vLetto As String = xmlElement.InnerText
                        Select Case VarType(vDefault)
                            Case vbBoolean
                                Return IIf(UCase(Mid(vLetto, 1, 1)) = "F", False, True)
                            Case vbString
                                Return vLetto
                            Case vbByte
                                Return CByte(vLetto)
                            Case vbInteger
                                Return CInt(vLetto)
                            Case vbLong
                                Return CLng(vLetto)
                            Case vbDouble
                                Return CDbl(vLetto)
                        End Select
                    End If
                End If
            Next
            Return vDefault

        Catch ex As Exception
            Return vDefault

        End Try

    End Function

    Public Function Ping(ByVal IpAddress As String, ByVal Timeout As Integer) As Boolean
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
End Module



