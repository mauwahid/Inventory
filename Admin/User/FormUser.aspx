<%@ Page Title="" Language="C#" MasterPageFile="~/Template.master" AutoEventWireup="true" CodeFile="FormUser.aspx.cs" Inherits="Admin_User_FormUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="NavContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" Runat="Server">
    <h1>
          <asp:Label ID="LblHeader" runat="server" Text="Form User" />
            
    </h1>
    <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-user"></i>Administrasi</a></li>
            <li class="">Users</li><li class="active"><asp:Label ID="LblBC" runat="server" text="Form User"/></li>
    </ol>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <form id="Form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
    <section class="content">
         <div class="col-md-6 center-block">
          <div class="box box-primary">
                <div class="box-header">
                  <h3 class="box-title">Form User</h3>
                </div><!-- /.box-header -->
                <!-- form start -->
                  <div class="box-body">
                    <div class="form-group">
                      <label for="exampleInputEmail1">Username</label>
                      <asp:HiddenField ID="HiddenId" runat="server" Value="0" />
                      <asp:TextBox ID="TbUsername" Text="" MaxLength="20" runat="server" CssClass="form-control" ToolTip="Masukan username"/>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="Username harus diisi"
                        ForeColor="Red"
                        ValidationGroup="ValGroup"
                        ControlToValidate="TbUsername"
                        runat="server" />
                    </div>
                    <div class="form-group">
                      <label for="exampleInputPassword1">Password</label>
                      <asp:TextBox ID="TbPassword"  runat="server" TextMode="Password" CssClass="form-control" ToolTip="Masukan password"/>
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ErrorMessage="Password harus diisi"
                        ForeColor="Red"
                        ValidationGroup="ValGroup"
                        ControlToValidate="TbPassword"
                        runat="server" />
                    </div>
                    <div class="form-group">
                      <label for="exampleInputEmail1">Nama Lengkap</label>
                      <asp:TextBox ID="TbNamaLengkap" Text="" MaxLength="50"  runat="server" CssClass="form-control" ToolTip="Masukan Nama Lengkap"/>
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ErrorMessage="Nama Lengkap harus diisi"
                        ForeColor="Red"
                        ValidationGroup="ValGroup"
                        ControlToValidate="TbNamaLengkap"
                        runat="server" />
                    </div>
                   <div class="form-group">
                      <label for="exampleInputEmail1">No Telp</label>
                       <asp:TextBox ID="TbNoTelp" Text="" TextMode="Number"  runat="server" CssClass="form-control" ToolTip="Masukan No Telepon"/>
                  </div>
                    <div class="form-group">
                      <label for="exampleInputEmail1">Email</label>
                      <asp:TextBox ID="TbEmail" TextMode="Email" MaxLength="20" Text=""  runat="server" CssClass="form-control" />
                    </div>
                    <div class="form-group">
                      <label for="exampleInputEmail1">Info Profil</label>
                      <asp:TextBox ID="TbDeskripsi" Text="" MaxLength="40" TextMode="MultiLine"  runat="server" CssClass="form-control" ToolTip="Masukan username"/>
                    </div>
                    <div class="form-group">
                      <label for="exampleInputEmail1">Role</label>
                      <asp:DropDownList ID="DDLRole"  CssClass="form-control" runat="server">
                        <asp:ListItem Value="1" Text="Administrator" />
                        <asp:ListItem Value="2" Text="Ketua Divisi" />
                        <asp:ListItem Value="3" Text="Koordinator BPRTIK" />
                        <asp:ListItem Value="4" Text="Bagian Pencatatan" />
                        <asp:ListItem Value="5" Text="Teknisi" />
                      </asp:DropDownList>
                    </div>
                  </div><!-- /.box-body -->

                  <div class="box-footer">
                   <asp:Button ID="BtnSubmit" runat="server" ValidationGroup="ValGroup" CssClass="btn btn-primary" Text="Simpan" 
                          onclick="BtnSubmit_Click" />
                    <asp:Button ID="BtnCancel" runat="server" CssClass="btn btn-delete" 
                          Text="Batalkan" onclick="BtnCancel_Click" UseSubmitBehavior="false"
                           />
                  </div>
              </div><!-- /.box -->
              </div>
   
     </section>
    </ContentTemplate>
    </asp:UpdatePanel>
       </form>


</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="JsContent" Runat="Server">
      <script type="text/javascript">

          $('ul li').removeClass('active');
          $('#m_admin_administrasi').addClass('active');

          function loadTambahUser() {
              alert("hiii");
              $('#m_admin_form_user').addClass('active');
          }
          
       

    </script>
</asp:Content>
