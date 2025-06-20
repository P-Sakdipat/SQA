Public Class MyWebPage
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Admin") = "" Then
            Response.Redirect("Login.aspx")
        End If
        ''Session("Admin") = "Admin"
        ''Session("uname") = "itd"
        If Not Page.IsPostBack Then

        Else

        End If
    End Sub

End Class