Imports System
Imports System.Data
Public Class frm_user
    Dim id As Integer
    Dim buttonStat As New ButtonState
    Private Sub frm_user_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

        txtKode.Text = ""
        txtNama.Text = ""
        txtPass.Text = ""
        cmbAkses.SelectedIndex = -1
        chkAkses.Checked = True

        txtKode.Enabled = False
        txtNama.Enabled = False
        txtPass.Enabled = False
        cmbAkses.Enabled = False
        chkAkses.Enabled = False

        btnNew.Enabled = True
        btnSave.Enabled = False
        btnEdit.Enabled = False
        btnDelete.Enabled = False

        buttonStat = ButtonState.Normal
        btnNew.Text = "New"


    End Sub

    Private Sub TextNew()

        txtKode.Text = ""
        txtNama.Text = ""
        txtPass.Text = ""
        cmbAkses.SelectedIndex = -1
        chkAkses.Checked = True

        txtNama.Enabled = True
        txtPass.Enabled = True
        cmbAkses.Enabled = True
        chkAkses.Enabled = True

        btnNew.Enabled = True
        btnSave.Enabled = True
        btnEdit.Enabled = False
        btnDelete.Enabled = False

        btnNew.Text = "Cancel"
        txtNama.Focus()
    End Sub

    Private Sub TextEdit()
        txtNama.Enabled = True
        txtPass.Enabled = True
        cmbAkses.Enabled = True
        chkAkses.Enabled = True

        btnNew.Enabled = True
        btnSave.Enabled = True
        btnEdit.Enabled = False
        btnDelete.Enabled = False

        btnNew.Text = "Cancel"

        txtNama.Focus()

    End Sub
    Private Sub TextHapus()
        txtNama.Enabled = False
        txtPass.Enabled = False
        cmbAkses.Enabled = False
        chkAkses.Enabled = False

        btnNew.Enabled = True
        btnSave.Enabled = False
        btnEdit.Enabled = False
        btnDelete.Enabled = True

        btnNew.Text = "Cancel"

    End Sub
    Private Sub TampilData()
        Try
            Dim dt As New DataTable
            Dim Query = "Select id_user Kode, nama_user Nama, pass Password, Akses Akses, status Status from tbl_user"
            dt = GetDataTableByCommand(Query)
            GridUser.DataSource = dt
        Catch ex As Exception

        End Try
    End Sub

    Private Function isControlEmpty() As Boolean
        Try
            If txtNama.Text = "" OrElse txtPass.Text = "" OrElse cmbAkses.SelectedIndex = -1 OrElse cmbAkses.SelectedIndex = 0 Then
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
            Dim isComplete As Boolean = isControlEmpty()
            If isComplete Then
                Dim Query As String = "Insert into tbl_user(id_user,nama_user,pass,akses,status)" &
                                       "values('" & txtKode.Text & "','" & txtNama.Text & "', '" & txtPass.Text & "', " &
                                       "'" & cmbAkses.Text & "', '" & chkAkses.CheckState & "')"
                Dim status As Integer
                status = ExecQuery(Query)
                If status <> 0 Then
                    MsgBox("Save Successfuly !", MsgBoxStyle.Information, "Iformasi")
                    Awal()
                End If
            End If
        End If

        If buttonStat = ButtonState.Edit Then
            Dim isEdit As Boolean = isControlEmpty()
            If isEdit Then
                Dim Query As String = "update tbl_user set nama_user = '" & txtNama.Text & "', " &
                                      "pass ='" & txtPass.Text & "', " &
                                      "akses ='" & cmbAkses.Text & "', " &
                                      "status = '" & chkAkses.CheckState & "' where id_user ='" & txtKode.Text & "'"
                Dim status As Integer = ExecQuery(Query)
                If status <> 0 Then
                    MsgBox("Update Successfuly !", MsgBoxStyle.Information, "Iformasi")
                    Awal()
                End If
            End If
        End If

    End Sub

    Private Sub GridUser_DoubleClick(sender As Object, e As EventArgs) Handles GridUser.DoubleClick
        Try
            If GridUser.RowCount > 0 Then
                txtKode.Text = GridUser.SelectedRows(0).Cells(0).Value.ToString()
                txtNama.Text = GridUser.SelectedRows(0).Cells(1).Value.ToString()
                txtPass.Text = GridUser.SelectedRows(0).Cells(2).Value.ToString()
                cmbAkses.Text = GridUser.SelectedRows(0).Cells(3).Value.ToString()
                Dim status As Boolean = Convert.ToBoolean(GridUser.SelectedRows(0).Cells(4).Value)
                chkAkses.Checked = status
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
            Dim Query As String = "delete from tbl_user where id_user = '" & txtKode.Text & "'"
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
                Dim Query As String = "Select user_id ID, user_name Nama, pass Password, Akses Akses, status Status from tbl_user " &
                                        "where user_name like '%" & txtSearch.Text & "%'"
                Dim dt As DataTable
                dt = New DataTable
                dt = GetDataTable(Query)
                GridUser.DataSource = dt
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
    Private Sub KodeOtomatis()


        Dim Query As String = "Select * from tbl_user order by id_user desc"

        Dim dt As DataTable
        dt = New DataTable
        dt = GetDataTable(Query)
        If dt.Rows.Count = 0 Then
            txtKode.Text = "Usr01"
        Else
            txtKode.Text = Val(Microsoft.VisualBasic.Mid(dt.Rows(0).Item("id_user").ToString, 4, 2)) + 1
            If Len(txtKode.Text) = 1 Then
                txtKode.Text = "Usr0" & txtKode.Text & ""
            ElseIf Len(txtKode.Text) = 2 Then
                txtKode.Text = "Usr" & txtKode.Text & ""

            End If
        End If

    End Sub

    Private Sub GridUser_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles GridUser.CellContentClick

    End Sub

    Private Sub cmbAkses_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbAkses.SelectedIndexChanged

    End Sub

    Private Sub cmbAkses_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbAkses.KeyPress

    End Sub
End Class