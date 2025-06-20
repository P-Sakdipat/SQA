Imports System.Data.SqlClient
Imports System.IO
Public Class PageUser
    Inherits System.Web.UI.Page
    Public X2 As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("uname") = "" Then
            Response.Redirect("Login.aspx")
        End If
        ''Session("Admin") = "Admin"
        ''Session("uname") = "itd"
        If Not Page.IsPostBack Then
            If Session("Admin") <> "Admin" Then
                'menuSetup.Visible = False
            End If
            tuser.Value = Session("uname")
            Me.iGetData()
        Else

        End If
    End Sub

    Sub iGetData()
        Dim sqltxt As String
        Dim DT As DataTable
        Dim az As New AzureVM

        sqltxt = "select t_name,t_idno from facUser where t_user = '" & Session("uname") & "' "
        DT = az.GetDataTable(sqltxt)
        If DT.Rows.Count > 0 Then
            tname.Value = DT.Rows(0).Item("t_name")
            tidno.Text = DT.Rows(0).Item("t_idno")
            sqltxt = " Select * from facImageRC where t_idno = '" & DT.Rows(0).Item("t_idno") & "'"
            DT = az.GetDataTable(sqltxt)
            If DT.Rows.Count > 0 Then
                Dim imageUrl As String = "data:image/jpg;base64," & Convert.ToBase64String(CType(DT.Rows(0).Item("t_image"), Byte()))
                wizardPicturePreview.Src = imageUrl
            Else
                wizardPicturePreview.Src = "./images/manager128.png"
            End If
        Else
            tname.Value = ""
            tidno.Text = ""
            Exit Sub
        End If


        DT.Dispose()
        DT = Nothing
        az = Nothing

    End Sub

    Private Sub btnSave_ServerClick(sender As Object, e As EventArgs) Handles btnSave.ServerClick
        Try
            Dim sqltxt As String
            Dim DT As DataTable
            Dim az As New AzureVM

            sqltxt = "select * from facUser where t_user = '" & Session("uname") & "' "
            DT = az.GetDataTable(sqltxt)
            If DT.Rows.Count > 0 Then
                If DT.Rows(0).Item("t_pass") = topass.Value Then
                    sqltxt = "Update facUser set " &
                             " t_pass = N'" & tnpass.Value.Replace("'", "''") & "' " &
                             " Where t_user = '" & tuser.Value & "' "
                    Dim y As Integer = az.Execute(sqltxt)
                    If y > 0 Then
                        X2 = "แก้ไขรหัสผ่านเสร็จแล้ว"
                        Dim ipad As String = IPNetworking.GetIP4Address
                        sqltxt = "insert into facLogData(t_user,t_type,t_note,t_ipad,t_date) values('" &
                                 Session("uname") & "','2',N'เปลี่ยนรหัสผ่าน','" & ipad & "', getdate() ) "
                        az.Execute(sqltxt)
                        Me.iGetData()
                    Else
                        X2 = "แก้ไขไม่สำเร็จ"
                    End If
                Else
                    X2 = "รหัสเก่าไม่ถูกต้อง"
                    Exit Sub
                End If
            End If

            DT.Dispose()
            DT = Nothing
            az = Nothing
        Catch ex As Exception
            X2 = ex.Message
        End Try
    End Sub

    Private Sub btnHSave_Click(sender As Object, e As EventArgs) Handles btnHSave.Click
        Try
            If tname.Value.Trim = "" Then
                X2 = "กรุณากรอกชื่อ-นามสกุล"
                Exit Sub
            End If
            If tidno.Text.Trim = "" Then
                X2 = "no userid บันทึกไม่สำเร็จ"
                Exit Sub
            End If

            ''เช็คไฟล์รูปภาพ
            If file2.PostedFile.FileName > "" Then
                Dim filename As String = Path.GetFileName(file2.PostedFile.FileName)
                Dim extension As String = Path.GetExtension(filename)
                If extension = ".jpg" Or extension = ".png" Or extension = ".jpeg" Then

                Else
                    X2 = "อนุญาตเฉพาะไฟล์ .jpg | .jpeg | .png เท่านั้น "
                    Exit Sub
                End If
                If file2.PostedFile.ContentLength > 3145728 Then
                    X2 = "ไม่สามารถบันทึกได้\nไฟล์แนบมีขนาดใหญ่กว่า 3MB. กรุณาลดขนาดไฟล์"
                    Exit Sub
                End If

            End If

            Dim sqltxt As String
            Dim DT As DataTable
            Dim az As New AzureVM

            sqltxt = "select * from facUser where t_user = '" & Session("uname") & "' "
            DT = az.GetDataTable(sqltxt)
            If DT.Rows.Count > 0 Then
                sqltxt = "Update facUser set " &
                         " t_name = N'" & tname.Value.Trim.Replace("'", "''") & "' " &
                         " Where t_user = '" & tuser.Value & "' "
                Dim y As Integer = az.Execute(sqltxt)
                If y > 0 Then
                    Dim ipad As String = IPNetworking.GetIP4Address
                    sqltxt = "insert into facLogData(t_user,t_type,t_note,t_ipad,t_date) values('" &
                             Session("uname") & "','2',N'Update User Profile','" & ipad & "', getdate() ) "
                    az.Execute(sqltxt)


                    If file2.Value > "" Then
                        Dim strSql As String
                        Dim Cmd As SqlCommand
                        sqltxt = "select * from facImageRC where t_idno = '" & tidno.Text & "' "
                        DT = az.GetDataTable(sqltxt)
                        If DT.Rows.Count > 0 Then
                            ''มีรูปเดิมให้ update รูปใหม่
                            Dim c1 As New MISSQL
                            strSql = " Update facImageRC set " &
                                     " t_image = @P1 " &
                                     " Where t_idno = '" & tidno.Text & "' "
                            Cmd = c1.CreateCommand(strSql)
                            c1.CreateParam(Cmd, "P")
                            Dim s1 As IO.Stream = file2.PostedFile.InputStream
                            Dim XSize As Long = file2.PostedFile.ContentLength
                            Dim B1(XSize) As Byte
                            Dim X As Integer = s1.Read(B1, 0, XSize)
                            Cmd.Parameters("@P1").Value = B1
                            Dim z As Integer = c1.Execute(Cmd)
                            If z > 0 Then
                                ''X2 = "บันทึกเสร็จแล้ว"
                                ''Me.iGetData()
                            Else
                                X2 = "บันทึกรูปภาพไม่สำเร็จ (Update user picture)"
                                Exit Sub
                            End If
                            c1 = Nothing
                        Else
                            ''ยังไม่มีรูป ใส่รูปใหม่
                            Dim c1 As New AzureVM
                            strSql = " insert into facImageRC(t_idno,t_image) values(@P1, @P2) "
                            Cmd = c1.CreateCommand(strSql)
                            c1.CreateParam(Cmd, "IP")
                            Cmd.Parameters("@P1").Value = tidno.Text
                            Dim s1 As IO.Stream = file2.PostedFile.InputStream
                            Dim XSize As Long = file2.PostedFile.ContentLength
                            Dim B1(XSize) As Byte
                            Dim X As Integer = s1.Read(B1, 0, XSize)
                            Cmd.Parameters("@P2").Value = B1
                            y = c1.Execute(Cmd)
                            If y > 0 Then
                                ''X2 = "บันทึกเสร็จแล้ว"
                                ''Me.iGetData()
                            Else
                                X2 = "บันทึกรูปภาพไม่สำเร็จ (Add User Picture)"
                                Exit Sub
                            End If
                            c1 = Nothing
                        End If
                    End If

                    X2 = "แก้ไขข้อมูลเสร็จแล้ว"
                    Me.iGetData()
                Else
                    X2 = "แก้ไขไม่สำเร็จ"
                End If

            End If

            DT.Dispose()
            DT = Nothing
            az = Nothing
        Catch ex As Exception
            X2 = ex.Message
        End Try
    End Sub

End Class