Public Class Logout
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim sqltxt As String
            Dim az As New AzureVM

            If Not IsNothing(Request.Cookies("FACCookie")) Then
                Dim xCode = Request.Cookies("FACCookie")("Code")
                Dim delCookie As HttpCookie
                delCookie = New HttpCookie("FACCookie")
                delCookie.Expires = DateTime.Now.AddDays(-1D)
                Response.Cookies.Add(delCookie)
                sqltxt = "delete facCookies where t_user = '" & Session("uname") & "' and t_code = '" & xCode & "' "
                az.Execute(sqltxt)
            End If

            Dim ipad As String = IPNetworking.GetIP4Address
            sqltxt = "Insert into facLogin(t_user,t_type,t_note,t_ipad,t_date) values('" & Session("uname") & "','3','Factory','" & ipad & "', getdate() )"
            az.Execute(sqltxt)
            az = Nothing

            Session("Admin") = ""
            Session("uname") = ""
            Session("Manu") = ""
            Session.Abandon()
            Response.Redirect("Login.aspx")

        End If
    End Sub

End Class