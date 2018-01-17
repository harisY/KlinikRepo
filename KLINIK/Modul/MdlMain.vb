Imports System.Data.SqlClient
Imports System.Xml
Module MdlMain

    Public Enum MessageEnum As Integer
        SimpanBerhasil = 1000
        SimpanGagal = 1001
        HapusBerhasil = 3000
        HapusGagal = 3001
        PropertyKosong = 555
        ListPropertyKosong = 556
        HakAksesGagal = 2001
        HakInsertGagal = 2002
        HakUpdateGagal = 2003
        HakDeleteGagal = 2004
        HakSpesialGagal = 2005
        DataTidakKetemu = 2006
        UserPassTidakCocok = 2007
        InsertGagal = 2008
        DataTelahDigunakan = 2010
        ValidasiInsertGagal = 4000
        ValidasiUpdateGagal = 4001
        ValidasiDeleteGagal = 4002
    End Enum
    Public Enum MessageTypeEnum As Byte
        NormalMessage = 0
        ErrorMessage = 1
        NotBoxMessage = 2
    End Enum
#Region "--Global Variables--"
    Public MyAss As System.Reflection.Assembly = System.Reflection.Assembly.GetExecutingAssembly
    '# Menu Collection...
    'Public MyMenus As New Collection(Of ToolStripMenuItem)
    '# Form Collection...
    'Public MyForms As New Collection(Of Form)

    '# Server Instance...
    Public gs_Database As String = "KLINIKDB"
    Public gs_DBServer As String = "MMTNOTE02\SQLEXPRESS"
    Public gs_DBAuthMode As String = "mixed"
    Public gs_DBUserName As String = "sa"
    Public gs_DBPassword As String = "12345"
    Public gs_DBPasswordDefault As String = "sysadm"

    Public gs_LoginUserID As String = ""

    Public gs_Error As String = ""
    Public gs_SQL As String = ""
    Public ge_Error As Exception

    Public gs_FormatPecahan As String = "#,##0.####"
    Public gs_FormatDecimal As String = "#,##0" 'Hanya untuk AKC tidak pake 0 di belakang koma "#,##0.#0"
    Public gs_FormatBulat As String = "#,##0"
    Public gs_FormatSQLDate As String = "yyyy-MM-dd"
    Public gs_FormatGenNoDate As String = "yyyyMM"
    Public gs_FormatReportDate As String = "dd MMMM yyyy"
    Public gs_FormatTextDate As String = "dd MMM yyyy"
    Public Const gs_ALL As String = "ALL"

    Public gs_RptPaperName As String = "LetterHalfS2" '"1/2Letter" '"LetterHS2"
    Public gsg_RptWidth As Single = 803 'inc '850
    Public gsg_RptHeight As Single = 550 'inc '551
    Public gi_PrintIdx As Integer = -1
    Public gi_RptBatasRow As Integer = 15
    Public gs_ConfirmDetailOpen As String = "Form Detail sudah dibuka, apakah ingin menimpa dengan inputan baru?"

    Public gh_Common As InstanceVariables.CommonHelper
    Public gh_Trans As InstanceVariables.TransactionHelper

#End Region

#Region "Transaction Variables"
    Public gSC_Command As SqlCommand = Nothing
#End Region

