<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MyMasterPage.Master" CodeBehind="SetSupplierB1.aspx.vb" Inherits="SQA.SetSupplierB1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script>
        $(document).ready(function () {
            var table = $('#').DataTable({
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
                        target: [0, 1],
                        className: 'desktop'
                    },
                    {
                        target: [2],
                        className: 'none'
                    },
                    {
                        target: [0, 1],
                        className: 'mobile'
                    },
                    {
                        target: [0, 1],
                        className: 'tablet'
                    },
                    {
                        target: [0, 1, 2],
                        className: 'dt-head-center'
                    },
                    //{
                    //    target: [3, 4, 5],
                    //    className: 'dt-body-center'
                    //},
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

            $('#').DataTable({
                order: [[0, 'desc']],
                responsive: true,
                pageLength: 10,
                scrollToTop: true,
                autoWidth: false,
                searching: false,
                lengthChange: false,
                info: false,
                columnDefs: [
                    {
                        target: [0],
                    },
                    {
                        target: [0, 1],
                        className: 'desktop'
                    },
                    {
                        target: [0, 1],
                        className: 'mobile,tablet'
                    },
                    {
                        target: [0, 1],
                        className: 'dt-head-center'
                    },

                ],
                fixedHeader: {
                    header: true,
                    footer: true
                }
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


        <div class="container2">

            <img src="./images/aside4.svg" alt="" class="hero2" /><br />
            <div class="card mb-3">
                <div class="card-body">
                    <div class="d-flex justify-content-between align-items-center flex-wrap">
                        <h4 class="mb-2">Supplier Profile</h4>

                        <ul class="nav nav-pills">
                            <li class="nav-item">
                                <a class="nav-link" href="#page_PType">Product Type</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" href="#page_Customer">Customer</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" href="#page_Rupload">Related upload</a>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>

            <!-- ปุ่มแก้ไขอยู่ row แยกต่างหาก -->
            <div class="row mb-3">
                <div class="col text-end">
                    <asp:LinkButton runat="server" ID="lnkNew" class="btn btn-warning w-100" style="background =">
                        <i class="fa fa-pencil" aria-hidden="true"></i>แก้ไข
                    </asp:LinkButton>
                </div>
            </div>

            <div class="row">
                <div class="col-12 mb-1">
                    <%--Company Profile--%>
                    <div class="card-header" style="font-weight: 600;" runat="server" id="lbPosition">
                        <i class="fa fa-building-o" aria-hidden="true"></i> Company Profile
                          <i class="fa fa-flag" style="font-size: 20px;" aria-hidden="true" id="lbFlag" runat="server"></i>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-lg-6 col-sm-12">
                    <label class="sr-only" for="inlineFormInputGroup">sec1</label>
                    <div class="input-group mb-3">
                        <div class="input-group-prepend">
                            <div class="input-group-text">
                                <div style="width: 120px;">Ref NO.</div>
                            </div>
                        </div>
                        <input type="text" class="form-control" id="t_SQAM_SUBsName" runat="server" placeholder="" readonly />
                    </div>
                </div>

                <div class="col-lg-6 col-sm-12">
                    <label class="sr-only" for="inlineFormInputGroup">sec1</label>
                    <div class="input-group mb-3">
                        <div class="input-group-prepend">
                            <div class="input-group-text">
                                <div style="width: 120px;">Reported Date</div>
                            </div>
                        </div>
                        <input type="text" class="form-control" id="t_SQAQ_Date" runat="server" placeholder="" readonly />
                    </div>
                </div>

                <div class="col-lg-12 col-sm-12">
                    <label class="sr-only" for="inlineFormInputGroup">sec1</label>
                    <div class="input-group mb-3">
                        <div class="input-group-prepend">
                            <div class="input-group-text">
                                <div style="width: 120px;">Contract Name</div>
                            </div>
                        </div>
                        <input type="text" class="form-control" id="t_SQAQ_Contact" runat="server" placeholder="" readonly />
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
                        <input type="text" class="form-control" id="t_SQASu_Subject" runat="server" placeholder="" readonly />
                    </div>
                </div>

                <div class="col-lg-12 col-sm-12">
                    <label class="sr-only" for="inlineFormInputGroup">sec1</label>
                    <div class="input-group mb-3">
                        <div class="input-group-prepend">
                            <div class="input-group-text">
                                <div style="width: 120px;">Company name</div>
                            </div>
                        </div>
                        <input type="text" class="form-control" id="t_SQAM_SUBName" runat="server" placeholder="" readonly />
                    </div>
                </div>

            </div>

        </div>

        <div class="container2">

            <div class="row">
                <div class="col-12 mb-1">
                    <%--Product Type--%>
                    <div class="card-header" style="font-weight: 600;" runat="server" id="Div14">
                        <i class="fa fa-building-o" aria-hidden="true"></i> Product Type
                          <i class="fa fa-flag" style="font-size: 20px;" aria-hidden="true" id="I12" runat="server"></i>
                    </div>
                </div>
            </div>

            <div class="row">

                <div class="col-lg-6 col-sm-12">
                    <label class="sr-only" for="inlineFormInputGroup">Material Type</label>

                    <div class="input-group mb-3">
                        <div class="input-group-prepend">
                            <div class="input-group-text" style="width: 250px;">Material Type</div>
                        </div>
                        <input type="text" id="t_SQAM_TMaterials" name="t_SQAM_TMaterials" runat="server" class="form-control" readonly clientidmode="Static" />
                    </div>
                </div>

                <div class="col-lg-6 col-sm-12">
                    <label class="sr-only" for="inlineFormInputGroup">Specialized Material Type</label>
                    <div class="input-group mb-3">
                        <div class="input-group-prepend">
                            <div class="input-group-text" style="width: 250px;">Specialized Material Type</div>
                        </div>
                        <input type="text" id="t_SQAM_Type" name="t_SQAM_Type" runat="server" class="form-control" readonly clientidmode="Static" />
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-lg-12 col-sm-12">
                    <label class="sr-only" for="inlineFormInputGroup">Product Type</label>
                    <div class="input-group mb-3">
                        <div class="input-group-prepend">
                            <div class="input-group-text" style="width: 250px;">Product Type</div>
                        </div>
                        <input type="text" id="t_SQAM_ProductT" name="t_SQAM_ProductT" runat="server" class="form-control" readonly clientidmode="Static" />
                    </div>
                </div>
            </div>
        </div>



        <section id="page_ProblemOccurred" class="services">

            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <contenttemplate>

                    <div class="container2">

                        <div class="row">
                            <div class="col-12 mb-1">
                                <%--Product Type--%>
                                <div class="card-header" style="font-weight: 600;" runat="server" id="Div1">
                                    <i class="fa fa-building-o" aria-hidden="true"></i> Problem Occurred During
                                </div>

                            </div>
                        </div>

                        <div class="row">

                            <div class="col-lg-12 col-sm-12">

                                <div class="input-group mb-3">
                                    <div class="input-group-prepend">
                                        <div class="input-group-text" style="width: 250px;">Problem During</div>
                                    </div>
                                    <input type="text" id="t_SQAQ_FouPro" name="t_SQAQ_FouPro" runat="server" class="form-control" readonly clientidmode="Static" />
                                </div>
                            </div>
                        </div>
                    </div>

                </contenttemplate>
            </asp:UpdatePanel>

        </section>

        <section id="page_Customer" class="services">
            <div class="container2">

                <div class="row">
                    <div class="col-12 mb-1">
                        <%--Customer--%>
                        <div class="card-header" style="font-weight: 600;" runat="server" id="Div2">
                            <i class="fa fa-building-o" aria-hidden="true"></i> Severity level
                          <i class="fa fa-flag" style="font-size: 20px;" aria-hidden="true" id="I2" runat="server"></i>
                        </div>
                    </div>
                </div>

                <div class="row">

                    <div class="col-lg-12 col-sm-12">

                        <div class="input-group mb-3">
                            <div class="input-group-prepend">
                                <div class="input-group-text" style="width: 250px;">Severity level</div>
                            </div>
                            <input type="text" id="t_SQAQ_SevLev" name="t_SQAQ_SevLev" runat="server" class="form-control" readonly clientidmode="Static" />
                        </div>
                    </div>
                </div>

            </div>
        </section>

        <div class="container2">

            <div class="row">

                <div class="col-lg-12 col-sm-12">
                    <%--Employee amount--%>
                    <div class="card-header" style="font-weight: 600;" runat="server" id="Div5">
                        <i class="fa fa-building-o" aria-hidden="true"></i> The details of the problem are as follows by
                           <i class="fa fa-flag" style="font-size: 20px;" aria-hidden="true" id="I3" runat="server"></i>
                    </div>

                    <div class="input-group mb-3">
                        <div class="input-group-prepend">
                            <div class="input-group-text mt-1" style="width: 250px;">Problem Details</div>
                        </div>
                        <input type="text" id="t_SQAQ_DetPro" name="t_SQAQ_DetPro" runat="server" class="form-control mt-1" readonly clientidmode="Static" />
                    </div>

                    <div class="input-group mb-3">
                        <div class="input-group-prepend">
                            <div class="input-group-text" style="width: 250px;">Coil / Batch No.</div>
                        </div>
                        <input type="text" id="t_SQAQ_Coil" name="t_SQAQ_Coil" runat="server" class="form-control" readonly clientidmode="Static" />
                    </div>

                    <div class="input-group mb-3">
                        <div class="input-group-prepend">
                            <div class="input-group-text" style="width: 250px;">DO NO.</div>
                        </div>
                        <input type="text" id="t_SQAQ_DO" name="t_SQAQ_DO" runat="server" class="form-control" readonly clientidmode="Static" />
                    </div>

                    <div class="input-group mb-3">
                        <div class="input-group-prepend">
                            <div class="input-group-text" style="width: 250px;">Date of Admission</div>
                        </div>
                        <input type="text" id="t_SQAQ_InDate" name="t_SQAQ_InDate" runat="server" class="form-control" readonly clientidmode="Static" />
                    </div>

                    <div class="input-group mb-3">
                        <div class="input-group-prepend">
                            <div class="input-group-text" style="width: 250px;">Number of problems found</div>
                        </div>
                        <input type="text" id="t_SQAQ_NoPro" name="t_SQAQ_NoPro" runat="server" class="form-control" readonly clientidmode="Static" />
                    </div>

                    <div class="input-group mb-3">
                        <div class="input-group-prepend">
                            <div class="input-group-text" style="width: 250px;">Annotation</div>
                        </div>
                        <input type="text" id="t_SQAQ_Note" name="t_SQAQ_Note" runat="server" class="form-control" readonly clientidmode="Static" />
                    </div>

                </div>
            </div>
        </div>

        <div class="container2">

            <div class="row">

                <div class="col-lg-12 col-sm-12">
                    <%--Employee amount--%>
                    <div class="card-header" style="font-weight: 600;" runat="server" id="Div3">
                        <i class="fa fa-building-o" aria-hidden="true"></i> RCI initial troubleshooting 
                           <i class="fa fa-flag" style="font-size: 20px;" aria-hidden="true" id="I1" runat="server"></i>
                    </div>

                    <div class="input-group mb-3 mt-1">
                        <div class="input-group-prepend">
                            <div class="input-group-text " style="width: 250px;">Troubleshooting</div>
                        </div>
                        <input type="text" id="t_SQAS_Name" name="t_SQAS_Name" runat="server" class="form-control" readonly clientidmode="Static" />
                    </div>

                    <div class="input-group mb-3">
                        <div class="input-group-prepend">
                            <div class="input-group-text" style="width: 250px;">Details of Troubleshooting.</div>
                        </div>
                        <input type="text" id="t_SQAQ_RCISolProDet" name="t_SQAQ_RCISolProDet" runat="server" class="form-control" readonly clientidmode="Static" />
                    </div>

                </div>
            </div>
        </div>

        <div class="container2">

            <div class="row">

                <div class="col-lg-12 col-sm-12">

                    <%--Capacity--%>
                    <div class="card-header" style="font-weight: 600;" runat="server" id="Div7">
                        <i class="fa fa-building-o" aria-hidden="true"></i> Collect samples for supplier
                        <i class="fa fa-flag" style="font-size: 20px;" aria-hidden="true" id="I5" runat="server"></i>
                    </div>

                    <div class="input-group mb-3 mt-1">
                        <div class="input-group-prepend">
                            <div class="input-group-text" style="width: 250px;">Type</div>
                        </div>
                        <input type="text" id="t_SQAQ_ExpTyp" name="t_SQAQ_ExpTyp" runat="server" class="form-control" readonly clientidmode="Static" />
                    </div>

                    <div class="input-group mb-3">
                        <div class="input-group-prepend">
                            <div class="input-group-text" style="width: 250px;">Amount</div>
                        </div>
                        <input type="text" id="t_SQAQ_ExpTypCou" name="t_SQAQ_ExpTypCou" runat="server" class="form-control" readonly clientidmode="Static" />
                    </div>

                </div>

                <div class="col-lg-12 col-sm-12">

                    <%--Lead Time --%>
                    <div class="card-header" style="font-weight: 600;" runat="server" id="Div8">
                        <i class="fa fa-building-o" aria-hidden="true"></i> Summary of evaluation results
                        <i class="fa fa-flag" style="font-size: 20px;" aria-hidden="true" id="I6" runat="server"></i>
                    </div>

                    <label class="sr-only" for="inlineFormInputGroup">sec1</label>

                    <div class="input-group mb-3 mt-1">
                        <div class="input-group-prepend">
                            <div class="input-group-text" style="width: 250px;">Result</div>
                        </div>
                        <input type="text" id="t_SQAQ_Res" name="t_SQAQ_Res" runat="server" class="form-control" readonly clientidmode="Static" />
                    </div>


                </div>

                <div class="col-lg-12 col-sm-12">

                    <%--Minimum of Order (MOQ)--%>
                    <div class="card-header" style="font-weight: 600;" runat="server" id="Div9">
                        <i class="fa fa-building-o" aria-hidden="true"></i> การดำเนินการกับผลิตภัณฑ์ที่ไม่เป็นไปตามข้อกำหนด
                        <i class="fa fa-flag" style="font-size: 20px;" aria-hidden="true" id="I7" runat="server"></i>
                    </div>

                    <div class="input-group mb-3 mt-1">
                        <div class="input-group-prepend">
                            <div class="input-group-text" style="width: 250px;">Result</div>
                        </div>
                        <input type="text" id="t_SQAQ_ResDet" name="t_SQAQ_ResDet" runat="server" class="form-control" readonly clientidmode="Static" />
                    </div>

                    <div class="input-group mb-3">
                        <div class="input-group-prepend">
                            <div class="input-group-text" style="width: 250px;">Details (ถ้ามี)</div>
                        </div>
                        <input type="text" id="t_SQAQ_ResDetOth" name="t_SQAQ_ResDetOth" runat="server" class="form-control" readonly clientidmode="Static" />
                    </div>

                </div>

            </div>

            <div class="row">

                <div class="col-lg-12 col-sm-12">

                    <%--Name / Contact No. / Email--%>
                    <div class="card-header" style="font-weight: 600;" runat="server" id="Div10">
                        <i class="fa fa-building-o" aria-hidden="true"></i> Attachments
                        <i class="fa fa-flag" style="font-size: 20px;" aria-hidden="true" id="I8" runat="server"></i>
                    </div>

                    <div class="input-group mb-3 mt-1">
                        <div class="input-group-prepend">
                            <div class="input-group-text" style="width: 250px;">Type</div>
                        </div>
                        <input type="text" id="t_SQAA_Name" name="t_SQAA_Name" runat="server" class="form-control" readonly clientidmode="Static" />
                    </div>

                    <div class="input-group mb-3">
                        <div class="input-group-prepend">
                            <div class="input-group-text" style="width: 250px;">Details (ถ้ามี)</div>
                        </div>
                        <input type="text" id="t_SQAQ_AttOth" name="t_SQAQ_AttOth" runat="server" class="form-control" readonly clientidmode="Static" />
                    </div>

                </div>

                <div class="col-lg-12 col-sm-12">

                    <%--Minimum of Order (MOQ)--%>
                    <div class="card-header mt-3" runat="server" id="Div4" style="font-weight: normal;">
                        <p style="text-indent: 180px;">
                            ขอให้ทาง supplier ดำเนินการแก้ไขและตอบกลับมาภายใน 5 วัน โดยนับจากวันที่ RCI แจ้งปัญหา
                        </p>
                        <p>
                            จึงเรียนมาเพื่อทราบ ทางบริษัท Royale Can Industries CO,LTD ขอขอบคุณที่ท่านให้ความร่วมมือเป็นอย่างดีเสมอมาไว้ ณ ที่นี้
                        </p>
                        <div style="text-align: right; margin-top: 60px;">
                            ขอแสดงความนับถือ
                            <br />
                            <br />

                            __________________ 
                            <br />
                            <br />
                            <input type="text" class="form-control d-inline-block"
                                id="t_SQAQ_SIName" name="t_SQAQ_SIName"
                                runat="server"
                                style="width: 150px; text-align: center;" readonly />
                        </div>
                    </div>
                </div>

            </div>

        </div>
        <section id="page_Rupload" class="services">

        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>

        <div class="container2">

            <div class="row">
                <div class="col-12 mt-3">

                    <div class="card-header mb-1" style="font-weight: 600;" runat="server" id="Div6">
                        <i class="fa fa-building-o" aria-hidden="true"></i>Related Upload
                        <i class="fa fa-flag" style="font-size: 20px;" aria-hidden="true" id="I4" runat="server"></i>
                    </div>


                    <%-------GridView-------%>
                    <asp:GridView ID="example_RelUpload" CssClass="footable" runat="server" AutoGenerateColumns="false" DataKeyNames="SQAU_ID">
                        <Columns>
                            <%-- 0 --%>
                            <asp:BoundField DataField="No" HeaderText="No." />
                            <%-- 1 --%>
                            <asp:BoundField DataField="SQAU_Name" HeaderText="File Name" />
                            <%-- 2 --%>
                            <asp:BoundField DataField="SQAU_Link" HeaderText="Link" Visible="false" />
                            <%-- 3 --%>
                            <asp:TemplateField HeaderText="File">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="l2563" Text='<%# Eval("SQAU_Name") %>'></asp:Label>&nbsp;&nbsp;
                <asp:HyperLink runat="server" ID="hpl_Rel" NavigateUrl='<%# Eval("SQAU_Link") %>' Target="_blank">
                    <i class="fa fa-file-pdf-o" aria-hidden="true"></i>
                </asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>

        </div>

        </ContentTemplate>
        </asp:UpdatePanel>

        </section>

        <br />
        <br />
        <asp:LinkButton ID="btnReset" runat="server" CssClass="btn btn-warning btn-lg w-100">
      <i class="fa fa-arrow-left ms-2 mr-3" aria-hidden="true"></i>กลับหน้าหลัก 
        </asp:LinkButton>

        <%--<asp:Button class="btn btn-primary btn-lg" ID="" runat="server" Text=" บันทึก " />--%>
        <br />
        <br />

        
    </div>

</asp:Content>
