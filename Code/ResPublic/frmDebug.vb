

Public Class frmDebug

    Private picGraf As GraphicResults

    Private IdCamera As Byte = 0
    Private Const ExposureTimeMult = 1000.0

    Private Sub frmDebug_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If CameraConfig.IsAnyCameraEnabled Then
            If ActiveCameraId = cCameraCentral Then
                optCamera0.Checked = True
            ElseIf ActiveCameraId = cCameraRight Then
                optCameraDX.Checked = True
            Else
                optCameraSX.Checked = True
            End If
        End If

        IdCamera = CameraConfig.GetCameraId(CameraNames(cCameraCentral))


        Dim MinExpo As Double = xCamerasConfig.CameraList(IdCamera).ExposureTime.Min
        Dim MaxExpo As Double = xCamerasConfig.CameraList(IdCamera).ExposureTime.Max
        gtbarExposureTime.MaxValue = MaxExpo * ExposureTimeMult
        gtbarExposureTime.MinValue = MinExpo * ExposureTimeMult
        gtbarExposureTime.ChangeSmall = xCamerasConfig.CameraList(IdCamera).ExposureTime.Inc * ExposureTimeMult
        gtbarExposureTime.ChangeLarge = gtbarExposureTime.ChangeSmall * 10
        gtbarExposureTime.Value = xCamerasConfig.CameraList(IdCamera).ExposureTime.Value * ExposureTimeMult
        If xCamerasConfig.CameraList(IdCamera).ExposureTime.Auto Then
            gtbarExposureTime.Enabled = False
        End If

    End Sub


#Region "Select camera"

    Private Sub optCamera0_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optCamera0.CheckedChanged
        IdCamera = CameraConfig.GetCameraId(CameraNames(cCameraCentral))
    End Sub

    Private Sub optCameraDX_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optCameraDX.CheckedChanged
        IdCamera = CameraConfig.GetCameraId(CameraNames(cCameraRight))
    End Sub

    Private Sub optCameraSX_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optCameraSX.CheckedChanged
        IdCamera = CameraConfig.GetCameraId(CameraNames(cCameraLeft))
    End Sub

    'Private Sub optCameraLDLH_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optCameraLDLH.CheckedChanged
    '    ActiveCameraId = cCameraLDLH
    '    IdCamera = ActiveCameraId
    'End Sub

    'Private Sub optCameraLDRH_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optCameraLDRH.CheckedChanged
    '    ActiveCameraId = cCameraLDRH
    '    IdCamera = ActiveCameraId
    'End Sub

#End Region


#Region "LIN"
    Private Sub chkKey_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkKey.CheckedChanged
        'If chkKey.Checked Then
        '    xInOut.Key(cON)
        'Else
        '    xInOut.Key(cOFF)
        'End If
    End Sub

    Private Sub chkLinOn_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkLinOn.CheckedChanged
        'If chkLinOn.Checked Then
        '    xInOut.LinSupply(cON)
        'Else
        '    xInOut.LinSupply(cOFF)
        'End If
    End Sub
    Private Sub plsViewFormDebugLIN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles plsViewFormDebugLIN.Click
        'Dim frm As New frmDebugNiCAN
        'frm.Show(Me)

    End Sub

    Private Sub plsViewAmisLisa_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles plsViewAmisLisa.Click
        'Dim frm As New frmDebugAmisLisa
        'frm.Show(Me)
    End Sub

