<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MyMasterPage.Master" CodeBehind="SetSupplier.aspx.vb" Inherits="SQA.SetSupplier" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script>
        $(document).ready(function () {
            var table = $('#ContentPlaceHolder1_example').DataTable({
                order: [[0, 'desc']],
                responsive: true,
                scrollToTop: true,
                autoWidth: false,
                searching: false,
                columnDefs: [
                    {
                        target: [0],
                    },
                    {
                        target: [0, 1, 2, 3, 4, 23, 24, 25],
                        className: 'desktop'
                    },
                    {
                        target: [5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22],
                        className: 'none'
                    },
                    {
                        target: [0, 1, 2],
                        className: 'mobile'
                    },
                    {
                        target: [0, 1, 2, 3],
                        className: 'tablet'
                    },
                    {
                        target: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 23, 24, 25],
                        className: 'dt-head-center'
                    },
                    {
                        target: [23, 24, 25],
                        className: 'dt-body-center'
                    },
                    {
                        type: 'date-uk',
                        target: 0
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

    <style>
        table.dataTable thead tr {
            background-color: #dce9f9;
            background-image: linear-gradient(#ebf3fc, #dce9f9);
            background: #dce9f9 !important;
            position: sticky !important;
            top: 0;
            box-shadow: 0 2px 2px -1px rgba(0, 0, 0, 0.2);
            font-family: 'Kanit',Tahoma,'Lato', sans-serif;
            font-size: 17px;
        }

        table td {
            font-family: 'Kanit',Tahoma,'Lato', sans-serif;
            font-weight: 300;
            font-size: 16px;
        }

        body {
            background: #fff;
            font-family: 'Kanit',Tahoma,'Lato', sans-serif;
            font-weight: 300;
            font-size: 16px;
            line-height: 22px;
            color: #000;
        }

        .container2 {
            width: 100%;
            padding-right: 30px;
            padding-left: 30px;
            margin-right: auto;
            margin-left: auto;
        }

        .navbar-icon-top .navbar-nav .nav-link > .fa {
            position: relative;
            width: 36px;
        }

            .navbar-icon-top .navbar-nav .nav-link > .fa > .badge {
                font-size: 0.75rem;
                position: absolute;
                right: 0;
                font-family: sans-serif;
            }

        .navbar-icon-top .navbar-nav .nav-link > .fa {
            top: 3px;
            line-height: 12px;
        }

            .navbar-icon-top .navbar-nav .nav-link > .fa > .badge {
                top: -10px;
            }
    </style>

    <%--<link href="css/file_upload.css" rel="stylesheet" />--%>

    <link href="css/datepicker.css" rel="stylesheet" />
    <script src="css/bootstrap-datepicker.js"></script>

    <div id="form1" runat="server" autocomplete="off">

        <div class="container1 mb-3">
            <div class="bg-light border rounded px-4 py-3 d-flex flex-column flex-md-row align-items-start align-items-md-center justify-content-between mt-3">
                <!-- ส่วนหัวด้านซ้าย -->
                <div class="d-flex align-items-center mb-3 mb-md-0">
                    <img src="./images/aside4.svg" alt="" class="hero2 me-3" style="height: 40px;" />
                    <h4 class="m-0 fw-semibold">
                        <i class="fa-thin fa-money-bill-1-wave me-2" aria-hidden="true"></i>
                        Create Supplier
                    </h4>
                </div>

            </div>
            
            <div class="bg-white border rounded px-4 py-3 mt-2">
                <div class="row align-items-center">
                    <div class="col-lg-12">

                    </div>
                    <!-- Radio Buttons -->
                    <div class="col-md-5 mb-3 mb-md-0">
                        <asp:RadioButtonList ID="rBL_search" runat="server" RepeatDirection="Horizontal" CssClass="d-flex flex-wrap gap-8 fs-5">
                            <asp:ListItem>Ref No. &nbsp; &nbsp;  </asp:ListItem>
                            <asp:ListItem>Contact &nbsp; &nbsp;</asp:ListItem>
                            <asp:ListItem>Company Name &nbsp; &nbsp;</asp:ListItem>
                            <asp:ListItem>Topic</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>

                    <!-- ช่องค้นหา -->
                    <div class="col-lg-5 col-md-5 mb-3 mb-md-0">
                        <input type="text" class="form-control" id="txtSearch" runat="server" placeholder="พิมพ์เพื่อค้นหา..." />
                    </div>

                    <!-- ปุ่มค้นหา -->
                    <div class="col-md-2">
                        <asp:LinkButton ID="lnkFind" runat="server" CssClass="btn btn-secondary w-100">
                    <i class="fa fa-search" aria-hidden="true"></i> ค้นหา
                        </asp:LinkButton>
                    </div>

                </div>
            </div>

            <!-- ปุ่มเพิ่มใหม่ (อยู่นอก box) -->
            <asp:LinkButton runat="server" ID="lnkAddNew" CssClass="btn btn-success btn-sm w-100 mt-2">
    <i class="fa fa-plus-square me-1" style="font-size: 20px" aria-hidden="true"></i> เพิ่มใหม่
            </asp:LinkButton>

        </div>

        <%-------ค้นหา-ddCustomerH------%>
        <div class="container2">
            <%-------GridView-------%>
            <%-------GridView-------%>
            <asp:GridView ID="example" CssClass="footable" runat="server" AutoGenerateColumns="false" DataKeyNames="SQAQ_ID">

                <Columns>
                    <%--0--%>
                    <asp:BoundField DataField="SQAM_SUBsName" HeaderText="Ref NO."></asp:BoundField>
                    <%--1--%>
                    <asp:TemplateField HeaderText="Contact">
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblCustomer" runat="server"><%# Eval("SQAQ_Contact") %></asp:Label>
                            &nbsp;<asp:Image ID="imgNewcust" runat="server" ImageUrl="./images/new4.png" Visible="false" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--2--%>
                    <asp:BoundField DataField="SQAM_SUBName" HeaderText="Company name"></asp:BoundField>
                    <%--3--%>
                    <asp:BoundField DataField="SQASu_Subject" HeaderText="Topic"></asp:BoundField>
                    <%--4--%>
                    <asp:BoundField DataField="SQAQ_Date" HeaderText="วันที่แจ้งปัญหา"
                        DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" />
                    <%--5--%>
                    <asp:BoundField DataField="SQAM_TMaterials" HeaderText="ประเภทของวัตถุดิบ"></asp:BoundField>
                    <%--6--%>
                    <asp:BoundField DataField="SQAM_ProductT" HeaderText="Product type"></asp:BoundField>
                    <%--7--%>
                    <asp:BoundField DataField="SQAQ_FouPro" HeaderText="พบปัญหาในระหว่าง"></asp:BoundField>
                    <%--8--%>
                    <asp:BoundField DataField="SQAQ_SevLev" HeaderText="ระดับความรุนแรง"></asp:BoundField>
                    <%--9--%>
                    <asp:BoundField DataField="SQAQ_DO" HeaderText="DO NO."></asp:BoundField>
                    <%--10--%>
                    <asp:BoundField DataField="SQAQ_InDate" HeaderText="วันที่รับเข้า"></asp:BoundField>
                    <%--11--%>
                    <asp:BoundField DataField="SQAQ_NoPro" HeaderText="จำนวนที่พบปัญหา"></asp:BoundField>
                    <%--12--%>
                    <asp:BoundField DataField="SQAQ_Note" HeaderText="หมายเหตุอื่นๆ"></asp:BoundField>
                    <%--13--%>
                    <asp:BoundField DataField="SQAS_Name" HeaderText="RCI แก้ไขปัญหาเบื้องต้น"></asp:BoundField>
                    <%--14--%>
                    <asp:BoundField DataField="SQAQ_RCISolProDet" HeaderText="อธิบายการแก้ไขปัญหาเบื้องต้น"></asp:BoundField>
                    <%--15--%>
                    <asp:BoundField DataField="SQAQ_ExpTyp" HeaderText="เก็บตัวอย่างให้ Supplier : ลักษณะแบบ"></asp:BoundField>
                    <%--16--%>
                    <asp:BoundField DataField="SQAQ_ExpTypCou" HeaderText="เก็บตัวอย่างให้ Supplier : จำนวน"></asp:BoundField>
                    <%--17--%>
                    <asp:BoundField DataField="SQAQ_Res" HeaderText="ผลการประเมิน"></asp:BoundField>
                    <%--18--%>
                    <asp:BoundField DataField="SQAQ_ResDet" HeaderText="การดำเนินการกับผลิตภัณฑ์ที่ไม่เป็นไปตามข้อกำหนด"></asp:BoundField>
                    <%--19--%>
                    <asp:BoundField DataField="SQAQ_ResDetOth" HeaderText="การดำเนินการกับผลิตภัณฑ์ที่ไม่เป็นไปตามข้อกำหนด (อื่นๆ)"></asp:BoundField>
                    <%--20--%>
                    <asp:BoundField DataField="SQAA_Name" HeaderText="สิ่งที่แนบ"></asp:BoundField>
                    <%--21--%>
                    <asp:BoundField DataField="SQAQ_AttOth" HeaderText="สิ่งที่แนบ (อื่นๆ)"></asp:BoundField>
                    <%--22--%>
                    <asp:TemplateField HeaderText="" ItemStyle-Width="0">
                        <%-- <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:Image ID="headChk" runat="server" ImageUrl="./images/check.gif" Visible="false" />
                        </ItemTemplate>--%>
                    </asp:TemplateField>
                    <%--23--%>
                    <asp:TemplateField HeaderText="Edit" ItemStyle-Width="60">
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:LinkButton runat="server" ID="lbtEdit" CommandName="iEdit" CommandArgument='<%# Eval("SQAQ_ID")%>'><i class="fa fa-pencil fa-lg" aria-hidden="true"></i></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--24--%>
                    <asp:TemplateField HeaderText="Delete" ItemStyle-Width="60">
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:LinkButton runat="server" ID="lbtDelete" CommandName="iDelete" CommandArgument='<%# Eval("SQAQ_ID")%>'><i class="fa fa-trash fa-lg" aria-hidden="true"></i></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--25--%>
                    <asp:TemplateField HeaderText="View" ItemStyle-Width="60">
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:LinkButton runat="server" ID="lbtView" CommandName="iView" CommandArgument='<%# Eval("SQAQ_ID")%>'><i class="fa fa-eye fa-lg" aria-hidden="true"></i></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

            <%-------หน้าเพิ่ม-แก้ไข(modalId)-ddCustomer-----%>
            <br />
            <br />
            <div class="modal fade" tabindex="-1" role="dialog" id="modalId" data-backdrop="static" aria-labelledby="staticBackdropLabel">
                <div class="modal-dialog modal-lg" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Create Supplier Data</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">

                            <div class="row">
                                <div class="col-lg-7 col-sm-12">
                                    <label class="sr-only" for="inlineFormInputGroup">sec1</label>
                                    <div class="input-group mb-3">
                                        <div class="input-group-prepend">
                                            <div class="input-group-text">
                                                <div style="width: 120px;">RefNO.</div>
                                            </div>
                                        </div>
                                        <input type="text" class="form-control" id="t_RefNO" runat="server" readonly />
                                    </div>
                                </div>

                                <div class="col-lg-5 col-sm-12">
                                    <label class="sr-only" for="txtSQAQ_Date">วันที่</label>
                                    <div class="input-group mb-3">
                                        <div class="input-group-prepend">
                                            <div class="input-group-text">
                                                <div style="width: 120px;">Reported Date</div>
                                            </div>
                                        </div>
                                        <asp:TextBox
                                            ID="t_SQAQ_Date"
                                            runat="server"
                                            CssClass="form-control"
                                            TextMode="Date"
                                            placeholder="เลือกวันที่" />
                                    </div>
                                </div>

                                <div class="col-lg-12 col-sm-12">
                                    <label class="sr-only" for="inlineFormInputGroup">sec1</label>
                                    <div class="input-group mb-3">
                                        <div class="input-group-prepend">
                                            <div class="input-group-text">
                                                <div style="width: 120px;">To.....</div>
                                            </div>
                                        </div>
                                        <input type="text" class="form-control" id="t_SQAQ_Contact" runat="server" placeholder="" />
                                    </div>
                                </div>

                                <div class="col-lg-12 col-sm-12">
                                    <label class="sr-only" for="inlineFormInputGroup">sec1</label>
                                    <div class="input-group mb-3">
                                        <div class="input-group-prepend">
                                            <div class="input-group-text">
                                                <div style="width: 120px;">Topic</div>
                                            </div>
                                        </div>
                                        <asp:DropDownList ID="dd_SQASu_Subject" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-lg-7 col-sm-12">
                                    <label class="sr-only" for="inlineFormInputGroup">sec1</label>
                                    <div class="input-group mb-3">
                                        <div class="input-group-prepend">
                                            <div class="input-group-text">
                                                <div style="width: 120px;">Company Name</div>
                                            </div>
                                        </div>
                                        <asp:DropDownList ID="dd_SQAM_SUBName" runat="server" CssClass="form-control" AutoPostBack="false" onchange="updateSubName()"></asp:DropDownList>
                                    </div>

                                </div>

                                <div class="col-lg-5 col-sm-12">
                                    <label class="sr-only" for="inlineFormInputGroup">sec1</label>
                                    <div class="input-group mb-3">
                                        <div class="input-group-prepend">
                                            <div class="input-group-text">
                                                <div style="width: 120px;">Company Code</div>
                                            </div>
                                        </div>
                                        <input type="text" class="form-control" id="t_SQAM_SUBsName" runat="server" placeholder="" readonly />
                                    </div>
                                </div>


                                <%--                                <div class="col-lg-7 col-sm-12">
                                    <label class="sr-only" for="inlineFormInputGroup">sec1</label>
                                    <div class="input-group mb-3">
                                        <div class="input-group-prepend">
                                            <div class="input-group-text">
                                                <div style="width: 120px;">Product Type</div>
                                            </div>
                                        </div>
                                        <asp:DropDownList ID="dd_SQAM_ProductT" runat="server" CssClass="form-control" AutoPostBack="false" onchange="updateMatName()"></asp:DropDownList>
                                    </div>
                                </div>--%>

                                <%--                                <div class="col-lg-5 col-sm-12">
                                    <label class="sr-only" for="inlineFormInputGroup">sec1</label>
                                    <div class="input-group mb-3">
                                        <div class="input-group-prepend">
                                            <div class="input-group-text">
                                                <div style="width: 120px;">Materail Type</div>
                                            </div>
                                        </div>
                                        <input type="text" class="form-control" id="t_SQAM_TMaterials" runat="server" placeholder="" readonly />
                                    </div>
                                </div>--%>


                                <%--CheckBox พบปัญหาระหว่าง
                                <div class="col-lg-12 col-sm-12">
                                    <div class="input-group mb-3" style="width: 100%;">
                                        <div class="input-group-prepend mb-1">
                                            <div class="input-group-text" style="min-width: 150px;">Problem Occurred During</div>
                                        </div>
                                        <div class="form-control" style="width: 100%; background-color: #fff;">

                                            <!-- Checkbox Row  จัดแบบกระจายเท่า ๆ กัน -->
                                            <div class="d-flex justify-content-between align-items-center">

                                                <div class="form-check mr-4">
                                                    <asp:CheckBox ID="chk_Inspection" runat="server" CssClass="form-check-input" />
                                                    <label class="form-check-label" for="chk_Inspection">During Incoming Inspection</label>
                                                </div>

                                                <div class="form-check mr-4">
                                                    <asp:CheckBox ID="chk_Production" runat="server" CssClass="form-check-input" />
                                                    <label class="form-check-label" for="chk_Production">During Production Line Usage</label>
                                                </div>

                                                <div class="form-check">
                                                    <asp:CheckBox ID="chk_Customer" runat="server" CssClass="form-check-input" />
                                                    <label class="form-check-label" for="chk_Customer">Customer Complaint</label>
                                                </div>

                                            </div>

                                        </div>
                                    </div>
                                </div>--%>

                                <%--                                CheckBox ระดับความรุนแรง
                                <div class="col-lg-12 col-sm-12">
                                    <div class="input-group mb-3" style="width: 100%;">
                                        <div class="input-group-prepend mb-1">
                                            <div class="input-group-text" style="min-width: 150px;">Severity level</div>
                                        </div>
                                        <div class="form-control" style="width: 100%; background-color: #fff;">

                                            <!-- Checkbox Row  จัดแบบกระจายเท่า ๆ กัน -->
                                            <div class="d-flex justify-content-between align-items-center">

                                                <div class="form-check mr-4">
                                                    <asp:CheckBox ID="CheckBox1" runat="server" CssClass="form-check-input" />
                                                    <label class="form-check-label" for="chk_Inspection">Critical Defect</label>
                                                </div>

                                                <div class="form-check mr-4">
                                                    <asp:CheckBox ID="CheckBox2" runat="server" CssClass="form-check-input" />
                                                    <label class="form-check-label" for="chk_Production">Major Defect</label>
                                                </div>

                                                <div class="form-check">
                                                    <asp:CheckBox ID="CheckBox3" runat="server" CssClass="form-check-input" />
                                                    <label class="form-check-label" for="chk_Customer">Minor Defect</label>
                                                </div>

                                            </div>

                                        </div>
                                    </div>
                                </div>--%>



                                <%--<div class="col-lg-4 col-sm-12">
                                    <label class="sr-only" for="inlineFormInputGroup">sec1</label>
                                    <div class="input-group mb-3">                                       
                                        <asp:RadioButtonList ID="rBL_ProdType" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem>Factory &nbsp; &nbsp; </asp:ListItem>
                                            <asp:ListItem>Trading</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>--%>

                                <%--<div class="col-12">
                                    <label class="sr-only" for="inlineFormInputGroup">sec1</label>
                                    <div class="input-group mb-3">
                                        <div class="input-group-prepend">
                                            <div class="input-group-text">
                                                <div style="width:120px;">Address</div>
                                            </div>
                                        </div>
                                        <input type="text" class="form-control" id="t_ComPro_Add" runat="server" placeholder=""/>
                                    </div>
                                </div>--%>
                                <%-- <div class="col-lg-6 col-sm-12">
                                    <label class="sr-only" for="inlineFormInputGroup">sec1</label>
                                    <div class="input-group mb-3">
                                        <div class="input-group-prepend">
                                            <div class="input-group-text">
                                                <div style="width:120px;">City</div>
                                            </div>
                                        </div>
                                        <input type="text" class="form-control" id="t_ComPro_City" runat="server" placeholder=""/>
                                    </div>
                                </div>--%>
                                <%--<div class="col-lg-6 col-sm-12">
                                    <label class="sr-only" for="inlineFormInputGroup">sec1</label>
                                    <div class="input-group mb-3">
                                        <div class="input-group-prepend">
                                            <div class="input-group-text">
                                                <div style="width:120px;">Province</div>
                                            </div>
                                        </div>
                                        <input type="text" class="form-control" id="t_ComPro_Provi" runat="server" placeholder=""/>
                                    </div>
                                </div>--%>



                                <%--<div class="col-lg-4 col-sm-12">
                                    <label class="sr-only" for="inlineFormInputGroup">sec1</label>
                                    <div class="input-group mb-3">
                                        <div class="input-group-prepend">
                                            <div class="input-group-text">
                                                <div style="width:120px;">Post Code</div>
                                            </div>
                                        </div>
                                        <input type="text" class="form-control" id="t_ComPro_PCode" runat="server" placeholder=""/>
                                    </div>
                                </div>--%>
                                <%--<div class="col-12">
                                    <label class="sr-only" for="inlineFormInputGroup">sec1</label>
                                    <div class="input-group mb-3">
                                        <div class="input-group-prepend">
                                            <div class="input-group-text">
                                                <div style="width:120px;">Call</div>
                                            </div>
                                        </div>
                                        <input type="text" class="form-control" id="t_ComPro_Call" runat="server" placeholder=""/>
                                    </div>
                                </div>--%>
                                <%--<div class="col-12">
                                    <label class="sr-only" for="inlineFormInputGroup">sec1</label>
                                    <div class="input-group mb-3">
                                        <div class="input-group-prepend">
                                            <div class="input-group-text">
                                                <div style="width:120px;">E-Mail</div>
                                            </div>
                                        </div>
                                        <input type="text" class="form-control" id="t_ComPro_EMail" runat="server" placeholder=""/>
                                    </div>
                                </div>--%>
                                <%--<div class="col-12">
                                    <label class="sr-only" for="inlineFormInputGroup">sec1</label>
                                    <div class="input-group mb-3">
                                        <div class="input-group-prepend">
                                            <div class="input-group-text">
                                                <div style="width:120px;">Contact Person</div>
                                            </div>
                                        </div>
                                        <input type="text" class="form-control" id="t_ComPro_ConPer" runat="server" placeholder=""/>
                                    </div>
                                </div>--%>
                                <%--<div class="col-12">
                                    <label class="sr-only" for="inlineFormInputGroup">sec1</label>
                                    <div class="input-group mb-3">
                                        <div class="input-group-prepend">
                                            <div class="input-group-text">
                                                <div style="width:120px;">Other</div>
                                            </div>
                                        </div>
                                        <input type="text" class="form-control" id="t_ComPro_Other" runat="server" placeholder=""/>
                                    </div>
                                </div>--%>

                                <%--<button type="button" id="_ISCComProfile" runat="server" class="btn btn-primary" data-dismiss="modal">บันทึกข้อมูล</button>--%>
                            </div>

                        </div>
                        <div class="modal-footer">
                            <button type="button" id="_ISCComProfile" runat="server" class="btn btn-primary" data-dismiss="modal">บันทึกข้อมูล</button>
                            <asp:Label ID="txtidno" runat="server" Text="" Visible="false"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>

            <%-------หน้า Upload-----%>
            <div class="modal fade" tabindex="-1" role="dialog" id="modalUpload" data-backdrop="static" aria-labelledby="staticBackdropLabel">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Upload ใบเสนอราคา</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">

                            <div class="container center" style="background-color: #1e2832;">
                                <div class="row">
                                    <div class="col-md-12">
                                        <h1 class="white">File Upload</h1>
                                        <p class="white">อนุญาตเฉพาะไฟล์ .pdf ขนาดไม่เกิน 5MB.</p>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-10" style="text-align: center; margin: auto">
                                        <div class="btn-container">
                                            <!--the three icons: default, ok file (img), error file (not an img)-->
                                            <h1 class="imgupload"><i class="fa fa-file-pdf-o"></i></h1>
                                            <h1 class="imgupload ok"><i class="fa fa-check"></i></h1>
                                            <h1 class="imgupload stop"><i class="fa fa-times"></i></h1>
                                            <!--this field changes dinamically displaying the filename we are trying to upload-->
                                            <p id="namefile">Only .pdf file allowed!</p>
                                            <!--our custom btn which which stays under the actual one-->
                                            <button type="button" id="btnup" class="btn btn-primary btn-lg">Browse for your file</button>
                                            <!--this is the actual file input, is set with opacity=0 beacause we wanna see our custom one-->
                                            <input type="file" name="fileup" accept="application/pdf" id="fileup" runat="server" />
                                        </div>
                                    </div>
                                </div>
                                <!--additional fields-->
                                <div class="row">
                                    <div class="col-md-12">
                                        <!--the defauld disabled btn and the actual one shown only if the three fields are valid-->
                                        <input type="submit" value="Upload File" class="btn btn-primary" id="submitbtn" runat="server" />
                                        <button type="button" class="btn btn-default" disabled="disabled" id="fakebtn">Submit! <i class="fa fa-minus-circle"></i></button>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>

        </div>

    </div>

    <script>
        document.addEventListener("keyup", function (e) {
            var keyCode = e.keyCode ? e.keyCode : e.which;
            if (keyCode == 44) {
                stopPrntScr();
            }
        });

        function stopPrntScr() {
            var inpFld = document.createElement("input");
            inpFld.setAttribute("value", ".");
            inpFld.setAttribute("width", "0");
            inpFld.style.height = "0px";
            inpFld.style.width = "0px";
            inpFld.style.border = "0px";
            document.body.appendChild(inpFld);
            inpFld.select();
            document.execCommand("copy");
            inpFld.remove(inpFld);
        }

        $(document).ready(function () {

            var readURL = function (input) {
                if (input.files && input.files[0]) {
                    var reader = new FileReader();

                    reader.onload = function (e) {
                        $('.profile-pic').attr('src', e.target.result);
                    }

                    reader.readAsDataURL(input.files[0]);
                }
            }

            $(".file-upload").on('change', function () {
                readURL(this);
            });

            $(".upload-button").on('click', function () {
                $(".file-upload").click();
            });
        });


        $('#fileup').change(function () {
            //here we take the file extension and set an array of valid extensions
            var res = $('#fileup').val();
            var arr = res.split("\\");
            var filename = arr.slice(-1)[0];
            filextension = filename.split(".");
            filext = "." + filextension.slice(-1)[0];
            valid = [".pdf"];
            //if file is not valid we show the error icon, the red alert, and hide the submit button
            if (valid.indexOf(filext.toLowerCase()) == -1) {
                $(".imgupload").hide("slow");
                $(".imgupload.ok").hide("slow");
                $(".imgupload.stop").show("slow");

                $('#namefile').css({ "color": "red", "font-weight": 700 });
                $('#namefile').html("File " + filename + " is not PDF !");

                $("#submitbtn").hide();
                $("#fakebtn").show();
            } else {
                //if file is valid we show the green alert and show the valid submit
                $(".imgupload").hide("slow");
                $(".imgupload.stop").hide("slow");
                $(".imgupload.ok").show("slow");

                $('#namefile').css({ "color": "green", "font-weight": 700 });
                $('#namefile').html(filename);

                $("#submitbtn").show();
                $("#fakebtn").hide();
            }
        });
    </script>

    <script>
        $('#tvdate').datepicker({
            format: 'dd/mm/yyyy',
            autoclose: true,
        });
    </script>

    <script>
        x1 = "<%=X2%>";
        if (x1 != "") {
            alert(x1);
        }
    </script>

    <script type="text/javascript">
        function updateSubName() {
            var ddl = document.getElementById('<%= dd_SQAM_SUBName.ClientID %>');
            var txt = document.getElementById('<%= t_SQAM_SUBsName.ClientID %>');
            txt.value = ddl.value; // ใส่ value (SQAM_SUBsName) ลงใน input
        }
    </script>

    <%--    <script type="text/javascript">
        function updateMatName() {
            var ddl = document.getElementById('<%= dd_SQAM_ProductT.ClientID %>');
                var txt = document.getElementById('<%= t_SQAM_TMaterials.ClientID %>');
            txt.value = ddl.value; // ใส่ value (SQAM_SUBsName) ลงใน input
        }
    </script>--%>
</asp:Content>
