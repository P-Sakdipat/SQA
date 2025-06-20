<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MyMasterPage.Master" CodeBehind="PageUser.aspx.vb" Inherits="SQA.PageUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /*Profile Pic Start*/
        .picture-container{
            position: relative;
            cursor: pointer;
            text-align: center;
        }
        .picture{
            width: 106px;
            height: 106px;
            background-color: #999999;
            border: 4px solid #CCCCCC;
            color: #FFFFFF;
            border-radius: 50%;
            margin: 0px auto;
            overflow: hidden;
            transition: all 0.2s;
            -webkit-transition: all 0.2s;
        }
        .picture:hover{
            border-color: #2ca8ff;
        }
        .content.ct-wizard-green .picture:hover{
            border-color: #05ae0e;
        }
        .content.ct-wizard-blue .picture:hover{
            border-color: #3472f7;
        }
        .content.ct-wizard-orange .picture:hover{
            border-color: #ff9500;
        }
        .content.ct-wizard-red .picture:hover{
            border-color: #ff3b30;
        }
        .picture input[type="file"] {
            cursor: pointer;
            display: block;
            height: 100%;
            left: 0;
            opacity: 0 !important;
            position: absolute;
            top: 0;
            width: 100%;
        }

        .picture-src{
            width: 100%;
    
        }
        /*Profile Pic End*/

    </style>
    <script>
        $(document).ready(function () {
            // Prepare the preview for profile picture
            $("#ContentPlaceHolder1_file2").change(function () {
                readURL(this);
            });

        });
        function readURL(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();

                reader.onload = function (e) {
                    $('#ContentPlaceHolder1_wizardPicturePreview').attr('src', e.target.result).fadeIn('slow');
                    
                }
                reader.readAsDataURL(input.files[0]);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <br />
    <div class="container">
        <div><h2>User Setup &nbsp; 
            <asp:LinkButton runat="server" ID="lnkOrderFind" class="btn btn-danger btn-sm" data-toggle="modal" data-target="#modalId">🔑 เปลี่ยนรหัสผ่าน</asp:LinkButton>
            </h2> 
        </div><br />
    </div>
    <div class="container">
        <div class="row col-lg-6 text-center">
            <div class="picture-container col-sm-12">
                <div class="picture">
                    <img src="./images/manager128.png" class="picture-src" id="wizardPicturePreview" title="" runat="server"/>
                    <input type="file" id="file2" accept="image/*" class="" runat="server"/> 
                </div>
                <h6 class=""><i class="fa fa-camera" aria-hidden="true"></i></h6>
                <span>ไฟล์ .jpg , .jpeg , .png ขนาดไม่เกิน 3MB.</span>
            </div>
        </div>   
        <div class="row">
            <div class="col-auto">
                <label class="sr-only" for="inlineFormInputGroup">sec1</label>
                <div class="input-group mb-3">
                    <div class="input-group-prepend">
                        <div class="input-group-text">
                            <div style="width:100px;">User Name</div>
                        </div>
                    </div>
                    <input type="text" class="form-control" id="tuser" runat="server" Readonly/>&nbsp;
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-10 col-sm-12">
                <label class="sr-only" for="inlineFormInputGroup">sec1</label>
                <div class="input-group mb-3">
                    <div class="input-group-prepend">
                        <div class="input-group-text">
                            <div style="width:100px;">Full Name</div>
                        </div>
                    </div>
                    <input type="text" class="form-control" id="tname" runat="server" placeholder="ชื่อ-นามสกุล"/>&nbsp;                        
                </div>
            </div>
        </div>
        <div class="form-row">
            <div class="col-auto">
                <asp:Button ID="btnHSave" class="btn btn-primary mb-2" runat="server" Text=" บันทึก "/>
                <asp:Label ID="tidno" runat="server" Text="" Visible="false"></asp:Label>
            </div>
        </div><br />
            
        <div class="modal" tabindex="-1" role="dialog" id="modalId" data-backdrop="static" aria-labelledby="staticBackdropLabel">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                    <h5 class="modal-title">Change Password</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-auto">
                                <label class="sr-only" for="inlineFormInputGroup">sec1</label>
                                <div class="input-group mb-3">
                                    <div class="input-group-prepend">
                                        <div class="input-group-text">
                                            <div style="width:120px;">รหัสเก่า</div>
                                        </div>
                                    </div>
                                    <input type="password" class="form-control" id="topass" runat="server" placeholder="old password"/>
                                </div>
                            </div>

                        </div>
                        <div class="row">
                            <div class="col-auto">
                                <label class="sr-only" for="inlineFormInputGroup">sec2</label>
                                <div class="input-group mb-3">
                                    <div class="input-group-prepend">
                                        <div class="input-group-text">
                                            <div style="width:120px;">รหัสใหม่</div>
                                        </div>
                                    </div>
                                    <input type="password" class="form-control" id="tnpass" runat="server" placeholder="new password"/>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" id="btnSave" runat="server" class="btn btn-primary" data-dismiss="modal">บันทึกข้อมูล</button>                            
                        <asp:Label ID="txtidno" runat="server" Text="" Visible="false"></asp:Label>
                    </div>
                </div>
            </div>
        </div>

    </div>

    <script>
        x1 = "<%=X2%>";
        if (x1 != "") {
            alert(x1);
        }
    </script>

</asp:Content>
