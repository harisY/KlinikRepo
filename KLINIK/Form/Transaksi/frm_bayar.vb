Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization


Public Class frm_bayar

    Dim Total As Integer
    Dim totalBayar As Decimal
    Dim bayar As Decimal
    Dim kembali As Decimal


    Dim strCurrency As String = ""
    Dim acceptableKey As Boolean = False

    Private Sub frm_bayar_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub txtPembayaran_KeyDown(sender As Object, e As KeyEventArgs) Handles txtPembayaran.KeyDown
        If (e.KeyCode >= Keys.D0 And e.KeyCode <= Keys.D9) OrElse (e.KeyCode >= Keys.NumPad0 And e.KeyCode <= Keys.NumPad9) OrElse e.KeyCode = Keys.Back Then
            acceptableKey = True
        Else
            acceptableKey = False
        End If
    End Sub

    Private Sub txtPembayaran_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPembayaran.KeyPress


        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Not IsNumeric(e.KeyChar) Then
            e.Handled = True
        Else
            If Asc(e.KeyChar) = 13 Then

                Total = txtPembayaran.Text
                txtPembayaran.Text = Total.ToString("##,##,###.00")



                Decimal.TryParse(txtTotalPembayaran.Text, totalBayar)
                Decimal.TryParse(txtPembayaran.Text, bayar)

                If bayar < totalBayar Then

                    MsgBox("Uang Pembayaran Kurang", MsgBoxStyle.Critical, "Warning")
                Else

                    kembali = bayar - totalBayar

                    txtKembali.Text = kembali.ToString("##,##,###.00")

                End If


            End If
        End If

        




    End Sub

    Private Sub txtPembayaran_TextChanged(sender As Object, e As EventArgs) Handles txtPembayaran.TextChanged
        
    End Sub

    
    Private Sub btnBayar_Click(sender As Object, e As EventArgs) Handles btnBayar.Click



        If txtTotalPembayaran.Text = "" Or txtTotalPembayaran.Text = "0" Then

            MsgBox("Belum Ada Total Transaksi")
        ElseIf txtPembayaran.Text = "" Or txtPembayaran.Text = "0" Then
            MsgBox("Masukan Pembayaran", MsgBoxStyle.Information)

        Else

            Dim QuerySimpan As String = "Insert into tbl_pembayaran(id_transaksi,id_Pendaftaran, " &
                                                       "tgl_bayar,Biaya_adm,Biaya_Dokter," &
                                                       "Total_Obat,Total_Pembayaran,Bayar,Kembali)" &
                                                       "values " &
                                                       "('" & frm_pembayaran.txtTrans.Text & "'," &
                                                       "'" & frm_pembayaran.cmbRegistrasi.Text & "'," &
                                                       "'" & Format(DateTime.Parse(frm_pembayaran.txtTanggal.Text), "yyyy/MM/dd") & "'," &
                                                       "'" & frm_pembayaran.txtAdm.Text & "'," &
                                                       "'" & frm_pembayaran.txtBiayaDokter.Text & "'," &
                                                       "'" & frm_pembayaran.txtTotalObat.Text & "'," &
                                                       "'" & totalBayar & "'," &
                                                       "'" & bayar & "'," &
                                                       "'" & kembali & "')"



            ExecQuery(QuerySimpan)

            ' Update Post Transaction di pendaftaran

            Dim UpdatePost As String = "update tbl_pendaftaran set Post = '2' where id_pendaftaran ='" & frm_pembayaran.cmbRegistrasi.Text & "'"
            ExecQuery(UpdatePost)

            frm_pembayaran.AwalPembayaran()
            Me.Close()

        End If


       


    End Sub

    Private Sub txtKembali_TextChanged(sender As Object, e As EventArgs) Handles txtKembali.TextChanged

    End Sub

    Private Sub txtTotalPembayaran_TextChanged(sender As Object, e As EventArgs) Handles txtTotalPembayaran.TextChanged

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
End Class