#Region "Global Functions"
    Public Function GetMessage(ByVal msgType As MessageEnum) As String
        Select Case msgType
            Case MessageEnum.SimpanBerhasil
                Return "Data berhasil disimpan!"
            Case MessageEnum.SimpanGagal
                Return "Data gagal disimpan!"
            Case MessageEnum.HapusBerhasil
                Return "Data berhasil dihapus!"
            Case MessageEnum.HapusGagal
                Return "Data gagal dihapus!"
            Case MessageEnum.PropertyKosong
                Return "Data tidak boleh kosong!"
            Case MessageEnum.ListPropertyKosong
                Return "Data berikut tidak boleh kosong : "
            Case MessageEnum.HakAksesGagal
                Return "Akses ditolak!"
            Case MessageEnum.HakInsertGagal
                Return "Simpan data ditolak!"
            Case MessageEnum.HakUpdateGagal
                Return "Ubah data ditolak!"
            Case MessageEnum.HakDeleteGagal
                Return "Hapus data ditolak!"
            Case MessageEnum.HakSpesialGagal
                Return "Hak spesial ditolak!"
            Case MessageEnum.DataTidakKetemu
                Return "Tidak ada data!"
            Case MessageEnum.UserPassTidakCocok
                Return "Username/password tidak sesuai!"
            Case MessageEnum.InsertGagal
                Return "Data sudah terdaftar dalam system!"
            Case MessageEnum.DataTelahDigunakan
                Return "Data sudah digunakan pada proses lain!"
            Case MessageEnum.ValidasiInsertGagal
                Return "Validasi simpan data gagal!"
            Case MessageEnum.ValidasiUpdateGagal
                Return "Validasi ubah data gagal!"
            Case MessageEnum.ValidasiDeleteGagal
                Return "Validasi hapus data gagal!"
            Case Else
                Return ""
        End Select
    End Function

    Public Function GetConnString(Optional ByVal DBMS As String = "SQLServer") As String
        Select Case DBMS
            Case "SQLServer"
                If gs_DBAuthMode = "win" Then
                    Return "Data Source=" & gs_DBServer & ";Initial Catalog=" & gs_Database & ";Integrated Security=True"
                Else
                    Return "Data Source=" & gs_DBServer & ";Initial Catalog=" & gs_Database & ";User ID=" & gs_DBUserName & ";pwd=" & gs_DBPassword
                End If
            Case Else
                Return ""
        End Select
    End Function


    Public Function GetDataSet(ByVal query As String, ByRef ds As DataSet) As String
        Dim conn As SqlConnection = Nothing
        Dim da As SqlDataAdapter = Nothing
        Dim dsa As New DataSet
        gs_Error = ""
        gs_SQL = query
        Try
            If gSC_Command IsNot Nothing AndAlso IsNothing(gSC_Command) = False Then
                gSC_Command.CommandText = query
                da = New SqlClient.SqlDataAdapter(gSC_Command)
            Else
                conn = New SqlConnection(GetConnString)
                da = New SqlDataAdapter(query, conn)
            End If
            da.Fill(dsa)
            da = Nothing
            ds = dsa
        Catch ex As Exception
            gs_Error = ex.Message
            Return ex.Message
        End Try
        Return ""
    End Function
    Public Function GetDataTable(ByVal pQuery As String, Optional ByVal pTimeOut As Integer = 60) As DataTable
        Dim dta As New DataTable
        Dim da As SqlDataAdapter = Nothing
        Try
            If gh_Trans IsNot Nothing AndAlso gh_Trans.Command IsNot Nothing Then
                gh_Trans.Command.CommandType = CommandType.Text
                gh_Trans.Command.CommandText = pQuery
                gh_Trans.Command.CommandTimeout = pTimeOut
                da = New SqlClient.SqlDataAdapter(gh_Trans.Command)
                da.Fill(dta)
            Else
                'Using conn As New SqlClient.SqlConnection
                '    conn.ConnectionString = GetConnString()
                '    conn.Open()
                '    da = New SqlDataAdapter(pQuery, conn)
                '    da.Fill(dta)
                'End Using
                Using conn As New SqlClient.SqlConnection
                    conn.ConnectionString = GetConnString()
                    conn.Open()
                    Dim cmd As New SqlCommand
                    cmd.CommandType = CommandType.Text
                    cmd.CommandTimeout = pTimeOut
                    cmd.CommandText = pQuery
                    cmd.Connection = conn

                    da = New SqlDataAdapter(cmd)
                    da.Fill(dta)
                    cmd = Nothing
                End Using
            End If
            da = Nothing
            Return dta
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetDataTableV2(ByVal pQuery As String, Optional ByVal pTimeOut As Integer = 60) As DataTable
        Dim dta As New DataTable
        Dim da As SqlDataAdapter = Nothing
        Try
            If gh_Trans IsNot Nothing AndAlso gh_Trans.Command IsNot Nothing Then
                gh_Trans.Command.CommandType = CommandType.Text
                gh_Trans.Command.CommandText = pQuery
                gh_Trans.Command.CommandTimeout = pTimeOut
                da = New SqlClient.SqlDataAdapter(gh_Trans.Command)
                da.Fill(dta)
            Else
                Using conn As New SqlClient.SqlConnection
                    conn.ConnectionString = GetConnString()
                    conn.Open()
                    Dim cmd As New SqlCommand
                    cmd.CommandType = CommandType.Text
                    cmd.CommandTimeout = pTimeOut
                    cmd.CommandText = pQuery
                    cmd.Connection = conn

                    da = New SqlDataAdapter(cmd)
                    da.Fill(dta)
                    cmd = Nothing
                End Using
            End If
            da = Nothing
            Return dta
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetDataTableByCommand(ByVal pQue As String, Optional ByVal pParam() As SqlParameter = Nothing, Optional ByVal CommandTimeOut As Integer = 60) As DataTable

        Dim dta As New DataTable
        Dim da As SqlDataAdapter = Nothing
        Try
            If gh_Trans IsNot Nothing AndAlso gh_Trans.Command IsNot Nothing Then
                gh_Trans.Command.CommandType = CommandType.Text
                gh_Trans.Command.CommandText = pQue
                gh_Trans.Command.CommandTimeout = CommandTimeOut
                gh_Trans.Command.Parameters.Clear()
                If pParam IsNot Nothing Then
                    For i As Integer = 0 To pParam.Length - 1
                        gh_Trans.Command.Parameters.Add(pParam(i))
                    Next
                End If
                da = New SqlClient.SqlDataAdapter(gh_Trans.Command)
                da.Fill(dta)
            Else
                Using conn As New SqlClient.SqlConnection
                    conn.ConnectionString = GetConnString()
                    Dim cmd As New SqlCommand
                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = pQue
                    cmd.CommandTimeout = CommandTimeOut
                    cmd.Connection = conn
                    If pParam IsNot Nothing Then
                        For i As Integer = 0 To pParam.Length - 1
                            cmd.Parameters.Add(pParam(i))
                        Next
                    End If
                    conn.Open()
                    da = New SqlDataAdapter(cmd)
                    da.Fill(dta)
                End Using
            End If
            da = Nothing
            Return dta
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function ExecQuery(ByVal pQuery As String, Optional ByVal pTimeOut As Integer = 30) As Integer
        Dim pRowAff As Integer = -1
        Try
            If gh_Trans IsNot Nothing AndAlso gh_Trans.Command IsNot Nothing Then
                gh_Trans.Command.CommandType = CommandType.Text
                gh_Trans.Command.CommandText = pQuery
                gh_Trans.Command.CommandTimeout = pTimeOut
                pRowAff = gh_Trans.Command.ExecuteNonQuery()
            Else
                Using Conn1 As New SqlClient.SqlConnection
                    Conn1.ConnectionString = GetConnString()
                    Dim cmd As New SqlClient.SqlCommand(pQuery, Conn1)
                    cmd.CommandTimeout = pTimeOut
                    Conn1.Open()
                    pRowAff = cmd.ExecuteNonQuery()
                End Using
            End If
            Return pRowAff
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function ExecQueryByCommand(ByVal pQue As String, Optional ByVal pParam() As SqlParameter = Nothing, Optional ByVal pConnStr As String = "", Optional ByVal CommandTimeOut As Integer = 30) As Integer
        Dim pRowAff As Integer = -1
        gs_Error = ""
        Try
            If gSC_Command IsNot Nothing Then
                gSC_Command.CommandType = CommandType.Text
                gSC_Command.CommandText = pQue
                gSC_Command.CommandTimeout = CommandTimeOut
                gSC_Command.Parameters.Clear()
                If pParam IsNot Nothing Then
                    For i As Integer = 0 To pParam.Length - 1
                        gSC_Command.Parameters.Add(pParam(i))
                    Next
                End If
                pRowAff = gSC_Command.ExecuteNonQuery()
            Else
                Using Conn1 As New SqlClient.SqlConnection
                    Conn1.ConnectionString = GetConnString()
                    If pConnStr <> "" Then
                        Conn1.ConnectionString = pConnStr
                    End If
                    Dim cmd As New SqlCommand
                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = pQue
                    cmd.Connection = Conn1
                    If pParam IsNot Nothing Then
                        For i As Integer = 0 To pParam.Length - 1
                            cmd.Parameters.Add(pParam(i))
                        Next
                    End If
                    Conn1.Open()
                    pRowAff = cmd.ExecuteNonQuery()
                End Using
            End If
            Return pRowAff
        Catch ex As Exception
            Throw
        End Try
    End Function


    Public Function gf_SaveImage(ByVal pQuery As String, ByVal ls_SQLParam As String, ByVal ImageByte As Byte()) As Integer
        Dim pRowAff As Integer = -1
        gs_Error = ""
        gs_SQL = pQuery
        Try
            If gSC_Command IsNot Nothing AndAlso IsNothing(gSC_Command) = False Then
                gSC_Command.CommandText = pQuery
                Dim param = New SqlClient.SqlParameter(ls_SQLParam, SqlDbType.Image)
                If ImageByte Is Nothing Then
                    param.Value = DBNull.Value
                Else
                    param.Value = ImageByte
                End If
                gSC_Command.Parameters.Add(param)
                pRowAff = gSC_Command.ExecuteNonQuery()
            Else
                Using Conn1 As New SqlClient.SqlConnection
                    Conn1.ConnectionString = GetConnString()
                    Dim cmd As New SqlClient.SqlCommand(pQuery, Conn1)
                    Dim param = New SqlClient.SqlParameter(ls_SQLParam, SqlDbType.Image)
                    If ImageByte Is Nothing Then
                        param.Value = DBNull.Value
                    Else
                        param.Value = ImageByte
                    End If
                    cmd.Parameters.Add(param)
                    Conn1.Open()
                    pRowAff = cmd.ExecuteNonQuery()
                End Using
            End If
            Return pRowAff
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub ShowMessage(ByVal message As String, Optional ByVal Flag As MessageTypeEnum = MessageTypeEnum.NormalMessage, Optional ByVal ls_StackTrace As String = "")
        If ls_StackTrace <> "" Then Call WriteToErrorLog(message, gs_LoginUserID, ls_StackTrace)
    End Sub
    Public Sub WriteToErrorLog(ByVal ls_Message As String, ByVal ls_User As String, ByVal ls_StackTrace As String)
        Dim ls_Path As String = Application.StartupPath & "\Logs\"
        Dim ls_FileName As String = "logs.txt"
        Dim lb_CheckSize As Boolean = True
        Dim ll_MaxLogByte As Long = 10000000

        Try
            If Not System.IO.Directory.Exists(ls_Path) Then
                System.IO.Directory.CreateDirectory(ls_Path)
                lb_CheckSize = False
            End If

            'check the file
            Dim fs As System.IO.FileStream = New System.IO.FileStream(ls_Path & ls_FileName, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite)
            If lb_CheckSize Then
                If fs.Length > ll_MaxLogByte Then
                    fs.Close()
                    My.Computer.FileSystem.RenameFile(ls_Path & ls_FileName, DateTime.Now.ToString("yyyyMMddHHmmss") & ls_FileName)
                    fs = New System.IO.FileStream(ls_Path & ls_FileName, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite)
                End If
            End If
            Dim s As System.IO.StreamWriter = New System.IO.StreamWriter(fs)
            s.Close()
            fs.Close()

            'log it
            Dim fs1 As System.IO.FileStream = New System.IO.FileStream(ls_Path & ls_FileName, System.IO.FileMode.Append, System.IO.FileAccess.Write)
            Dim s1 As System.IO.StreamWriter = New System.IO.StreamWriter(fs1)
            Dim ls_String = ""

            ls_String &= "" & Application.ProductName & ";"
            ls_String &= "W: " & DateTime.Now.ToString() & ";"
            ls_String &= "U: " & ls_User & ";"
            ls_String &= "M: " & ls_Message & ";"
            ls_String &= IIf(ls_StackTrace.Trim <> "", "ST: " & ls_StackTrace & ";", "")
            s1.WriteLine(ls_String)
            's1.Write("User: " & ls_User & vbCrLf)
            's1.Write("Message: " & ls_Message & vbCrLf)
            's1.Write("Date/Time: " & DateTime.Now.ToString() & vbCrLf)
            's1.Write("================================================" & vbCrLf)
            s1.Close()
            fs1.Close()
        Catch ex As Exception
            Call ShowMessage("Write to Log Error !" & ex.Message, MessageTypeEnum.ErrorMessage)
        End Try
    End Sub
    Public Function NumValue(ByVal value As Object) As Double
        '# Check if value is numeric...
        If IsNumeric(value & "0") Then
            Dim numstring As String = value & ""
            'numstring = CDbl(numstring & "0") '.ToString()
            If numstring.IndexOf(".") > 0 Then
                Return CDbl(numstring & "0")
            Else
                Return CDbl(numstring & "0") / 10
            End If
        Else
            Return 0
        End If
    End Function

    Public Function ServerDate() As DateTime
        Dim svrdate As DateTime = Now
        Try
            Dim dt As DataTable = GetDataTable("SELECT dt = GETDATE()")
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then svrdate = dt.Rows(0)("dt")
            Return svrdate
        Catch ex As Exception
            ShowMessage(ex.Message, MessageTypeEnum.ErrorMessage) : Return svrdate
        End Try

    End Function

    Public Function QVal(ByVal value As Object, Optional ByVal pLongDate As Boolean = True, Optional ByVal pMiddleDate As Boolean = False) As Object
        Dim result As Object = ""
        If value Is Nothing OrElse (value = Nothing AndAlso value.ToString <> "0" AndAlso value.ToString <> "False") Then
            result = "NULL"
        ElseIf TypeOf value Is String Then
            If value.ToString.Trim.ToLower = "null" OrElse value.ToString.Trim = "" Then
                result = "NULL"
            Else
                result = "'" & CStr(value & "").Replace("'", "''") & "'"
            End If
        ElseIf TypeOf value Is Boolean Then
            If value = True Then
                result = "1"
            Else
                result = "0"
            End If
        ElseIf IsNumeric(value) Then
            result = value
        ElseIf TypeOf value Is Date Then
            If pLongDate = True Then
                result = "'" & Format(value, "yyyy-MM-dd HH:mm:ss") & "'"
            ElseIf pMiddleDate = True Then
                result = "'" & Format(value, "yyyy-MM-dd HH:mm") & "'"
            Else
                result = "'" & Format(value, "yyyy-MM-dd") & "'"
            End If
        ElseIf TypeOf value Is Image Then
            Return value
        ElseIf TypeOf value Is Byte() Then
            Return value
        End If
        Return result
    End Function
    Public Function gf_DefaultPrinterName() As String
        Dim oPS As New System.Drawing.Printing.PrinterSettings
        Dim ls_PrinterName As String
        Try
            ls_PrinterName = oPS.PrinterName
        Catch ex As System.Exception
            ls_PrinterName = ""
        Finally
            oPS = Nothing
        End Try
        Return ls_PrinterName
    End Function
    Public Function Num2Word(ByVal n As Double) As String 'max 2.147.483.647
        Dim satuan() As String
        satuan = New String() {"", "Satu", "Dua", "Tiga", "Empat", "Lima", "Enam", "Tujuh", "Delapan", "Sembilan", "Sepuluh", "Sebelas"}
        Select Case n
            Case 0 To 11
                Num2Word = satuan(Fix(n))
            Case 12 To 19
                Num2Word = Trim(Num2Word(n Mod 10)) + " Belas "
            Case 20 To 99
                Num2Word = Trim(Num2Word(Fix(n / 10))) + " Puluh " + Trim(Num2Word(n Mod 10))
            Case 100 To 199
                Num2Word = " Seratus " + Trim(Num2Word(n - 100))
            Case 200 To 999
                Num2Word = Trim(Num2Word(Fix(n / 100))) + " Ratus " + Trim(Num2Word(n Mod 100))
            Case 1000 To 1999
                Num2Word = " Seribu " + Trim(Num2Word(n - 1000))
            Case 2000 To 999999
                Num2Word = Trim(Num2Word(Fix(n / 1000))) + " Ribu " + Trim(Num2Word(n Mod 1000))
            Case 1000000 To 999999999
                Num2Word = Trim(Num2Word(Fix(n / 1000000))) + " Juta " + Trim(Num2Word(n Mod 1000000))
            Case Else
                Num2Word = Trim(Num2Word(Fix(n / 1000000000))) + " Milyar " + Trim(Num2Word(n Mod 1000000000))
        End Select
    End Function
    Public Function DataEncrypt(ByVal StringToEncrypt As String, Optional ByVal AlphaEncoding As Boolean = False) As String
        Dim ls_Results As String = "", ls_Char As String = ""
        If StringToEncrypt.Trim <> "" Then
            Try
                For i As Integer = 1 To Len(StringToEncrypt)
                    ls_Char = Asc(Mid(StringToEncrypt, i, 1))
                    ls_Results = ls_Results & Len(ls_Char) & ls_Char
                Next i
                If AlphaEncoding Then
                    StringToEncrypt = ls_Results
                    ls_Results = ""
                    For i As Integer = 1 To Len(StringToEncrypt)
                        ls_Results = ls_Results & Chr(Mid(StringToEncrypt, i, 1) + 147)
                    Next i
                End If
            Catch ex As Exception
                gs_Error = "Error encrypting string"
                Call ShowMessage(gs_Error, MessageTypeEnum.ErrorMessage)
                Return ""
            End Try
        End If
        Return ls_Results
    End Function

    Public Function HashData(ByVal s As String) As String
        'Convert the string to a byte array
        Dim bytDataToHash As Byte() = (New System.Text.UnicodeEncoding()).GetBytes(s)

        'Compute the MD5 hash algorithm
        Dim bytHashValue As Byte() = New System.Security.Cryptography.MD5CryptoServiceProvider().ComputeHash(bytDataToHash)

        Return BitConverter.ToString(bytHashValue)
    End Function

    Public Function DataDecrypt(ByVal StringToDecrypt As String, Optional ByVal AlphaDecoding As Boolean = False) As String
        Dim ls_Results As String = ""
        Dim CharCode As String
        Dim CharPos As Integer
        gs_Error = ""
        If StringToDecrypt.Trim <> "" Then
            Try
                If AlphaDecoding Then
                    ls_Results = StringToDecrypt
                    StringToDecrypt = ""
                    For i As Integer = 1 To Len(ls_Results)
                        StringToDecrypt = StringToDecrypt & (Asc(Mid(ls_Results, i, 1)) - 147)
                    Next i
                End If
                ls_Results = ""
                Do
                    CharPos = Left(StringToDecrypt, 1)
                    StringToDecrypt = Mid(StringToDecrypt, 2)
                    CharCode = Left(StringToDecrypt, CharPos)
                    StringToDecrypt = Mid(StringToDecrypt, Len(CharCode) + 1)
                    ls_Results = ls_Results & Chr(CharCode)
                Loop Until StringToDecrypt = ""
            Catch ex As Exception
                gs_Error = "Error decrypting string"
                Call ShowMessage(gs_Error, MessageTypeEnum.ErrorMessage)
                Return ""
            End Try
        End If
        Return ls_Results
    End Function
    
#End Region
#Region "Print Function"

    '''<summary>
    '''Gets the PaperSizeID from the PrinterName based on PaperSizeName.
    '''</summary>
    '''<returns></returns>
    '''<remarks></remarks>
    Public Function GetPapersizeID(ByVal pPrinterName As String, ByVal pPaperSizeName As String) As Integer
        Dim DocToPrint As New System.Drawing.Printing.PrintDocument()
        Dim iPaperSizeID As Integer = -1
        Try
            DocToPrint.PrinterSettings.PrinterName = pPrinterName '(ex."EpsonSQ-1170ESC/P2")
            For i As Integer = 0 To DocToPrint.PrinterSettings.PaperSizes.Count - 1
                Dim iRawKind As Integer
                If DocToPrint.PrinterSettings.PaperSizes(i).PaperName.ToLower.Trim = pPaperSizeName.ToLower.Trim Then
                    iRawKind = CInt(DocToPrint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(DocToPrint.PrinterSettings.PaperSizes(i)))
                    iPaperSizeID = iRawKind
                    Exit For
                End If
            Next
        Catch ex As Exception
        End Try
        Return iPaperSizeID
    End Function

#End Region

End Module