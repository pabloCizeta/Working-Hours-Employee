
Imports System.Text
Imports System.IO

Public Class eEventLOG

    Private myLock As New Object

#Region "Proprieta"

    Private mDir As String
    Property FileFolder() As String
        Get
            Return mDir
        End Get
        Set(ByVal value As String)
            mDir = value
        End Set
    End Property

    Private mFileNamePrefix As String
    Property FileNamePrefix() As String
        Get
            Return mFileNamePrefix
        End Get
        Set(ByVal value As String)
            mFileNamePrefix = value
        End Set
    End Property

    Public Property Enabled As Boolean = False

#End Region

#Region "Costruttori"

    'Public Sub New()
    '    mDir = ""
    'End Sub

    Public Sub New(ByVal sPath As String, ByVal sFileNamePrefix As String)
        mDir = sPath
        mFileNamePrefix = sFileNamePrefix
        If sPath <> "" Then
            myCreaDir(mDir)
        End If

    End Sub

#End Region

#Region "Metodi"

    Public Function TraceEventOnDailyFile(ByVal sOrigin As String, ByVal sEvent As String, ByVal sResult As String) As String
        Dim sNomeFile As String
        Dim sRiga As String

        Dim TraceMsg As String = sOrigin & ": " & sEvent & " - " & sResult

        If Not Enabled Then Return TraceMsg
        If mDir = "" Then Return TraceMsg

        Dim sb As New StringBuilder()
        sb.Append(mDir)
        If Not mDir.EndsWith("\") Then
            sb.Append("\")
        End If
        sb.Append(mFileNamePrefix)
        If Not mDir.EndsWith("_") Then
            sb.Append("_")
        End If
        sb.Append(Format(Now, "yyyy"))
        sb.Append(Format(Now, "MM"))
        sb.Append(Format(Now, "dd"))
        sb.Append(".txt")
        sNomeFile = sb.ToString

        Dim sb1 As New StringBuilder()
        sb1.Append(Format(Now, "yyyy/MM/dd"))
        sb1.Append(";")
        sb1.Append(Format(Now, "HH:mm:ss"))
        sb1.Append(";")
        sb1.Append(sOrigin)
        sb1.Append(";")
        sb1.Append(sEvent)
        sb1.Append(";")
        sb1.Append(sResult)
        sb1.Append(";")
        sRiga = sb1.ToString

        SyncLock myLock
            Try
                Dim fs As New FileStream(sNomeFile, FileMode.Append, FileAccess.Write, FileShare.None)
                Dim s As New StreamWriter(fs)
                s.WriteLine(sRiga)
                s.Close()
                fs.Close()
            Catch ex As Exception

            End Try
        End SyncLock
        Return TraceMsg

    End Function

    Public Function TraceEventOnDayOfWeekFile(ByVal sOrigin As String, ByVal sEvent As String, ByVal sResult As String) As String
        Dim sNomeFile As String
        Dim sRiga As String
        Static OldNomeFile As String = ""

        Dim TraceMsg As String = sOrigin & ": " & sEvent & " - " & sResult

        If Not Enabled Then Return TraceMsg
        If mDir = "" Then Return TraceMsg

        Dim sb As New StringBuilder()
        sb.Append(mDir)
        If Not mDir.EndsWith("\") Then
            sb.Append("\")
        End If
        sb.Append(mFileNamePrefix)
        If Not mDir.EndsWith("_") Then
            sb.Append("_")
        End If
        sb.Append(Format(Now, "ddd"))
        sb.Append("_")
        sb.Append(Format(Now, "HH"))
        sb.Append(".txt")
        sNomeFile = sb.ToString

        If sNomeFile <> OldNomeFile Then
            If My.Computer.FileSystem.FileExists(sNomeFile) Then
                My.Computer.FileSystem.DeleteFile(sNomeFile)
            End If
            OldNomeFile = sNomeFile
        End If

        Dim sb1 As New StringBuilder()
        sb1.Append(Format(Now, "yyyy/MM/dd"))
        sb1.Append(";")
        sb1.Append(Format(Now, "HH:mm:ss"))
        sb1.Append(";")
        sb1.Append(sOrigin)
        sb1.Append(";")
        sb1.Append(sEvent)
        sb1.Append(";")
        sb1.Append(sResult)
        sb1.Append(";")
        sRiga = sb1.ToString

        SyncLock myLock
            Try
                Dim fs As New FileStream(sNomeFile, FileMode.Append, FileAccess.Write, FileShare.None)
                Dim s As New StreamWriter(fs)
                s.WriteLine(sRiga)
                s.Close()
                fs.Close()
            Catch ex As Exception

            End Try
        End SyncLock

        Return TraceMsg

    End Function

    Public Sub TracePlcCom(ByVal sOrigin As String, ByVal sMsg As String)
        Dim sNomeFile As String
        Dim sRiga As String
        Static OldNomeFile As String = ""

        If mDir = "" Then Exit Sub

        Dim sb As New StringBuilder()
        sb.Append(mDir)
        If Not mDir.EndsWith("\") Then
            sb.Append("\")
        End If
        sb.Append(mFileNamePrefix)
        If Not mDir.EndsWith("_") Then
            sb.Append("_")
        End If
        sb.Append(Format(Now, "ddd"))
        sb.Append("_")
        sb.Append(Format(Now, "HH"))
        sb.Append(".txt")
        sNomeFile = sb.ToString

        If sNomeFile <> OldNomeFile Then
            If My.Computer.FileSystem.FileExists(sNomeFile) Then
                My.Computer.FileSystem.DeleteFile(sNomeFile)
            End If
            OldNomeFile = sNomeFile
        End If

        Dim sb1 As New StringBuilder()
        sb1.Append(Format(Now, "yyyy/MM/dd"))
        sb1.Append(";")
        sb1.Append(Format(Now, "HH:mm:ss"))
        sb1.Append(";")
        sb1.Append(Format(Timer, "#0.00"))
        sb1.Append(";")
        sb1.Append(sOrigin)
        sb1.Append(";")
        sb1.Append(sMsg)
        sb1.Append(";")
        sRiga = sb1.ToString

        Dim fs As New FileStream(sNomeFile, FileMode.Append, FileAccess.Write, FileShare.None)
        Dim s As New StreamWriter(fs)
        s.WriteLine(sRiga)
        s.Close()
        fs.Close()

    End Sub



#End Region



    Private Sub myCreaDir(ByVal sPath As String)
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

End Class
