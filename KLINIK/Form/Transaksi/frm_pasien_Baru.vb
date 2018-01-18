Imports System
Imports System.Data.SqlClient
Public Class frm_pasien_baru
    Dim id As Integer
    Dim buttonStat As New ButtonState

    Private Sub frm_pasien_baru_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Awal()
    End Sub

    Private Sub Awal()
        TextAwal()
    End Sub
    Enum ButtonState
        Normal
        Save
        Edit
        Baru
        Delete
    End Enum
    Private Sub TextAwal()

        txtKode.Text = ""
        txtNama.Text = ""
        txtAlamat.Text = ""
        cmbJK.SelectedIndex = -1
        txtUmur.Text = ""
        txtTelp.Text = ""

        txtKode.Enabled = False
        txtNama.Enabled = False
        txtAlamat.Enabled = False
        cmbJK.Enabled = False
        txtUmur.Enabled = False
        txtTelp.Enabled = False


        btnNew.Enabled = True
        btnSave.Enabled = False


        buttonStat = ButtonState.Normal
        btnNew.Text = "New"


    End Sub

    Private Sub TextNew()
        txtNama.Text = ""
        cmbJK.SelectedIndex = -1
        txtUmur.Text = ""
        txtTelp.Text = ""
        txtAlamat.Text = ""



        txtNama.Enabled = True
        cmbJK.Enabled = True
        txtUmur.Enabled = True
        txtTelp.Enabled = True
        txtAlamat.Enabled = True

        btnNew.Enabled = True
        btnSave.Enabled = True

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


        btnNew.Text = "Cancel"

        txtNama.Focus()

    End Sub
    Private Sub TextHapus()
        txtNama.Enabled = False
        txtTelp.Enabled = False

        btnNew.Enabled = True
        btnSave.Enabled = False


        btnNew.Text = "Cancel"

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
            Dim Query1 As String = "Select * from tbl_pasien where id_pasien ='" & txtKode.Text & "'"
            Dim dt As DataTable
            dt = New DataTable
            dt = GetDataTable(Query1)
            If dt.Rows.Count > 0 Then
                MsgBox("Kode Sudah Digunakan", vbOKOnly, "Cek Data")
            Else
                'Dim isComplete As Boolean = isControlEmpty()
                'If isComplete Then
                '    Dim Query As String = "Insert into tbl_pasien(id_pasien,nama,alamat,jk,umur,telp)" &
                '                           "values('" & txtKode.Text & "','" & txtNama.Text & "','" & txtAlamat.Text & "', " &
                '                           "'" & cmbJK.Text & "', '" & txtUmur.Text & "','" & txtTelp.Text & "')"

                '    Dim status As Integer
                '    status = ExecQuery(Query)
                '    If status <> 0 Then
                '        MsgBox("Save Successfuly !", MsgBoxStyle.Information, "Iformasi")

                frm_pendaftaran.CmbPasien.Text = txtKode.Text
                frm_pendaftaran.txtNama.Text = txtNama.Text
                frm_pendaftaran.txtAlamat.Text = txtAlamat.Text
                frm_pendaftaran.cmbJK.Text = cmbJK.Text
                frm_pendaftaran.txtUmur.Text = txtUmur.Text
                frm_pendaftaran.txtTelp.Text = txtTelp.Text


                Me.Close()

                'End If
                'End If
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

    Private Sub btnDelete_Click(sender As Object, e As EventArgs)
        Try
            buttonStat = ButtonState.Delete
            Dim Query As String = "delete from tbl_pasien where id_pasien = '" & txtKode.Text & "'"
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

    Private Sub GridPasien_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)

    End Sub


    Private Sub KodeOtomatis()


        Dim Query As String = "Select * from tbl_pasien order by id_pasien desc"

        Dim dt As DataTable
        dt = New DataTable
        dt = GetDataTable(Query)
        If dt.Rows.Count = 0 Then
            txtKode.Text = "PS001"
        Else
            txtKode.Text = Val(Microsoft.VisualBasic.Mid(dt.Rows(0).Item("id_pasien").ToString, 3, 3)) + 1
            If Len(txtKode.Text) = 1 Then
                txtKode.Text = "PS00" & txtKode.Text & ""
            ElseIf Len(txtKode.Text) = 2 Then
                txtKode.Text = "PS0" & txtKode.Text & ""
            ElseIf Len(txtKode.Text) = 2 Then
                txtKode.Text = "PS" & txtKode.Text & ""

            End If
        End If

    End Sub

    Private Sub txtSearch_Click(sender As Object, e As EventArgs)

    End Sub


End Class