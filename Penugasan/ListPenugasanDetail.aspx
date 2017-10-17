<%@ Page Language="C#" MasterPageFile="~/Template.master" AutoEventWireup="true" CodeFile="ListPenugasanDetail.aspx.cs" Inherits="Penugasan_ListPenugasanTeknisi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="NavContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" Runat="Server">
    <h1>
            Detail Penugasan Teknisi
            
    </h1>
    <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-user"></i>List Penugasan</a></li>
            <li class="active">Detail Penugasan</li>
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
                      <asp:HiddenField ID="HiddenIdItem" runat="server" />
                      <asp:HiddenField ID="HiddenStatusUbah" Value="Tambah" runat="server" />
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
                         CssClass="btn btn-success" Visible="false" runat="server" Text="Tambah Tugas Inventaris" 
                        onclick="BtnTambahTugas_Click"/>
                    <asp:Button ID="Button1" 
                         CssClass="btn btn-info"  Visible="false" runat="server" Text="Tambah Pembelian" 
                        onclick="BtnTambahBeli_Click"/>
                        <asp:Button ID="Button2" 
                         CssClass="btn bg-purple"  Visible="false" runat="server" Text="Tambah Service" 
                        onclick="BtnTambahService_Click"/>
                  <asp:Button ID="BtnSimpanPenugasan"
                        CssClass="btn bg-navy"   Visible="false" runat="server" 
                        Text="Kirim Penugasan" OnClick="BtnSimpan_Click"  OnClientClick="return confirm('Anda yakin akan menyimpan data penerimaan?')" />
                  <asp:Button ID="BtnDraft"  Visible="false"  OnClick="BtnDraft_Click"  CssClass="btn btn-warning"  runat="server" Text="Simpan Draft"/>
                  <asp:Button ID="BtnDelete"  Visible="false" OnClick="BtnDeletePengajuan_Click"  
                        OnClientClick="return confirm('Anda yakin akan menghapus draft penugasan?')"  
                        CssClass="btn btn-danger"  runat="server" Text="Hapus" />
                  <asp:Button ID="BtnOK"
                        CssClass="btn bg-info"  Text="OK" runat="server"   OnClick="BtnOK_Click" />
                  
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
                                    <asp:BoundField DataField="jenis"  HeaderText="Jenis" />
                                    <asp:BoundField DataField="no_ref"  HeaderText="No Referensi" />
                                    <asp:BoundField DataField="judul_ref"  HeaderText="Judul" />
                                    <asp:BoundField DataField="summary"  HeaderText="Ringkasan" />
                                    <asp:BoundField DataField="status_desc" HeaderText="Status" Visible="true" />
                                    <asp:BoundField DataField="updated_date" HeaderText="Upd Date" Visible="true" />
                                    
                                 </Columns>
                                </asp:GridView>
                </div>
              
              </div><!-- /.box -->
    </section>
                    <!-- Modal Cari Data Inventaris -->
  
     <div class="modal" id="modalLain" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"  aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="H1">
                        <asp:Label runat="server" ID="LblForm" Text="Tambah"/> Tugas</h4>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label >Inventaris</label>
                        <asp:HiddenField ID="HIddenIdTugas" Value="0" runat="server" />
                       <asp:DropDownList ID="DDLInventaris" runat="server"  CssClass="form-control chzn-select" />
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                              ValidationGroup="ValGroup"
                                              ControlToValidate="DDLInventaris"
                                              ErrorMessage="Inventaris Harus diisi"
                                              ForeColor="Red"
                                              />
                                  
                   </div>
                    <div class="form-group">
                        <label >Jenis</label>
                        <asp:DropDownList ID="DDLJenisTugasInv"  runat="server" CssClass="form-control" >
                            <asp:ListItem Text="Perbaikan" Value="PERBAIKAN" />
                            <asp:ListItem Text="Pemasangan" Value="PEMASANGAN" />
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="ReqValJns" runat="server"
                                    ValidationGroup="ValGroup"
                                    ControlToValidate="DDLJenisTugasInv"
                                    ErrorMessage="Jumlah Harus diisi"
                                    ForeColor="Red"
                                    />
                   </div>
                <!--    <div class="form-group">
                        <label >Qty</label>
                        <asp:TextBox ID="TbTugasQty" Text="" TextMode="Number"  runat="server" CssClass="form-control" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                    ValidationGroup="ValGroup"
                                    ControlToValidate="TbTugasQty"
                                    ErrorMessage="Jumlah Harus diisi"
                                    ForeColor="Red"
                                    />
                   </div> -->
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
         
     
     <div class="modal" id="modalBeli" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"  aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="H2">
                        <asp:Label runat="server" ID="Label1" Text="Tambah"/> Pembelian</h4>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label >ID Pembelian-Judul</label>
                        <asp:HiddenField ID="HiddenField1" Value="0" runat="server" />
                       <asp:DropDownList ID="DDLPembelian" runat="server"  CssClass="form-control chzn-select" />
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                              ValidationGroup="BeliGroup"
                                              ControlToValidate="DDLPembelian"
                                              ErrorMessage="Pembelian Harus dipilih"
                                              ForeColor="Red"/>
                   </div>
                   
                    <div class="form-group">
                        <label >Catatan</label>
                        <asp:TextBox ID="TbCatatanBeli" Text="" TextMode="MultiLine"  runat="server" CssClass="form-control" />
                     </div>
                     
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        Close</button>
                    <asp:Button ID="Button3" ValidationGroup="BeliGroup" onclick="SimpanBeli_Click" runat="server" CssClass="btn btn-primary" Text="Simpan" />

                    <%--<button type="button"  class="btn btn-primary">
                        Save changes</button>--%>
                    </div>
            </div>
        </div>
    </div>
                
   
          <div class="modal" id="modalService" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"  aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="H3">
                        <asp:Label runat="server" ID="Label2" Text="Tambah"/> Tugas Service</h4>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label >ID Service-Judul</label>
                        <asp:HiddenField ID="HiddenField2" Value="0" runat="server" />
                       <asp:DropDownList ID="DDLService" runat="server"  CssClass="form-control chzn-select" />
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                              ValidationGroup="ServiceGroup"
                                              ControlToValidate="DDLService"
                                              ErrorMessage="Pembelian Harus dipilih"
                                              ForeColor="Red"/>
                                  
                   </div>
                   
                    <div class="form-group">
                        <label >Catatan</label>
                        <asp:TextBox ID="TbCatatanService" Text="" TextMode="MultiLine"  runat="server" CssClass="form-control" />
                     </div>
                     
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        Close</button>
                    <asp:Button ID="Button4" ValidationGroup="BeliGroup" onclick="SimpanService_Click" runat="server" CssClass="btn btn-primary" Text="Simpan" />

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

    $('#m_kabag_penugasan').addClass('active');
    $('#m_kabag_penugasan_list').addClass('active');


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

    function showModalBeli() {
        $(".modal-backdrop").remove();
        $("#modalBeli").modal("show");
        //   initFunction();
    }

    function hideModalBeli() {
        $(".modal-backdrop").remove();
        $("#modalBeli").modal("hide");
        activeTab2();
        //   initFunction();
    }

    function showModalService() {
        $(".modal-backdrop").remove();
        $("#modalService").modal("show");
        //   initFunction();
    }

    function hideModalService() {
        $(".modal-backdrop").remove();
        $("#modalService").modal("hide");
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

