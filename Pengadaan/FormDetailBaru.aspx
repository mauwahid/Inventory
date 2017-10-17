﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Template.master" AutoEventWireup="true" CodeFile="FormDetailBaru.aspx.cs" Inherits="Pengadaan_FormDetail" %>


<asp:Content ID="Content1" ContentPlaceHolderID="NavContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" Runat="Server">
    <h1>
            Detail Pengajuan
          
    </h1>
    <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-server"></i>Tambah Pengajuan</a></li>
            <li class="active">Detail Pengajuan</li>
    </ol>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
     <form id="Form1" runat="server" defaultbutton="BtnTambah" >
     <asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel runat="server">
    <ContentTemplate>
       
    <section class="content">
                     <div class="box">
                <div class="box-header">
                  <h3 class="box-title"></h3>
                </div><!-- /.box-header -->
                <div class="box-body no-padding">
                  <table class="table table-striped" width="100%">
                    <tr>
                      <td>ID</td>
                      <td><asp:Label runat="server" ID="LblID" /></td>
                       <td>Prioritas</td>
                      <td><asp:Label runat="server" ID="LblPrioritas" /></td>
                     </tr>
                    <tr>
                      <td>Tanggal Pengajuan</td>
                      <td><asp:HiddenField ID="HiddenIDPengajuan" runat="server" /> <asp:Label runat="server" ID="LblTglPengajuan" /></td>
                       <td>Pengaju (Kabag)</td>
                      <td><asp:Label runat="server" ID="LblKabag" /></td>
                    </tr>
                    <tr>
                      <td>Judul Pengajuan</td>
                      <td><asp:Label runat="server" ID="LblJudulPengajuan" /></td>
                      <td>Catatan Kabag</td>
                      <td><asp:Label runat="server" ID="LblMemoKabag" /></td>
                    
                      </tr>
                    <tr>
                        <td>Biaya Total (Rp)</td>
                        <td><asp:Label runat="server" ID="LblBiayaTotal" Text="0" /></td>
                        <td>Surat</td>
                      <td><asp:Label runat="server" ID="LblSurat" ForeColor="Red" Text="Surat Belum Diupload" />
                         </td>
                    </tr>
                   
                    <tr>
                     <td></td>
                        <td></td>
                       
                    <td>Upload Surat
                        </td>
                        <td>
                        <asp:FileUpload ID="FileUpload1"  runat="server" ></asp:FileUpload><br />
                        <asp:Button runat="server" CssClass="btn btn-primary"  Text="Upload File Terpilih" OnClientClick="uploadFile();"  />
                        
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
                  <asp:Button ID="BtnCancel"  CssClass="btn btn-warning"  
                        runat="server" Text="Simpan Sbg Draft" onclick="BtnCancel_Click"/>
                  <asp:Button ID="BtnDeletePengajuan"   
                        OnClientClick="return confirm('Anda yakin akan menghapus pengajuan?')"  
                        CssClass="btn btn-danger"  runat="server" Text="Batal" 
                        onclick="BtnDeletePengajuan_Click"/>
                </div>
              </div><!-- /.box -->
       
              <div class="box box-primary">
                <div class="box-header">
                  <h3 class="box-title"></h3>
                </div>
                <div class="box-body">
                      <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                                   DataKeyNames="id_pengajuan_detail"  AllowPaging="true"
                                   ShowHeaderWhenEmpty="true"
                                   CssClass="table table-bordered" 
                                   OnRowCommand="RowCommand_Click"
                                   OnPageIndexChanging="GView_PageIndexChanging" >
                                <PagerStyle CssClass="gridview" />
                                <Columns>
                                 <asp:TemplateField HeaderText="No.">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                    <asp:BoundField DataField="id_pengajuan_detail" HeaderText="Id" Visible="false" />
                                    <asp:BoundField DataField="id_pengajuan" HeaderText="Id Pengajuan" Visible="false" />
                                    <asp:BoundField DataField="nama_inventaris" HeaderText="Nama Inventaris" />
                                    <asp:BoundField DataField="qty" HeaderText="Jumlah" />
                                    <asp:BoundField DataField="harga_satuan" HeaderText="Harga Satuan" />
                                    <asp:BoundField DataField="harga_total" HeaderText="Harga Total" />
                                    <asp:BoundField DataField="catatan_kabag" HeaderText="Catatan" />
                                    <asp:TemplateField HeaderText="Edit">
                                         <ItemTemplate>
                                           <asp:LinkButton ID="LBEdit" 
                                             CommandArgument='<%# Eval("id_pengajuan_detail") %>'
                                             CommandName="Ubah" runat="server">
                                             Ubah</asp:LinkButton>
                                         </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Hapus">
                                         <ItemTemplate>
                                           <asp:LinkButton ID="LinkButton1" 
                                             CommandArgument='<%# Eval("id_pengajuan_detail") %>'
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
                                          <asp:DropDownList ID="DDLInventaris" OnSelectedIndexChanged="DDLInv_SelectedIndexChanged"  runat="server" CssClass="form-control chzn-select" AutoPostBack="true" />
                                            </div>
                                             <div class="form-group">
                                              <label >Harga Satuan</label>
                                             <asp:DropDownList ID="DDLHarga" Enabled="false" runat="server" CssClass="form-control"/>
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                              ValidationGroup="ValGroup"
                                              ControlToValidate="DDLHarga"
                                              ErrorMessage="Nama Inventaris Harus diisi"
                                              ForeColor="Red"
                                              />
                                            </div>
                                            
                                             <div class="form-group">
                                              <label >Harga Total</label>
                                              <asp:TextBox ID="TbHargaTotal" Enabled="false" TextMode="Number"  runat="server" CssClass="form-control" />
                                            </div>
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
            <asp:Button ID="BtnUploadHide" runat="server" style="display:none;" Text="Test" OnClick="Button2_Click" />
            <asp:Button runat="server" style="display:none;" ID="BtnDownload" 
                               onclick="Button1_Click" />
    </form>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="JsContent" Runat="Server">
<script type="text/javascript">

    $('ul li').removeClass('active');
    $('#m_kabag_pengajuan').addClass('active');
    $('#m_kabag_pengajuan_form').addClass('active');

    function uploadFile() {
        $("#MainContent_BtnUploadHide").click();
    }

    function downloadFile() {
        $("#MainContent_BtnDownload").click();
    }
       

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




