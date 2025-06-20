<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MyMasterPage.Master" CodeBehind="SetSupplierE.aspx.vb" Inherits="SQA.SetSupplierE" %>

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
                        target: [0, 1, 3],
                        className: 'desktop'
                    },
                    {
                        target: [2],
                        className: 'none'
                    },
                    {
                        target: [0, 1, 3],
                        className: 'mobile'
                    },
                    {
                        target: [0, 1, 3],
                        className: 'tablet'
                    },
                    {
                        target: [0, 1, 2, 3],
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

    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.9.0/css/bootstrap-datepicker.min.css" />

    <%--<link href="css/file_upload.css" rel="stylesheet" />--%>

    <link href="css/datepicker.css" rel="stylesheet" />
    <script src="css/bootstrap-datepicker.js"></script>

    <div id="form1" runat="server" autocomplete="off">


        <div class="container2">

            <img src="./images/aside4.svg" alt="" class="hero2" /><br />
            <div class="card mb-3">
                <div class="card-body">
                    <div class="d-flex justify-content-between align-items-center flex-wrap">
                        <h4 class="mb-2">Edit Supplier Profile</h4>

                        <ul class="nav nav-pills">
                            <li class="nav-item">
                                <a class="nav-link" href="#page_ProblemOccurred">Problem Occurred</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" href="#page_Summary">Summary</a>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>


            <div class="row mb-1">
                <div class="col-12">
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

            <div class="row mb-1">
                <div class="col-12">
                    <%--Product Type--%>
                    <div class="card-header" style="font-weight: 600;" runat="server" id="Div14">
                        <i class="fa fa-building-o" aria-hidden="true"></i>Product Type
                          <i class="fa fa-flag" style="font-size: 20px;" aria-hidden="true" id="I12" runat="server"></i>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-lg-4 col-sm-12">
                    <label class="sr-only" for="inlineFormInputGroup">Material Type</label>
                    <div class="input-group mb-3">
                        <div class="input-group-prepend">
                            <div class="input-group-text" style="width: 120px;">Material Type</div>
                        </div>
                        <input type="text" id="t_SQAM_TMaterials" name="t_SQAM_TMaterials" runat="server" class="form-control" readonly clientidmode="Static" />
                    </div>
                </div>

                <div class="col-lg-8 col-sm-12">
                    <label class="sr-only" for="inlineFormInputGroup">Specialized Material Type</label>
                    <div class="input-group mb-3">
                        <div class="input-group-prepend">
                            <div class="input-group-text" style="width: 200px;">Specialized Material Type</div>
                        </div>
                        <input type="text" id="t_SQAM_Type" name="t_SQAM_Type" runat="server" class="form-control" readonly clientidmode="Static" />
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12 col-sm-12">
                    <label class="sr-only" for="inlineFormInputGroup"> Product Type</label>
                    <div class="input-group mb-3">
                        <div class="input-group-prepend">
                            <div class="input-group-text" style="width: 120px;"> Product Type</div>
                        </div>
                        <asp:DropDownList ID="dd_SQAM_ProductT" runat="server" CssClass="form-control"
                            onchange="updateMatName()" ClientIDMode="Static" />
                    </div>
                </div>
            </div>
        </div>


        <section id="page_ProblemOccurred" class="services">

            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>

                    <div class="container2">

                        <div class="row">

                            <div class="col-lg-12 col-sm-12">
                                <%--Problem Occurred During--%>
                                <div class="card-header" style="font-weight: 600;" runat="server" id="Div1"><i class="fa fa-building-o" aria-hidden="true"></i> Problem Occurred During </div>

                                <div class="row">
                                    <%--RadioButton พบปัญหาระหว่าง--%>
                                    <div class="col-lg-12 col-sm-12 mt-1">
                                        <div class="input-group mb-3" style="width: 100%;">
                                            <div class="form-control" style="width: 100%; background-color: #fff;">

                                                <!-- RadioButton Row จัดแบบกระจายเท่า ๆ กัน -->
                                                <div class="d-flex justify-content-between align-items-center">

                                                    <div class="form-check mr-4">
                                                        <asp:RadioButton ID="rdo_Inspection" runat="server" CssClass="form-check-input" GroupName="ProblemOccuredGroup" />
                                                        <label class="form-check-label" for="rdo_Inspection">During Incoming Inspection</label>
                                                    </div>

                                                    <div class="form-check mr-4">
                                                        <asp:RadioButton ID="rdo_Production" runat="server" CssClass="form-check-input" GroupName="ProblemOccuredGroup" />
                                                        <label class="form-check-label" for="rdo_Production">During Production Line Usage</label>
                                                    </div>

                                                    <div class="form-check">
                                                        <asp:RadioButton ID="rdo_Customer" runat="server" CssClass="form-check-input" GroupName="ProblemOccuredGroup" />
                                                        <label class="form-check-label" for="rdo_Customer">Customer Complaint</label>
                                                    </div>

                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                            </div>

        </ContentTemplate>
        </asp:UpdatePanel>

        </section>

        <section id="page_Severity" class="services">

            <%--<asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager> ใช้อันเดียวพอแล้ว--%>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>

                    <div class="container2">

                        <div class="row">

                            <div class="col-lg-12 col-sm-12">
                                <%--Severity level--%>
                                <div class="card-header" style="font-weight: 600;" runat="server" id="Div4"><i class="fa fa-building-o" aria-hidden="true"></i> Severity level </div>

                                <div class="row">
                                    <%--RadioButton Severity level--%>
                                    <div class="col-lg-12 col-sm-12 mt-1">
                                        <div class="input-group mb-3" style="width: 100%;">
                                            <div class="form-control" style="width: 100%; background-color: #fff;">

                                                <!-- RadioButton Row จัดแบบกระจายเท่า ๆ กัน -->
                                                <div class="d-flex justify-content-between align-items-center">

                                                    <div class="form-check mr-4">
                                                        <asp:RadioButton ID="rdo_CriticalDef" runat="server" CssClass="form-check-input" GroupName="SeverityLevelGroup" />
                                                        <label class="form-check-label" for="rdo_Inspection">Critical Defect </label>
                                                    </div>

                                                    <div class="form-check mr-4">
                                                        <asp:RadioButton ID="rdo_MajorDef" runat="server" CssClass="form-check-input" GroupName="SeverityLevelGroup" />
                                                        <label class="form-check-label" for="rdo_Production">Major Defect </label>
                                                    </div>

                                                    <div class="form-check">
                                                        <asp:RadioButton ID="rdo_minorDef" runat="server" CssClass="form-check-input" GroupName="SeverityLevelGroup" />
                                                        <label class="form-check-label" for="rdo_Customer">Minor Defect </label>
                                                    </div>

                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>

        </ContentTemplate>
        </asp:UpdatePanel>

        </section>

        <div class="container2">

            <div class="row">

                <div class="col-lg-12 col-sm-12 mb-2">
                     <%--Employee amount--%>
                    <div class="card-header" style="font-weight: 600;" runat="server" id="Div5">
                        <i class="fa fa-building-o" aria-hidden="true"></i> The details of the problem are as follows by
                            <i class="fa fa-flag ms-2" style="font-size: 20px;" aria-hidden="true" id="I3" runat="server"></i>
                    </div>

                </div>

            </div>

             <div class="mb-3 text-center">
                 <label for="t_SQAQ_DetPro" class="form-label fw-semibold">Details</label>
                 <textarea class="form-control text-primary" id="t_SQAQ_DetPro" runat="server" rows="3" maxlength="1000"></textarea>
             </div>

            <div class="mb-3 text-center">
                <label for="t_SQAQ_Coil" class="form-label fw-semibold">Coil / Batch NO.</label>
                <textarea class="form-control text-primary" id="t_SQAQ_Coil" runat="server" rows="3" maxlength="1000"></textarea>
            </div>

            <div class="mb-3 text-center">
                <label for="t_SQAQ_DO" class="form-label fw-semibold">DO NO.</label>
                <textarea class="form-control text-primary" id="t_SQAQ_DO" runat="server" rows="3" maxlength="1000"></textarea>
            </div>

            <div class="mb-3 text-center">
                <label for="t_SQAQ_InDate" class="form-label fw-semibold">Date of Admission</label>
                <textarea class="form-control text-primary" id="t_SQAQ_InDate" runat="server" rows="3" maxlength="1000"></textarea>
            </div>

            <div class="mb-3 text-center">
                <label for="t_SQAQ_NoPro" class="form-label fw-semibold">Number of problems found</label>
                <textarea class="form-control text-primary" id="t_SQAQ_NoPro" runat="server" rows="3" maxlength="1000"></textarea>
            </div>

            <div class="mb-3 text-center">
                <label for="t_SQAQ_Note" class="form-label fw-bold">Annotation</label>
                <textarea class="form-control text-primary" id="t_SQAQ_Note" runat="server" rows="3" maxlength="1000"></textarea>
            </div>

        </div>

        <div class="container2">

            <div class="row">

                <div class="col-12">
                    <%--Quality System--%>
                    <div class="card-header" style="font-weight: 600;" runat="server" id="Div6" >
                        <i class="fa fa-building-o" aria-hidden="true"></i> RCI initial troubleshooting
                           <i class="fa fa-flag" style="font-size: 20px;" aria-hidden="true" id="I4" runat="server"></i>
                    </div>
                </div>

            </div>

            <div class="row">

                <div class="col-lg-12 col-sm-12 mt-2 mb-3">
                    <label class="sr-only" for="inlineFormInputGroup">sec1</label>
                    <div class="input-group mb-3">
                        <div class="input-group-prepend">
                            <div class="input-group-text">
                                <div style="width: 120px;">Troubleshooting </div>
                            </div>
                        </div>
                        <asp:DropDownList ID="dd_SQAS_Name" runat="server" CssClass="form-control" AutoPostBack="false"></asp:DropDownList>
                    </div>
                </div>

                <div class="col-lg-12 col-sm-12 mb-3">
                    <label for="t_SQAQ_RCISolProDet" class="form-label text-center w-100" style="font-weight: 600;">Details of Troubleshooting</label>
                    <textarea class="form-control" id="t_SQAQ_RCISolProDet" runat="server" rows="3" style="color: blue;" maxlength="1000"></textarea>
                </div>

            </div>

            <div class="row">

                <div class="col-lg-12 col-sm-12 mb-3">
                    <div class="card-header" style="font-weight: 600;" runat="server" id="Div3">
                        <i class="fa fa-building-o" aria-hidden="true"></i> Collect samples for supplier
                           
                    </div>
                </div>

                <div class="col-lg-6 col-sm-12">
                    <label class="sr-only" for="inlineFormInputGroup">sec1</label>
                    <div class="input-group mb-3">
                        <div class="input-group-prepend">
                            <div class="input-group-text">
                                <div style="width: 120px;">Type </div>
                            </div>
                        </div>
                        <input type="text" class="form-control" id="t_SQAQ_ExpTyp" runat="server" placeholder="">
                    </div>
                </div>

                <div class="col-lg-6 col-sm-12">
                    <label class="sr-only" for="inlineFormInputGroup">sec1</label>
                    <div class="input-group mb-3">
                        <div class="input-group-prepend">
                            <div class="input-group-text">
                                <div style="width: 120px;">Amount </div>
                            </div>
                        </div>
                        <input type="text" class="form-control" id="t_SQAQ_ExpTypCou" runat="server" placeholder="">
                    </div>
                </div>
            </div>

        </div>

        <section id="page_Summary" class="services">
            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <ContentTemplate>

                    <div class="container2">
                        <div class="row ">

                            <div class="col-lg-12 col-sm-12">

                                <%--Summary--%>
                                <div class="card-header" style="font-weight: 600;" runat="server" id="Div7">
                                    <i class="fa fa-building-o" aria-hidden="true"></i> Summary of evaluation results 
                        <i class="fa fa-flag" style="font-size: 20px;" aria-hidden="true" id="I5" runat="server"></i>
                                </div>

                                <label class="sr-only" for="inlineFormInputGroup">sec1</label>
                                <div class="input-group mb-3">
                                    <!-- หัว Result ด้านซ้าย -->
                                    <div class="input-group-prepend mt-1">
                                        <div class="input-group-text" style="width: 120px;">
                                            Result
                                        </div>
                                    </div>

                                    <!-- กล่องที่เหมือน input แต่ภายในมี radio 3 ตัว -->
                                    <div class="form-control d-flex justify-content-between align-items-center mt-1" style="background-color: #fff;">

                                        <label class="form-check-label ml-4">
                                            <asp:RadioButton ID="rdo_Reject" runat="server"
                                                GroupName="ResultGroup"
                                                CssClass="form-check-input"
                                                ClientIDMode="Static" />
                                            Reject
                                        </label>

                                        <label class="form-check-label ml-4">
                                            <asp:RadioButton ID="rdo_Hold" runat="server"
                                                GroupName="ResultGroup"
                                                CssClass="form-check-input"
                                                ClientIDMode="Static" />
                                            Hold
                                        </label>

                                        <label class="form-check-label ml-4">
                                            <asp:RadioButton ID="rdo_Warning" runat="server"
                                                GroupName="ResultGroup"
                                                CssClass="form-check-input"
                                                ClientIDMode="Static" />
                                            Warning
                                        </label>

                                    </div>
                                </div>

                                <label class="sr-only" for="inlineFormInputGroup">sec1</label>

                                <div class="d-flex flex-column mb-3" style="width: 100%; max-width: 100%;">

                                    <!-- กล่องข้อความหัวข้อ ขยายเต็มความกว้าง -->
                    <div class="input-group-text justify-content-center" font-size: 1rem; width: 100%;">
                        การดำเนินการกับผลิตภัณฑ์ที่ไม่เป็นไปตามข้อกำหนด
                    </div>

                                    <!-- กล่อง radio button ขยายเต็มความกว้างเท่ากัน แนวตั้ง -->
                                    <div class="form-control mt-2" style="background-color: #fff; width: 100%; padding: 1rem 1.25rem; height: auto; min-height: auto;">

                                        <!-- จัดแนวตั้ง + ให้มีระยะห่าง -->
                                        <div class="d-flex flex-column gap-3 w-100">
                                            <asp:Panel ID="pnlCheckBoxGroup" runat="server" CssClass="d-flex flex-column gap-3 w-100">

                                                <div class="form-check form-check-inline">
                                                    <asp:CheckBox ID="chkReturnToSupplier" runat="server" CssClass="form-check-input" Text="" ClientIDMode="Static" />
                                                    <label for="chkReturnToSupplier" class="form-check-label" style="margin-left: 0.3rem;">ส่งคืนผลิตภัณฑ์ที่ไม่เป็นไปตามข้อกำหนดให้กับทาง Supplier</label>
                                                </div>

                                                <div class="form-check form-check-inline">
                                                    <asp:CheckBox ID="chkClaimSupplier" runat="server" CssClass="form-check-input" Text="" ClientIDMode="Static" />
                                                    <label for="chkClaimSupplier" class="form-check-label" style="margin-left: 0.3rem;">เคลมกับทาง Supplier</label>
                                                </div>

                                                <div class="form-check form-check-inline">
                                                    <asp:CheckBox ID="chkSupplierSorting" runat="server" CssClass="form-check-input" Text="" ClientIDMode="Static" />
                                                    <label for="chkSupplierSorting" class="form-check-label" style="margin-left: 0.3rem;">ให้ทาง Supplier เข้ามาคัดแยกผลิตภัณฑ์ที่ไม่เป็นไปตามข้อกำหนด</label>
                                                </div>

                                                <div class="form-check form-check-inline">
                                                    <asp:CheckBox ID="chkRCISortingClaim" runat="server" CssClass="form-check-input" Text="" ClientIDMode="Static" />
                                                    <label for="chkRCISortingClaim" class="form-check-label" style="margin-left: 0.3rem;">ทาง RCI ทำการคัดแยกและเคลมค่าแรงกับทาง Supplier</label>
                                                </div>

                                                <div class="form-check form-check-inline">
                                                    <asp:CheckBox ID="chkRequestDoc" runat="server" CssClass="form-check-input" Text="" ClientIDMode="Static" />
                                                    <label for="chkRequestDoc" class="form-check-label" style="margin-left: 0.3rem;">ขอเอกสารชี้แจงสาเหตุและดำเนินการแก้ไข</label>
                                                </div>

                                                <div class="form-check form-check-inline">
                                                    <asp:CheckBox ID="chkAcceptCondition" runat="server" CssClass="form-check-input" Text="" ClientIDMode="Static" />
                                                    <label for="chkAcceptCondition" class="form-check-label" style="margin-left: 0.3rem;">ใช้ตามสภาพหรือยอมรับใช้พิเศษ (กรณ๊พบปัญหาซ้ำจะทำการ Reject ผลิตภัณฑ์ใน Lot. นั้นทั้งหมด)</label>
                                                </div>

                                                <div class="form-check form-check-inline">
                                                    <asp:CheckBox ID="chkOther" runat="server" CssClass="form-check-input" Text="" OnCheckedChanged="chkOther_CheckedChanged" ClientIDMode="Static" />
                                                    <label for="chkOther" class="form-check-label" style="margin-left: 0.3rem;">อื่น ๆ</label>
                                                </div>

                                                <!-- ช่อง input สำหรับ "อื่น ๆ" -->
                                                <asp:Panel ID="t_SQAQ_ResDetOth" runat="server" CssClass="mt-2" Style="display: none;">
                                                    <asp:TextBox ID="txtOtherDetail" runat="server" CssClass="form-control" placeholder="โปรดระบุอื่น ๆ" ClientIDMode="Static"/>
                                                </asp:Panel>

                                            </asp:Panel>
                        </div>
                                </div>
                            </div>
                        </div>

                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>
        </section>
        
        



                    <div class="col-lg-12 col-sm-12">

                <%--Attachments --%>
                <div class="card-header mb-2" style="font-weight: 600;" runat="server" id="Div2">
                    <i class="fa fa-building-o" aria-hidden="true"></i> Attachments 
                    <i class="fa fa-flag" style="font-size: 20px;" aria-hidden="true" id="I2" runat="server"></i>
                </div>

                <label class="sr-only" for="inlineFormInputGroup">sec1</label>
                <asp:DropDownList ID="dd_SQAA_Name" runat="server" CssClass="form-control"
                    onchange="handleDropdownChange(this)" AutoPostBack="false" ClientIDMode="Static">
                </asp:DropDownList>

                <!-- TextBox สำหรับกรอกอื่น ๆ -->
                <asp:TextBox ID="t_SQAQ_AttOth" runat="server" CssClass="form-control mt-2 mb-3"
                    ClientIDMode="Static"
                    placeholder="โปรดระบุอื่น ๆ"
                    Style="visibility: hidden; height: 0px;"></asp:TextBox>

                <div class="col-lg-12 col-sm-12">

                    <%--Minimum of Order (MOQ)--%>
                    <div class="card-header mt-3" runat="server" id="Div9" style="font-weight: normal;">
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
                                style="width: 150px;" />
                        </div>
                    </div>
                </div>
        </div>

        <section id="page_Rupload" class="services">

        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>

        <div class="container2">

            <div class="row">

                <div class="col-12 mt-2 mb-2">
                     <%--Related upload--%>
                     <div class="card-header" style="font-weight:600;" runat="server" id="Div13"><i class="fa fa-building-o" aria-hidden="true"></i> Related upload
                           <i class="fa fa-flag" style="font-size:20px;" aria-hidden="true" id="I11" runat="server"></i>
                     </div>
                </div>

            </div>

            <div class="row">

                <div class="col-lg-12 col-sm-12">
                    <label class="sr-only" for="inlineFormInputGroup">sec1</label>
                    <div class="input-group mb-3">
                        <div class="input-group-prepend">
                            <div class="input-group-text">
                                <div style="width: 80px;">File Name</div>
                            </div>
                        </div>
                        <input type="text" class="form-control" id="t_RelUpl_M_Name" runat="server" placeholder="" />
                    </div>
                </div>

            </div>

            <div class="row">
                <div class="col-lg-12 col-sm-12">
                    <label class="sr-only" for="inlineFormInputGroup">sec1</label>
                    <div class="input-group mb-3">
                        <div class="input-group-prepend">
                            <div class="input-group-text">
                                <div style="width: 100px;">Link PDF File</div>
                            </div>
                        </div>
                        <input type="text" class="form-control" id="t_RelUpl_Link" runat="server" placeholder="" />
                    </div>
                </div>
                <div class="col-lg-12 col-sm-12 d-flex justify-content-center">
                    <button type="button" id="_RelUpload" runat="server" class="btn btn-primary" data-dismiss="modal">บันทึก</button>
                </div>

                <div class="col-lg-1 col-sm-12">
                    <asp:Label ID="txt_RelUpload" runat="server" Text="" Visible="true"></asp:Label>
                </div>
                <div class="col-lg-11 col-sm-12 d-flex gap-2">
                    <button type="button" id="S_RelUpload" runat="server" class="btn btn-primary" data-dismiss="modal">Update</button>
                    <%--<button type="button" id="C_ProdType" runat="server" class="btn btn-primary" data-dismiss="modal">ยกเลิก แก้ไข</button>--%>
                    <asp:Button class="btn btn-warning btn-lg" ID="C_RelUpload" runat="server" Text=" Cancle " />
                </div>
            </div>

            <div class="row">
                <div class="col-12 mt-3">
                    <%-------GridView-------%>
                    <asp:GridView ID="example_RelUpload" CssClass="footable" runat="server" AutoGenerateColumns="false" DataKeyNames="SQAU_ID">
                        <Columns>
                            <asp:BoundField DataField="No" HeaderText="No." />
                            <asp:TemplateField HeaderText="Delete" ItemStyle-Width="60">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" ID="lbtDelete_RelUpload" CommandName="iDelete" CommandArgument='<%# Eval("SQAU_ID") %>'>
                    <i class="fa fa-trash fa-lg" aria-hidden="true"></i>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label runat="server" ID="h2563">File Name</asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="l2563"><%# Eval("SQAU_Name") %>&nbsp;&nbsp;</asp:Label>
                                    <asp:HyperLink runat="server" ID="hpl_Rel" NavigateUrl='<%# Eval("SQAU_Link") %>' Target="_blank">
                    <i class='fa fa-file-pdf-o' aria-hidden='true'></i>
                                    </asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Edit" ItemStyle-Width="60">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" ID="lbtEdit" CommandName="iEdit" CommandArgument='<%# Eval("SQAU_ID") %>'>
                    <i class="fa fa-pencil fa-lg" aria-hidden="true"></i>
                                    </asp:LinkButton>
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
   
<div class="modal-footer d-flex justify-content-center" style="gap:10px; width: 500px; margin: auto;">
    <button type="button" id="_ISCComProfile" runat="server" class="btn btn-success btn-lg" style="flex: 1; min-width: 0;">
        บันทึกข้อมูล
    </button>
    <asp:Button ID="btnReset" runat="server" Text="ยกเลิก" CssClass="btn btn-danger btn-lg" style="flex: 1; min-width: 0;" />
</div>
    
    <br />
    <br />


    </div>

    <script type="text/javascript">
        $(function () {
            $('#ContentPlaceHolder1_t_RelUpl_Date').datepicker({
                format: 'dd/mm/yyyy',
                autoclose: true,
                todayHighlight: true
            });
        });

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function (sender, args) {
            $('#ContentPlaceHolder1_t_RelUpl_Date').datepicker({
                format: 'dd/mm/yyyy',
                autoclose: true,
                todayHighlight: true
            });
        });

        $(function () {
            $('#ContentPlaceHolder1_t_ComPro_PO_Date').datepicker({
                format: 'dd/mm/yyyy',
                autoclose: true,
                todayHighlight: true
            });
        });

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function (sender, args) {
            $('#ContentPlaceHolder1_t_ComPro_PO_Date').datepicker({
                format: 'dd/mm/yyyy',
                autoclose: true,
                todayHighlight: true
            });
        });

    </script>

    <%--<script>
         $('#ContentPlaceHolder1_t_RelUpl_Date').datepicker({
             format: 'dd/mm/yyyy',
             autoclose: true,
         });
     </script>--%>

    <%--    <script>
        x1 = "<%=X2%>";
        if (x1 != "") {
            alert(x1);
        }
    </script>--%>

    <!-- DropDown และ Fields -->
    <script type="text/javascript">
        function updateMatName() {
            const ddl = document.getElementById("dd_SQAM_ProductT");
            const value = ddl.value;

            if (value && value.includes("|")) {
                const parts = value.split("|");
                const material = parts[0];
                const type = parts[1];

                document.getElementById("t_SQAM_TMaterials").value = material;
                document.getElementById("t_SQAM_Type").value = type;
            } else {
                document.getElementById("t_SQAM_TMaterials").value = "";
                document.getElementById("t_SQAM_Type").value = "";
            }

            // เก็บชื่อที่เลือกไว้ใน Hidden field
            document.getElementById("t_SQAM_ProductTText").value = ddl.options[ddl.selectedIndex].text;
        }
    </script>


    <script>
        function handleDropdownChange(selectElement) {
            const otherTextBox = document.getElementById('t_SQAQ_AttOth');
            if (selectElement.value.includes('อื่น')) {
                otherTextBox.style.visibility = 'visible';
                otherTextBox.style.height = 'auto';
            } else {
                otherTextBox.style.visibility = 'hidden';
                otherTextBox.style.height = '0px';
                otherTextBox.value = '';
            }
        }

        window.addEventListener('DOMContentLoaded', function () {
            const ddl = document.getElementById('dd_SQAA_Name');
            if (ddl.value.includes('อื่น')) {
                handleDropdownChange(ddl);
            }
        });
    </script>

