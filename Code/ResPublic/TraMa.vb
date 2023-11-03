Imports System.IO
Imports System.Data.SqlClient

Public Class TraMa

    Private WithEvents Pc As ePc
    Private ToPc As TraMaDataCoding
    Private PcCnf As AppConfig.udtParams.udtPcConfig
    Private FromPc As New TraMaDataDecoding

    Private myDatabase As New EnzoLib.czDatabase
    Private dbConnection As New SqlConnection

    'Private OldStatus As Integer = -1
    'Private OldWorkerId As Integer = -1
    'Private LastStatusTimeStamp As Date = Now


    Public Enum enumResults
        Unknown
        FoolProofDisabled
        NoAnswer
        Workable
        NotWorkable
        FileError
        Ack
        GenericError
    End Enum

    'Public Enum enumMode
    '    File
    '    SQL
    'End Enum

    Public Class Result
        Public Property Status As enumResults = enumResults.Unknown
        Public Property NotWorkabelCode As Integer = 0
        Public Property NotWorkabelMsg As String = ""
        Public Property MasterGood As Boolean = False
        Public Property MasterFail As Boolean = False
        Public Sub New()

        End Sub
    End Class


    Public Class Measure
        Public Property Name As String = ""
        Public Property Done As Boolean = False
        Public Property Failed As Boolean = False
        Public Property lsl As Single = 0
        Public Property val As Single = 0
        Public Property usl As Single = 0
    End Class

#Region "Properties"
    Public Property PcName As String = ""
    Public Property Enabled As Boolean = True
    Public Property AnswerTimeout As Integer = 1000
    Public Property SharedFolder As String = ""
    Public Property CharSepar As String = ";"
    Public Property ConnectionToDatabase As String = ""
    Public Property dbTableName As String = "TraMaInterface"
    Public Property IpAddress As String = "127.0.0.0"

    Public Property TraMaStationName As String = ""

    Public Property LastMsgIdFromTraMa As Integer = 0

    Public Property MeasuresEnabled As Boolean = True
    Public Property MeasuresDatabaseConnection As String = ""
    Public Property MeasuresTableName As String = "Measures"
    Public Property MeasureTimeStamp As String = "TimeStamp"
    Public Property MeasureName As String = "Name"
    Public Property MeasureValue As String = "Value"

    Public Property FailuresEnabled As Boolean = True
    Public Property FailuresDatabaseConnection As String = ""
    Public Property FailuresTableName As String = "Failures"
    Public Property FailureStationName As String = "Station"
    Public Property FailureCode As String = "Code"
    Public Property FailureDescription As String = "Description"


#End Region


