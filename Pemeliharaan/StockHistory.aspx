<%@ Page Title="" Language="C#" MasterPageFile="~/Template.master" AutoEventWireup="true" CodeFile="StockHistory.aspx.cs" Inherits="Pemeliharaan_StockHistory" %>


<asp:Content ID="Content1" ContentPlaceHolderID="NavContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" Runat="Server">
    <h1>
            History Stock ID Jenis Inventaris : <asp:Label ID="lblIdJenisInv" runat="server" />
            
    </h1>
    <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-user"></i>Pemeliharaan</a></li>
            <li class="active">Management Stock</li>
             <li class="active">History</li>
    </ol>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
      <form id="Form1" runat="server">
    <section class="content">
              <div class="box box-primary">
                <div class="box-header">
                  <h3 class="box-title"></h3>
                </div>
                <asp:HiddenField ID="HiddenId" runat="server" Value="" />
                <div class="box-body">
                      <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                                   DataKeyNames="id"   AllowPaging="true"
                                   ShowHeaderWhenEmpty="true"
                                   CssClass="table table-bordered" 
                                   OnPageIndexChanging="GView_PageIndexChanging">
                                <PagerStyle CssClass="gridview" />
                                <Columns>
                                    <asp:BoundField DataField="Id" HeaderText="Id" />
                                    <asp:BoundField DataField="nama_inventaris" HeaderText="Jenis Inventaris" />
                                    <asp:BoundField DataField="sumber" HeaderText="Sumber" />
                                    <asp:BoundField DataField="id_referensi" HeaderText="ID Sumber" />
                                    <asp:BoundField DataField="tipe" HeaderText="Tipe" />
                                    <asp:BoundField DataField="qty" HeaderText="Qty" />
                                   <asp:BoundField DataField="created_date" HeaderText="Tanggal" />
                                   
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