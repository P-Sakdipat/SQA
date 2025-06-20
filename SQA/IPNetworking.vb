Imports System.Net
Imports System.IO
Imports System.Web.Script.Serialization

Public Class IPNetworking
    Public Shared Function GetIP4Address() As String
        Dim IP4Address As String = String.Empty

        For Each IPA As IPAddress In Dns.GetHostAddresses(HttpContext.Current.Request.UserHostAddress)
            If IPA.AddressFamily.ToString() = "InterNetwork" Then
                IP4Address = IPA.ToString()
                Exit For
            End If
        Next
        If IP4Address <> String.Empty Then
            Return IP4Address
        End If

        For Each IPA As IPAddress In Dns.GetHostAddresses(Dns.GetHostName())
            If IPA.AddressFamily.ToString() = "InterNetwork" Then
                IP4Address = IPA.ToString()
                Exit For
            End If
        Next
        Return IP4Address
    End Function

    Public Shared Function CookieLogin() As String
        Try
            ''Dim Cookie = HttpContext.Current.Request.Cookies.Get("FACCookie")
            ''If Not IsNothing(Cookie) Then
            ''    Return HttpContext.Current.Request.Cookies.Get("FACCookie")("Code")

            ''Else
            ''    Return "xxx"
            ''End If

            If Not IsNothing(HttpContext.Current.Request.Cookies.Get("FACCookie")) Then

                If HttpContext.Current.Request.Cookies.Get("FACCookie")("Name") = "" Then
                    Return ""
                End If
                Dim encry As New codify
                Dim xUser As String = encry.Decrypt(HttpContext.Current.Request.Cookies.Get("FACCookie")("Name"))
                Dim xCode = HttpContext.Current.Request.Cookies.Get("FACCookie")("Code")

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
                        HttpContext.Current.Response.Cookies.Set(newCookie)

                        xCode = myCookie
                        ''xCode = HttpContext.Current.Request.Cookies.Get("FACCookie")("Code")

                        sqltxt = "insert into facCookies(t_user,t_code,t_date) values('" & xUser & "','" & myCookie & "', getdate() ) "
                        az.Execute(sqltxt)

                        DT.Dispose()
                        DT = Nothing
                        az = Nothing


                        Return xUser & "," & xCode

                    Else
                        ''SendLines("SalesSmart Cookie", "User ไม่ตรง -> " & xUser)
                        Return ""
                    End If
                Else
                    Return ""
                    ''SendLines("SalesSmart Cookie", "code cookie ไม่ตรง -> " & xCode)
                End If
            Else
                Return ""
            End If
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function

    Public Shared Sub SendLines(ByVal refNo, ByVal remarkStr)
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

    Public Shared Function RandomString(r As Random)
        Dim s As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"
        Dim sb As New StringBuilder
        Dim cnt As Integer = r.Next(15, 33)
        For i As Integer = 1 To cnt
            Dim idx As Integer = r.Next(0, s.Length)
            sb.Append(s.Substring(idx, 1))
        Next
        Return sb.ToString()
    End Function

End Class