#Region "Methods"

    Public Function IsConnected() As Boolean
        Try
            Dim MyPing As System.Net.NetworkInformation.Ping
            Dim PingReply As System.Net.NetworkInformation.PingReply
            MyPing = New System.Net.NetworkInformation.Ping
            PingReply = MyPing.Send(IpAddress, 500)
            If (PingReply.Status = System.Net.NetworkInformation.IPStatus.Success) Then
                Dim sFileName As String = SharedFolder & "DataFromTraMa.txt"
                If My.Computer.FileSystem.FileExists(sFileName) Then
                    Try
                        Dim fs As New System.IO.FileStream(sFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
                        Dim s As New System.IO.StreamReader(fs)
                        Dim aryStr As String() = s.ReadLine.Split(";")
                        s.Close()
                        fs.Close()
                        If EnzoLib.TextToInt(aryStr(0)) <> LastMsgIdFromTraMa Then
                            LastMsgIdFromTraMa = EnzoLib.TextToInt(aryStr(0))
                            Return True
                        Else
                            Return False
                        End If

                    Catch ex As Exception
                        Return False
                    End Try
                Else
                    Return False
                End If

            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try


    End Function

    'Public Sub CreateTableInDatabase()
    '    'Create the interface table in the database.

    '    myDatabase = New EnzoLib.czDatabase

    '    Dim dbConnection As New SqlConnection()
    '    dbConnection.ConnectionString = ConnectionToDatabase
    '    dbConnection.Open()

    '    'Crea la tabella  (Mettere sempre una chiave primaria!!!!)
    '    If myDatabase.CreateTable(dbConnection, dbTableName, "Id", "Int PRIMARY KEY") Then
    '        myDatabase.CreateField(dbConnection, dbTableName, "PieceCode", "NVarChar(40)  NULL")
    '        myDatabase.CreateField(dbConnection, dbTableName, "HostAck", "Bit")
    '        'IsWorkable
    '        myDatabase.CreateField(dbConnection, dbTableName, "IsWorkableReq", "Bit")
    '        myDatabase.CreateField(dbConnection, dbTableName, "IsWorkableYes", "Bit")
    '        myDatabase.CreateField(dbConnection, dbTableName, "IsWorkableNo", "Bit")
    '        myDatabase.CreateField(dbConnection, dbTableName, "ToBeRepaired", "Bit")
    '        myDatabase.CreateField(dbConnection, dbTableName, "IsMasterGood", "Bit")
    '        myDatabase.CreateField(dbConnection, dbTableName, "IsMasterFail", "Bit")
    '        myDatabase.CreateField(dbConnection, dbTableName, "NotWorkableCode", "Int")
    '        myDatabase.CreateField(dbConnection, dbTableName, "NotWorkableMsg", "NVarChar(255)  NULL")
    '        'Work done
    '        myDatabase.CreateField(dbConnection, dbTableName, "TestDone", "Bit")
    '        myDatabase.CreateField(dbConnection, dbTableName, "TestPassed", "Bit")
    '        myDatabase.CreateField(dbConnection, dbTableName, "TestFailed", "Bit")
    '        'Params changed
    '        myDatabase.CreateField(dbConnection, dbTableName, "ParamsChanged", "Bit")
    '        myDatabase.CreateField(dbConnection, dbTableName, "UserName", "NVarChar(40)  NULL")
    '        myDatabase.CreateField(dbConnection, dbTableName, "Params", "NVarChar(255)  NULL")

    '    End If

    '    dbConnection.Close()

    'End Sub

    'Public Function IsWorkableReqToHost(ByVal PieceCode As String) As Result
    '    Dim mResult As New Result

    '    If Not Enabled Then
    '        mResult.Status = enumResults.FoolProofDisabled
    '        Return mResult
    '    End If

    '    Select Case Mode
    '        Case enumMode.File
    '            Return IsWorkableReqToHostByFile(PieceCode)
    '        Case enumMode.SQL
    '            Return IsWorkableReqToHostBySQL(PieceCode)
    '        Case Else
    '            Return mResult
    '    End Select

    'End Function

    'Public Function SetWorkDoneToHost(ByVal PieceCode As String, ByVal ResultOk As Boolean, ByVal ParamArray param() As String) As Boolean

    '    If Not Enabled Then
    '        Return True
    '    End If

    '    Select Case Mode
    '        Case enumMode.File
    '            Return SetWorkDoneToHostByFile(PieceCode, ResultOk, param)
    '        Case enumMode.SQL
    '            Return SetWorkDoneToHostBySql(PieceCode, ResultOk, param)
    '        Case Else
    '            Return False
    '    End Select

    'End Function

    'Public Function ChangeParamToHost(ByVal UserName As String, ByVal ParamArray param() As String) As Boolean

    '    If Not Enabled Then
    '        Return True
    '    End If

    '    Select Case Mode
    '        Case enumMode.File
    '            Return ChangeParamToHostByFile(UserName, param)
    '        Case enumMode.SQL
    '            Return ChangeParamToHostBySql(UserName, param)
    '        Case Else
    '            Return False
    '    End Select

    'End Function

    'Public Function SetStatusToHost(ByVal Status As Integer, ByVal WorkerId As Integer) As Boolean
    '    Dim bRes As Boolean = False

    '    If Math.Abs(DateDiff(DateInterval.Minute, LastStatusTimeStamp, Date.Now)) >= 1 Then
    '        OldStatus = -1
    '    End If

    '    If (Status <> OldStatus) Or (WorkerId <> OldWorkerId) Then
    '        Select Case Mode
    '            Case enumMode.File
    '                bRes = SetStatusToHostByFile(Status, WorkerId, False)
    '            Case enumMode.SQL
    '                bRes = SetStatusToHostBySql(Status, WorkerId)
    '            Case Else
    '                Return False
    '        End Select
    '        If bRes Then
    '            OldStatus = Status
    '            OldWorkerId = WorkerId
    '            LastStatusTimeStamp = Date.Now
    '        End If
    '        Return bRes
    '    End If

    '    Return True

    'End Function

    'Public Function InitStatusToHost(ByVal Status As Integer, ByVal WorkerId As Integer) As Boolean
    '    Dim bRes As Boolean = False

    '    If (Status <> OldStatus) Or (WorkerId <> OldWorkerId) Then
    '        Select Case Mode
    '            Case enumMode.File
    '                bRes = SetStatusToHostByFile(Status, WorkerId, True)
    '            Case enumMode.SQL
    '                bRes = SetStatusToHostBySql(Status, WorkerId)
    '            Case Else
    '                Return False
    '        End Select
    '        If bRes Then
    '            OldStatus = Status
    '            OldWorkerId = WorkerId
    '        End If
    '        Return bRes
    '    End If

    '    Return True

    'End Function

    Public Function SendMeasuresToHost(PieceCode As String, ByVal myRes As udtTestResults, ByVal myModel As Model) As Boolean
        If Not Enabled Then Return True

        Dim MeasList As New List(Of Measure)
        MeasList.Add(GetMeasure("5600_10_LowBeamHorizontalPosition", myRes.GeomTest.LowBeamHotSpot.Hori, myModel.Geom.LowBeamHotSpot.Hori))
        MeasList.Add(GetMeasure("5600_11_LowBeamVerticalPosition", myRes.GeomTest.LowBeamHotSpot.Vert, myModel.Geom.LowBeamHotSpot.Vert))
        MeasList.Add(GetMeasure("5600_12_HighBeamHorizontalPosition", myRes.GeomTest.HighBeamHotSpot.Hori, myModel.Geom.HighBeamHotSpot.Hori))
        MeasList.Add(GetMeasure("5600_13_HighBeamVerticalPosition", myRes.GeomTest.HighBeamHotSpot.Vert, myModel.Geom.HighBeamHotSpot.Vert))
        MeasList.Add(GetMeasure("5600_14_PointHcHorizontalPosition", myRes.GeomTest.PointHC.Hori, myModel.Geom.PointHC.Hori))
        MeasList.Add(GetMeasure("5600_15_PointHcVerticalPosition", myRes.GeomTest.PointHC.Vert, myModel.Geom.PointHC.Vert))
        MeasList.Add(GetMeasure("5600_16_BiglVerticalPosition", myRes.GeomTest.BiglMaxGrad.Vert, myModel.Geom.BiglMaxGrad.Vert))


        Dim rowsAffected As Integer = 0
        Dim TableName As String = xTraMa.MeasuresTableName
        Try

            Dim dbConnection As SqlConnection
            dbConnection = New SqlConnection(xTraMa.FailuresDatabaseConnection)
            dbConnection.Open()

            For Each meas As Measure In MeasList
                Dim daTab As New SqlDataAdapter, dsTab As New DataSet
                daTab = New SqlDataAdapter("SELECT * From " & TableName & " WHERE PieceCode = '" & PieceCode & "' AND " & MeasureName & " = '" & meas.Name & "'", dbConnection)
                dsTab = New DataSet
                daTab.Fill(dsTab, TableName)

                If dsTab.Tables(0).Rows.Count = 0 Then
                    'Add measure
                    Dim sqlq As String = "INSERT INTO [" & TableName & "]"
                    sqlq += " (PieceCode," & MeasureName & ","
                    sqlq += MeasureTimeStamp & ","
                    sqlq += MeasureValue & ")"
                    sqlq += " VALUES ('" & PieceCode & "','" & meas.Name & "',"
                    sqlq += "'" & Date.Now & "',"
                    sqlq += "'" & Format(meas.val, "#0.00") & "'"
                    sqlq += ")"
                    Dim cmd As New SqlCommand(sqlq, dbConnection)
                    rowsAffected = cmd.ExecuteNonQuery()
                Else
                    'Modify value
                    Dim sqlq As String = "UPDATE " & TableName & " SET "
                    sqlq += MeasureTimeStamp & " = '" & Date.Now & "'" & ","
                    sqlq += MeasureValue & " = '" & Format(meas.val, "#0.00") & "'"
                    sqlq += " WHERE PieceCode = '" & PieceCode & "' AND " & MeasureName & " = '" & meas.Name & "'"
                    Dim cmd As New SqlCommand(sqlq, dbConnection)
                    rowsAffected = cmd.ExecuteNonQuery()
                End If

            Next

            dbConnection.Close()

            Return (rowsAffected > 0)

        Catch ex As Exception

            Return False
        End Try



        Return True
    End Function
    Private Function GetMeasure(MeasName As String, res As ResultData, rec As Recipe.udtTestMinMax) As Measure
        Dim mes As New Measure
        mes.Name = MeasName
        mes.Done = res.TestDone
        mes.Failed = Not res.Ok
        mes.lsl = rec.Min
        mes.usl = rec.Max
        mes.val = EnzoLib.TextToSingle(res.ValueStr)
        Return mes
    End Function

    Public Function SendFailuresToHost(PieceCode As String, ByVal myRes As udtTestResults) As Boolean
        If Not Enabled Then Return True

        If myRes.Ok Then Return True

        Dim ListFailures As New List(Of Test.udtFailure)
        ListFailures = xTest.SetFailures(PieceCode, myRes)

        If ListFailures.Count = 0 Then Return True

        Dim rowsAffected As Integer = 0

        Dim TableName As String = xTraMa.FailuresTableName

        Try

            Dim dbConnection As SqlConnection
            dbConnection = New SqlConnection(xTraMa.FailuresDatabaseConnection)
            dbConnection.Open()

            For Each fail As Test.udtFailure In ListFailures
                Dim daTab As New SqlDataAdapter, dsTab As New DataSet
                Dim sqlq As String = "SELECT * From " & TableName & " WHERE PieceCode = '" & PieceCode & "'"
                sqlq += " AND " & FailureStationName & " = '" & TraMaStationName & "'"
                sqlq += " AND " & FailureCode & " = '" & fail.Code & "'"
                daTab = New SqlDataAdapter(sqlq, dbConnection)
                dsTab = New DataSet
                daTab.Fill(dsTab, TableName)
                If dsTab.Tables(0).Rows.Count = 0 Then
                    'Add failure
                    sqlq = "INSERT INTO [" & TableName & "]"
                    sqlq += " (PieceCode," & FailureStationName & "," & FailureCode & "," & FailureDescription & ","
                    sqlq += MeasureTimeStamp & ")"
                    sqlq += " VALUES ('" & PieceCode & "','" & TraMaStationName & "','" & fail.Code & "','" & fail.Description & "',"
                    sqlq += "'" & Date.Now & "'"
                    sqlq += ")"
                    Dim cmd As New SqlCommand(sqlq, dbConnection)
                    rowsAffected = cmd.ExecuteNonQuery()
                Else
                    'Modify value
                    sqlq = "UPDATE " & TableName & " SET "
                    sqlq += MeasureTimeStamp & " = '" & Date.Now & "'" & ","
                    sqlq += " WHERE PieceCode = '" & PieceCode & "'"
                    sqlq += " AND " & FailureStationName & " = '" & TraMaStationName & "'"
                    sqlq += " AND " & FailureCode & " = '" & fail.Code & "'"
                    Dim cmd As New SqlCommand(sqlq, dbConnection)
                    rowsAffected = cmd.ExecuteNonQuery()
                End If

            Next

            dbConnection.Close()

            Return (rowsAffected > 0)

        Catch ex As Exception

            Return False
        End Try

        Return True
    End Function



    'Private Function SetResults(ByVal myRes As udtTestResults, ByVal myModel As Model) As List(Of String)
    '    Dim listRes As New List(Of String)
    '    Dim TestResult As New PlcDataCoding.Results

    '    If myRes.Ok Then Return Nothing

    '    'drive type
    '    If myRes.GeomTest.DriveType.TestDone And Not myRes.GeomTest.DriveType.Ok Then
    '        TestResult = xToPlc.SetTestResults(Test.enumFailureCode.DriveTypeTest, xFromPlc.PieceCode, myRes.GeomTest.DriveType.ValueStr)
    '        listRes.Add(TestResult.Code & ": " & TestResult.Description)
    '    End If

    '    If myRes.ElectTest.Ok Then
    '    Else
    '        If myRes.ElectTest.Vcc.TestDone And Not myRes.ElectTest.Vcc.Ok Then
    '            TestResult = xToPlc.SetTestResults(Test.enumFailureCode.ElectricalVcc, xFromPlc.PieceCode, myRes.ElectTest.Vcc.ValueStr)
    '            listRes.Add(TestResult.Code & ": " & TestResult.Description)
    '        End If
    '        If myRes.ElectTest.LowBeamAbsortion.TestDone And Not myRes.ElectTest.LowBeamAbsortion.Ok Then
    '            TestResult = xToPlc.SetTestResults(Test.enumFailureCode.LowBeamAbsortion, xFromPlc.PieceCode, myRes.ElectTest.LowBeamAbsortion.ValueStr)
    '            listRes.Add(TestResult.Code & ": " & TestResult.Description)
    '        End If
    '        If myRes.ElectTest.HighBeamAbsortion.TestDone And Not myRes.ElectTest.HighBeamAbsortion.Ok Then
    '            TestResult = xToPlc.SetTestResults(Test.enumFailureCode.HighBeamAbsortion, xFromPlc.PieceCode, myRes.ElectTest.HighBeamAbsortion.ValueStr)
    '            listRes.Add(TestResult.Code & ": " & TestResult.Description)
    '        End If
    '        If myRes.ElectTest.TurnLightAbsortion.TestDone And Not myRes.ElectTest.TurnLightAbsortion.Ok Then
    '            TestResult = xToPlc.SetTestResults(Test.enumFailureCode.TurnLightAbsortion, xFromPlc.PieceCode, myRes.ElectTest.TurnLightAbsortion.ValueStr)
    '            listRes.Add(TestResult.Code & ": " & TestResult.Description)
    '        End If
    '        If myRes.ElectTest.ParkLightAbsortion.TestDone And Not myRes.ElectTest.ParkLightAbsortion.Ok Then
    '            TestResult = xToPlc.SetTestResults(Test.enumFailureCode.ParkLightAbsortion, xFromPlc.PieceCode, myRes.ElectTest.ParkLightAbsortion.ValueStr)
    '            listRes.Add(TestResult.Code & ": " & TestResult.Description)
    '        End If
    '        If myRes.ElectTest.SideMarkerAbsortion.TestDone And Not myRes.ElectTest.SideMarkerAbsortion.Ok Then
    '            TestResult = xToPlc.SetTestResults(Test.enumFailureCode.SideMarkerAbsortion, xFromPlc.PieceCode, myRes.ElectTest.SideMarkerAbsortion.ValueStr)
    '            listRes.Add(TestResult.Code & ": " & TestResult.Description)
    '        End If
    '        If myRes.ElectTest.FogLightAbsortion.TestDone And Not myRes.ElectTest.FogLightAbsortion.Ok Then
    '            TestResult = xToPlc.SetTestResults(Test.enumFailureCode.FogBeamAbsortion, xFromPlc.PieceCode, myRes.ElectTest.FogLightAbsortion.ValueStr)
    '            listRes.Add(TestResult.Code & ": " & TestResult.Description)
    '        End If
    '        If myRes.ElectTest.CorneLightAbsortion.TestDone And Not myRes.ElectTest.CorneLightAbsortion.Ok Then
    '            TestResult = xToPlc.SetTestResults(Test.enumFailureCode.CorneLightAbsortion, xFromPlc.PieceCode, myRes.ElectTest.CorneLightAbsortion.ValueStr)
    '            listRes.Add(TestResult.Code & ": " & TestResult.Description)
    '        End If
    '    End If

    '    'Motors test
    '    If myRes.MotorTest.Ok Then
    '    Else
    '        For iPos As Byte = 0 To 8
    '            If myRes.MotorTest.Positions(iPos).Vert.TestDone And Not myRes.MotorTest.Positions(iPos).Vert.Ok Then
    '                Dim str As String = "Pos: " & iPos & " @ " & myRes.MotorTest.Positions(iPos).Vert.ValueStr
    '                TestResult = xToPlc.SetTestResults(Test.enumFailureCode.MotorsVertPosition, xFromPlc.PieceCode, str)
    '                listRes.Add(TestResult.Code & ": " & TestResult.Description)
    '            End If
    '        Next
    '        For iPos As Byte = 0 To 8
    '            If myRes.MotorTest.Positions(iPos).Hori.TestDone And Not myRes.MotorTest.Positions(iPos).Hori.Ok Then
    '                Dim str As String = "Pos: " & iPos & " @ " & myRes.MotorTest.Positions(iPos).Hori.ValueStr
    '                TestResult = xToPlc.SetTestResults(Test.enumFailureCode.MotorsHoriPosition, xFromPlc.PieceCode, str)
    '                listRes.Add(TestResult.Code & ": " & TestResult.Description)
    '            End If
    '        Next
    '        For iPos As Byte = 0 To 8
    '            If myRes.MotorTest.Positions(iPos).Curr.TestDone And Not myRes.MotorTest.Positions(iPos).Curr.Ok Then
    '                Dim str As String = "Pos: " & iPos & " @ " & myRes.MotorTest.Positions(iPos).Curr.ValueStr
    '                TestResult = xToPlc.SetTestResults(Test.enumFailureCode.MotorsAbsortion, xFromPlc.PieceCode, str)
    '                listRes.Add(TestResult.Code & ": " & TestResult.Description)
    '            End If
    '        Next

    '    End If

    '    'drive type
    '    If myRes.GeomTest.DriveType.TestDone And Not myRes.GeomTest.DriveType.Ok Then
    '        TestResult = xToPlc.SetTestResults(Test.enumFailureCode.DriveTypeTest, xFromPlc.PieceCode, myRes.GeomTest.DriveType.ValueStr)
    '        listRes.Add(TestResult.Code & ": " & TestResult.Description)
    '    End If

    '    'Aiming and Geomet
    '    If myRes.AimingTest.Ok And myRes.GeomTest.Ok Then
    '    Else
    '        'AIming
    '        If SetResultAimingCapacityToPlc(myRes.AimingTest.LowBeamCapacity, Test.enumFailureCode.AimingCapacityLowBeam) Then Return
    '        If SetResultAimingCapacityToPlc(myRes.AimingTest.HighBeamCapacity, Test.enumFailureCode.AimingCapacityHighBeam) Then Return
    '        If SetResultAimingCapacityToPlc(myRes.AimingTest.FogBeamCapacity, Test.enumFailureCode.AimingCapacityFogBeam) Then Return
    '        If SetResultAimingCapacityToPlc(myRes.AimingTest.CornerLightCapacity, Test.enumFailureCode.AimingCapacityCornerLight) Then Return
    '        If SetResultAimingCapacityToPlc(myRes.AimingTest.BiglCapacity, Test.enumFailureCode.AimingCapacityBiGL) Then Return



    '        'Geomet
    '        If SetResultsPosToPlc(myRes.GeomTest.PointHC, Test.enumFailureCode.GeomBrightnessPointHC) Then Return
    '        If SetResultsPosToPlc(myRes.GeomTest.LowBeamHotSpot, Test.enumFailureCode.GeomBrightnessMaxLB) Then Return
    '        If SetResultsPosToPlc(myRes.GeomTest.HighBeamHotSpot, Test.enumFailureCode.GeomBrightnessMaxHB) Then Return
    '        If SetResultsPosToPlc(myRes.GeomTest.FogBeamHotSpot, Test.enumFailureCode.GeomBrightnessMaxFB) Then Return
    '        If SetResultsPosToPlc(myRes.GeomTest.CornerLightHotSpot, Test.enumFailureCode.GeomBrightnessMaxCL) Then Return
    '        If SetResultsPosToPlc(myRes.GeomTest.CornerLightMaxGrad, Test.enumFailureCode.GeomBrightnessCutoffCL) Then Return
    '        If SetResultsPosToPlc(myRes.GeomTest.BiglHotSpot, Test.enumFailureCode.GeomBrightnessMaxBiGL) Then Return
    '        If SetResultsPosToPlc(myRes.GeomTest.BiglMaxGrad, Test.enumFailureCode.GeomBrightnessCutoffBiGL) Then Return

    '        'Low beam sharpness
    '        Dim NotOK As Boolean = False
    '        Dim str As String = ""
    '        For i As Byte = 1 To 5
    '            str += myRes.GeomTest.LowBeamSharpness.MaxGradAtPos(1).ValueStr & ","
    '            If myRes.GeomTest.LowBeamSharpness.MaxGradAtPos(i).TestDone And Not myRes.GeomTest.LowBeamSharpness.MaxGradAtPos(i).Ok Then
    '                NotOK = True
    '            End If
    '        Next
    '        If NotOK Then
    '            TestResult = xToPlc.SetTestResults(Test.enumFailureCode.GeomSharpnessLB, xFromPlc.PieceCode, str)
    '            listRes.Add(TestResult.Code & ": " & TestResult.Description)
    '        End If

    '        If myRes.GeomTest.LowBeamShape.Azim.TestDone And Not myRes.GeomTest.LowBeamShape.Azim.Ok Or _
    '            myRes.GeomTest.LowBeamShape.Site.TestDone And Not myRes.GeomTest.LowBeamShape.Site.Ok Or _
    '            myRes.GeomTest.LowBeamShape.Height.TestDone And Not myRes.GeomTest.LowBeamShape.Height.Ok Then
    '            str = "Aizm: " & myRes.GeomTest.LowBeamShape.Azim.ValueStr & ",Site: " & myRes.GeomTest.LowBeamShape.Site.ValueStr & ", Step: " & myRes.GeomTest.LowBeamShape.Height.ValueStr
    '            TestResult = xToPlc.SetTestResults(Test.enumFailureCode.GeomShapeLB, xFromPlc.PieceCode, str)
    '            listRes.Add(TestResult.Code & ": " & TestResult.Description)
    '        End If


    '    End If


    '    'Photometric
    '    If myRes.PhotomTest.Ok Then
    '    Else
    '        If SetResultPhotomToPlc(myRes.PhotomTest.LowBeam, Test.enumFailureCode.PhotomLowBeamEmax) Then Return
    '        If SetResultPhotomToPlc(myRes.PhotomTest.HighBeam, Test.enumFailureCode.PhotomHighBeamEmax) Then Return
    '    End If

    '    'LEDs
    '    If myRes.LEDsTest.Ok Then
    '    Else
    '        If myRes.LEDsTest.ParkLight.TestDone And Not myRes.LEDsTest.ParkLight.Ok Then
    '            xToPlc.TestResult = xToPlc.SetTestResults(Test.enumFailureCode.LEDsParkLight, xFromPlc.PieceCode, myRes.LEDsTest.ParkLight.ValueStr)
    '            listRes.Add(TestResult.Code & ": " & TestResult.Description)
    '        End If
    '        If myRes.LEDsTest.TurnLight.TestDone And Not myRes.LEDsTest.TurnLight.Ok Then
    '            TestResult = xToPlc.SetTestResults(Test.enumFailureCode.LEDsTurnLight, xFromPlc.PieceCode, myRes.LEDsTest.TurnLight.ValueStr)
    '            listRes.Add(TestResult.Code & ": " & TestResult.Description)
    '        End If
    '        If myRes.LEDsTest.DayLight.TestDone And Not myRes.LEDsTest.DayLight.Ok Then
    '            TestResult = xToPlc.SetTestResults(Test.enumFailureCode.LEDsDayLight, xFromPlc.PieceCode, myRes.LEDsTest.DayLight.ValueStr)
    '            listRes.Add(TestResult.Code & ": " & TestResult.Description)
    '        End If
    '        If myRes.LEDsTest.SideMarker.TestDone And Not myRes.LEDsTest.SideMarker.Ok Then
    '            TestResult = xToPlc.SetTestResults(Test.enumFailureCode.LEDsSideMarker, xFromPlc.PieceCode, myRes.LEDsTest.SideMarker.ValueStr)
    '            listRes.Add(TestResult.Code & ": " & TestResult.Description)
    '        End If
    '        If myRes.LEDsTest.ComingHome.TestDone And Not myRes.LEDsTest.ComingHome.Ok Then
    '            TestResult = xToPlc.SetTestResults(Test.enumFailureCode.LEDsComingHome, xFromPlc.PieceCode, myRes.LEDsTest.ComingHome.ValueStr)
    '            listRes.Add(TestResult.Code & ": " & TestResult.Description)
    '        End If
    '    End If

    '    'ECUs
    '    If myRes.ECUsTest.TestDone Then
    '        If myRes.ECUsTest.HLI.TestDone And Not myRes.ECUsTest.HLI.Ok Then
    '            TestResult = xToPlc.SetTestResults(Test.enumFailureCode.ECUsHLI, xFromPlc.PieceCode, myRes.ECUsTest.HLI.ValueStr)
    '            listRes.Add(TestResult.Code & ": " & TestResult.Description)
    '        End If
    '        If myRes.ECUsTest.LCU.TestDone And Not myRes.ECUsTest.LCU.Ok Then
    '            TestResult = xToPlc.SetTestResults(Test.enumFailureCode.ECUsLCU, xFromPlc.PieceCode, myRes.ECUsTest.LCU.ValueStr)
    '            listRes.Add(TestResult.Code & ": " & TestResult.Description)
    '        End If
    '        If myRes.ECUsTest.TestDone And Not myRes.ECUsTest.Ok Then
    '            TestResult = xToPlc.SetTestResults(Test.enumFailureCode.ECUs, xFromPlc.PieceCode, myRes.ECUsTest.Message)
    '            listRes.Add(TestResult.Code & ": " & TestResult.Description)
    '        End If
    '    End If





    'End Function

#End Region


#Region "IsWorkable"
    Private Function IsWorkableReqToHostByFile(ByVal PieceCode As String) As Result
        Dim mResult As New Result

        Dim sFileName As String
        Dim Folder As String

        Folder = SharedFolder
        If Not Folder.EndsWith("\") Then
            Folder += "\"
        End If
        My.Computer.FileSystem.CreateDirectory(Folder)

        'Delete any previuos answer
        sFileName = Folder & "IsWorkableAnswer.txt"
        If My.Computer.FileSystem.FileExists(sFileName) Then
            My.Computer.FileSystem.DeleteFile(sFileName)
        End If

        'Send request
        sFileName = Folder & "IsWorkableRequest.txt"
        SyncLock genSync
            Try
                Dim fs As New FileStream(sFileName, FileMode.Create, FileAccess.Write, FileShare.None)
                Dim s As New StreamWriter(fs)
                s.WriteLine(PieceCode)
                s.Close()
                fs.Close()
            Catch ex As Exception
            End Try
        End SyncLock


        'Waiting answer
        sFileName = Folder & "IsWorkableAnswer.txt"
        Dim crono As New Stopwatch
        crono.Restart()
        Do
            If My.Computer.FileSystem.FileExists(sFileName) Then
                Exit Do
            End If
            If crono.ElapsedMilliseconds > AnswerTimeout Then
                mResult.Status = enumResults.NoAnswer
                Return mResult
            End If
            Threading.Thread.Sleep(20)
        Loop
        If IsFileAvailable(sFileName, AnswerTimeout) Then
            SyncLock genSync
                Try
                    Dim fs As New System.IO.FileStream(sFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
                    Dim s As New System.IO.StreamReader(fs)
                    Dim sRiga As String
                    sRiga = s.ReadLine
                    s.Close()
                    fs.Close()
                    Dim aryStr() As String = sRiga.Split(CharSepar)
                    If aryStr(1).ToUpper = "YES" Then
                        mResult.Status = enumResults.Workable
                        mResult.NotWorkabelCode = aryStr(2)
                        mResult.NotWorkabelMsg = aryStr(3)
                        Return mResult
                    Else
                        mResult.Status = enumResults.NotWorkable
                        mResult.NotWorkabelCode = aryStr(2)
                        mResult.NotWorkabelMsg = aryStr(3)
                        Return mResult
                    End If
                Catch ex As Exception
                    mResult.Status = enumResults.FileError
                    mResult.NotWorkabelMsg = ex.Message
                    Return mResult
                End Try
            End SyncLock

        Else
            mResult.Status = enumResults.NoAnswer
            Return mResult
        End If


    End Function

    Private Function IsWorkableReqToHostBySQL(ByVal PieceCode As String) As Result
        Dim mResult As New Result

        Dim _dataset As New DataSet()
        Dim da As New SqlDataAdapter()
        Dim cmd As SqlCommand
        Dim cmdBuilder As SqlCommandBuilder

        Try
            Dim dbConnection As New SqlConnection(ConnectionToDatabase)

            cmd = New SqlCommand("SELECT  * FROM " & dbTableName, dbConnection)
            da.SelectCommand = cmd
            _dataset.Clear()
            da.FillSchema(_dataset, SchemaType.Source, dbTableName)
            da.Fill(_dataset, dbTableName)
            If _dataset.Tables(0).Rows.Count = 0 Then
                'Add a row
                Dim riga As DataRow
                riga = _dataset.Tables(0).NewRow()
                riga("ID") = 1
                riga("IsWorkableReq") = False
                riga("PieceCode") = ""
                riga("IsWorkableYes") = False
                riga("IsWorkableNo") = False
                riga("ToBeRepaired") = False
                _dataset.Tables(0).Rows.Add(riga)
            Else
                'cmd = New SqlCommand("SELECT TOP 1 * FROM " & dbTableName, dbConnection)
                'da.SelectCommand = cmd
                '_dataset.Clear()
                'da.Fill(_dataset, dbTableName)
            End If

            'Set request
            _dataset.Tables(0).Rows(0)("IsWorkableReq") = True
            _dataset.Tables(0).Rows(0)("PieceCode") = PieceCode
            _dataset.Tables(0).Rows(0)("IsWorkableYes") = False
            _dataset.Tables(0).Rows(0)("IsWorkableNo") = False
            _dataset.Tables(0).Rows(0)("ToBeRepaired") = False
            _dataset.Tables(0).Rows(0)("IsMasterGood") = False
            _dataset.Tables(0).Rows(0)("IsMasterFail") = False
            cmdBuilder = New SqlCommandBuilder(da)
            da.Update(_dataset, dbTableName)


            'Waiting for Host ACK
            Dim crono As New Stopwatch
            crono.Restart()
            Do
                cmd = New SqlCommand("SELECT TOP 1 * FROM " & dbTableName, dbConnection)
                da.SelectCommand = cmd
                _dataset.Clear()
                da.Fill(_dataset, dbTableName)

                If _dataset.Tables(0).Rows(0)("IsWorkableYes") Then
                    mResult.Status = enumResults.Workable
                    If _dataset.Tables(0).Rows(0)("IsMasterGood") Then
                        mResult.MasterGood = True
                    Else
                        If _dataset.Tables(0).Rows(0)("IsMasterFail") Then
                            mResult.MasterFail = True
                        Else
                        End If
                    End If
                    Exit Do
                End If
                If _dataset.Tables(0).Rows(0)("IsWorkableNo") Then
                    mResult.Status = enumResults.NotWorkable
                    Exit Do
                End If

                If crono.ElapsedMilliseconds > 5000 Then
                    mResult.Status = enumResults.NoAnswer
                    Exit Do
                End If
            Loop

            'Reset request
            _dataset.Tables(0).Rows(0)("IsWorkableReq") = False
            _dataset.Tables(0).Rows(0)("PieceCode") = ""
            cmdBuilder = New SqlCommandBuilder(da)
            da.Update(_dataset, dbTableName)

            dbConnection.Close()

        Catch ex As Exception
            MsgBox("Pippo")
            mResult.Status = enumResults.NoAnswer

        End Try

        Return mResult



    End Function

    Private Function IsFileAvailable(ByVal sFilename As String, ByVal TimeOut As Long) As Boolean

        Dim crono As New Stopwatch
        crono.Restart()
        Do
            Try
                Dim fs As New System.IO.FileStream(sFilename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
                fs.Close()
                Return True
            Catch ex As Exception
            End Try
            If crono.ElapsedMilliseconds > TimeOut Then
                Return False
            End If
            Threading.Thread.Sleep(20)
        Loop

    End Function

#End Region

#Region "WorkDone"

    Public Function SetWorkDoneToHostByFile(ByVal PieceCode As String, ByVal ResultOk As Boolean, ByVal ParamArray param() As String) As Boolean
        Dim sFileName As String
        Dim sRow As String

        sFileName = SharedFolder
        If Not sFileName.EndsWith("\") Then
            sFileName += "\"
        End If
        My.Computer.FileSystem.CreateDirectory(sFileName)

        sFileName += PieceCode & "_WorkDone.txt"

        sRow = PieceCode & CharSepar & IIf(ResultOk, "True", "False") & CharSepar
        For i As Integer = 0 To UBound(param)
            sRow += param(i) + CharSepar
        Next

        SyncLock genSync
            Try
                Dim fs As New FileStream(sFileName, FileMode.Create, FileAccess.Write, FileShare.None)
                Dim s As New StreamWriter(fs)
                s.WriteLine(sRow)
                s.Close()
                fs.Close()
            Catch ex As Exception

            End Try
        End SyncLock


        ''Waiting for Host ACK
        'If My.Computer.FileSystem.FileExists(sFileName) Then
        '    Dim crono As New Stopwatch
        '    crono.Restart()
        '    Do
        '        If Not My.Computer.FileSystem.FileExists(sFileName) Then
        '            Exit Do
        '        End If
        '        If crono.ElapsedMilliseconds > AnswerTimeout Then
        '            Return False
        '        End If
        '        Threading.Thread.Sleep(20)
        '    Loop
        'End If


        Return True

    End Function

    Private Function SetWorkDoneToHostBySql(ByVal PieceCode As String, ByVal ResultOk As Boolean, ByVal ParamArray param() As String) As Boolean
        Dim bRes As Boolean = False
        Dim _dataset As New DataSet()
        Dim da As New SqlDataAdapter()
        Dim cmd As SqlCommand
        Dim cmdBuilder As SqlCommandBuilder

        ' Dim dbTableName As String = "TraMaInterface"

        Try
            Dim dbConnection As New SqlConnection(ConnectionToDatabase)

            cmd = New SqlCommand("SELECT  * FROM " & dbTableName, dbConnection)
            da.SelectCommand = cmd
            _dataset.Clear()
            da.FillSchema(_dataset, SchemaType.Source, dbTableName)
            da.Fill(_dataset, dbTableName)
            If _dataset.Tables(0).Rows.Count = 0 Then
                'Add a row
                Dim riga As DataRow
                riga = _dataset.Tables(0).NewRow()
                riga("ID") = 1
                riga("TestDone") = False
                riga("TestPassed") = False
                riga("TestFailed") = False
                _dataset.Tables(0).Rows.Add(riga)
            Else
                'cmd = New SqlCommand("SELECT TOP 1 * FROM " & dbTableName, dbConnection)
                'da.SelectCommand = cmd
                '_dataset.Clear()
                'da.Fill(_dataset, dbTableName)
            End If

            'Set request
            _dataset.Tables(0).Rows(0)("TestDone") = True
            _dataset.Tables(0).Rows(0)("TestPassed") = ResultOk
            _dataset.Tables(0).Rows(0)("TestFailed") = Not ResultOk
            _dataset.Tables(0).Rows(0)("PieceCode") = PieceCode
            _dataset.Tables(0).Rows(0)("HostAck") = False

            ''Params
            '_dataset.Tables(0).Rows(0)("AbsortionVccDone") = Result.AbsortionTest.Vcc.TestDone
            '_dataset.Tables(0).Rows(0)("AbsortionVccOk") = Result.AbsortionTest.Vcc.Ok
            '_dataset.Tables(0).Rows(0)("AbsortionVccMin") = RunningModel.ElectParams.Absortion.VoltageSupply.Min
            '_dataset.Tables(0).Rows(0)("AbsortionVccValue") = Result.AbsortionTest.Vcc.Value
            '_dataset.Tables(0).Rows(0)("AbsortionVccMax") = RunningModel.ElectParams.Absortion.VoltageSupply.Max



            'Write to DB
            cmdBuilder = New SqlCommandBuilder(da)
            da.Update(_dataset, dbTableName)


            'Waiting for Host ACK
            Dim crono As New Stopwatch
            crono.Restart()
            Do
                cmd = New SqlCommand("SELECT TOP 1 * FROM " & dbTableName, dbConnection)
                da.SelectCommand = cmd
                _dataset.Clear()
                da.Fill(_dataset, dbTableName)

                If _dataset.Tables(0).Rows(0)("HostAck") Then
                    bRes = True
                    Exit Do
                End If

                If crono.ElapsedMilliseconds > 5000 Then
                    bRes = False
                    Exit Do
                End If
            Loop

            'Reset request
            _dataset.Tables(0).Rows(0)("TestDone") = False
            _dataset.Tables(0).Rows(0)("TestPassed") = False
            _dataset.Tables(0).Rows(0)("TestFailed") = False
            _dataset.Tables(0).Rows(0)("PieceCode") = ""
            _dataset.Tables(0).Rows(0)("HostAck") = False
            cmdBuilder = New SqlCommandBuilder(da)
            da.Update(_dataset, dbTableName)

            dbConnection.Close()

        Catch ex As Exception
            MsgBox("Pippo")

        End Try

        Return bRes

    End Function

#End Region

#Region "ChangeParam"

    Private Function ChangeParamToHostByFile(ByVal UserName As String, ByVal ParamArray param() As String) As Boolean
        Dim sFileName As String
        Dim sRow As String

        If Not Enabled Then
            Return True
        End If


        sFileName = SharedFolder
        If Not sFileName.EndsWith("\") Then
            sFileName += "\"
        End If
        My.Computer.FileSystem.CreateDirectory(sFileName)

        sFileName += "ParamChanged.txt"

        sRow = UserName & CharSepar
        For i As Integer = 0 To UBound(param)
            sRow += param(i) + CharSepar
        Next

        SyncLock genSync
            Try
                Dim fs As New FileStream(sFileName, FileMode.Create, FileAccess.Write, FileShare.None)
                Dim s As New StreamWriter(fs)
                s.WriteLine(sRow)
                s.Close()
                fs.Close()
            Catch ex As Exception

            End Try
        End SyncLock


        ''Waiting for Host ACK
        'If My.Computer.FileSystem.FileExists(sFileName) Then
        '    Dim crono As New Stopwatch
        '    crono.Restart()
        '    Do
        '        If Not My.Computer.FileSystem.FileExists(sFileName) Then
        '            Exit Do
        '        End If
        '        If crono.ElapsedMilliseconds > AnswerTimeout Then
        '            Return False
        '        End If
        '        Threading.Thread.Sleep(20)
        '    Loop
        'End If


        Return True

    End Function

    Private Function ChangeParamToHostBySql(ByVal UserName As String, ByVal ParamArray param() As String) As Boolean
        Dim bRes As Boolean = False
        Dim _dataset As New DataSet()
        Dim da As New SqlDataAdapter()
        Dim cmd As SqlCommand
        Dim cmdBuilder As SqlCommandBuilder

        ' Dim dbTableName As String = "TraMaInterface"

        Try
            Dim dbConnection As New SqlConnection(ConnectionToDatabase)

            cmd = New SqlCommand("SELECT  * FROM " & dbTableName, dbConnection)
            da.SelectCommand = cmd
            _dataset.Clear()
            da.FillSchema(_dataset, SchemaType.Source, dbTableName)
            da.Fill(_dataset, dbTableName)
            If _dataset.Tables(0).Rows.Count = 0 Then
                'Add a row
                Dim riga As DataRow
                riga = _dataset.Tables(0).NewRow()
                riga("ID") = 1
                riga("ParamsChanged") = False
                riga("UserName") = ""
                riga("Params") = ""
                riga("HostAck") = False
                _dataset.Tables(0).Rows.Add(riga)
            Else
                'cmd = New SqlCommand("SELECT TOP 1 * FROM " & dbTableName, dbConnection)
                'da.SelectCommand = cmd
                '_dataset.Clear()
                'da.Fill(_dataset, dbTableName)
            End If

            'Set request
            _dataset.Tables(0).Rows(0)("ParamsChanged") = True
            _dataset.Tables(0).Rows(0)("UserName") = UserName
            _dataset.Tables(0).Rows(0)("Params") = ""
            For i As Integer = 0 To UBound(param)
                _dataset.Tables(0).Rows(0)("Params") += param(i) + CharSepar
            Next
            _dataset.Tables(0).Rows(0)("HostAck") = False
            cmdBuilder = New SqlCommandBuilder(da)
            da.Update(_dataset, dbTableName)


            'Waiting for Host ACK
            Dim crono As New Stopwatch
            crono.Restart()
            Do
                cmd = New SqlCommand("SELECT TOP 1 * FROM " & dbTableName, dbConnection)
                da.SelectCommand = cmd
                _dataset.Clear()
                da.Fill(_dataset, dbTableName)

                If _dataset.Tables(0).Rows(0)("HostAck") Then
                    bRes = True
                    Exit Do
                End If

                If crono.ElapsedMilliseconds > 5000 Then
                    bRes = False
                    Exit Do
                End If
            Loop

            'Reset reques
            _dataset.Tables(0).Rows(0)("ParamsChanged") = False
            _dataset.Tables(0).Rows(0)("UserName") = ""
            _dataset.Tables(0).Rows(0)("Params") = ""
            _dataset.Tables(0).Rows(0)("HostAck") = False
            cmdBuilder = New SqlCommandBuilder(da)
            da.Update(_dataset, dbTableName)

            dbConnection.Close()

        Catch ex As Exception
            MsgBox("Pippo")

        End Try

        Return bRes


    End Function

#End Region

#Region "Status"
    Public Function SetStatusToHostByFile(ByVal Status As Integer, ByVal WorkerId As Integer, ByVal bInit As Boolean) As Boolean
        Dim sFileName As String
        Dim sRow As String

        sFileName = SharedFolder
        If Not sFileName.EndsWith("\") Then
            sFileName += "\"
        End If
        My.Computer.FileSystem.CreateDirectory(sFileName)

        sFileName += "Status.txt"

        If bInit Then
            If My.Computer.FileSystem.FileExists(sFileName) Then
                My.Computer.FileSystem.DeleteFile(sFileName)
            End If
        End If

        If My.Computer.FileSystem.FileExists(sFileName) Then Return False

        sRow = Status.ToString & CharSepar & WorkerId.ToString & CharSepar

        SyncLock genSync
            Try
                Dim fs As New FileStream(sFileName, FileMode.Create, FileAccess.Write, FileShare.ReadWrite)
                Dim s As New StreamWriter(fs)
                s.WriteLine(sRow)
                s.Close()
                fs.Close()
            Catch ex As Exception

            End Try
        End SyncLock

        Return True

    End Function

    Private Function SetStatusToHostBySql(ByVal Status As Integer, ByVal WorkerId As Integer) As Boolean
        Dim bRes As Boolean = False
        Dim _dataset As New DataSet()
        Dim da As New SqlDataAdapter()
        Dim cmd As SqlCommand
        Dim cmdBuilder As SqlCommandBuilder

        ' Dim dbTableName As String = "TraMaInterface"

        Try
            Dim dbConnection As New SqlConnection(ConnectionToDatabase)

            cmd = New SqlCommand("SELECT  * FROM " & dbTableName, dbConnection)
            da.SelectCommand = cmd
            _dataset.Clear()
            da.FillSchema(_dataset, SchemaType.Source, dbTableName)
            da.Fill(_dataset, dbTableName)
            If _dataset.Tables(0).Rows.Count = 0 Then
                'Add a row
                Dim riga As DataRow
                riga = _dataset.Tables(0).NewRow()
                riga("ID") = 1
                riga("Status") = 0
                riga("WorkerId") = ""
                _dataset.Tables(0).Rows.Add(riga)
            Else
                'cmd = New SqlCommand("SELECT TOP 1 * FROM " & dbTableName, dbConnection)
                'da.SelectCommand = cmd
                '_dataset.Clear()
                'da.Fill(_dataset, dbTableName)
            End If

            'Set request
            _dataset.Tables(0).Rows(0)("Status") = Status
            _dataset.Tables(0).Rows(0)("WorkerId") = WorkerId


            'Write to DB
            cmdBuilder = New SqlCommandBuilder(da)
            da.Update(_dataset, dbTableName)


            dbConnection.Close()

        Catch ex As Exception
            MsgBox("Pippo")

        End Try

        Return bRes

    End Function

#End Region

End Class
