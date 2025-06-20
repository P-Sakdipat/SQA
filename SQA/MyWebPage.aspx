<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MyMasterPage.Master" CodeBehind="MyWebPage.aspx.vb" Inherits="SQA.MyWebPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        @media screen and (max-width: 600px) {
            .hero2 img {
                width: 80%;
            }
        }       
    </style>
    <div class="container text-center">
        <br />
        <div class="hero2">
            <%--<img src="./images/rc-logo3.png" />--%>
            <%--<img src="./images/Picture1.png" style="width:50%;height:50%" />--%>
        </div>        
    </div>
</asp:Content>
