Imports System
Imports System.Drawing
Imports System.Globalization
Imports System.Windows.Forms

Public Class CheckInputData

    Private culture As CultureInfo

    Public Enum enumNokResult
        ValoreErrato
        ValoreDeveEsistere
        ValoreDeveEssereNumerico
        ValoreTroppoGrosso
        ValoreTroppoPiccolo
        MinoreMaggioreDelMassimo
        FormatoOraNonValido
        CommessaNonValida
    End Enum

    Public Property NokResult As enumNokResult

    Public Sub New()

        culture = CultureInfo.CurrentCulture

    End Sub


#Region "Methods"

    Public Function OnlyNumber(ByVal TextBox As TextBox, ByVal TastoPremuto As Char, ByVal Segno As Char) As Char
        'Segno: "+" accetta solo numeri positivi
        '       "-" accetta solo numeri negativi
        '       "*" accetta tutti i numeri

        Const CharOk = "0123456789+-.,"
        Dim iPos As Integer = InStr(CharOk, TastoPremuto)
        If iPos = 0 Then
            If TastoPremuto = vbBack Then
                Return TastoPremuto
            Else
                Return ""
            End If
        Else
            'Cambio segno decimale in funzione SO
            If TastoPremuto = "." Or TastoPremuto = "," Then
                If TastoPremuto <> culture.NumberFormat.NumberDecimalSeparator Then
                    TastoPremuto = culture.NumberFormat.NumberDecimalSeparator
                End If
                If InStr(TextBox.Text, TastoPremuto) <> 0 Then Return ""
            End If
            'Acceta segno solo come primo carattere
            If TastoPremuto = "-" And TextBox.Text.Length <> 0 And TextBox.SelectionLength = 0 Then Return ""
            If TastoPremuto = "+" And TextBox.Text.Length <> 0 And TextBox.SelectionLength = 0 Then Return ""

            'Accetta solo segno concorde a quello richiesto
            If TastoPremuto = "-" And Segno = "+" Then Return ""
            If TastoPremuto = "+" And Segno = "-" Then Return ""

            Return TastoPremuto
        End If

    End Function

    Public Function OnlyInteger(ByVal TextBox As TextBox, ByVal TastoPremuto As Char, ByVal Segno As Char) As Char
        'Segno: "+" accetta solo numeri positivi
        '       "-" accetta solo numeri negativi
        '       "*" accetta tutti i numeri

        Const CharOk = "0123456789+-"
        Dim iPos As Integer = InStr(CharOk, TastoPremuto)
        If iPos = 0 Then
            If TastoPremuto = vbBack Then
                Return TastoPremuto
            Else
                Return ""
            End If
        Else
            'Acceta segno solo come primo carattere
            If TastoPremuto = "-" And TextBox.Text.Length <> 0 And TextBox.SelectionLength = 0 Then Return ""
            If TastoPremuto = "+" And TextBox.Text.Length <> 0 And TextBox.SelectionLength = 0 Then Return ""
            'Accetta solo segno concorde a quello richiesto
            If TastoPremuto = "-" And Segno = "+" Then Return ""
            If TastoPremuto = "+" And Segno = "-" Then Return ""

            Return TastoPremuto
        End If

    End Function

    Public Function OnlyTime(ByVal TextBox As TextBox, ByVal TastoPremuto As Char) As Char
        'Segno: "+" accetta solo numeri positivi
        '       "-" accetta solo numeri negativi
        '       "*" accetta tutti i numeri

        Const CharsOk = "0123456789.:"
        If CharsOk.Contains(TastoPremuto) Then
            Return TastoPremuto
        End If
        If TastoPremuto = vbBack Then Return TastoPremuto
        'If TastoPremuto = ":" Then Return "."
        If TastoPremuto = "." Then Return ":"
        Return ""

    End Function

    Public Function AcceptOnlyMinusAsSpecial(ByVal ComboBox As ComboBox, ByVal TastoPremuto As Char) As Char
        'Accetta solo il trattino come carattere speciale

        Const CharsNOk = " .:_*\/?!()&%$£'^+,;<>|[]§#@°"

        If TastoPremuto = Chr(34) Then
            Return "-"
        End If

        If CharsNOk.Contains(TastoPremuto) Then
            Return "-"
        Else
            Return TastoPremuto
        End If



        Return ""

    End Function

    Public Function AcceptOnlyHex(ByVal TextBox As TextBox, ByVal KeyPressed As Char) As Char
        Const CharOk = "0123456789ABCDEFabcdef"
        Dim iPos As Integer = InStr(CharOk, KeyPressed)
        If iPos = 0 Then
            If KeyPressed = vbBack Then
                Return KeyPressed
            Else
                Return ""
            End If
        Else
            Return UCase(KeyPressed)
        End If

    End Function
    Public Function AcceptOnlyHexWithX(ByVal TextBox As TextBox, ByVal KeyPressed As Char) As Char
        Const CharOk = "0123456789ABCDEFabcdefXx"
        Dim iPos As Integer = InStr(CharOk, KeyPressed)
        If iPos = 0 Then
            If KeyPressed = vbBack Then
                Return KeyPressed
            Else
                Return ""
            End If
        Else
            Return UCase(KeyPressed)
        End If

    End Function

    Public Function IsHex(ByVal value As String) As Boolean
        Dim i As Integer

        If Len(value) = 0 Then
            Return False
        End If

        For i = 1 To Len(value)
            If InStr("0123456789ABCDEF", UCase$(Mid$(value, i, 1))) = 0 Then
                Return False
            End If
        Next
        Return True

    End Function
    Public Function IsHexWithX(ByVal value As String) As Boolean
        Dim i As Integer

        If Len(value) = 0 Then
            Return False
        End If

        For i = 1 To Len(value)
            If InStr("0123456789ABCDEFXabcdefx", UCase$(Mid$(value, i, 1))) = 0 Then
                Return False
            End If
        Next
        Return True

    End Function

    Public Function IsValueExisting(ByVal txtBox As TextBox) As Boolean
        If Len(txtBox.Text) = 0 Then
            ShowMsg(enumNokResult.ValoreDeveEsistere)
            txtBox.Focus()
            txtBox.BackColor = Color.Coral
            Return False
        End If
        Return True
    End Function
    Public Function IsValueExisting(ByVal ComboBox As ComboBox) As Boolean
        If Len(ComboBox.Text) = 0 Then
            ShowMsg(enumNokResult.ValoreDeveEsistere)
            ComboBox.Focus()
            ComboBox.BackColor = Color.Coral
            Return False
        End If
        Return True
    End Function
    Public Function IsValueExisting(ByVal txtBox As TextBox, ByVal tabDati As TabControl, ByVal iTab As Integer) As Boolean
        If Len(txtBox.Text) = 0 Then
            ShowMsg(enumNokResult.ValoreDeveEsistere)
            tabDati.SelectTab(iTab)
            txtBox.Focus()
            txtBox.BackColor = Color.Coral
            Return False
        End If
        Return True
    End Function
    Public Function IsValueExisting(ByVal ComboBox As ComboBox, ByVal tabDati As TabControl, ByVal iTab As Integer) As Boolean
        If Len(ComboBox.Text) = 0 Then
            ShowMsg(enumNokResult.ValoreDeveEsistere)
            tabDati.SelectTab(iTab)
            ComboBox.Focus()
            ComboBox.BackColor = Color.Coral
            Return False
        End If
        Return True
    End Function

    Public Function IsValueNumericOk(ByVal txtBox As TextBox, ByVal ValMin As Double, ByVal ValMax As Double) As Boolean

        If Not IsValueExisting(txtBox) Then Return False

        If Not IsNumeric(txtBox.Text) Then
            ShowMsg(enumNokResult.ValoreDeveEssereNumerico)
            txtBox.Focus()
            txtBox.BackColor = Color.Coral
            Return False
        End If

        'Valori minimi e massimi
        If TextToSingle(txtBox.Text) > ValMax Then
            ShowMsg(enumNokResult.ValoreTroppoGrosso)
            txtBox.Focus()
            txtBox.BackColor = Color.Coral
            Return False
        End If
        If TextToSingle(txtBox.Text) < ValMin Then
            ShowMsg(enumNokResult.ValoreTroppoPiccolo)
            txtBox.Focus()
            txtBox.BackColor = Color.Coral
            Return False
        End If

        Return True

    End Function
    Public Function IsValueNumericOk(ByRef txtBox As TextBox, ByVal ValMin As Double, ByVal ValMax As Double, ByVal tabDati As TabControl, ByVal iTab As Integer) As Boolean

        If Not IsValueExisting(txtBox, tabDati, iTab) Then Return False

        If Not IsNumeric(txtBox.Text) Then
            ShowMsg(enumNokResult.ValoreDeveEssereNumerico)
            tabDati.SelectTab(iTab)
            txtBox.Focus()
            txtBox.BackColor = Color.Coral
            Return False
        End If

        'Valori minimi e massimi
        If TextToSingle(txtBox.Text) > ValMax Then
            ShowMsg(enumNokResult.ValoreTroppoGrosso)
            tabDati.SelectTab(iTab)
            txtBox.Focus()
            txtBox.BackColor = Color.Coral
            Return False
        End If
        If TextToSingle(txtBox.Text) < ValMin Then
            ShowMsg(enumNokResult.ValoreTroppoPiccolo)
            tabDati.SelectTab(iTab)
            txtBox.Focus()
            txtBox.BackColor = Color.Coral
            Return False
        End If

        Return True

    End Function
    Public Function IsValueNumericOk(ByVal sValue As String, ByVal ValMin As Double, ByVal ValMax As Double) As Boolean
        Dim myTextBox As New TextBox
        myTextBox.Text = sValue

        If Not IsValueExisting(myTextBox) Then Return False

        If Not IsNumeric(sValue) Then
            ShowMsg(enumNokResult.ValoreDeveEssereNumerico)
            Return False
        End If

        'Valori minimi e massimi
        If TextToSingle(sValue) > ValMax Then
            ShowMsg(enumNokResult.ValoreTroppoGrosso)
            Return False
        End If
        If TextToSingle(sValue) < ValMin Then
            ShowMsg(enumNokResult.ValoreTroppoPiccolo)
            Return False
        End If

        Return True

    End Function

    Public Function IsValueNumericOk(ByVal txtBox As TextBox, ByVal ValMin As Double) As Boolean

        If Not IsValueExisting(txtBox) Then Return False

        If Not IsNumeric(txtBox.Text) Then
            ShowMsg(enumNokResult.ValoreDeveEssereNumerico)
            txtBox.Focus()
            txtBox.BackColor = Color.Coral
            Return False
        End If

        If TextToSingle(txtBox.Text) < ValMin Then
            ShowMsg(enumNokResult.ValoreTroppoPiccolo)
            txtBox.Focus()
            txtBox.BackColor = Color.Coral
            Return False
        End If

        Return True

    End Function
    Public Function IsValueNumericOk(ByRef txtBox As TextBox, ByVal ValMin As Double, ByVal tabDati As TabControl, ByVal iTab As Integer) As Boolean

        If Not IsValueExisting(txtBox, tabDati, iTab) Then Return False

        If Not IsNumeric(txtBox.Text) Then
            ShowMsg(enumNokResult.ValoreDeveEssereNumerico)
            tabDati.SelectTab(iTab)
            txtBox.Focus()
            txtBox.BackColor = Color.Coral
            Return False
        End If

        If TextToSingle(txtBox.Text) < ValMin Then
            ShowMsg(enumNokResult.ValoreTroppoPiccolo)
            tabDati.SelectTab(iTab)
            txtBox.Focus()
            txtBox.BackColor = Color.Coral
            Return False
        End If

        Return True

    End Function
    Public Function IsValueNumericOk(ByVal sValue As String, ByVal ValMin As Double) As Boolean
        Dim myTextBox As New TextBox
        myTextBox.Text = sValue

        If Not IsValueExisting(myTextBox) Then Return False

        If Not IsNumeric(sValue) Then
            ShowMsg(enumNokResult.ValoreDeveEssereNumerico)
            Return False
        End If

        If TextToSingle(sValue) < ValMin Then
            ShowMsg(enumNokResult.ValoreTroppoPiccolo)
            Return False
        End If

        Return True

    End Function

    Public Function IsValueHexOk(ByVal txtBox As TextBox, ByVal ValMin As Double, ByVal ValMax As Double) As Boolean

        If Not IsValueExisting(txtBox) Then Return False

        If Not IsHex(txtBox.Text) Then
            ShowMsg(enumNokResult.ValoreDeveEssereNumerico)
            txtBox.Focus()
            txtBox.BackColor = Color.Coral
            Return False
        End If

        'Valori minimi e massimi
        If TextToSingle(txtBox.Text) > ValMax Then
            ShowMsg(enumNokResult.ValoreTroppoGrosso)
            txtBox.Focus()
            txtBox.BackColor = Color.Coral
            Return False
        End If
        If TextToSingle(txtBox.Text) < ValMin Then
            ShowMsg(enumNokResult.ValoreTroppoPiccolo)
            txtBox.Focus()
            txtBox.BackColor = Color.Coral
            Return False
        End If

        Return True

    End Function

    Public Function IsValueHexWithXOk(ByVal txtBox As TextBox, ByVal ValMin As Double, ByVal ValMax As Double) As Boolean

        If Not IsValueExisting(txtBox) Then Return False

        If Not IsHexWithX(txtBox.Text) Then
            ShowMsg(enumNokResult.ValoreDeveEssereNumerico)
            txtBox.Focus()
            txtBox.BackColor = Color.Coral
            Return False
        End If

        Dim sVal As String

        'Valori minimi e massimi
        sVal = txtBox.Text
        sVal = sVal.Replace("X", "F")
        If TextToSingle(sVal) > ValMax Then
            ShowMsg(enumNokResult.ValoreTroppoGrosso)
            txtBox.Focus()
            txtBox.BackColor = Color.Coral
            Return False
        End If
        sVal = sVal.Replace("X", "0")
        If TextToSingle(sVal) < ValMin Then
            ShowMsg(enumNokResult.ValoreTroppoPiccolo)
            txtBox.Focus()
            txtBox.BackColor = Color.Coral
            Return False
        End If

        Return True

    End Function
    Public Function IsValueHexOk(ByVal sValue As String, ByVal ValMin As Double, ByVal ValMax As Double) As Boolean
        Dim myTextBox As New TextBox
        myTextBox.Text = sValue

        If Not IsValueExisting(myTextBox) Then Return False

        If Not IsHex(sValue) Then
            ShowMsg(enumNokResult.ValoreDeveEssereNumerico)
            Return False
        End If

        'Valori minimi e massimi
        If TextToSingle(sValue) > ValMax Then
            ShowMsg(enumNokResult.ValoreTroppoGrosso)
            Return False
        End If
        If TextToSingle(sValue) < ValMin Then
            ShowMsg(enumNokResult.ValoreTroppoPiccolo)
            Return False
        End If

        Return True

    End Function

    Public Function IsMinMaxOk(ByVal txtBoxMin As TextBox, ByVal txtBoxMax As TextBox, ByVal ValMin As Double, ByVal ValMax As Double) As Boolean

        If Not IsValueNumericOk(txtBoxMin, ValMin, ValMax) Then Return False
        If Not IsValueNumericOk(txtBoxMax, ValMin, ValMax) Then Return False

        If TextToSingle(txtBoxMin.Text) > TextToSingle(txtBoxMax.Text) Then
            ShowMsg(enumNokResult.MinoreMaggioreDelMassimo)
            txtBoxMin.Focus()
            txtBoxMin.BackColor = Color.Coral
            txtBoxMax.BackColor = Color.Coral
            Return False
        End If

        Return True

    End Function
    Public Function IsMinMaxOk(ByRef txtBoxMin As TextBox, ByRef txtBoxMax As TextBox, ByVal ValMin As Double, ByVal ValMax As Double,
                               ByVal tabDati As TabControl, ByVal iTab As Integer) As Boolean

        If Not IsValueNumericOk(txtBoxMin, ValMin, ValMax, tabDati, iTab) Then Return False
        If Not IsValueNumericOk(txtBoxMax, ValMin, ValMax, tabDati, iTab) Then Return False

        If TextToSingle(txtBoxMin.Text) > TextToSingle(txtBoxMax.Text) Then
            ShowMsg(enumNokResult.MinoreMaggioreDelMassimo)
            tabDati.SelectTab(iTab)
            txtBoxMin.Focus()
            txtBoxMin.BackColor = Color.Coral
            txtBoxMax.BackColor = Color.Coral
            Return False
        End If

        Return True

    End Function


    Public Function IsTime(ByVal txtBox As TextBox) As Boolean
        If txtBox.Text = "" Then
            txtBox.Focus()
            txtBox.BackColor = Color.Coral
            ShowMsg(enumNokResult.FormatoOraNonValido)
            Return False
        Else
            ' Try
            Dim str As String = txtBox.Text
            str = str.Replace(".", ":")
            Dim aryStr As String() = str.Split(":")
            If aryStr.Length = 1 Then
                If CInt(str) <= 24 Then Return True
            ElseIf aryStr.Length = 2 Then
                If CInt(aryStr(0)) < 24 And CInt(aryStr(1)) <= 59 Then Return True
                If CInt(aryStr(0)) = 24 And CInt(aryStr(1)) = 0 Then Return True
            Else

            End If
            txtBox.Focus()
            txtBox.BackColor = Color.Coral
            ShowMsg(enumNokResult.FormatoOraNonValido)
            Return False

        End If
    End Function

    Public Function IsPathExisting(ByVal txtBox As TextBox) As Boolean
        If Not IsValueExisting(txtBox) Then Return False

        'If Not My.Computer.FileSystem.DirectoryExists(txtBox.Text) Then
        '    Dim result1 As DialogResult = MessageBox.Show(myTexts.GetText("PathDoesntExistCreate", ""), "", MessageBoxButtons.YesNo)
        '    If result1 = DialogResult.Yes Then
        '        Try
        '            My.Computer.FileSystem.CreateDirectory(txtBox.Text)
        '            Return True
        '        Catch ex As Exception
        '            MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        End Try
        '        txtBox.Focus()
        '        txtBox.BackColor = Color.Coral
        '        Return False
        '    Else
        '        txtBox.Focus()
        '        txtBox.BackColor = Color.Coral
        '        Return False
        '    End If
        'End If
        Return True
    End Function
    Public Function IsPathExisting(ByVal txtBox As TextBox, ByVal tabDati As TabControl, ByVal iTab As Integer) As Boolean
        If Not IsValueExisting(txtBox) Then
            tabDati.SelectTab(iTab)
            txtBox.Focus()
            txtBox.BackColor = Color.Coral
            Return False
        End If
        'If Not My.Computer.FileSystem.DirectoryExists(txtBox.Text) Then
        '    Dim result1 As DialogResult = MessageBox.Show(myTexts.GetText("PathDoesntExistCreate", ""), "", MessageBoxButtons.YesNo)
        '    If result1 = DialogResult.Yes Then
        '        Try
        '            My.Computer.FileSystem.CreateDirectory(txtBox.Text)
        '            Return True
        '        Catch ex As Exception
        '            MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        End Try
        '        tabDati.SelectTab(iTab)
        '        txtBox.Focus()
        '        txtBox.BackColor = Color.Coral
        '        Return False
        '    Else
        '        tabDati.SelectTab(iTab)
        '        txtBox.Focus()
        '        txtBox.BackColor = Color.Coral
        '        Return False
        '    End If
        'End If
        Return True
    End Function


    Public Sub SetDefaultBackColor(ByVal objCollection As Control.ControlCollection)

        For Each obj As Control In objCollection
            If TypeOf (obj) Is TextBox Then
                obj.BackColor = Color.White
            End If
        Next
    End Sub

    Private Sub ShowMsg(NokRes As enumNokResult)

        NokResult = NokRes

    End Sub


#End Region




End Class
