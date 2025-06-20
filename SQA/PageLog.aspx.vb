Public Class PageLog
    Inherits System.Web.UI.Page
    Dim DV1 As DataView
    Public X2 As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Admin") = "" Then
            Response.Redirect("Login.aspx")
        End If
        ''Session("Admin") = "Admin"
        ''Session("uname") = "itd"
        If Not Page.IsPostBack Then
            Me.iGetData()
        Else
            DV1 = Session("xlogin")
        End If
    End Sub

    Sub iGetData()
        Dim sqltxt As String
        Dim DT As DataTable
        Dim az As New AzureVM

        sqltxt = "Select * from ( " &
                 " select convert(varchar(10),t_date,103) + ' ' + convert(varchar(5),t_date,108) t_date2, t_ipad " &
                 " , t_type =  case when t_type = 1 and t_note = 'Factory' then 'login success' " &
                 "             when t_type = 1 and t_note = 'cookie login' then 'cookie' " &
                 "             when t_type = 2 then 'login fail' " &
                 "             when t_type = 3 then 'logout' " &
                 "             else '' end " &
                 " from facLogin " &
                 " where t_user = '" & Session("uname") & "' " &
                 " Union All " &
                 " select convert(varchar(10),t_date,103) + ' ' + convert(varchar(5),t_date,108) t_date2, t_ipad " &
                 " , t_note " &
                 " from facLogData " &
                 " where t_user = '" & Session("uname") & "' " &
                 " ) tx " &
                 " order by t_date2 desc "
        DT = az.GetDataTable(sqltxt)
        If DT.Rows.Count > 0 Then
            DV1 = New DataView(DT)
            grid1.DataSource = DV1
            Session("xlogin") = DV1
        Else
            grid1.DataSource = Nothing
        End If
        grid1.DataBind()

        DT.Dispose()
        DT = Nothing
        az = Nothing

    End Sub

    Private Sub grid1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grid1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.RowIndex Mod 2 = 0 Then
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#FFFFFF'")
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#ced8ee'")
            Else
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#FFFFFF'")
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#ced8ee'")
            End If
        End If
    End Sub

    Private Sub grid1_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles grid1.PageIndexChanging
        grid1.PageIndex = e.NewPageIndex
        grid1.DataSource = DV1
        grid1.DataBind()
    End Sub
End Class