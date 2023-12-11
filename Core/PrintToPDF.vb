Imports iTextSharp.text
Imports System.IO
Imports iTextSharp.text.pdf
Imports iTextSharp.text.pdf.draw
Imports AcroPDFLib

Public Class PrintToPDF

    Private Doc As Document
    Private fileStream As FileStream
    Private Writer As PdfWriter

    'Declare a printerSettings
    Private PrinterToUseSetting As System.Drawing.Printing.PrinterSettings = Nothing


    Private Class TableColumnsDefinition

        Private _dictionary


        Public Sub New(ColumnsName() As String)
            _dictionary = New Dictionary(Of String, Integer)
            For i As Integer = 0 To ColumnsName.Length - 1
                _dictionary.add(ColumnsName(i), i)
            Next
        End Sub

        Public Function Column(ColumnName As String) As Integer
            ' Return value from private Dictionary.
            Return Me._dictionary.Item(ColumnName)
        End Function

    End Class
    Private tcdOreLavoro As TableColumnsDefinition
    Private tcdRefound As TableColumnsDefinition

    Public Sub New()

    End Sub


#Region "Methods"
    Public Sub PrintOreLavoro(FileName As String, Filter As Diario.udtFilterOreLavoro)

        If My.Computer.FileSystem.FileExists(FileName) Then
            My.Computer.FileSystem.DeleteFile(FileName)
        End If


        Doc = New Document(PageSize.A4, 50, 20, 15, 15)
        Try
            fileStream = New FileStream(FileName, FileMode.Create, FileAccess.Write, FileShare.None)
            Writer = PdfWriter.GetInstance(Doc, fileStream)
            Doc.Open()

            LogoOreLavoro()

            IntestazioneOreLavoro(Filter.EmployeeName, Filter.BeginDate.ToString("MMMM"), Filter.BeginDate.ToString("yyyy"))

            TabellaOreLavoro(Filter)



        Catch ex As Exception

        End Try

        Doc.Close()

    End Sub

    Public Sub PrintExpenses(FileName As String, Filter As Diario.udtFilterExpenses)

        If My.Computer.FileSystem.FileExists(FileName) Then
            My.Computer.FileSystem.DeleteFile(FileName)
        End If


        Doc = New Document(PageSize.A4.Rotate, 50, 20, 15, 15)
        Try
            fileStream = New FileStream(FileName, FileMode.Create, FileAccess.Write, FileShare.None)
            Writer = PdfWriter.GetInstance(Doc, fileStream)
            Doc.Open()

            LogoSpese()

            IntestazioneSpese(Filter.EmployeeName, Filter.BeginDate.ToString("MMMM"), Filter.BeginDate.ToString("yyyy"))

            TabellaSpese(Filter)


        Catch ex As Exception

        End Try

        Doc.Close()

    End Sub

#End Region

