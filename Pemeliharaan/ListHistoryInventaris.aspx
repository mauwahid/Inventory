<%@ Page Title="" Language="C#" MasterPageFile="~/Template.master" AutoEventWireup="true" CodeFile="ListHistoryInventaris.aspx.cs" Inherits="Pemeliharaan_ListHistoryInventaris" %>
<asp:Content ID="Content1" ContentPlaceHolderID="NavContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" Runat="Server">
    <h1>
            Pilih Riwayat Inventaris <small>Step 4</small>
            
    </h1>
    <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-user"></i>Pemeliharaan</a></li>
            <li class="active">Pilih Riwayat Inventaris</li>
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
                    <div></div>
                </div>
               <div class="row">
                 <div class="col-md-6">
                        <asp:Button CssClass="btn btn-info"  runat="server" Text="Refresh" 
                            ID="btnRefresh" />
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
                <asp:HiddenField ID="HiddenId" runat="server" Value="1" />
                <div class="box-body">
                      <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                                   DataKeyNames="Id"   AllowPaging="true"
                                   ShowHeaderWhenEmpty="true"
                                   CssClass="table table-bordered" 
                                   OnRowCommand="RowCommand_Click"
                                   OnPageIndexChanging="GView_PageIndexChanging">
                                <PagerStyle CssClass="gridview" />
                                <Columns>
                                    <asp:BoundField DataField="Id" HeaderText="Id" />
                                    <asp:BoundField DataField="NamaInventaris" HeaderText="Nama Riwayat Inventaris" />
                                    <asp:BoundField DataField="JenisRiwayat Inventaris" HeaderText="Jenis Riwayat Inventaris" />
                                    <asp:BoundField DataField="QtyTersedia" HeaderText="Jmh Riwayat Inventaris Tersedia" />
                                    <asp:BoundField DataField="QtyHilang" HeaderText="Jml Riwayat Inventaris Hilang" />
                                    <asp:BoundField DataField="QtyRusak" HeaderText="Jml Riwayat Inventaris Rusak" />
                                    <asp:BoundField DataField="Keterangan" HeaderText="Keterangan" />
                                    <asp:TemplateField HeaderText="">
                                         <ItemTemplate>
                                           <asp:LinkButton ID="LinkButton1" 
                                             CommandArgument='<%# Eval("Id") %>'
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
    $('#m_pencatat_pemeliharaan').addClass('active');
    $('#m_pencatat_pemeliharaan_stock').addClass('active');


</script>
</asp:Content>



