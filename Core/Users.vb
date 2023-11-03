Imports System.Data.SqlClient
Imports System.IO
Imports System.Drawing

Public Class Users

    Private UsersFileName As String
    Private SuperUsersFileName As String
    Private LoginByTrace As Boolean = False

    Private LevelsForUser As List(Of Integer)
    Private LevelsForMainte As List(Of Integer)
    Private LevelsForAdmin As List(Of Integer)

    Public Enum enumUserRole
        Unknown
        User
        Administrator
        Maintenance
        Master
    End Enum

    Public Class udtUser
        Implements IEquatable(Of udtUser)

        Public Property Name As String = ""
        Public Property LoginName As String = ""
        Public Property Password As String = ""
        Public Property Role As enumUserRole = enumUserRole.Unknown
        Public Property BadgeCode As String = ""
        Public Property AutoLogoffTime As Integer = 10
        Public Property Photo As Image

        Public Sub New()

        End Sub
        Public Sub New(UserName As String, LoginName As String, Password As String, role As enumUserRole, BadgeCode As String, AutoLogoffTime As Integer)
            Me.Name = UserName
            Me.LoginName = LoginName
            Me.Password = Password
            Me.Role = role
            Me.BadgeCode = BadgeCode
            Me.AutoLogoffTime = AutoLogoffTime
        End Sub
        Public Overloads Function DeepCopy() As udtUser
            Dim other As udtUser = DirectCast(Me.MemberwiseClone(), udtUser)
            Return other
        End Function
        Public Overrides Function Equals(obj As Object) As Boolean
            If obj Is Nothing Then
                Return False
            End If
            Dim objAsUserData As udtUser = TryCast(obj, udtUser)
            If objAsUserData Is Nothing Then
                Return False
            Else
                Return Equals(objAsUserData)
            End If
        End Function
        Public Overloads Function Equals(other As udtUser) As Boolean _
        Implements IEquatable(Of udtUser).Equals
            If other Is Nothing Then
                Return False
            End If
            Return (Me.Name.Equals(other.Name))
        End Function

    End Class


#Region "Proprerties"

    Public Property Users As List(Of udtUser)
    Public Property SuperUsers As List(Of udtUser)
    Public Property UserLogged As udtUser

    Public Property LevelsForUserRole() As String
        Get
            Dim str As String = ""
            For Each item As Integer In LevelsForUser
                str = str & item.ToString & ","
            Next
            Return str
        End Get
        Set(ByVal value As String)
            Dim aryStr As String() = value.Split(",")
            LevelsForUser = New List(Of Integer)
            For i As Integer = 0 To aryStr.Length - 1
                LevelsForUser.Add(TextToInt(aryStr(i)))
            Next
        End Set
    End Property

    Public Property LevelsForMaintenanceRole() As String
        Get
            Dim str As String = ""
            For Each item As Integer In LevelsForMainte
                str = str & item.ToString & ","
            Next
            Return str
        End Get
        Set(ByVal value As String)
            Dim aryStr As String() = value.Split(",")
            LevelsForMainte = New List(Of Integer)
            For i As Integer = 0 To aryStr.Length - 1
                LevelsForMainte.Add(TextToInt(aryStr(i)))
            Next
        End Set
    End Property

    Public Property LevelsForAdministratorRole() As String
        Get
            Dim str As String = ""
            For Each item As Integer In LevelsForAdmin
                str = str & item.ToString & ","
            Next
            Return str
        End Get
        Set(ByVal value As String)
            Dim aryStr As String() = value.Split(",")
            LevelsForAdmin = New List(Of Integer)
            For i As Integer = 0 To aryStr.Length - 1
                LevelsForAdmin.Add(TextToInt(aryStr(i)))
            Next
        End Set
    End Property
#End Region



#Region "Costructors"

    Public Sub New()
        Users = New List(Of udtUser)
        SuperUsers = New List(Of udtUser)
        UserLogged = New udtUser
        Me.LoginByTrace = False

        FillUsersList()

    End Sub

#End Region