#Region "OreLavoro"
    Private Sub LogoOreLavoro()

        Try

            Dim table = New PdfPTable(2)
            table.DefaultCell.Border = Rectangle.NO_BORDER
            table.TotalWidth = 216.0F
            table.SetWidths(New Single() {1.0, 1.0})

            Dim Arial = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL)
            Dim FontCizeta = FontFactory.GetFont("Arial", 48, iTextSharp.text.Font.BOLD, New BaseColor(Drawing.Color.OrangeRed))

            'Dim p = New Paragraph(New Chunk(New iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, Color.BLACK, Element.ALIGN_LEFT, 1)))
            Dim LineSeparator = New LineSeparator(2.0F, 100.0F, New BaseColor(Drawing.Color.Black), Element.ALIGN_LEFT, 1)
            Doc.Add(LineSeparator)

            table.AddCell(GetCell("cizeta", 1, 4, FontCizeta, Element.ALIGN_CENTER))
            table.AddCell(GetCell("CIZETA AUTOMAZIONE S.R.L.", Arial, Element.ALIGN_LEFT))
            table.AddCell(GetCell("15057 TORTONA (AL)", Arial, Element.ALIGN_LEFT))
            table.AddCell(GetCell("Via S.FERRARI 20/5", Arial, Element.ALIGN_LEFT))
            table.AddCell(GetCell("p.i. 0096859 0067", Arial, Element.ALIGN_LEFT))
            Doc.Add(table)

            Doc.Add(New LineSeparator(2.0F, 100.0F, New BaseColor(Drawing.Color.Black), Element.ALIGN_LEFT, -5))
            ' Doc.Add(Chunk.NEWLINE)

        Catch ex As Exception

        End Try

    End Sub
    Private Sub IntestazioneOreLavoro(EmployeeName As String, Month As String, Year As String)

        Dim Arial = FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.NORMAL)
        Dim ArialSmall = FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.NORMAL)

        Try

            Dim table = New PdfPTable(7)
            table.DefaultCell.Border = Rectangle.NO_BORDER
            table.TotalWidth = 530
            table.LockedWidth = True
            table.SetWidths(New Single() {50, 180, 50, 100, 50, 40, 70})

            InsertEmptyRow(table, 6)
            table.AddCell(GetCell("Nome ", ArialSmall, Element.ALIGN_RIGHT))
            table.AddCell(GetCell(EmployeeName, ArialSmall, Element.ALIGN_CENTER, New BaseColor(Drawing.Color.PaleTurquoise)))
            table.AddCell(GetCell("Mese ", ArialSmall, Element.ALIGN_RIGHT))
            table.AddCell(GetCell(Month, ArialSmall, Element.ALIGN_CENTER, New BaseColor(Drawing.Color.PaleTurquoise)))
            table.AddCell(GetCell("Anno ", ArialSmall, Element.ALIGN_RIGHT))
            table.AddCell(GetCell(Year, ArialSmall, Element.ALIGN_CENTER, New BaseColor(Drawing.Color.PaleTurquoise)))
            table.AddCell(GetCell(" ", ArialSmall, Element.ALIGN_RIGHT))
            InsertEmptyRow(table, 6)
            Doc.Add(table)

            Doc.Add(New LineSeparator(2.0F, 100.0F, New BaseColor(Drawing.Color.Black), Element.ALIGN_LEFT, +5))
            'Doc.Add(Chunk.NEWLINE)

        Catch ex As Exception

        End Try

    End Sub
    Private Sub TabellaOreLavoro(Filter As Diario.udtFilterOreLavoro)
        Dim Arial = FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.NORMAL)
        Dim ArialBold = FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD)

        Try

            Dim table = New PdfPTable(6)
            table.DefaultCell.Border = Rectangle.NO_BORDER
            table.TotalWidth = 530
            table.LockedWidth = True
            table.SetWidths(New Single() {40, 180, 100, 80, 60, 60})

            'Intestazione
            table.AddCell(GetCell("Giorno ", ArialBold, Element.ALIGN_CENTER))
            table.AddCell(GetCell("Commessa ", ArialBold, Element.ALIGN_LEFT))
            table.AddCell(GetCell("Attività ", ArialBold, Element.ALIGN_LEFT))
            table.AddCell(GetCell("Settore ", ArialBold, Element.ALIGN_LEFT))
            'table.AddCell(GetCell("Commessa ", ArialBold, Element.ALIGN_CENTER))
            table.AddCell(GetCell("OreLavoro ", ArialBold, Element.ALIGN_CENTER))
            table.AddCell(GetCell("OreViaggio ", ArialBold, Element.ALIGN_CENTER))
            Doc.Add(table)

            'Commesse
            xDiario.GetWorkedHoursMonthly(Filter)
            Dim Orders As New List(Of String)
            For Each item As Diario.udtDgvOreLavoro In xDiario.WorkedHoursMonthly
                If Not Orders.Contains(item.Commessa) Then
                    Orders.Add(item.Commessa)
                End If
            Next

            Dim f As New Diario.udtFilterOreLavoro
                f.EmployeeName = Filter.EmployeeName
                f.OrderName = Filter.OrderName
                f.Activity = Filter.Activity
                f.Sector = Filter.Sector
                f.BeginDate = Filter.BeginDate
                f.EndDate = Filter.EndDate
                TabellaOreLavoroCommesse(f)

            Doc.Add(New LineSeparator(2.0F, 100.0F, New BaseColor(Drawing.Color.Black), Element.ALIGN_LEFT, +5))

            'Totali
            Dim OreLavoroTot As String = ""
            Dim OreViaggioTot As String = ""
            Dim ore As udtOre = GetOre(xDiario.WorkedHoursMonthly)
            OreLavoroTot = TimeSpanToText(ore.Lavoro)
            OreViaggioTot = TimeSpanToText(ore.Viaggio)

            table = New PdfPTable(6)
            table.TotalWidth = 530
            table.LockedWidth = True
            table.SetWidths(New Single() {40, 100, 90, 180, 60, 60})

            table.AddCell(GetCell(" ", ArialBold, Element.ALIGN_CENTER))
            table.AddCell(GetCell(" ", ArialBold, Element.ALIGN_CENTER))
            table.AddCell(GetCell(" ", ArialBold, Element.ALIGN_CENTER))
            table.AddCell(GetCell("Totale ore mese", ArialBold, Element.ALIGN_CENTER))
            table.AddCell(GetCellWithBorder(OreLavoroTot, ArialBold, Element.ALIGN_CENTER, New BaseColor(Drawing.Color.PaleTurquoise)))
            table.AddCell(GetCellWithBorder(OreViaggioTot, ArialBold, Element.ALIGN_CENTER, New BaseColor(Drawing.Color.PaleTurquoise)))
            InsertEmptyRow(table, 6)
            Doc.Add(table)

            'Straordinario
            Dim wh As TimeSpan = GetWorkingHours(Filter.BeginDate, Filter.EndDate)
            ore.Straordinario = ore.Lavoro.Add(ore.Viaggio)
            Dim tot As String = TimeSpanToText(ore.Straordinario)
            ore.Straordinario = ore.Straordinario.Subtract(wh)
            Dim OreStraordinario As String = TimeSpanToText(ore.Straordinario)
            Dim work As String = TimeSpanToText(wh)

            table = New PdfPTable(3)
            InsertEmptyRow(table, 6)
            table.AddCell(GetCellWithBorder("Ore totali", ArialBold, Element.ALIGN_CENTER, New BaseColor(Drawing.Color.PaleTurquoise)))
            table.AddCell(GetCellWithBorder("Ore lavorative", ArialBold, Element.ALIGN_CENTER, New BaseColor(Drawing.Color.PaleTurquoise)))
            table.AddCell(GetCellWithBorder("Ore straordinario", ArialBold, Element.ALIGN_CENTER, New BaseColor(Drawing.Color.PaleTurquoise)))
            table.AddCell(GetCellWithBorder(tot, ArialBold, Element.ALIGN_CENTER))
            table.AddCell(GetCellWithBorder(work, ArialBold, Element.ALIGN_CENTER))
            table.AddCell(GetCellWithBorder(OreStraordinario, ArialBold, Element.ALIGN_CENTER))
            Doc.Add(table)

        Catch ex As Exception

        End Try

    End Sub
    Private Sub TabellaOreLavoroCommesse(Filter As Diario.udtFilterOreLavoro)

        Dim Arial = FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.NORMAL)
        Dim ArialBold = FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD)

        Try

            Dim table = New PdfPTable(6)
            table.DefaultCell.Border = Rectangle.NO_BORDER
            table.TotalWidth = 530
            table.LockedWidth = True
            table.SetWidths(New Single() {40, 180, 100, 80, 60, 60})

            'Dati
            Dim font As Font = FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.NORMAL)
            Dim PdfPCell As PdfPCell = Nothing

            Dim iRow As Integer = 0
            xDiario.GetWorkedHoursMonthly(Filter)
            For Each item As Diario.udtDgvOreLavoro In xDiario.WorkedHoursMonthly
                Dim alignemnt As Integer = Element.ALIGN_CENTER
                Dim str As String = item.Giorno
                If iRow Mod (2) = 0 Then
                    table.AddCell(GetCell(item.Giorno, font, Element.ALIGN_CENTER, New BaseColor(Drawing.Color.Lavender)))
                    table.AddCell(GetCell(item.Commessa, font, Element.ALIGN_LEFT, New BaseColor(Drawing.Color.Lavender)))
                    table.AddCell(GetCell(item.Attività, font, Element.ALIGN_LEFT, New BaseColor(Drawing.Color.Lavender)))
                    table.AddCell(GetCell(item.Settore, font, Element.ALIGN_LEFT, New BaseColor(Drawing.Color.Lavender)))
                    table.AddCell(GetCell(item.OreLavoro, font, Element.ALIGN_CENTER, New BaseColor(Drawing.Color.Lavender)))
                    table.AddCell(GetCell(item.OreViaggio, font, Element.ALIGN_CENTER, New BaseColor(Drawing.Color.Lavender)))
                Else
                    table.AddCell(GetCell(item.Giorno, font, Element.ALIGN_CENTER))
                    table.AddCell(GetCell(item.Commessa, font, Element.ALIGN_LEFT))
                    table.AddCell(GetCell(item.Attività, font, Element.ALIGN_LEFT))
                    table.AddCell(GetCell(item.Settore, font, Element.ALIGN_LEFT))
                    table.AddCell(GetCell(item.OreLavoro, font, Element.ALIGN_CENTER))
                    table.AddCell(GetCell(item.OreViaggio, font, Element.ALIGN_CENTER))
                End If
                iRow += 1
            Next

            InsertEmptyRow(table, 6)
            Doc.Add(table)

            'Doc.Add(New Paragraph(".", FontFactory.GetFont("Arial", 6, iTextSharp.text.Font.NORMAL)))
            Doc.Add(New LineSeparator(1.0F, 100.0F, New BaseColor(Drawing.Color.Black), Element.ALIGN_LEFT, +5))

            'Totali
            Dim OreLavoroTot As String = ""
            Dim OreViaggioTot As String = ""
            Dim ore As udtOre = GetOre(xDiario.WorkedHoursMonthly)
            OreLavoroTot = TimeSpanToText(ore.Lavoro)
            OreViaggioTot = TimeSpanToText(ore.Viaggio)

            table = New PdfPTable(6)
            table.TotalWidth = 530
            table.LockedWidth = True
            table.SetWidths(New Single() {40, 180, 100, 80, 60, 60})

            table.AddCell(GetCell(" ", ArialBold, Element.ALIGN_CENTER))
            table.AddCell(GetCell(" ", ArialBold, Element.ALIGN_CENTER))
            table.AddCell(GetCell(" ", ArialBold, Element.ALIGN_CENTER))
            table.AddCell(GetCell(" ", ArialBold, Element.ALIGN_CENTER))
            table.AddCell(GetCellWithBorder(OreLavoroTot, ArialBold, Element.ALIGN_CENTER, New BaseColor(Drawing.Color.PaleTurquoise)))
            table.AddCell(GetCellWithBorder(OreViaggioTot, ArialBold, Element.ALIGN_CENTER, New BaseColor(Drawing.Color.PaleTurquoise)))

            InsertEmptyRow(table, 6)
            Doc.Add(table)

            'Doc.Add(Chunk.NEWLINE)

        Catch ex As Exception

        End Try


    End Sub
