<%@ Page Title="" Language="C#" MasterPageFile="~/Template.master" AutoEventWireup="true" CodeFile="PencatatanDetail.aspx.cs" Inherits="Pemeliharaan_KerusakanDetail" %>


<asp:Content ID="Content1" ContentPlaceHolderID="NavContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" Runat="Server">
    <h1>
            Detail Pencatatan Barang
            
    </h1>
    <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-user"></i>Pemeliharaan</a></li>
            <li class="active">Detail Pencatatan Barang</li>
    </ol>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
     <form id="Form1" runat="server" defaultbutton="BtnTambah" >
     <asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
       
    <section class="content">
       
                     <div class="box">
                <div class="box-header">
                  <h3 class="box-title"></h3>
                </div><!-- /.box-header -->
                <div class="box-body no-padding">
                  <table class="table table-striped">
                    <tr>
                      <td>No Pencatatan</td>
                      <td><asp:HiddenField ID="HiddenIdPencatatan" runat="server" /> <asp:Label runat="server" ID="LblNoPencatatan" /></td>
                       <td>Pencatat</td>
                      <td><asp:HiddenField ID="HiddenIdRuangan" runat="server" /> <asp:Label runat="server" ID="LblPencatat" /></td>
                       </tr>
                    <tr>
                      <td>Tanggal Pencatatan</td>
                      <td><asp:Label runat="server" ID="LblTanggalPencatatan" /></td>
                      <td>Catatan</td>
                      <td><asp:Label runat="server" ID="LblCatatan" /></td>
                      </tr>
                    
                    
                  </table>
                </div><!-- /.box-body -->
                <div class="box-footer clearfix">
                  <asp:Button ID="BtnTambah" 
                         CssClass="btn btn-primary" runat="server" Text="Tambah Inventaris" 
                        onclick="BtnTambah_Click"/>
                  <asp:Button ID="BtnSimpanPengajuan"
                        CssClass="btn btn-info"  runat="server" 
                        Text="Simpan Pengajuan" onclick="SimpanPencatatan_Click"  OnClientClick="return confirm('Anda yakin akan mengirim pengajuan?')" />
                  <asp:Button ID="BtnCancel" UseSubmitBehavior="false"  CssClass="btn btn-warning"  runat="server" Text="Batal"/>
                  <asp:Button ID="BtnDeletePengajuan"   
                        OnClientClick="return confirm('Anda yakin akan menghapus draft pengajuan?')"  
                        CssClass="btn btn-danger"  runat="server" Text="Hapus Draft" 
                        onclick="BtnDeletePencatatan_Click"/>
                </div>
              </div><!-- /.box -->
       
              <div class="box box-primary">
                <div class="box-header">
                  <h3 class="box-title"></h3>
                </div>
                <div class="box-body">
                      <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                                   DataKeyNames="id"   AllowPaging="true"
                                   ShowHeaderWhenEmpty="true"
                                   CssClass="table table-bordered" 
                                   OnRowCommand="RowCommand_Click"
                                   OnPageIndexChanging="GView_PageIndexChanging" >
                                <PagerStyle CssClass="gridview" />
                                <Columns>
                                    <asp:BoundField DataField="id" HeaderText="Id" Visible="true" />
                                    <asp:BoundField DataField="id_inventaris" />
                                    <asp:BoundField DataField="nama_inventaris" HeaderText="Nama Inventaris" />
                                    <asp:BoundField DataField="qty" HeaderText="Jumlah" />
                                    <asp:BoundField DataField="tipe" HeaderText="Tipe" />
                                    <asp:BoundField DataField="catatan" HeaderText="Catatan" />
                                    <asp:TemplateField HeaderText="Edit">
                                         <ItemTemplate>
                                           <asp:LinkButton ID="LBEdit" 
                                             CommandArgument='<%# Eval("Id") %>'
                                             CommandName="Ubah" runat="server">
                                             Ubah</asp:LinkButton>
                                         </ItemTemplate>
                                    </asp:TemplateField>
                                 </Columns>
                                </asp:GridView>
							
                 
                </div><!-- /.box-body -->
              
              </div><!-- /.box -->
    </section>
                    <!-- Modal -->
                    <div class="modal" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"  aria-hidden="true">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h4 class="modal-title" id="myModalLabel">
                                       <asp:Label runat="server" ID="LblForm" Text="Tambah" /> Inventaris</h4>
                                </div>
                                <div class="modal-body">
                                        <div class="form-group">
                                          <label >Nama Inventaris</label>
                                          <asp:HiddenField ID="HiddenIdInventaris" runat="server" Value="0" />
                                          <asp:DropDownList ID="DDLInventaris"  runat="server" CssClass="form-control" />
                                            </div>
                                             <div class="form-group">
                                              <label >Tipe</label>
                                              <asp:DropDownList ID="DDLType"  runat="server" CssClass="form-control" >
                                                <asp:ListItem Text="Kerusakan" Value="1" />
                                                <asp:ListItem Text="Kehilangan" Value="2" />
                                              </asp:DropDownList>
                                            </div>
                                             <div class="form-group">
                                              <label >Jumlah</label>
                                              <asp:TextBox ID="TbQty" Text=""  runat="server" CssClass="form-control" />
                                            </div>
                                             <div class="form-group">
                                              <label >Catatan</label>
                                              <asp:TextBox ID="TbCatatanInventaris" TextMode="MultiLine"  runat="server" CssClass="form-control" />
                                            </div>
                                  
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-default" data-dismiss="modal">
                                        Close</button>
                                 <asp:Button ID="BtnSimpanInventaris" OnClick="SimpanInventaris_Click"  runat="server" CssClass="btn btn-primary" Text="Simpan" />

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
    $('#m_pencatat_pemeliharaan').addClass('active');
    

    function closeAddInvModal() {
        //  alert("Hallo");
        $('.modal-backdrop').remove();
        $('#myModal').modal('hide');
    }

    function alertSukses() {
        alert("Data Sukses Diajukan");
    }

    function alertSuksesDelete() {
        alert("Pengajuan Sukses Dihapus");
    }

    function showModalInventaris() {
        $(".modal-backdrop").remove();
        $("#myModal").modal("show");
        //   initFunction();
    }

    function noAddInventaris() {
        alert("Tidak ada data inventaris yang dapat ditambahkan");
    }

</script>
</asp:Content>

