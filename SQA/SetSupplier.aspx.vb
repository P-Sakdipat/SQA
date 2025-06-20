Imports System.Data.SqlClient
Imports System.Globalization
Imports System.IO
Imports System.Net
Imports System.Net.Mail
Imports System.Web.Script.Serialization

Public Class SetSupplier
    Inherits System.Web.UI.Page
    Public X2 As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Manu") = "ExecN" Then Response.Redirect("MyWebPage.aspx")

        If Not IsNothing(Request.Cookies("FACCookie")) Then
            If Request.Cookies("FACCookie")("Name") = "" Then
                Response.Redirect("Login.aspx")
            End If
            Dim encry As New codify
                Dim xUser As String = encry.Decrypt(Request.Cookies("FACCookie")("Name"))
                Dim xCode = Request.Cookies("FACCookie")("Code")
                Dim sqltxt As String
                Dim DT As DataTable
                Dim az As New AzureVM
                sqltxt = "Select * from facMailLogin where t_code = '" & xCode & "' "
                DT = az.GetDataTable(sqltxt)

                If DT.Rows.Count = 0 Then 'PIYA----
                    'If DT.Rows.Count > 0 Then
                    ' login
                    Dim ipad As String = IPNetworking.GetIP4Address
                    sqltxt = "Insert into facLogin(t_user,t_type,t_note,t_ipad,t_date) values('" & xUser & "','1','cookie login','" & ipad & "', getdate() )"
                    az.Execute(sqltxt)

                Else
                    Response.Redirect("Login.aspx")
                End If
            Else
                Response.Redirect("Login.aspx")
        End If

        If Session("Admin") = "" Then
            Response.Redirect("Login.aspx")
        End If

        If Not Page.IsPostBack Then

            Me.iGetISCTopic()
            Me.iGetISCCompanyName()
            'Me.iGetISCMatType()
            Me.iReset()
            txtidno.Text = "ADD"
            rBL_search.SelectedIndex = 0
            Me.iGetData()

        Else

        End If
    End Sub

    Sub iGetISCTopic()  '---DropDownList Topic--
        Try
            Dim az As New AzureVM
            Dim sqltxt As String
            Dim DT As New DataTable

            sqltxt = "SELECT SQASu_Subject,SQASu_ID FROM SQASubject ORDER BY SQASu_Subject"

            DT = az.GetDataTable(sqltxt)
            If DT.Rows.Count > 0 Then
                dd_SQASu_Subject.DataSource = DT
                dd_SQASu_Subject.DataTextField = "SQASu_Subject"
                dd_SQASu_Subject.DataValueField = "SQASu_ID"
            Else
                dd_SQASu_Subject.DataSource = Nothing
            End If
            dd_SQASu_Subject.DataBind()
            dd_SQASu_Subject.Items.Insert(0, New ListItem("เลือก", "-"))

            DT.Dispose()
            DT = Nothing
            az = Nothing
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Sub iGetISCCompanyName()  '---DropDownList Company Name--
        Try
            Dim az As New AzureVM
            Dim sqltxt As String = "SELECT DISTINCT SQAM_SUBName, SQAM_SUBsName FROM SQAMaster ORDER BY SQAM_SUBName"
            Dim DT As DataTable = az.GetDataTable(sqltxt)

            If DT.Rows.Count > 0 Then
                dd_SQAM_SUBName.DataSource = DT
                dd_SQAM_SUBName.DataTextField = "SQAM_SUBName"    ' ชื่อบริษัท
                dd_SQAM_SUBName.DataValueField = "SQAM_SUBsName"  ' ชื่อย่อ (จะใช้แสดงใน input)
            Else
                dd_SQAM_SUBName.DataSource = Nothing
            End If

            dd_SQAM_SUBName.DataBind()
            dd_SQAM_SUBName.Items.Insert(0, New ListItem("เลือก", "")) ' สำหรับให้เลือกเริ่มต้น

            DT.Dispose()
            az = Nothing
        Catch ex As Exception
            Response.Write("เกิดข้อผิดพลาด: " & ex.Message)
        End Try
    End Sub

    'Sub iGetISCMatType()  '---DropDownList Mat Type--
    '    Try
    '        Dim az As New AzureVM
    '        Dim sqltxt As String
    '        Dim DT As New DataTable

    '        sqltxt = "SELECT SQAM_ProductT, SQAM_TMaterials " &
    '                   "FROM SQAMaster " &
    '                   "WHERE ISNULL(SQAM_ProductT, '') <> '' " &
    '                   "ORDER BY SQAM_ProductT"

    '        DT = az.GetDataTable(sqltxt)
    '        If DT.Rows.Count > 0 Then
    '            dd_SQAM_ProductT.DataSource = DT
    '            dd_SQAM_ProductT.DataTextField = "SQAM_ProductT"
    '            dd_SQAM_ProductT.DataValueField = "SQAM_TMaterials"
    '        Else
    '            dd_SQAM_ProductT.DataSource = Nothing
    '        End If
    '        dd_SQAM_ProductT.DataBind()
    '        dd_SQAM_ProductT.Items.Insert(0, New ListItem("เลือก", "-"))

    '        DT.Dispose()
    '        DT = Nothing
    '        az = Nothing
    '    Catch ex As Exception
    '        Response.Write(ex.Message)
    '    End Try
    'End Sub


    Sub iReset()   '-----ล้างข้อมูลหน้าเพิ่ม แก้ไข กรณีเพิ่ม

        t_SQAQ_Contact.Value = ""
        t_SQAM_SUBsName.Value = ""
        't_SQAM_TMaterials.Value = ""
        t_RefNO.Value = "" '-- รหัสเอกสาร (SQAM_SUBsName)
        dd_SQAM_SUBName.SelectedIndex = 0
        dd_SQAM_SUBName.Enabled = True  ' เปิด dropdown
        'dd_SQAM_ProductT.SelectedIndex = 0
        'rBL_ProdType.SelectedIndex = 0
        't_ComPro_Add.Value = ""
        't_ComPro_City.Value = ""
        't_ComPro_Provi.Value = ""
        't_ComPro_PCode.Value = ""
        't_ComPro_Call.Value = ""
        't_ComPro_EMail.Value = ""
        't_ComPro_ConPer.Value = ""
        't_ComPro_Other.Value = ""
        txtidno.Text = "ADD"
        dd_SQASu_Subject.SelectedIndex = 0

    End Sub



    Sub iGetData() '-----เอาข้อมูลใส่ GridView
        Try
            Dim sqltxt As String
            Dim DT As DataTable
            Dim az As New AzureVM
            Dim txt As String = ""
            Dim strVendor As String = ""

            Select Case rBL_search.SelectedIndex
                Case 0
                    strVendor = " and t1.SQAM_SUBsName Like '%" & txtSearch.Value.Trim & "%' order by t1.SQAM_SUBsName"
                Case 1
                    strVendor = " and t1.SQAQ_Contact Like '%" & txtSearch.Value.Trim & "%' order by t1.SQAQ_Contact"
                Case 2
                    strVendor = " and t1.SQAM_SUBName Like N'%" & txtSearch.Value.Trim & "%' order by t1.SQAM_SUBName"
                Case 3
                    strVendor = " and t1.SQASu_Subject Like N'%" & txtSearch.Value.Trim & "%' order by t1.SQASu_Subject"
            End Select

            txtidno.Text = "ADD"

            sqltxt = "Select t_idno,t_user,t_level,t_name from facUser where t_user = '" & Session("uname") & "' "
            DT = az.GetDataTable(sqltxt)
            If DT.Rows.Count > 0 Then
                'If Session("Admin") = "User" Then
                '    txt = " and t1.t_sale = '" & DT.Rows(0).Item("t_idno") & "' "
                'ElseIf Session("Admin") = "Admin" Then
                '    txt = ""
                'ElseIf Session("Admin") = "Exec" Then
                '    txt = " and t1.t_head = '" & DT.Rows(0).Item("t_idno") & "' "
                'ElseIf Session("Admin") = "RCI" Then
                '    txt = ""
                'Else
                '    txt = " and 1=2 "
                'End If

                sqltxt = "select t1.*, t2.SQASu_Subject from SQAQue t1 " &
                         "       left outer join SQASubject t2 on t1.SQAQ_ID = t2.SQASu_ID  " &
                         " where t1.SQAQ_Status = 1 and SQAQ_StatusSub = 0" & strVendor

                DT = az.GetDataTable(sqltxt)
                If DT.Rows.Count > 0 Then
                    example.DataSource = DT
                Else
                    example.DataSource = Nothing
                End If
                example.DataBind()


            End If
            DT.Dispose()
            DT = Nothing
            az = Nothing
        Catch ex As Exception
            X2 = ex.Message
        End Try

    End Sub

    Private Sub example_PreRender(sender As Object, e As EventArgs) Handles example.PreRender
        Try
            example.UseAccessibleHeader = True
            'example.HeaderRow.TableSection = TableRowSection.TableHeader
        Catch ex As Exception

        End Try
    End Sub

    '-----เพิ่มข้อมูลหน้าเพิ่ม แก้ไข กรณีแก้ไข
    Private Sub example_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles example.RowCommand
        Select Case e.CommandName
            Case "iEdit"
                Try

                    ' 1. Bind ข้อมูล DropDownList ก่อน
                    iGetISCCompanyName()
                    iGetISCTopic()

                    Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()
                    sb.Append("<script language='javascript'>")
                    sb.Append("$('#modalId').modal('show');")
                    sb.Append("</script>")
                    ClientScript.RegisterStartupScript(Me.[GetType](), "JSScript", sb.ToString())

                    Dim sqltxt As String
                    Dim DT As DataTable
                    Dim az As New AzureVM

                    sqltxt = "SELECT sq.SQAQ_ID, sq.SQAM_SUBName, sq.SQAQ_Contact, sq.SQAM_SUBsName, sq.SQASu_Subject, sq.SQAQ_Date, sm.SQAM_SUBsName AS subName
                    FROM SQAQue AS SQ LEFT JOIN SQAMaster AS SM ON SQ.SQAM_SUBName = SM.SQAM_SUBName WHERE SQAQ_Status = 1 AND SQ.SQAQ_ID ='" & e.CommandArgument & "'"

                    DT = az.GetDataTable(sqltxt)

                    If DT IsNot Nothing AndAlso DT.Rows.Count > 0 Then

                        Dim SS = DT.Rows(0)

                        txtidno.Text = e.CommandArgument
                        t_SQAQ_Contact.Value = DT.Rows(0).Item("SQAQ_Contact")
                        t_RefNO.Value = DT.Rows(0).Item("SQAM_SUBsName")
                        t_SQAM_SUBsName.Value = DT.Rows(0).Item("subName")
                        't_SQAM_TMaterials.Value = DT.Rows(0).Item("SQAM_TMaterials")
                        'dd_SQAM_SUBName.SelectedValue = DT.Rows(0).Item("SQAM_SUBName")

                        Dim ddl_SQAM_SUBName As String = DT.Rows(0).Item("SQAM_SUBName").ToString().Trim()

                        ' ตอนนี้ DropDown มีข้อมูลแล้ว ค่อยเลือก
                        If dd_SQAM_SUBName.Items.FindByText(ddl_SQAM_SUBName) IsNot Nothing Then
                            dd_SQAM_SUBName.SelectedItem.Text = ddl_SQAM_SUBName
                        ElseIf dd_SQAM_SUBName.Items.FindByValue(ddl_SQAM_SUBName) IsNot Nothing Then
                            dd_SQAM_SUBName.SelectedValue = ddl_SQAM_SUBName
                        Else
                            dd_SQAM_SUBName.SelectedIndex = 0 ' ไม่เจอ ให้เลือก "เลือก"
                        End If

                        'dd_SQAM_ProductT.SelectedIndex = DT.Rows(0).Item("SQAM_ProductT")
                        'If DT.Rows(0).Item("ComPro_Type") = "Factory" Then rBL_ProdType.SelectedIndex = 0 Else rBL_ProdType.SelectedIndex = 1
                        't_ComPro_Add.Value = DT.Rows(0).Item("ComPro_Add")
                        't_ComPro_City.Value = DT.Rows(0).Item("ComPro_City")
                        't_ComPro_Provi.Value = DT.Rows(0).Item("ComPro_Provi")
                        't_ComPro_PCode.Value = DT.Rows(0).Item("ComPro_PCode")
                        't_ComPro_Call.Value = DT.Rows(0).Item("ComPro_Call")
                        't_ComPro_EMail.Value = DT.Rows(0).Item("ComPro_EMail")
                        't_ComPro_ConPer.Value = DT.Rows(0).Item("ComPro_ConPer")
                        't_ComPro_Other.Value = DT.Rows(0).Item("ComPro_Other")

                        Dim ddl_SQASu_Subject As String = DT.Rows(0).Item("SQASu_Subject").ToString().Trim()

                        ' ตอนนี้ DropDown มีข้อมูลแล้ว ค่อยเลือก
                        If dd_SQASu_Subject.Items.FindByText(ddl_SQASu_Subject) IsNot Nothing Then
                            dd_SQASu_Subject.SelectedItem.Text = ddl_SQASu_Subject
                        ElseIf dd_SQASu_Subject.Items.FindByValue(ddl_SQASu_Subject) IsNot Nothing Then
                            dd_SQASu_Subject.SelectedValue = ddl_SQASu_Subject
                        Else
                            dd_SQASu_Subject.SelectedIndex = 0 ' ไม่เจอ ให้เลือก "เลือก"
                        End If

                    End If

                    DT.Dispose()
                    DT = Nothing
                    az = Nothing
                Catch ex As Exception
                    X2 = ex.Message
                    'txtidno.Text = "ADD"
                End Try


            Case "iDelete"
                Try
                    Dim sqltxt As String
                    Dim az As New AzureVM
                    'sqltxt = "Delete ssSPrice where t_spid = '" & e.CommandArgument & "' "
                    sqltxt = "Update SQAQue set SQAQ_Status = 0 where SQAQ_ID = '" & e.CommandArgument & "' "
                    Dim y As Integer = az.Execute(sqltxt)
                    If y > 0 Then
                        Me.iGetData()
                    Else
                        X2 = "ลบไม่สำเร็จ"
                    End If
                    az = Nothing
                Catch ex As Exception
                    X2 = "ลบไม่สำเร็จ"
                End Try

            Case "iView"
                Try
                    Session("SQAQ_ID") = e.CommandArgument
                    Response.Redirect("SetSupplierB1.aspx")
                Catch ex As Exception
                    X2 = "System Error"
                End Try

            Case "iComment"
                Response.Redirect("frmCommentSP.aspx?id=" & e.CommandArgument)

        End Select
    End Sub

    Private Sub example_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles example.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim drv As DataRowView = e.Row.DataItem
            Dim xDelete As LinkButton = e.Row.FindControl("lbtDelete")
            Dim xEdit As LinkButton = e.Row.FindControl("lbtEdit")
            'Dim xImage As Image = e.Row.FindControl("headChk")
            Dim xNewcust As Image = e.Row.FindControl("imgNewcust")
            xDelete.Attributes.Add("onclick", "javascript:return confirm('ลบข้อมูล ?');")
            lnkAddNew.Visible = True

            'If drv!t_spacknow2 = "0" Then
            '    e.Row.ForeColor = Drawing.Color.Red
            '    xImage.Visible = False
            'Else
            '    xImage.Visible = True
            '    xDelete.Visible = False
            'End If

            If Session("Admin") <> "Admin" Then
                If drv!t_user <> Session("uname") Then
                    xDelete.Visible = False
                    xEdit.Visible = False
                End If

                If drv!t_hmname = Session("fname") Then
                    xDelete.Visible = False
                    xEdit.Visible = True
                    lnkAddNew.Visible = False
                End If

            End If

        End If
    End Sub

    Private Sub lnkAddNew_Click(sender As Object, e As EventArgs) Handles lnkAddNew.Click
        Me.iGetISCCompanyName()
        Me.iGetISCTopic()

        Me.iReset()

        Dim sb As New System.Text.StringBuilder()
        sb.Append("<script language='javascript'>")
        sb.Append("$('#modalId').modal('show');")
        sb.Append("document.getElementById('" & dd_SQAM_SUBName.ClientID & "').disabled = false;")
        sb.Append("</script>")
        ClientScript.RegisterStartupScript(Me.[GetType](), "JSScript", sb.ToString())

    End Sub

    Private Sub lnkFind_Click(sender As Object, e As EventArgs) Handles lnkFind.Click
        Me.iGetData()
    End Sub


    Private Sub _ISCComProfile_ServerClick(sender As Object, e As EventArgs) Handles _ISCComProfile.ServerClick
        Try
            Dim sqltxt As String
            Dim az As New AzureVM
            Dim txt As String = ""
            Dim txtLastDate As String = ""
            Dim txtTime As String = "0"
            Dim txtHead As String = ""
            Dim txtSale As String = ""
            Dim strMessage As String = ""
            'Dim fouProValue As Integer = GetSQAQ_FouProValue()
            'Dim severityValue As Integer = GetSeverityLevelValue()

            ' แปลงค่าจาก TextBox วันที่
            Dim userSelectedDate As DateTime
            If String.IsNullOrEmpty(t_SQAQ_Date.Text) OrElse Not DateTime.TryParse(t_SQAQ_Date.Text, userSelectedDate) Then
                ' ถ้าว่างหรือแปลงไม่ได้ ให้ใช้วันที่ปัจจุบัน
                userSelectedDate = DateTime.Now
            End If

            Dim formattedDate As String = userSelectedDate.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)


            ' สร้างตัวแปรวันที่ปัจจุบัน
            Dim CurrentDate As DateTime = Now
            ' เรียกฟังก์ชัน GetRunningNo โดยส่ง CurrentDate และ CompanyShortName เข้าไป
            Dim RunningNo As String = GetRunningNo(CurrentDate)
            ' สร้าง RefNo
            Dim datePart As String = CurrentDate.ToString("yyMMdd", System.Globalization.CultureInfo.InvariantCulture)
            Dim RefNo As String = t_SQAM_SUBsName.Value & datePart & RunningNo

            ' ใส่ใน textbox เผื่อใช้งานต่อ
            t_RefNO.Value = RefNo


            Dim txtComPro_Type As String = ""

            If t_SQAQ_Contact.Value.Trim = "" And t_SQAM_SUBsName.Value.Trim = "" Then
                X2 = "กรุณากรอกข้อมูล Vendor Code หรือ Company Name อย่างใดอย่างหนึงให้ครบ"
                Exit Sub
            End If

            If dd_SQASu_Subject.SelectedIndex = 0 And dd_SQAM_SUBName.SelectedIndex = 0 Then
                X2 = "กรุณากรอกข้อมูล Topic และ ชื่อบริษัท ด้วยครับ"
                Exit Sub
            End If

            If txtidno.Text = "ADD" Then


                sqltxt = "INSERT INTO SQAQue (" &
                       "SQAQ_Contact, SQAM_SUBName, SQAM_SUBsName, SQASu_Subject, " &
                       "SQAQ_Date, SQAQ_Status, SQAQ_DateTime, SQAQ_StatusSub) VALUES (" &
                       "N'" & t_SQAQ_Contact.Value.Trim.Replace("'", "''") & "', " &
                       "'" & dd_SQAM_SUBName.SelectedItem.Text.Replace("'", "''") & "', " &
                       "'" & RefNo & "', " &
                       "N'" & dd_SQASu_Subject.SelectedItem.Text.Replace("'", "''") & "', " &
                       "'" & formattedDate & "', " &
                       "1, " &   'ตั้งค่า SQAQ_Status เป็น 1 ตรงนี้
                       "GETDATE(), " &
                       "0)"     ' SQAQ_StatusSub เป็น 0

                Dim y As Integer = az.Execute(sqltxt)
                If y > 0 Then
                    X2 = "บันทึกสำเร็จ"
                    Me.iGetData()
                    Me.iReset()
                Else
                    X2 = "บันทึกไม่สำเร็จ"
                End If


            Else
                ' แก้ไข

                Dim p_SQAM_Type As String
                'If rBL_ProdType.SelectedIndex = 0 Then p_ComPro_Type = "Factory" Else p_ComPro_Type = "Trading"

                sqltxt = "UPDATE SQAQue SET " &
                "SQAQ_Contact = N'" & t_SQAQ_Contact.Value.Trim.Replace("'", "''") & "', " &
                "SQAM_SUBName = '" & dd_SQAM_SUBName.SelectedValue.Replace("'", "''") & "', " &
                "SQASu_Subject = '" & dd_SQASu_Subject.SelectedValue.Replace("'", "''") & "', " &
                "SQAQ_Date = GETDATE() " &
                "WHERE SQAQ_ID = '" & txtidno.Text.Trim.Replace("'", "''") & "'"

                Dim y As Integer = az.Execute(sqltxt)
                If y > 0 Then
                    Me.iGetData()
                    Me.iReset()
                Else
                    X2 = "แก้ไขไม่สำเร็จ"
                End If

            End If

            az = Nothing
        Catch ex As Exception
            X2 = ex.Message
        End Try
    End Sub

    Protected Sub dd_SQAM_SUBName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dd_SQAM_SUBName.SelectedIndexChanged
        If dd_SQAM_SUBName.SelectedIndex <> 0 Then
            Dim shortName As String = dd_SQAM_SUBName.SelectedValue.ToString()
            t_SQAM_SUBsName.Value = shortName
            t_SQAM_SUBsName.Attributes("readonly") = "readonly"
        Else
            t_SQAM_SUBsName.Value = ""
            t_SQAM_SUBsName.Attributes.Remove("readonly")
        End If
    End Sub

    'Private Sub dd_SQAM_ProductT_TextChanged(sender As Object, e As EventArgs) Handles dd_SQAM_ProductT.TextChanged
    '    If dd_SQAM_ProductT.SelectedIndex <> 0 Then
    '        Dim shortName As String = dd_SQAM_ProductT.SelectedValue.ToString()
    '        t_SQAM_TMaterials.Value = shortName
    '        t_SQAM_TMaterials.Attributes("readonly") = "readonly"
    '    Else
    '        t_SQAM_TMaterials.Value = ""
    '        t_SQAM_TMaterials.Attributes.Remove("readonly")
    '    End If
    'End Sub

    ' หาค่า Running Number รายเดือน
    Private Function GetRunningNo(ByVal CurrentDate As DateTime) As String
        Dim az As New AzureVM
        Dim runningNo As Integer = 1
        Dim result As String = ""

        Dim yyMM As String = CurrentDate.ToString("yyMM", System.Globalization.CultureInfo.InvariantCulture)

        ' คำสั่ง SQL: ดึงรายการล่าสุดของเดือนเดียวกัน
        Dim sql As String = "
        SELECT TOP 1 SQAM_SUBsName 
        FROM SQAQue 
        WHERE FORMAT(SQAQ_DateTime, 'yyMM') = '" & yyMM & "' 
        ORDER BY SQAQ_DateTime DESC"

        Dim dt As DataTable = az.GetDataTable(sql)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Dim lastRef As String = dt.Rows(0)("SQAM_SUBsName").ToString()
            If lastRef.Length >= 3 Then
                Dim lastRunStr As String = lastRef.Substring(lastRef.Length - 3)
                Dim lastRun As Integer
                If Integer.TryParse(lastRunStr, lastRun) Then
                    runningNo = lastRun + 1
                End If
            End If
        End If

        result = runningNo.ToString("D3") ' เช่น 001
        Return result
    End Function

    ''CheckBox พบปัญหาในระหว่างการทำงาน
    'Private Function GetSQAQ_FouProValue() As Integer
    '    If chk_Inspection.Checked Then
    '        Return 1 ' ขณะตรวจรับ
    '    ElseIf chk_Production.Checked Then
    '        Return 2 ' ขณะนำไปใช้ใน line ผลิต
    '    ElseIf chk_Customer.Checked Then
    '        Return 3 ' ลูกค้าพบปัญหา
    '    Else
    '        Return 0 ' ไม่เลือก
    '    End If
    'End Function

    'Private Function GetSeverityLevelValue() As Integer
    '    If CheckBox1.Checked Then
    '        Return 1  ' Critical Defect
    '    ElseIf CheckBox2.Checked Then
    '        Return 2  ' Major Defect
    '    ElseIf CheckBox3.Checked Then
    '        Return 3  ' Minor Defect
    '    Else
    '        Return 0  ' ไม่เลือก
    '    End If
    'End Function

End Class