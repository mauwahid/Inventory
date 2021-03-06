﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Template.master" AutoEventWireup="true" CodeFile="PenerimaanDetail.aspx.cs" Inherits="Pengadaan_PenerimaanDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="NavContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" Runat="Server">
    <h1>
            Detail Penerimaan Inventaris
            
    </h1>
    <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-user"></i>Penerimaan Inventaris</a></li>
            <li class="active">Detail Penerimaan Inventaris</li>
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
                      <td>ID</td>
                      <td><asp:Label runat="server" ID="LblID" /></td>
                      <td>Tanggal Penerimaan</td>
                      <td><asp:HiddenField ID="HiddenIDPenerimaan" runat="server" />
                      <asp:HiddenField ID="HiddenIDPengajuan" runat="server" /> 
                      <asp:HiddenField ID="HiddenType" runat="server" />
                      <asp:Label runat="server" ID="LblTglPenerimaan" /></td>
                     
                    </tr>
                    <tr>
                    <td>Judul Penerimaan</td>
                      <td><asp:Label runat="server" ID="LblJudulPenerimaan" /></td>
                        <td>Referensi Pembelian/Service</td>
                      <td><asp:Label runat="server" ID="LblRefBeliService" /></td>
                      </tr>
                     </tr>
                    <tr>
                       <td>Penerima</td>
                      <td><asp:Label runat="server" ID="LblPenerima" /></td>
                     <td>Referensi Pengajuan</td>
                      <td><asp:Label runat="server" ID="LblRefPengajuan" /></td>
                      </tr>
                    <tr>
                    <td></td><td></td>
                     <td>Keterangan</td>
                      <td><asp:Label runat="server" ID="LblKeterangan" /></td>
                     </tr>
                    
                  </table>
                </div><!-- /.box-body -->
                <div class="box-footer clearfix">
                  <asp:Button ID="BtnTambah" 
                         CssClass="btn btn-primary" runat="server" Text="Tambah Inventaris" 
                        onclick="BtnTambah_Click"/>
                  <asp:Button ID="BtnSimpanPengajuan"
                        CssClass="btn btn-info"  runat="server" 
                        Text="Simpan Penerimaan" onclick="SimpanPenerimaan_Click"  OnClientClick="return confirm('Anda yakin akan menyimpan data penerimaan?')" />
                   <asp:Button ID="BtnDeletePenerimaan"   
                        OnClientClick="return confirm('Anda yakin akan membatalkan penerimaan?')"  
                        CssClass="btn btn-danger"  runat="server" Text="Batal" 
                        onclick="BtnDeletePenerimaan_Click"/>
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
                                   OnPageIndexChanging="GView_PageIndexChanging" >
                                <PagerStyle CssClass="gridview" />
                                <Columns>
                                    <asp:BoundField DataField="id" HeaderText="Id" Visible="false" />
                                    <asp:BoundField DataField="id_pengajuan" Visible="false" HeaderText="Id Pengajuan"  />
                                    <asp:BoundField DataField="nama_inventaris" HeaderText="Nama Inventaris" Visible="true" />
                                    <asp:BoundField DataField="id_inventaris" HeaderText="Id Inventaris" Visible="true"/>
                                    <asp:BoundField DataField="qty" HeaderText="Jumlah Pengajuan" />
                                    <asp:BoundField DataField="catatan_kabag" HeaderText="Catatan Pengajuan" />
                                     <asp:TemplateField SortExpression="qtyDiterima">
                                            <HeaderTemplate>
                                                <asp:Label ID="Label1" runat="server" Text="Jumlah Diterima" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="TbQtyDiterima"  CssClass="form-control" runat="server" />
                                            </ItemTemplate>
                                     </asp:TemplateField>
                                    <asp:TemplateField SortExpression="Catatan">
                                             <HeaderTemplate>
                                                <asp:Label ID="Label1" runat="server"  Text="Catatan Penerimaan" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="TbCatatan" TextMode="MultiLine" CssClass="form-control" runat="server" />
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
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-default" data-dismiss="modal">
                                        Close</button>
                                  <asp:Button ID="BtnSimpanInventaris" ValidationGroup="ValGroup" onclick="SimpanInventaris_Click"    runat="server" CssClass="btn btn-primary" Text="Simpan" />
                               
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
    $('#m_kabag_penerimaan').addClass('active');
    $('#m_kabag_penerimaan_barang').addClass('active');



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



