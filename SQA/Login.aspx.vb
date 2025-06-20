Imports System.Net
Imports System.IO
Imports System.Web.Script.Serialization

Public Class Login
    Inherits System.Web.UI.Page
    Public X2 As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            showError.Visible = False
            Try
                If Not IsNothing(Request.Cookies("FACCookie")) Then
                    ''Dim delCookie As HttpCookie
                    ''delCookie = New HttpCookie("FACCookie")
                    ''delCookie.Expires = DateTime.Now.AddDays(-1D)
                    ''Response.Cookies.Add(delCookie)
                    ''Dim sqltxt As String
                    ''Dim az As New AzureVM
                    ''sqltxt = "delete facCookies where t_user = '" & Session("uname") & "' "
                    ''az.Execute(sqltxt)
                    ''az = Nothing

                    If Request.Cookies("FACCookie")("Name") = "" Then
                        Exit Sub
                    End If
                    Dim encry As New codify
                    Dim xUser As String = encry.Decrypt(Request.Cookies("FACCookie")("Name"))
                    Dim xCode = Request.Cookies("FACCookie")("Code")

                    Dim sqltxt As String
                    Dim DT As DataTable
                    Dim az As New AzureVM
                    sqltxt = "Select * from facCookies where t_code = '" & xCode & "' "
                    DT = az.GetDataTable(sqltxt)
                    If DT.Rows.Count > 0 Then
                        If DT.Rows(0).Item("t_user") = xUser Then
                            '' login
                            Dim ipad As String = IPNetworking.GetIP4Address
                            sqltxt = "Insert into facLogin(t_user,t_type,t_note,t_ipad,t_date) values('" & xUser & "','1','cookie login','" & ipad & "', getdate() )"
                            az.Execute(sqltxt)

                            sqltxt = "delete facCookies where t_code = '" & xCode & "' "
                            az.Execute(sqltxt)

                            Dim r As New Random
                            Dim myCookie As String = RandomString(r)
                            Dim newCookie As HttpCookie = New HttpCookie("FACCookie")
                            newCookie("Code") = myCookie
                            newCookie("Name") = encry.encrypt(xUser)
                            newCookie.Expires = DateTime.Now.AddDays(30)
                            Response.Cookies.Add(newCookie)
                            sqltxt = "insert into facCookies(t_user,t_code,t_date) values('" & xUser & "','" & myCookie & "', getdate() ) "
                            az.Execute(sqltxt)

                            sqltxt = "select t_idno,t_user,t_name,t_level from facUser where isnull(t_status,'0') <> '1' and t_user = '" & xUser & "' "
                            DT = az.GetDataTable(sqltxt)

                            If DT.Rows(0).Item("t_level") = "1" Then
                                Session("Admin") = "Admin"
                                Session("uname") = DT.Rows(0).Item("t_user")
                                Session("fname") = DT.Rows(0).Item("t_name")
                                Session("Manu") = "Admin"
                                Response.Redirect("MyWebPage.aspx")
                            ElseIf DT.Rows(0).Item("t_level") = "2" Then
                                Session("Admin") = "Exec"
                                Session("uname") = DT.Rows(0).Item("t_user")
                                Session("fname") = DT.Rows(0).Item("t_name")
                                Session("Manu") = "Exec" 'ผู้บริหาร
                                Response.Redirect("MyWebPage.aspx")
                            ElseIf DT.Rows(0).Item("t_level") = "3" Then
                                Session("Admin") = "User"
                                Session("uname") = DT.Rows(0).Item("t_user")
                                Session("fname") = DT.Rows(0).Item("t_name")
                                Session("Manu") = "User"
                                Response.Redirect("MyWebPage.aspx")
                            ElseIf DT.Rows(0).Item("t_level") = "4" Then
                                Session("Admin") = "RCI"
                                Session("uname") = DT.Rows(0).Item("t_user")
                                Session("fname") = DT.Rows(0).Item("t_name")
                                Session("Manu") = "RCI"
                                Response.Redirect("MyWebPage.aspx")
                            ElseIf DT.Rows(0).Item("t_level") = "5" Then
                                Session("Admin") = "DSI"
                                Session("uname") = DT.Rows(0).Item("t_user")
                                Session("fname") = DT.Rows(0).Item("t_name")
                                Session("Manu") = "DSI"
                                Response.Redirect("MyWebPage.aspx")

                            ElseIf DT.Rows(0).Item("t_level") = "6" Then
                                Session("Admin") = "Admin"
                                Session("uname") = DT.Rows(0).Item("t_user")
                                Session("fname") = DT.Rows(0).Item("t_name")
                                Session("Manu") = "RD"
                                Response.Redirect("MyWebPage.aspx")
                            ElseIf DT.Rows(0).Item("t_level") = "7" Then
                                Session("Admin") = "Admin"
                                Session("uname") = DT.Rows(0).Item("t_user")
                                Session("fname") = DT.Rows(0).Item("t_name")
                                Session("Manu") = "PUR"
                                Response.Redirect("MyWebPage.aspx")
                            ElseIf DT.Rows(0).Item("t_level") = "8" Then
                                Session("Admin") = "Admin"
                                Session("uname") = DT.Rows(0).Item("t_user")
                                Session("fname") = DT.Rows(0).Item("t_name")
                                Session("Manu") = "MCD"
                                Response.Redirect("MyWebPage.aspx")
                            ElseIf DT.Rows(0).Item("t_level") = "9" Then
                                Session("Admin") = "Admin"
                                Session("uname") = DT.Rows(0).Item("t_user")
                                Session("fname") = DT.Rows(0).Item("t_name")
                                Session("Manu") = "supplier"
                                Response.Redirect("MyWebPage.aspx")
                            ElseIf DT.Rows(0).Item("t_level") = "10" Then
                                Session("Admin") = "Admin"
                                Session("uname") = DT.Rows(0).Item("t_user")
                                Session("fname") = DT.Rows(0).Item("t_name")
                                Session("Manu") = "ExecN" 'ผู้บริหาร
                                Response.Redirect("MyWebPage.aspx")
                            ElseIf DT.Rows(0).Item("t_level") = "11" Then
                                Session("Admin") = "Admin"
                                Session("uname") = DT.Rows(0).Item("t_user")
                                Session("fname") = DT.Rows(0).Item("t_name")
                                Session("Manu") = "UserN"
                                Response.Redirect("MyWebPage.aspx")
                            Else
                                X2 = "ยังไม่กำหนดสิทธิ์"
                                Exit Sub
                            End If
                        Else
                            'SendLines("SalesSmart Cookie", "User ไม่ตรง -> " & xUser)
                        End If
                    Else
                        'SendLines("SalesSmart Cookie", "code cookie ไม่ตรง -> " & xCode)
                    End If
                    DT.Dispose()
                    DT = Nothing
                    az = Nothing

                End If
            Catch ex As Exception
                X2 = ex.Message
            End Try
        End If
    End Sub

    Sub iLogin()
        Try
            If txtUser.Value.Trim > "" Then
                Dim az = New AzureVM
                Dim DT As DataTable
                Dim sqltxt As String
                Dim ipad As String = IPNetworking.GetIP4Address

                '' เช็ค login ย้อนหลัง 1 ชม.
                sqltxt = "Select * from facLogin where t_user = '" & txtUser.Value.Trim.Replace("'", "").Replace(" ", "") & "' and t_type = '2' and t_date > DateAdd(hh, -1, getdate()) "
                DT = az.GetDataTable(sqltxt)
                If DT.Rows.Count > 5 Then
                    sqltxt = "Insert into facLogin(t_user,t_type,t_note,t_ipad,t_date) values('" & txtUser.Value.Trim.Replace("'", "").Replace(" ", "") & "','2','','" & ipad & "', getdate() )"
                    az.Execute(sqltxt)
                    SendLines("Factory Intelligent กรอกรหัสผ่านผิด 5 ครั้ง", txtUser.Value.Trim & " (" & ipad & ")")
                    X2 = "กรอกรหัสผิดเกิน 5 ครั้ง"
                    Exit Sub
                End If

                sqltxt = "Select * from facUser where t_user = '" & txtUser.Value.Trim.Replace("'", "").Replace(" ", "") & "' and isnull(t_status,0) = '0' "
                DT = az.GetDataTable(sqltxt)
                If DT.Rows.Count > 0 Then
                    If txtPass.Value = DT.Rows(0).Item("t_pass") Then
                        sqltxt = "Insert into facLogin(t_user,t_type,t_note,t_ipad,t_date) values('" & txtUser.Value.Trim.Replace("'", "").Replace(" ", "") & "','1','Factory','" & ipad & "', getdate() )"
                        az.Execute(sqltxt)

                        Dim encry As New codify
                        Dim r As New Random
                        Dim myCookie As String = RandomString(r)
                        Dim newCookie As HttpCookie = New HttpCookie("FACCookie")
                        newCookie("Code") = myCookie
                        newCookie("Name") = encry.encrypt(txtUser.Value.Trim.ToUpper)

                        newCookie.Expires = DateTime.Now.AddDays(30)
                        Response.Cookies.Add(newCookie)
                        sqltxt = "insert into facCookies(t_user,t_code,t_date) values('" & txtUser.Value.Trim.ToUpper & "','" & myCookie & "', getdate() ) "
                        az.Execute(sqltxt)

                        If DT.Rows(0).Item("t_level") = "1" Then
                            Session("Admin") = "Admin"
                            Session("uname") = txtUser.Value.Trim.ToUpper
                            Session("fname") = DT.Rows(0).Item("t_name")
                            Session("Manu") = "Admin"
                            Response.Redirect("MyWebPage.aspx")
                        ElseIf DT.Rows(0).Item("t_level") = "2" Then
                            Session("Admin") = "Exec"
                            Session("uname") = txtUser.Value.Trim.ToUpper
                            Session("fname") = DT.Rows(0).Item("t_name")
                            Session("Manu") = "Exec"
                            Response.Redirect("MyWebPage.aspx")
                        ElseIf DT.Rows(0).Item("t_level") = "3" Then
                            Session("Admin") = "User"
                            Session("uname") = txtUser.Value.Trim.ToUpper
                            Session("fname") = DT.Rows(0).Item("t_name")
                            Session("Manu") = "User"
                            Response.Redirect("MyWebPage.aspx")
                        ElseIf DT.Rows(0).Item("t_level") = "4" Then
                            Session("Admin") = "RCI"
                            Session("uname") = txtUser.Value.Trim.ToUpper
                            Session("fname") = DT.Rows(0).Item("t_name")
                            Session("Manu") = "RCI"
                            Response.Redirect("MyWebPage.aspx")
                        ElseIf DT.Rows(0).Item("t_level") = "5" Then
                            Session("Admin") = "DSI"
                            Session("uname") = txtUser.Value.Trim.ToUpper
                            Session("fname") = DT.Rows(0).Item("t_name")
                            Session("Manu") = "DSI"
                            Response.Redirect("MyWebPage.aspx")

                        ElseIf DT.Rows(0).Item("t_level") = "6" Then
                            Session("Admin") = "Admin"
                            Session("uname") = DT.Rows(0).Item("t_user")
                            Session("fname") = DT.Rows(0).Item("t_name")
                            Session("Manu") = "RD"
                            Response.Redirect("MyWebPage.aspx")
                        ElseIf DT.Rows(0).Item("t_level") = "7" Then
                            Session("Admin") = "Admin"
                            Session("uname") = DT.Rows(0).Item("t_user")
                            Session("fname") = DT.Rows(0).Item("t_name")
                            Session("Manu") = "PUR"
                            Response.Redirect("MyWebPage.aspx")
                        ElseIf DT.Rows(0).Item("t_level") = "8" Then
                            Session("Admin") = "Admin"
                            Session("uname") = DT.Rows(0).Item("t_user")
                            Session("fname") = DT.Rows(0).Item("t_name")
                            Session("Manu") = "MCD"
                            Response.Redirect("MyWebPage.aspx")
                        ElseIf DT.Rows(0).Item("t_level") = "9" Then
                            Session("Admin") = "Admin"
                            Session("uname") = DT.Rows(0).Item("t_user")
                            Session("fname") = DT.Rows(0).Item("t_name")
                            Session("Manu") = "supplier"
                            Response.Redirect("MyWebPage.aspx")
                        ElseIf DT.Rows(0).Item("t_level") = "10" Then
                            Session("Admin") = "Admin"
                            Session("uname") = DT.Rows(0).Item("t_user")
                            Session("fname") = DT.Rows(0).Item("t_name")
                            Session("Manu") = "ExecN" 'ผู้บริหาร
                            Response.Redirect("MyWebPage.aspx")
                        ElseIf DT.Rows(0).Item("t_level") = "11" Then
                            Session("Admin") = "Admin"
                            Session("uname") = DT.Rows(0).Item("t_user")
                            Session("fname") = DT.Rows(0).Item("t_name")
                            Session("Manu") = "UserN"
                            Response.Redirect("MyWebPage.aspx")
                        Else
                            X2 = "ยังไม่กำหนดสิทธิ์"
                            Exit Sub
                        End If

                    Else
                        '' fail
                        sqltxt = "Insert into facLogin(t_user,t_type,t_note,t_ipad,t_date) values('" & txtUser.Value.Trim.Replace("'", "").Replace(" ", "") & "','2','Factory','" & ipad & "', getdate() )"
                        az.Execute(sqltxt)
                        X2 = "Login ไม่ถูกต้อง !"
                    End If

                Else
                    sqltxt = "Insert into facLogin(t_user,t_type,t_note,t_ipad,t_date) values('" & txtUser.Value.Trim.Replace("'", "").Replace(" ", "") & "','2','Factory','" & ipad & "', getdate() )"
                    az.Execute(sqltxt)
                    X2 = "Login ไม่ถูกต้อง"
                End If

                DT.Dispose()
                DT = Nothing
                az = Nothing
            Else
                X2 = "user/password ไม่ถูกต้อง"
            End If
        Catch ex As Exception
            X2 = ex.Message
        End Try
    End Sub

    Sub SendLines(ByVal refNo, ByVal remarkStr)
        Try
            System.Net.ServicePointManager.Expect100Continue = False
            System.Net.ServicePointManager.SecurityProtocol = CType(3072, SecurityProtocolType)
            Dim request = DirectCast(WebRequest.Create("https://notify-api.line.me/api/notify"), HttpWebRequest)
            Dim postData = String.Format("message={0}", "** " & refNo & " **" & vbCrLf & remarkStr)
            Dim data = Encoding.UTF8.GetBytes(postData)
            request.Method = "POST"
            request.ContentType = "application/x-www-form-urlencoded"
            request.ContentLength = data.Length
            request.Headers.Add("Authorization", "Bearer 6dVg35TICj4SKW9IdStjkSlxFLDlMxCnPGmW90fyEfa")
            request.AllowWriteStreamBuffering = True
            request.KeepAlive = False
            request.Credentials = CredentialCache.DefaultCredentials
            Using stream = request.GetRequestStream()
                stream.Write(data, 0, data.Length)
            End Using
            Dim response = DirectCast(request.GetResponse(), HttpWebResponse)
            Dim responseString = New StreamReader(response.GetResponseStream()).ReadToEnd()
        Catch ex As Exception

        Finally

        End Try
    End Sub

    Function RandomString(r As Random)
        Dim s As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"
        Dim sb As New StringBuilder
        Dim cnt As Integer = r.Next(15, 33)
        For i As Integer = 1 To cnt
            Dim idx As Integer = r.Next(0, s.Length)
            sb.Append(s.Substring(idx, 1))
        Next
        Return sb.ToString()
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            'Dim url As String
            Dim secretKey As String = "6LcfXqckAAAAAFaHJ8qWGAbp8sVFaTdsiZI4lhIh"
            Dim recaptcha_response = gRecaptchaResponse.Value
            Label1.Text = ""
            Label1.Visible = True

            ' Create a request using a URL that can receive a post.   
            Dim Myrequest As WebRequest = WebRequest.Create("https://www.google.com/recaptcha/api/siteverify")
            ' Set the Method property of the request to POST.  
            Myrequest.Method = "POST"

            ' Create POST data and convert it to a byte array.  
            Dim ipad As String = IPNetworking.GetIP4Address
            Dim postData As String = "secret=" & secretKey & "&response=" & recaptcha_response & "&remoteip=" & ipad
            Dim byteArray As Byte() = Encoding.UTF8.GetBytes(postData)

            ' Set the ContentType property of the WebRequest.  
            Myrequest.ContentType = "application/x-www-form-urlencoded"
            ' Set the ContentLength property of the WebRequest.  
            Myrequest.ContentLength = byteArray.Length

            ' Get the request stream.  
            Dim dataStream As Stream = Myrequest.GetRequestStream()
            ' Write the data to the request stream.  
            dataStream.Write(byteArray, 0, byteArray.Length)
            ' Close the Stream object.  
            dataStream.Close()

            ' Get the response.  
            Dim wresponse As WebResponse = Myrequest.GetResponse()
            ' Display the status.  
            Console.WriteLine(CType(wresponse, HttpWebResponse).StatusDescription)

            ' Get the stream containing content returned by the server.  
            ' The using block ensures the stream is automatically closed.
            Using dataStream1 As Stream = wresponse.GetResponseStream()
                ' Open the stream using a StreamReader for easy access.  
                Dim reader As New StreamReader(dataStream1)
                ' Read the content.  
                Dim responseFromServer As String = reader.ReadToEnd()
                ' Response to string.  
                Dim jsonData As String = responseFromServer
                Dim JsonResults As Object = New JavaScriptSerializer().Deserialize(Of Object)(jsonData)

                If JsonResults("success") = True Then
                    'Submit request
                    'For testing only
                    '----------------------------------------------------------------------
                    ''Label1.Text = "<br/>"
                    ''Label1.Text &= "success: " & JsonResults("success") & "<br/>"
                    ''Label1.Text &= "score: " & JsonResults("score") & "<br/>"
                    ' ''Label1.Text &= "action: " & JsonResults("action") & "<br/>"
                    ''Label1.Text &= "challenge_ts: " & JsonResults("challenge_ts") & "<br/>"
                    '----------------------------------------------------------------------

                    If JsonResults("score") >= "0.6" Then
                        ''Response.Write("Save<br>")
                        Try
                            Me.iLogin()
                        Catch ex As Exception
                            Label1.Text = ex.Message
                        End Try

                    Else
                        Label1.Text = "score: < 0.6"
                    End If


                Else
                    'Stop submit
                    'For testing only
                    '----------------------------------------------------------------------
                    Label1.Text &= "success: " & JsonResults("success") & "<br/>"
                    Label1.Text &= "errorcodes: " & JsonResults("error-codes")(0) & "<br/>"
                    '----------------------------------------------------------------------
                End If

            End Using

            ' Clean up the response.  
            wresponse.Close()
        Catch ex As Exception
            Label1.Text = ex.Message
        End Try
    End Sub
End Class