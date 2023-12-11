<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmExpenses
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmExpenses))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.panMenu = New System.Windows.Forms.Panel()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.panOreLavoro = New System.Windows.Forms.Panel()
        Me.plsPrevious = New System.Windows.Forms.Button()
        Me.lblWY = New System.Windows.Forms.Label()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.lblRiporti = New System.Windows.Forms.Label()
        Me.txtRiporti = New System.Windows.Forms.TextBox()
        Me.lblEmployeeName = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.dgv = New System.Windows.Forms.DataGridView()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.chkHotel = New System.Windows.Forms.CheckBox()
        Me.chkEstero = New System.Windows.Forms.CheckBox()
        Me.chkItaly = New System.Windows.Forms.CheckBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtCity = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtKm = New System.Windows.Forms.TextBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtValuta = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtCartaCredito = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtVarie = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtAlloggio = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtVitto = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtMezziPubblici = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtAutostrada = New System.Windows.Forms.TextBox()
        Me.plsFilter = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboOrderName = New System.Windows.Forms.ComboBox()
        Me.plsNext = New System.Windows.Forms.Button()
        Me.lblData = New System.Windows.Forms.Label()
        Me.dtpDate = New System.Windows.Forms.DateTimePicker()
        Me.mnu = New OreLavoroDipendenti.ucMenu()
        Me.panMenu.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.panOreLavoro.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'panMenu
        '
        Me.panMenu.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panMenu.Controls.Add(Me.mnu)
        Me.panMenu.Location = New System.Drawing.Point(4, 3)
        Me.panMenu.Name = "panMenu"
        Me.panMenu.Size = New System.Drawing.Size(136, 620)
        Me.panMenu.TabIndex = 146
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
        Me.Panel4.Location = New System.Drawing.Point(52, 768)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(124, 51)
        Me.Panel4.TabIndex = 149
        '
        'panOreLavoro
        '
        Me.panOreLavoro.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panOreLavoro.Controls.Add(Me.plsPrevious)
        Me.panOreLavoro.Controls.Add(Me.lblWY)
        Me.panOreLavoro.Controls.Add(Me.GroupBox4)
        Me.panOreLavoro.Controls.Add(Me.lblEmployeeName)
        Me.panOreLavoro.Controls.Add(Me.Label15)
        Me.panOreLavoro.Controls.Add(Me.dgv)
        Me.panOreLavoro.Controls.Add(Me.GroupBox3)
        Me.panOreLavoro.Controls.Add(Me.GroupBox2)
        Me.panOreLavoro.Controls.Add(Me.GroupBox1)
        Me.panOreLavoro.Controls.Add(Me.plsFilter)
        Me.panOreLavoro.Controls.Add(Me.Label1)
        Me.panOreLavoro.Controls.Add(Me.cboOrderName)
        Me.panOreLavoro.Controls.Add(Me.plsNext)
        Me.panOreLavoro.Controls.Add(Me.lblData)
        Me.panOreLavoro.Controls.Add(Me.dtpDate)
        Me.panOreLavoro.Location = New System.Drawing.Point(146, 3)
        Me.panOreLavoro.Name = "panOreLavoro"
        Me.panOreLavoro.Size = New System.Drawing.Size(895, 620)
        Me.panOreLavoro.TabIndex = 150
        '
        'plsPrevious
        '
        Me.plsPrevious.Cursor = System.Windows.Forms.Cursors.Hand
        Me.plsPrevious.Image = CType(resources.GetObject("plsPrevious.Image"), System.Drawing.Image)
        Me.plsPrevious.Location = New System.Drawing.Point(470, 4)
        Me.plsPrevious.Name = "plsPrevious"
        Me.plsPrevious.Size = New System.Drawing.Size(25, 26)
        Me.plsPrevious.TabIndex = 247
        Me.plsPrevious.TabStop = False
        Me.plsPrevious.UseVisualStyleBackColor = True
        '
        'lblWY
        '
        Me.lblWY.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblWY.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWY.Location = New System.Drawing.Point(522, 7)
        Me.lblWY.Name = "lblWY"
        Me.lblWY.Size = New System.Drawing.Size(56, 20)
        Me.lblWY.TabIndex = 246
        Me.lblWY.Text = "Week 52"
        Me.lblWY.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.lblRiporti)
        Me.GroupBox4.Controls.Add(Me.txtRiporti)
        Me.GroupBox4.Location = New System.Drawing.Point(555, 39)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(309, 47)
        Me.GroupBox4.TabIndex = 241
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Dati"
        '
        'lblRiporti
        '
        Me.lblRiporti.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblRiporti.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRiporti.Location = New System.Drawing.Point(6, 18)
        Me.lblRiporti.Name = "lblRiporti"
        Me.lblRiporti.Size = New System.Drawing.Size(79, 20)
        Me.lblRiporti.TabIndex = 237
        Me.lblRiporti.Text = "Riporti attuali"
        Me.lblRiporti.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtRiporti
        '
        Me.txtRiporti.Location = New System.Drawing.Point(85, 18)
        Me.txtRiporti.Name = "txtRiporti"
        Me.txtRiporti.ReadOnly = True
        Me.txtRiporti.Size = New System.Drawing.Size(68, 20)
        Me.txtRiporti.TabIndex = 238
        Me.txtRiporti.TabStop = False
        Me.txtRiporti.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblEmployeeName
        '
        Me.lblEmployeeName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblEmployeeName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEmployeeName.Location = New System.Drawing.Point(41, 7)
        Me.lblEmployeeName.Name = "lblEmployeeName"
        Me.lblEmployeeName.Size = New System.Drawing.Size(207, 20)
        Me.lblEmployeeName.TabIndex = 240
        Me.lblEmployeeName.Text = "Nome"
        Me.lblEmployeeName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label15
        '
        Me.Label15.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(3, 7)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(38, 20)
        Me.Label15.TabIndex = 239
        Me.Label15.Text = "Nome"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'dgv
        '
        Me.dgv.AllowUserToAddRows = False
        Me.dgv.AllowUserToDeleteRows = False
        Me.dgv.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.Lavender
        Me.dgv.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgv.DefaultCellStyle = DataGridViewCellStyle3
        Me.dgv.Location = New System.Drawing.Point(5, 143)
        Me.dgv.Name = "dgv"
        Me.dgv.ReadOnly = True
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv.RowHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.dgv.RowTemplate.Height = 16
        Me.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgv.Size = New System.Drawing.Size(859, 470)
        Me.dgv.TabIndex = 11
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Label13)
        Me.GroupBox3.Controls.Add(Me.Label12)
        Me.GroupBox3.Controls.Add(Me.Label11)
        Me.GroupBox3.Controls.Add(Me.chkHotel)
        Me.GroupBox3.Controls.Add(Me.chkEstero)
        Me.GroupBox3.Controls.Add(Me.chkItaly)
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Controls.Add(Me.txtCity)
        Me.GroupBox3.Controls.Add(Me.Label3)
        Me.GroupBox3.Controls.Add(Me.txtKm)
        Me.GroupBox3.Location = New System.Drawing.Point(5, 39)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(536, 47)
        Me.GroupBox3.TabIndex = 2
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Località"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(460, 6)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(67, 15)
        Me.Label13.TabIndex = 239
        Me.Label13.Text = "Pernottamento"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(403, 6)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(34, 15)
        Me.Label12.TabIndex = 238
        Me.Label12.Text = "Estero"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(356, 6)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(39, 15)
        Me.Label11.TabIndex = 237
        Me.Label11.Text = "Italia"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'chkHotel
        '
        Me.chkHotel.Appearance = System.Windows.Forms.Appearance.Button
        Me.chkHotel.BackgroundImage = CType(resources.GetObject("chkHotel.BackgroundImage"), System.Drawing.Image)
        Me.chkHotel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.chkHotel.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkHotel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.chkHotel.FlatAppearance.BorderSize = 0
        Me.chkHotel.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.chkHotel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.chkHotel.Location = New System.Drawing.Point(473, 21)
        Me.chkHotel.Name = "chkHotel"
        Me.chkHotel.Size = New System.Drawing.Size(34, 23)
        Me.chkHotel.TabIndex = 232
        Me.chkHotel.TabStop = False
        Me.chkHotel.UseVisualStyleBackColor = True
        '
        'chkEstero
        '
        Me.chkEstero.Appearance = System.Windows.Forms.Appearance.Button
        Me.chkEstero.BackgroundImage = CType(resources.GetObject("chkEstero.BackgroundImage"), System.Drawing.Image)
        Me.chkEstero.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.chkEstero.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkEstero.Cursor = System.Windows.Forms.Cursors.Hand
        Me.chkEstero.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.chkEstero.FlatAppearance.BorderSize = 0
        Me.chkEstero.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.chkEstero.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.chkEstero.Location = New System.Drawing.Point(403, 21)
        Me.chkEstero.Name = "chkEstero"
        Me.chkEstero.Size = New System.Drawing.Size(34, 23)
        Me.chkEstero.TabIndex = 231
        Me.chkEstero.TabStop = False
        Me.chkEstero.UseVisualStyleBackColor = True
        '
        'chkItaly
        '
        Me.chkItaly.Appearance = System.Windows.Forms.Appearance.Button
        Me.chkItaly.BackgroundImage = CType(resources.GetObject("chkItaly.BackgroundImage"), System.Drawing.Image)
        Me.chkItaly.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.chkItaly.Cursor = System.Windows.Forms.Cursors.Hand
        Me.chkItaly.FlatAppearance.BorderColor = System.Drawing.Color.Red
        Me.chkItaly.FlatAppearance.BorderSize = 0
        Me.chkItaly.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.chkItaly.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.chkItaly.Location = New System.Drawing.Point(356, 21)
        Me.chkItaly.Name = "chkItaly"
        Me.chkItaly.Size = New System.Drawing.Size(39, 23)
        Me.chkItaly.TabIndex = 230
        Me.chkItaly.TabStop = False
        Me.chkItaly.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(13, 19)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(38, 20)
        Me.Label2.TabIndex = 223
        Me.Label2.Text = "Città"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtCity
        '
        Me.txtCity.Location = New System.Drawing.Point(51, 19)
        Me.txtCity.Name = "txtCity"
        Me.txtCity.Size = New System.Drawing.Size(224, 20)
        Me.txtCity.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(285, 19)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(25, 20)
        Me.Label3.TabIndex = 225
        Me.Label3.Text = "Km"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtKm
        '
        Me.txtKm.Location = New System.Drawing.Point(310, 19)
        Me.txtKm.Name = "txtKm"
        Me.txtKm.Size = New System.Drawing.Size(41, 20)
        Me.txtKm.TabIndex = 3
        Me.txtKm.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Controls.Add(Me.txtValuta)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.txtCartaCredito)
        Me.GroupBox2.Location = New System.Drawing.Point(661, 90)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(203, 47)
        Me.GroupBox2.TabIndex = 4
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Anticipi"
        '
        'Label10
        '
        Me.Label10.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(105, 18)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(39, 20)
        Me.Label10.TabIndex = 239
        Me.Label10.Text = "Valuta"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtValuta
        '
        Me.txtValuta.Location = New System.Drawing.Point(144, 18)
        Me.txtValuta.Name = "txtValuta"
        Me.txtValuta.Size = New System.Drawing.Size(49, 20)
        Me.txtValuta.TabIndex = 10
        Me.txtValuta.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label9
        '
        Me.Label9.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(6, 18)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(39, 20)
        Me.Label9.TabIndex = 237
        Me.Label9.Text = "Carta"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtCartaCredito
        '
        Me.txtCartaCredito.Location = New System.Drawing.Point(45, 18)
        Me.txtCartaCredito.Name = "txtCartaCredito"
        Me.txtCartaCredito.Size = New System.Drawing.Size(49, 20)
        Me.txtCartaCredito.TabIndex = 9
        Me.txtCartaCredito.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.txtVarie)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.txtAlloggio)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.txtVitto)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.txtMezziPubblici)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtAutostrada)
        Me.GroupBox1.Location = New System.Drawing.Point(7, 90)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(571, 47)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Spese"
        '
        'Label8
        '
        Me.Label8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(467, 18)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(39, 20)
        Me.Label8.TabIndex = 235
        Me.Label8.Text = "Varie"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtVarie
        '
        Me.txtVarie.Location = New System.Drawing.Point(506, 18)
        Me.txtVarie.Name = "txtVarie"
        Me.txtVarie.Size = New System.Drawing.Size(49, 20)
        Me.txtVarie.TabIndex = 8
        Me.txtVarie.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label7
        '
        Me.Label7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(352, 18)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(60, 20)
        Me.Label7.TabIndex = 233
        Me.Label7.Text = "Alloggio"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtAlloggio
        '
        Me.txtAlloggio.Location = New System.Drawing.Point(412, 18)
        Me.txtAlloggio.Name = "txtAlloggio"
        Me.txtAlloggio.Size = New System.Drawing.Size(49, 20)
        Me.txtAlloggio.TabIndex = 7
        Me.txtAlloggio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label6
        '
        Me.Label6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(258, 19)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(38, 20)
        Me.Label6.TabIndex = 231
        Me.Label6.Text = "Vitto"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtVitto
        '
        Me.txtVitto.Location = New System.Drawing.Point(296, 19)
        Me.txtVitto.Name = "txtVitto"
        Me.txtVitto.Size = New System.Drawing.Size(49, 20)
        Me.txtVitto.TabIndex = 6
        Me.txtVitto.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label5
        '
        Me.Label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(128, 19)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(76, 20)
        Me.Label5.TabIndex = 229
        Me.Label5.Text = "Mezzi pubblici"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtMezziPubblici
        '
        Me.txtMezziPubblici.Location = New System.Drawing.Point(204, 19)
        Me.txtMezziPubblici.Name = "txtMezziPubblici"
        Me.txtMezziPubblici.Size = New System.Drawing.Size(49, 20)
        Me.txtMezziPubblici.TabIndex = 5
        Me.txtMezziPubblici.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label4
        '
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(12, 19)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(60, 20)
        Me.Label4.TabIndex = 227
        Me.Label4.Text = "Autostrada"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtAutostrada
        '
        Me.txtAutostrada.Location = New System.Drawing.Point(72, 19)
        Me.txtAutostrada.Name = "txtAutostrada"
        Me.txtAutostrada.Size = New System.Drawing.Size(49, 20)
        Me.txtAutostrada.TabIndex = 4
        Me.txtAutostrada.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'plsFilter
        '
        Me.plsFilter.Cursor = System.Windows.Forms.Cursors.Hand
        Me.plsFilter.Image = CType(resources.GetObject("plsFilter.Image"), System.Drawing.Image)
        Me.plsFilter.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.plsFilter.Location = New System.Drawing.Point(864, 4)
        Me.plsFilter.Name = "plsFilter"
        Me.plsFilter.Size = New System.Drawing.Size(25, 26)
        Me.plsFilter.TabIndex = 222
        Me.plsFilter.TabStop = False
        Me.plsFilter.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(580, 7)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(60, 20)
        Me.Label1.TabIndex = 221
        Me.Label1.Text = "Commessa"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboOrderName
        '
        Me.cboOrderName.BackColor = System.Drawing.SystemColors.Window
        Me.cboOrderName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboOrderName.Location = New System.Drawing.Point(640, 7)
        Me.cboOrderName.MaxLength = 32
        Me.cboOrderName.Name = "cboOrderName"
        Me.cboOrderName.Size = New System.Drawing.Size(220, 21)
        Me.cboOrderName.TabIndex = 1
        '
        'plsNext
        '
        Me.plsNext.Cursor = System.Windows.Forms.Cursors.Hand
        Me.plsNext.Image = CType(resources.GetObject("plsNext.Image"), System.Drawing.Image)
        Me.plsNext.Location = New System.Drawing.Point(496, 4)
        Me.plsNext.Name = "plsNext"
        Me.plsNext.Size = New System.Drawing.Size(25, 26)
        Me.plsNext.TabIndex = 218
        Me.plsNext.TabStop = False
        Me.plsNext.UseVisualStyleBackColor = True
        '
        'lblData
        '
        Me.lblData.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblData.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblData.Location = New System.Drawing.Point(260, 7)
        Me.lblData.Name = "lblData"
        Me.lblData.Size = New System.Drawing.Size(33, 20)
        Me.lblData.TabIndex = 205
        Me.lblData.Text = "Data"
        Me.lblData.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'dtpDate
        '
        Me.dtpDate.Location = New System.Drawing.Point(293, 7)
        Me.dtpDate.Name = "dtpDate"
        Me.dtpDate.Size = New System.Drawing.Size(175, 20)
        Me.dtpDate.TabIndex = 0
        Me.dtpDate.Value = New Date(2023, 7, 13, 0, 0, 0, 0)
        '
        'mnu
        '
        Me.mnu.Location = New System.Drawing.Point(-1, -2)
        Me.mnu.Name = "mnu"
        Me.mnu.Size = New System.Drawing.Size(135, 619)
        Me.mnu.TabIndex = 1
        '
        'frmExpenses
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1044, 633)
        Me.Controls.Add(Me.panOreLavoro)
        Me.Controls.Add(Me.panMenu)
        Me.Controls.Add(Me.Panel4)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmExpenses"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "frmExpenses"
        Me.panMenu.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.panOreLavoro.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents panMenu As Panel
    Friend WithEvents Label24 As Label
    Friend WithEvents Label25 As Label
    Friend WithEvents Panel4 As Panel
    Friend WithEvents panOreLavoro As Panel
    Friend WithEvents plsNext As Button
    Friend WithEvents lblData As Label
    Friend WithEvents dtpDate As DateTimePicker
    Friend WithEvents mnu As ucMenu
    Friend WithEvents plsFilter As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents cboOrderName As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label8 As Label
    Friend WithEvents txtVarie As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents txtAlloggio As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents txtVitto As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents txtMezziPubblici As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents txtAutostrada As TextBox
    Friend WithEvents txtKm As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents txtCity As TextBox
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Label9 As Label
    Friend WithEvents txtCartaCredito As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents txtValuta As TextBox
    Private WithEvents dgv As DataGridView
    Friend WithEvents chkEstero As CheckBox
    Friend WithEvents chkItaly As CheckBox
    Friend WithEvents Label15 As Label
    Friend WithEvents lblEmployeeName As Label
    Friend WithEvents chkHotel As CheckBox
    Friend WithEvents Label11 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents lblRiporti As Label
    Friend WithEvents txtRiporti As TextBox
    Friend WithEvents lblWY As Label
    Friend WithEvents plsPrevious As Button
End Class
