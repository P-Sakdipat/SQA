<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="SQA.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="robots" content="noindex"/>
    <meta name="googlebot" content="noindex"/>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8"/>
    <!-- Meta, title, CSS, favicons, etc. -->
    <meta charset="utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <title>Login</title>
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="font-awesome-4.7.0/css/font-awesome.min.css" rel="stylesheet" />
    <script src="jquery-3.2.1.js"></script>
    <script src="css/bootstrap.min.js"></script>
    <link href="https://fonts.googleapis.com/css?family=Work+Sans:400,600&display=swap" rel="stylesheet" />
	<link href="https://fonts.googleapis.com/css2?family=Kanit:wght@200&display=swap" rel="stylesheet"/>
    <link href="https://fonts.googleapis.com/css2?family=Kanit:wght@200&display=swap" rel="stylesheet"/>
    <style>
        body {
            background: #fff;
            font-family: 'Prompt','Kanit',Tahoma,'Lato', sans-serif;
            font-weight: 300;
            font-size: 16px;
            line-height: 22px;
            color: #000;
        }
            .img2 {
                width:100%;
                height:100%
            }

        .hero2 {
          position: absolute;
          top: 70px;
          left:70px;
          z-index: -9;
          margin-top: 10px;
          width: 85%;
        }

        .img2 {
            width:5%;
            height:5%
        }

        @media screen and (max-width: 1200px) {
            .img2 {
                width:8%;
                height:8%
            }
        }
        @media screen and (max-width: 770px) {
            .img2 {
                width:10%;
                height:10%
            }
        }
        @media screen and (max-width: 600px) {
            .hero2 img {
                /*display:none;*/
                position: absolute;
                top: -40px;
                left:88px;
                z-index: -9;
                margin-top: 10px;
                width: 40%;
            }

            .img2 {
                width:13%;
                height:13%
            }
        }
        @media screen and (max-width: 400px) {
            .img2 {
                width:15%;
                height:15%
            }
        }
    </style>

    <script src="https://www.google.com/recaptcha/api.js"></script>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off"><br />
        <asp:HiddenField ID="gRecaptchaResponse" runat="server" />
        <div class="container body"/>          
            <asp:Panel ID="Panel1" runat="server" defaultbutton="btnLogin">
            <div class="main_container">
                <div class="right_col" role="main">
                    <div class="row">
                        <div class="col-md-12 col-sm-12 col-xs-12">
                            <div class="text-left">  
                                
                                <%--<img src="./images/Picture1.png" style="width:13%;height:13%" class="img2" />--%>
                                <img src="./images/syncing.png" style="width:13%;height:13%" class="img2" />
                                <h2><b>Supplier Question Answer </b></h2>
                            <br />
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-md-3 col-sm-3 col-xs-12" for="first-name">User Name 
                                    </label>
                                    <div class="col-md-6 col-sm-6 col-xs-12">
                                        <input type="text" id="txtUser" runat="server" maxlength="12" class="form-control col-md-7 col-xs-12"/>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-md-3 col-sm-3 col-xs-12" for="last-name">Password 
                                    </label>
                                    <div class="col-md-6 col-sm-6 col-xs-12">
                                        <input type="password" id="txtPass" maxlength="15" runat="server" class="form-control col-md-7 col-xs-12"/>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-md-6 col-sm-6 col-xs-12 col-md-offset-3">						                
                                        <asp:Button ID="btnLogin" runat="server" Text="Login" Width="100" class="g-recaptcha btn btn-primary b1" data-sitekey="6LcfXqckAAAAAD1salnYZuQyl8jaZPxLDg7txTX_" data-callback='onSubmit' />
                                        <asp:Button ID="Button1" runat="server" style="display:none"/> 
                                    </div>
                                </div>
                               <asp:Label ID="Label1" runat="server" Text=""></asp:Label>                                         
                           </div>
                        </div>
                </div>
            </div>
            </asp:Panel> 
                       
            <div class="alert alert-primary" role="alert" id="showError" runat="server">
                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
                <h4 class="alert-heading">แจ้งเตือน </h4>
                <p>
                    <asp:Label ID="txtError" runat="server" Text=""></asp:Label>
                </p>
            </div>            
                      
    </form>
    <script>
        function onSubmit(token) {
            document.getElementById('gRecaptchaResponse').value = token;
            document.getElementById('Button1').click();
        }
    </script>
    <script>
        x1 = "<%=X2%>";
        if (x1 != "") {
            alert(x1);
        }
    </script>
</body>
</html>
