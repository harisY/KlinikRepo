Imports System
Imports System.Data
Public Class frm_dokter
    Dim id As Integer
    Dim buttonStat As New ButtonState
    Private Sub frm_dokter_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
        txtKodeDokter.Text = ""
        txtNama.Text = ""
        txtAlamat.Text = ""
        cmbSpesialis.SelectedIndex = -1
        txtTlp.Text = ""
        txtTarif.Text = ""

        txtKodeDokter.Enabled = False
        txtNama.Enabled = False
        txtAlamat.Enabled = False
        cmbSpesialis.Enabled = False
        txtTlp.Enabled = False
        txtTarif.Enabled = False

        btnNew.Enabled = True
        btnSave.Enabled = False
        btnEdit.Enabled = False
        btnDelete.Enabled = False

        buttonStat = ButtonState.Normal
        btnNew.Text = "New"


    End Sub

    Private Sub TextNew()
        txtKodeDokter.Text = ""
        txtNama.Text = ""
        txtAlamat.Text = ""
        cmbSpesialis.SelectedIndex = -1
        txtTarif.Text = ""

        txtNama.Enabled = True
        txtAlamat.Enabled = True
        cmbSpesialis.Enabled = True
        txtTlp.Enabled = True
        txtTarif.Enabled = True

        btnNew.Enabled = True
        btnSave.Enabled = True
        btnEdit.Enabled = False
        btnDelete.Enabled = False

        btnNew.Text = "Cancel"
        txtNama.Focus()
    End Sub

    Private Sub TextEdit()
        txtNama.Enabled = True
        txtAlamat.Enabled = True
        cmbSpesialis.Enabled = True
        txtTlp.Enabled = True
        txtTarif.Enabled = True

        btnNew.Enabled = True
        btnSave.Enabled = True
        btnEdit.Enabled = False
        btnDelete.Enabled = False

        btnNew.Text = "Cancel"

        txtNama.Focus()

    End Sub
    Private Sub TextHapus()
        txtNama.Enabled = False
        txtAlamat.Enabled = False
        cmbSpesialis.Enabled = False

        btnNew.Enabled = True
        btnSave.Enabled = False
        btnEdit.Enabled = False
        btnDelete.Enabled = True

        btnNew.Text = "Cancel"

    End Sub
    Private Sub TampilData()
        Try
            Dim dt As New DataTable
            Dim Query = "Select id_dokter ID, nama Nama, spesialis Spesialis, alamat Alamat, telp Telepon, tarif Tarif from tbl_dokter"
            dt = GetDataTableByCommand(Query)
            GridDokter.DataSource = dt
            'GridDokter.Columns(0).Visible = False
        Catch ex As Exception

        End Try
    End Sub

    Private Function isControlEmpty() As Boolean
        Try
            If txtNama.Text = "" OrElse txtAlamat.Text = "" OrElse cmbSpesialis.SelectedIndex = -1 OrElse cmbSpesialis.SelectedIndex = 0 Then
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
            'buttonStat = ButtonState.Save
            Dim Query1 As String = "Select * from tbl_obat where id_obat ='" & txtKodeDokter.Text & "'"
            Dim dt As DataTable
            dt = New DataTable
            dt = GetDataTable(Query1)
            If dt.Rows.Count > 0 Then
                MsgBox("Kode Sudah Digunakan", vbOKOnly, "Cek Data")
            Else

                Dim isComplete As Boolean = isControlEmpty()
                If isComplete Then
                    Dim Query As String = "Insert into tbl_dokter(id_dokter,nama,spesialis,alamat,telp,tarif)" &
                                           "values('" & txtKodeDokter.Text & "','" & txtNama.Text & "', '" & cmbSpesialis.Text & "', " &
                                           "'" & txtAlamat.Text & "','" & txtTlp.Text & "','" & txtTarif.Text & "')"
                    Dim status As Integer
                    status = ExecQuery(Query)
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
                Dim Query As String = "update tbl_dokter set nama = '" & txtNama.Text & "', " &
                                      "spesialis = '" & cmbSpesialis.Text & "'," &
                                       "alamat='" & txtAlamat.Text & "'," &
                                       "telp = '" & txtTlp.Text & "'," &
                                       "tarif = '" & txtTarif.Text & "'" &
                                       " where id_dokter ='" & txtKodeDokter.Text & "'"
                Dim status As Integer = ExecQuery(Query)
                If status <> 0 Then
                    MsgBox("Update Successfuly !", MsgBoxStyle.Information, "Iformasi")
                    Awal()
                End If
            End If
        End If

    End Sub

    Private Sub Griddokter_DoubleClick(sender As Object, e As EventArgs) Handles GridDokter.DoubleClick
        Try
            If GridDokter.RowCount > 0 Then
                txtKodeDokter.Text = GridDokter.SelectedRows(0).Cells(0).Value.ToString()
                txtNama.Text = GridDokter.SelectedRows(0).Cells(1).Value.ToString()
                cmbSpesialis.Text = GridDokter.SelectedRows(0).Cells(2).Value.ToString()
                txtAlamat.Text = GridDokter.SelectedRows(0).Cells(3).Value.ToString()
                txtTlp.Text = GridDokter.SelectedRows(0).Cells(4).Value.ToString()

                btnEdit.Enabled = True
                btnDelete.Enabled = True
                btnNew.Text = "Cancel"
            End If
        Catch ex As Exception

        End Try
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

            buttonStat = ButtonState.Delete
            Dim Query As String = "delete from tbl_dokter where id_dokter = '" & txtKodeDokter.Text & "'"
            Dim status As Integer = ExecQuery(Query)
                If status <> 0 Then
                    MsgBox("Delete Successfuly !", MsgBoxStyle.Information, "Iformasi")
                    Awal()
                End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub SearchData()
        Try
            If txtSearch.Text <> "" Then
                Dim Query As String = "Select id_dokter ID, nama Nama, spesialis Spesialis, alamat Alamat, telp Telepon from tbl_dokter " &
                                        "where nama like '%" & txtSearch.Text & "%'"
                Dim dt As DataTable
                dt = New DataTable
                dt = GetDataTable(Query)
                GridDokter.DataSource = dt
            Else
                TampilData()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        Try
            SearchData()
        Catch ex As Exception
            MsgBox(ex.Message)

        End Try
    End Sub

    Private Sub GridDokter_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles GridDokter.CellContentClick

    End Sub

    Private Sub txtSearch_Click(sender As Object, e As EventArgs) Handles txtSearch.Click

    End Sub
    Private Sub KodeOtomatis()


        Dim Query As String = "Select * from tbl_dokter order by id_dokter desc"

        Dim dt As DataTable
        dt = New DataTable
        dt = GetDataTable(Query)
        If dt.Rows.Count = 0 Then
            txtKodeDokter.Text = "DK01"
        Else
            txtKodeDokter.Text = Val(Microsoft.VisualBasic.Mid(dt.Rows(0).Item("id_dokter").ToString, 3, 2)) + 1
            If Len(txtKodeDokter.Text) = 1 Then
                txtKodeDokter.Text = "DK0" & txtKodeDokter.Text & ""
            ElseIf Len(txtKodeDokter.Text) = 2 Then
                txtKodeDokter.Text = "DK" & txtKodeDokter.Text & ""

            End If
        End If

    End Sub

    Private Sub ToolStrip2_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles ToolStrip2.ItemClicked

    End Sub

    Private Sub cmbSpesialis_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSpesialis.SelectedIndexChanged

    End Sub

    Private Sub cmbSpesialis_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbSpesialis.KeyPress

    End Sub
End Class