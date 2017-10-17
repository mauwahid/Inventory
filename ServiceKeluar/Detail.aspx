<%@ Page Title="" Language="C#" MasterPageFile="~/Template.master" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="ServiceKeluar_Detail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="NavContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" Runat="Server">
    <h1>
            Detail Service Keluar
            
    </h1>
    <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-user"></i>Detail Service Barang</a></li>
            <li class="active">Detail Service Keluar</li>
    </ol>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
     <form id="Form1" runat="server" defaultbutton="BtnCancel" >
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
                      <td><asp:HiddenField ID="HiddenField1" runat="server" /> <asp:Label runat="server" ID="LblID" /></td>
                       <td>Judul</td>
                      <td><asp:Label runat="server" ID="LblJudul" /></td>
                     </tr>
                     <tr>
                      <td>Tanggal</td>
                      <td><asp:Label runat="server" ID="LblTglServices" /></td>
                       <td>Referensi Service</td>
                      <td><asp:Label runat="server" ID="LblRefService" /></td>
                    </tr>
                    <tr>
                      <td></td>
                      <td></td>
                      <td>Keterangan</td>
                      <td><asp:Label runat="server" ID="LblKeterangan" /></td>
                      </tr>
                    
                  </table>
                </div><!-- /.box-body -->
                <div class="box-footer clearfix">
                   <asp:Button ID="BtnCancel" UseSubmitBehavior="false"  CssClass="btn btn-info"  
                        runat="server" Text="OK" onclick="BtnCancel_Click"/>
                
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
                                   OnPageIndexChanging="GView_PageIndexChanging" >
                                <PagerStyle CssClass="gridview" />
                                <Columns>
                                    <asp:BoundField DataField="id_pengajuan_service_detail" HeaderText="Id" Visible="false" />
                                    <asp:BoundField DataField="id_pengajuan_service_detail" HeaderText="Id Pengajuan" Visible="false" />
                                    <asp:BoundField DataField="nama_inventaris" HeaderText="Nama Inventaris" />
                                    <asp:BoundField DataField="qty" HeaderText="Jumlah" />
                                    <asp:BoundField DataField="biaya_service" HeaderText="Biaya" />
                                    <asp:BoundField DataField="catatan_kabag" HeaderText="Catatan" />
                                   
                                 </Columns>
                                </asp:GridView>
							
                 
                </div><!-- /.box-body -->
              
              </div><!-- /.box -->
    </section>
                    <!-- Modal -->
                </ContentTemplate>
            </asp:UpdatePanel>
    </form>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="JsContent" Runat="Server">
<script type="text/javascript">

    $('ul li').removeClass('active');
    $('#m_kabag_service_keluar').addClass('active');
    $('#m_kabag_service_keluar_list').addClass('active');



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





