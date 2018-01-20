Imports System
Imports System.Data
Imports System.Globalization
Public Class frm_pendaftaran
    Dim id As Integer
    Dim buttonStat As New ButtonState
    Dim en As New CultureInfo("en-US")
    ' Public FormPasienBaru As New frm_pasien_baru
    Private Sub frm_pendaftaran_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Awal()
        Call Load_Poli()
        Call Load_dokter()
        Call Load_pasien()
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

        CmbPasien.Text = ""
        txtNama.Text = ""
        txtAlamat.Text = ""
        cmbJK.SelectedIndex = -1
        txtUmur.Text = ""
        txtTelp.Text = ""
        txtNoDaftar.Text = ""
        txtTanggal.Text = ""
        cmbPoli.Text = ""
        CmbDok.Text = ""
        txtBiaya.Text = "0"


        CmbPasien.Enabled = False
        txtNama.Enabled = False
        txtAlamat.Enabled = False
        cmbJK.Enabled = False
        txtUmur.Enabled = False
        txtTelp.Enabled = False
        txtNoDaftar.Enabled = False
        cmbPoli.Enabled = False
        CmbDok.Enabled = False
        txtBiaya.Enabled = False
        txtTanggal.Enabled = False



        btnNew.Enabled = True
        btnSave.Enabled = False
        btnBatal.Enabled = False

        buttonStat = ButtonState.Normal
        btnNew.Text = "New"



    End Sub

    Private Sub TextNew()

        CmbPasien.Text = ""
        txtNama.Text = ""
        txtAlamat.Text = ""
        cmbJK.SelectedIndex = -1
        txtUmur.Text = ""
        txtTelp.Text = ""
        txtNoDaftar.Text = ""
        txtTanggal.Text = ""
        cmbPoli.Text = ""
        CmbDok.Text = ""
        txtBiaya.Text = "0"


        CmbPasien.Enabled = True
        txtNama.Enabled = True
        cmbJK.Enabled = True
        txtUmur.Enabled = True
        txtTelp.Enabled = True
        txtAlamat.Enabled = True
        cmbJK.Enabled = True
        txtUmur.Enabled = True
        txtTelp.Enabled = True
        txtNoDaftar.Enabled = True
        cmbPoli.Enabled = True
        CmbDok.Enabled = True
        txtBiaya.Enabled = True
        txtTanggal.Enabled = True

        btnNew.Enabled = True
        btnSave.Enabled = True
        btnBatal.Enabled = False

        btnNew.Text = "Cancel"
        txtNama.Focus()
    End Sub

    Private Sub TextEdit()
        txtNama.Enabled = True
        cmbJK.Enabled = True
        txtUmur.Enabled = True
        txtTelp.Enabled = True
        txtAlamat.Enabled = True

        btnNew.Enabled = True
        btnSave.Enabled = True
        btnBatal.Enabled = False

        btnNew.Text = "Cancel"

        txtNama.Focus()

    End Sub
    Private Sub TextHapus()
        txtNama.Enabled = False
        txtTelp.Enabled = False

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

    Private Function isControlEmpty() As Boolean
        Try
            If txtNama.Text = "" OrElse cmbJK.SelectedIndex = -1 OrElse cmbJK.SelectedIndex = 0 OrElse txtUmur.Text = "" OrElse txtUmur.Text = "0" OrElse txtTelp.Text = "" OrElse txtTelp.Text = "" Then
                Throw New Exception("Data tidak lengkap !")
            Else
                Return True
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Warning")
            Return False
        End Try
    End Function

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
                Dim isComplete As Boolean = isControlEmpty()
                Dim poli As String() = cmbPoli.Text.Split(New Char() {"/"c}, 2)
                Dim dokter As String() = CmbDok.Text.Split(New Char() {"/"c}, 2)
                Dim pasien As String() = CmbPasien.Text.Split(New Char() {"/"c}, 2)
                If isComplete Then
                    Dim Query As String = "Insert into tbl_pendaftaran(id_pendaftaran,tgl,id_dokter,id_pasien,id_poli,biaya,post)" &
                                           "values('" & txtNoDaftar.Text & "','" & Format(DateTime.Parse(txtTanggal.Text), "yyyy/MM/dd hh:mm:ss") & "','" & dokter(0) & "', " &
                                           "'" & pasien(0) & "', '" & poli(0) & "','" & txtBiaya.Text & "','0')"

                    Dim status As Integer



                    status = ExecQuery(Query)
                    If status <> 0 Then
                        MsgBox("Save Successfuly !", MsgBoxStyle.Information, "Iformasi")

                    End If

                    'Pasien Baru
                    Dim Query2 As String = "Select * from tbl_pasien where id_pasien ='" & Microsoft.VisualBasic.Left(CmbPasien.Text, 5) & "'"
                    Dim dt2 As DataTable
                    dt2 = New DataTable
                    dt2 = GetDataTable(Query2)
                    If dt2.Rows.Count = 0 Then

                        Dim Query3 As String = "Insert into tbl_pasien values " &
                                                " ('" & CmbPasien.Text & "', " &
                                                "'" & txtNama.Text & "', " &
                                                "'" & txtAlamat.Text & "', " &
                                                "'" & cmbJK.Text & "', " &
                                                "'" & txtUmur.Text & "', " &
                                                "'" & txtTelp.Text & "')"
                        ExecQuery(Query3)

                    End If

                    Awal()

                End If
            End If
        End If

        'If buttonStat = ButtonState.Edit Then
        '    Dim isEdit As Boolean = isControlEmpty()
        '    If isEdit Then
        '        Dim Query As String = "update tbl_pasien set nama = '" & txtNama.Text & "', " &
        '                              "alamat='" & txtAlamat.Text & "', " &
        '                              "jk='" & cmbJK.Text & "', " &
        '                              "umur='" & txtUmur.Text & "', " &
        '                              "telp = '" & txtTelp.Text & "' where id_pasien ='" & CmbPasien.Text & "'"
        '        Dim status As Integer = ExecQuery(Query)
        '        If status <> 0 Then
        '            MsgBox("Update Successfuly !", MsgBoxStyle.Information, "Iformasi")
        '            Awal()
        '        End If
        '    End If
        'End If

    End Sub



    Private Sub btnEdit_Click(sender As Object, e As EventArgs)
        Try
            TextEdit()
            buttonStat = ButtonState.Edit

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnBatal.Click
        Try
            buttonStat = ButtonState.Delete
            Dim Query As String = "delete from tbl_pasien where id_pasien = '" & CmbPasien.Text & "'"
            Dim status As Integer = ExecQuery(Query)
            If status <> 0 Then
                MsgBox("Delete Successfuly !", MsgBoxStyle.Information, "Iformasi")
                Awal()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub







    Private Sub txtHarga_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtUmur.KeyPress
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Not IsNumeric(e.KeyChar) Then
            MessageBox.Show("Please enter numbers only")
            e.Handled = True
        End If
    End Sub

    Private Sub txtHarga_TextChanged(sender As Object, e As EventArgs) Handles txtUmur.TextChanged

    End Sub

    Private Sub txtJumlah_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTelp.KeyPress
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Not IsNumeric(e.KeyChar) Then
            MessageBox.Show("Please enter numbers only")
            e.Handled = True
        End If
    End Sub

    Private Sub txtJumlah_TextChanged(sender As Object, e As EventArgs) Handles txtTelp.TextChanged

    End Sub

    Private Sub cmbJenis_KeyPress(sender As Object, e As KeyPressEventArgs)
        If Asc(e.KeyChar) <> 0 Then
            e.Handled = True
        End If
    End Sub

    Private Sub cmbJenis_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub cmbKategori_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbJK.KeyPress
        If Asc(e.KeyChar) <> 0 Then
            e.Handled = True
        End If
    End Sub

    Private Sub cmbKategori_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbJK.SelectedIndexChanged

    End Sub

    Private Sub GridPasien_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles GridPasien.CellContentClick

    End Sub

    Private Sub GridPasien_DoubleClick(sender As Object, e As EventArgs) Handles GridPasien.DoubleClick
        Try
            If GridPasien.RowCount > 0 Then
                CmbPasien.Text = GridPasien.SelectedRows(0).Cells(0).Value.ToString()
                txtNama.Text = GridPasien.SelectedRows(0).Cells(1).Value.ToString()
                txtAlamat.Text = GridPasien.SelectedRows(0).Cells(2).Value.ToString()
                cmbJK.Text = GridPasien.SelectedRows(0).Cells(3).Value.ToString()
                txtUmur.Text = GridPasien.SelectedRows(0).Cells(4).Value.ToString()
                txtTelp.Text = GridPasien.SelectedRows(0).Cells(5).Value.ToString()
                btnBatal.Enabled = True
                btnNew.Text = "Cancel"
            End If
        Catch ex As Exception

        End Try
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
    Private Sub Load_pasien()

        Dim dt As DataTable
        dt = New DataTable
        Dim Query As String = "Select * from tbl_pasien order by id_pasien"

        dt = GetDataTable(Query)

        For i As Integer = 0 To dt.Rows.Count - 1
            CmbPasien.Items.Add(dt.Rows.Item(i)("id_pasien") & "/" & dt.Rows.Item(i)("nama_pasien"))
        Next


    End Sub


    Private Sub txtSearch_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub cmbPoli_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbPoli.SelectedIndexChanged

    End Sub

    Private Sub txtSearch_KeyPress(sender As Object, e As KeyPressEventArgs)

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

    Private Sub CmbDok_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbDok.SelectedIndexChanged
        Dim dt As DataTable
        dt = New DataTable
        Dim Query As String = "Select * from tbl_dokter where id_dokter = '" & Microsoft.VisualBasic.Left(CmbDok.Text, 4) & "' "

        dt = GetDataTable(Query)
        txtBiaya.Text = dt.Rows(0).Item("tarif")

    End Sub

    Private Sub CmbPasien_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbPasien.SelectedIndexChanged

        Dim dt As DataTable
        dt = New DataTable
        Dim Query As String = "Select * from tbl_pasien where id_pasien = '" & Microsoft.VisualBasic.Left(CmbPasien.Text, 5) & "' "

        dt = GetDataTable(Query)
        txtNama.Text = dt.Rows(0).Item("nama_pasien")
        txtAlamat.Text = dt.Rows(0).Item("alamat")
        cmbJK.Text = dt.Rows(0).Item("jk")
        txtUmur.Text = dt.Rows(0).Item("umur")
        txtTelp.Text = dt.Rows(0).Item("telp")

    End Sub

    Private Sub CmbPasien_Leave(sender As Object, e As EventArgs) Handles CmbPasien.Leave

    End Sub

    Private Sub CmbDok_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CmbDok.KeyPress
        If Asc(e.KeyChar) <> 0 Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtBiaya_TextChanged(sender As Object, e As EventArgs) Handles txtBiaya.TextChanged

    End Sub

    Private Sub txtBiaya_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtBiaya.KeyPress
        If Asc(e.KeyChar) <> 0 Then
            e.Handled = True
        End If
    End Sub

    Private Sub CmbPasien_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CmbPasien.KeyPress
        If Asc(e.KeyChar) <> 0 Then
            e.Handled = True
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' FormPasienBaru.Show()
        frm_pasien_baru.Show()
    End Sub
End Class