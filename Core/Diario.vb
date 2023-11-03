Imports Core




Public Class Diario

    Private ser As Serialization

    Private FileName As String

#Region "DataTypes"

    Public Enum enummTipoTrasferta
        Italia
        ItaliaHotel
        Estero
        EsteroLunga
    End Enum

#Region "OreLavoro"
    Public Class udtOreLavoro
        Implements IComparable(Of udtOreLavoro)

        Public Property Data As Date
        Public Property OrderName As String
        Public Property Activity As String
        Public Property Sector As String
        Public Property WorkingHours As String
        Public Property JourneyHours As String

        Private Function CompareTo(comparePart As udtOreLavoro) As Integer Implements IComparable(Of udtOreLavoro).CompareTo
            If comparePart Is Nothing Then
                Return 1
            Else
                Return Me.Data.CompareTo(comparePart.Data)
            End If
        End Function
    End Class

    Public Class udtDgvOreLavoro
        Public Property Giorno As Integer
        Public Property Commessa As String
        Public Property Attività As String
        Public Property Settore As String
        Public Property OreLavoro As String
        Public Property OreViaggio As String
    End Class

    Public Class udtFilterOreLavoro
        Public Property EmployeeName As String = "%"
        Public Property OrderName As String = "%"
        Public Property Activity As String = "%"
        Public Property Sector As String = "%"
        Public Property BeginDate As Date = Date.Now
        Public Property EndDate As Date = Date.Now
        Public Overloads Function DeepCopy() As udtFilterOreLavoro
            Dim other As udtFilterOreLavoro = DirectCast(Me.MemberwiseClone(), udtFilterOreLavoro)
            Return other
        End Function
    End Class
#End Region

#Region "Spese"

    Public Class udtExpense
        Implements IComparable(Of udtExpense)

        Public Property Data As Date
        Public Property OrderName As String
        Public Property City As String = ""
        Public Property Km As Integer = 0
        Public Property Autostrada As Single = 0
        Public Property MezziPubblici As Single = 0
        Public Property Vitto As Single = 0
        Public Property Alloggio As Single = 0
        Public Property Varie As Single = 0
        Public Property CCA As Single = 0
        Public Property Valuta As Single = 0
        'Public Property Trasferta As enummTipoTrasferta = enummTipoTrasferta.Italia
        Public Property TipoRimborso As String = ""
        Public Property Motivo As String = ""
        Public Property Diaria As Single = 0
        Public Function CompareTo(comparePart As udtExpense) As Integer Implements IComparable(Of udtExpense).CompareTo
            If comparePart Is Nothing Then
                Return 1
            Else
                Return Me.Data.CompareTo(comparePart.Data)
            End If
        End Function
    End Class

    Public Class udtDgvExpenses
        Public Property Giorno As Integer
        Public Property Commessa As String
        Public Property Località As String
        Public Property Km As String
        Public Property Autostrada As String
        Public Property Mezzi As String
        Public Property Vitto As String
        Public Property Alloggio As String
        Public Property Varie As String
        Public Property Carta As String
        Public Property Valuta As String
        Public Property Trasferta As String
    End Class

    Public Class udtFilterExpenses
        Public Property EmployeeName As String = "%"
        Public Property OrderName As String = "%"
        Public Property BeginDate As Date = Date.Now
        Public Property EndDate As Date = Date.Now
        Public Overloads Function DeepCopy() As udtFilterExpenses
            Dim other As udtFilterExpenses = DirectCast(Me.MemberwiseClone(), udtFilterExpenses)
            Return other
        End Function
    End Class

    'Public Class udtRecordExpenses
    '    Public Property Data As Date = Date.Now
    '    Public Property OrderName As String = ""
    '    Public Property City As String = ""
    '    Public Property Km As String = ""
    '    Public Property Autostrada As Single = 0
    '    Public Property MezziPubblici As Single = 0
    '    Public Property Vitto As Single = 0
    '    Public Property Alloggio As Single = 0
    '    Public Property Varie As Single = 0
    '    Public Property CartaCredito As Single = 0
    '    Public Property Valuta As Single = 0
    '    Public Property Trasferta As enummTipoTrasferta
    'End Class

