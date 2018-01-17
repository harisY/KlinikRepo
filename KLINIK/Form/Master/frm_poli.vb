Imports System
Imports System.Data
Public Class frm_poli
    Dim id As Integer
    Dim buttonStat As New ButtonState
    Private Sub frm_poli_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
        txtidpoli.Text = ""
        txtNama.Text = ""
        txtidpoli.Enabled = False
        txtNama.Enabled = False

        btnNew.Enabled = True
        btnSave.Enabled = False
        btnEdit.Enabled = False
        btnDelete.Enabled = False

        buttonStat = ButtonState.Normal
        btnNew.Text = "New"


    End Sub

    Private Sub TextNew()
        txtidpoli.Text = ""
        txtNama.Text = ""
        'txtidpoli.Enabled = True
        txtNama.Enabled = True

        btnNew.Enabled = True
        btnSave.Enabled = True
        btnEdit.Enabled = False
        btnDelete.Enabled = False

        btnNew.Text = "Cancel"
        txtidpoli.Focus()
    End Sub

    Private Sub TextEdit()
        ' txtidpoli.Enabled = True
        txtNama.Enabled = True

        btnNew.Enabled = True
        btnSave.Enabled = True
        btnEdit.Enabled = False
        btnDelete.Enabled = False

        btnNew.Text = "Cancel"


    End Sub
    Private Sub TextHapus()
        txtidpoli.Enabled = False
        txtNama.Enabled = False

        btnNew.Enabled = True
        btnSave.Enabled = False
        btnEdit.Enabled = False
        btnDelete.Enabled = True

        btnNew.Text = "Cancel"

    End Sub
    Private Sub TampilData()
        Try
            Dim dt As New DataTable
            Dim Query = "Select id_poli Kode, nama_poli Nama from tbl_poli"
            dt = GetDataTableByCommand(Query)
            GridPoli.DataSource = dt

        Catch ex As Exception

        End Try
    End Sub

    Private Function isControlEmpty() As Boolean
        Try
            If txtNama.Text = "" Then
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
            'buttonStat = ButtonState.Save
            Dim Query1 As String = "Select * from tbl_poli where id_poli ='" & txtidpoli.Text & "'"
            Dim dt As DataTable
            dt = New DataTable
            dt = GetDataTable(Query1)
            If dt.Rows.Count > 0 Then
                MsgBox("Kode Sudah Digunakan", vbOKOnly, "Cek Data")
            Else
                Dim isComplete As Boolean = isControlEmpty()
                If isComplete Then
                    Dim Query As String = "Insert into tbl_poli(id_poli,nama_poli) values('" & txtidpoli.Text & "', '" & txtNama.Text & "')"
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
                Dim Query As String = "update tbl_poli set nama_poli = '" & txtNama.Text & "' where id_poli = '" & txtidpoli.Text & "'"
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
            buttonStat = ButtonState.Delete
            Dim Query As String = "delete from tbl_poli where id_poli = '" & txtidpoli.Text & "'"
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
                Dim Query As String = "Select id_poli Kode,nama_poli Nama from tbl_poli " &
                                        "where nama_poli like '%" & txtSearch.Text & "%'"
                Dim dt As DataTable
                dt = New DataTable
                dt = GetDataTable(Query)
                GridPoli.DataSource = dt
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

    Private Sub GridPoli_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles GridPoli.CellContentClick

    End Sub

    Private Sub GridPoli_DoubleClick(sender As Object, e As EventArgs) Handles GridPoli.DoubleClick
        Try
            If GridPoli.RowCount > 0 Then
                txtidpoli.Text = GridPoli.SelectedRows(0).Cells(0).Value.ToString()
                txtNama.Text = GridPoli.SelectedRows(0).Cells(1).Value.ToString()
                btnEdit.Enabled = True
                btnDelete.Enabled = True
                btnNew.Text = "Cancel"
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub KodeOtomatis()


        Dim Query As String = "Select * from tbl_poli order by id_poli desc"

        Dim dt As DataTable
        dt = New DataTable
        dt = GetDataTable(Query)
        If dt.Rows.Count = 0 Then
            txtidpoli.Text = "PL01"
        Else
            txtidpoli.Text = Val(Microsoft.VisualBasic.Mid(dt.Rows(0).Item("id_poli").ToString, 3, 2)) + 1
            If Len(txtidpoli.Text) = 1 Then
                txtidpoli.Text = "PL0" & txtidpoli.Text & ""
            ElseIf Len(txtidpoli.Text) = 2 Then
                txtidpoli.Text = "PL" & txtidpoli.Text & ""

            End If
        End If

    End Sub

    Private Sub txtSearch_Click(sender As Object, e As EventArgs) Handles txtSearch.Click

    End Sub
End Class