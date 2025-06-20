Public Class PageUserAdd
    Inherits System.Web.UI.Page
    Public X2 As String
    Private Const checkChar = "*/-+.%$#@!'"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Admin") = "" Then
            Response.Redirect("Login.aspx")
        End If
        'Session("Admin") = "Admin"
        'Session("uname") = "ITD"

        If Not Page.IsPostBack Then
            If Session("Admin") <> "Admin" Then
                Response.Redirect("MyWebPage.aspx")
            End If
            Me.iGetData()
        Else

        End If
    End Sub

    Sub iReset()
        tidno.Text = "ADD"
        tuser.Value = ""
        tname.Value = ""
        tpassword.Value = ""
        ddLevel.SelectedValue = "0"
        chkCancel.Checked = False
    End Sub

    Sub iGetData()
        Dim sqltxt As String
        Dim DT As DataTable
        Dim az As New AzureVM
        sqltxt = "select t1.*,t_status2 = Case t1.t_status When 0 Then N'ใช้งาน' Else N'ยกเลิก' End " &
                 "      ,t_type = Case t1.t_level When 1 Then 'Admin' When 2 Then 'Head RM' " &
                 "                 When 3 Then 'Sales RM' When 4 Then 'Executive' Else '-' End " &
                 " from facUser t1 "
        DT = az.GetDataTable(sqltxt)
        If DT.Rows.Count > 0 Then
            GridView1.DataSource = DT
        Else
            GridView1.DataSource = Nothing
        End If
        GridView1.DataBind()

        DT.Dispose()
        DT = Nothing
        az = Nothing
    End Sub

    Private Sub GridView1_PreRender(sender As Object, e As EventArgs) Handles GridView1.PreRender
        Try
            GridView1.UseAccessibleHeader = True
            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        Select Case e.CommandName
            Case "iEdit"
                Try
                    Dim sqltxt As String
                    Dim DT As DataTable
                    Dim az As New AzureVM
                    sqltxt = "Select * From facUser Where t_idno = '" & e.CommandArgument & "' "
                    DT = az.GetDataTable(sqltxt)
                    If DT.Rows.Count > 0 Then
                        tidno.Text = DT.Rows(0).Item("t_idno")
                        tuser.Value = DT.Rows(0).Item("t_user")
                        tname.Value = DT.Rows(0).Item("t_name")
                        ddLevel.SelectedValue = DT.Rows(0).Item("t_level")
                        If DT.Rows(0).Item("t_status") = "1" Then
                            chkCancel.Checked = True
                        Else
                            chkCancel.Checked = False
                        End If
                    Else
                        Me.iReset()
                        X2 = "ไม่พบผู้ใช้งาน "
                    End If
                    DT.Dispose()
                    DT = Nothing
                    az = Nothing
                Catch ex As Exception
                    X2 = ex.Message
                End Try

            Case "iDelete"
                Try
                    Dim sqltxt As String
                    Dim DT As DataTable
                    Dim az As New AzureVM
                    Dim ipad As String = IPNetworking.GetIP4Address
                    sqltxt = "Select t_user,t_name From facUser Where t_idno = '" & e.CommandArgument & "' "
                    DT = az.GetDataTable(sqltxt)
                    If DT.Rows.Count > 0 Then
                        sqltxt = "delete facuser where t_idno = '" & e.CommandArgument & "' "
                        Dim y As Integer = az.Execute(sqltxt)
                        If y > 0 Then
                            Me.iReset()
                            Me.iGetData()
                            sqltxt = "insert into facLogData(t_user,t_type,t_note,t_ipad,t_date) values('" &
                                     Session("uname") & "','3',N'Delete User " & DT.Rows(0).Item("t_user") & " : " & DT.Rows(0).Item("t_name") & "','" & ipad & "', getdate() ) "
                            az.Execute(sqltxt)
                        Else
                            X2 = "ลบไม่สำเร็จ"
                        End If
                    End If
                    DT.Dispose()
                    DT = Nothing
                    az = Nothing
                Catch ex As Exception
                    X2 = ex.Message
                End Try
        End Select
    End Sub

    Private Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim drv As DataRowView = e.Row.DataItem
            Dim xDelete As LinkButton = e.Row.FindControl("lbtDelete")
            Dim xStatus As Label = e.Row.FindControl("lblStatus")

            xDelete.Attributes.Add("onclick", "javascript:return confirm('ลบข้อมูล ?');")

            If drv!t_status = "1" Then
                e.Row.ForeColor = Drawing.Color.Gray
                xStatus.Text = "<i class='fa fa-toggle-off' aria-hidden='true'></i>"
                xStatus.ForeColor = Drawing.Color.Gray
            Else
                xStatus.Text = "<i class='fa fa-toggle-on' aria-hidden='true'></i>"
                xStatus.ForeColor = Drawing.Color.Blue
            End If

        End If
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.iReset()
    End Sub

    Private Sub btnHSave_Click(sender As Object, e As EventArgs) Handles btnHSave.Click
        Try
            If tname.Value.Trim = "" Or tuser.Value.Trim = "" Or ddLevel.SelectedValue = "0" Then
                X2 = "กรุณากรอก User Name, Full Name, User Type "
                Exit Sub
            End If
            If tuser.Value.Trim.IndexOfAny(checkChar.ToArray) > -1 Then
                X2 = "User Name ไม่ถูกต้อง ( ห้ามมีเครื่องหมาย */-+.%$#@!' )"
                Exit Sub
            End If

            Dim az As New AzureVM
            Dim sqltxt As String
            Dim DT As DataTable
            Dim txt As String = ""
            Dim ipad As String = IPNetworking.GetIP4Address

            If tidno.Text = "ADD" Then
                '' Add new user
                Dim mxid As String
                If tpassword.Value.Trim > "" Then
                    sqltxt = "select t_user from facUser where t_user = '" & tuser.Value.Trim & "' "
                    DT = az.GetDataTable(sqltxt)
                    If DT.Rows.Count > 0 Then
                        X2 = "User Name ซ้ำ"
                    Else
                        sqltxt = "select max(isnull(t_idno,0)) + 1 mxid from facUser "
                        DT = az.GetDataTable(sqltxt)
                        If DT.Rows.Count > 0 Then
                            mxid = DT.Rows(0).Item("mxid")
                            sqltxt = "insert into facUser(t_idno,t_user,t_pass,t_name,t_level,t_status,t_sdate) values('" &
                                     mxid & "','" & tuser.Value.Trim & "','" & tpassword.Value.Trim & "',N'" &
                                     tname.Value.Trim.Replace("'", "''") & "','" & ddLevel.SelectedValue & "','0', getdate() )"
                            Dim y As Integer = az.Execute(sqltxt)
                            If y > 0 Then
                                Me.iReset()
                                Me.iGetData()
                                sqltxt = "insert into facLogData(t_user,t_type,t_note,t_ipad,t_date) values('" &
                                         Session("uname") & "','1','Add User " & tuser.Value.Trim & "','" & ipad & "', getdate() ) "
                                az.Execute(sqltxt)
                                X2 = "บันทึกเสร็จแล้ว"
                            Else
                                X2 = "บันทึกไม่สำเร็จ"
                            End If
                        Else
                            X2 = "บันทึกไม่สำเร็จ"
                        End If
                    End If
                    DT.Dispose()
                    DT = Nothing
                    az = Nothing
                Else
                    X2 = "กรุณากรอกรหัสผ่าน"
                End If
            Else
                '' Edit user
                If tpassword.Value.Trim > "" Then
                    txt = ",t_pass = '" & tpassword.Value.Trim & "' "
                Else
                    txt = ""
                End If
                Dim strStatus As String
                If chkCancel.Checked = True Then
                    strStatus = "1"
                Else
                    strStatus = "0"
                End If

                sqltxt = "Select t_user from facUser where t_user = '" & tuser.Value.Trim & "' and t_idno <> '" & tidno.Text & "' "
                DT = az.GetDataTable(sqltxt)
                If DT.Rows.Count > 0 Then
                    X2 = "User Name ซ้ำ"
                Else
                    sqltxt = "Update facUser set " &
                             " t_user = '" & tuser.Value.Trim & "' " &
                             txt &
                             ", t_name = N'" & tname.Value.Trim.Replace("'", "''") & "' " &
                             ", t_level = '" & ddLevel.SelectedValue & "' " &
                             ", t_status = '" & strStatus & "' " &
                             ", t_sdate = getdate() " &
                             " Where t_idno = '" & tidno.Text & "' "
                    Dim y As Integer = az.Execute(sqltxt)
                    If y > 0 Then
                        Me.iReset()
                        Me.iGetData()
                        sqltxt = "insert into facLogData(t_user,t_type,t_note,t_ipad,t_date) values('" &
                                 Session("uname") & "','2','Update User " & tuser.Value.Trim & "','" & ipad & "', getdate() ) "
                        az.Execute(sqltxt)
                        X2 = "แก้ไขเสร็จแล้ว"
                    Else
                        X2 = "แก้ไขไม่สำเร็จ"
                    End If
                End If
                DT.Dispose()
                DT = Nothing
                az = Nothing
            End If

        Catch ex As Exception
            X2 = ex.Message
        End Try
    End Sub

End Class