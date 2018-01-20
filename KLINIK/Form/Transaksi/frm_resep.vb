Imports System
Imports System.Data
Imports System.Globalization
Public Class frm_resep
    Dim id As Integer
    Dim buttonStat As New ButtonState
    Dim en As New CultureInfo("en-US")
    ' Public FormPasienBaru As New frm_pasien_baru
    Private Sub frm_pasien_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Awal()
        Call ColumsDataGrid()


    End Sub

    Private Sub Awal()

        TextAwal()
        Call Load_Pendaftaran()
        Call Load_obat()
        GridResep.Rows.Clear()

    End Sub
    Enum ButtonState
        Normal
        Save
        Edit
        Baru
        Delete
    End Enum
    Private Sub TextAwal()

        txtNoresep.Text = ""
        txtTanggal.Text = ""
        cmbPendaftaran.Text = ""
        txtTotalBiaya.Text = "0"
        txtNamaPasien.Text = ""


        txtNoresep.Enabled = False
        cmbPendaftaran.Enabled = False
        txtTotalBiaya.Enabled = False
        txtTanggal.Enabled = False
        txtNamaPasien.Enabled = False



        btnNew.Enabled = True
        btnSave.Enabled = False
        btnBatal.Enabled = False

        buttonStat = ButtonState.Normal
        btnNew.Text = "New"



    End Sub

    Private Sub TextNew()

        txtNoresep.Text = ""
        txtTanggal.Text = ""
        cmbPendaftaran.Text = ""
        txtTotalBiaya.Text = "0"
        txtNamaPasien.Text = ""


        txtNoresep.Enabled = True
        cmbPendaftaran.Enabled = True
        txtTotalBiaya.Enabled = True
        txtTanggal.Enabled = True
        txtNamaPasien.Enabled = True

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
        GridResep.DataSource = dt
        ' Catch ex As Exception

        'End Try
    End Sub

    Private Function isControlEmpty() As Boolean
        Try
            If txtNoresep.Text = "" OrElse cmbPendaftaran.SelectedIndex = -1 OrElse txtTanggal.Text = "0" OrElse txtTotalBiaya.Text = "" Then
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
                Call Load_Pendaftaran()
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

            If GridResep.RowCount <= 0 Then
                MsgBox("Anda Belum Melakukan Input Data", MsgBoxStyle.Critical, "Validation")
            Else

                Dim Query As String = "Insert into tbl_resep(Id_resep,id_pendaftaran,tgl,total_harga_resep)" &
                                       "values('" & txtNoresep.Text & "','" & cmbPendaftaran.Text & "','" & Format(DateTime.Parse(txtTanggal.Text), "yyyy/MM/dd hh:mm:ss") & "','" & txtTotalBiaya.Text & "')"

                ' MsgBox("Next")

                ExecQuery(Query)

                For a As Integer = 0 To GridResep.RowCount - 1

                    Dim Query2 As String = "Insert into tbl_resep_detail(Id_resep,id_obat,jumlah,harga,subtotal)" &
                                          "values('" & txtNoresep.Text & "','" & GridResep.Rows(a).Cells(0).Value & "','" & GridResep.Rows(a).Cells(2).Value & "','" & GridResep.Rows(a).Cells(3).Value & "','" & GridResep.Rows(a).Cells(4).Value & "')"

                    ExecQuery(Query2)
                Next

                ' Update Post Transaction di pendaftaran

                Dim UpdatePost As String = "update tbl_pendaftaran set Post = '1' where id_pendaftaran ='" & cmbPendaftaran.Text & "'"
                ExecQuery(UpdatePost)

                MsgBox("Save Successfuly !", MsgBoxStyle.Information, "Iformasi")

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

    Private Sub GridPasien_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles GridResep.CellContentClick

    End Sub

    Private Sub KodeOtomatis()

        Dim dt As DataTable
        dt = New DataTable

        Dim date1 As String
        date1 = Format(Now, "yy")
        Dim date2 As String
        date2 = Format(Now, "MM")

        Dim Ulang As String = date2 & "/" & date1


        Dim Query As String = "Select * from tbl_resep order by id_resep desc"

        dt = GetDataTable(Query)

        If dt.Rows.Count = 0 Then


            txtNoresep.Text = "APTK/" & date2 & "/" & date1 & "/01"
        Else
            txtNoresep.Text = dt.Rows(0).Item("id_resep")
            txtNoresep.Text = Microsoft.VisualBasic.Mid(txtNoresep.Text, 6, 5)
            If txtNoresep.Text <> Ulang Then
                txtNoresep.Text = "APTK/" & date2 & "/" & date1 & "/01"
            Else
                txtNoresep.Text = dt.Rows(0).Item("id_resep")
                txtNoresep.Text = Val(Microsoft.VisualBasic.Mid(txtNoresep.Text, 12, 2)) + 1
                If Len(txtNoresep.Text) = 1 Then
                    txtNoresep.Text = "APTK/" & date2 & "/" & date1 & "/0" & txtNoresep.Text & ""
                Else
                    txtNoresep.Text = "APTK/" & date2 & "/" & date1 & "/" & txtNoresep.Text & ""

                End If

            End If
        End If


    End Sub

    Private Sub Load_Pendaftaran()

        Dim dt As DataTable
        dt = New DataTable
        Dim Query As String = "Select * from tbl_pendaftaran where Post = '0' order by id_pendaftaran"

        dt = GetDataTable(Query)
        cmbPendaftaran.Items.Clear()
        For i As Integer = 0 To dt.Rows.Count - 1
            cmbPendaftaran.Items.Add(dt.Rows.Item(i)("id_pendaftaran"))
        Next


    End Sub

   
    Private Sub txtTanggal_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTanggal.KeyPress
        If Asc(e.KeyChar) <> 0 Then
            e.Handled = True
        End If
    End Sub

   

    Private Sub txtNoDaftar_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNoresep.KeyPress
        If Asc(e.KeyChar) <> 0 Then
            e.Handled = True
        End If
    End Sub

    Private Sub cmbPendaftaran_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbPendaftaran.KeyPress
        If Asc(e.KeyChar) <> 0 Then
            e.Handled = True
        End If
    End Sub

    Private Sub cmbPendaftaran_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbPendaftaran.SelectedIndexChanged

        Dim dt As DataTable
        dt = New DataTable
        'Dim Query As String = "Select * from tbl_pasien where id_pasien = '" & Microsoft.VisualBasic.Left(CmbPasien.Text, 5) & "' "

        Dim Query = "select a.id_Pendaftaran, a.tgl, b.nama_pasien, c.nama_dokter, d.nama_poli, a.biaya" &
                            " from tbl_pendaftaran a " &
                            " inner join tbl_pasien b on a.id_pasien = b.id_pasien " &
                            " inner join tbl_dokter c on a.id_dokter = c.id_dokter " &
                            " inner join tbl_poli d on a.id_poli = d.id_poli " &
                            " where id_pendaftaran ='" & cmbPendaftaran.Text & "'"

        dt = GetDataTable(Query)
        txtNamaPasien.Text = dt.Rows(0).Item("nama_pasien")
       

    End Sub

    Private Sub ColumsDataGrid()

        GridResep.Columns.Add(0, "Kode Obat")
        GridResep.Columns.Add(1, "Nama Obat")
        GridResep.Columns.Add(2, "Jumlah")
        GridResep.Columns.Add(3, "Harga")
        GridResep.Columns.Add(4, "Sub Total")


        'GridResep.Columns(0).Width = 100
        'GridResep.Columns(1).Width = 200
        'GridResep.Columns(2).Width = 100
        'GridResep.Columns(3).Width = 100

    End Sub

    Private Sub Load_obat()

        Dim dt As DataTable
        dt = New DataTable
        Dim Query As String = "Select * from tbl_obat order by id_obat"

        dt = GetDataTable(Query)

        For i As Integer = 0 To dt.Rows.Count - 1
            CmbCariObat.Items.Add(dt.Rows.Item(i)("id_obat") & "/" & dt.Rows.Item(i)("nama_obat"))
            'CmbDok.Items.Add(dt.Rows.Item(i)("id_dokter"))
        Next


    End Sub

    Private Sub CmbCariObat_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbCariObat.SelectedIndexChanged
        txtJml.SelectAll()
        txtJml.Focus()
    End Sub

    Private Sub txtJml_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtJml.KeyPress

        Dim tombol As Integer
        tombol = Asc(e.KeyChar)

        If Not (((tombol >= 48) And (tombol <= 57)) Or (tombol = 8) Or (tombol = 13)) Then
            e.Handled = True
        End If

        If tombol = 13 Then
            If CmbCariObat.Text = "" OrElse txtJml.Text = "" OrElse txtJml.Text = "0" Then
                MsgBox("Data masih ada yang kosong", MsgBoxStyle.Information)
                txtJml.Focus()
            Else

                Dim obat As String() = CmbCariObat.Text.Split(New Char() {"/"c}, 2)

                Dim dt As DataTable
                dt = New DataTable
                Dim Query As String = "Select * from tbl_obat where id_obat = '" & obat(0) & "'"
                dt = GetDataTable(Query)

                ' -----------------Cek Double Data di Grid ----------------------------------

                For a As Integer = 0 To GridResep.Rows.Count - 1
                    Dim Cari As String = GridResep.Rows(a).Cells(0).Value

                    If Cari = obat(0) Then
                        GridResep.Item(2, a).Value = txtJml.Text
                        GridResep.Item(4, a).Value = Val(dt.Rows(0).Item("harga") * txtJml.Text)
                        txtJml.Focus()
                        CmbCariObat.Text = ""
                        txtJml.Text = ""
                        Call Calcaulation()
                        Exit Sub
                    End If
                Next

                '------------------------------------------------------------------------------


                Dim Subtotal As Integer = Val(dt.Rows(0).Item("harga") * txtJml.Text)
                Dim row As String() = New String() {dt.Rows(0).Item("id_obat"), dt.Rows(0).Item("nama_obat"), txtJml.Text, dt.Rows(0).Item("harga"), Subtotal}

                GridResep.Rows.Add(row)
                CmbCariObat.Text = ""
                txtJml.Text = ""
                CmbCariObat.Focus()
                Call Calcaulation()
            End If
        End If
    End Sub


    Private Sub Calcaulation()

        If GridResep.RowCount > 0 Then

            Dim iTotal As Integer = 0
            'if you have the other column to get the result you  could add a new one like these above 
            For i As Integer = 0 To GridResep.RowCount - 1
                iTotal += Convert.ToInt32(GridResep.Rows(i).Cells(4).Value)
            Next
            txtTotalBiaya.Text = iTotal

            'if you have the other column to get the result you  could add a new one like these above 
        End If
    End Sub

    Private Sub txtJml_TextChanged(sender As Object, e As EventArgs) Handles txtJml.TextChanged

    End Sub

    Private Sub txtNoresep_TextChanged(sender As Object, e As EventArgs) Handles txtNoresep.TextChanged

    End Sub

    Private Sub txtTanggal_TextChanged(sender As Object, e As EventArgs) Handles txtTanggal.TextChanged

    End Sub

    Private Sub txtNamaPasien_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNamaPasien.KeyPress
        If Asc(e.KeyChar) <> 0 Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtNamaPasien_TextChanged(sender As Object, e As EventArgs) Handles txtNamaPasien.TextChanged

    End Sub

    Private Sub txtTotalBiaya_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotalBiaya.KeyPress
        If Asc(e.KeyChar) <> 0 Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtTotalBiaya_TextChanged(sender As Object, e As EventArgs) Handles txtTotalBiaya.TextChanged

    End Sub
End Class