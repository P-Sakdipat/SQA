<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MyMasterPage.Master" CodeBehind="PageLog.aspx.vb" Inherits="SQA.PageLog" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <br />
        <div><h2>User Log </h2> 
        </div>
        <asp:GridView ID="grid1" runat="server" CellPadding="4" CssClass="x1" AutoGenerateColumns="False" BorderColor="#C1C4C8" BorderWidth="1px" BorderStyle="Solid" AllowPaging="True" PageSize="20" AllowSorting="False">
            <AlternatingRowStyle BorderColor="#404040" BorderWidth="1px" />
            <HeaderStyle BackColor="#CBE4FB" BorderColor="#404040" BorderWidth="1px" />
            <RowStyle BackColor="White" BorderColor="#404040" BorderWidth="1px" />
            <Columns>
                <asp:BoundField DataField="t_date2" HeaderText="วันที่-เวลา">
                    <HeaderStyle CssClass="text-center" BorderColor="#C1C4C8" BorderWidth="1"></HeaderStyle>
                    <ItemStyle BorderColor="#C1C4C8" BorderWidth="1"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="t_type" HeaderText="การใช้งาน">
                    <HeaderStyle CssClass="text-center" BorderColor="#C1C4C8" BorderWidth="1"></HeaderStyle>
                    <ItemStyle BorderColor="#C1C4C8" BorderWidth="1"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="t_ipad" HeaderText="IP Address">
                    <HeaderStyle CssClass="text-center" BorderColor="#C1C4C8" BorderWidth="1"></HeaderStyle>
                    <ItemStyle BorderColor="#C1C4C8" BorderWidth="1"></ItemStyle>
                </asp:BoundField>
            </Columns>
        </asp:GridView>
    </div>
    <script>
        x1 = "<%=X2%>";
        if (x1 != "") {
            alert(x1);
        }
    </script>
</asp:Content>