#End Region


#Region "Spese"
    Private Sub LogoSpese()

        Try

            Dim table = New PdfPTable(3)
            table.TotalWidth = 780
            table.LockedWidth = True
            table.SetWidths(New Single() {280, 200, 400})

            Dim Arial = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL)
            Dim FontCizeta = FontFactory.GetFont("Arial", 40, iTextSharp.text.Font.BOLD, New BaseColor(Drawing.Color.OrangeRed))
            Dim FontNotaSpese = FontFactory.GetFont("Arial", 40, iTextSharp.text.Font.BOLD, New BaseColor(Drawing.Color.Blue))

            Dim LineSeparator = New LineSeparator(2.0F, 100.0F, New BaseColor(Drawing.Color.Black), Element.ALIGN_LEFT, 1)
            Doc.Add(LineSeparator)

            table.AddCell(GetCell("cizeta", 1, 4, FontCizeta, Element.ALIGN_CENTER))
            table.AddCell(GetCell("CIZETA AUTOMAZIONE S.R.L.", Arial, Element.ALIGN_LEFT))
            table.AddCell(GetCell("NOTA SPESE", 1, 4, FontNotaSpese, Element.ALIGN_CENTER))

            table.AddCell(GetCell("15057 TORTONA (AL)", Arial, Element.ALIGN_LEFT))
            table.AddCell(GetCell("Via S.FERRARI 20/5", Arial, Element.ALIGN_LEFT))
            table.AddCell(GetCell("p.i. 0096859 0067", Arial, Element.ALIGN_LEFT))

            Doc.Add(table)

            Doc.Add(New LineSeparator(2.0F, 100.0F, New BaseColor(Drawing.Color.Black), Element.ALIGN_LEFT, -5))
            ' Doc.Add(Chunk.NEWLINE)

        Catch ex As Exception

        End Try

    End Sub

    Private Sub IntestazioneSpese(EmployeeName As String, Month As String, Year As String)

        Dim Arial = FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.NORMAL)
        Dim ArialSmall = FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.NORMAL)

        Try

            Dim table = New PdfPTable(7)
            table.DefaultCell.Border = Rectangle.NO_BORDER
            table.TotalWidth = 780
            table.LockedWidth = True
            table.SetWidths(New Single() {50, 180, 50, 100, 50, 40, 320})

            InsertEmptyRow(table, 6)
            table.AddCell(GetCell("Nome ", ArialSmall, Element.ALIGN_RIGHT))
            table.AddCell(GetCell(EmployeeName, ArialSmall, Element.ALIGN_CENTER, New BaseColor(Drawing.Color.PaleTurquoise)))
            table.AddCell(GetCell("Mese ", ArialSmall, Element.ALIGN_RIGHT))
            table.AddCell(GetCell(Month, ArialSmall, Element.ALIGN_CENTER, New BaseColor(Drawing.Color.PaleTurquoise)))
            table.AddCell(GetCell("Anno ", ArialSmall, Element.ALIGN_RIGHT))
            table.AddCell(GetCell(Year, ArialSmall, Element.ALIGN_CENTER, New BaseColor(Drawing.Color.PaleTurquoise)))
            table.AddCell(GetCell(" ", ArialSmall, Element.ALIGN_RIGHT))
            InsertEmptyRow(table, 6)
            Doc.Add(table)

            Doc.Add(New LineSeparator(2.0F, 100.0F, New BaseColor(Drawing.Color.Black), Element.ALIGN_LEFT, +5))
            'Doc.Add(Chunk.NEWLINE)

        Catch ex As Exception

        End Try

    End Sub

    Private Sub TabellaSpese(Filter As Diario.udtFilterExpenses)

        Dim Arial = FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.NORMAL)
        Dim ArialBold = FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD)
        Dim ArialSmall = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL)

        Dim ColunmnsName() As String = {"Data", "Commessa", "Località", "Km", "Autostrada", "MezziPub", "Vitto", "Alloggio", "Varie", "Diaria", "CCA", "Valuta"}
        tcdRefound = New TableColumnsDefinition(ColunmnsName)

        Try

            Dim table = New PdfPTable(13)
            table.TotalWidth = 780
            table.LockedWidth = True
            table.SetWidths(New Single() {50, 120, 110, 30, 70, 60, 50, 70, 50, 50, 50, 50, 20})

            'Intestazione
            table.AddCell(GetCell("Data", ArialBold, Element.ALIGN_CENTER))
            table.AddCell(GetCell("Commessa", ArialBold, Element.ALIGN_LEFT))
            table.AddCell(GetCell("Località", ArialBold, Element.ALIGN_LEFT))
            table.AddCell(GetCell("Km", ArialBold, Element.ALIGN_CENTER))
            table.AddCell(GetCell("Autostrada", ArialBold, Element.ALIGN_CENTER))
            table.AddCell(GetCell("MezziPub", ArialBold, Element.ALIGN_CENTER))
            table.AddCell(GetCell("Vitto", ArialBold, Element.ALIGN_CENTER))
            table.AddCell(GetCell("Alloggio", ArialBold, Element.ALIGN_CENTER))
            table.AddCell(GetCell("Varie", ArialBold, Element.ALIGN_CENTER))
            table.AddCell(GetCell("CCA", ArialBold, Element.ALIGN_CENTER))
            table.AddCell(GetCell("Valuta", ArialBold, Element.ALIGN_CENTER))
            table.AddCell(GetCell("Trasferta", ArialBold, Element.ALIGN_CENTER))
            table.AddCell(GetCell(" ", ArialBold, Element.ALIGN_CENTER))
            InsertEmptyRow(table, 6)
            Doc.Add(table)
            Doc.Add(New LineSeparator(1.0F, 100.0F, New BaseColor(Drawing.Color.Black), Element.ALIGN_LEFT, +5))


            'Dati
            table = New PdfPTable(13)
            table.TotalWidth = 770
            table.LockedWidth = True
            table.SetWidths(New Single() {50, 120, 100, 30, 70, 60, 50, 70, 50, 50, 50, 50, 20})


            Dim font As Font = FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.NORMAL)
            Dim PdfPCell As PdfPCell = Nothing
            Dim Totali(12) As Single

            Dim iRow As Integer = 0
            xDiario.GetExpensesMonthly(Filter)
            For Each item As Diario.udtDgvExpenses In xDiario.ExpensesMonthly
                Dim alignemnt As Integer = Element.ALIGN_CENTER
                Dim str As String = item.Giorno
                If iRow Mod (2) = 0 Then
                    table.AddCell(GetCell(item.Giorno, font, Element.ALIGN_CENTER, New BaseColor(Drawing.Color.Lavender)))
                    table.AddCell(GetCell(item.Commessa, font, Element.ALIGN_LEFT, New BaseColor(Drawing.Color.Lavender)))
                    table.AddCell(GetCell(item.Località, font, Element.ALIGN_LEFT, New BaseColor(Drawing.Color.Lavender)))
                    table.AddCell(GetCell(item.Km, font, Element.ALIGN_RIGHT, New BaseColor(Drawing.Color.Lavender)))
                    table.AddCell(GetCell(item.Autostrada, font, Element.ALIGN_RIGHT, New BaseColor(Drawing.Color.Lavender)))
                    table.AddCell(GetCell(item.Mezzi, font, Element.ALIGN_RIGHT, New BaseColor(Drawing.Color.Lavender)))
                    table.AddCell(GetCell(item.Vitto, font, Element.ALIGN_RIGHT, New BaseColor(Drawing.Color.Lavender)))
                    table.AddCell(GetCell(item.Alloggio, font, Element.ALIGN_RIGHT, New BaseColor(Drawing.Color.Lavender)))
                    table.AddCell(GetCell(item.Varie, font, Element.ALIGN_RIGHT, New BaseColor(Drawing.Color.Lavender)))
                    table.AddCell(GetCell(item.Carta, font, Element.ALIGN_RIGHT, New BaseColor(Drawing.Color.Lavender)))
                    table.AddCell(GetCell(item.Valuta, font, Element.ALIGN_RIGHT, New BaseColor(Drawing.Color.Lavender)))
                    table.AddCell(GetCell(item.Trasferta, font, Element.ALIGN_CENTER, New BaseColor(Drawing.Color.Lavender)))
                Else
                    table.AddCell(GetCell(item.Giorno, font, Element.ALIGN_CENTER))
                    table.AddCell(GetCell(item.Commessa, font, Element.ALIGN_LEFT))
                    table.AddCell(GetCell(item.Località, font, Element.ALIGN_LEFT))
                    table.AddCell(GetCell(item.Km, font, Element.ALIGN_RIGHT))
                    table.AddCell(GetCell(item.Autostrada, font, Element.ALIGN_RIGHT))
                    table.AddCell(GetCell(item.Mezzi, font, Element.ALIGN_RIGHT))
                    table.AddCell(GetCell(item.Vitto, font, Element.ALIGN_RIGHT))
                    table.AddCell(GetCell(item.Alloggio, font, Element.ALIGN_RIGHT))
                    table.AddCell(GetCell(item.Varie, font, Element.ALIGN_RIGHT))
                    table.AddCell(GetCell(item.Carta, font, Element.ALIGN_RIGHT))
                    table.AddCell(GetCell(item.Valuta, font, Element.ALIGN_RIGHT))
                    table.AddCell(GetCell(item.Trasferta, font, Element.ALIGN_CENTER))
                End If
                table.AddCell(GetCell(" ", font, Element.ALIGN_LEFT))
                Totali(3) = Totali(3) + TextToSingle(item.Km)
                Totali(4) = Totali(4) + TextToSingle(item.Autostrada)
                Totali(5) = Totali(5) + TextToSingle(item.Mezzi)
                Totali(6) = Totali(6) + TextToSingle(item.Vitto)
                Totali(7) = Totali(7) + TextToSingle(item.Alloggio)
                Totali(8) = Totali(8) + TextToSingle(item.Varie)
                Totali(9) = Totali(9) + TextToSingle(item.Carta)
                Totali(10) = Totali(10) + TextToSingle(item.Valuta)
                iRow += 1
            Next
            InsertEmptyRow(table, 6)
            Doc.Add(table)
            Doc.Add(New LineSeparator(1.0F, 100.0F, New BaseColor(Drawing.Color.Black), Element.ALIGN_LEFT, +5))




            'Sommatoria
            table = New PdfPTable(13)
            table.TotalWidth = 770
            table.LockedWidth = True
            table.SetWidths(New Single() {50, 120, 100, 30, 70, 60, 50, 70, 50, 50, 50, 50, 20})

            table.AddCell(GetCell("TOTALI", ArialBold, Element.ALIGN_CENTER))
            table.AddCell(GetCell(" ", ArialBold, Element.ALIGN_LEFT))
            table.AddCell(GetCell(" ", ArialBold, Element.ALIGN_LEFT))
            table.AddCell(GetCell(Format(Totali(3), "#0"), ArialBold, Element.ALIGN_RIGHT))     'KM
            table.AddCell(GetCell(Format(Totali(4), "#0"), ArialBold, Element.ALIGN_RIGHT))
            table.AddCell(GetCell(Format(Totali(5), "#0.00"), ArialBold, Element.ALIGN_RIGHT))
            table.AddCell(GetCell(Format(Totali(6), "#0.00"), ArialBold, Element.ALIGN_RIGHT))
            table.AddCell(GetCell(Format(Totali(7), "#0.00"), ArialBold, Element.ALIGN_RIGHT))
            table.AddCell(GetCell(Format(Totali(8), "#0.00"), ArialBold, Element.ALIGN_RIGHT))
            table.AddCell(GetCell(Format(Totali(9), "#0.00"), ArialBold, Element.ALIGN_RIGHT))
            table.AddCell(GetCell(Format(Totali(10), "#0.00"), ArialBold, Element.ALIGN_RIGHT))
            ' table.AddCell(GetCell(Format(Totali(11), "#0.00"), ArialBold, Element.ALIGN_RIGHT))
            table.AddCell(GetCell(" ", ArialBold, Element.ALIGN_RIGHT))
            table.AddCell(GetCell(" ", ArialBold, Element.ALIGN_LEFT))

            InsertEmptyRow(table, 6)
            Doc.Add(table)
            Doc.Add(New LineSeparator(1.0F, 100.0F, New BaseColor(Drawing.Color.Black), Element.ALIGN_LEFT, +5))


        Catch ex As Exception

        End Try



    End Sub

    Private Sub PlaceFooterExpenses(Totali() As Single, RimbKm As Single)
        Dim Arial = FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.NORMAL)

        Dim footerTot As PdfPTable = New PdfPTable(10)
        footerTot.TotalWidth = 770
        footerTot.SetWidths(New Single() {80, 80, 80, 80, 80, 80, 100, 80, 100, 10})
        footerTot.LockedWidth = True

        footerTot.AddCell(GetCellWithBorder("Viaggio", Arial, Element.ALIGN_CENTER, New BaseColor(Drawing.Color.PaleTurquoise)))
        footerTot.AddCell(GetCellWithBorder("+Vitto", Arial, Element.ALIGN_CENTER, New BaseColor(Drawing.Color.PaleTurquoise)))
        footerTot.AddCell(GetCellWithBorder("+Alloggio", Arial, Element.ALIGN_CENTER, New BaseColor(Drawing.Color.PaleTurquoise)))
        footerTot.AddCell(GetCellWithBorder("+Varie", Arial, Element.ALIGN_CENTER, New BaseColor(Drawing.Color.PaleTurquoise)))
        footerTot.AddCell(GetCellWithBorder("+Diaria", Arial, Element.ALIGN_CENTER, New BaseColor(Drawing.Color.PaleTurquoise)))
        footerTot.AddCell(GetCellWithBorder("-CCA", Arial, Element.ALIGN_CENTER, New BaseColor(Drawing.Color.PaleTurquoise)))
        footerTot.AddCell(GetCellWithBorder("=Totale spese", Arial, Element.ALIGN_CENTER, New BaseColor(Drawing.Color.PaleTurquoise)))
        footerTot.AddCell(GetCellWithBorder("-Anticipo", Arial, Element.ALIGN_CENTER, New BaseColor(Drawing.Color.PaleTurquoise)))
        footerTot.AddCell(GetCellWithBorder("=Rimborso", Arial, Element.ALIGN_CENTER, New BaseColor(Drawing.Color.PaleTurquoise)))
        footerTot.AddCell(GetCell(" ", Arial, Element.ALIGN_CENTER))

        Dim SpeseViaggio As Single = RimbKm + Totali(4) + Totali(5)
        Dim TotSpese As Single = SpeseViaggio + Totali(6) + Totali(7) + Totali(8) + Totali(9) - Totali(10)
        Dim Rimborso As Single = TotSpese - Totali(11)
        footerTot.AddCell(GetCellWithBorder("€ " & Format(SpeseViaggio, "#0.00"), Arial, Element.ALIGN_CENTER))
        footerTot.AddCell(GetCellWithBorder("€ " & Format(Totali(6), "#0.00"), Arial, Element.ALIGN_CENTER))
        footerTot.AddCell(GetCellWithBorder("€ " & Format(Totali(7), "#0.00"), Arial, Element.ALIGN_CENTER))
        footerTot.AddCell(GetCellWithBorder("€ " & Format(Totali(8), "#0.00"), Arial, Element.ALIGN_CENTER))
        footerTot.AddCell(GetCellWithBorder("€ " & Format(Totali(9), "#0.00"), Arial, Element.ALIGN_CENTER))
        footerTot.AddCell(GetCellWithBorder("€ " & Format(Totali(10), "#0.00"), Arial, Element.ALIGN_CENTER))
        footerTot.AddCell(GetCellWithBorder("€ " & Format(TotSpese, "#0.00"), Arial, Element.ALIGN_CENTER))
        footerTot.AddCell(GetCellWithBorder("€ " & Format(Totali(11), "#0.00"), Arial, Element.ALIGN_CENTER))
        footerTot.AddCell(GetCellWithBorder("€ " & Format(Rimborso, "#0.00"), Arial, Element.ALIGN_CENTER))
        footerTot.AddCell(GetCell(" ", Arial, Element.ALIGN_CENTER))

        footerTot.WriteSelectedRows(0, -1, 45, 80, Writer.DirectContent)


        Dim str As String = "Tortona il " & Format(Date.Now, "dd/MM/yyyy") & "            Firma ............................................"
        Dim footer As Paragraph = New Paragraph(str, FontFactory.GetFont(FontFactory.TIMES, 10, iTextSharp.text.Font.NORMAL))
        footer.Alignment = Element.ALIGN_RIGHT
        Dim footerTbl As PdfPTable = New PdfPTable(1)
        footerTbl.TotalWidth = 1000
        footerTbl.HorizontalAlignment = Element.ALIGN_CENTER
        Dim cell2 As PdfPCell = New PdfPCell(footer)
        cell2.Border = 0
        cell2.PaddingLeft = 10
        footerTbl.AddCell(cell2)
        footerTbl.WriteSelectedRows(0, -1, 500, 30, Writer.DirectContent)

    End Sub