#Region "Methods"

    'Public Function AddNewUser(User As udtUser) As Boolean
    '    Return AddNewUser(User.Name, User.LoginName, User.Password, User.Role, User.BadgeCode, User.AutoLogoffTime)
    'End Function
    'Public Function AddNewUser(UserName As String, LoginName As String, Password As String, role As enumUserRole, BadgeCode As String, AutoLogoffTime As Integer) As Boolean
    '    Users.Add(New udtUser(UserName, LoginName, Password, role, BadgeCode, AutoLogoffTime))

    '    'Users.Add(New udtUserData() With {
    '    '          .UserName = UserName,
    '    '          .LoginName = LoginName,
    '    '          .Password = Password,
    '    '          .Role = role,
    '    '          .BadgeCode = BadgeCode,
    '    '          .AutoLogoffTime = AutoLogoffTime
    '    '      })

    '    Return True
    'End Function

    'Public Function UpdateUser(User As udtUser) As Boolean
    '    Return UpdateUser(User.Name, User.LoginName, User.Password, User.Role, User.BadgeCode, User.AutoLogoffTime)
    'End Function
    'Public Function UpdateUser(UserName As String, LoginName As String, Password As String, role As enumUserRole, BadgeCode As String, AutoLogoffTime As Integer) As Boolean
    '    For Each item As udtUser In Users
    '        If item.Name = UserName Then
    '            item.LoginName = LoginName
    '            item.Password = Password
    '            item.Role = role
    '            item.BadgeCode = BadgeCode
    '            item.AutoLogoffTime = AutoLogoffTime
    '            Exit For
    '        End If
    '    Next
    '    Return True
    'End Function

    'Public Function RemoveUser(UserName As String) As Boolean
    '    Users.Remove(New udtUser() With {.Name = UserName})
    '    Return True
    'End Function

    'Public Function IsUserNameAlreadyUsed(UserName As String) As Boolean

    '    For Each item As Users.udtUser In Users
    '        If UserName = item.Name Then
    '            Return True
    '        End If
    '    Next
    '    For Each item As Users.udtUser In SuperUsers
    '        If UserName = item.Name Then
    '            Return True
    '        End If
    '    Next

    '    Return False
    'End Function

    'Public Function IsLoginPasswordAlreadyUsed(UserName As String, LoginName As String, Password As String) As Boolean
    '    For Each item As Users.udtUser In Users
    '        If item.Name <> UserName Then
    '            If (LoginName = item.LoginName) And (Password = item.Password) Then
    '                Return True
    '            End If
    '        End If
    '    Next
    '    For Each item As Users.udtUser In SuperUsers
    '        If item.Name <> UserName Then
    '            If (LoginName = item.LoginName) And (Password = item.Password) Then
    '                Return True
    '            End If
    '        End If
    '    Next

    '    Return False
    'End Function

    'Public Function IsBadgeCodeAlreadyUsed(UserName As String, BadgeCode As String) As Boolean
    '    For Each item As Users.udtUser In Users
    '        If item.Name <> UserName Then
    '            If (BadgeCode <> "") And (BadgeCode = item.BadgeCode) Then
    '                Return True
    '            End If
    '        End If
    '    Next
    '    For Each item As Users.udtUser In SuperUsers
    '        If item.Name <> UserName Then
    '            If (BadgeCode <> "") And (BadgeCode = item.BadgeCode) Then
    '                Return False
    '            End If
    '        End If
    '    Next

    '    Return False
    'End Function


    Public Function Login(ByVal sLoginName As String, ByVal sPassword As String) As Boolean

        'If sLoginName = "" And sPassword = "" Then
        '    For Each Drive In System.IO.DriveInfo.GetDrives()
        '        Try
        '            If Drive.DriveType = IO.DriveType.Removable And Drive.VolumeLabel = "LAS" Then
        '                UserLogged.Name = "Enzo Boveri"
        '                UserLogged.AutoLogoffTime = 0
        '                UserLogged.Role = enumUserRole.Master
        '                Return True
        '            End If
        '        Catch ex As Exception
        '        End Try
        '    Next
        'End If

        'Check superuser
        For Each item As udtUser In SuperUsers
            If UCase(sLoginName) = UCase(item.LoginName) And sPassword = item.Password Then
                UserLogged = item
                UserLogged.AutoLogoffTime = 0
                UserLogged.Role = enumUserRole.Master
                Return True
            End If
        Next

        'Users
        For Each item As udtUser In Users
            If UCase(sLoginName) = UCase(item.LoginName) And sPassword = item.Password Then
                UserLogged = item
                Return True
            End If
        Next

        sLoginName = StrReverse(sLoginName)
        For Each item As udtUser In Users
            If UCase(sLoginName) = UCase(item.LoginName) And sPassword = item.Password Then
                UserLogged = item
                Return True
            End If
        Next


        Return False
    End Function
    Public Function Login(ByVal sBadgeCode As String) As Boolean

        'Check superuser
        For Each item As udtUser In SuperUsers
            If UCase(sBadgeCode) = UCase(item.BadgeCode) Then
                Return Login(item.LoginName, item.Password)
            End If
        Next

        'Users
        If LoginByTrace Then

        Else
            For Each item As udtUser In Users
                If UCase(sBadgeCode) = UCase(item.BadgeCode) Then
                    Return Login(item.LoginName, item.Password)
                End If
            Next
        End If


        Return False
    End Function

    Public Function IsLoginNameOK(ByVal sLoginName As String) As Boolean

        'Check superuser
        For Each item As udtUser In SuperUsers
            If UCase(sLoginName) = UCase(item.LoginName) Then
                Return True
            End If
        Next

        For Each item As udtUser In Users
            If UCase(sLoginName) = UCase(item.LoginName) Then
                Return True
            End If
        Next

        Return False
    End Function

    Public Function Logout() As Boolean
        UserLogged = New udtUser
        Return True
    End Function

    Public Function UserLevelToUserRole(UserLevel As Integer) As enumUserRole

        For Each item As Integer In LevelsForUser
            If item = UserLevel Then Return enumUserRole.User
        Next

        For Each item As Integer In LevelsForMainte
            If item = UserLevel Then Return enumUserRole.Maintenance
        Next

        For Each item As Integer In LevelsForAdmin
            If item = UserLevel Then Return enumUserRole.Administrator
        Next

        Return enumUserRole.Unknown
    End Function

