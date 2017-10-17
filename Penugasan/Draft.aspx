<%@ Page Title="" Language="C#" MasterPageFile="~/Template.master" AutoEventWireup="true" CodeFile="Draft.aspx.cs" Inherits="Penugasan_ListPenugasan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="NavContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" Runat="Server">
    <h1>
           Draft Penugasan Untuk Teknisi
            
    </h1>
    <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-user"></i>Penugasan</a></li>
            <li class="active">List Penugasan Teknisi</li>
    </ol>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
      <form id="Form1" runat="server" defaultbutton="btnCari">
    <section class="content">
              <div class="box box-primary">
                <div class="box-header">
                  <h3 class="box-title"></h3>
                </div>
                <div class="box-body">
                <div class="row">
                 <div class="col-md-6">
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
                                   DataKeyNames="Id_penugasan"   AllowPaging="true"
                                   ShowHeaderWhenEmpty="true"
                                   CssClass="table table-bordered" 
                                   OnRowCommand="RowCommand_Click"
                                   OnPageIndexChanging="GView_PageIndexChanging">
                                <PagerStyle CssClass="gridview" />
                                <Columns>
                                    <asp:BoundField DataField="id_penugasan" HeaderText="Id" />
                                    <asp:BoundField DataField="tanggal_penugasan" HeaderText="Tanggal Penugasan" />
                                    <asp:BoundField DataField="judul" HeaderText="Judul" />
                                    <asp:BoundField DataField="username" HeaderText="Pemberi Tugas" />
                                    <asp:BoundField DataField="status" HeaderText="Status" />
                                      <asp:TemplateField HeaderText="Detail">
                                         <ItemTemplate>
                                           <asp:LinkButton ID="LinkButton1" 
                                             CommandArgument='<%# Eval("Id_penugasan") %>'
                                             CommandName="Pilih" runat="server">
                                             Pilih</asp:LinkButton>
                                         </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Hapus">
                                         <ItemTemplate>
                                           <asp:LinkButton ID="LBDelete"
                                             OnClientClick="return confirm('Yakin data akan dihapus?');"
                                             CommandArgument='<%# Eval("Id_penugasan") %>'
                                             CommandName="Hapus" runat="server">
                                             Hapus</asp:LinkButton>
                                         </ItemTemplate>
                                    </asp:TemplateField>
                                
                                 </Columns>
                                </asp:GridView>
							
                 
                </div><!-- /.box-body -->
              
              </div><!-- /.box -->
    </section>
    </form>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="JsContent" Runat="Server">
<script type="text/javascript">

    $('ul li').removeClass('active');
    $('#m_kabag_penugasan').addClass('active');
    $('#m_kabag_penugasan_draft').addClass('active');


</script>
</asp:Content>
