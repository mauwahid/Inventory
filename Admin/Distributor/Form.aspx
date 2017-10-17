<%@ Page Title="" Language="C#" MasterPageFile="~/Template.master" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="Admin_Distributor_Form" %>

<asp:Content ID="Content1" ContentPlaceHolderID="NavContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" Runat="Server">
    <h1>
          <asp:Label ID="LblHeader" runat="server" Text="Form Distributor" />
            
    </h1>
    <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-support"></i>Data Master</a></li>
            <li class="">Distributor</li><li class="active"><asp:Label ID="LblBC" runat="server" text="Form Distributor"/></li>
    </ol>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <form id="Form1" runat="server">
    <asp:ValidationSummary ID="ValSum1" ForeColor="Red" runat="server" ValidationGroup="ValGroup"/>
    <section class="content">

        <div class="col-md-6 center-block">
          <div class="box box-primary">
                <div class="box-header">
                  <h3 class="box-title">Form Distributor</h3>
                </div><!-- /.box-header -->
                <!-- form start -->
                  <div class="box-body">
                    <div class="form-group">
                      <label for="exampleInputEmail1">Nama Distributor</label>
                      <asp:HiddenField ID="HiddenId" runat="server" Value="0" />
                      <asp:TextBox ID="TbDistributor" Text="" MaxLength="50"  runat="server" ValidationGroup="ValGroup" CssClass="form-control" />
                      <asp:RequiredFieldValidator ID="ReqVal1"
                        ControlToValidate="TbDistributor"
                        ValidationGroup="ValGroup"
                        runat="server"
                        ForeColor="Red"
                        Tooltip="Nama Distributor Harus diisi"
                        ErrorMessage="Error. Nama Distributor harus diisi"
                      ></asp:RequiredFieldValidator>
                    </div>
                     <div class="form-group">
                      <label for="exampleInputEmail1">Alamat</label>
                      <asp:TextBox ID="TbAlamat" Text=""  runat="server"  MaxLength="1000" CssClass="form-control" />
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                        ControlToValidate="TbAlamat"
                        ValidationGroup="ValGroup"
                        runat="server"
                        ForeColor="Red"
                        Tooltip="Alamat Distributor Harus diisi"
                        ErrorMessage="Error. Alamat Distributor harus diisi"
                      ></asp:RequiredFieldValidator>
                    </div>
                     <div class="form-group">
                      <label for="exampleInputEmail1">No Telp</label>
                      <asp:TextBox ID="TbNoTelp" Text="" TextMode="Number" MaxLength="20" runat="server" CssClass="form-control" />
                    </div>
                     <div class="form-group">
                      <label for="exampleInputEmail1">Email</label>
                      <asp:TextBox ID="TbEmail" Text="" TextMode="Email"  MaxLength="50"  runat="server" CssClass="form-control" />
                      <asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                         runat="server"
                        ToolTip="Email is not valid."
                        ValidationGroup="DefaultForm"
                        ErrorMessage="Email tidak valid"
                        ControlToValidate="TbEmail"
                         ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-
                        .]\w+)*">*</asp:RegularExpressionValidator>
                    </div>
                     <div class="form-group">
                      <label for="exampleInputEmail1">Website</label>
                      <asp:TextBox ID="TbWebsite" Text=""   runat="server"  MaxLength="50" CssClass="form-control" />
                    </div>
                    </div><!-- /.box-body -->

                  <div class="box-footer">
                   <asp:Button ID="BtnSubmit" runat="server" ValidationGroup="ValGroup" CssClass="btn btn-primary" Text="Simpan" 
                          onclick="BtnSubmit_Click" />
                    <asp:Button ID="BtnCancel" runat="server" CssClass="btn btn-delete" 
                          Text="Batalkan" onclick="BtnCancel_Click" 
                           />
                  </div>
              </div><!-- /.box -->
              </div>
   
     </section>
    </form>


</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="JsContent" Runat="Server">
      <script type="text/javascript">

          $('ul li').removeClass('active');
          $('#m_admin_tree_master').addClass('active');
          $('#m_admin_distributor').addClass('active');

       

    </script>
</asp:Content>

