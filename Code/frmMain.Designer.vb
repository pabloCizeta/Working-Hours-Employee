<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.picUserLogged = New System.Windows.Forms.PictureBox()
        Me.lblLoginName = New System.Windows.Forms.Label()
        Me.panMenu = New System.Windows.Forms.Panel()
        Me.mnu = New OreLavoroDipendenti.ucMenu()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.panLogin = New System.Windows.Forms.Panel()
        Me.panUserLogged = New System.Windows.Forms.Panel()
        Me.lblRelease = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label0 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        CType(Me.picUserLogged, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panMenu.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.panLogin.SuspendLayout()
        Me.panUserLogged.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'picUserLogged
        '
        Me.picUserLogged.Cursor = System.Windows.Forms.Cursors.Default
        Me.picUserLogged.Image = CType(resources.GetObject("picUserLogged.Image"), System.Drawing.Image)
        Me.picUserLogged.Location = New System.Drawing.Point(13, 16)
        Me.picUserLogged.Name = "picUserLogged"
        Me.picUserLogged.Size = New System.Drawing.Size(73, 73)
        Me.picUserLogged.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picUserLogged.TabIndex = 6
        Me.picUserLogged.TabStop = False
        '
        'lblLoginName
        '
        Me.lblLoginName.AutoEllipsis = True
        Me.lblLoginName.BackColor = System.Drawing.Color.Transparent
        Me.lblLoginName.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblLoginName.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLoginName.Location = New System.Drawing.Point(102, 37)
        Me.lblLoginName.Name = "lblLoginName"
        Me.lblLoginName.Size = New System.Drawing.Size(246, 27)
        Me.lblLoginName.TabIndex = 5
        Me.lblLoginName.Text = "LoginName"
        Me.lblLoginName.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'panMenu
        '
        Me.panMenu.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panMenu.Controls.Add(Me.mnu)
        Me.panMenu.Location = New System.Drawing.Point(4, 3)
        Me.panMenu.Name = "panMenu"
        Me.panMenu.Size = New System.Drawing.Size(136, 440)
        Me.panMenu.TabIndex = 141
        '
        'mnu
        '
        Me.mnu.Location = New System.Drawing.Point(-1, -6)
        Me.mnu.Name = "mnu"
        Me.mnu.Size = New System.Drawing.Size(135, 447)
        Me.mnu.TabIndex = 0
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.SystemColors.Control
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.ForeColor = System.Drawing.Color.OrangeRed
        Me.Label24.Location = New System.Drawing.Point(28, 21)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(65, 17)
        Me.Label24.TabIndex = 15
        Me.Label24.Text = "cizeta"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.SystemColors.Control
        Me.Label25.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label25.Location = New System.Drawing.Point(28, 5)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(65, 17)
        Me.Label25.TabIndex = 14
        Me.Label25.Text = "Powered by"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel4
        '
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel4.Controls.Add(Me.Label24)
        Me.Panel4.Controls.Add(Me.Label25)
        Me.Panel4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Panel4.Location = New System.Drawing.Point(4, 771)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(124, 51)
        Me.Panel4.TabIndex = 145
        '
        'panLogin
        '
        Me.panLogin.Controls.Add(Me.panUserLogged)
        Me.panLogin.Controls.Add(Me.lblRelease)
        Me.panLogin.Controls.Add(Me.PictureBox1)
        Me.panLogin.Controls.Add(Me.Label9)
        Me.panLogin.Controls.Add(Me.Label0)
        Me.panLogin.Controls.Add(Me.Label8)
        Me.panLogin.Location = New System.Drawing.Point(164, 12)
        Me.panLogin.Name = "panLogin"
        Me.panLogin.Size = New System.Drawing.Size(367, 431)
        Me.panLogin.TabIndex = 143
        '
        'panUserLogged
        '
        Me.panUserLogged.Controls.Add(Me.picUserLogged)
        Me.panUserLogged.Controls.Add(Me.lblLoginName)
        Me.panUserLogged.Location = New System.Drawing.Point(3, 312)
        Me.panUserLogged.Name = "panUserLogged"
        Me.panUserLogged.Size = New System.Drawing.Size(361, 109)
        Me.panUserLogged.TabIndex = 223
        '
        'lblRelease
        '
        Me.lblRelease.BackColor = System.Drawing.Color.Transparent
        Me.lblRelease.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRelease.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblRelease.Location = New System.Drawing.Point(83, 278)
        Me.lblRelease.Name = "lblRelease"
        Me.lblRelease.Size = New System.Drawing.Size(222, 19)
        Me.lblRelease.TabIndex = 221
        Me.lblRelease.Text = "rel. 0.0.0"
        Me.lblRelease.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(123, 136)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(134, 129)
        Me.PictureBox1.TabIndex = 75
        Me.PictureBox1.TabStop = False
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Arial", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(35, 100)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(319, 33)
        Me.Label9.TabIndex = 74
        Me.Label9.Text = "ORE LAVORO "
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label0
        '
        Me.Label0.AutoEllipsis = True
        Me.Label0.BackColor = System.Drawing.Color.Transparent
        Me.Label0.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Label0.Font = New System.Drawing.Font("Arial", 48.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label0.ForeColor = System.Drawing.Color.OrangeRed
        Me.Label0.Location = New System.Drawing.Point(46, 0)
        Me.Label0.Name = "Label0"
        Me.Label0.Size = New System.Drawing.Size(290, 70)
        Me.Label0.TabIndex = 73
        Me.Label0.Text = "cizeta"
        Me.Label0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(35, 70)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(319, 20)
        Me.Label8.TabIndex = 72
        Me.Label8.Text = "CIZETA AUTOMAZIONE s.r.l."
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(542, 451)
        Me.Controls.Add(Me.panLogin)
        Me.Controls.Add(Me.panMenu)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Form1"
        CType(Me.picUserLogged, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panMenu.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.panLogin.ResumeLayout(False)
        Me.panUserLogged.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents picUserLogged As System.Windows.Forms.PictureBox
    Friend WithEvents lblLoginName As System.Windows.Forms.Label
    Friend WithEvents panMenu As System.Windows.Forms.Panel
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents mnu As ucMenu
    Friend WithEvents panLogin As Panel
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Label9 As Label
    Friend WithEvents Label0 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents lblRelease As Label
    Friend WithEvents panUserLogged As Panel
    ' Friend WithEvents ucMasterMonitor As LasCore.ucMasterMonitor

End Class
