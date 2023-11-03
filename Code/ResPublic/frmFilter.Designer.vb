<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFilter
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmFilter))
        Me.plsCancel = New System.Windows.Forms.Button()
        Me.plsConfirm = New System.Windows.Forms.Button()
        Me.txtFilter = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'plsCancel
        '
        Me.plsCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.plsCancel.Image = CType(resources.GetObject("plsCancel.Image"), System.Drawing.Image)
        Me.plsCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.plsCancel.Location = New System.Drawing.Point(12, 55)
        Me.plsCancel.Name = "plsCancel"
        Me.plsCancel.Size = New System.Drawing.Size(87, 28)
        Me.plsCancel.TabIndex = 273
        Me.plsCancel.Text = "Annulla"
        Me.plsCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.plsCancel.UseVisualStyleBackColor = True
        '
        'plsConfirm
        '
        Me.plsConfirm.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.plsConfirm.Image = CType(resources.GetObject("plsConfirm.Image"), System.Drawing.Image)
        Me.plsConfirm.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.plsConfirm.Location = New System.Drawing.Point(117, 55)
        Me.plsConfirm.Name = "plsConfirm"
        Me.plsConfirm.Size = New System.Drawing.Size(87, 28)
        Me.plsConfirm.TabIndex = 274
        Me.plsConfirm.Text = "Conferma"
        Me.plsConfirm.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.plsConfirm.UseVisualStyleBackColor = True
        '
        'txtFilter
        '
        Me.txtFilter.Location = New System.Drawing.Point(12, 12)
        Me.txtFilter.Name = "txtFilter"
        Me.txtFilter.Size = New System.Drawing.Size(192, 20)
        Me.txtFilter.TabIndex = 275
        '
        'frmFilter
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(214, 88)
        Me.Controls.Add(Me.txtFilter)
        Me.Controls.Add(Me.plsCancel)
        Me.Controls.Add(Me.plsConfirm)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmFilter"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "frmFilter"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents plsCancel As Button
    Friend WithEvents plsConfirm As Button
    Friend WithEvents txtFilter As TextBox
End Class
