Imports System
Imports System.Data
Imports System.Globalization
Public Class frm_pembayaran
    Dim id As Integer
    Dim buttonStat As New ButtonState
    Dim en As New CultureInfo("en-US")

    Private Sub frm_pembayaran_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        'Call Awal()
    End Sub
    ' Public FormPasienBaru As New frm_pasien_baru

    Private Sub frm_pembayaran_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AwalPembayaran()
        Call Load_Registrasi()
        Call ColumsDataGrid()

    End Sub

    Public Sub AwalPembayaran()
        TextAwal()
        'TampilData()
    End Sub
    Enum ButtonState
        Normal
        Save
        Edit
        Baru
        Delete
    End Enum
    Private Sub TextAwal()

        txtTrans.Text = ""
        txtTanggal.Text = ""
        cmbRegistrasi.Text = ""
        txtPasien.Text = ""
        txtTotalObat.Text = "0"
        txtBiayaDokter.Text = "0"
        txtAdm.Text = "0"
        txtTotalBayar.Text = "0"

        txtTrans.Enabled = False
        cmbRegistrasi.Enabled = False
        txtPasien.Enabled = False
        txtTanggal.Enabled = False
        txtTotalObat.Enabled = False
        txtBiayaDokter.Enabled = False
        txtAdm.Enabled = False
        txtTotalBayar.Enabled = False



        btnNew.Enabled = True
        btnSave.Enabled = False
        btnBatal.Enabled = False

        buttonStat = ButtonState.Normal
        btnNew.Text = "New"
        GridPembayaran.Rows.Clear()



    End Sub

    Private Sub TextNew()

        txtTrans.Text = ""
        txtTanggal.Text = ""
        cmbRegistrasi.Text = ""
        txtPasien.Text = ""
        txtTotalObat.Text = "0"
        txtBiayaDokter.Text = "0"
        txtAdm.Text = "0"
        txtTotalBayar.Text = "0"


        txtTrans.Enabled = True
        cmbRegistrasi.Enabled = True
        txtPasien.Enabled = True
        txtTanggal.Enabled = True
        txtTotalObat.Enabled = True
        txtBiayaDokter.Enabled = True
        txtAdm.Enabled = True
        txtTotalBayar.Enabled = True

        btnNew.Enabled = True
        btnSave.Enabled = True
        btnBatal.Enabled = False

        btnNew.Text = "Cancel"
    End Sub

    Private Sub TextEdit()

        btnNew.Enabled = True
        btnSave.Enabled = True
        btnBatal.Enabled = False

        btnNew.Text = "Cancel"


    End Sub
    Private Sub TextHapus()

        btnNew.Enabled = True
        btnSave.Enabled = False
        btnBatal.Enabled = True

        btnNew.Text = "Cancel"

    End Sub
    Private Sub TampilData()
        'Try
        Dim dt As New DataTable
        'a = tbl_pendaftaran
        'b = tbl_resep
        'c = tbl_resep_detail
        'd = tbl_obat
        'e = tbl_pasien

        Dim Query = "select c.id_obat, d.nama_obat, c.jumlah , c.harga, c.subtotal, e.nama_pasien, b.total_harga_resep,a.biaya,a.adm" &
                            " from tbl_pendaftaran a  " &
                            " inner join tbl_pasien e on a.id_pasien = e.id_pasien " &
                            " inner join tbl_resep b on a.id_pendaftaran = b.id_pendaftaran " &
                            " inner join tbl_resep_detail c on b.id_resep = c.id_resep " &
                            " inner join tbl_obat d on c.id_obat = d.id_obat where a.id_pendaftaran = '" & cmbRegistrasi.Text & "' and a.post = '1'"

        dt = GetDataTableByCommand(Query)

        txtPasien.Text = dt.Rows(0).Item("nama_pasien")
        txtTotalObat.Text = dt.Rows(0).Item("total_harga_resep")
        txtAdm.Text = dt.Rows(0).Item("adm")
        txtBiayaDokter.Text = dt.Rows(0).Item("biaya")

        GridPembayaran.Rows.Clear()

        For i As Integer = 0 To dt.Rows.Count - 1
            Dim row As String() = New String() {i + 1, dt.Rows(i).Item("id_obat"), dt.Rows(i).Item("nama_obat"), dt.Rows(i).Item("jumlah"), dt.Rows(i).Item("harga"), dt.Rows(i).Item("subtotal")}
            GridPembayaran.Rows.Add(row)
        Next



        ''GridPembayaran.DataSource = dt
        '' Catch ex As Exception

        ''End Try
    End Sub



    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Try
            If btnNew.Text = "New" Then
                TextNew()
                buttonStat = ButtonState.Baru
                txtTanggal.Text = Format(Date.Now, "dd-MM-yyyy hh:mm:ss")
                Call KodeOtomatis()
                Call Load_Registrasi()
            Else
                buttonStat = ButtonState.Normal
                AwalPembayaran()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        If buttonStat = ButtonState.Baru Then
            Dim Query1 As String = "Select * from tbl_pendaftaran where id_pendaftaran ='" & txtTrans.Text & "'"
            Dim dt As DataTable
            dt = New DataTable
            dt = GetDataTable(Query1)
            If dt.Rows.Count > 0 Then
                MsgBox("Kode Sudah Digunakan", vbOKOnly, "Cek Data")
            Else

                AwalPembayaran()

            End If
        End If

    End Sub



    Private Sub btnEdit_Click(sender As Object, e As EventArgs)
        Try
            TextEdit()
            buttonStat = ButtonState.Edit

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub txtHarga_KeyPress(sender As Object, e As KeyPressEventArgs)
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Not IsNumeric(e.KeyChar) Then
            MessageBox.Show("Please enter numbers only")
            e.Handled = True
        End If
    End Sub


    Private Sub txtJumlah_KeyPress(sender As Object, e As KeyPressEventArgs)
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Not IsNumeric(e.KeyChar) Then
            MessageBox.Show("Please enter numbers only")
            e.Handled = True
        End If
    End Sub

    Private Sub txtJumlah_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub cmbJenis_KeyPress(sender As Object, e As KeyPressEventArgs)
        If Asc(e.KeyChar) <> 0 Then
            e.Handled = True
        End If
    End Sub

    Private Sub cmbJenis_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub cmbKategori_KeyPress(sender As Object, e As KeyPressEventArgs)
        If Asc(e.KeyChar) <> 0 Then
            e.Handled = True
        End If
    End Sub


    Public Sub KodeOtomatis()

        Dim dt As DataTable
        dt = New DataTable

        Dim date1 As String
        date1 = Format(Now, "yy")
        Dim date2 As String
        date2 = Format(Now, "MM")

        Dim Ulang As String = date2 & "/" & date1


        Dim Query As String = "Select * from tbl_pembayaran order by id_transaksi desc"

        dt = GetDataTable(Query)

        If dt.Rows.Count = 0 Then

            txtTrans.Text = "FNCL/" & date2 & "/" & date1 & "/01"
        Else
            txtTrans.Text = dt.Rows(0).Item("id_transaksi")
            txtTrans.Text = Microsoft.VisualBasic.Mid(txtTrans.Text, 6, 5)
            If txtTrans.Text <> Ulang Then
                txtTrans.Text = "FNCL/" & date2 & "/" & date1 & "/01"
            Else
                txtTrans.Text = dt.Rows(0).Item("id_transaksi")
                txtTrans.Text = Val(Microsoft.VisualBasic.Mid(txtTrans.Text, 12, 2)) + 1
                If Len(txtTrans.Text) = 1 Then
                    txtTrans.Text = "FNCL/" & date2 & "/" & date1 & "/0" & txtTrans.Text & ""
                Else
                    txtTrans.Text = "FNCL/" & date2 & "/" & date1 & "/" & txtTrans.Text & ""

                End If

            End If
        End If


    End Sub

    Public Sub Load_Registrasi()

        Dim dt As DataTable
        dt = New DataTable
        Dim Query As String = "Select * from tbl_pendaftaran where post = '1' order by id_pendaftaran desc"

        dt = GetDataTable(Query)
        cmbRegistrasi.Items.Clear()
        For i As Integer = 0 To dt.Rows.Count - 1
            cmbRegistrasi.Items.Add(dt.Rows.Item(i)("id_pendaftaran"))
        Next


    End Sub

    Private Sub cmbPoli_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbRegistrasi.KeyPress
        If Asc(e.KeyChar) <> 0 Then
            e.Handled = True
        End If
    End Sub



    Private Sub cmbDokter_KeyPress(sender As Object, e As KeyPressEventArgs)
        If Asc(e.KeyChar) <> 0 Then
            e.Handled = True
        End If
    End Sub


    Private Sub txtTanggal_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTanggal.KeyPress
        If Asc(e.KeyChar) <> 0 Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtNoDaftar_TextChanged(sender As Object, e As EventArgs) Handles txtTrans.TextChanged

    End Sub

    Private Sub txtNoDaftar_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTrans.KeyPress
        If Asc(e.KeyChar) <> 0 Then
            e.Handled = True
        End If
    End Sub

   

   
    Private Sub CmbDok_KeyPress(sender As Object, e As KeyPressEventArgs)
        If Asc(e.KeyChar) <> 0 Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtBiaya_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub txtBiaya_KeyPress(sender As Object, e As KeyPressEventArgs)
        If Asc(e.KeyChar) <> 0 Then
            e.Handled = True
        End If
    End Sub

    Private Sub CmbPasien_KeyPress(sender As Object, e As KeyPressEventArgs)
        If Asc(e.KeyChar) <> 0 Then
            e.Handled = True
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        ' FormPasienBaru.Show()
        frm_pasien_baru.Show()
    End Sub


    Private Sub ColumsDataGrid()

        GridPembayaran.Columns.Add(0, "No")
        GridPembayaran.Columns.Add(1, "Kode Obat")
        GridPembayaran.Columns.Add(2, "Nama Obat")
        GridPembayaran.Columns.Add(3, "Jumlah")
        GridPembayaran.Columns.Add(4, "Harga")
        GridPembayaran.Columns.Add(5, "Sub Total")

        GridPembayaran.Columns(0).Width = 40
    End Sub


    Private Sub cmbRegistrasi_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbRegistrasi.SelectedIndexChanged

        Call TampilData()
        Dim Total As Integer

        Total = Val(txtTotalObat.Text) + Val(txtBiayaDokter.Text) + Val(txtAdm.Text)
        txtTotalBayar.Text = Total.ToString("##,##,###.00")

    End Sub

  
 
    Private Sub btnBayar_Click(sender As Object, e As EventArgs) Handles btnBayar.Click

        If txtTotalBayar.Text = "" Or txtTotalBayar.Text = "0" Then
            MsgBox("Belum Ada Inputan Data", MsgBoxStyle.Information)
        Else
            frm_bayar.txtTotalPembayaran.Text = txtTotalBayar.Text

            frm_bayar.Show()
            frm_bayar.txtPembayaran.Focus()
        End If

       




    End Sub

    Private Sub txtTotalObat_TextChanged(sender As Object, e As EventArgs) Handles txtTotalObat.TextChanged

    End Sub
End Class