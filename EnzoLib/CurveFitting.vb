Imports System.Drawing

Public Class CurveFitting

    ' The function.
    Public Function F(ByVal coeffs As List(Of Double), ByVal x As Double) As Double
        Dim total As Double = 0
        Dim x_factor As Double = 1
        For i As Integer = 0 To coeffs.Count - 1
            total += x_factor * coeffs(i)
            x_factor *= x
        Next i
        Return total
    End Function

    ' Return the error squared.
    Public Function ErrorSquared(ByVal points As List(Of PointF), ByVal coeffs As List(Of Double)) As Double
        Dim total As Double = 0
        For Each pt As PointF In points
            Dim dy As Double = pt.Y - F(coeffs, pt.X)
            total += dy * dy
        Next pt
        Return total
    End Function

    ' Find the least squares linear fit.
    Public Function FindPolynomialLeastSquaresFit(ByVal points As List(Of PointF), ByVal degree As Integer) As List(Of Double)
        ' Allocate space for (degree + 1) equations with 
        ' (degree + 2) terms each (including the constant term).
        Dim coeffs(degree, degree + 1) As Double

        ' Calculate the coefficients for the equations.
        For j As Integer = 0 To degree
            ' Calculate the coefficients for the jth equation.

            ' Calculate the constant term for this equation.
            coeffs(j, degree + 1) = 0
            For Each pt As PointF In points
                coeffs(j, degree + 1) -= Math.Pow(pt.X, j) * pt.Y
            Next pt

            ' Calculate the other coefficients.
            For a_sub As Integer = 0 To degree
                ' Calculate the dth coefficient.
                coeffs(j, a_sub) = 0
                For Each pt As PointF In points
                    coeffs(j, a_sub) -= Math.Pow(pt.X, a_sub + j)
                Next pt
            Next a_sub
        Next j

        ' Solve the equations.
        Dim answer() As Double = GaussianElimination(coeffs)

        ' Return the result converted into a List(Of Double).
        Return answer.ToList()
    End Function

    ' Perform Gaussian elimination on these coefficients.
    ' Return the array of values that gives the solution.
    Private Function GaussianElimination(ByVal coeffs(,) As Double) As Double()
        Dim max_equation As Integer = coeffs.GetUpperBound(0)
        Dim max_coeff As Integer = coeffs.GetUpperBound(1)
        For i As Integer = 0 To max_equation
            ' Use equation_coeffs(i, i) to eliminate the ith
            ' coefficient in all of the other equations.

            ' Find a row with non-zero ith coefficient.
            If (coeffs(i, i) = 0) Then
                For j As Integer = i + 1 To max_equation
                    ' See if this one works.
                    If (coeffs(j, i) <> 0) Then
                        ' This one works. Swap equations i and j.
                        ' This starts at k = i because all
                        ' coefficients to the left are 0.
                        For k As Integer = i To max_coeff
                            Dim temp As Double = coeffs(i, k)
                            coeffs(i, k) = coeffs(j, k)
                            coeffs(j, k) = temp
                        Next k
                        Exit For
                    End If
                Next j
            End If

            ' Make sure we found an equation with
            ' a non-zero ith coefficient.
            Dim coeff_i_i As Double = coeffs(i, i)
            If coeff_i_i = 0 Then
                Throw New ArithmeticException(String.Format( _
                    "There is no unique solution for these points.", _
                    coeffs.GetUpperBound(0) - 1))
            End If

            ' Normalize the ith equation.
            For j As Integer = i To max_coeff
                coeffs(i, j) /= coeff_i_i
            Next j

            ' Use this equation value to zero out
            ' the other equations' ith coefficients.
            For j As Integer = 0 To max_equation
                ' Skip the ith equation.
                If (j <> i) Then
                    ' Zero the jth equation's ith coefficient.
                    Dim coef_j_i As Double = coeffs(j, i)
                    For d As Integer = 0 To max_coeff
                        coeffs(j, d) -= coeffs(i, d) * coef_j_i
                    Next d
                End If
            Next j
        Next i

        ' At this point, the ith equation contains
        ' 2 non-zero entries:
        '      The ith entry which is 1
        '      The last entry coeffs(max_coeff)
        ' This means Ai = equation_coef(max_coeff).
        Dim solution(max_equation) As Double
        For i As Integer = 0 To max_equation
            solution(i) = coeffs(i, max_coeff)
        Next i

        ' Return the solution values.
        Return solution
    End Function




    'Public Enum CurveFitType
    '    CurveFitLinear = 1      ' y = Ax + B
    '    CurveFit1 = 2           ' y = A/x + B
    '    CurveFit2 = 3           ' y = A/[x + B]
    '    CurveFit3 = 4           ' y = 1/[Ax + B]
    '    CurveFit4 = 5           ' y = x/[A + Bx]
    '    CurveFit5 = 6           ' y = A ln(x) + B
    '    CurveFit6 = 7           ' y = A*exp(Bx)
    '    CurveFit7 = 8           ' y = A*x^B
    '    CurveFit8 = 9           ' y = [Ax + B]^(-2)
    'End Enum


    'Public Sub LeastSquaresFit(ByVal x() As Double, ByVal y() As Double, ByVal FitType As CurveFitType, ByVal a As Double, ByVal b As Double)
    '    Dim tX() As Double
    '    Dim tY() As Double
    '    Dim i As Long
    '    Dim tA As Double
    '    Dim tB As Double

    '    ' Make a temporary copy of x(), y()
    '    tX = x
    '    tY = y

    '    ' Transformations to linearize data
    '    Select Case FitType
    '        Case CurveFitType.CurveFitLinear
    '            'Do Nothing
    '        Case CurveFitType.CurveFit1
    '            For i = LBound(tX) To UBound(tX)
    '                tX(i) = 1 / tX(i)
    '            Next i

    '        Case CurveFitType.CurveFit2
    '            For i = LBound(tX) To UBound(tX)
    '                tX(i) = tX(i) * tY(i)
    '            Next i

    '        Case CurveFitType.CurveFit3
    '            For i = LBound(tX) To UBound(tX)
    '                tY(i) = 1 / tY(i)
    '            Next i

    '        Case CurveFitType.CurveFit4
    '            For i = LBound(tX) To UBound(tX)
    '                tX(i) = 1 / tX(i)
    '                tY(i) = 1 / tY(i)
    '            Next i

    '        Case CurveFitType.CurveFit5
    '            For i = LBound(tX) To UBound(tX)
    '                tX(i) = Math.Log(tX(i))
    '            Next i

    '        Case CurveFitType.CurveFit6
    '            For i = LBound(tX) To UBound(tX)
    '                tY(i) = Math.Log(tY(i))
    '            Next i

    '        Case CurveFitType.CurveFit7
    '            For i = LBound(tX) To UBound(tX)
    '                tX(i) = Math.Log(tX(i))
    '                tY(i) = Math.Log(tY(i))
    '            Next i

    '        Case CurveFitType.CurveFit8
    '            For i = LBound(tX) To UBound(tX)
    '                tY(i) = tY(i) ^ -0.5
    '            Next i
    '    End Select


    '    Call LeastSquaresFitLine(tX, tY, tA, tB)

    '    'Transform coefficents if necessary
    '    Select Case FitType
    '        Case CurveFitType.CurveFit2
    '            a = -tB / tA
    '            b = -1 / tA

    '        Case CurveFitType.CurveFit5
    '            a = Math.Exp(tB)
    '            b = tA

    '        Case CurveFitType.CurveFit6
    '            a = Math.Exp(tB)
    '            b = tA

    '        Case Else
    '            a = tA
    '            b = tB
    '    End Select
    'End Sub


    ''Curve fit to y = Ax + B
    'Public Sub LeastSquaresFitLine(ByVal x() As Double, ByVal y() As Double, ByRef a As Double, ByRef b As Double)
    '    Dim Xmean As Double
    '    Dim Ymean As Double
    '    Dim i As Long
    '    Dim N As Double
    '    Dim SumX As Double
    '    Dim SumXY As Double


    '    N = UBound(x) - LBound(x) + 1

    '    'Find the mean of x
    '    Xmean = 0
    '    For i = LBound(x) To UBound(x)
    '        Xmean = Xmean + x(i)
    '    Next i
    '    Xmean = Xmean / N

    '    'Find the mean of y
    '    Ymean = 0
    '    For i = LBound(y) To UBound(y)
    '        Ymean = Ymean + y(i)
    '    Next i
    '    Ymean = Ymean / N

    '    'Find Sum(x(i)-Xmean)^2
    '    SumX = 0
    '    For i = LBound(x) To UBound(x)
    '        SumX = SumX + ((x(i) - Xmean) ^ 2)
    '    Next i

    '    'Find Sum(x(i)-Xmean)(y(i)-Ymean)
    '    SumXY = 0
    '    For i = LBound(x) To UBound(x)
    '        SumXY = SumXY + ((x(i) - Xmean) * (y(i) - Ymean))
    '    Next i

    '    'Compute the slope
    '    a = SumXY / SumX
    '    'Compute the y-intercept
    '    b = Ymean - (a * Xmean)
    'End Sub

    ''Curve fit to P(x) = c(1) + c(2)x + c(3)x^2 + c(4)x^3 + ... c(M+1)x^(M)
    'Public Function PolynomialCurveFit(ByVal x() As Double, ByVal y() As Double, ByVal M As Long) As Double()
    '    Dim c() As Double
    '    Dim b() As Double
    '    Dim p() As Double
    '    Dim a(,) As Double
    '    Dim i As Long
    '    Dim j As Long
    '    Dim tY As Double
    '    Dim tX As Double
    '    Dim tP As Double

    '    ReDim b(M + 1)
    '    ReDim c(M + 1)
    '    ReDim p(2 * M)
    '    ReDim a(M + 1, M + 1)

    '    'Compute the column vector
    '    For i = LBound(x) To UBound(x)
    '        tY = y(i)
    '        tX = x(i)
    '        tP = 1

    '        For j = 1 To M + 1
    '            b(j) = b(j) + tY * tP
    '            tP = tP * tX
    '        Next j
    '    Next i


    '    'Compute the sum of powers
    '    For i = LBound(x) To UBound(x)
    '        tX = x(i)
    '        tP = x(i)

    '        For j = 1 To 2 * M
    '            p(j) = p(j) + tP
    '            tP = tP * tX
    '        Next j
    '    Next i


    '    'Determine the matrix entries
    '    For i = 1 To M + 1
    '        For j = 1 To M + 1
    '            a(i, j) = p(i + j - 2)
    '        Next j
    '    Next i

    '    'Solve A*C = B
    '    Call SolveEqu(a, c, b)

    '    Return c

    'End Function

    ''Solves A*C = B
    'Private Sub SolveEqu(ByVal a(,) As Double, ByRef c() As Double, ByVal b() As Double)
    '    Dim i As Long
    '    Dim j As Long
    '    Dim k As Long
    '    Dim t As Double
    '    Dim f As Double

    '    'Call PrintMatrix(a, b)

    '    For i = LBound(a, 2) To UBound(a, 2)
    '        'Try to swap invalid rows
    '        If a(i, i) = 0 Then
    '            For j = i + 1 To UBound(a, 2)
    '                If a(i, j) <> 0 Then
    '                    For k = LBound(a, 1) To UBound(a, 1)
    '                        t = a(k, i)
    '                        a(k, i) = a(k, j)
    '                        a(k, j) = t
    '                    Next k

    '                    t = b(i)
    '                    b(i) = b(j)
    '                    b(j) = t

    '                    'Debug.Print "Swap."
    '                    'Call PrintMatrix(a, b)
    '                    Exit For
    '                End If
    '            Next j
    '        End If

    '        If a(i, i) <> 0 Then
    '            'Normalize
    '            f = a(i, i)
    '            For k = LBound(a, 1) To UBound(a, 1)
    '                a(k, i) = a(k, i) / f
    '            Next k
    '            b(i) = b(i) / f

    '            'Debug.Print "Normalize."
    '            'Call PrintMatrix(a, b)

    '            For j = i + 1 To UBound(a, 2)
    '                If a(i, j) <> 0 Then
    '                    f = -a(i, j) / a(i, i)

    '                    For k = LBound(a, 1) To UBound(a, 1)
    '                        a(k, j) = a(k, j) + f * a(k, i)
    '                    Next k
    '                    b(j) = b(j) + f * b(i)
    '                End If
    '            Next j
    '            'Debug.Print "Pivot."
    '            'Call PrintMatrix(a, b)
    '        End If
    '    Next i

    '    'Solve up.
    '    For i = UBound(a, 2) To LBound(a, 2) Step -1
    '        c(i) = b(i)

    '        For j = i - 1 To LBound(a, 2) Step -1
    '            b(j) = b(j) - (a(i, j) * b(i))
    '            a(i, j) = 0.0!
    '        Next j
    '    Next i

    '    'Debug.Print "Solved."
    '    'Call PrintMatrix(a, b)

    '    'Fill out the result array
    '    For i = LBound(b) To UBound(b)
    '        c(i) = b(i)
    '    Next i
    'End Sub


    'Private Sub PrintMatrix(ByVal a(,) As Double, ByVal b() As Double)
    '    Dim i As Long
    '    Dim j As Long

    '    For i = LBound(b) To UBound(b)
    '        For j = LBound(a, 2) To UBound(a, 2)
    '            Debug.Print(Right$("      " & Format(a(j, i), "00.0"), 6))
    '        Next j
    '        Debug.Print(" | " & Right$("      " & Format(b(i), "00.0"), 6))
    '    Next i
    'End Sub


End Class
