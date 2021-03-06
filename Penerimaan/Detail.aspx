﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Template.master" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="Penerimaan_Detail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="NavContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" Runat="Server">
    <h1>
            Detail Penerimaan
            
    </h1>
    <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-server"></i>Penerimaan</a></li>
            <li class="active">Detail Penerimaan</li>
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
                  <table class="table table-striped" width="100%">
                    <tr>
                      <td>Tanggal Penerimaan</td>
                      <td><asp:HiddenField ID="HiddenIDPengajuan" runat="server" /> <asp:Label runat="server" ID="LblTglPengajuan" /></td>
                       <td>Penerima (Kabag)</td>
                      <td><asp:Label runat="server" ID="LblKabag" /></td>
                       </tr>
                    <tr>
                      <td>No Penerimaan</td>
                      <td><asp:Label runat="server" ID="LblNoPengajuan" /></td>
                      <td>Catatan</td>
                      <td><asp:Label runat="server" ID="LblCatatan" /></td>
                    
                      </tr>
                    <tr>
                      <td>Judul Penerimaan</td>
                      <td><asp:Label runat="server" ID="LblJudulPenerimaan" /></td>
                     <td >Nota</td>
                      <td><asp:Label runat="server" ID="LblNota" ForeColor="Red" Text="Nota Belum Diupload" />
                         </td>
                    </tr>
                   
                    <tr>
                        <td></td>
                        <td></td>
                    <td>Upload Nota
                        </td>
                        <td>
                        <asp:FileUpload ID="FileUpload1"  runat="server" ></asp:FileUpload><br />
                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary"  Text="Upload File Terpilih" OnClientClick="uploadFile();"  />
                        
                        </td>
                       
                    </tr>
                    
                  </table>
                </div><!-- /.box-body -->
                <div class="box-footer clearfix">
                   <asp:Button ID="Button2" 
                         CssClass="btn btn-primary" runat="server" Text="OK" />
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
                                 <asp:TemplateField HeaderText="No.">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                    <asp:BoundField DataField="id" HeaderText="Id" Visible="false" />
                                    <asp:BoundField DataField="nama_inventaris" HeaderText="Nama Inventaris" />
                                    <asp:BoundField DataField="qty_diterima" HeaderText="Jumlah" />
                                    <asp:BoundField DataField="catatan" HeaderText="Catatan" />
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
