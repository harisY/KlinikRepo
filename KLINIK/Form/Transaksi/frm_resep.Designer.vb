﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frm_resep
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
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.txtNamaPasien = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtTotalBiaya = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.cmbPendaftaran = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtTanggal = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtNoresep = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.GridResep = New System.Windows.Forms.DataGridView()
        Me.ToolStrip2 = New System.Windows.Forms.ToolStrip()
        Me.btnNew = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnSave = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnBatal = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.CmbCariObat = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtJml = New System.Windows.Forms.TextBox()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.GridResep, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip2.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Location = New System.Drawing.Point(12, 12)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(595, 164)
        Me.TabControl1.TabIndex = 1
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.txtNamaPasien)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me.txtTotalBiaya)
        Me.TabPage1.Controls.Add(Me.Label11)
        Me.TabPage1.Controls.Add(Me.cmbPendaftaran)
        Me.TabPage1.Controls.Add(Me.Label9)
        Me.TabPage1.Controls.Add(Me.txtTanggal)
        Me.TabPage1.Controls.Add(Me.Label8)
        Me.TabPage1.Controls.Add(Me.txtNoresep)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(587, 138)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Resep Obat"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'txtNamaPasien
        '
        Me.txtNamaPasien.Location = New System.Drawing.Point(117, 92)
        Me.txtNamaPasien.Name = "txtNamaPasien"
        Me.txtNamaPasien.Size = New System.Drawing.Size(121, 20)
        Me.txtNamaPasien.TabIndex = 24
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 95)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(70, 13)
        Me.Label1.TabIndex = 25
        Me.Label1.Text = "Nama Pasien"
        '
        'txtTotalBiaya
        '
        Me.txtTotalBiaya.Location = New System.Drawing.Point(439, 10)
        Me.txtTotalBiaya.Name = "txtTotalBiaya"
        Me.txtTotalBiaya.Size = New System.Drawing.Size(121, 20)
        Me.txtTotalBiaya.TabIndex = 22
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(314, 13)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(94, 13)
        Me.Label11.TabIndex = 23
        Me.Label11.Text = "Total Biaya Resep"
        '
        'cmbPendaftaran
        '
        Me.cmbPendaftaran.FormattingEnabled = True
        Me.cmbPendaftaran.Location = New System.Drawing.Point(117, 65)
        Me.cmbPendaftaran.Name = "cmbPendaftaran"
        Me.cmbPendaftaran.Size = New System.Drawing.Size(121, 21)
        Me.cmbPendaftaran.TabIndex = 18
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(9, 40)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(46, 13)
        Me.Label9.TabIndex = 19
        Me.Label9.Text = "Tanggal"
        '
        'txtTanggal
        '
        Me.txtTanggal.Location = New System.Drawing.Point(117, 37)
        Me.txtTanggal.Name = "txtTanggal"
        Me.txtTanggal.Size = New System.Drawing.Size(121, 20)
        Me.txtTanggal.TabIndex = 17
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(9, 68)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(82, 13)
        Me.Label8.TabIndex = 16
        Me.Label8.Text = "No Pendaftaran"
        '
        'txtNoresep
        '
        Me.txtNoresep.Location = New System.Drawing.Point(117, 10)
        Me.txtNoresep.Name = "txtNoresep"
        Me.txtNoresep.Size = New System.Drawing.Size(121, 20)
        Me.txtNoresep.TabIndex = 14
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 13)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(55, 13)
        Me.Label2.TabIndex = 15
        Me.Label2.Text = "No Resep"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.GridResep)
        Me.Panel1.Controls.Add(Me.ToolStrip2)
        Me.Panel1.Location = New System.Drawing.Point(12, 206)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(587, 223)
        Me.Panel1.TabIndex = 3
        '
        'GridResep
        '
        Me.GridResep.AllowUserToAddRows = False
        Me.GridResep.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.GridResep.BackgroundColor = System.Drawing.SystemColors.Control
        Me.GridResep.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.GridResep.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridResep.Location = New System.Drawing.Point(0, 25)
        Me.GridResep.Name = "GridResep"
        Me.GridResep.ReadOnly = True
        Me.GridResep.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.GridResep.Size = New System.Drawing.Size(587, 198)
        Me.GridResep.TabIndex = 1
        '
        'ToolStrip2
        '
        Me.ToolStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnNew, Me.ToolStripSeparator1, Me.btnSave, Me.ToolStripSeparator2, Me.ToolStripSeparator3, Me.btnBatal, Me.ToolStripSeparator4})
        Me.ToolStrip2.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip2.Name = "ToolStrip2"
        Me.ToolStrip2.Size = New System.Drawing.Size(587, 25)
        Me.ToolStrip2.TabIndex = 0
        Me.ToolStrip2.Text = "ToolStrip2"
        '
        'btnNew
        '
        Me.btnNew.Image = Global.KLINIK.My.Resources.Resources.new_16x16
        Me.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(51, 22)
        Me.btnNew.Text = "New"
        Me.btnNew.ToolTipText = "New"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'btnSave
        '
        Me.btnSave.Image = Global.KLINIK.My.Resources.Resources.if_save_2639912
        Me.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(51, 22)
        Me.btnSave.Text = "Save"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 25)
        '
        'btnBatal
        '
        Me.btnBatal.Image = Global.KLINIK.My.Resources.Resources.delete_16x16
        Me.btnBatal.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnBatal.Name = "btnBatal"
        Me.btnBatal.Size = New System.Drawing.Size(53, 22)
        Me.btnBatal.Text = "Batal"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 25)
        '
        'CmbCariObat
        '
        Me.CmbCariObat.FormattingEnabled = True
        Me.CmbCariObat.Location = New System.Drawing.Point(133, 178)
        Me.CmbCariObat.Name = "CmbCariObat"
        Me.CmbCariObat.Size = New System.Drawing.Size(121, 21)
        Me.CmbCariObat.TabIndex = 27
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(14, 182)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(51, 13)
        Me.Label3.TabIndex = 26
        Me.Label3.Text = "Cari Obat"
        '
        'txtJml
        '
        Me.txtJml.Location = New System.Drawing.Point(260, 178)
        Me.txtJml.Name = "txtJml"
        Me.txtJml.Size = New System.Drawing.Size(41, 20)
        Me.txtJml.TabIndex = 26
        '
        'frm_resep
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(606, 440)
        Me.Controls.Add(Me.txtJml)
        Me.Controls.Add(Me.CmbCariObat)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.TabControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "frm_resep"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Form Resep (Apoteker)"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.GridResep, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip2.ResumeLayout(False)
        Me.ToolStrip2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ToolStrip2 As ToolStrip
    Friend WithEvents GridResep As DataGridView
    Friend WithEvents btnNew As ToolStripButton
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents btnSave As ToolStripButton
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents btnBatal As ToolStripButton
    Friend WithEvents ToolStripSeparator4 As ToolStripSeparator
    Friend WithEvents Label8 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents txtTanggal As TextBox
    Public WithEvents txtNoresep As TextBox
    Friend WithEvents txtTotalBiaya As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents cmbPendaftaran As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtNamaPasien As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents CmbCariObat As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtJml As System.Windows.Forms.TextBox
End Class
