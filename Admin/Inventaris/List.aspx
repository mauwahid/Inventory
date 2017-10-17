<%@ Page Title="" Language="C#" MasterPageFile="~/Template.master" AutoEventWireup="true" CodeFile="List.aspx.cs" Inherits="Admin_Inventaris_List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="NavContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" Runat="Server">
    <h1>
            Inventaris
            
    </h1>
    <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-support"></i>Data Master</a></li>
            <li class="active">Inventaris</li>
    </ol>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
      <form id="Form1" runat="server" defaultbutton="btnCari">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate> 
    

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
                        <asp:HiddenField ID="HiddenItemID" runat="server" />
                    
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
                                   DataKeyNames="id_inventaris"   AllowPaging="true"
                                   ShowHeaderWhenEmpty="true"
                                   CssClass="table table-bordered" 
                                   OnRowCommand="RowCommand_Click"
                                   OnPageIndexChanging="GView_PageIndexChanging"
                                   OnRowEditing="RowEdit_Click">
                                <PagerStyle CssClass="gridview" />
                                <Columns>
                                    <asp:BoundField DataField="id_inventaris" HeaderText="Id" />
                                    <asp:BoundField DataField="nama_inventaris" HeaderText="Nama Inventaris" />
                                    <asp:BoundField DataField="jenis_inventaris" HeaderText="Jenis" />
                                    <asp:BoundField DataField="Harga" HeaderText="Harga" />
                                    <asp:BoundField DataField="Merk" HeaderText="Merk" />
                                    <asp:BoundField DataField="Distributor" HeaderText="Distributor" />
                                    <asp:BoundField DataField="Qty" HeaderText="Qty" />
                                    <asp:BoundField DataField="qty_belum_terpakai" HeaderText="Qty Belum Terpakai" />
                                    <asp:BoundField DataField="qty_dalam_ruangan" HeaderText="Qty Dalam Ruangan" />
                                    <asp:BoundField DataField="Keterangan" HeaderText="Keterangan" />
                                      <asp:CommandField ShowEditButton="true" HeaderText="Edit" />
                                       <asp:TemplateField HeaderText="Ubah Qty">
                                         <ItemTemplate>
                                           <asp:LinkButton ID="LBUbhqty" 
                                             CommandArgument='<%# Eval("id_inventaris") %>'
                                             CommandName="UbahQty" runat="server">
                                             Pilih</asp:LinkButton>
                                         </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Hapus">
                                         <ItemTemplate>
                                           <asp:LinkButton ID="LinkButton1" 
                                             CommandArgument='<%# Eval("id_inventaris") %>'
                                             OnClientClick="return confirm('Yakin ingin dihapus?')" 
                                             CommandName="Hapus" runat="server">
                                             Hapus</asp:LinkButton>
                                         </ItemTemplate>
                                    </asp:TemplateField>
                                 </Columns>
                                </asp:GridView>
							
                 
                </div><!-- /.box-body -->
              
              </div><!-- /.box -->
    </section>
      <div class="modal" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"  aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="H1">
                        <asp:Label runat="server" ID="LblForm" Text="" /> Ubah Qty Inventaris</h4>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label >Nama</label>
                        <asp:TextBox ID="TbNama" Text=""  Enabled="false"  runat="server" CssClass="form-control" />
                        
                   </div>
                   <div class="form-group">
                        <label >Jumlah</label>
                        <asp:TextBox ID="TbJumlah" Text=""  TextMode="Number"  runat="server" CssClass="form-control" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ControlToValidate="TbJumlah"
                        ErrorMessage="Jumlah Harus diisi"
                        ValidationGroup="ValGroup"
                        ForeColor="Red"></asp:RequiredFieldValidator>
                   </div>
                  
                   
                 </div>
                <div class="modal-footer">
                    <asp:Button ID="BtnTambahQty" onclick="Tambah_Click" ValidationGroup="ValGroup" runat="server" CssClass="btn btn-primary" Text="Tambah" />
                    <asp:Button ID="BtnKurangiQty" onclick="Kurangi_Click" ValidationGroup="ValGroup" runat="server" CssClass="btn btn-success" Text="Kurangi" />

                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        Close</button>
                    
                    <%--<button type="button"  class="btn btn-primary">
                        Save changes</button>--%>
                    </div>
            </div>
        </div>
    </div>
     </ContentTemplate>
            </asp:UpdatePanel> 
   
    </form>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="JsContent" Runat="Server">
      <script type="text/javascript">

          $('ul li').removeClass('active');
          $('#m_admin_tree_master').addClass('active');
          $('#m_admin_inventaris').addClass('active');

          function showModalEditInv() {
              //  alert("Hallo");
              $('.modal-backdrop').remove();
              $('#myModal').modal('show');
          }

          function hideModal() {
              //  alert("Hallo");
              $('.modal-backdrop').remove();
              $('#myModal').modal('hide');
          }
       

    </script>
</asp:Content>



