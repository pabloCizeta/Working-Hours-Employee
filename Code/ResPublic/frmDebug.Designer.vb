<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDebug
    Inherits System.Windows.Forms.Form

    'Form esegue l'override del metodo Dispose per pulire l'elenco dei componenti.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Richiesto da Progettazione Windows Form
    Private components As System.ComponentModel.IContainer

    'NOTA: la procedura che segue è richiesta da Progettazione Windows Form
    'Può essere modificata in Progettazione Windows Form.  
    'Non modificarla nell'editor del codice.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.chkLinOn = New System.Windows.Forms.CheckBox()
        Me.chkKey = New System.Windows.Forms.CheckBox()
        Me.plsViewAmisLisa = New System.Windows.Forms.Button()
        Me.plsViewFormDebugLIN = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.optCameraLDRH = New System.Windows.Forms.RadioButton()
        Me.optCameraLDLH = New System.Windows.Forms.RadioButton()
        Me.optCameraSX = New System.Windows.Forms.RadioButton()
        Me.optCamera0 = New System.Windows.Forms.RadioButton()
        Me.optCameraDX = New System.Windows.Forms.RadioButton()
        Me.picprova = New System.Windows.Forms.PictureBox()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.plsGetImagine = New System.Windows.Forms.Button()
        Me.plsLive = New System.Windows.Forms.Button()
        Me.plsToBitmap = New System.Windows.Forms.Button()
        Me.plsCalibDiaph = New System.Windows.Forms.Button()
        Me.plsMaxLum = New System.Windows.Forms.Button()
        Me.chkLowBeam = New System.Windows.Forms.CheckBox()
        Me.chkIsolux = New System.Windows.Forms.CheckBox()
        Me.chkTurnLight = New System.Windows.Forms.CheckBox()
        Me.gtbarExposureTime = New gTrackBar.gTrackBar()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.picprova, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.chkLinOn)
        Me.GroupBox3.Controls.Add(Me.chkKey)
        Me.GroupBox3.Controls.Add(Me.plsViewAmisLisa)
        Me.GroupBox3.Controls.Add(Me.plsViewFormDebugLIN)
        Me.GroupBox3.Location = New System.Drawing.Point(32, 384)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(277, 47)
        Me.GroupBox3.TabIndex = 120
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "LIN"
        '
        'chkLinOn
        '
        Me.chkLinOn.AutoSize = True
        Me.chkLinOn.Location = New System.Drawing.Point(172, 17)
        Me.chkLinOn.Name = "chkLinOn"
        Me.chkLinOn.Size = New System.Drawing.Size(54, 17)
        Me.chkLinOn.TabIndex = 139
        Me.chkLinOn.Text = "LinOn"
        Me.chkLinOn.UseVisualStyleBackColor = True
        '
        'chkKey
        '
        Me.chkKey.AutoSize = True
        Me.chkKey.Location = New System.Drawing.Point(227, 17)
        Me.chkKey.Name = "chkKey"
        Me.chkKey.Size = New System.Drawing.Size(44, 17)
        Me.chkKey.TabIndex = 138
        Me.chkKey.Text = "Key"
        Me.chkKey.UseVisualStyleBackColor = True
        '
        'plsViewAmisLisa
        '
        Me.plsViewAmisLisa.Location = New System.Drawing.Point(78, 13)
        Me.plsViewAmisLisa.Name = "plsViewAmisLisa"
        Me.plsViewAmisLisa.Size = New System.Drawing.Size(89, 23)
        Me.plsViewAmisLisa.TabIndex = 101
        Me.plsViewAmisLisa.Text = "ViewAmisLisa"
        Me.plsViewAmisLisa.UseVisualStyleBackColor = True
        '
        'plsViewFormDebugLIN
        '
        Me.plsViewFormDebugLIN.Location = New System.Drawing.Point(6, 13)
        Me.plsViewFormDebugLIN.Name = "plsViewFormDebugLIN"
        Me.plsViewFormDebugLIN.Size = New System.Drawing.Size(66, 23)
        Me.plsViewFormDebugLIN.TabIndex = 100
        Me.plsViewFormDebugLIN.Text = "ViewLIN"
        Me.plsViewFormDebugLIN.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.optCameraLDRH)
        Me.GroupBox2.Controls.Add(Me.optCameraLDLH)
        Me.GroupBox2.Controls.Add(Me.optCameraSX)
        Me.GroupBox2.Controls.Add(Me.optCamera0)
        Me.GroupBox2.Controls.Add(Me.optCameraDX)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 192)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(264, 69)
        Me.GroupBox2.TabIndex = 121
        Me.GroupBox2.TabStop = False
        '
        'optCameraLDRH
        '
        Me.optCameraLDRH.AutoSize = True
        Me.optCameraLDRH.Location = New System.Drawing.Point(182, 46)
        Me.optCameraLDRH.Name = "optCameraLDRH"
        Me.optCameraLDRH.Size = New System.Drawing.Size(58, 17)
        Me.optCameraLDRH.TabIndex = 116
        Me.optCameraLDRH.TabStop = True
        Me.optCameraLDRH.Text = "LD-RH"
        Me.optCameraLDRH.UseVisualStyleBackColor = True
        '
        'optCameraLDLH
        '
        Me.optCameraLDLH.AutoSize = True
        Me.optCameraLDLH.Location = New System.Drawing.Point(10, 46)
        Me.optCameraLDLH.Name = "optCameraLDLH"
        Me.optCameraLDLH.Size = New System.Drawing.Size(56, 17)
        Me.optCameraLDLH.TabIndex = 115
        Me.optCameraLDLH.TabStop = True
        Me.optCameraLDLH.Text = "LD-LH"
        Me.optCameraLDLH.UseVisualStyleBackColor = True
        '
        'optCameraSX
        '
        Me.optCameraSX.AutoSize = True
        Me.optCameraSX.Location = New System.Drawing.Point(10, 13)
        Me.optCameraSX.Name = "optCameraSX"
        Me.optCameraSX.Size = New System.Drawing.Size(75, 17)
        Me.optCameraSX.TabIndex = 114
        Me.optCameraSX.TabStop = True
        Me.optCameraSX.Text = "CameraSX"
        Me.optCameraSX.UseVisualStyleBackColor = True
        '
        'optCamera0
        '
        Me.optCamera0.AutoSize = True
        Me.optCamera0.Location = New System.Drawing.Point(91, 13)
        Me.optCamera0.Name = "optCamera0"
        Me.optCamera0.Size = New System.Drawing.Size(86, 17)
        Me.optCamera0.TabIndex = 112
        Me.optCamera0.TabStop = True
        Me.optCamera0.Text = "CameraCentr"
        Me.optCamera0.UseVisualStyleBackColor = True
        '
        'optCameraDX
        '
        Me.optCameraDX.AutoSize = True
        Me.optCameraDX.Location = New System.Drawing.Point(182, 13)
        Me.optCameraDX.Name = "optCameraDX"
        Me.optCameraDX.Size = New System.Drawing.Size(76, 17)
        Me.optCameraDX.TabIndex = 113
        Me.optCameraDX.TabStop = True
        Me.optCameraDX.Text = "CameraDX"
        Me.optCameraDX.UseVisualStyleBackColor = True
        '
        'picprova
        '
        Me.picprova.Location = New System.Drawing.Point(-2, -2)
        Me.picprova.Name = "picprova"
        Me.picprova.Size = New System.Drawing.Size(340, 180)
        Me.picprova.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picprova.TabIndex = 122
        Me.picprova.TabStop = False
        '
        'Panel4
        '
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel4.Controls.Add(Me.picprova)
        Me.Panel4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Panel4.Location = New System.Drawing.Point(5, 1)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(346, 185)
        Me.Panel4.TabIndex = 139
        '
        'plsGetImagine
        '
        Me.plsGetImagine.Location = New System.Drawing.Point(13, 307)
        Me.plsGetImagine.Name = "plsGetImagine"
        Me.plsGetImagine.Size = New System.Drawing.Size(124, 23)
        Me.plsGetImagine.TabIndex = 140
        Me.plsGetImagine.Text = "BeccaImmagine"
        Me.plsGetImagine.UseVisualStyleBackColor = True
        '
        'plsLive
        '
        Me.plsLive.Location = New System.Drawing.Point(143, 307)
        Me.plsLive.Name = "plsLive"
        Me.plsLive.Size = New System.Drawing.Size(66, 23)
        Me.plsLive.TabIndex = 141
        Me.plsLive.Text = "Live"
        Me.plsLive.UseVisualStyleBackColor = True
        '
        'plsToBitmap
        '
        Me.plsToBitmap.Location = New System.Drawing.Point(280, 202)
        Me.plsToBitmap.Name = "plsToBitmap"
        Me.plsToBitmap.Size = New System.Drawing.Size(71, 23)
        Me.plsToBitmap.TabIndex = 142
        Me.plsToBitmap.Text = "ToBitmap"
        Me.plsToBitmap.UseVisualStyleBackColor = True
        '
        'plsCalibDiaph
        '
        Me.plsCalibDiaph.Location = New System.Drawing.Point(13, 348)
        Me.plsCalibDiaph.Name = "plsCalibDiaph"
        Me.plsCalibDiaph.Size = New System.Drawing.Size(75, 23)
        Me.plsCalibDiaph.TabIndex = 143
        Me.plsCalibDiaph.Text = "CalibDiaphr"
        Me.plsCalibDiaph.UseVisualStyleBackColor = True
        '
        'plsMaxLum
        '
        Me.plsMaxLum.Location = New System.Drawing.Point(94, 348)
        Me.plsMaxLum.Name = "plsMaxLum"
        Me.plsMaxLum.Size = New System.Drawing.Size(60, 23)
        Me.plsMaxLum.TabIndex = 144
        Me.plsMaxLum.Text = "MaxLum"
        Me.plsMaxLum.UseVisualStyleBackColor = True
        '
        'chkLowBeam
        '
        Me.chkLowBeam.AutoSize = True
        Me.chkLowBeam.Location = New System.Drawing.Point(280, 231)
        Me.chkLowBeam.Name = "chkLowBeam"
        Me.chkLowBeam.Size = New System.Drawing.Size(73, 17)
        Me.chkLowBeam.TabIndex = 146
        Me.chkLowBeam.Text = "LowBeam"
        Me.chkLowBeam.UseVisualStyleBackColor = True
        '
        'chkIsolux
        '
        Me.chkIsolux.AutoSize = True
        Me.chkIsolux.Location = New System.Drawing.Point(224, 311)
        Me.chkIsolux.Name = "chkIsolux"
        Me.chkIsolux.Size = New System.Drawing.Size(53, 17)
        Me.chkIsolux.TabIndex = 147
        Me.chkIsolux.Text = "Isolux"
        Me.chkIsolux.UseVisualStyleBackColor = True
        '
        'chkTurnLight
        '
        Me.chkTurnLight.AutoSize = True
        Me.chkTurnLight.Location = New System.Drawing.Point(280, 260)
        Me.chkTurnLight.Name = "chkTurnLight"
        Me.chkTurnLight.Size = New System.Drawing.Size(70, 17)
        Me.chkTurnLight.TabIndex = 148
        Me.chkTurnLight.Text = "Turn light"
        Me.chkTurnLight.UseVisualStyleBackColor = True
        '
        'gtbarExposureTime
        '
        Me.gtbarExposureTime.BackColor = System.Drawing.SystemColors.Control
        Me.gtbarExposureTime.Label = Nothing
        Me.gtbarExposureTime.Location = New System.Drawing.Point(6, 267)
        Me.gtbarExposureTime.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.gtbarExposureTime.Name = "gtbarExposureTime"
        Me.gtbarExposureTime.Size = New System.Drawing.Size(269, 34)
        Me.gtbarExposureTime.SliderWidthHigh = 1.0!
        Me.gtbarExposureTime.SliderWidthLow = 1.0!
        Me.gtbarExposureTime.TabIndex = 149
        Me.gtbarExposureTime.TickThickness = 1.0!
        Me.gtbarExposureTime.Value = 0
        Me.gtbarExposureTime.ValueAdjusted = 0.0!
        Me.gtbarExposureTime.ValueBox = gTrackBar.gTrackBar.eValueBox.Right
        Me.gtbarExposureTime.ValueBoxBorder = System.Drawing.SystemColors.ControlText
        Me.gtbarExposureTime.ValueBoxFontColor = System.Drawing.SystemColors.ControlText
        Me.gtbarExposureTime.ValueBoxShape = gTrackBar.gTrackBar.eShape.Ellipse
        Me.gtbarExposureTime.ValueBoxSize = New System.Drawing.Size(40, 20)
        Me.gtbarExposureTime.ValueDivisor = gTrackBar.gTrackBar.eValueDivisor.e1000
        Me.gtbarExposureTime.ValueStrFormat = "#0.000"
        '
        'frmDebug
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(355, 443)
        Me.Controls.Add(Me.gtbarExposureTime)
        Me.Controls.Add(Me.chkTurnLight)
        Me.Controls.Add(Me.chkIsolux)
        Me.Controls.Add(Me.chkLowBeam)
        Me.Controls.Add(Me.plsMaxLum)
        Me.Controls.Add(Me.plsCalibDiaph)
        Me.Controls.Add(Me.plsToBitmap)
        Me.Controls.Add(Me.plsLive)
        Me.Controls.Add(Me.plsGetImagine)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox3)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmDebug"
        Me.Text = "frmDebug"
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.picprova, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents plsViewAmisLisa As System.Windows.Forms.Button
    Friend WithEvents plsViewFormDebugLIN As System.Windows.Forms.Button
    Friend WithEvents chkLinOn As System.Windows.Forms.CheckBox
    Friend WithEvents chkKey As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents optCameraSX As System.Windows.Forms.RadioButton
    Friend WithEvents optCamera0 As System.Windows.Forms.RadioButton
    Friend WithEvents optCameraDX As System.Windows.Forms.RadioButton
    Friend WithEvents picprova As System.Windows.Forms.PictureBox
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents plsGetImagine As System.Windows.Forms.Button
    Friend WithEvents plsLive As System.Windows.Forms.Button
    Friend WithEvents plsToBitmap As System.Windows.Forms.Button
    Friend WithEvents plsCalibDiaph As System.Windows.Forms.Button
    Friend WithEvents plsMaxLum As System.Windows.Forms.Button
    Friend WithEvents chkLowBeam As System.Windows.Forms.CheckBox
    Friend WithEvents chkIsolux As System.Windows.Forms.CheckBox
    Friend WithEvents optCameraLDRH As System.Windows.Forms.RadioButton
    Friend WithEvents optCameraLDLH As System.Windows.Forms.RadioButton
    Friend WithEvents chkTurnLight As System.Windows.Forms.CheckBox
    Friend WithEvents gtbarExposureTime As gTrackBar.gTrackBar
End Class