#End Region


    Private Sub FillUsersList()
        'Dim strs As New List(Of String)

        'Dim daTab As New SqlDataAdapter, dsTab As New DataTable
        'Dim sqlCon As Configuration.udtAppConfig.udtSqlConnectionConfig = xConfig.GetSqlConnection(cLocalSqlServer)

        'Try

        '    Using dbConnection As New SqlConnection(sqlCon.ConnectionString)
        '        dbConnection.Open()

        '        Dim cmd As New SqlCommand
        '        cmd.Connection = dbConnection
        '        cmd.CommandText = "GetUsersList"
        '        cmd.CommandType = CommandType.StoredProcedure

        '        'Dim sqlParamPiecesQty As New SqlParameter
        '        'sqlParamPiecesQty = cmd.Parameters.Add("@PiecesQty", SqlDbType.Int)
        '        'sqlParamPiecesQty.Value = PieceQty

        '        Dim dr As SqlDataReader
        '        dr = cmd.ExecuteReader()
        '        If dr.HasRows Then
        '            dsTab.Load(dr)
        '        End If
        '        dr.Close()

        '        'strs.Clear()
        '        Users.Clear()
        '        For Each row As DataRow In dsTab.Rows
        '            ' strs.Add(row("Name"))
        '            Dim u As New udtUser
        '            u.Name = row("Nome")
        '            u.AutoLogoffTime = 0
        '            u.BadgeCode = ""
        '            u.LoginName = row("LoginName")
        '            u.Password = row("Password")
        '            Select Case row("Matricola")
        '                'Case "Boveri Enzo", "Cairo Giovanni", "Cairo Paolo", "Zavattaro Albertino", "Zavattaro Michela"
        '                Case 2, 6, 22, 7, 26
        '                    u.Role = enumUserRole.Administrator
        '                Case Else
        '                    u.Role = enumUserRole.User
        '            End Select

        '            If IsDBNull(row("Fotografia")) Then
        '                u.Photo = Nothing
        '            Else
        '                'u.Photo = Image.FromHbitmap(row("Fotografia"))
        '                Dim newImage As Image = Nothing
        '                Dim imageData As Byte() = DirectCast(row("Fotografia"), Byte())
        '                Using ms As New MemoryStream(imageData, 0, imageData.Length)
        '                    ms.Write(imageData, 0, imageData.Length)
        '                    newImage = Image.FromStream(ms, True)
        '                End Using
        '                u.Photo = newImage
        '            End If

        '            Users.Add(u)
        '        Next

        '    End Using

        'Catch ex As Exception

        'End Try



    End Sub




End Class
