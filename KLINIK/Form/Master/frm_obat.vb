Imports System
Imports System.Data.SqlClient
Public Class frm_Obat
    Dim id As Integer
    Dim buttonStat As New ButtonState
    Dim Ds As DataSet
    Dim Da As SqlDataAdapter
    Dim dt As DataTable
    Dim cmd As SqlCommand
    Dim Dr As SqlDataReader
    Private Sub frm_obat_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Awal()

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
        txtNama.Text = ""
        cmbJenis.SelectedIndex = -1
        cmbKategori.SelectedIndex = -1
        txtHarga.Text = "0"
        txtJumlah.Text = "0"
        txtKodeObat.Text = ""

        txtNama.Enabled = False
        cmbJenis.Enabled = False
        cmbKategori.Enabled = False
        txtHarga.Enabled = False
        txtJumlah.Enabled = False
        txtKodeObat.Enabled = False


        btnNew.Enabled = True
        btnSave.Enabled = False
        btnEdit.Enabled = False
        btnDelete.Enabled = False

        buttonStat = ButtonState.Normal
        btnNew.Text = "New"
        txtKodeObat.Focus()

    End Sub

    Private Sub TextNew()
        txtKodeObat.Text = ""
        txtNama.Text = ""
        cmbJenis.SelectedIndex = -1
        cmbKategori.SelectedIndex = -1
        txtHarga.Text = "0"
        txtJumlah.Text = "0"




        txtNama.Enabled = True
        cmbJenis.Enabled = True
        cmbKategori.Enabled = True
        txtHarga.Enabled = True
        txtJumlah.Enabled = True
        'txtKodeObat.Enabled = True

        btnNew.Enabled = True
        btnSave.Enabled = True
        btnEdit.Enabled = False
        btnDelete.Enabled = False

        btnNew.Text = "Cancel"

    End Sub

    Private Sub TextEdit()
        txtNama.Enabled = True
        cmbJenis.Enabled = True
        cmbKategori.Enabled = True
        txtHarga.Enabled = True
        txtJumlah.Enabled = True

        btnNew.Enabled = True
        btnSave.Enabled = True
        btnEdit.Enabled = False
        btnDelete.Enabled = False

        btnNew.Text = "Cancel"

        txtNama.Focus()

    End Sub
    Private Sub TextHapus()
        txtNama.Enabled = False
        txtJumlah.Enabled = False
        cmbJenis.Enabled = False

        btnNew.Enabled = True
        btnSave.Enabled = False
        btnEdit.Enabled = False
        btnDelete.Enabled = True

        btnNew.Text = "Cancel"

    End Sub
    Private Sub TampilData()
        Try
            Dim dt As New DataTable
            'Dim Query = "Select * from tbl_obat"
            Dim Query = "Select id_obat Kode, nama_obat Nama, jenis Jenis, kategori Kategori, harga Harga, jumlah Jumlah from tbl_obat"
            dt = GetDataTableByCommand(Query)
            GridObat.DataSource = dt
            ' GridObat.Columns(0).Visible = False
        Catch ex As Exception

        End Try
    End Sub

    Private Function isControlEmpty() As Boolean
        Try
            If txtNama.Text = "" OrElse cmbJenis.SelectedIndex = -1 OrElse cmbJenis.SelectedIndex = 0 OrElse cmbKategori.SelectedIndex = -1 OrElse cmbKategori.SelectedIndex = 0 OrElse txtHarga.Text = "" OrElse txtHarga.Text = "0" OrElse txtJumlah.Text = "" OrElse txtJumlah.Text = "" Then
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
                txtNama.Focus()
                KodeOtomatis()
            Else
                buttonStat = ButtonState.Normal
                Awal()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        If buttonStat = ButtonState.Baru Then

            Dim Query As String = "Select * from tbl_obat where id_obat ='" & txtKodeObat.Text & "'"
            Dim dt As DataTable
            dt = New DataTable
            dt = GetDataTable(Query)
            If dt.Rows.Count > 0 Then
                MsgBox("Kode Sudah Digunakan", vbOKOnly, "Cek Data")
            Else
                Dim isComplete As Boolean = isControlEmpty()
                If isComplete Then

                    Dim Query1 As String = "Insert into tbl_obat(id_obat,nama_obat,jenis,kategori,harga,jumlah)" &
                                              "values('" & txtKodeObat.Text & "','" & txtNama.Text & "', '" & cmbJenis.Text & "', " &
                                              "'" & cmbKategori.Text & "', '" & txtHarga.Text & "','" & txtJumlah.Text & "')"

                    Dim status As Integer
                    status = ExecQuery(Query1)
                    If status <> 0 Then
                        MsgBox("Save Successfuly !", MsgBoxStyle.Information, "Iformasi")
                        Awal()
                    End If
                End If

            End If

        End If


        If buttonStat = ButtonState.Edit Then
            Dim isEdit As Boolean = isControlEmpty()
            If isEdit Then
                Dim Query As String = "update tbl_obat set nama_obat = '" & txtNama.Text & "', " &
                                      "jenis='" & cmbJenis.Text & "',kategori='" & cmbKategori.Text & "', " &
                                      "harga = '" & txtHarga.Text & "', jumlah = '" & txtJumlah.Text & "' where id_obat ='" & txtKodeObat.Text & "'"
                Dim status As Integer = ExecQuery(Query)
                If status <> 0 Then
                    MsgBox("Update Successfuly !", MsgBoxStyle.Information, "Iformasi")
                    Awal()
                End If
            End If
        End If

    End Sub

    

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        Try

            TextEdit()
            buttonStat = ButtonState.Edit


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            If id <> 0 Then
                buttonStat = ButtonState.Delete
                Dim Query As String = "delete from tbl_obat where id_obat = '" & txtKodeObat.Text & "'"
                Dim status As Integer = ExecQuery(Query)
                If status <> 0 Then
                    MsgBox("Delete Successfuly !", MsgBoxStyle.Information, "Iformasi")
                    Awal()
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub SearchData()
        Try
            If txtSearch.Text <> "" Then
                Dim Query As String = "Select id_obat Kode, nama_obat Nama, jenis Jenis, kategori Kategori, harga  Harga, jumlah Jumlah from tbl_obat " &
                                        "where nama_obat like '%" & txtSearch.Text & "%'"
                Dim dt As DataTable
                dt = New DataTable
                dt = GetDataTable(Query)
                GridObat.DataSource = dt
            Else
                TampilData()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtHarga_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtHarga.KeyPress
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Not IsNumeric(e.KeyChar) Then
            MessageBox.Show("Please enter numbers only")
            e.Handled = True
        End If
    End Sub

    Private Sub txtHarga_TextChanged(sender As Object, e As EventArgs) Handles txtHarga.TextChanged

    End Sub

    Private Sub txtJumlah_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtJumlah.KeyPress
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Not IsNumeric(e.KeyChar) Then
            MessageBox.Show("Please enter numbers only")
            e.Handled = True
        End If
    End Sub

    Private Sub txtJumlah_TextChanged(sender As Object, e As EventArgs) Handles txtJumlah.TextChanged

    End Sub

    Private Sub cmbJenis_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbJenis.KeyPress
        If Asc(e.KeyChar) <> 0 Then
            e.Handled = True
        End If
    End Sub

    Private Sub cmbJenis_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbJenis.SelectedIndexChanged

    End Sub

    Private Sub cmbKategori_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbKategori.KeyPress
        If Asc(e.KeyChar) <> 0 Then
            e.Handled = True
        End If
    End Sub

    Private Sub cmbKategori_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbKategori.SelectedIndexChanged

    End Sub

    Private Sub GridObat_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles GridObat.CellContentClick

    End Sub

 
    Private Sub GridObat_DoubleClick(sender As Object, e As EventArgs) Handles GridObat.DoubleClick
        Try
            If GridObat.RowCount > 0 Then
                'id = GridObat.SelectedRows(0).Cells(0).Value.ToString()
                txtKodeObat.Text = GridObat.SelectedRows(0).Cells(0).Value.ToString()
                txtNama.Text = GridObat.SelectedRows(0).Cells(1).Value.ToString()
                cmbJenis.Text = GridObat.SelectedRows(0).Cells(2).Value.ToString()
                cmbKategori.Text = GridObat.SelectedRows(0).Cells(3).Value.ToString()
                txtHarga.Text = GridObat.SelectedRows(0).Cells(4).Value.ToString()
                txtJumlah.Text = GridObat.SelectedRows(0).Cells(5).Value.ToString()
                btnEdit.Enabled = True
                btnDelete.Enabled = True
                btnNew.Text = "Cancel"
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtSearch_Click(sender As Object, e As EventArgs) Handles txtSearch.Click

    End Sub



    Private Sub txtKodeObat_TextChanged(sender As Object, e As EventArgs) Handles txtKodeObat.TextChanged

    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        Try
            SearchData()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub KodeOtomatis()


        Dim Query As String = "Select * from tbl_obat order by id_obat desc"

        Dim dt As DataTable
        dt = New DataTable
        dt = GetDataTable(Query)
        If dt.Rows.Count = 0 Then
            txtKodeObat.Text = "OB01"
        Else
            txtKodeObat.Text = Val(Microsoft.VisualBasic.Mid(dt.Rows(0).Item("id_obat").ToString, 3, 2)) + 1
            If Len(txtKodeObat.Text) = 1 Then
                txtKodeObat.Text = "OB0" & txtKodeObat.Text & ""
            ElseIf Len(txtKodeObat.Text) = 2 Then
                txtKodeObat.Text = "OB" & txtKodeObat.Text & ""

            End If
        End If

    End Sub

End Class