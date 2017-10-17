<%@ Page Title="" Language="C#" MasterPageFile="~/Template.master" AutoEventWireup="true" CodeFile="PengajuanBaru.aspx.cs" Inherits="Kabag_ListPengajuan" %>


<asp:Content ID="Content1" ContentPlaceHolderID="NavContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" Runat="Server">
    <h1>
            Pengajuan Baru
            
    </h1>
    <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-user"></i>Persetujuan Pengajuan</a></li>
            <li class="active">Pengajuan baru</li>
    </ol>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
      <form id="Form1" runat="server" defaultbutton="btnCari">
    <section class="content">
              <div class="box box-primary">
                <div class="box-header">
                  <h3 class="box-title"></h3>
                </div>
                <div class="box-body">
                <div class="row">
                 <div class="col-md-6">
                        <asp:Button CssClass="btn btn-info"  runat="server" Text="Refresh" 
                            ID="btnRefresh" onclick="btnRefresh_Click" />
                  </div>
                    <div class="col-md-6">
                        <div class="input-group input-group-sm">
                        <asp:TextBox CssClass="form-control" ID="TbCari" runat="server" />
                        <span class="input-group-btn">
                            <asp:Button ID="btnCari" CssClass="btn btn-info btn-flat" runat="server" 
                                Text="Cari !" onclick="btnCari_Click" />
                        </span>
                        </div>
                    
                    </div>
                </div>
                </div><!-- /.box-body -->
                </div><!-- /.box -->
              <div class="box box-primary">
                <div class="box-header">
                  <h3 class="box-title"></h3>
                </div>
                <div class="box-body">
                      <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                                   DataKeyNames="id_pengajuan"   AllowPaging="true"
                                   ShowHeaderWhenEmpty="true"
                                   CssClass="table table-bordered" 
                                   OnRowCommand="RowCommand_Click"
                                   OnPageIndexChanging="GView_PageIndexChanging"
                                   >
                                <PagerStyle CssClass="gridview" />
                                <Columns>
                                    <asp:BoundField DataField="id_pengajuan" HeaderText="ID" />
                                    <asp:BoundField DataField="judul_pengajuan" HeaderText="Judul" />
                                    <asp:BoundField DataField="catatan_kabag" HeaderText="Keterangan" />
                                    <asp:BoundField DataField="kabag" HeaderText="Pengaju" />
                                    <asp:BoundField DataField="created_date" HeaderText="Tanggal Dibuat" />
                                    <asp:BoundField DataField="approval_date" HeaderText="Tanggal Persetujuan" />
                                    <asp:BoundField DataField="status_approval_ket" HeaderText="Status" />
                                    <asp:BoundField DataField="pimpinan" HeaderText="Approver" />
                                    <asp:BoundField DataField="catatan_pimpinan" HeaderText="Catatan Pimpinan" />
                                     <asp:TemplateField HeaderText="Surat">
                                         <ItemTemplate>
                                           <asp:LinkButton ID="LbSurat" 
                                             CommandArgument='<%# Eval("id_pengajuan") %>'
                                             CommandName="Surat" runat="server">
                                             Pilih</asp:LinkButton>
                                         </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Lampiran">
                                         <ItemTemplate>
                                           <asp:LinkButton ID="LBLampiran" 
                                             CommandArgument='<%# Eval("id_pengajuan") %>'
                                             tag-lampiran='<%# Eval("id_pengajuan") %>'
                                             CommandName="Lampiran" runat="server">
                                             Pilih</asp:LinkButton>
                                         </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Detail">
                                         <ItemTemplate>
                                           <asp:LinkButton ID="LinkButton1" 
                                             CommandArgument='<%# Eval("id_pengajuan") %>'
                                             CommandName="Pilih" runat="server">
                                             Pilih</asp:LinkButton>
                                         </ItemTemplate>
                                    </asp:TemplateField>
                                
                                 </Columns>
                                </asp:GridView>
							
                 
                </div><!-- /.box-body -->
              
              </div><!-- /.box -->
    </section>
    </form>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="JsContent" Runat="Server">
<script type="text/javascript">
    $('ul li').removeClass('active');
    $('#m_pimpinan_persetujuan').addClass('active');
    $('#m_pimpinan_persetujuan_baru').addClass('active');



    function bukaLampiran(obj) {
        var idLampiran = $(obj).attr('tag-lampiran');
        var url = "~/Laporan/ReportPengajuan.aspx?IdPengajuan=" + idLampiran;
        window.open(url, '_blank');
    }



</script>
</asp:Content>


