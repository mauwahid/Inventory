<%@ Page Title="" Language="C#" MasterPageFile="~/Template.master" AutoEventWireup="true" CodeFile="Approval.aspx.cs" Inherits="Pimpinan_Approval" %>




<asp:Content ID="Content1" ContentPlaceHolderID="NavContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" Runat="Server">
    <h1>
           Approval Pengajuan Service
            
    </h1>
    <ol class="breadcrumb">
            <li class="active">Detail Pengajuan Service</li>
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
                      <td>Tanggal Pengajuan</td>
                      <td><asp:HiddenField ID="HiddenIDPengajuan" runat="server" /> <asp:Label runat="server" ID="LblTglPengajuan" /></td>
                       <td>Pengaju</td>
                      <td><asp:Label runat="server" ID="LblKabag" /></td>
                       </tr>
                    <tr>
                      <td>ID Pengajuan</td>
                      <td><asp:Label runat="server" ID="LblIDPengajuan" /></td>
                      </tr>
                    <tr>
                      <td>Judul Pengajuan</td>
                      <td><asp:Label runat="server" ID="LblJudulPengajuan" /></td>
                      <td>Catatan Kabag</td>
                      <td><asp:Label runat="server" ID="LblMemoKabag" /></td>
                    </tr>
                     <tr>
                      <td>Tindakan</td>
                      <td><asp:Button CssClass="btn btn-primary" runat="server" ID="BtnSetuju" 
                              OnClientClick="return confirm('Anda yakin menyetujui pengajuan ini?');" 
                              Text="Disetujui" onclick="BtnSetuju_Click" />&nbsp 
                          <asp:Button CssClass="btn btn-danger" runat="server" 
                              OnClientClick="return confirm('Anda yakin menolak pengajuan ini?');" ID="BtnRejected" 
                              Text="Ditolak" onclick="BtnRejected_Click" /> </td>
                      <td>Catatan:</td>
                      <td><asp:TextBox runat="server" ID="TbCatatan" TextMode="MultiLine" CssClass="form-control"/></td>
                    </tr>
                    
                  </table>
                </div><!-- /.box-body -->
                <div class="box-footer clearfix">
                   <asp:Button ID="BtnCancel" UseSubmitBehavior="false"   CssClass="btn btn-warning"  
                        runat="server" Text="Batalkan" onclick="BtnCancel_Click"/>
                
                </div>
              </div><!-- /.box -->
       
              <div class="box box-primary">
                <div class="box-header">
                  <h3 class="box-title"></h3>
                </div>
                <div class="box-body">
                      <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                                   DataKeyNames="id_pengajuan_service"   AllowPaging="true"
                                   ShowHeaderWhenEmpty="true"
                                   CssClass="table table-bordered" 
                                   OnPageIndexChanging="GView_PageIndexChanging" >
                                <PagerStyle CssClass="gridview" />
                                <Columns>
                                    <asp:BoundField DataField="id_pengajuan_service" HeaderText="Id" Visible="true" />
                                     <asp:BoundField DataField="id_pengajuan_service" HeaderText="Id Pengajuan" Visible="false" />
                                    <asp:BoundField DataField="nama_inventaris" HeaderText="Nama Inventaris" />
                                    <asp:BoundField DataField="qty" HeaderText="Jumlah" />
                                    <asp:BoundField DataField="biaya_service" HeaderText="Harga Total" />
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
    $('#m_pimpinan_pengajuan').addClass('active');
    $('#m_pimpinan_service_baru').addClass('active');


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




