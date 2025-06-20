Public Class MyMasterPage
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Admin") = "" Then
            Response.Redirect("Login.aspx")
        End If
        ''Session("Admin") = "Admin"
        ''Session("uname") = "itd"

        ''If Session("Admin") = "" Then
        ''    Dim strLogin As String() = IPNetworking.CookieLogin.Split(",")
        ''    If UBound(strLogin) = 2 Then
        ''        Dim encry As New codify
        ''        Session("Admin") = encry.Decrypt(strLogin(0))
        ''        Session("uname") = strLogin(1)
        ''    Else
        ''        Response.Redirect("Login.aspx")
        ''    End If
        ''End If

        If Not Page.IsPostBack Then
            Try
                ''menuLogin.Visible = False
                ''If Session("Admin") = "Admin" Then
                ''    menuLogin.Visible = True
                ''End If
                lblName.Text = Session("uname")
                Me.iGetData()

                'mRD.Visible = False
                mPUR.Visible = False
                'mMCD.Visible = False
                mSUPPLIER.Visible = False
                mProfile.Visible = False
                mLOG.Visible = False
                mCreate.Visible = False

                Select Case Session("Manu")
                    Case "Admin"
                        'mRD.Visible = True
                        mPUR.Visible = True
                        'mMCD.Visible = True
                        mSUPPLIER.Visible = True
                        mProfile.Visible = True
                        mLOG.Visible = True
                        mCreate.Visible = True
                    Case "ExecN"
                        'mRD.Visible = True
                        mPUR.Visible = True
                        'mMCD.Visible = True
                        mSUPPLIER.Visible = False
                        mProfile.Visible = True
                        mLOG.Visible = False
                        mCreate.Visible = False
                    Case "UserN"
                        'mRD.Visible = True
                        mPUR.Visible = True
                        'mMCD.Visible = True
                        mSUPPLIER.Visible = False
                        mProfile.Visible = True
                        mLOG.Visible = False
                        mCreate.Visible = False
                    Case "RD"
                        'mRD.Visible = True
                        mPUR.Visible = False
                        'mMCD.Visible = False
                        mSUPPLIER.Visible = False
                        mProfile.Visible = True
                        mLOG.Visible = False
                        mCreate.Visible = False
                    Case "PUR"
                        'mRD.Visible = False
                        mPUR.Visible = True
                        'mMCD.Visible = False
                        mSUPPLIER.Visible = False
                        mProfile.Visible = True
                        mLOG.Visible = False
                        mCreate.Visible = False
                    Case "MCD"
                        'mRD.Visible = False
                        mPUR.Visible = False
                        'mMCD.Visible = True
                        mSUPPLIER.Visible = False
                        mProfile.Visible = True
                        mLOG.Visible = False
                        mCreate.Visible = False
                    Case "supplier"
                        'mRD.Visible = False
                        mPUR.Visible = False
                        'mMCD.Visible = False
                        mSUPPLIER.Visible = True
                        mProfile.Visible = False
                        mLOG.Visible = False
                        mCreate.Visible = False
                End Select

            Catch ex As Exception
                Response.Write("> " & ex.Message)
            End Try
        Else

        End If
    End Sub

    Sub iGetData()
        Try
            Dim sqltxt As String
            Dim DT As DataTable
            Dim az As New AzureVM

            sqltxt = "select t_name,t_idno,t_level from facUser where t_user = '" & Session("uname") & "' "
            DT = az.GetDataTable(sqltxt)
            If DT.Rows.Count > 0 Then
                sqltxt = " Select * from facImageRC where t_idno = '" & DT.Rows(0).Item("t_idno") & "'"
                DT = az.GetDataTable(sqltxt)
                If DT.Rows.Count > 0 Then
                    Dim imageUrl As String = "data:image/jpg;base64," & Convert.ToBase64String(CType(DT.Rows(0).Item("t_image"), Byte()))
                    imgProfile.ImageUrl = imageUrl
                End If
            End If

            DT.Dispose()
            DT = Nothing
            az = Nothing
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

End Class