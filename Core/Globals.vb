Imports System.Xml.Serialization
Imports System.IO
Imports System.Text
Imports System.Data.OleDb
Imports Core


Public Module ResPublic

    Public Const cLocalSqlServer = "LocalSqlServer"
    Public Const cRemoteSqlServer = "RemoteSqlServer"
    Public Const cTipoRimborso = "F"
    Public Const cMotivoRimborso = "R"
    Public Const cTrasfertaItalia = 46
    Public Const cTrasfertaItaliaHotel = 46
    Public Const cTrasfertaEstero = 77

    Public ClosingProgram As Boolean = False

    Public xGlobals As Globals
    Public xConfig As Configuration
    ' Public xDatabase As DatabaseManagement

    Public xUsers As Users

    Public xDiario As diario

    Public Class udtOre
        Public Property Lavoro As New TimeSpan
        Public Property Viaggio As New TimeSpan
        Public Property Straordinario As New TimeSpan
    End Class


    Public ReadOnly Property LocalIp As String
        Get
            Return System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName).AddressList(0).ToString()
        End Get
    End Property

    Public ReadOnly Property SqlDate As String
        Get
            Return Format(Date.Now, "yyyy-MM-dd") & "T" & Format(Date.Now, "HH:mm:ss") & "." & Date.Now.Millisecond
            ' Return Format(Date.Now, "yyyy-MM-dd") & "" & Format(Date.Now, "HH:mm:ss.nnn")

        End Get
    End Property

    Public ReadOnly Property TimeSep As String
        Get
            Dim Info As System.Globalization.DateTimeFormatInfo
            Info = System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat
            Return Info.TimeSeparator
        End Get
    End Property
    Public ReadOnly Property TimeFormat As String
        Get
            Return "HH" & TimeSep & "mm" & TimeSep & "ss"
        End Get
    End Property
    Public ReadOnly Property DateSep As String
        Get
            Dim Info As System.Globalization.DateTimeFormatInfo
            Info = System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat
            Return Info.DateSeparator
        End Get
    End Property
    Public ReadOnly Property DateFormat As String
        Get
            Return "MM" & DateSep & "dd" & DateSep & "yyyy"
        End Get
    End Property

    Public Function ThreadTimeout(ThreadSleepTime As Long) As Long
        Dim timeout As Long = 1000
        If ThreadSleepTime * 3 > timeout Then
            timeout = ThreadSleepTime * 3
        End If
        Return timeout
    End Function



    Public Function TextToByte(ByVal sValore As String) As Byte
        If IsNumeric(sValore) Then
            Dim num As Integer
            If Not Integer.TryParse(sValore, num) Then
                Dim s As Single = System.Convert.ToSingle(sValore)
                Return CByte(s)
            End If
            Return System.Convert.ToByte(sValore)
        Else
            Return 0
        End If
    End Function

    Public Function TextToInt(ByVal sValore As String) As Integer
        If IsNumeric(sValore) Then
            Dim num As Integer
            If Not Integer.TryParse(sValore, num) Then
                Dim s As Single = System.Convert.ToSingle(sValore)
                Return CInt(s)
            End If
            Return System.Convert.ToInt32(sValore)
        Else
            Return 0
        End If
    End Function

    Public Function TextTolong(ByVal sValore As String) As Long
        If IsNumeric(sValore) Then
            Return System.Convert.ToInt64(sValore)
        Else
            Return 0
        End If
    End Function

    Public Function TextToSingle(ByVal sValore As String) As Single
        If IsNumeric(sValore) Then
            Return System.Convert.ToSingle(sValore)
        Else
            Return 0
        End If
    End Function

    Public Function TimeSpanToText(ts As TimeSpan) As String
        Return (ts.Days * 24 + ts.Hours).ToString & ":" & Math.Abs(ts.Minutes).ToString
    End Function
    Public Function TextToTimeSpan(str As String) As TimeSpan
        Dim aryStr As String() = str.Split(":")
        Select Case aryStr.Count
            Case 0
                Return New TimeSpan()
            Case 1
                Return New TimeSpan(TextToInt(aryStr(0)), 0, 0)
            Case 2
                Return New TimeSpan(TextToInt(aryStr(0)), TextToInt(aryStr(1)), 0)
            Case Else
                Return New TimeSpan
        End Select
    End Function


    Public Function GetOre(dgv As System.Windows.Forms.DataGridView, iColOreLavoro As Integer, iColOreViaggio As Integer) As udtOre
        Dim Hour As New udtOre

        For iRow As Integer = 0 To dgv.Rows.Count - 1

            Dim ts As TimeSpan = OreToTimespan(dgv.Rows(iRow).Cells(iColOreLavoro).Value)
            Hour.Lavoro = Hour.Lavoro.Add(ts)

            ts = OreToTimespan(dgv.Rows(iRow).Cells(iColOreViaggio).Value)
            Hour.Viaggio = Hour.Viaggio.Add(ts)

        Next

        Return Hour

    End Function
    Public Function GetOre(dgv As List(Of Diario.udtDgvOreLavoro)) As udtOre
        Dim Hour As New udtOre

        For Each item As Diario.udtDgvOreLavoro In dgv
            Dim ts As TimeSpan = OreToTimespan(item.OreLavoro)
            Hour.Lavoro = Hour.Lavoro.Add(ts)

            ts = OreToTimespan(item.OreViaggio)
            Hour.Viaggio = Hour.Viaggio.Add(ts)
        Next

        Return Hour

    End Function
    Public Function GetOre(dt As DataTable, iColOreLavoro As Integer, iColOreViaggio As Integer) As udtOre
        Dim Hour As New udtOre

        For iRow As Integer = 0 To dt.Rows.Count - 1

            Dim ts As TimeSpan = OreToTimespan(dt.Rows(iRow).Item(iColOreLavoro).ToString)
            Hour.Lavoro = Hour.Lavoro.Add(ts)

            ts = OreToTimespan(dt.Rows(iRow).Item(iColOreViaggio).ToString)
            Hour.Viaggio = Hour.Viaggio.Add(ts)

        Next

        Return Hour

    End Function
    Public Function OreToTimespan(Tempo As String) As TimeSpan
        Dim ts As TimeSpan

        Dim str As String = Tempo   '.Replace(".", ":")
        If str.Contains(".") Then str = Tempo.Replace(".", ":")

        Dim OreLavoro As String = str
        Dim aryStr As String() = OreLavoro.Split(":")
        Dim ore As Integer = 0
        Dim minuti As Integer = 0
        If aryStr.Length = 2 Then
            ore = TextToInt(aryStr(0))
            minuti = TextToInt(aryStr(1))
        Else
            ore = TextToInt(aryStr(0))
            minuti = 0
        End If
        If minuti < 10 Then minuti = minuti * 10
        Dim giorni As Integer = ore \ 24
        ore = ore Mod (24)
        str = giorni.ToString & "." & ore.ToString & ":" & minuti.ToString
        ts = TimeSpan.Parse(str)
        Return TimeSpan.Parse(str)

    End Function


    Public Function GetWorkingHours(BeginDate As Date, EndDate As Date) As TimeSpan
        Dim Lavorative As New TimeSpan
        Dim actDate As New Date
        actDate = BeginDate

        Dim year As Integer = Date.Now.Year

        Dim Festivita As New List(Of DateTime)
        Festivita.Clear()
        Festivita.Add(New DateTime(year, 1, 1))     'Capodanno
        Festivita.Add(New DateTime(year, 1, 6))     'Epifania
        Festivita.Add(EasternDate(year))            'Pasqua
        Festivita.Add(EasternDate(year).AddDays(1)) 'Lunedi angelo
        Festivita.Add(New DateTime(year, 4, 25))    'Liberazione
        Festivita.Add(New DateTime(year, 5, 1))     'Lavoro
        Festivita.Add(New DateTime(year, 6, 2))     'Repubblica
        Festivita.Add(New DateTime(year, 8, 15))    'Ferragosto
        Festivita.Add(New DateTime(year, 11, 1))    'Santi
        Festivita.Add(New DateTime(year, 12, 8))    'Immacolata
        Festivita.Add(New DateTime(year, 12, 25))   'Natale
        Festivita.Add(New DateTime(year, 12, 26))   'S.Stafano

        ' Festivita.Add(xConfig.OreLavorod.FestaPatronale)   'S.Marziano  Festa patronale

        Do
            If (actDate.DayOfWeek = DayOfWeek.Saturday) Or (actDate.DayOfWeek = DayOfWeek.Sunday) Then
            ElseIf Festivita.Contains(actDate) Then
            Else
                Lavorative = Lavorative.Add(New TimeSpan(8, 0, 0))
            End If
            actDate = DateAdd("d", 1, actDate)
        Loop While (DateDiff("d", actDate, EndDate) >= 0)

        Return Lavorative

    End Function

    Private Function EasternDate(year As Integer) As Date
        Dim a As Double
        Dim b As Double
        Dim c As Double
        Dim d As Double
        Dim e As Double
        Dim m As Double
        Dim n As Double
        Dim day As Double
        Dim month As Double

        If (year <= 2099) Then
            m = 24
            n = 5
        ElseIf (year <= 2199) Then
            m = 24
            n = 6
        ElseIf (year <= 2299) Then
            m = 25
            n = 0
        ElseIf (year <= 2399) Then
            m = 26
            n = 1
        ElseIf (year <= 2499) Then
            m = 25
            n = 1
        End If

        a = year Mod 19
        b = year Mod 4
        c = year Mod 7
        d = ((19 * a) + m) Mod 30
        e = ((2 * b) + (4 * c) + (6 * d) + n) Mod 7

        If ((d + e) < 10) Then
            day = d + e + 22
            month = 3
        Else
            day = d + e - 9
            month = 4
        End If

        If (day = 26 And month = 4) Then
            day = 19
            month = 4
        End If

        If (day = 25 And month = 4 And d = 28 And e = 6 And a > 10) Then
            day = 18
            month = 4
        End If

        Return DateSerial(year, month, day)
    End Function




    Public Sub ExportDataTable(dsTab As DataTable, FileName As String)
        Dim IncludeHeaders As Boolean = True

        Using writer As StreamWriter = New StreamWriter(FileName)
            'Rfc4180Writer.WriteDataTable(sourceTable, writer, True)

            If (IncludeHeaders) Then
                Dim headerValues As List(Of String) = New List(Of String)()
                For Each column As DataColumn In dsTab.Columns
                    headerValues.Add(QuoteValue(column.ColumnName))
                Next
                writer.WriteLine(String.Join(";", headerValues))
            End If

            Dim items() As String = Nothing
            For Each row As DataRow In dsTab.Rows
                items = row.ItemArray.Select(Function(obj) QuoteValue(obj.ToString())).ToArray()
                writer.WriteLine(String.Join(";", items))
            Next

            writer.Flush()

        End Using

    End Sub
    Public Sub ExportDataTable(dgv As System.Windows.Forms.DataGridView, FileName As String)
        Dim IncludeHeaders As Boolean = True

        Using writer As StreamWriter = New StreamWriter(FileName)
            'Rfc4180Writer.WriteDataTable(sourceTable, writer, True)

            If (IncludeHeaders) Then
                Dim headerValues As List(Of String) = New List(Of String)()
                For Each column As System.Windows.Forms.DataGridViewTextBoxColumn In dgv.Columns
                    headerValues.Add(QuoteValue(column.HeaderText))
                Next
                writer.WriteLine(String.Join(";", headerValues))
            End If

            For iRow As Integer = 0 To dgv.Rows.Count - 1
                Dim str As String = ""
                For iCol As Integer = 0 To dgv.Columns.Count - 1
                    str = str & QuoteValue(dgv.Rows(iRow).Cells(iCol).Value.ToString()) & ";"
                Next
                writer.WriteLine(str)
            Next

            writer.Flush()

        End Using

    End Sub
    Private Function QuoteValue(ByVal value As String) As String
        Return String.Concat("""", value.Replace("""", """"""), """")
    End Function




End Module


Public Class Globals

    Public Release As String

    Public PicturePath As String
    Public DataPath As String
    ' Public DatabasePath As String


    Public Sub New()

    End Sub

End Class


Public Class enumeration

    Public Sub LoadEnumIntoComboBox(ByVal enumType As Type, ByRef cbo As System.Windows.Forms.ComboBox)

        cbo.Items.Clear()
        Dim enumItems() As System.Reflection.FieldInfo = enumType.GetFields
        For Each item As System.Reflection.FieldInfo In enumItems
            If item.IsLiteral Then
                cbo.Items.Add(item.Name)
            End If
        Next


    End Sub

    Public Function ComboBoxToEnum(ByVal enumType As Type, ByVal cbo As System.Windows.Forms.ComboBox) As Integer

        Dim enumItems() As System.Reflection.FieldInfo = enumType.GetFields
        For Each item As System.Reflection.FieldInfo In enumItems
            If item.IsLiteral Then
                If cbo.Text = item.Name Then
                    Return CType(item.GetValue(Nothing), Integer)
                    Exit For
                End If
            End If
        Next
        Return -1
    End Function

    Public Function EnumToList(ByVal enumType As Type) As List(Of String)
        Dim lst As New List(Of String)

        lst.Clear()
        Dim enumItems() As System.Reflection.FieldInfo = enumType.GetFields
        For Each item As System.Reflection.FieldInfo In enumItems
            If item.IsLiteral Then
                lst.Add(item.Name)
            End If
        Next

        Return lst
    End Function

    Public Function StringToEnum(ByVal enumType As Type, ByVal str As String) As Integer

        Dim enumItems() As System.Reflection.FieldInfo = enumType.GetFields
        For Each item As System.Reflection.FieldInfo In enumItems
            If item.IsLiteral Then
                If str = item.Name Then
                    Return CType(item.GetValue(Nothing), Integer)
                    Exit For
                End If
            End If
        Next

    End Function



    Public Shared Function GetValues(Of T)() As IEnumerable(Of T)
        Return [Enum].GetValues(GetType(T)).Cast(Of T)()

        'Use example
        'Dim types As IEnumerable(Of Test.enumTestType) = enumeration.GetValues(Of Test.enumTestType)
        'For Each t As Test.enumTestType In types
        'If t <> Test.enumTestType.None Then
        '            'Me.Add(New StationPartner(t, String.Format("{0}_1", t.ToString())))
        '            Console.WriteLine(t)
        '            Console.WriteLine(t.ToString)
        '        End If
        'Next

    End Function


End Class

Public Class Serialization
    Private x As XmlSerializer

    ' Private xFileDamaged As New frmFileDamaged
    Private _ArchiveFolderName As String = ""
    Private _FileName As String = ""
    Private _obj As Object = Nothing

    Private _err As String = ""

    Public ReadOnly Property Result As String
        Get
            Return _err
        End Get
    End Property

    Public Property IgnoreEnabled As Boolean = False


#Region "Consturctors"
    Public Sub New(ByVal FileNameXml As String, ByVal ObjectToSer As Object, ArchiveFolderName As String)
        _ArchiveFolderName = ArchiveFolderName
        _FileName = FileNameXml
        _obj = ObjectToSer
    End Sub
    Public Sub New(ByVal FileNameXml As String, ByVal ObjectToSer As Object)
        _ArchiveFolderName = ""
        _FileName = FileNameXml
        _obj = ObjectToSer
    End Sub
    Public Sub New(ByVal ObjectToSer As Object)
        _ArchiveFolderName = ""
        _FileName = ""
        _obj = ObjectToSer
    End Sub
#End Region

#Region "Methods"
    Public Function Deserialize() As Object
        Return myDeserialize()
    End Function
    Public Function Deserialize(ByVal FileNameXml As String) As Object
        _FileName = FileNameXml
        Return myDeserialize()
    End Function

    Public Sub Serialize()
        mySerialize()
    End Sub
    Public Sub Serialize(ByVal FileNameXml As String)
        _FileName = FileNameXml
        mySerialize()
    End Sub
#End Region


#Region "Private"

    Private Function DeserializeFile() As String
        Dim objStreamReader As StreamReader
        Dim _err As String = ""
        Try
            'Deserialize text file to a new object.
            x = New XmlSerializer(_obj.GetType)
            objStreamReader = New StreamReader(_FileName)
            _obj = x.Deserialize(objStreamReader)
        Catch ex As Exception
            _err = ex.Message
        Finally
            If My.Computer.FileSystem.FileExists(_FileName) Then
                objStreamReader.Close()
            End If
        End Try

        Return _err
    End Function

    Private Function myDeserialize() As Object

        _err = DeserializeFile()
        If _err = "" Then
            Return _obj
        Else
            'If _ArchiveFolderName <> "" Then
            '    For iDay As Integer = 1 To 6
            '        xFileDamaged.iDay = iDay
            '        xFileDamaged.IgnoreEnabled = IgnoreEnabled
            '        xFileDamaged.lblFileName.Text = _FileName
            '        xFileDamaged.ArchiveFolderName = _ArchiveFolderName
            '        xFileDamaged.txtError.Text = _err
            '        Dim DiagRes As Windows.Forms.DialogResult = xFileDamaged.ShowDialog()
            '        If DiagRes = DialogResult.OK Then
            '            _err = DeserializeFile()
            '            If _err = "" Then
            '                Exit For
            '            Else
            '                If iDay = 6 Then
            '                    _err = "RestoreFailed"
            '                    Exit For
            '                End If
            '            End If
            '        ElseIf DiagRes = DialogResult.Ignore Then
            '            _err = ""
            '            Exit For
            '        Else
            '            _err = "ForcedEndProgram"
            '            Exit For
            '        End If
            '    Next
            'Else
            '    xFileDamaged.iDay = 0
            '    xFileDamaged.IgnoreEnabled = IgnoreEnabled
            '    xFileDamaged.lblFileName.Text = _FileName
            '    xFileDamaged.txtError.Text = _err
            '    Dim DiagRes As Windows.Forms.DialogResult = xFileDamaged.ShowDialog()
            '    If DiagRes = DialogResult.Ignore Then
            '        _err = ""
            '    Else
            '        _err = "ForcedEndProgram"
            '    End If
            'End If            
        End If


        Return _obj
    End Function


    Private Function myDeserializeOld() As Object
        Dim objStreamReader As StreamReader
        Try
            'Deserialize text file to a new object.
            If My.Computer.FileSystem.FileExists(_FileName) Then
                x = New XmlSerializer(_obj.GetType)
                objStreamReader = New StreamReader(_FileName)
                _obj = x.Deserialize(objStreamReader)
                objStreamReader.Close()
                Return _obj
            End If
        Catch ex As Exception

        End Try


        Return Nothing
    End Function

    Private Sub mySerialize()
        'Serialize object to a text file.
        If _FileName <> "" Then
            x = New XmlSerializer(_obj.GetType)
            Dim objStreamWriter As New StreamWriter(_FileName)
            x.Serialize(objStreamWriter, _obj)
            objStreamWriter.Close()
        End If
    End Sub

#End Region

End Class

Public Class TextsManagement

    Public Enum Languages
        Ita
        Eng
        Local
    End Enum

    Public Class udtMultiLanguageText
        Public Property Ita As String = ""
        Public Property Eng As String = ""
        Public Property Loc As String = ""
        Public Sub New()
        End Sub
        Public Overloads Function DeepCopy() As udtMultiLanguageText
            Dim other As udtMultiLanguageText = DirectCast(Me.MemberwiseClone(), udtMultiLanguageText)
            Return other
        End Function
    End Class

    Public Class udtText
        Public Property Name As String = ""
        Public Property MultiLang As New udtMultiLanguageText
        Public Sub New()
        End Sub
        Public Sub New(Name As String, Ita As String, Eng As String, Loc As String)
            Me.Name = Name
            MultiLang.Ita = Ita
            MultiLang.Eng = Eng
            MultiLang.Loc = Loc
        End Sub
        Public Overloads Function DeepCopy() As udtText
            Dim other As udtText = DirectCast(Me.MemberwiseClone(), udtText)
            other.MultiLang = New udtMultiLanguageText
            other.MultiLang = MultiLang.DeepCopy
            Return other
        End Function
    End Class

    Public Class udtFormTexts
        Public Property FormName As String = ""
        Public Property Texts As New List(Of udtText)
        Public Sub New()
        End Sub
        Public Overloads Function DeepCopy() As udtFormTexts
            Dim other As udtFormTexts = DirectCast(Me.MemberwiseClone(), udtFormTexts)
            other.Texts.Clear()
            For Each item As udtText In Texts
                other.Texts.Add(item)
            Next
            Return other
        End Function
    End Class

    Public Class FormsTextsDefs

        Private ser As Serialization
        Private FileNameXml As String

        Public Class udtParams

            Public Property FormsTexts As New List(Of TextsManagement.udtFormTexts)

            Public Overloads Function DeepCopy() As udtParams
                Dim other As udtParams = DirectCast(Me.MemberwiseClone(), udtParams)
                other.FormsTexts = New List(Of TextsManagement.udtFormTexts)
                For Each item As TextsManagement.udtFormTexts In FormsTexts
                    other.FormsTexts.Add(item)
                Next
                Return other
            End Function
            Public Sub New()

            End Sub
        End Class

        Public Property Params As New udtParams

        Public Sub New()

        End Sub
        Public Sub New(ByVal ConfigFileName As String, RestoreFolder As String)
            FileNameXml = ConfigFileName
            Params = New udtParams
            ser = New Serialization(ConfigFileName, Params, RestoreFolder)
            ser.IgnoreEnabled = True
        End Sub
        Public Function ReadDataFromFile() As String

            Dim ret As String = String.Empty
            Try
                Params = ser.Deserialize()
                ret = ser.Result
            Catch ex As Exception
                ret = ex.Message
            Finally
            End Try
            Return ret

        End Function
        Public Sub WriteDataToFile()
            ser.Serialize()
        End Sub

        Public Overloads Function DeepCopy() As FormsTextsDefs
            Dim other As FormsTextsDefs = DirectCast(Me.MemberwiseClone(), FormsTextsDefs)
            other.Params = New udtParams
            other.Params = Params.DeepCopy
            Return other
        End Function

    End Class


    Private Texts As New List(Of udtText)

    Private Class udtTag
        Public Property Name As String
        Public Property Eng As String
        Public Property Ita As String
        Public Property Loc As String
        Public Sub New(Name As String, Eng As String, Ita As String, Loc As String)
            Me.Name = Name
            Me.Eng = Eng
            Me.Ita = Ita
            Me.Loc = Loc
        End Sub
    End Class

    Public Property LanguageToUse As Languages = Languages.Eng
    Public Property FormsTexts As New FormsTextsDefs

    Public Sub New()
        Me.LanguageToUse = Languages.Eng
    End Sub
    Public Sub New(ByVal LanguageToUse As Languages)
        Me.LanguageToUse = LanguageToUse
    End Sub


    Public Overloads Function DeepCopy() As TextsManagement
        Dim other As TextsManagement = DirectCast(Me.MemberwiseClone(), TextsManagement)
        other.FormsTexts = New FormsTextsDefs
        other.FormsTexts = FormsTexts.DeepCopy

        Return other
    End Function


    Public Function ReadTexts(ByVal TextsFileName As String, RestoreFolder As String) As Boolean
        FormsTexts = New FormsTextsDefs(TextsFileName, RestoreFolder)
        If FormsTexts.ReadDataFromFile() <> "" Then Return False

        Return True
    End Function

    Public Function ReadFormTexts(FormName As String) As Boolean

        For Each item As TextsManagement.udtFormTexts In FormsTexts.Params.FormsTexts
            If item.FormName = FormName Then
                Me.Texts = New List(Of udtText)
                For Each txt As udtText In item.Texts
                    Me.Texts.Add(txt)
                Next
                Return True
            End If
        Next
        Return False
    End Function

    Public Function ReadTexts(Texts As List(Of udtText)) As Boolean
        Me.Texts = New List(Of udtText)
        For Each item As udtText In Texts
            Me.Texts.Add(item)
        Next
        Return True
    End Function

    Public Function GetText(ByVal sTextName As String, DefaultText As String) As String

        Return GetText(sTextName, Texts, DefaultText)

    End Function

    Public Function GetFormText(FormName As String, ByVal sTextName As String) As String

        For Each item As TextsManagement.udtFormTexts In FormsTexts.Params.FormsTexts
            If item.FormName = FormName Then
                Return GetText(sTextName, item.Texts, ".")
            End If
        Next

        Return sTextName

    End Function

    Private Function GetFormTexts(FormName As String) As List(Of udtText)

        For Each item As TextsManagement.udtFormTexts In FormsTexts.Params.FormsTexts
            If item.FormName = FormName Then
                Return item.Texts
            End If
        Next
        Return Nothing
    End Function

    Private Function GetText(ByVal sTextName As String, TextList As List(Of udtText), DefaultText As String) As String
        Dim str As String = DefaultText

        If DefaultText = "." Then str = sTextName

        For Each item As udtText In TextList
            If item.Name = sTextName Then
                Select Case LanguageToUse
                    Case Languages.Ita
                        str = item.MultiLang.Ita
                    Case Languages.Eng
                        str = item.MultiLang.Eng
                    Case Languages.Local
                        str = item.MultiLang.Loc
                    Case Else
                        str = item.MultiLang.Eng
                End Select
            End If
        Next


        str = str.Replace("#CRLF#", vbCrLf)

        Return str
    End Function


End Class
