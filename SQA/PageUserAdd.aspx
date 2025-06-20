<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MyMasterPage.Master" CodeBehind="PageUserAdd.aspx.vb" Inherits="SQA.PageUserAdd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="DataTables-1.13.1/css/jquery.dataTables.min.css" rel="stylesheet" />
    <script src="DataTables-1.13.1/js/jquery.dataTables.min.js"></script>
    <link href="DataTables-1.13.1/css/responsive.bootstrap.min.css" rel="stylesheet" />
    <script src="DataTables-1.13.1/js/dataTables.responsive.min.js"></script>
    <link href="DataTables-1.13.1/css/footable.min.css" rel="stylesheet" />
    
    <style>
        .ChkBoxClass input {top: .8rem;
         scale: 2.0;
         margin-right: 0.7rem;}

        .x1 {
            text-align:center;

        }
    </style>
        <div class="container">
             <div><h2>User Management &nbsp;</h2> 
             </div>
        </div>
        <div class="container">
            <img src="./images/aside4.svg" alt="" class="hero2" />
            <div class="row">
                <div class="col-lg-4 col-sm-6">
                    <label class="sr-only" for="inlineFormInputGroup">sec1</label>
                    <div class="input-group mb-3">
                        <div class="input-group-prepend">
                            <div class="input-group-text">
                                <div style="width:100px;">User Name</div>
                            </div>
                        </div>
                        <input type="text" class="form-control" id="tuser" runat="server" maxlength="15" placeholder="ชื่อผู้ใช้"/>&nbsp;
                    </div>
                </div>
                <div class="col-lg-4 col-sm-6">
                    <label class="sr-only" for="inlineFormInputGroup">sec1</label>
                    <div class="input-group mb-3">
                        <div class="input-group-prepend">
                            <div class="input-group-text">
                                <div style="width:100px;">Password</div>
                            </div>
                        </div>
                        <input type="password" class="form-control" id="tpassword" runat="server" placeholder="รหัสผ่าน" maxlength="15"/>&nbsp;
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-8 col-sm-12">
                    <label class="sr-only" for="inlineFormInputGroup">sec1</label>
                    <div class="input-group mb-3">
                        <div class="input-group-prepend">
                            <div class="input-group-text">
                                <div style="width:100px;">Full Name</div>
                            </div>
                        </div>
                        <input type="text" class="form-control" id="tname" runat="server" placeholder="ชื่อ-นามสกุล" maxlength="50"/>&nbsp;
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-4 col-sm-6">
                    <label class="sr-only" for="inlineFormInputGroup">sec1</label>
                    <div class="input-group mb-3">
                        <div class="input-group-prepend">
                            <div class="input-group-text">
                                <div style="width:100px;">User Type</div>
                            </div>
                        </div>
                        <asp:DropDownList ID="ddLevel" runat="server" CssClass="form-control">
                            <asp:ListItem Value="0">เลือก</asp:ListItem>
                            <asp:ListItem Value="1">Admin</asp:ListItem>
                            <asp:ListItem Value="4">Executive</asp:ListItem>
                        </asp:DropDownList>                         
                    </div>
                </div>
                <div class="col-lg-4 col-sm-6">
                    <label class="sr-only" for="inlineFormInputGroup">sec1</label>
                    <div class="input-group my-2">
                        <p style="font-weight:600;font-size:16px;">&nbsp;&nbsp;ยกเลิก &nbsp;&nbsp;<asp:CheckBox ID="chkCancel" runat="server" CssClass="ChkBoxClass"/></p>
                    </div>
                </div>
            </div>

            <div class="form-row">
                <div class="col-auto">
                    <asp:Button ID="btnCancel" class="btn btn-warning mb-2" runat="server" Text=" ยกเลิก "/>
                    <asp:Button ID="btnHSave" class="btn btn-primary mb-2" runat="server" Text=" บันทึก "/>
                    <asp:Label ID="tidno" runat="server" Text="ADD" Visible="False"></asp:Label>
                </div>
            </div><br />

            <asp:GridView ID="GridView1" CssClass="footable" runat="server" AutoGenerateColumns="False">
                <Columns>                    
                    <asp:BoundField DataField="t_user" HeaderText="User Name"/>
                    <asp:BoundField DataField="t_name" HeaderText="Full Name"/>
                    <asp:BoundField DataField="t_type" HeaderText="User Type"/>
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblStatus"><i class="fa fa-toggle-on" aria-hidden="true"></i></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Password">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblView" ToolTip='<%# Eval("t_pass")%>'><i class="fa fa-eye-slash" aria-hidden="true"></i></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Edit">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" ID ="lbtEdit" CommandName="iEdit" CommandArgument='<%# Eval("t_idno")%>' CssClass="x1"><i class="fa fa-pencil fa-lg" aria-hidden="true"></i></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Delete">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" ID ="lbtDelete" CommandName="iDelete" CommandArgument='<%# Eval("t_idno")%>'><i class="fa fa-trash fa-lg" aria-hidden="true"></i></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView> 
            <br /><br />
        </div>
    <script>
        x1 = "<%=X2%>";
        if (x1 != "") {
            alert(x1);
        }
	</script>
    <script>
        $(document).ready(function () {
            var table = $('#GridView1').DataTable({
                order: [[0, 'asc']],
                responsive: true,
                pageLength: 100,
                scrollToTop: true,
                autoWidth: false,
                searching: false,
                paging: false,
                info: false,
                columnDefs: [
                    {
                        target: [0],
                    },
                    {
                        target: [0, 1, 2, 3, 4,5,6 ],
                        className: 'desktop'
                    },
                    {
                        target: [0, 1],
                        className: 'mobile'
                    },
                    {
                        target: [0, 1, 2],
                        className: 'tablet'
                    },
                    {
                        target: [0, 1, 2, 3, 4, ],
                        className: 'dt-head-center'
                    },
                    {
                        target: [5,6],
                        className: 'dt-body-center'
                    },
                ],
                fixedHeader: {
                    header: true,
                    footer: true
                }
            });

            table.on('page.dt', function () {
                $('html, body').animate({
                    scrollTop: $(".dataTables_wrapper").offset().top
                }, 'slow');
            });

        });
    </script>
</asp:Content>