#End Region

    Private Sub InsertEmptyRow(ByRef table As PdfPTable, fontSize As Integer)
        Dim font = FontFactory.GetFont("Arial", fontSize, iTextSharp.text.Font.NORMAL)
        For iRow As Integer = 0 To table.NumberOfColumns - 1
            table.AddCell(GetCell(" ", font, Element.ALIGN_LEFT))
        Next
    End Sub
    Private Function GetCell(ByVal text As String, f As Font, alignment As Integer, BackColor As BaseColor) As PdfPCell
        Return GetCell(text, 1, 1, f, alignment, BackColor)
    End Function
    Private Function GetCell(ByVal text As String, f As Font, alignment As Integer) As PdfPCell
        Return GetCell(text, 1, 1, f, alignment, Nothing)
    End Function
    Private Function GetCell(ByVal text As String, ByVal colSpan As Integer, ByVal rowSpan As Integer, f As Font, alignment As Integer) As PdfPCell
        Return GetCell(text, colSpan, rowSpan, f, alignment, New BaseColor(Drawing.Color.Transparent))
    End Function
    Private Function GetCell(ByVal text As String, ByVal colSpan As Integer, ByVal rowSpan As Integer, f As Font, alignment As Integer, BackColor As BaseColor) As PdfPCell
        Dim cell As PdfPCell = New PdfPCell(New Phrase(text, f))
        cell.HorizontalAlignment = alignment
        ' cell.VerticalAlignment = Element.ALIGN_TOP
        cell.Rowspan = rowSpan
        cell.Colspan = colSpan
        cell.Border = Rectangle.NO_BORDER
        cell.BackgroundColor = BackColor
        Return cell
    End Function

    Private Function GetCellWithBorder(ByVal text As String, f As Font, alignment As Integer, BackColor As BaseColor) As PdfPCell
        Return GetCellWithBorder(text, 1, 1, f, alignment, BackColor)
    End Function
    Private Function GetCellWithBorder(ByVal text As String, f As Font, alignment As Integer) As PdfPCell
        Return GetCellWithBorder(text, 1, 1, f, alignment, Nothing)
    End Function
    Private Function GetCellWithBorder(ByVal text As String, ByVal colSpan As Integer, ByVal rowSpan As Integer, f As Font, alignment As Integer) As PdfPCell
        Return GetCellWithBorder(text, colSpan, rowSpan, f, alignment, New BaseColor(Drawing.Color.Transparent))
    End Function
    Private Function GetCellWithBorder(ByVal text As String, ByVal colSpan As Integer, ByVal rowSpan As Integer, f As Font, alignment As Integer, BackColor As BaseColor) As PdfPCell
        Dim cell As PdfPCell = New PdfPCell(New Phrase(text, f))
        cell.HorizontalAlignment = alignment
        ' cell.VerticalAlignment = Element.ALIGN_TOP
        cell.Rowspan = rowSpan
        cell.Colspan = colSpan
        'cell.Border = Rectangle.NO_BORDER
        cell.BackgroundColor = BackColor
        Return cell
    End Function


    Private Sub PlaceText(ByVal pdfContentByte As PdfContentByte, ByVal text As String, ByVal font As iTextSharp.text.Font, ByVal lowerLeftx As Single, ByVal lowerLefty As Single, ByVal upperRightx As Single, ByVal upperRighty As Single, ByVal leading As Single, ByVal alignment As Integer)
        Dim ct As ColumnText = New ColumnText(pdfContentByte)
        ct.SetSimpleColumn(New Phrase(text, font), lowerLeftx, lowerLefty, upperRightx, upperRighty, leading, alignment)
        ct.Go()
    End Sub



    Private Sub cmdPrint_Click(FileName As String)

        Try

            'Get de the default printer in the system
            PrinterToUseSetting = DocumentPrinter.GetDefaultPrinterSetting

            'uncomment if you want to change the default printer before print
            DocumentPrinter.ChangePrinterSettings(PrinterToUseSetting)


            'print your file 
            If DocumentPrinter.PrintFile(FileName, PrinterToUseSetting) Then
                'MsgBox("your print file success message")
            Else
                'MsgBox("your print file failed message")
            End If

        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try

    End Sub


    Public NotInheritable Class DocumentPrinter

        Shared Sub New()

        End Sub

        Public Shared Function PrintFile(ByVal fileName As String, printerSetting As System.Drawing.Printing.PrinterSettings) As Boolean

            Dim printProcess As System.Diagnostics.Process = Nothing
            Dim printed As Boolean = False


            Try

                If printerSetting IsNot Nothing Then

                    Dim startInfo As New ProcessStartInfo()
                    startInfo.Verb = "Print"
                    startInfo.Arguments = printerSetting.PrinterName.ToString     ' <----printer to use---- 
                    startInfo.FileName = fileName
                    startInfo.UseShellExecute = True
                    startInfo.CreateNoWindow = True
                    startInfo.WindowStyle = ProcessWindowStyle.Hidden

                    Using print As System.Diagnostics.Process = Process.Start(startInfo)

                        'Close the application after X milliseconds with WaitForExit(X)   

                        print.WaitForExit(10000)

                        If print.HasExited = False Then

                            If print.CloseMainWindow() Then
                                printed = True
                            Else
                                printed = True
                            End If

                        Else
                            printed = True

                        End If

                        print.Close()

                    End Using


                Else
                    Throw New Exception("Stampante non trovata...")
                End If


            Catch ex As Exception
                Throw
            End Try

            Return printed

        End Function

        Public Shared Sub ChangePrinterSettings(ByRef defaultPrinterSetting As System.Drawing.Printing.PrinterSettings)

            Dim printDialogBox As New System.Windows.Forms.PrintDialog

            If printDialogBox.ShowDialog = System.Windows.Forms.DialogResult.OK Then

                If printDialogBox.PrinterSettings.IsValid Then
                    defaultPrinterSetting = printDialogBox.PrinterSettings
                End If

            End If

        End Sub

        Public Shared Function GetDefaultPrinterSetting() As System.Drawing.Printing.PrinterSettings

            Dim defaultPrinterSetting As System.Drawing.Printing.PrinterSettings = Nothing

            For Each printer As String In System.Drawing.Printing.PrinterSettings.InstalledPrinters


                defaultPrinterSetting = New System.Drawing.Printing.PrinterSettings
                defaultPrinterSetting.PrinterName = printer

                If defaultPrinterSetting.IsDefaultPrinter Then
                    Return defaultPrinterSetting
                End If

            Next

            Return defaultPrinterSetting

        End Function

    End Class

End Class
