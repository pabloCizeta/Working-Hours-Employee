Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Data.Odbc

Imports System.Management
Imports System

'Imports System.IO
Public Class czDatabase
    Private sMyResult As String
    Private myConnectionStr As String

    ReadOnly Property Status() As String
        Get
            Return sMyResult
        End Get
    End Property

    Public Sub New()
        sMyResult = ""
        myConnectionStr = "Data source=.\SQLExpress;Integrated Security=SSPI;User Instance=True"
    End Sub

    Public Sub New(ByVal ConnectionString As String)
        sMyResult = ""
        myConnectionStr = ConnectionString
    End Sub

    Public Function CheckSqlService(ByVal SqlServiceName As String) As Boolean
        Dim iRetries As Integer = 0

        'For Each service As ServiceController In ServiceController.GetServices()
        '    Dim serviceName As String = service.ServiceName
        '    Dim serviceDisplayName As String = service.DisplayName
        '    Dim serviceType As String = service.ServiceType.ToString()
        '    Dim sta As String = service.Status.ToString()
        '    'ListBox1.Items.Add(serviceName + "  " + serviceDisplayName +
        '    '    serviceType + " " + status)

        '    If serviceName.Contains("SQL") Then
        '        serviceName = serviceName
        '    End If
        'Next

        'Check sql service status
        Dim myServiceName As String = SqlServiceName   '"MSSQL&MSSQLEXPRESS" 'service name of SQL Server Express
        Dim status As String  'service status (For example, Running or Stopped)
        Dim mySC As System.ServiceProcess.ServiceController
        mySC = New System.ServiceProcess.ServiceController(myServiceName)
        Try
            status = mySC.Status.ToString
        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached Then
                Dim sMsg As String = "SQLEXPRESS: Service not found. It is probably not installed. [exception=" & ex.Message & "]"
                MsgBox(sMsg, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
            End If
            sMyResult = ex.Message
            Return False
        End Try

        'If mySC.Status.Equals(System.ServiceProcess.ServiceControllerStatus.Running) Then
        '    mySC.Stop()
        '    mySC.WaitForStatus(System.ServiceProcess.ServiceControllerStatus.Stopped)
        'End If


        'if service is Stopped or StopPending, you can run it with the following code.
        If mySC.Status.Equals(System.ServiceProcess.ServiceControllerStatus.Stopped) Or mySC.Status.Equals(System.ServiceProcess.ServiceControllerStatus.StopPending) Then
            Try
                mySC.Start()
                mySC.WaitForStatus(System.ServiceProcess.ServiceControllerStatus.Running)
            Catch ex As Exception
                If System.Diagnostics.Debugger.IsAttached Then
                    Dim sMsg As String = "SQLEXPRESS: Error on starting the service: " & ex.Message
                    MsgBox(sMsg, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
                End If
                sMyResult = ex.Message
                Return False
            End Try
        End If

        Return True
    End Function

    Public Function CheckConnection(ByVal ConnectionString As String) As Boolean
        Dim iRetries As Integer = 0

        Do
            Try
                Dim dbConnection As New SqlConnection(ConnectionString)
                dbConnection.Open()
                dbConnection.Close()
                sMyResult = "Done"
                Return True
            Catch ex As Exception
                sMyResult = ex.Message
                iRetries += 1
                If iRetries > 0 Then Return False
            End Try

            Threading.Thread.Sleep(100)

        Loop

        Return False

        '        Click Browse, locate the SMO assemblies in the C:\Program Files\Microsoft SQL Server\110\SDK\Assemblies folder, and then select the following files. These are the minimum files that are required to build an SMO application:
        '        Microsoft.SqlServer.ConnectionInfo.dll()
        '        Microsoft.SqlServer.SqlEnum.dll()
        '        Microsoft.SqlServer.Smo.dll()
        '        Microsoft.SqlServer.Management.Sdk.Sfc()
        '        Note()
        'Use the Ctrl key to select more than one file.
        'Add any additional SMO assemblies that are required. For example, if you are specifically programming Service Broker, add the following assemblies:
        '        Microsoft.SqlServer.ServiceBrokerEmum.dll()
        'Click Open.
        'On the View menu, click Code.-Or-Select the Module1.vb window to show the code window.
        'In the code, before any declarations, type the following Imports statements to qualify the types in the SMO namespace.
        'Imports Microsoft.SqlServer.Management.Smo
        'Imports Microsoft.SqlServer.Management.Common

        'Dim server As New Microsoft.SqlServer.Management.Smo.Server("localhost")
        'For Each db As Database In server.Databases
        '    Console.WriteLine(db.Name)
        'Next

    End Function


    Public Function CreateDatabase(ByVal sDatabaseFileName As String, ByVal DatabaseName As String) As Boolean
        'Dim dbConnection As SqlConnection()
        'Dim sConnection As String
        Dim sqlStatement As String
        Dim cmdSql As New SqlCommand()

        Dim iPos As Int32
        sDatabaseFileName = sDatabaseFileName
        iPos = InStr(sDatabaseFileName, ".")
        If iPos > 0 Then
            sDatabaseFileName = Mid(sDatabaseFileName, 1, iPos - 1)
        End If

        Dim dbConnection As New SqlConnection()

        ' Eseguire un tentativo di connessione all'istanza di SQL Server.  
        Try
            ' La classe SqlConnection consente di comunicare con SQL Server.
            ' Il costruttore accetta una stringa di connessione come argomento. Questa
            ' stringa di connessione utilizza la protezione integrata, per cui 
            ' è necessario disporre di un account di accesso a SQL Server oppure 
            ' appartenere al gruppo Administrators
            ' per garantire un funzionamento corretto.
            '            sConnection = "Data source=.\SQLExpress;Integrated Security=SSPI;User Instance=True"
            dbConnection.ConnectionString = myConnectionStr

            If Not My.Computer.FileSystem.FileExists(sDatabaseFileName & ".mdf") Then
                'Se il file non esiste cancello l'eventuale database nel server SQL
                sqlStatement = _
                    "IF EXISTS (" & _
                    "SELECT * " & _
                    "FROM master..sysdatabases " & _
                    "WHERE Name = '" & DatabaseName & "')" & vbCrLf & _
                    "DROP DATABASE " & DatabaseName

                cmdSql.Connection = dbConnection
                cmdSql.CommandText = sqlStatement
                dbConnection.Open()
                cmdSql.ExecuteNonQuery()
                dbConnection.Close()
            End If

        Catch sqlExc As SqlException
            sMyResult = sqlExc.Message
        Catch exc As Exception
            ' Impossibile connettersi a SQL Server o MSDE
            'MsgBox(exc.Message, MsgBoxStyle.OkOnly, "Connection failed.")
            dbConnection.Close()
            sMyResult = exc.Message
            Return False
        End Try
        dbConnection.Close()

        Try
            ' La classe SqlConnection consente di comunicare con SQL Server.
            ' Il costruttore accetta una stringa di connessione come argomento. Questa
            ' stringa di connessione utilizza la protezione integrata, per cui 
            ' è necessario disporre di un account di accesso a SQL Server oppure 
            ' appartenere al gruppo Administrators
            ' per garantire un funzionamento corretto.
            'sConnection = "Data source=.\SQLExpress;Integrated Security=SSPI;User Instance=True"
            dbConnection.ConnectionString = myConnectionStr

            If Not My.Computer.FileSystem.FileExists(sDatabaseFileName & ".mdf") Then
                'Creo il database sul nuovo file
                sqlStatement = _
                    "IF NOT EXISTS (" & _
                    "SELECT * " & _
                    "FROM master..sysdatabases " & _
                    "WHERE Name = '" & DatabaseName & "')" & vbCrLf & _
                    "CREATE DATABASE " & DatabaseName & " " & _
                    "ON PRIMARY (NAME = " & DatabaseName & "_Data, " & _
                    "FILENAME = '" & sDatabaseFileName & ".mdf', " & _
                    "SIZE = 4MB, MAXSIZE = 10MB, FILEGROWTH = 10%) " & _
                    "LOG ON (NAME = " & DatabaseName & "_Log, " & _
                    "FILENAME = '" & sDatabaseFileName & "_log.ldf', " & _
                    "SIZE = 4MB, MAXSIZE = 10MB, FILEGROWTH = 10%) "

                cmdSql.Connection = dbConnection
                cmdSql.CommandText = sqlStatement
                dbConnection.Open()
                cmdSql.ExecuteNonQuery()
                dbConnection.Close()

                'Dim builder As New SqlConnectionStringBuilder(sConnection)
                'builder.AttachDBFilename = sDatabaseFileName & ".mdf"
                'sConnection = builder.ConnectionString
            Else
                'sConnection = sConnection & "; AttachDBFilename =" & sDatabaseFileName & ".mdf"
            End If
            sMyResult = "Done"
            Return True

        Catch sqlExc As SqlException
            'statusForm.Close()
            'MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly, "SQL Exception Error")
            sMyResult = sqlExc.Message
        Catch exc As Exception
            ' Impossibile connettersi a SQL Server o MSDE
            'MsgBox(exc.Message, MsgBoxStyle.OkOnly, "Connection failed.")
            sMyResult = exc.Message
        End Try

        dbConnection.Close()
        Return False

    End Function

    Public Function DropAndCreateTable(ByRef dbConnection As SqlConnection, ByVal sTableName As String, ByVal sFieldName As String, ByVal sFieldType As String) As Boolean
        'Crea una tabella con almeno un campo.
        Dim sqlStatement As String
        Dim cmdSql As New SqlCommand()

        Try
            Dim TableList As DataTable
            TableList = dbConnection.GetSchema(System.Data.SqlClient.SqlClientMetaDataCollectionNames.Tables)
            If DoTableExist(sTableName, TableList) Then
                'Cancella tabella
                sqlStatement = "DROP TABLE " & sTableName
                cmdSql.Connection = dbConnection
                cmdSql.CommandText = sqlStatement
                cmdSql.ExecuteNonQuery()
            End If

            'Creare tabella
            sqlStatement = _
            "CREATE TABLE " & sTableName & " (" & sFieldName & " " & sFieldType & ")"
            cmdSql.Connection = dbConnection
            cmdSql.CommandText = sqlStatement
            cmdSql.ExecuteNonQuery()

            sMyResult = "Done"
            Return True


        Catch sqlExc As SqlException
            'MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly, "SQL Exception Error")
            sMyResult = sqlExc.Message
            Return False
        End Try

    End Function

    Public Function TableExist(ByRef dbConnection As SqlConnection, ByVal sTableName As String) As Boolean

        Try
            Dim TableList As DataTable
            TableList = dbConnection.GetSchema(System.Data.SqlClient.SqlClientMetaDataCollectionNames.Tables)
            If DoTableExist(sTableName, TableList) Then
                Return True
            Else
                Return False
            End If

        Catch sqlExc As SqlException
            'MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly, "SQL Exception Error")
            sMyResult = sqlExc.Message
            Return False
        End Try

    End Function

    Public Function CreateTable(ByRef dbConnection As SqlConnection, ByVal sTableName As String, ByVal sFieldName As String, ByVal sFieldType As String) As Boolean
        'Crea una tabella con almeno un campo.
        Dim sqlStatement As String
        Dim cmdSql As New SqlCommand()

        Try
            Dim TableList As DataTable
            TableList = dbConnection.GetSchema(System.Data.SqlClient.SqlClientMetaDataCollectionNames.Tables)
            'DisplayData(TableList)
            If DoTableExist(sTableName, TableList) Then
                ''Cancella tabella
                'sqlStatement = "DROP TABLE " & sTableName
                'cmdSql.Connection = dbConnection
                'cmdSql.CommandText = sqlStatement
                'cmdSql.ExecuteNonQuery()
            Else
                'Creare tabella
                sqlStatement = _
                "CREATE TABLE " & sTableName & " (" & sFieldName & " " & sFieldType & ")"
                cmdSql.Connection = dbConnection
                cmdSql.CommandText = sqlStatement
                cmdSql.ExecuteNonQuery()
            End If
            sMyResult = "Done"
            Return True


        Catch sqlExc As SqlException
            'MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly, "SQL Exception Error")
            sMyResult = sqlExc.Message
            Return False
        End Try

    End Function

    Public Function CreateField(ByRef dbConnection As SqlConnection, ByVal sTableName As String, ByVal sFieldName As String, ByVal sFieldType As String) As Boolean
        'Crea un campo su una tabella. Se la tabella non esiste la crea.
        Dim sqlStatement As String
        Dim cmdSql As New SqlCommand()

        Try
            'Dim TableList As DataTable
            'TableList = dbConnection.GetSchema(System.Data.SqlClient.SqlClientMetaDataCollectionNames.Tables)
            ''DisplayData(TableList)
            'If DoTableExist(sTableName, TableList) Then
            '    ''Cancella tabella
            '    'sqlStatement = "DROP TABLE " & sTableName
            '    'cmdSql.Connection = dbConnection
            '    'cmdSql.CommandText = sqlStatement
            '    'cmdSql.ExecuteNonQuery()
            'Else
            '    'Creare tabella
            '    sqlStatement = _
            '    "CREATE TABLE " & sTableName & " (" & sFieldName & " " & sFieldType & ")"
            '    cmdSql.Connection = dbConnection
            '    cmdSql.CommandText = sqlStatement
            '    cmdSql.ExecuteNonQuery()
            'End If

            Dim FieldList As DataTable
            FieldList = dbConnection.GetSchema(System.Data.SqlClient.SqlClientMetaDataCollectionNames.Columns)
            'DisplayData(FieldList)
            If DoFieldExist(sFieldName, sTableName, FieldList) Then
                ''Cancello campo
                'sqlStatement = "ALTER TABLE " & sTableName & " DROP COLUMN " & sFieldName
                'cmdSql.Connection = dbConnection
                'cmdSql.CommandText = sqlStatement
                'cmdSql.ExecuteNonQuery()
                ''Aggiungo campo
                'sqlStatement = "ALTER TABLE " & sTableName & " ADD " & _
                'sFieldName & " " & sFieldType
                'cmdSql.Connection = dbConnection
                'cmdSql.CommandText = sqlStatement
                'cmdSql.ExecuteNonQuery()
                ' ''Modifico campo- non funziona su primary key - provare con CONSTRAINT
                'Funziona su NULL-NOT NULL
                ''sqlStatement = "ALTER TABLE " & sTableName & " ALTER COLUMN  " & _
                ''sFieldName & " " & sFieldType
                ''cmdSql.Connection = dbConnection
                ''cmdSql.CommandText = sqlStatement
                ''cmdSql.ExecuteNonQuery()
            Else
                'Aggiungo campo
                sqlStatement = "ALTER TABLE " & sTableName & " ADD " & _
                sFieldName & " " & sFieldType
                cmdSql.Connection = dbConnection
                cmdSql.CommandText = sqlStatement
                cmdSql.ExecuteNonQuery()
            End If

            ''Cancello campo
            'sqlStatement = "ALTER TABLE " & sTableName & " DROP COLUMN Gruppo"
            'cmdSql.Connection = dbConnection
            'cmdSql.CommandText = sqlStatement
            'cmdSql.ExecuteNonQuery()

            sMyResult = "Done"
            Return True

        Catch sqlExc As SqlException
            'MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly, "SQL Exception Error")
            sMyResult = sqlExc.Message
            Return False
        End Try

    End Function

    Public Function CreateIndex(ByRef dbConnection As SqlConnection, IndexName As String, ByVal sTableName As String, ByVal sIndexFieldName As String, ByVal IncludeFields As List(Of String)) As Boolean
        Dim cmdSql As New SqlCommand()

        'Check if idex already exists
        Dim str1 As String = "IF EXISTS (SELECT 'foo'  FROM sys.indexes WHERE object_id = OBJECT_ID('"  'dbo.ModelsList') AND name='IdxID') SELECT 1 ELSE SELECT 0"
        str1 += sTableName & "') AND name='" & IndexName & "') SELECT 1 ELSE SELECT 0"
        Dim cmd1 As New SqlCommand(str1, dbConnection)
        Dim exist As Integer = Convert.ToInt32(cmd1.ExecuteScalar())
        If exist Then Return True

        Dim sqlq As String = String.Format("CREATE NONCLUSTERED INDEX [{0}] ON [dbo].[{1}] ([{2}])",
                                             IndexName, sTableName, sIndexFieldName)
        If IncludeFields.Count > 0 Then
            sqlq = sqlq & " INCLUDE ("
            For Each str As String In IncludeFields
                sqlq = sqlq & "[" & str & "],"
            Next
            sqlq = sqlq.Remove(sqlq.Length - 1)
            sqlq = sqlq & ")"
        End If

        Try
            Dim cmd As New SqlCommand(sqlq, dbConnection)
            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try


    End Function
    Public Function CreateIndex(ByRef dbConnection As SqlConnection, IndexName As String, ByVal sTableName As String, ByVal sIndexFieldName As String) As Boolean

        Dim IncludeFields As New List(Of String)
        Dim cmdFieldList As New SqlCommand("SELECT * FROM " & sTableName, dbConnection)
        Dim myReader As SqlDataReader
        myReader = cmdFieldList.ExecuteReader(CommandBehavior.KeyInfo)

        'Retrieve column schema into a DataTable.
        Dim schemaTable As DataTable
        schemaTable = myReader.GetSchemaTable()
        'For each field in the table...
        For Each myField In schemaTable.Rows
            'For each property of the field...
            For Each myProperty In schemaTable.Columns
                If myProperty.ColumnName = "ColumnName" Then
                    If myField(myProperty).ToString() <> sIndexFieldName Then
                        IncludeFields.Add(myField(myProperty).ToString())
                    End If
                End If
            Next
        Next
        myReader.Close()

        Return CreateIndex(dbConnection, IndexName, sTableName, sIndexFieldName, IncludeFields)

    End Function

    Public Function CreateEmptyTable(ByRef dbConnection As SqlConnection, ByVal sTableName As String, ByVal sFieldName As String, ByVal sFieldType As String) As Boolean
        'Crea un campo su una tabella. Se la tabella non esiste la crea.
        Dim sqlStatement As String
        Dim cmdSql As New SqlCommand()

        Try
            Dim TableList As DataTable
            TableList = dbConnection.GetSchema(System.Data.SqlClient.SqlClientMetaDataCollectionNames.Tables)
            'DisplayData(TableList)
            If DoTableExist(sTableName, TableList) Then
                'Cancella tabella
                sqlStatement = "DROP TABLE " & sTableName
                cmdSql.Connection = dbConnection
                cmdSql.CommandText = sqlStatement
                cmdSql.ExecuteNonQuery()
            End If
            'Creare tabella
            sqlStatement = _
            "CREATE TABLE " & sTableName & " (" & sFieldName & " " & sFieldType & ")"
            cmdSql.Connection = dbConnection
            cmdSql.CommandText = sqlStatement
            cmdSql.ExecuteNonQuery()

            sMyResult = "Done"
            Return True

        Catch sqlExc As SqlException
            'MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly, "SQL Exception Error")
            sMyResult = sqlExc.Message
            Return False
        End Try

    End Function

    Public Function RemoveTable(ByRef dbConnection As SqlConnection, ByVal sTableName As String) As Boolean
        'Crea una tabella con almeno un campo.
        Dim sqlStatement As String
        Dim cmdSql As New SqlCommand()

        Try
            Dim TableList As DataTable
            TableList = dbConnection.GetSchema(System.Data.SqlClient.SqlClientMetaDataCollectionNames.Tables)
            'DisplayData(TableList)
            If DoTableExist(sTableName, TableList) Then
                'Cancella tabella
                sqlStatement = "DROP TABLE " & sTableName
                cmdSql.Connection = dbConnection
                cmdSql.CommandText = sqlStatement
                cmdSql.ExecuteNonQuery()
            End If
            sMyResult = "Done"
            Return True


        Catch sqlExc As SqlException
            'MsgBox(sqlExc.Message, MsgBoxStyle.OkOnly, "SQL Exception Error")
            sMyResult = sqlExc.Message
            Return False
        End Try

    End Function



    Public Function ExecuteSqlCommand(cmd As SqlCommand) As String
        Dim retry As Byte = 0
        Dim msg As String = ""
        Do
            Try
                Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
                If rowsAffected > 0 Then Return ""
                retry += 1
            Catch ex As Exception
                retry += 1
                If retry > 5 Then
                    Throw ex
                    Return ex.Message
                End If
            End Try
            Threading.Thread.Sleep(10)
        Loop
        Return msg
    End Function


    Private Sub DisplayData(ByVal table As DataTable)
        For Each row As DataRow In table.Rows
            For Each col As DataColumn In table.Columns
                Console.WriteLine("{0} = {1}", col.ColumnName, row(col))
            Next
            Console.WriteLine("============================")
        Next
    End Sub
    Private Function DoDatabaseExist(ByVal DatabaseName As String, ByVal table As DataTable) As Boolean
        For Each row As DataRow In table.Rows
            For Each col As DataColumn In table.Columns
                If UCase(row(col).ToString) = UCase(DatabaseName) Then
                    Return True
                End If
            Next
        Next
        Return False
    End Function
    Private Function DoTableExist(ByVal TableName As String, ByVal table As DataTable) As Boolean
        For Each row As DataRow In table.Rows
            For Each col As DataColumn In table.Columns
                If UCase(row(col).ToString) = UCase(TableName) Then
                    Return True
                End If
            Next
        Next
        Return False
    End Function
    Private Function DoFieldExist(ByVal FieldName As String, ByVal TableName As String, ByVal table As DataTable) As Boolean
        Dim bTableOk As Boolean

        For Each row As DataRow In table.Rows
            For Each col As DataColumn In table.Columns

                'Console.WriteLine("{0} = {1}", col.ColumnName, row(col))

                If UCase(col.ColumnName.ToString) = "TABLE_NAME" Then
                    If UCase(row(col).ToString) = UCase(TableName) Then
                        bTableOk = True
                    Else
                        bTableOk = False
                    End If
                End If
                If bTableOk Then
                    If UCase(row(col).ToString) = UCase(FieldName) Then
                        Return True
                    End If
                End If
            Next
        Next
        Return False
    End Function

    'Private Function DoFileExist(ByVal sNomeFile As String) As Boolean

    '    Dim sr As System.IO.StreamReader
    '    Try
    '        sr = New System.IO.StreamReader(sNomeFile)
    '        sr.Close()
    '        Return True

    '    Catch ex As System.IO.FileNotFoundException
    '        Return False
    '    Catch ex As Exception
    '        Return True
    '    End Try

    'End Function


End Class
