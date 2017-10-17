<%@ Page Title="" Language="C#" MasterPageFile="~/Template.master" AutoEventWireup="true" CodeFile="Profile.aspx.cs" Inherits="Admin_User_Profile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="NavContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" Runat="Server">
    <h1>
          <asp:Label ID="LblHeader" runat="server" Text="Profile" />
            
    </h1>
    <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-user"></i>Administrasi</a></li>
            <li class="">Users</li><li class="active"><asp:Label ID="LblBC" runat="server" text="Profile"/></li>
    </ol>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <form id="Form1" runat="server">
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
                      <asp:TextBox ID="TbUsername" Text=""  runat="server" CssClass="form-control" ToolTip="Masukan username"/>
                    </div>
                    <div class="form-group">
                      <label for="exampleInputPassword1">Password</label>
                     <asp:TextBox ID="TbPassword"  runat="server" TextMode="Password" CssClass="form-control" ToolTip="Masukan password"/>
                    </div>
                    <div class="form-group">
                      <label for="exampleInputEmail1">Nama Lengkap</label>
                      <asp:TextBox ID="TbNamaLengkap" Text=""  runat="server" CssClass="form-control" ToolTip="Masukan Nama Lengkap"/>
                    </div>
                   <div class="form-group">
                      <label for="exampleInputEmail1">No Telp</label>
                       <asp:TextBox ID="TbNoTelp" Text="" TextMode="Number"  runat="server" CssClass="form-control" ToolTip="Masukan No Telepon"/>
                  </div>
                    <div class="form-group">
                      <label for="exampleInputEmail1">Email</label>
                      <asp:TextBox ID="TbEmail" TextMode="Email" Text=""  runat="server" CssClass="form-control" />
                    </div>
                    <div class="form-group">
                      <label for="exampleInputEmail1">Info Profil</label>
                      <asp:TextBox ID="TbDeskripsi" Text="" TextMode="MultiLine"  runat="server" CssClass="form-control" ToolTip="Masukan username"/>
                    </div>
                    <div class="form-group">
                      <label for="exampleInputEmail1">Role</label>
                      <asp:DropDownList ID="DDLRole"  CssClass="form-control" runat="server">
                        <asp:ListItem Value="1" Text="Administrator" />
                        <asp:ListItem Value="2" Text="Kepala Bagian" />
                        <asp:ListItem Value="3" Text="Pimpinan" />
                        <asp:ListItem Value="4" Text="Bagian Pencatatan" />
                        <asp:ListItem Value="5" Text="Teknisi" />
                      </asp:DropDownList>
                    </div>
                  </div><!-- /.box-body -->

                  <div class="box-footer">
                   <asp:Button ID="BtnSubmit" runat="server" CssClass="btn btn-primary" Text="Simpan" 
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
          $('#m_admin_administrasi').addClass('active');
          $('#m_admin_profile').addClass('active');

       

    </script>
</asp:Content>

