Imports Core
Public Class EmployeeData

    Private ser As Serialization

    Private FileName As String

#Region "DataTypes"
    Public Class udtMoneyData
        Public Property Riporti As Single = 0

    End Class

    Public Class udtEmployeeData
        Public Property MoneyAttuali As New List(Of udtMoneyData)

        Public Sub New()
        End Sub

    End Class
#End Region


#Region "Properties"
    Public Property Data As udtEmployeeData
#End Region

#Region "Costructors"
    Public Sub New()

    End Sub
    Public Sub New(ByVal FileName As String)
        Data = New udtEmployeeData
        ser = New Serialization(FileName, Data)
    End Sub

#End Region

#Region "Methods"
    Public Sub ReadDataFromFile()
        Data = ser.Deserialize()
    End Sub

    Public Sub WriteDataToFile()
        ser.Serialize()
    End Sub
#End Region
End Class
