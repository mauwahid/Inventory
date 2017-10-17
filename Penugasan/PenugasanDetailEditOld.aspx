<%@ Page Title="" Language="C#" MasterPageFile="~/Template.master" AutoEventWireup="true" CodeFile="PenugasanDetailEditOld.aspx.cs" Inherits="Penugasan_PilihInventarisRusak" %>

<asp:Content ID="Content1" ContentPlaceHolderID="NavContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" Runat="Server">
    <h1>
            Detail Penugasan Teknisi
            
    </h1>
    <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-user"></i>Detail Pengadaan Barang</a></li>
            <li class="active">Detail Pengajuan Barang</li>
    </ol>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
     <form id="Form1" runat="server" defaultbutton="BtnTambahTugas" >
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
                      <td>Tanggal Penugasan</td>
                      <td><asp:HiddenField ID="HiddenIDPenugasan" runat="server" />
                      <asp:HiddenField ID="HiddenIdInventaris" runat="server" />
                      <asp:Label runat="server" ID="LblTglPenugasan" /></td>
                       <td>Pemberi Tugas (Kabag)</td>
                      <td><asp:Label runat="server" ID="LblPemberiTugas" /></td>
                       </tr>
                    <tr>
                      <td>Judul</td>
                      <td><asp:Label runat="server" ID="LblJudul" /></td>
                      </tr>
                    
                    
                  </table>
                </div><!-- /.box-body -->
                <div class="box-footer clearfix">
                   <asp:Button ID="BtnTambahTugas" 
                         CssClass="btn btn-success" runat="server" Text="Tambah Tugas" 
                        onclick="BtnTambahTugas_Click"/>

                  <asp:Button ID="BtnSimpanPenugasan"
                        CssClass="btn btn-info"  runat="server" 
                        Text="Kirim Penugasan" OnClick="BtnSimpan_Click"  OnClientClick="return confirm('Anda yakin akan menyimpan data penerimaan?')" />
                  <asp:Button ID="BtnCancel" onclick="BtnCancel_Click"  CssClass="btn btn-warning"  runat="server" Text="Batal"/>
                 
                </div>
              </div><!-- /.box -->
       
              <div class="box box-primary">
                <div class="box-header">
                  <h3 class="box-title"></h3>
                </div>
                <div class="box-body">
                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                                   DataKeyNames="Id"   AllowPaging="true"
                                   ShowHeaderWhenEmpty="true"
                                    OnRowCommand="RowCommand_Click"
                                   CssClass="table table-bordered" 
                                   OnPageIndexChanging="GView2_PageIndexChanging">
                                <PagerStyle CssClass="gridview" />
                                <Columns>
                                    <asp:BoundField DataField="Id" HeaderText="Id" Visible="true" />
                                    <asp:BoundField DataField="nama_inventaris"  HeaderText="Nama Inventaris" />
                                    <asp:BoundField DataField="qty"  HeaderText="Qty" />
                                    <asp:BoundField DataField="Catatan" HeaderText="Catatan" Visible="true" />
                                      <asp:TemplateField HeaderText="Ubah">
                                         <ItemTemplate>
                                           <asp:LinkButton ID="LinkButton2" 
                                             CommandArgument='<%# Eval("Id") %>'
                                             CommandName="UbahLain" runat="server">
                                             Ubah</asp:LinkButton>
                                         </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Hapus">
                                         <ItemTemplate>
                                           <asp:LinkButton ID="LinkButton1" 
                                             CommandArgument='<%# Eval("Id") %>'
                                             OnClientClick="return confirm('Yakin ingin dihapus?')" 
                                             CommandName="HapusLain" runat="server">
                                             Hapus</asp:LinkButton>
                                         </ItemTemplate>
                                    </asp:TemplateField>
                                   
                                 </Columns>
                                </asp:GridView>
                    

                     		
                 
                </div><!-- /.box-body -->
              
              </div><!-- /.box -->
    </section>
                    <!-- Modal Cari Data Inventaris -->
  
     <div class="modal" id="modalLain" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"  aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="H1">
                        <asp:Label runat="server" ID="LblForm" Text="Tambah" /> Tugas</h4>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label >Inventaris</label>
                        <asp:HiddenField ID="HIddenIdTugas" Value="0" runat="server" />
                       <asp:DropDownList ID="DDLInventaris" runat="server"  CssClass="form-control chzn-select" />
                   </div>
                    <div class="form-group">
                        <label >Qty</label>
                        <asp:TextBox ID="TbTugasQty" Text="" TextMode="Number"  runat="server" CssClass="form-control" />
                        <asp:RequiredFieldValidator runat="server"
                            ControlToValidate="TbTugasQty"
                            ForeColor="Red"
                            ErrorMessage="Qty harus diisi"
                            ValidationGroup="ValGroup"
                            />
                   </div>
                    <div class="form-group">
                        <label >Catatan</label>
                        <asp:TextBox ID="TbCatatan" Text="" TextMode="MultiLine"  runat="server" CssClass="form-control" />
                     </div>
                     
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        Close</button>
                    <asp:Button ID="BtnSimpanJudul" ValidationGroup="ValGroup" onclick="SimpanTugasDetail_Click" runat="server" CssClass="btn btn-primary" Text="Simpan" />

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
    $('#li_tab1').addClass('active');

    $('ul li').removeClass('active');
    $('#m_kabag_penugasan').addClass('active');
    $('#m_kabag_penugasan_draft').addClass('active');


    function closeAddInvModal() {
        //  alert("Hallo");
        $('.modal-backdrop').remove();
        $('#myModal').modal('hide');
        activeTab1();
   
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

    function hideModalInventaris() {
        $(".modal-backdrop").remove();
        $("#myModal").modal("hide");
        //   initFunction();
    }

    function showModalTugasLain() {
        $(".modal-backdrop").remove();
        $("#modalLain").modal("show");
        //   initFunction();
    }

    function hideModalTugasLain() {
        $(".modal-backdrop").remove();
        $("#modalLain").modal("hide");
        activeTab2();
        //   initFunction();
    }

    function activeTab1() {
        $('#li_tab2').removeClass('active');
        $('#li_tab1').removeClass('active');
        $('#li_tab1').addClass('active');
   
    }

    function activeTab2() {
        $('#li_tab1').removeClass('active');
        $('#li_tab2').removeClass('active');
        $('#li_tab2').addClass('active');
       
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