#End Region


    Private Sub plsGetImagine_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles plsGetImagine.Click
        If Not xCamerasConfig.Simulation Then
            Dim cam As Icamera = GetCamera(ActiveCameraName)
            cam.StopLive()
        Else
            Dim FileName As String = "C:\AimingTestLamps\Doc\Pictures\LowBeamShut7.bmp"
            Dim FileDlg As New OpenFileDialog
            FileDlg.InitialDirectory = "C:\AimingTestLamps\Doc\Pictures"
            FileDlg.Filter = "Wizard files (*.bmp)|*.bmp"
            FileDlg.RestoreDirectory = True
            If FileDlg.ShowDialog() = DialogResult.OK Then
                FileName = FileDlg.FileName
            End If
            FileDlg.Dispose()
            frmMain.picImageCamera.Image = Image.FromFile(FileName)
        End If
    End Sub

    Private Sub plsLive_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles plsLive.Click
        If xCameraList(IdCamera).Live Then
            StopLiveOnCamera(CameraNames(IdCamera))
            plsLive.Text = "Start Live"
        Else
            StartLiveOnCamera(CameraNames(IdCamera))
            plsLive.Text = "Stop Live"
        End If
    End Sub

    Private Sub plsToBitmap_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles plsToBitmap.Click

        'Dim IdCamera = xCamerasConfig.ActiveCameraId
        'Dim pLast As IntPtr = xCameraData(IdCamera).pCurMem
        'Dim nLastID As Integer = xCameraData(IdCamera).CurMemId

        'Dim bmp As Bitmap = frmMain.GetLastImage(IdCamera)



        'Dim width As Integer = 0, height As Integer = 0, bitspp As Integer, pitch As Integer = 0

        'xCamera(IdCamera).InquireImageMem(pLast, nLastID, width, height, bitspp, pitch)

        ''Convertire memori to bitmap
        'Dim encoding = New System.Text.UTF8Encoding

        ''Dim NomeFile As String = "C:\Pippo.bmp"
        ''If My.Computer.FileSystem.FileExists(NomeFile) Then
        ''    My.Computer.FileSystem.DeleteFile(NomeFile)
        ''End If
        ''Dim ByArrFile() As Byte = encoding.getbytes(NomeFile)
        ''Dim iRes As Integer = Camera(IdCamera).SaveImageMem(ByArrFile, pLast, nLastID)
        ''Dim bm1 As New Bitmap(NomeFile)
        ''Dim bmpdata As System.Drawing.Imaging.BitmapData = bm1.LockBits(New Rectangle(0, 0, bm1.Width, bm1.Height), _
        ''                                    Imaging.ImageLockMode.ReadOnly, bm1.PixelFormat)

        ''Dim bm As New Bitmap(width, height, bmpdata.Stride, bmpdata.PixelFormat, pLast)
        ''bm1.UnlockBits(bmpdata)

        ''Dim bmp As Bitmap = xImageProc(IdCamera).ImageToBitmap(pLast, width, height, pitch)
        'picprova.Image = bmp
        'picprova.Refresh()

        'picGraf = New GraphicResults(picprova.CreateGraphics, 0, 0, picprova.Width, picprova.Height)
        'picGraf.DeltaXfor1Grad = xThisAppConfig.ConfigData.Geometry.GradToPixelHoriz
        'picGraf.DeltaYfor1Grad = xThisAppConfig.ConfigData.Geometry.GradToPixelVert
        'picGraf.DrawCrossWithCircle(Color.Red, 1, 100, 100, 30, 30, 3)

    End Sub

    Private Sub plsCalibDiaph_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles plsCalibDiaph.Click
        'calibrazione diaframma
        Dim IdCamera = 0

        ''Becca immagine e buttala su bitmap
        'Camera(IdCamera).EnableMessage(uEye.IS_FRAME, Me.Handle.ToInt32())
        'Camera(IdCamera).FreezeVideo(uEye.IS_WAIT)

        ''Attesa immagine acqusita
        'xCameraData(IdCamera).ImageAcquired = False
        'Do
        '    Application.DoEvents()
        'Loop While xCameraData(IdCamera).ImageAcquired = False

        'Dim pLast As IntPtr = xCameraData(IdCamera).pCurMem
        'Dim nLastID As Integer = xCameraData(IdCamera).CurMemId
        'Dim eImaProc As New AforgeImageProcessingLib.eImageProc
        'Dim width As Integer = 0, height As Integer = 0, bitspp As Integer, pitch As Integer = 0
        'Camera(IdCamera).InquireImageMem(pLast, nLastID, width, height, bitspp, pitch)
        'Dim bmp As Bitmap = ImageProc(IdCamera).ImageToBitmap(pLast, width, height, pitch)

        IdCamera = ActiveCameraId

        Dim cam As Icamera = GetCamera(CameraNames(IdCamera))
        Dim bmp As Bitmap = cam.LastImage

        Dim eImaProc As New AforgeImageProcessingLib.eImageProc
        Dim Stat As AForge.Imaging.ImageStatistics
        Stat = eImaProc.GetImageStatistic(bmp)

        Dim BlobThreshold As Integer = (Stat.GrayWithoutBlack.Max) - CInt(Stat.GrayWithoutBlack.Max * 0.1)
        Dim BlobAreaMax As Integer = 0, IdBlobAreaMax As Integer = 0

        Dim Blobs() As AForge.Imaging.Blob
        Blobs = eImaProc.GetBlobs(bmp, BlobThreshold)

        For i As Integer = 0 To Blobs.Length - 1
            If Blobs(i).Area > BlobAreaMax Then
                BlobAreaMax = Blobs(i).Area
                IdBlobAreaMax = i
            End If
        Next
        If Blobs(IdBlobAreaMax).Area < 15000 Then
            frmMain.lblInfoOp.Text = Blobs(IdBlobAreaMax).Area & ": Open"
            frmMain.lblInfoOp.ForeColor = Color.Red
        ElseIf Blobs(IdBlobAreaMax).Area > 20000 Then
            frmMain.lblInfoOp.Text = Blobs(IdBlobAreaMax).Area & ": Close"
            frmMain.lblInfoOp.ForeColor = Color.Red
        Else
            frmMain.lblInfoOp.Text = Blobs(IdBlobAreaMax).Area & ": OK"
            frmMain.lblInfoOp.ForeColor = Color.Green
        End If

    End Sub

    Private Sub plsMaxLum_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles plsMaxLum.Click
        'Dim IdCamera = 0
        'Dim pLast As IntPtr = xCameraData(IdCamera).pCurMem
        'Dim nLastID As Integer = xCameraData(IdCamera).CurMemId
        'Dim width As Integer = 0, height As Integer = 0, bitspp As Integer, pitch As Integer = 0

        'Camera(IdCamera).InquireImageMem(pLast, nLastID, width, height, bitspp, pitch)
        'Dim PtoMaxLum As New udtImagePoint
        'Dim MaxLum As Integer = ImageProc(IdCamera).GetMaxLum(pLast, PtoMaxLum)
        'xGraphRes.DrawCrossWithCircle(Color.Red, 1, PtoMaxLum.X, PtoMaxLum.Y, 30, 30, 3)
        'lblMaxLumVal.Text = PtoMaxLum.Grey
        'lblMaxLumX.Text = PtoMaxLum.X
        'lblMaxLumY.Text = PtoMaxLum.Y

        Dim IdCamera As Byte = 0
        Dim cam As Icamera = GetCamera(CameraNames(IdCamera))

        Dim ptoMaxLum As New ImageProcessing.udtImagePoint

        IdCamera = ActiveCameraId
        Dim bmp As Bitmap = cam.LastImage
        Dim ImageProc As New ImageProcessing '(bmp.Width, bmp.Height, xCameraList(IdCamera).Eye.PixelFormat.GetBitsPerPixel)
        ptoMaxLum = ImageProc.GetMaxLumFine(bmp, 200, frmMainImageGraphics, True, Color.White)

        'lblMaxLumVal.Text = ptoMaxLum.Grey
        'lblMaxLumX.Text = ptoMaxLum.X
        'lblMaxLumY.Text = ptoMaxLum.Y
    End Sub


    Private Sub chkLowBeam_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkLowBeam.CheckedChanged
        If chkLowBeam.Checked Then
            xInOut.LowBeam(cON)
        Else
            xInOut.LowBeam(cOFF)
        End If
    End Sub

    Private Sub chkIsolux_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkIsolux.CheckedChanged
        xCameraList(ActiveCameraId).ViewIsolux = chkIsolux.Checked
    End Sub

    Private Sub chkTurnLight_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTurnLight.CheckedChanged
        If chkTurnLight.Checked Then
            xInOut.TurnLight(cON)
        Else
            xInOut.TurnLight(cOFF)
        End If
    End Sub


    Private Sub gtbarExposureTime_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gtbarExposureTime.ValueChanged
        Dim cam As Icamera = GetCamera(CameraNames(IdCamera))
        cam.SetExpositionTime(CDbl(gtbarExposureTime.Value / ExposureTimeMult))
    End Sub
End Class