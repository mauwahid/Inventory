<%@ Page Title="" Language="C#" MasterPageFile="~/Template.master" AutoEventWireup="true" CodeFile="ListLokasi.aspx.cs" Inherits="Pemeliharaan_ListLokasi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="NavContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" Runat="Server">
    <h1>
            Pilih Ruangan Untuk Pemeliharaan
            
    </h1>
    <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-user"></i>Pemeliharaan</a></li>
            <li class="active">Pilih Ruangan</li>
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
                        <div class="col-md-7 col-md-offset-5">
                            <div class="form-inline">
                            
                            <div class="form-group">
                                <asp:Label ID="Label1" Text="Gedung : " runat="server" />
                             </div>
                             <div class="form-group">
                                <asp:DropDownList ID="DDLGedung" CssClass="form-control" runat="server" 
                                     onselectedindexchanged="DDLGedung_SelectedIndexChanged" AutoPostBack="true"/>
                             </div>
                             <div class="form-group">
                                 <asp:Label ID="LblLantai" Text="Lantai : " runat="server" />
                             </div>
                              <div class="form-group">
                                <asp:DropDownList ID="DDLLantai" CssClass="form-control" runat="server" />
                             </div>
                             <div class="form-group">
                                 <asp:TextBox style="display:none;" CssClass="form-control" ID="TbCari" runat="server" />
                                 <asp:Button ID="btnCari" CssClass="btn btn-info btn-flat" runat="server" 
                                Text="Cari !" onclick="btnCari_Click" />
                             
                             </div>
                       
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
                                   DataKeyNames="Id_ruangan"   AllowPaging="true"
                                   ShowHeaderWhenEmpty="true"
                                   CssClass="table table-bordered" 
                                   OnRowCommand="RowCommand_Click"
                                   OnPageIndexChanging="GView_PageIndexChanging">
                                <PagerStyle CssClass="gridview" />
                                <Columns>
                                    <asp:BoundField DataField="Id_ruangan" HeaderText="Id" />
                                    <asp:BoundField DataField="nama_gedung" HeaderText="Nama Gedung" />
                                    <asp:BoundField DataField="lokasi_lantai" HeaderText="Lantai" />
                                    <asp:BoundField DataField="nama_ruangan" HeaderText="Nama Ruangan" />
                                    <asp:TemplateField HeaderText="">
                                         <ItemTemplate>
                                           <asp:LinkButton ID="LinkButton1" 
                                             CommandArgument='<%# Eval("Id_ruangan") %>'
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
    $('#m_pencatat_pemeliharaan_lokasi').addClass('active');


</script>
</asp:Content>
