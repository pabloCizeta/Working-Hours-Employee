<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmWorkingDiary
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmWorkingDiary))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.panOreLavoro = New System.Windows.Forms.Panel()
        Me.plsPrevious = New System.Windows.Forms.Button()
        Me.lblWY = New System.Windows.Forms.Label()
        Me.lblInfo = New System.Windows.Forms.Label()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.lblTotaleOre = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lblEmployeeName = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.lblTotaleOreStraordinario = New System.Windows.Forms.Label()
        Me.lblTotaleOreViaggio = New System.Windows.Forms.Label()
        Me.lblTotaleOreLavoro = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.plsFilter = New System.Windows.Forms.Button()
        Me.plsNext = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cboSector = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cboActivity = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtOreViaggio = New System.Windows.Forms.TextBox()
        Me.txtOreLavoro = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboOrderName = New System.Windows.Forms.ComboBox()
        Me.lblData = New System.Windows.Forms.Label()
        Me.dtpDate = New System.Windows.Forms.DateTimePicker()
        Me.dgvOreLavoro = New System.Windows.Forms.DataGridView()
        Me.panMenu = New System.Windows.Forms.Panel()
        Me.mnu = New OreLavoroDipendenti.ucMenu()
        Me.panOreLavoro.SuspendLayout()
        CType(Me.dgvOreLavoro, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panMenu.SuspendLayout()
        Me.SuspendLayout()
        '
        'panOreLavoro
        '
        Me.panOreLavoro.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panOreLavoro.Controls.Add(Me.plsPrevious)
        Me.panOreLavoro.Controls.Add(Me.lblWY)
        Me.panOreLavoro.Controls.Add(Me.lblInfo)
        Me.panOreLavoro.Controls.Add(Me.Label10)
        Me.panOreLavoro.Controls.Add(Me.Label8)
        Me.panOreLavoro.Controls.Add(Me.lblTotaleOre)
        Me.panOreLavoro.Controls.Add(Me.Label9)
        Me.panOreLavoro.Controls.Add(Me.lblEmployeeName)
        Me.panOreLavoro.Controls.Add(Me.Label15)
        Me.panOreLavoro.Controls.Add(Me.lblTotaleOreStraordinario)
        Me.panOreLavoro.Controls.Add(Me.lblTotaleOreViaggio)
        Me.panOreLavoro.Controls.Add(Me.lblTotaleOreLavoro)
        Me.panOreLavoro.Controls.Add(Me.Label14)
        Me.panOreLavoro.Controls.Add(Me.Label13)
        Me.panOreLavoro.Controls.Add(Me.Label12)
        Me.panOreLavoro.Controls.Add(Me.plsFilter)
        Me.panOreLavoro.Controls.Add(Me.plsNext)
        Me.panOreLavoro.Controls.Add(Me.Label7)
        Me.panOreLavoro.Controls.Add(Me.cboSector)
        Me.panOreLavoro.Controls.Add(Me.Label6)
        Me.panOreLavoro.Controls.Add(Me.cboActivity)
        Me.panOreLavoro.Controls.Add(Me.Label5)
        Me.panOreLavoro.Controls.Add(Me.Label4)
        Me.panOreLavoro.Controls.Add(Me.Label3)
        Me.panOreLavoro.Controls.Add(Me.txtOreViaggio)
        Me.panOreLavoro.Controls.Add(Me.txtOreLavoro)
        Me.panOreLavoro.Controls.Add(Me.Label2)
        Me.panOreLavoro.Controls.Add(Me.Label1)
        Me.panOreLavoro.Controls.Add(Me.cboOrderName)
        Me.panOreLavoro.Controls.Add(Me.lblData)
        Me.panOreLavoro.Controls.Add(Me.dtpDate)
        Me.panOreLavoro.Controls.Add(Me.dgvOreLavoro)
        Me.panOreLavoro.Location = New System.Drawing.Point(146, 4)
        Me.panOreLavoro.Name = "panOreLavoro"
        Me.panOreLavoro.Size = New System.Drawing.Size(673, 635)
        Me.panOreLavoro.TabIndex = 144
        '
        'plsPrevious
        '
        Me.plsPrevious.Cursor = System.Windows.Forms.Cursors.Hand
        Me.plsPrevious.Image = CType(resources.GetObject("plsPrevious.Image"), System.Drawing.Image)
        Me.plsPrevious.Location = New System.Drawing.Point(226, 36)
        Me.plsPrevious.Name = "plsPrevious"
        Me.plsPrevious.Size = New System.Drawing.Size(25, 26)
        Me.plsPrevious.TabIndex = 248
        Me.plsPrevious.TabStop = False
        Me.plsPrevious.UseVisualStyleBackColor = True
        '
        'lblWY
        '
        Me.lblWY.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblWY.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWY.Location = New System.Drawing.Point(282, 40)
        Me.lblWY.Name = "lblWY"
        Me.lblWY.Size = New System.Drawing.Size(56, 20)
        Me.lblWY.TabIndex = 245
        Me.lblWY.Text = "Week 52"
        Me.lblWY.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblInfo
        '
        Me.lblInfo.BackColor = System.Drawing.Color.Transparent
        Me.lblInfo.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblInfo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInfo.ImageKey = "Info.ico"
        Me.lblInfo.ImageList = Me.ImageList1
        Me.lblInfo.Location = New System.Drawing.Point(327, 12)
        Me.lblInfo.Name = "lblInfo"
        Me.lblInfo.Size = New System.Drawing.Size(20, 20)
        Me.lblInfo.TabIndex = 244
        Me.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "Icojam-Blue-Bits-Math-add.png")
        Me.ImageList1.Images.SetKeyName(1, "Math-Equal.png")
        Me.ImageList1.Images.SetKeyName(2, "Info.ico")
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ImageKey = "Math-Equal.png"
        Me.Label10.ImageList = Me.ImageList1
        Me.Label10.Location = New System.Drawing.Point(303, 607)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(20, 20)
        Me.Label10.TabIndex = 243
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ImageKey = "Icojam-Blue-Bits-Math-add.png"
        Me.Label8.ImageList = Me.ImageList1
        Me.Label8.Location = New System.Drawing.Point(140, 607)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(20, 20)
        Me.Label8.TabIndex = 242
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTotaleOre
        '
        Me.lblTotaleOre.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotaleOre.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotaleOre.Location = New System.Drawing.Point(422, 607)
        Me.lblTotaleOre.Name = "lblTotaleOre"
        Me.lblTotaleOre.Size = New System.Drawing.Size(45, 20)
        Me.lblTotaleOre.TabIndex = 241
        Me.lblTotaleOre.Text = "123.59"
        Me.lblTotaleOre.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label9
        '
        Me.Label9.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(325, 607)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(97, 20)
        Me.Label9.TabIndex = 240
        Me.Label9.Text = "Totale ore"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblEmployeeName
        '
        Me.lblEmployeeName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblEmployeeName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEmployeeName.Location = New System.Drawing.Point(56, 10)
        Me.lblEmployeeName.Name = "lblEmployeeName"
        Me.lblEmployeeName.Size = New System.Drawing.Size(238, 20)
        Me.lblEmployeeName.TabIndex = 238
        Me.lblEmployeeName.Text = "Nome"
        Me.lblEmployeeName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label15
        '
        Me.Label15.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(4, 10)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(49, 20)
        Me.Label15.TabIndex = 237
        Me.Label15.Text = "Nome"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTotaleOreStraordinario
        '
        Me.lblTotaleOreStraordinario.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotaleOreStraordinario.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotaleOreStraordinario.Location = New System.Drawing.Point(619, 607)
        Me.lblTotaleOreStraordinario.Name = "lblTotaleOreStraordinario"
        Me.lblTotaleOreStraordinario.Size = New System.Drawing.Size(45, 20)
        Me.lblTotaleOreStraordinario.TabIndex = 225
        Me.lblTotaleOreStraordinario.Text = "123.59"
        Me.lblTotaleOreStraordinario.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblTotaleOreViaggio
        '
        Me.lblTotaleOreViaggio.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotaleOreViaggio.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotaleOreViaggio.Location = New System.Drawing.Point(260, 607)
        Me.lblTotaleOreViaggio.Name = "lblTotaleOreViaggio"
        Me.lblTotaleOreViaggio.Size = New System.Drawing.Size(45, 20)
        Me.lblTotaleOreViaggio.TabIndex = 224
        Me.lblTotaleOreViaggio.Text = "123.59"
        Me.lblTotaleOreViaggio.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblTotaleOreLavoro
        '
        Me.lblTotaleOreLavoro.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotaleOreLavoro.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotaleOreLavoro.Location = New System.Drawing.Point(96, 607)
        Me.lblTotaleOreLavoro.Name = "lblTotaleOreLavoro"
        Me.lblTotaleOreLavoro.Size = New System.Drawing.Size(45, 20)
        Me.lblTotaleOreLavoro.TabIndex = 223
        Me.lblTotaleOreLavoro.Text = "123.59"
        Me.lblTotaleOreLavoro.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label14
        '
        Me.Label14.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(501, 607)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(118, 20)
        Me.Label14.TabIndex = 222
        Me.Label14.Text = "Totale ore straordinario"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label13
        '
        Me.Label13.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(163, 607)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(97, 20)
        Me.Label13.TabIndex = 221
        Me.Label13.Text = "Totale ore viaggio"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label12
        '
        Me.Label12.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(5, 607)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(92, 20)
        Me.Label12.TabIndex = 220
        Me.Label12.Text = "Totale ore lavoro"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'plsFilter
        '
        Me.plsFilter.Cursor = System.Windows.Forms.Cursors.Hand
        Me.plsFilter.Image = CType(resources.GetObject("plsFilter.Image"), System.Drawing.Image)
        Me.plsFilter.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.plsFilter.Location = New System.Drawing.Point(642, 13)
        Me.plsFilter.Name = "plsFilter"
        Me.plsFilter.Size = New System.Drawing.Size(25, 26)
        Me.plsFilter.TabIndex = 219
        Me.plsFilter.TabStop = False
        Me.plsFilter.UseVisualStyleBackColor = True
        '
        'plsNext
        '
        Me.plsNext.Cursor = System.Windows.Forms.Cursors.Hand
        Me.plsNext.Image = CType(resources.GetObject("plsNext.Image"), System.Drawing.Image)
        Me.plsNext.Location = New System.Drawing.Point(253, 36)
        Me.plsNext.Name = "plsNext"
        Me.plsNext.Size = New System.Drawing.Size(25, 26)
        Me.plsNext.TabIndex = 218
        Me.plsNext.TabStop = False
        Me.plsNext.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(349, 67)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(67, 20)
        Me.Label7.TabIndex = 217
        Me.Label7.Text = "Settore"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboSector
        '
        Me.cboSector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSector.FormattingEnabled = True
        Me.cboSector.Location = New System.Drawing.Point(416, 67)
        Me.cboSector.Name = "cboSector"
        Me.cboSector.Size = New System.Drawing.Size(220, 21)
        Me.cboSector.TabIndex = 3
        '
        'Label6
        '
        Me.Label6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(349, 39)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(67, 20)
        Me.Label6.TabIndex = 215
        Me.Label6.Text = "Attività"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboActivity
        '
        Me.cboActivity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboActivity.FormattingEnabled = True
        Me.cboActivity.Location = New System.Drawing.Point(416, 39)
        Me.cboActivity.Name = "cboActivity"
        Me.cboActivity.Size = New System.Drawing.Size(220, 21)
        Me.cboActivity.TabIndex = 2
        '
        'Label5
        '
        Me.Label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(349, 13)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(67, 20)
        Me.Label5.TabIndex = 213
        Me.Label5.Text = "Commessa"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(259, 73)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(44, 13)
        Me.Label4.TabIndex = 212
        Me.Label4.Text = "[hh:mm]"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(104, 72)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(44, 13)
        Me.Label3.TabIndex = 211
        Me.Label3.Text = "[hh:mm]"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtOreViaggio
        '
        Me.txtOreViaggio.Location = New System.Drawing.Point(218, 68)
        Me.txtOreViaggio.Name = "txtOreViaggio"
        Me.txtOreViaggio.Size = New System.Drawing.Size(37, 20)
        Me.txtOreViaggio.TabIndex = 5
        Me.txtOreViaggio.Text = "00:00"
        Me.txtOreViaggio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtOreLavoro
        '
        Me.txtOreLavoro.Location = New System.Drawing.Point(67, 67)
        Me.txtOreLavoro.Name = "txtOreLavoro"
        Me.txtOreLavoro.Size = New System.Drawing.Size(37, 20)
        Me.txtOreLavoro.TabIndex = 4
        Me.txtOreLavoro.Text = "00:00"
        Me.txtOreLavoro.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label2
        '
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(154, 68)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(64, 20)
        Me.Label2.TabIndex = 208
        Me.Label2.Text = "Ore Viaggio"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(4, 67)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(63, 20)
        Me.Label1.TabIndex = 207
        Me.Label1.Text = "Ore Lavoro"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboOrderName
        '
        Me.cboOrderName.Location = New System.Drawing.Point(416, 13)
        Me.cboOrderName.MaxLength = 32
        Me.cboOrderName.Name = "cboOrderName"
        Me.cboOrderName.Size = New System.Drawing.Size(220, 21)
        Me.cboOrderName.TabIndex = 1
        '
        'lblData
        '
        Me.lblData.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblData.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblData.Location = New System.Drawing.Point(4, 39)
        Me.lblData.Name = "lblData"
        Me.lblData.Size = New System.Drawing.Size(41, 20)
        Me.lblData.TabIndex = 205
        Me.lblData.Text = "Data"
        Me.lblData.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'dtpDate
        '
        Me.dtpDate.Location = New System.Drawing.Point(47, 39)
        Me.dtpDate.Name = "dtpDate"
        Me.dtpDate.Size = New System.Drawing.Size(177, 20)
        Me.dtpDate.TabIndex = 0
        Me.dtpDate.Value = New Date(2023, 5, 24, 0, 0, 0, 0)
        '
        'dgvOreLavoro
        '
        Me.dgvOreLavoro.AllowUserToAddRows = False
        Me.dgvOreLavoro.AllowUserToDeleteRows = False
        Me.dgvOreLavoro.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.Lavender
        Me.dgvOreLavoro.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvOreLavoro.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvOreLavoro.DefaultCellStyle = DataGridViewCellStyle3
        Me.dgvOreLavoro.Location = New System.Drawing.Point(3, 95)
        Me.dgvOreLavoro.Name = "dgvOreLavoro"
        Me.dgvOreLavoro.ReadOnly = True
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvOreLavoro.RowHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.dgvOreLavoro.RowTemplate.Height = 16
        Me.dgvOreLavoro.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvOreLavoro.Size = New System.Drawing.Size(664, 506)
        Me.dgvOreLavoro.TabIndex = 6
        '
        'panMenu
        '
        Me.panMenu.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panMenu.Controls.Add(Me.mnu)
        Me.panMenu.Location = New System.Drawing.Point(5, 4)
        Me.panMenu.Name = "panMenu"
        Me.panMenu.Size = New System.Drawing.Size(136, 637)
        Me.panMenu.TabIndex = 143
        '
        'mnu
        '
        Me.mnu.Location = New System.Drawing.Point(-1, -3)
        Me.mnu.Name = "mnu"
        Me.mnu.Size = New System.Drawing.Size(135, 636)
        Me.mnu.TabIndex = 0
        '
        'frmWorkingDiary
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(823, 644)
        Me.Controls.Add(Me.panOreLavoro)
        Me.Controls.Add(Me.panMenu)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmWorkingDiary"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "frmWorkingDiary"
        Me.panOreLavoro.ResumeLayout(False)
        Me.panOreLavoro.PerformLayout()
        CType(Me.dgvOreLavoro, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panMenu.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents panOreLavoro As Panel
    Friend WithEvents Label15 As Label
    Friend WithEvents lblTotaleOreStraordinario As Label
    Friend WithEvents lblTotaleOreViaggio As Label
    Friend WithEvents lblTotaleOreLavoro As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents plsFilter As Button
    Friend WithEvents plsNext As Button
    Friend WithEvents Label7 As Label
    Friend WithEvents cboSector As ComboBox
    Friend WithEvents Label6 As Label
    Friend WithEvents cboActivity As ComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents txtOreViaggio As TextBox
    Friend WithEvents txtOreLavoro As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents cboOrderName As ComboBox
    Friend WithEvents lblData As Label
    Friend WithEvents dtpDate As DateTimePicker
    Private WithEvents dgvOreLavoro As DataGridView
    Friend WithEvents panMenu As Panel
    Friend WithEvents mnu As ucMenu
    Friend WithEvents lblEmployeeName As Label
    Friend WithEvents lblTotaleOre As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents ImageList1 As ImageList
    Friend WithEvents Label8 As Label
    Friend WithEvents lblInfo As Label
    Friend WithEvents lblWY As Label
    Friend WithEvents plsPrevious As Button
End Class