<script type="text/javascript">
    window.onload = function () {
        const chkReturnToSupplier = document.getElementById('<%= chkReturnToSupplier.ClientID %>');
        const chkClaimSupplier = document.getElementById('<%= chkClaimSupplier.ClientID %>');
        const chkSupplierSorting = document.getElementById('<%= chkSupplierSorting.ClientID %>');
        const chkRCISortingClaim = document.getElementById('<%= chkRCISortingClaim.ClientID %>');
        const chkRequestDoc = document.getElementById('<%= chkRequestDoc.ClientID %>');
        const chkAcceptCondition = document.getElementById('<%= chkAcceptCondition.ClientID %>');
        const chkOther = document.getElementById('<%= chkOther.ClientID %>');
        const allCheckboxes = [
            chkReturnToSupplier,
            chkClaimSupplier,
            chkSupplierSorting,
            chkRCISortingClaim,
            chkRequestDoc,
            chkAcceptCondition,
            chkOther
        ];

        const panelOther = document.getElementById('<%= t_SQAQ_ResDetOth.ClientID %>');
        const txtOtherDetail = document.getElementById('<%= txtOtherDetail.ClientID %>');

        allCheckboxes.forEach(chk => {
            chk.onclick = function () {
                // ยกเลิก checkbox อื่น
                allCheckboxes.forEach(c => {
                    if (c !== chk) c.checked = false;
                });

                // จัดการช่อง input "อื่น ๆ"
                if (chk === chkOther && chk.checked) {
                    panelOther.style.display = 'block';
                    txtOtherDetail.disabled = false;
                } else {
                    panelOther.style.display = 'none';
                    txtOtherDetail.disabled = true;
                    txtOtherDetail.value = '';
                }
            };
        });
    };