#End Region


    'Public Class Part
    '    'Inherits IEquatable(Of Part)
    '    Implements IComparable(Of Part)

    '    Public Property PartName As String
    '    Public Property PartId As Integer

    '    Public Overrides Function ToString() As String
    '        Return "ID: " & PartId & "   Name: " & PartName
    '    End Function

    '    'Public Overrides Function Equals(ByVal obj As Object) As Boolean
    '    '    If obj Is Nothing Then Return False
    '    '    Dim objAsPart As Part = TryCast(obj, Part)

    '    '    If objAsPart Is Nothing Then
    '    '        Return False
    '    '    Else
    '    '        Return Equals(objAsPart)
    '    '    End If
    '    'End Function

    '    Public Function SortByNameAscending(ByVal name1 As String, ByVal name2 As String) As Integer
    '        Return name1.CompareTo(name2)
    '    End Function

    '    Public Function CompareTo(ByVal comparePart As Part) As Integer
    '        If comparePart Is Nothing Then
    '            Return 1
    '        Else
    '            Return Me.PartId.CompareTo(comparePart.PartId)
    '        End If
    '    End Function


    '    Public Overrides Function GetHashCode() As Integer
    '        Return PartId
    '    End Function

    '    'Public Function Equals(ByVal other As Part) As Boolean
    '    '    If other Is Nothing Then Return False
    '    '    Return (Me.PartId.Equals(other.PartId))
    '    'End Function
    'End Class


#End Region

    Public Class udtDiario

        Public Property OreLavoro As New List(Of udtOreLavoro)
        Public Property Spese As New List(Of udtExpense)

        Public Sub New()
        End Sub

    End Class

#Region "Properties"

    Public Property Diario As udtDiario
    Public Property NomeFileDiario As String

    Public Property Commesse As New List(Of String)
    Public Property WorkedHoursMonthly As New List(Of udtDgvOreLavoro)
    Public Property ExpensesMonthly As New List(Of udtDgvExpenses)
#End Region

#Region "Costructors"
    'Public Sub New()

    'End Sub
    Public Sub New(ByVal EmployeeName As String)
        FileName = xGlobals.DataPath & "Diario" & EmployeeName.Replace(" ", "") & ".xml"
        Diario = New udtDiario
        ser = New Serialization(FileName, Diario)
    End Sub

    Public Sub New(ByVal EmployeeName As String, import As Boolean)
        FileName = xGlobals.DataPath & "Import\" & "Diario" & EmployeeName.Replace(" ", "") & ".xml"
        Diario = New udtDiario
        ser = New Serialization(FileName, Diario)
    End Sub

#End Region

