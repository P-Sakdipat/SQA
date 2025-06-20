Imports System.Data.SqlClient

Public Class SetSupplierB1
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

                'iGetSQAQDetail()
                iResetComProF()
                iGetComProF()
                ProdType()
                ProbAndSuv()
                ProbDetails()
                LoadRCISolve()
                LoadExpTypData()
                LoadResultDetails()
                LoadAttachmentDetails()
                LoadSIName()
                iGetData_RelUpload()

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
        t_SQASu_Subject.Value = ""
        t_SQAQ_Date.Value = ""
        t_SQAM_TMaterials.Value = ""
        t_SQAM_ProductT.Value = ""
        t_SQAM_Type.Value = ""
        t_SQAQ_FouPro.Value = ""
        t_SQAQ_SevLev.Value = ""
        t_SQAQ_DetPro.Value = ""
        t_SQAQ_Coil.Value = ""
        t_SQAQ_DO.Value = ""
        t_SQAQ_InDate.Value = ""
        t_SQAQ_NoPro.Value = ""
        t_SQAQ_Note.Value = ""
        t_SQAS_Name.Value = ""
        t_SQAQ_RCISolProDet.Value = ""
        t_SQAQ_ExpTyp.Value = ""
        t_SQAQ_ExpTypCou.Value = ""
        t_SQAQ_Res.Value = ""
        t_SQAQ_ResDet.Value = ""
        t_SQAQ_ResDetOth.Value = ""
        t_SQAA_Name.Value = ""
        t_SQAQ_AttOth.Value = ""
        t_SQAQ_SIName.Value = ""

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

            sqltxt = "SELECT SQAQ_Contact, SQAM_SUBName, SQAM_SUBsName, SQAQ_Date, SQASu_Subject " &
                 "FROM SQAQue WHERE SQAQ_ID = '" & Session("SQAQ_ID") & "'"

            DT = az.GetDataTable(sqltxt)

            If DT.Rows.Count > 0 Then
                t_SQAQ_Contact.Value = DT.Rows(0).Item("SQAQ_Contact").ToString()
                t_SQAM_SUBName.Value = DT.Rows(0).Item("SQAM_SUBName").ToString()
                t_SQAM_SUBsName.Value = DT.Rows(0).Item("SQAM_SUBsName").ToString()
                t_SQASu_Subject.Value = DT.Rows(0).Item("SQASu_Subject").ToString()
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

    Sub ProdType()  '---Load ข้อมูล Product Type--
        Try
            'Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()
            'sb.Append("<script language='javascript'>")
            'sb.Append("$('#modalId').modal('show');")
            'sb.Append("</script>")
            'ClientScript.RegisterStartupScript(Me.[GetType](), "JSScript", sb.ToString())

            Dim sqltxt As String
            Dim DT As DataTable
            Dim az As New AzureVM

            sqltxt = "SELECT SQAM_TMaterials, SQAM_ProductT, SQAM_Type FROM SQAQue WHERE SQAQ_ID = '" & Session("SQAQ_ID") & "'"

            DT = az.GetDataTable(sqltxt)

            If DT.Rows.Count > 0 Then
                t_SQAM_TMaterials.Value = DT.Rows(0).Item("SQAM_TMaterials").ToString()
                t_SQAM_ProductT.Value = DT.Rows(0).Item("SQAM_ProductT").ToString()
                t_SQAM_Type.Value = DT.Rows(0).Item("SQAM_Type").ToString()
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

    Sub ProbAndSuv()  '---Load ข้อมูล Product Type--
        Try
            Dim sqltxt As String
            Dim DT As DataTable
            Dim az As New AzureVM

            sqltxt = "SELECT SQAQ_FouPro, SQAQ_SevLev FROM SQAQue WHERE SQAQ_ID = '" & Session("SQAQ_ID") & "'"

            DT = az.GetDataTable(sqltxt)

            If DT.Rows.Count > 0 Then
                ' แปลงค่า SQAQ_FouPro
                Dim fouProInt As Integer = 0
                Integer.TryParse(DT.Rows(0).Item("SQAQ_FouPro").ToString(), fouProInt)

                Dim fouProText As String = ""

                Select Case fouProInt
                    Case 1
                        fouProText = "ขณะตรวจรับ"
                    Case 2
                        fouProText = "ขณะนำไปใช้ใน Line ผลิต"
                    Case 3
                        fouProText = "ลูกค้าพบปัญหา"
                    Case Else
                        fouProText = "ไม่ระบุสถานะ"
                End Select

                t_SQAQ_FouPro.Value = fouProText

                ' แปลงค่า SQAQ_SevLev
                Dim sevLevInt As Integer = 0
                Integer.TryParse(DT.Rows(0).Item("SQAQ_SevLev").ToString(), sevLevInt)

                Dim sevLevText As String = ""

                Select Case sevLevInt
                    Case 1
                        sevLevText = "ระดับความรุนแรงต่ำ"
                    Case 2
                        sevLevText = "ระดับความรุนแรงกลาง"
                    Case 3
                        sevLevText = "ระดับความรุนแรงสูง"
                    Case Else
                        sevLevText = "ไม่ระบุระดับความรุนแรง"
                End Select

                t_SQAQ_SevLev.Value = sevLevText

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

    Sub ProbDetails()  '---Load ข้อมูล Product Details--
        Try
            Dim sqltxt As String
            Dim DT As DataTable
            Dim az As New AzureVM

            sqltxt = "SELECT SQAQ_DetPro, SQAQ_Coil, SQAQ_DO, SQAQ_InDate, " &
         "SQAQ_NoPro, SQAQ_Note FROM SQAQue WHERE SQAQ_ID = '" & Session("SQAQ_ID") & "'"

            DT = az.GetDataTable(sqltxt)

            If DT.Rows.Count > 0 Then
                t_SQAQ_DetPro.Value = DT.Rows(0).Item("SQAQ_DetPro").ToString()
                t_SQAQ_Coil.Value = DT.Rows(0).Item("SQAQ_Coil").ToString()
                t_SQAQ_DO.Value = DT.Rows(0).Item("SQAQ_DO").ToString()
                t_SQAQ_InDate.Value = DT.Rows(0).Item("SQAQ_InDate").ToString()
                t_SQAQ_NoPro.Value = DT.Rows(0).Item("SQAQ_NoPro").ToString()
                t_SQAQ_Note.Value = DT.Rows(0).Item("SQAQ_Note").ToString()
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

    Sub LoadRCISolve()
        Try
            Dim sqltxt As String
            Dim DT As DataTable
            Dim az As New AzureVM

            sqltxt = "SELECT SQAS_Name, SQAQ_RCISolProDet FROM SQAQue WHERE SQAQ_ID = '" & Session("SQAQ_ID") & "'"

            DT = az.GetDataTable(sqltxt)

            If DT.Rows.Count > 0 Then
                t_SQAS_Name.Value = DT.Rows(0).Item("SQAS_Name").ToString()
                t_SQAQ_RCISolProDet.Value = DT.Rows(0).Item("SQAQ_RCISolProDet").ToString()
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

    Sub LoadExpTypData()
        Try
            Dim sqltxt As String
            Dim DT As DataTable
            Dim az As New AzureVM

            sqltxt = "SELECT SQAQ_ExpTyp, SQAQ_ExpTypCou FROM SQAQue WHERE SQAQ_ID = '" & Session("SQAQ_ID") & "'"

            DT = az.GetDataTable(sqltxt)

            If DT.Rows.Count > 0 Then
                t_SQAQ_ExpTyp.Value = DT.Rows(0).Item("SQAQ_ExpTyp").ToString()
                t_SQAQ_ExpTypCou.Value = DT.Rows(0).Item("SQAQ_ExpTypCou").ToString()
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

    Sub LoadResultDetails()
        Try
            Dim sqltxt As String
            Dim DT As DataTable
            Dim az As New AzureVM

            sqltxt = "SELECT SQAQ_Res, SQAQ_ResDet, SQAQ_ResDetOth FROM SQAQue WHERE SQAQ_ID = '" & Session("SQAQ_ID") & "'"

            DT = az.GetDataTable(sqltxt)

            If DT.Rows.Count > 0 Then
                Dim resInt As Integer = 0
                Dim resDetInt As Integer = 0

                Integer.TryParse(DT.Rows(0).Item("SQAQ_Res").ToString(), resInt)
                Integer.TryParse(DT.Rows(0).Item("SQAQ_ResDet").ToString(), resDetInt)

                ' แปลงค่า SQAQ_Res
                Dim resText As String = ""
                Select Case resInt
                    Case 1
                        resText = "Reject"
                    Case 2
                        resText = "Hold"
                    Case 3
                        resText = "Warning"
                    Case Else
                        resText = "ไม่ระบุผลการประเมิน"
                End Select

                ' แปลงค่า SQAQ_ResDet
                Dim resDetText As String = ""
                Select Case resDetInt
                    Case 1
                        resDetText = "ส่งคืนผลิตภัณฑ์ที่ไม่เป็นไปตามข้อกำหนดให้กับทาง Supplier"
                    Case 2
                        resDetText = "เคลมกับทาง Supplier"
                    Case 3
                        resDetText = "ให้ทาง Supplier เข้ามาคัดแยกผลิตภัณฑ์ที่ไม่เป็นไปตามข้อกำหนด"
                    Case 4
                        resDetText = "ทาง RCI ทำการคัดแยกและเคลมค่าแรงกับทาง Supplier"
                    Case 5
                        resDetText = "ขอเอกสารชี้แจ้งสาเหตุและดำเนินการแก้ไข"
                    Case 6
                        resDetText = "ใช้ตามสภาพหรือยอมรับใช้พอเศษ (กรณีพบปัญหาซ้ำจะทำการ Reject ผลิตภัณฑ์ใน Lot. นั้นทั้งหมด)"
                    Case 7
                        resDetText = "อื่นๆ"
                    Case Else
                        resDetText = "ไม่ระบุการดำเนินการ"
                End Select

                ' กำหนดค่าไปยัง Control
                t_SQAQ_Res.Value = resText
                t_SQAQ_ResDet.Value = resDetText
                t_SQAQ_ResDetOth.Value = DT.Rows(0).Item("SQAQ_ResDetOth").ToString()

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

    Sub LoadAttachmentDetails()
        Try
            Dim sqltxt As String
            Dim DT As DataTable
            Dim az As New AzureVM

            sqltxt = "SELECT SQAA_Name, SQAQ_AttOth FROM SQAQue WHERE SQAQ_ID = '" & Session("SQAQ_ID") & "'"

            DT = az.GetDataTable(sqltxt)

            If DT.Rows.Count > 0 Then
                t_SQAA_Name.Value = DT.Rows(0).Item("SQAA_Name").ToString()
                t_SQAQ_AttOth.Value = DT.Rows(0).Item("SQAQ_AttOth").ToString()
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

    Sub LoadSIName()
        Try
            Dim sqltxt As String
            Dim DT As DataTable
            Dim az As New AzureVM

            sqltxt = "SELECT SQAQ_SIName FROM SQAQue WHERE SQAQ_ID = '" & Session("SQAQ_ID") & "'"

            DT = az.GetDataTable(sqltxt)

            If DT.Rows.Count > 0 Then
                t_SQAQ_SIName.Value = DT.Rows(0).Item("SQAQ_SIName").ToString()
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
    Sub iGetData_RelUpload() '-----เอาข้อมูลใส่ GridView RelUpload
        Try
            Dim sqltxt As String
            Dim DT As DataTable
            Dim az As New AzureVM
            Dim txt As String = ""
            Dim strVendor As String = ""

            sqltxt = "SELECT ROW_NUMBER() OVER(ORDER BY u.SQAU_ID) AS No, " &
         " q.SQAQ_ID, u.SQAU_ID, u.SQAU_Name, u.SQAU_Link, u.SQAU_Status " &
         "FROM SQAUpload u " &
         "LEFT JOIN SQAQue q ON u.SQAQ_ID = q.SQAQ_ID " &
         "WHERE u.SQAQ_ID = '" & Session("SQAQ_ID") & "' " &
         "ORDER BY u.SQAU_ID"

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

    Private Sub example_RelUpload_PreRender(sender As Object, e As EventArgs) Handles example_RelUpload.PreRender
        Try
            example_RelUpload.UseAccessibleHeader = True
            example_RelUpload.HeaderRow.TableSection = TableRowSection.TableHeader
        Catch ex As Exception

        End Try
    End Sub



    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Session("SQAQ_ID") = ""
        Response.Redirect("SetSupplier.aspx")
    End Sub

    Private Sub lnkNew_Click(sender As Object, e As EventArgs) Handles lnkNew.Click
        If Session("Manu") = "ExecN" Then

        Else
            Response.Redirect("SetSupplierE.aspx")
        End If
    End Sub

End Class