Imports System.Data.SqlClient
Imports System.Globalization
Imports System.IO
Imports System.Net
Imports System.Net.Mail
Imports System.Web.Script.Serialization

Public Class SetSupplierE
    Inherits System.Web.UI.Page
    Public X2 As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Admin") = "" Then
            Response.Redirect("Login.aspx")
        End If

        Try
            If Not Page.IsPostBack Then
                If Session("SQAQ_ID") = "" Then
                    Response.Redirect("SetSupplier.aspx")
                End If
                ViewState("SQAQ_ID") = Session("SQAQ_ID")
                'Me.iGetData()
                'lbPosition.Attributes("class") &= " flagGold"

                iGetISCAttachments()
                iGetISCMatType()
                iGetTroubleType()
                iGetISCTopic()

                GetProblemOccuredValue()
                GetSeverityLevelValue()
                GetResultValue()

                iResetComProF()
                iGetTopic()
                iGetComProF()

                iGetData_RelUpload()


                _RelUpload.Visible = True
                txt_RelUpload.Text = ""
                S_RelUpload.Visible = False
                C_RelUpload.Visible = False
                example_RelUpload.Enabled = True


                rdo_Reject.Attributes("onclick") = "toggleCheckboxGroups();"
                rdo_Hold.Attributes("onclick") = "toggleCheckboxGroups();"
                rdo_Warning.Attributes("onclick") = "toggleCheckboxGroups();"



            Else

                'If GridView2.Rows.Count > 0 Then GridView2.HeaderRow.TableSection = TableRowSection.TableHeader
                'If GridView3.Rows.Count > 0 Then GridView3.HeaderRow.TableSection = TableRowSection.TableHeader
            End If
        Catch ex As Exception
            X2 = ex.Message
        End Try
    End Sub

    Sub iResetComProF()   '-----ล้างข้อมูล Company Profile
        t_SQAQ_Contact.Value = ""
        t_SQAM_SUBName.Value = ""
        t_SQAM_SUBsName.Value = ""
        dd_SQASu_Subject.SelectedIndex = 0
        t_SQAQ_Date.Value = ""
        t_SQAQ_DetPro.Value = ""
        t_SQAQ_Coil.Value = ""
        t_SQAQ_DO.Value = ""
        t_SQAQ_InDate.Value = ""
        t_SQAQ_NoPro.Value = ""
        t_SQAQ_Note.Value = ""
        dd_SQAS_Name.SelectedIndex = 0
        t_SQAQ_RCISolProDet.Value = ""
        t_SQAQ_ExpTyp.Value = ""
        t_SQAQ_ExpTypCou.Value = ""
        t_SQAQ_AttOth.Text = ""

        chkReturnToSupplier.Checked = False
        chkClaimSupplier.Checked = False
        chkSupplierSorting.Checked = False
        chkRCISortingClaim.Checked = False
        chkRequestDoc.Checked = False
        chkAcceptCondition.Checked = False
        chkOther.Checked = False

    End Sub

    Sub iGetComProF()  '---Load ข้อมูล Company Profile--
        Try
            'Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()
            'sb.Append("<script language='javascript'>")
            'sb.Append("$('#modalId').modal('show');")
            'sb.Append("</script>")
            'ClientScript.RegisterStartupScript(Me.[GetType](), "JSScript", sb.ToString())

            Dim sqltxt As String
            Dim DT As DataTable
            Dim az As New AzureVM

            sqltxt = "SELECT SQAQ_Contact, SQAM_SUBName, SQAM_SUBsName, SQAQ_Date " &
                 "FROM SQAQue WHERE SQAQ_ID = '" & Session("SQAQ_ID") & "'"

            DT = az.GetDataTable(sqltxt)

            If DT.Rows.Count > 0 Then
                t_SQAQ_Contact.Value = DT.Rows(0).Item("SQAQ_Contact").ToString()
                t_SQAM_SUBName.Value = DT.Rows(0).Item("SQAM_SUBName").ToString()
                t_SQAM_SUBsName.Value = DT.Rows(0).Item("SQAM_SUBsName").ToString()
                t_SQAQ_Date.Value = Convert.ToDateTime(DT.Rows(0).Item("SQAQ_Date")).ToString("yyyy-MM-dd")
            Else
                X2 = "ไม่พบข้อมูล SQAQ_ID = " & Session("SQAQ_ID")
            End If

            DT.Dispose()
            DT = Nothing
            az = Nothing
        Catch ex As Exception
            X2 = "Error: " & ex.Message
        End Try
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
    Sub iGetTopic()  '---Load ข้อมูล Company Profile Topic --
        Try
            Dim sqltxt As String
            Dim DT As DataTable
            Dim az As New AzureVM

            ' ดึง SQASu_Subject จาก SQAQue ก่อน เพื่อนำไปตั้ง default
            Dim selectedSubjectText As String = ""
            sqltxt = "SELECT SQASu_Subject FROM SQAQue WHERE SQAQ_ID = '" & Session("SQAQ_ID") & "'"
            DT = az.GetDataTable(sqltxt)
            If DT.Rows.Count > 0 Then
                selectedSubjectText = DT.Rows(0).Item("SQASu_Subject").ToString()
            End If
            DT.Dispose()

            ' ดึงรายการทั้งหมดจาก SQASubject มาใส่ DropDown
            sqltxt = "SELECT SQASu_ID, SQASu_Subject FROM SQASubject ORDER BY SQASu_Subject"
            DT = az.GetDataTable(sqltxt)
            If DT.Rows.Count > 0 Then
                dd_SQASu_Subject.DataSource = DT
                dd_SQASu_Subject.DataTextField = "SQASu_Subject"
                dd_SQASu_Subject.DataValueField = "SQASu_ID"
                dd_SQASu_Subject.DataBind()
                dd_SQASu_Subject.Items.Insert(0, New ListItem("เลือก", "-"))

                ' ตั้งค่า default โดยหาจากข้อความ
                If dd_SQASu_Subject.Items.FindByText(selectedSubjectText) IsNot Nothing Then
                    dd_SQASu_Subject.ClearSelection()
                    dd_SQASu_Subject.Items.FindByText(selectedSubjectText).Selected = True
                End If
            Else
                dd_SQASu_Subject.DataSource = Nothing
                dd_SQASu_Subject.DataBind()
            End If

            ' เคลียร์ object
            DT.Dispose()
            DT = Nothing
            az = Nothing
        Catch ex As Exception
            X2 = "Error: " & ex.Message
        End Try
    End Sub

    Sub iGetISCMatType()  '---DropDownList Prod Type--
        Try
            Dim az As New AzureVM
            Dim sqltxt As String
            Dim DT As New DataTable

            sqltxt = "SELECT SQAM_ProductT, SQAM_TMaterials, SQAM_Type " &
                 "FROM SQAMaster " &
                 "WHERE ISNULL(SQAM_ProductT, '') <> '' " &
                 "ORDER BY SQAM_ProductT"

            DT = az.GetDataTable(sqltxt)
            If DT.Rows.Count > 0 Then
                ' สร้างคอลัมน์ใหม่ รวม Material และ Type เข้าด้วยกัน เช่น "Plastic|Conductive"
                DT.Columns.Add("ValueCombined", GetType(String))
                For Each row As DataRow In DT.Rows
                    row("ValueCombined") = row("SQAM_TMaterials").ToString() & "|" & row("SQAM_Type").ToString()
                Next

                dd_SQAM_ProductT.DataSource = DT
                dd_SQAM_ProductT.DataTextField = "SQAM_ProductT"
                dd_SQAM_ProductT.DataValueField = "ValueCombined"
                dd_SQAM_ProductT.DataBind()
                dd_SQAM_ProductT.Items.Insert(0, New ListItem("เลือก", "-"))
            Else
                dd_SQAM_ProductT.DataSource = Nothing
                dd_SQAM_ProductT.DataBind()
                dd_SQAM_ProductT.Items.Insert(0, New ListItem("เลือก", "-"))
            End If

            DT.Dispose()
            DT = Nothing
            az = Nothing
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Function GetProblemOccuredValue() As Integer '---Radio btn Problem Occurred  --
        If rdo_Inspection.Checked Then
            Return 1
        ElseIf rdo_Production.Checked Then
            Return 2
        ElseIf rdo_Customer.Checked Then
            Return 3
        Else
            Return 0 ' หรือใช้ -1 เพื่อระบุว่าไม่ได้เลือกอะไรเลย
        End If
    End Function

    Private Function GetSeverityLevelValue() As Integer '---Radio btn Severity level  --
        If rdo_CriticalDef.Checked Then
            Return 1
        ElseIf rdo_MajorDef.Checked Then
            Return 2
        ElseIf rdo_minorDef.Checked Then
            Return 3
        Else
            Return 0 ' หรือใช้ -1 เพื่อระบุว่าไม่ได้เลือกอะไรเลย
        End If
    End Function

    ' ฟังก์ชันเก็บข้อมูลจาก TextArea เข้าเป็นตัวแปร String หัวข้อ The details of the problem
    Private Function GetFormData() As Dictionary(Of String, String)
        Dim data As New Dictionary(Of String, String)()
        data("Details") = t_SQAQ_DetPro.Value.Trim()
        data("CoilBatchNo") = t_SQAQ_Coil.Value.Trim()
        data("DONo") = t_SQAQ_DO.Value.Trim()
        data("AdmissionDate") = t_SQAQ_InDate.Value.Trim()
        data("NumberOfProblems") = t_SQAQ_NoPro.Value.Trim()
        data("Annotation") = t_SQAQ_Note.Value.Trim()

        ' ฟังก์ชันเก็บข้อมูลจาก TextArea เข้าเป็นตัวแปร String หัวข้อ RCI initial troubleshooting
        data("TroubleshootingDetails") = t_SQAQ_RCISolProDet.Value.Trim()
        data("Type") = t_SQAQ_ExpTyp.Value.Trim()
        data("Amount") = t_SQAQ_ExpTypCou.Value.Trim()

        Return data
    End Function

    Private Sub iGetTroubleType() '---DropDownList Troubleshooting--
        Try
            Dim az As New AzureVM
            Dim sqltxt As String = "SELECT SQAS_ID, SQAS_Name FROM SQASolve WHERE SQAS_Status = 1 ORDER BY SQAS_ID"
            Dim DT As DataTable = az.GetDataTable(sqltxt)

            If DT IsNot Nothing AndAlso DT.Rows.Count > 0 Then
                dd_SQAS_Name.DataSource = DT
                dd_SQAS_Name.DataTextField = "SQAS_Name"
                dd_SQAS_Name.DataValueField = "SQAS_ID"
            Else
                dd_SQAS_Name.DataSource = Nothing
            End If

            dd_SQAS_Name.DataBind()
            dd_SQAS_Name.Items.Insert(0, New ListItem("-- กรุณาเลือก --", ""))

            If DT IsNot Nothing Then
                DT.Dispose()
            End If

            az = Nothing
        Catch ex As Exception
            Response.Write("เกิดข้อผิดพลาด: " & ex.Message)
        End Try
    End Sub

    Private Function GetResultValue() As Integer '---Radio btn evaluation  --
        If rdo_Reject.Checked Then
            Return 1
        ElseIf rdo_Hold.Checked Then
            Return 2
        ElseIf rdo_Warning.Checked Then
            Return 3
        Else
            Return 0 ' หรือใช้ -1 เพื่อระบุว่าไม่ได้เลือกอะไรเลย
        End If
    End Function

    Sub iGetISCAttachments() '---DropDownList Attach--
        Try
            Dim az As New AzureVM
            Dim sqltxt As String = "SELECT DISTINCT SQAA_Name FROM SQAAtt ORDER BY SQAA_Name"
            Dim DT As DataTable = az.GetDataTable(sqltxt)

            If DT IsNot Nothing AndAlso DT.Rows.Count > 0 Then
                dd_SQAA_Name.DataSource = DT
                dd_SQAA_Name.DataTextField = "SQAA_Name"
                dd_SQAA_Name.DataValueField = "SQAA_Name"
            Else
                dd_SQAA_Name.DataSource = Nothing
            End If

            dd_SQAA_Name.DataBind()
            dd_SQAA_Name.Items.Insert(0, New ListItem("-- กรุณาเลือก --", ""))

            If DT IsNot Nothing Then
                DT.Dispose()
            End If
            az = Nothing
        Catch ex As Exception
            Response.Write("เกิดข้อผิดพลาด: " & ex.Message)
        End Try
    End Sub

    ' ฟังก์ชันหาค่าเลข 1-7 ของ checkbox ที่ถูกเลือก (เลือกได้ทีละตัว)
    Private Function GetSelectedCheckboxValue() As Integer
        If chkReturnToSupplier.Checked Then Return 1
        If chkClaimSupplier.Checked Then Return 2
        If chkSupplierSorting.Checked Then Return 3
        If chkRCISortingClaim.Checked Then Return 4
        If chkRequestDoc.Checked Then Return 5
        If chkAcceptCondition.Checked Then Return 6
        If chkOther.Checked Then Return 7
        Return 0 ' ไม่มีเลือก
    End Function

    ' Event เมื่อ checkbox "อื่น ๆ" ถูกเปลี่ยนสถานะ
    Protected Sub chkOther_CheckedChanged(sender As Object, e As EventArgs) Handles chkOther.CheckedChanged
        t_SQAQ_ResDetOth.Visible = chkOther.Checked
        If chkOther.Checked Then
            txtOtherDetail.Enabled = True
        Else
            txtOtherDetail.Text = ""
            txtOtherDetail.Enabled = False
        End If
    End Sub

    Private Function GetSIName() As String
        Dim siName As String = ""

        If Request.Form("t_SQAQ_SIName") IsNot Nothing Then
            siName = Request.Form("t_SQAQ_SIName").Trim()

            ' ตัดความยาวไม่เกิน 25 ตัวอักษร
            If siName.Length > 25 Then
                siName = siName.Substring(0, 25)
            End If

            ' แทนที่ ' ด้วย '' เพื่อป้องกัน SQL Injection แบบง่าย
            siName = siName.Replace("'", "''")
        End If

        Return siName
    End Function

    ' เก็บค่าที่ผู้ใช้เลือกใน RCI แก้ไขปัญหาเบื้องต้น
    Private Function GetSelectedSQAS_Name() As String
        If dd_SQAS_Name.SelectedItem IsNot Nothing Then
            Return dd_SQAS_Name.SelectedItem.Text.Trim().Replace("'", "''")
        Else
            Return ""
        End If
    End Function

    ' เก็บค่าที่ผู้ใช้เลือกใน Attachments
    Private Function GetSelectedSQAA_Name() As String
        Dim selectedAttachment As String = dd_SQAA_Name.SelectedValue.Trim()
        If Not String.IsNullOrEmpty(selectedAttachment) Then
            Return selectedAttachment
        Else
            Return "-"
        End If
    End Function


    ' คืนค่า SQAQ_AttOth: textbox ถ้าเลือก "อื่น", หรือ "0" ถ้าเลือกทั่วไป
    Private Function GetAttachmentOtherText() As String
        Dim selectedAttachment As String = dd_SQAA_Name.SelectedValue.Trim()
        Dim otherAttachment As String = t_SQAQ_AttOth.Text.Trim()

        If selectedAttachment.Contains("อื่น") Then
            If Not String.IsNullOrEmpty(otherAttachment) Then
                If otherAttachment.Length > 50 Then
                    otherAttachment = otherAttachment.Substring(0, 50)
                End If
                Return otherAttachment
            Else
                Return "อื่นๆ"
            End If
        Else
            Return "0"
        End If
    End Function


    Private Sub example_RelUpload_PreRender(sender As Object, e As EventArgs) Handles example_RelUpload.PreRender
        Try
            example_RelUpload.UseAccessibleHeader = True
            example_RelUpload.HeaderRow.TableSection = TableRowSection.TableHeader
        Catch ex As Exception

        End Try
    End Sub


    Private Sub _RelUpload_ServerClick(sender As Object, e As EventArgs) Handles _RelUpload.ServerClick
        Try
            Dim sqltxt As String
            Dim az As New AzureVM
            Dim txt As String = ""
            Dim txtLastDate As String = ""
            Dim txtTime As String = "0"
            Dim txtHead As String = ""
            Dim txtSale As String = ""
            Dim strMessage As String = ""

            Dim txtComPro_Type As String = ""



            If t_RelUpl_M_Name.Value.Trim = "" Or t_RelUpl_Link.Value.Trim = "" Then
                X2 = "กรุณากรอกข้อมูล ให้ครบ"
                Exit Sub
            End If

            sqltxt = "INSERT INTO SQAUpload (SQAQ_ID, SQAU_Name, SQAU_Link, SQAU_Status) " &
         "VALUES ('" & Session("SQAQ_ID") & "', " &
         "N'" & t_RelUpl_M_Name.Value.Trim().Replace("'", "") & "', " &
         "N'" & t_RelUpl_Link.Value.Trim().Replace("'", "") & "', 1)"

            Dim y As Integer = az.Execute(sqltxt)

            If y > 0 Then
                X2 = "บันทึกสำเร็จ"
                t_RelUpl_M_Name.Value = ""
                t_RelUpl_Link.Value = ""
                Me.iGetData_RelUpload()
            Else
                X2 = "บันทึกไม่สำเร็จ"
            End If

        Catch ex As Exception
            X2 = ex.Message
        End Try
    End Sub

    Private Sub example_RelUpload_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles example_RelUpload.RowCommand
        Select Case e.CommandName

            Case "iEdit"
                If Session("Manu") = "ExecN" Then
                    ' ไม่อนุญาตแก้ไข
                Else
                    Dim sqltxt As String
                    Dim DT As DataTable
                    Dim az As New AzureVM

                    ' ดึงข้อมูลเฉพาะชื่อและลิงก์ จาก ISCRelUpload
                    sqltxt = "SELECT SQAU_Name, SQAU_Link FROM SQAUpload WHERE SQAU_ID = '" & e.CommandArgument.ToString() & "'"

                    DT = az.GetDataTable(sqltxt)

                    If DT IsNot Nothing AndAlso DT.Rows.Count > 0 Then
                        txt_RelUpload.Text = e.CommandArgument.ToString()
                        t_RelUpl_M_Name.Value = DT.Rows(0).Item("SQAU_Name").ToString()
                        t_RelUpl_Link.Value = DT.Rows(0).Item("SQAU_Link").ToString()

                        _RelUpload.Visible = False
                        S_RelUpload.Visible = True
                        C_RelUpload.Visible = True
                        example_RelUpload.Enabled = False
                    End If

                    az = Nothing
                End If

            Case "iDelete"
                Try
                    Dim sqltxt As String
                    Dim az As New AzureVM

                    ' ลบข้อมูลจาก SQAUpload โดยใช้ SQAU_ID
                    sqltxt = "DELETE FROM SQAUpload WHERE SQAU_ID = '" & e.CommandArgument.ToString() & "'"
                    Dim y As Integer = az.Execute(sqltxt)
                    If y > 0 Then
                        Me.iGetData_RelUpload()
                        X2 = "ลบสำเร็จ"
                    Else
                        X2 = "ลบไม่สำเร็จ"
                    End If

                    az = Nothing
                Catch ex As Exception
                    X2 = "ลบไม่สำเร็จ: " & ex.Message
                End Try

        End Select
    End Sub

    Sub iGetData_RelUpload() '-----เอาข้อมูลใส่ GridView RelUpload
        Try
            Dim sqltxt As String
            Dim DT As DataTable
            Dim az As New AzureVM
            Dim txt As String = ""
            Dim strVendor As String = ""

            sqltxt = "SELECT ROW_NUMBER() OVER (ORDER BY t2.SQAU_ID DESC) AS No, " &
         "t2.SQAU_ID, t2.SQAQ_ID, t2.SQAU_Name, t2.SQAU_Link " &
         "FROM SQAUpload t2 " &
         "LEFT JOIN SQAQue t1 ON t2.SQAQ_ID = t1.SQAQ_ID " &
         "WHERE t2.SQAQ_ID = '" & Session("SQAQ_ID") & "' " &
         "ORDER BY t2.SQAU_ID DESC"

            DT = az.GetDataTable(sqltxt)
            If DT.Rows.Count > 0 Then
                example_RelUpload.DataSource = DT
            Else
                example_RelUpload.DataSource = Nothing
            End If
            example_RelUpload.DataBind()

            DT.Dispose()
            DT = Nothing
            az = Nothing
        Catch ex As Exception
            X2 = ex.Message
        End Try

    End Sub


    Private Sub C_RelUpload_Click(sender As Object, e As EventArgs) Handles C_RelUpload.Click
        _RelUpload.Visible = True
        txt_RelUpload.Text = ""
        S_RelUpload.Visible = False
        C_RelUpload.Visible = False
        example_RelUpload.Enabled = True

        t_RelUpl_M_Name.Value = ""
        t_RelUpl_Link.Value = ""
    End Sub

    Private Sub S_RelUpload_ServerClick(sender As Object, e As EventArgs) Handles S_RelUpload.ServerClick
        Try
            Dim sqltxt As String
            Dim az As New AzureVM
            Dim txt As String = ""
            Dim txtDate As String = ""


            sqltxt = "UPDATE SQAUpload SET " &
                       "SQAU_Name = N'" & t_RelUpl_M_Name.Value.Trim.Replace("'", "''") & "', " &
                       "SQAU_Link = N'" & t_RelUpl_Link.Value.Trim.Replace("'", "''") & "' " &
                       "WHERE SQAU_ID = '" & txt_RelUpload.Text.Trim & "'"

            Dim y As Integer = az.Execute(sqltxt)
            If y > 0 Then
                _RelUpload.Visible = True
                txt_RelUpload.Text = ""
                S_RelUpload.Visible = False
                C_RelUpload.Visible = False
                example_RelUpload.Enabled = True

                t_RelUpl_M_Name.Value = ""
                t_RelUpl_Link.Value = ""

                iGetData_RelUpload()
            Else
                X2 = "แก้ไขไม่สำเร็จ"
            End If

            az = Nothing
        Catch ex As Exception
            X2 = ex.Message
        End Try
    End Sub

    Protected Sub _ISCComProfile_ServerClick(sender As Object, e As EventArgs) Handles _ISCComProfile.ServerClick
        Try
            Dim az As New AzureVM
            Dim strMessage As String = ""

            ' ตรวจสอบ Session
            If Session("SQAQ_ID") Is Nothing Then
                Throw New Exception("ไม่พบค่า Session(""SQAQ_ID"")")
            End If

            Dim sqaqId As Integer = Convert.ToInt32(Session("SQAQ_ID"))
            Dim formData = GetFormData()
            Dim severityLevel As Integer = GetSeverityLevelValue()
            Dim problemOccured As Integer = GetProblemOccuredValue()
            Dim resultValue As Integer = GetResultValue()
            Dim siName As String = GetSIName()
            Dim attachmentName As String = GetSelectedSQAA_Name()
            Dim attachmentOther As String = GetAttachmentOtherText()
            Dim sqasName As String = GetSelectedSQAS_Name()
            Dim sqamProductT As String = dd_SQAM_ProductT.SelectedItem.Text
            Dim sqamTMaterials As String = t_SQAM_TMaterials.Value
            Dim sqamType As String = t_SQAM_Type.Value

            If sqamTMaterials Is Nothing Then sqamTMaterials = ""
            If sqamType Is Nothing Then sqamType = ""
            sqamTMaterials = sqamTMaterials.Replace("'", "''")
            sqamType = sqamType.Replace("'", "''")
            sqamProductT = sqamProductT.Replace("'", "''")

            ' ดึงชื่อเอกสารล่าสุดจาก SQAUpload
            Dim papName As String = ""
            Dim dt As DataTable = az.GetDataTable("SELECT TOP 1 SQAU_Name FROM SQAUpload WHERE SQAQ_ID = " & sqaqId & " ORDER BY SQAU_ID DESC")
            If dt.Rows.Count > 0 Then
                papName = dt.Rows(0)("SQAU_Name").ToString().Replace("'", "''")
            End If

            ' --- เริ่มเพิ่มส่วนเก็บ checkbox แบบเลข 1-7 และข้อความอื่น ๆ ---

            ' ฟังก์ชันภายในนี้ เพื่อหาค่าที่เลือก
            Dim selectedCheckboxValue As Integer = 0
            If chkReturnToSupplier.Checked Then
                selectedCheckboxValue = 1
            ElseIf chkClaimSupplier.Checked Then
                selectedCheckboxValue = 2
            ElseIf chkSupplierSorting.Checked Then
                selectedCheckboxValue = 3
            ElseIf chkRCISortingClaim.Checked Then
                selectedCheckboxValue = 4
            ElseIf chkRequestDoc.Checked Then
                selectedCheckboxValue = 5
            ElseIf chkAcceptCondition.Checked Then
                selectedCheckboxValue = 6
            ElseIf chkOther.Checked Then
                selectedCheckboxValue = 7
            End If

            Dim otherDetail As String = ""
            If selectedCheckboxValue = 7 Then
                otherDetail = txtOtherDetail.Text.Trim()
            End If
            otherDetail = otherDetail.Replace("'", "''") ' ป้องกัน syntax error

            ' แก้ตัวแปร resolutionDetailOther ให้เก็บข้อความอื่น ๆ ด้วย (ถ้าจำเป็น)
            Dim resolutionDetailOther As String = otherDetail

            ' สมมติ chkSQAQ_ResDet คือค่าที่เก็บ checkbox แบบเลข 1-7 ด้วย (หรือจะใช้ selectedCheckboxValue แทน)
            Dim chkSQAQ_ResDet As Integer = selectedCheckboxValue

            ' สร้าง SQL UPDATE โดยแก้ไขให้ใช้ chkSQAQ_ResDet และ resolutionDetailOther
            Dim sqltxt As String = "UPDATE SQAQue SET " &
            "SQAQ_DetPro = N'" & formData("Details").Replace("'", "''") & "', " &
            "SQAQ_Coil = N'" & formData("CoilBatchNo").Replace("'", "''") & "', " &
            "SQAQ_DO = N'" & formData("DONo").Replace("'", "''") & "', " &
            "SQAQ_InDate = N'" & formData("AdmissionDate").Replace("'", "''") & "', " &
            "SQAQ_NoPro = N'" & formData("NumberOfProblems").Replace("'", "''") & "', " &
            "SQAQ_Note = N'" & formData("Annotation").Replace("'", "''") & "', " &
            "SQAQ_RCISolProDet = N'" & formData("TroubleshootingDetails").Replace("'", "''") & "', " &
            "SQAQ_ExpTyp = N'" & formData("Type").Replace("'", "''") & "', " &
            "SQAQ_ExpTypCou = N'" & formData("Amount").Replace("'", "''") & "', " &
            "SQAQ_SevLev = " & severityLevel & ", " &
            "SQAQ_FouPro = " & problemOccured & ", " &
            "SQAQ_Res = " & resultValue & ", " &
            "SQAQ_ResDetOth = N'" & resolutionDetailOther & "', " &
            "SQAA_Name = N'" & attachmentName.Replace("'", "''") & "', " &
            "SQAQ_AttOth = N'" & attachmentOther.Replace("'", "''") & "', " &
            "SQAQ_SIName = N'" & siName.Replace("'", "''") & "', " &
            "SQAS_Name = N'" & sqasName.Replace("'", "''") & "', " &
            "SQAQ_ResDet = " & chkSQAQ_ResDet & ", " &
            "SQAQ_Pap = N'" & papName & "', " &
            "SQAM_ProductT = N'" & sqamProductT & "', " &
            "SQAM_TMaterials = N'" & sqamTMaterials & "', " &
            "SQAM_Type = N'" & sqamType & "' " &
            "WHERE SQAQ_ID = " & sqaqId

            Dim y As Integer = az.Execute(sqltxt)

            If y > 0 Then
                X2 = "บันทึกสำเร็จ"
                Response.Redirect("SetSupplierB1.aspx", False)
                Context.ApplicationInstance.CompleteRequest()
            Else
                X2 = "บันทึกไม่สำเร็จ (ไม่มีข้อมูลถูกอัปเดต) SQL: " & sqltxt
            End If

        Catch ex As Exception
            X2 = "เกิดข้อผิดพลาด: " & ex.Message
        End Try
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Session("SQAQ_ID") = ""
        Response.Redirect("SetSupplierB1.aspx")
    End Sub

End Class