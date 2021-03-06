﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Template.master" AutoEventWireup="true" CodeFile="FormDetail.aspx.cs" Inherits="Pengadaan_FormDetail" %>



<asp:Content ID="Content1" ContentPlaceHolderID="NavContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" Runat="Server">
    <h1>
            Detail Service
            
    </h1>
    <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-server"></i>Draft Service</a></li>
            <li class="active">Detail Pengajuan Barang</li>
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
                      <td>Tanggal Pengajuan</td>
                      <td><asp:HiddenField ID="HiddenIDPengajuan" runat="server" /> <asp:Label runat="server" ID="LblTglPengajuan" /></td>
                       <td>Pengaju (Kabag)</td>
                      <td><asp:Label runat="server" ID="LblKabag" /></td>
                       </tr>
                    <tr>
                      <td>No Pengajuan</td>
                      <td><asp:Label runat="server" ID="LblNoPengajuan" /></td>
                      <td>Catatan Kabag</td>
                      <td><asp:Label runat="server" ID="LblMemoKabag" /></td>
                    
                    </tr>
                    <tr>
                      <td>Judul Pengajuan</td>
                      <td><asp:Label runat="server" ID="LblJudulPengajuan" /></td>
                      <td >Surat</td>
                      <td><asp:Label runat="server" ID="LblSurat" ForeColor="Red" Text="Surat Belum Diupload" />
                         </td>
                    </tr>
                   
                    <tr>
                        <td></td>
                        <td></td>
                        <td>Upload Surat
                        </td>
                        <td>
                        <asp:FileUpload ID="FileUploader" runat="server" ></asp:FileUpload>
                        <br />
                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary"  Text="Upload File Terpilih" OnClientClick="uploadFileDut();"  />
                        
                        </td>
                       
                    </tr>
                    

                    
                  </table>
                </div><!-- /.box-body -->
                <div class="box-footer clearfix">
                  <asp:Button ID="BtnTambah" 
                         CssClass="btn btn-primary" runat="server" Text="Tambah Inventaris" 
                        onclick="BtnTambah_Click"/>
                  <asp:Button ID="BtnSimpanPengajuan"
                        CssClass="btn btn-info"  runat="server" 
                        Text="Ajukan Ke Pimpinan" onclick="SimpanPengajuan_Click"  OnClientClick="return confirm('Anda yakin akan mengirim pengajuan?')" />
                  <asp:Button ID="BtnCancel" UseSubmitBehavior="false"  CssClass="btn btn-warning"  
                        runat="server" Text="Batal" onclick="BtnCancel_Click"/>
                  <asp:Button ID="BtnDeletePengajuan"   
                        OnClientClick="return confirm('Anda yakin akan menghapus draft pengajuan?')"  
                        CssClass="btn btn-danger"  runat="server" Text="Hapus Draft" 
                        onclick="BtnDeletePengajuan_Click"/>
                </div>
              </div><!-- /.box -->
       
              <div class="box box-primary">
                <div class="box-header">
                  <h3 class="box-title"></h3>
                </div>
                <div class="box-body">
                      <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                                   DataKeyNames="id_pengajuan_service_detail"   AllowPaging="true"
                                   ShowHeaderWhenEmpty="true"
                                   CssClass="table table-bordered" 
                                   OnRowCommand="RowCommand_Click"
                                   OnPageIndexChanging="GView_PageIndexChanging" >
                                <PagerStyle CssClass="gridview" />
                                <Columns>
                                    <asp:BoundField DataField="id_pengajuan_service" HeaderText="Id" Visible="true" />
                                    <asp:BoundField DataField="id_pengajuan_service_detail" HeaderText="Id" Visible="false" />
                                    <asp:BoundField DataField="nama_inventaris" HeaderText="Nama Inventaris" />
                                    <asp:BoundField DataField="qty" HeaderText="Jumlah" />
                                    <asp:BoundField DataField="biaya_service" HeaderText="Biaya" />
                                    <asp:BoundField DataField="catatan_kabag" HeaderText="Catatan" />
                                    <asp:TemplateField HeaderText="Edit">
                                         <ItemTemplate>
                                           <asp:LinkButton ID="LBEdit" 
                                             CommandArgument='<%# Eval("Id_pengajuan_service_detail") %>'
                                             CommandName="Ubah" runat="server">
                                             Ubah</asp:LinkButton>
                                         </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Edit">
                                         <ItemTemplate>
                                           <asp:LinkButton ID="LinkButton1" 
                                             CommandArgument='<%# Eval("Id_pengajuan_service_detail") %>'
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
                                    <h4 class="modal-title" id="myModalLabel">
                                       <asp:Label runat="server" ID="LblForm" Text="Tambah" /> Inventaris</h4>
                                </div>
                                <div class="modal-body">
                                        <div class="form-group">
                                          <label >Nama Inventaris</label>
                                          <asp:HiddenField ID="HiddenIdInventaris" runat="server" Value="0" />
                                          <asp:DropDownList ID="DDLInventaris" runat="server" CssClass="form-control chzn-select"/>
                                             <div class="form-group">
                                              <label >Jumlah</label>
                                              <asp:TextBox ID="TbQty" Text="" TextMode="Number"  runat="server" CssClass="form-control" />
                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                              ValidationGroup="ValGroup"
                                              ControlToValidate="TbQty"
                                              ErrorMessage="Jumlah Harus diisi"
                                              ForeColor="Red"
                                              />
                                  
                                            </div>
                                             <div class="form-group">
                                              <label >Biaya Service</label>
                                              <asp:TextBox ID="TbHargaTotal"  TextMode="Number"  runat="server" CssClass="form-control" />
                                            </div>
                                             <div class="form-group">
                                              <label >Catatan</label>
                                              <asp:TextBox ID="TbMemoKabag" TextMode="MultiLine"  runat="server" CssClass="form-control" />
                                            </div>
                                            
                                  
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
             <asp:Button ID="BtnUpload" style="display:none;"  runat="server" Text="Test" OnClick="ButtonUpd_Click" />
            <asp:Button runat="server" style="display:none;" ID="BtnDownload" 
                               onclick="Button1_Click" />
    </form>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="JsContent" Runat="Server">
<script type="text/javascript">

    $('ul li').removeClass('active');
    $('#m_kabag_service').addClass('active');
    $('#m_kabag_service_draft').addClass('active');

           


       

        function closeAddInvModal() {
          //  alert("Hallo");
            $('.modal-backdrop').remove();
            $('#myModal').modal('hide');
        }
        function uploadFileDut() {
            $("#MainContent_BtnUpload").click();
        }

        function downloadFile() {
            $("#MainContent_BtnDownload").click();
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

         function pageLoad() {
             $(".chzn-select").chosen();
             $(".chzn-select-deselect").chosen({ allow_single_deselect: true });

             $("#MainContent_TbQty").keyup(function () {
                 var harga = $("#MainContent_DDLHarga").val();
                 var jml = $("#MainContent_TbQty").val();
                 var totHarga = harga * jml;
                 $("#MainContent_TbHargaTotal").val(totHarga);
             })
         }

</script>
</asp:Content>




