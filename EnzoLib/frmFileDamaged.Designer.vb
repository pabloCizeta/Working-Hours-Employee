<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFileDamaged
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmFileDamaged))
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblFileName = New System.Windows.Forms.Label()
        Me.plsRestore = New System.Windows.Forms.Button()
        Me.plsIgnore = New System.Windows.Forms.Button()
        Me.plsExit = New System.Windows.Forms.Button()
        Me.txtError = New System.Windows.Forms.TextBox()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PictureBox1
        '
        Me.PictureBox1.Cursor = System.Windows.Forms.Cursors.Default
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(12, 12)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(56, 51)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 1
        Me.PictureBox1.TabStop = False
        '
        'PictureBox2
        '
        Me.PictureBox2.Cursor = System.Windows.Forms.Cursors.Default
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(439, 12)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(52, 51)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox2.TabIndex = 2
        Me.PictureBox2.TabStop = False
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Maroon
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(74, 12)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(359, 51)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "File damaged"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblFileName
        '
        Me.lblFileName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblFileName.Location = New System.Drawing.Point(12, 66)
        Me.lblFileName.Name = "lblFileName"
        Me.lblFileName.Size = New System.Drawing.Size(479, 28)
        Me.lblFileName.TabIndex = 4
        Me.lblFileName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'plsRestore
        '
        Me.plsRestore.Image = CType(resources.GetObject("plsRestore.Image"), System.Drawing.Image)
        Me.plsRestore.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.plsRestore.Location = New System.Drawing.Point(12, 200)
        Me.plsRestore.Name = "plsRestore"
        Me.plsRestore.Size = New System.Drawing.Size(155, 28)
        Me.plsRestore.TabIndex = 182
        Me.plsRestore.Text = "Restore from Saturday"
        Me.plsRestore.UseVisualStyleBackColor = True
        '
        'plsIgnore
        '
        Me.plsIgnore.Image = CType(resources.GetObject("plsIgnore.Image"), System.Drawing.Image)
        Me.plsIgnore.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.plsIgnore.Location = New System.Drawing.Point(185, 200)
        Me.plsIgnore.Name = "plsIgnore"
        Me.plsIgnore.Size = New System.Drawing.Size(108, 28)
        Me.plsIgnore.TabIndex = 181
        Me.plsIgnore.Text = "Continue"
        Me.plsIgnore.UseVisualStyleBackColor = True
        '
        'plsExit
        '
        Me.plsExit.Image = CType(resources.GetObject("plsExit.Image"), System.Drawing.Image)
        Me.plsExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.plsExit.Location = New System.Drawing.Point(382, 200)
        Me.plsExit.Name = "plsExit"
        Me.plsExit.Size = New System.Drawing.Size(108, 28)
        Me.plsExit.TabIndex = 183
        Me.plsExit.Text = "Cancel"
        Me.plsExit.UseVisualStyleBackColor = True
        '
        'txtError
        '
        Me.txtError.Location = New System.Drawing.Point(12, 97)
        Me.txtError.Multiline = True
        Me.txtError.Name = "txtError"
        Me.txtError.ReadOnly = True
        Me.txtError.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtError.Size = New System.Drawing.Size(479, 96)
        Me.txtError.TabIndex = 220
        '
        'frmFileDamaged
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(503, 240)
        Me.Controls.Add(Me.txtError)
        Me.Controls.Add(Me.plsExit)
        Me.Controls.Add(Me.plsRestore)
        Me.Controls.Add(Me.plsIgnore)
        Me.Controls.Add(Me.lblFileName)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.PictureBox1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmFileDamaged"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "frmFileDamaged"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblFileName As System.Windows.Forms.Label
    Friend WithEvents plsRestore As System.Windows.Forms.Button
    Friend WithEvents plsIgnore As System.Windows.Forms.Button
    Friend WithEvents plsExit As System.Windows.Forms.Button
    Friend WithEvents txtError As System.Windows.Forms.TextBox
End Class