#Region "Methods"

    Public Sub ReadDataFromFile()
        Diario = ser.Deserialize()
        'If Diario.OreLavoro.Count = 0 Then
        '    Diario.OreLavoro = New List(Of udtOreLavoro)
        '    Dim ol As New udtOreLavoro
        '    ol.Data = Format(Date.Now, "yyyy-MM-dd")
        '    ol.OrderName = "OrderName"
        '    ol.Activity = "Activity"
        '    ol.Sector = "Sector"
        '    ol.WorkingHours = "8:00"
        '    ol.JourneyHours = "2:00"
        '    Diario.OreLavoro.Add(ol)
        'End If
        'If Diario.Spese.Count = 0 Then
        '    Diario.Spese = New List(Of udtExpense)
        '    Dim sp As New udtExpense
        '    sp.Data = Format(Date.Now, "yyyy-MM-dd")
        '    sp.OrderName = "OrderName"
        '    sp.City = "Tortona"
        '    sp.Km = 10
        '    sp.Autostrada = 0
        '    sp.Vitto = 10.5
        '    sp.Alloggio = 0
        '    sp.Varie = 0
        '    sp.CCA = 0
        '    sp.Valuta = 0
        '    Diario.Spese.Add(sp)
        'End If

        For Each item As udtOreLavoro In Diario.OreLavoro
            If Commesse.Contains(item.OrderName) Then
            Else
                Commesse.Add(item.OrderName)
            End If
        Next

    End Sub

    Public Sub WriteDataToFile()
        ser.Serialize()
    End Sub

    Public Function GetOrderList(Filter As String) As List(Of String)
        Dim orders As New List(Of String)

        For Each str As String In Commesse
            If Filter = "%" Or Filter = "" Then
                orders.Add(str)
            Else
                If str.Contains(Filter.ToUpper) Then
                    orders.Add(str)
                End If
            End If
        Next

        Return orders
    End Function

    Public Function GetWorkedHoursMonthly(Filter As udtFilterOreLavoro) As List(Of udtDgvOreLavoro)
        WorkedHoursMonthly.Clear()

        For Each item As udtOreLavoro In Diario.OreLavoro
            If (item.Data >= Filter.BeginDate) And (item.Data <= Filter.EndDate) Then
                Dim a As New udtDgvOreLavoro
                a.Giorno = item.Data.Day
                a.Commessa = item.OrderName
                a.Attività = item.Activity
                a.Settore = item.Sector
                a.OreLavoro = item.WorkingHours
                a.OreViaggio = item.JourneyHours
                WorkedHoursMonthly.Add(a)
            End If
        Next

        Return WorkedHoursMonthly
    End Function
    Public Function RecordOreLavoroExists(rec As udtOreLavoro) As Boolean
        For Each item As udtOreLavoro In Diario.OreLavoro
            If (item.Data.ToShortDateString = rec.Data.ToShortDateString) And (item.OrderName = rec.OrderName) And (item.Activity = rec.Activity) And (item.Sector = rec.Sector) Then
                Return True
            End If
        Next
        Return False
    End Function
    Public Function UpdateOreLavoro(rec As udtOreLavoro) As Boolean
        For Each item As udtOreLavoro In Diario.OreLavoro
            If (item.Data.ToShortDateString = rec.Data.ToShortDateString) And (item.OrderName = rec.OrderName) And (item.Activity = rec.Activity) And (item.Sector = rec.Sector) Then

                Dim ts As TimeSpan = ResPublic.TextToTimeSpan(rec.WorkingHours)
                item.WorkingHours = ts.Hours.ToString & ":" & ts.Minutes.ToString
                ts = ResPublic.TextToTimeSpan(rec.JourneyHours)
                item.JourneyHours = ts.Hours.ToString & ":" & ts.Minutes.ToString
                WriteDataToFile()
                Return True
            End If
        Next
        Return False
    End Function
    Public Function AddOreLavoro(rec As udtOreLavoro) As Boolean
        Dim item As New udtOreLavoro
        item.Data = rec.Data
        item.OrderName = rec.OrderName
        item.Activity = rec.Activity
        item.Sector = rec.Sector
        Dim ts As TimeSpan = ResPublic.TextToTimeSpan(rec.WorkingHours)
        item.WorkingHours = ts.Hours.ToString & ":" & ts.Minutes.ToString
        ts = ResPublic.TextToTimeSpan(rec.JourneyHours)
        item.JourneyHours = ts.Hours.ToString & ":" & ts.Minutes.ToString

        Diario.OreLavoro.Add(item)
        Diario.OreLavoro.Sort()

        WriteDataToFile()

        If Commesse.Contains(item.OrderName) Then
        Else
            Commesse.Add(item.OrderName)
        End If

        Return True
    End Function
    Public Function DeleteOreLavoro(rec As udtOreLavoro) As Boolean

        For Each item As udtOreLavoro In Diario.OreLavoro
            If (item.Data.ToShortDateString = rec.Data.ToShortDateString) And (item.OrderName = rec.OrderName) And (item.Activity = rec.Activity) And (item.Sector = rec.Sector) Then
                Diario.OreLavoro.Remove(item)
                WriteDataToFile()
                Return True
            End If
        Next

        Return True
    End Function

    Public Function GetExpensesMonthly(Filter As udtFilterExpenses) As List(Of udtDgvExpenses)
        ExpensesMonthly.Clear()

        Dim recExist As Boolean = False

        If Diario.Spese.Count > 0 Then
            For Each item As udtExpense In Diario.Spese
                If (item.Data >= Filter.BeginDate) And (item.Data <= Filter.EndDate) Then
                    Dim a As New udtDgvExpenses
                    a.Giorno = item.Data.Day
                    a.Commessa = item.OrderName
                    a.Località = item.City
                    a.Km = item.Km.ToString
                    a.Autostrada = Format(item.Autostrada, "#0.00")
                    a.Mezzi = Format(item.MezziPubblici, "#0.00")
                    a.Vitto = Format(item.Vitto, "#0.00")
                    a.Alloggio = Format(item.Alloggio, "#0.00")
                    a.Varie = Format(item.Varie, "#0.00")
                    a.Carta = Format(item.CCA, "#0.00")
                    a.Valuta = Format(item.Valuta, "#0.00")

                    If item.Diaria = 46 Then
                        a.Trasferta = Core.Diario.enummTipoTrasferta.Italia.ToString
                    End If
                    If item.Diaria = 77 Then
                        a.Trasferta = Core.Diario.enummTipoTrasferta.Estero.ToString
                    End If



                    ExpensesMonthly.Add(a)
                    End If
            Next
        End If

        If Diario.OreLavoro.Count > 0 Then
            For Each item As udtOreLavoro In Diario.OreLavoro
                If item.JourneyHours <> "0:0" Then
                    If (item.Data >= Filter.BeginDate) And (item.Data <= Filter.EndDate) Then
                        Dim a As New udtDgvExpenses
                        a.Giorno = item.Data.Day
                        a.Commessa = item.OrderName
                        a.Località = ""
                        a.Km = "0"
                        a.Autostrada = Format(0, "#0.00")
                        a.Mezzi = Format(0, "#0.00")
                        a.Vitto = Format(0, "#0.00")
                        a.Alloggio = Format(0, "#0.00")
                        a.Varie = Format(0, "#0.00")
                        a.Carta = Format(0, "#0.00")
                        a.Valuta = Format(0, "#0.00")
                        a.Trasferta = ""

                        recExist = RecordListExpensesExists(a)

                        If Not recExist Then
                            ExpensesMonthly.Add(a)
                        End If
                    End If


                End If
            Next

        End If

        Return ExpensesMonthly
    End Function
    Public Function RecordExpensesExists(rec As udtExpense) As Boolean
        For Each item As udtExpense In Diario.Spese
            If (item.Data.ToShortDateString = rec.Data.ToShortDateString) And (item.OrderName = rec.OrderName) Then
                Return True
            End If
        Next
        Return False
    End Function

    Public Function RecordListExpensesExists(rec As udtDgvExpenses) As Boolean

        If ExpensesMonthly.Count = 0 Then
            Return False
        End If
        For Each item As udtDgvExpenses In ExpensesMonthly
            If (item.Giorno = rec.Giorno) And (item.Commessa = rec.Commessa) Then
                Return True
            End If
        Next
        Return False
    End Function
    Public Function UpdateExpenses(rec As udtExpense) As Boolean
        For Each item As udtExpense In Diario.Spese
            If (item.Data.ToShortDateString = rec.Data.ToShortDateString) And (item.OrderName = rec.OrderName) Then
                item.City = rec.City
                item.Data = rec.Data
                item.Alloggio = rec.Alloggio
                item.Autostrada = rec.Autostrada
                item.CCA = rec.CCA
                item.Km = rec.Km
                item.MezziPubblici = rec.MezziPubblici
                item.Valuta = rec.Valuta
                item.Varie = rec.Varie
                item.Vitto = rec.Vitto
                item.TipoRimborso = rec.TipoRimborso
                item.Motivo = rec.Motivo
                item.Diaria = rec.Diaria
                WriteDataToFile()
                Return True
            End If
        Next
        Return False
    End Function
    Public Function AddExpenses(rec As udtExpense) As Boolean
        Dim item As New udtExpense
        item.Data = rec.Data
        item.OrderName = rec.OrderName
        item.City = rec.City
        item.Alloggio = rec.Alloggio
        item.Autostrada = rec.Autostrada
        item.CCA = rec.CCA
        item.Km = rec.Km
        item.MezziPubblici = rec.MezziPubblici
        item.Valuta = rec.Valuta
        item.Varie = rec.Varie
        item.Vitto = rec.Vitto
        'item.Trasferta = rec.Trasferta
        item.TipoRimborso = rec.TipoRimborso
        item.Motivo = rec.Motivo
        item.Diaria = rec.Diaria

        Diario.Spese.Add(item)
        Diario.Spese.Sort()

        WriteDataToFile()

        Return True
    End Function
    Public Function DeleteExpenses(rec As udtExpense) As Boolean

        For Each item As udtExpense In Diario.Spese
            If (item.Data.ToShortDateString = rec.Data.ToShortDateString) And (item.OrderName = rec.OrderName) Then
                Diario.Spese.Remove(item)
                WriteDataToFile()
                Return True
            End If
        Next

        Return True
    End Function

    'Public Function OreLavoroMese(dtpDate As Date) As List(Of udtOreLavoro)
    '    Dim params As New List(Of udtOreLavoro)

    '    Dim DaysInMonth As Integer = Date.DaysInMonth(dtpDate.Year, dtpDate.Month)
    '    Dim EndDate As Date = New Date(dtpDate.Year, dtpDate.Month, DaysInMonth)
    '    Dim BeginDate As Date = New Date(dtpDate.Year, dtpDate.Month, 1)

    '    For Each ore As udtOreLavoro In Diario.OreLavoro
    '        If (ore.Data >= BeginDate) And (ore.Data <= EndDate) Then
    '            Dim OreLav As New Diario.udtOreLavoro
    '            OreLav.Data = ore.Data
    '            OreLav.OrderName = ore.OrderName
    '            OreLav.Activity = ore.Activity
    '            OreLav.Sector = ore.Sector
    '            OreLav.WorkingHours = ore.WorkingHours
    '            OreLav.JourneyHours = ore.JourneyHours
    '            params.Add(OreLav)
    '        End If

    '    Next

    '    Return params
    'End Function

    'Public Function SpeseMese(dtpDate As Date) As List(Of udtExpense)
    '    Dim params As New List(Of udtExpense)

    '    Dim DaysInMonth As Integer = Date.DaysInMonth(dtpDate.Year, dtpDate.Month)
    '    Dim EndDate As Date = New Date(dtpDate.Year, dtpDate.Month, DaysInMonth)
    '    Dim BeginDate As Date = New Date(dtpDate.Year, dtpDate.Month, 1)

    '    For Each spese As udtExpense In Diario.Spese
    '        If (spese.Data >= BeginDate) And (spese.Data <= EndDate) Then
    '            Dim sp As New Diario.udtExpense
    '            sp.Data = spese.Data
    '            sp.OrderName = spese.OrderName
    '            sp.City = spese.City
    '            sp.Km = spese.Km
    '            sp.Autostrada = spese.Autostrada
    '            sp.Vitto = spese.Vitto
    '            sp.Alloggio = spese.Alloggio
    '            sp.Varie = spese.Varie
    '            sp.CCA = spese.CCA
    '            sp.Valuta = spese.Valuta

    '            params.Add(sp)
    '        End If
    '    Next

    '    Return params
    'End Function


#End Region


End Class