</script>

    <script type="text/javascript">
        function toggleCheckboxGroups() {
            const isReject = document.getElementById("rdo_Reject").checked;
            const isHold = document.getElementById("rdo_Hold").checked;
            const isWarning = document.getElementById("rdo_Warning").checked;

            // Map ของ checkbox ตามกลุ่ม
            const rejectGroup = ["chkReturnToSupplier", "chkClaimSupplier", "chkRequestDoc", "chkOther"];
            const holdGroup = ["chkSupplierSorting", "chkRCISortingClaim", "chkRequestDoc", "chkOther"];
            const warningGroup = ["chkRequestDoc", "chkAcceptCondition", "chkOther"];

            // รวมทุก checkbox
            const allCheckboxIds = [
                "chkReturnToSupplier", "chkClaimSupplier", "chkSupplierSorting",
                "chkRCISortingClaim", "chkRequestDoc", "chkAcceptCondition", "chkOther"
            ];

            // กำหนดกลุ่มที่จะแสดง
            let showGroup = [];
            if (isReject) {
                showGroup = rejectGroup;
            } else if (isHold) {
                showGroup = holdGroup;
            } else if (isWarning) {
                showGroup = warningGroup;
            } else {
                // ไม่เลือกอะไรเลย แสดงทั้งหมด
                showGroup = allCheckboxIds;
            }

            // Loop ซ่อน/แสดง checkbox
            allCheckboxIds.forEach(function (id) {
                const checkbox = document.getElementById(id);
                if (checkbox) {
                    const parentDiv = checkbox.closest(".form-check");
                    if (showGroup.includes(id)) {
                        parentDiv.style.display = "inline-block";
                    } else {
                        parentDiv.style.display = "none";
                        checkbox.checked = false; // เคลียร์ checkbox ที่ถูกซ่อน
                    }
                }
            });

            // ซ่อนช่อง input ถ้าไม่ได้เลือก chkOther
            const chkOther = document.getElementById("chkOther");
            const panelOther = document.getElementById("t_SQAQ_ResDetOth");
            if (chkOther && !chkOther.checked) {
                panelOther.style.display = "none";
            }
        }

        // ใช้งานเมื่อโหลดหน้า และเมื่อ radio เปลี่ยน
        window.addEventListener("load", toggleCheckboxGroups);
        document.getElementById("rdo_Reject").addEventListener("change", toggleCheckboxGroups);
        document.getElementById("rdo_Hold").addEventListener("change", toggleCheckboxGroups);
        document.getElementById("rdo_Warning").addEventListener("change", toggleCheckboxGroups);

        // แสดงช่อง "อื่น ๆ" ถ้าเลือก chkOther
        document.getElementById("chkOther").addEventListener("change", function () {
            const panelOther = document.getElementById("t_SQAQ_ResDetOth");
            if (this.checked) {
                panelOther.style.display = "block";
            } else {
                panelOther.style.display = "none";
                document.getElementById("txtOtherDetail").value = "";
            }
        });
    </script>

</asp:Content>
