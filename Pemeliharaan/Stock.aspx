<%@ Page Title="" Language="C#" MasterPageFile="~/Template.master" AutoEventWireup="true" CodeFile="Stock.aspx.cs" Inherits="Pemeliharaan_Stock" %>

<asp:Content ID="Content1" ContentPlaceHolderID="NavContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" Runat="Server">
    <h1>
            Management Stock Inventaris
            
    </h1>
    <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-user"></i>Pemeliharaan</a></li>
            <li class="active">Management Stock</li>
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
                       <asp:Button CssClass="btn btn-primary"  runat="server" Text="Tambah Stock Inventaris" 
                            ID="Button1" onclick="btnAdd_Click" />
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
                <asp:HiddenField ID="HiddenId" runat="server" Value="" />
                <div class="box-body">
                      <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                                   DataKeyNames="id_jenis_inventaris"   AllowPaging="true"
                                   ShowHeaderWhenEmpty="true"
                                   CssClass="table table-bordered" 
                                   OnRowCommand="RowCommand_Click"
                                   OnPageIndexChanging="GView_PageIndexChanging">
                                <PagerStyle CssClass="gridview" />
                                <Columns>
                                    <asp:BoundField DataField="Id" HeaderText="Id" />
                                    <asp:BoundField DataField="id_inventaris" Visible="false" />
                                    <asp:BoundField DataField="nama_inventaris" HeaderText="Nama Inventaris" />
                                    <asp:BoundField DataField="qty_tersedia" HeaderText="Jmh Tersedia" />
                                    <asp:BoundField DataField="qty_terpakai" HeaderText="Jml Terpakai" />
                                    <asp:BoundField DataField="qty_maintenance" HeaderText="Jml Diperbaiki" />
                                    <asp:BoundField DataField="qty_rusak" HeaderText="Jml Rusak" />
                                    <asp:BoundField DataField="qty_hilang" HeaderText="Jml Hilang" />
                                     <asp:TemplateField HeaderText="Stock History">
                                         <ItemTemplate>
                                           <asp:LinkButton ID="lbHistory" 
                                             CommandArgument='<%# Eval("id_inventaris") %>'
                                              CommandName="StockHistory" runat="server">
                                             Pilih</asp:LinkButton>
                                         </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Adjustment">
                                         <ItemTemplate>
                                           <asp:LinkButton ID="lbAdjustment" 
                                             CommandArgument='<%# Eval("id_inventaris") %>'
                                              CommandName="Adjustment" runat="server">
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