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
