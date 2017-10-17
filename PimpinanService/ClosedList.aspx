<%@ Page Title="" Language="C#" MasterPageFile="~/Template.master" AutoEventWireup="true" CodeFile="ClosedList.aspx.cs" Inherits="Kabag_ListTersetujui" %>
<asp:Content ID="Content1" ContentPlaceHolderID="NavContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" Runat="Server">
    <h1>
            Pengajuan Service
            
    </h1>
    <ol class="breadcrumb">
            <li class="active">Pengajuan Service</li>
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
                    <div class="col-md-2 col-md-offset-4">
                        <div class="input-group input-group-sm">
                        <asp:DropDownList CssClass="form-control" ID="DDLType" runat="server" >
                            <asp:ListItem Value="0" Selected="True" Text="All" />
                            <asp:ListItem Value="1" Text="Disetujui" />
                            <asp:ListItem Value="2" Text="Ditolak" />
                        </asp:DropDownList>
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
                                   DataKeyNames="Id_pengajuan_service"   AllowPaging="true"
                                   ShowHeaderWhenEmpty="true"
                                   CssClass="table table-bordered" 
                                   OnRowCommand="RowCommand_Click"
                                   OnPageIndexChanging="GView_PageIndexChanging"
                                   >
                                <PagerStyle CssClass="gridview" />
                                <Columns>
                                    <asp:BoundField DataField="id_pengajuan_service" HeaderText="Id" />
                                    <asp:BoundField DataField="judul_pengajuan_service" HeaderText="Judul" />
                                    <asp:BoundField DataField="catatan_kabag" HeaderText="Catatan" />
                                    <asp:BoundField DataField="kabag" HeaderText="Pengaju" />
                                    <asp:BoundField DataField="created_date" HeaderText="Tanggal Dibuat" />
                                    <asp:BoundField DataField="approval_date" HeaderText="Tanggal Persetujuan" />
                                    <asp:BoundField DataField="status_approval_ket" HeaderText="Status" />
                                    <asp:BoundField DataField="pimpinan" HeaderText="Approver" />
                                    <asp:BoundField DataField="catatan_pimpinan" HeaderText="Catatan Pimpinan" />
                                     <asp:TemplateField HeaderText="Detail">
                                         <ItemTemplate>
                                           <asp:LinkButton ID="LinkButton1" 
                                             CommandArgument='<%# Eval("Id_pengajuan_service") %>'
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
    $('#m_pimpinan_service_closed').addClass('active');


</script>
</asp:Content>
