<%@ Page Title="" Language="C#" MasterPageFile="~/Template.master" AutoEventWireup="true" CodeFile="UserList.aspx.cs" Inherits="Admin_User_UserList" %>


<asp:Content ID="Content1" ContentPlaceHolderID="NavContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" Runat="Server">
    <h1>
            User
            
    </h1>
    <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-user"></i>Administrasi</a></li>
            <li class="active">Users</li>
    </ol>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
      <form id="Form1" runat="server">
    <section class="content">
              <div class="box box-primary">
                <div class="box-header">
                  <h3 class="box-title"></h3>
                </div>
                <div class="box-body">
                <div class="row">
                 <div class="col-md-6">
                        <asp:Button CssClass="btn btn-primary"  runat="server" Text="Tambah" 
                            ID="btnTambah" onclick="btnTambah_Click" />
                        <asp:Button CssClass="btn btn-info"  runat="server" Text="Refresh" 
                            ID="btnRefresh" onclick="btnRefresh_Click" />
                  </div>
                    <div class="col-md-6">
                        <div class="input-group input-group-sm">
                        <asp:TextBox CssClass="form-control" ID="TbCari" runat="server" />
                        <span class="input-group-btn">
                            <asp:Button ID="btnCari" CssClass="btn btn-info btn-flat" runat="server" 
                                Text="Cari !" onclick="btnCari_Click" />
                        </span>
                        </div>
                    
                    </div>
                </div>
                </div><!-- /.box-body -->
                </div><!-- /.box -->
              <div class="box box-primary">
                <div class="box-header">
                  <h3 class="box-title"></h3>
                </div>
                <div class="box-body">
                      <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                                   DataKeyNames="Id"   AllowPaging="true"
                                   ShowHeaderWhenEmpty="true"
                                   CssClass="table table-bordered" 
                                   OnRowCommand="RowCommand_Click"
                                   OnPageIndexChanging="GView_PageIndexChanging"
                                   OnRowEditing="RowEdit_Click">
                                <PagerStyle CssClass="gridview" />
                                <Columns>
                                    <asp:BoundField DataField="Id" HeaderText="Id" />
                                    <asp:BoundField DataField="Username" HeaderText="Username" />
                                    <asp:BoundField DataField="NamaLengkap" HeaderText="Nama Lengkap" />
                                    <asp:BoundField DataField="NoTelp" HeaderText="No Telp" />
                                    <asp:BoundField DataField="Email" HeaderText="Email" />
                                    <asp:BoundField DataField="Deskripsi" HeaderText="Info Profil" />
                                    <asp:BoundField DataField="Role" HeaderText="Role" />
                                     <asp:CommandField ShowEditButton="true" HeaderText="Ubah" />
                                    <asp:TemplateField HeaderText="Hapus">
                                         <ItemTemplate>
                                           <asp:LinkButton ID="LinkButton1" 
                                             CommandArgument='<%# Eval("Id") %>'
                                             OnClientClick="return confirm('Yakin ingin dihapus?')" 
                                             CommandName="Hapus" runat="server">
                                             Delete</asp:LinkButton>
                                         </ItemTemplate>
                                    </asp:TemplateField>
                                 </Columns>
                                </asp:GridView>
							
                 
                </div><!-- /.box-body -->
              
              </div><!-- /.box -->
    </section>
    </form>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="JsContent" Runat="Server">
      <script type="text/javascript">

          $('ul li').removeClass('active');
          $('#m_admin_administrasi').addClass('active');
          $('#m_admin_user').addClass('active');

       

    </script>
</asp:Content>

