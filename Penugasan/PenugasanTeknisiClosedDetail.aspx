<%@ Page Title="" Language="C#" MasterPageFile="~/Template.master" AutoEventWireup="true" CodeFile="PenugasanTeknisiClosedDetail.aspx.cs" Inherits="Penugasan_OnProgressTeknisiDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="NavContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" Runat="Server">
    <h1>
            Detail Penugasan Baru
            
    </h1>
    <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-user"></i>Penugasan</a></li>
            <li class="active">Detail Penugasan Baru</li>
    </ol>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
     <form id="Form1" runat="server" defaultbutton="BtnOK" >
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
                      <td>Catatan</td>
                      <td><asp:Label runat="server" ID="LblCatatan" /></td>
                      </tr>
                    
                    
                  </table>
                </div><!-- /.box-body -->
                <div class="box-footer clearfix">
                  <asp:Button ID="BtnOK" 
                         CssClass="btn btn-primary" runat="server" Text="OK"
                         onclick="BtnSelesai_Click"/>
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
                                    <asp:BoundField DataField="judul"  HeaderText="Judul" />
                                    <asp:BoundField DataField="Catatan" HeaderText="Catatan" Visible="true" />
                                    <asp:BoundField DataField="Qty" HeaderText="Qty" Visible="true" />
                                    <asp:BoundField DataField="Status" HeaderText="Status" Visible="true" />
                                    <asp:BoundField DataField="Catatan_Teknisi" HeaderText="Catatan Teknisi" Visible="true" />
                                     
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
                        <asp:Label runat="server" ID="LblForm" Text="Tambah" />Tugas Lain</h4>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label >Judul</label>
                        <asp:HiddenField ID="HIddenIdTugasLain" Value="0" runat="server" />
                        <asp:TextBox ID="TbJudul" Text=""  Enabled="false" runat="server" CssClass="form-control" />
                   </div>
                    <div class="form-group">
                        <label >Qty</label>
                        <asp:TextBox ID="TbPopQty" Text="" Enabled="false"  runat="server" CssClass="form-control" />
                     </div>
                    <div class="form-group">
                        <label >Catatan</label>
                        <asp:TextBox ID="TbCatatan" Text="" Enabled="false" TextMode="MultiLine"  runat="server" CssClass="form-control" />
                     </div>
                     
                 <div class="form-group">
                        <label >Status</label>
                        <asp:DropDownList ID="DDLStatus" runat="server" CssClass="form-control">
                            <asp:ListItem Text="Baru" Value="0" />
                            <asp:ListItem Text="Sedang Dikerjakan" Value="1" />
                            <asp:ListItem Text="Selesai" Value="2" />
                            <asp:ListItem Text="Tidak bisa dikerjakan" Value="3" />
                        </asp:DropDownList>
                   </div>
                    <div class="form-group">
                        <label >Catatan Teknisi</label>
                        <asp:TextBox ID="TbCatatanTeknisi" Text="" TextMode="MultiLine"  runat="server" CssClass="form-control" />
                     </div>
                     
                 </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        Close</button>
                    <asp:Button ID="BtnSimpan" onclick="SimpanTugasLain_Click" runat="server" CssClass="btn btn-primary" Text="Simpan" />

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

    function hideModalInventaris() {
        $(".modal-backdrop").remove();
        $("#myModal").modal("hide");
        //   initFunction();
    }
    function noAddInventaris() {
        alert("Tidak ada data inventaris yang dapat ditambahkan");
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


</script>
</asp:Content>

