Imports System
Imports System.Data
Imports System.Globalization
Public Class frm_pembayaran
    Dim id As Integer
    Dim buttonStat As New ButtonState
    Dim en As New CultureInfo("en-US")
    ' Public FormPasienBaru As New frm_pasien_baru
    Private Sub frm_pembayaran_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Awal()
        Call Load_Poli()
        Call Load_dokter()
    End Sub

    Private Sub Awal()
        TextAwal()
        TampilData()
    End Sub
    Enum ButtonState
        Normal
        Save
        Edit
        Baru
        Delete
    End Enum
    Private Sub TextAwal()

        txtNoDaftar.Text = ""
        txtTanggal.Text = ""
        cmbPoli.Text = ""
        CmbDok.Text = ""


        txtNoDaftar.Enabled = False
        cmbPoli.Enabled = False
        CmbDok.Enabled = False
        txtTanggal.Enabled = False



        btnNew.Enabled = True
        btnSave.Enabled = False
        btnBatal.Enabled = False

        buttonStat = ButtonState.Normal
        btnNew.Text = "New"



    End Sub

    Private Sub TextNew()

        txtNoDaftar.Text = ""
        txtTanggal.Text = ""
        cmbPoli.Text = ""
        CmbDok.Text = ""


        txtNoDaftar.Enabled = True
        cmbPoli.Enabled = True
        CmbDok.Enabled = True
        txtTanggal.Enabled = True

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
        'Dim Query = "Select * from tbl_obat"
        Dim Query = "select a.id_Pendaftaran [No Daftar], a.tgl [Tanggal]," &
                            "b.nama_pasien [Nama Pasien], c.nama_dokter [Nama Dokter]," &
                            "d.nama_poli [Nama Poli] , a.biaya [Biaya Dokter]" &
                            " from tbl_pendaftaran a " &
                            " inner join tbl_pasien b on a.id_pasien = b.id_pasien " &
                            " inner join tbl_dokter c on a.id_dokter = c.id_dokter " &
                            " inner join tbl_poli d on a.id_poli = d.id_poli"
        dt = GetDataTableByCommand(Query)
        GridPasien.DataSource = dt
        ' Catch ex As Exception

        'End Try
    End Sub

 

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Try
            If btnNew.Text = "New" Then
                TextNew()
                buttonStat = ButtonState.Baru
                txtTanggal.Text = Format(Date.Now, "dd-MM-yyyy hh:mm:ss")
                Call KodeOtomatis()
            Else
                buttonStat = ButtonState.Normal
                Awal()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        If buttonStat = ButtonState.Baru Then
            Dim Query1 As String = "Select * from tbl_pendaftaran where id_pendaftaran ='" & txtNoDaftar.Text & "'"
            Dim dt As DataTable
            dt = New DataTable
            dt = GetDataTable(Query1)
            If dt.Rows.Count > 0 Then
                MsgBox("Kode Sudah Digunakan", vbOKOnly, "Cek Data")
            Else
                Dim poli As String() = cmbPoli.Text.Split(New Char() {"/"c}, 2)
                Dim dokter As String() = CmbDok.Text.Split(New Char() {"/"c}, 2)

                Dim Query As String = "Insert into tbl_pendaftaran(id_pendaftaran,tgl,id_dokter,id_pasien,id_poli,biaya,post)" &
                                           "values('" & txtNoDaftar.Text & "','" & Format(DateTime.Parse(txtTanggal.Text), "yyyy/MM/dd hh:mm:ss") & "','" & dokter(0) & "', " &
                                           "'" & poli(0) & "','0')"

                Dim status As Integer



                status = ExecQuery(Query)
                If status <> 0 Then
                    MsgBox("Save Successfuly !", MsgBoxStyle.Information, "Iformasi")

                End If

                Awal()

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

    Private Sub cmbKategori_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub GridPasien_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles GridPasien.CellContentClick

    End Sub

  
    Private Sub KodeOtomatis()

        Dim dt As DataTable
        dt = New DataTable

        Dim date1 As String
        date1 = Format(Now, "yy")
        Dim date2 As String
        date2 = Format(Now, "MM")

        Dim Ulang As String = date2 & "/" & date1


        Dim Query As String = "Select * from tbl_pendaftaran order by id_pendaftaran desc"

        dt = GetDataTable(Query)

        If dt.Rows.Count = 0 Then


            txtNoDaftar.Text = "TGRB/" & date2 & "/" & date1 & "/01"
        Else
            txtNoDaftar.Text = dt.Rows(0).Item("id_pendaftaran")
            txtNoDaftar.Text = Microsoft.VisualBasic.Mid(txtNoDaftar.Text, 6, 5)
            If txtNoDaftar.Text <> Ulang Then
                txtNoDaftar.Text = "TGRB/" & date2 & "/" & date1 & "/01"
            Else
                txtNoDaftar.Text = dt.Rows(0).Item("id_pendaftaran")
                txtNoDaftar.Text = Val(Microsoft.VisualBasic.Mid(txtNoDaftar.Text, 12, 2)) + 1
                If Len(txtNoDaftar.Text) = 1 Then
                    txtNoDaftar.Text = "TGRB/" & date2 & "/" & date1 & "/0" & txtNoDaftar.Text & ""
                Else
                    txtNoDaftar.Text = "TGRB/" & date2 & "/" & date1 & "/" & txtNoDaftar.Text & ""

                End If

            End If
        End If


    End Sub

    Private Sub Load_Poli()

        Dim dt As DataTable
        dt = New DataTable
        Dim Query As String = "Select * from tbl_poli order by id_poli"

        dt = GetDataTable(Query)

        For i As Integer = 0 To dt.Rows.Count - 1
            cmbPoli.Items.Add(dt.Rows.Item(i)("id_poli") & "/" & dt.Rows.Item(i)("nama_poli"))
        Next


    End Sub

    Private Sub Load_dokter()

        Dim dt As DataTable
        dt = New DataTable
        Dim Query As String = "Select * from tbl_dokter order by id_dokter"

        dt = GetDataTable(Query)

        For i As Integer = 0 To dt.Rows.Count - 1
            CmbDok.Items.Add(dt.Rows.Item(i)("id_dokter") & "/" & dt.Rows.Item(i)("nama_dokter"))
            'CmbDok.Items.Add(dt.Rows.Item(i)("id_dokter"))
        Next


    End Sub
   



    Private Sub cmbPoli_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbPoli.KeyPress
        If Asc(e.KeyChar) <> 0 Then
            e.Handled = True
        End If
    End Sub



    Private Sub cmbDokter_KeyPress(sender As Object, e As KeyPressEventArgs)
        If Asc(e.KeyChar) <> 0 Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtTanggal_TextChanged(sender As Object, e As EventArgs) Handles txtTanggal.TextChanged

    End Sub

    Private Sub txtTanggal_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTanggal.KeyPress
        If Asc(e.KeyChar) <> 0 Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtNoDaftar_TextChanged(sender As Object, e As EventArgs) Handles txtNoDaftar.TextChanged

    End Sub

    Private Sub txtNoDaftar_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNoDaftar.KeyPress
        If Asc(e.KeyChar) <> 0 Then
            e.Handled = True
        End If
    End Sub

   

   
    Private Sub CmbDok_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CmbDok.KeyPress
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


End Class