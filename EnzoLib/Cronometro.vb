Imports System.Threading

Public Class Cronometro
    Private mInizio As DateTime
    Private mDurata As TimeSpan
    Private mTicks As Long
    Private mTicksStart As Long
    Private stopW As System.Diagnostics.Stopwatch

    Public Sub New()
        mInizio = New DateTime
        mDurata = New TimeSpan
        'GC.Collect()
        'GC.WaitForPendingFinalizers()
        stopW = New System.Diagnostics.Stopwatch
        mTicks = 0
        mTicksStart = 0
    End Sub
    Public Sub Ferma()
        stopW.Stop()
    End Sub
    Public Sub Start()
        stopW.Reset()
        stopW.Start()
    End Sub
    Public ReadOnly Property Result() As TimeSpan
        Get
            Return stopW.Elapsed
        End Get
    End Property
    Public ReadOnly Property DurataInTicks() As Long
        Get
            Return stopW.ElapsedTicks
        End Get
    End Property
    Public ReadOnly Property ElapsedMilliseconds() As Long
        Get
            Return stopW.ElapsedMilliseconds
        End Get
    End Property
    Public ReadOnly Property ElapsedSeconds() As Long
        Get
            Return stopW.Elapsed.TotalSeconds
        End Get
    End Property
    Public ReadOnly Property ElapsedMinutes() As Long
        Get
            Return stopW.Elapsed.TotalMinutes
        End Get
    End Property

End Class

Public Class Somma
    Public Sub New()

    End Sub
    Public Function SommaDiDueValori(ByVal a As Integer, ByVal b As Integer) As Integer
        Return a + b
    End Function
End Class
