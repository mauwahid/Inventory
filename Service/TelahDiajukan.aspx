<%@ Page Title="" Language="C#" MasterPageFile="~/Template.master" AutoEventWireup="true" CodeFile="TelahDiajukan.aspx.cs" Inherits="Pengadaan_TelahDiajukan" %>


<asp:Content ID="Content1" ContentPlaceHolderID="NavContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" Runat="Server">
    <h1>
          Service Yang telah diajukan
            
    </h1>
    <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-server"></i>List Service</a></li>
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
                      <div class="row">
                        <div class="col-md-4 col-md-offset-8">
                                <div class="form-inline">
                                    <div class="form-group">
                                        <asp:Label ID="Label1" Text="Sort By" runat="server" />
                                    </div>
                                    <div class="form-group">
                                         <asp:DropDownList ID="DDLUrutkan" CssClass="form-control" runat="server" 
                                             onselectedindexchanged="DDLUrutkan_SelectedIndexChanged"  AutoPostBack="true">
                                            <asp:ListItem Text="ID" Value="ID"></asp:ListItem>
                                            <asp:ListItem Text="Tanggal Dibuat" Value="created_date"></asp:ListItem>
                                            <asp:ListItem Text="Tanggal Persetujuan" Value="approval_date"></asp:ListItem>
                                            <asp:ListItem Text="Status" Value="status_approval_ket"></asp:ListItem>
                                        </asp:DropDownList>
                                    
                                    </div>
                                    <div class="form-group">
                                      <asp:DropDownList ID="DDLASCDESC" CssClass="form-control" runat="server" 
                                            onselectedindexchanged="DDLASCDESC_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Text="ASC" Value="ASC"></asp:ListItem>
                                        <asp:ListItem Text="DESC" Selected="True" Value="DESC"></asp:ListItem>
                                        </asp:DropDownList>  
                                    </div>
                       
                                </div>
                                    </div>
                                  
                                  </div>
                      <div class="row">
                             <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                                   DataKeyNames="id_pengajuan_service"   AllowPaging="true"
                                   ShowHeaderWhenEmpty="true"
                                   CssClass="table table-bordered" 
                                   OnRowCommand="RowCommand_Click" 
                                   OnPageIndexChanging="GView_PageIndexChanging"
                                   >
                                <PagerStyle CssClass="gridview" />
                                <Columns>
                                    <asp:BoundField DataField="id_pengajuan_service" HeaderText="Id" SortExpression="Id" Visible="false" />
                                    <asp:BoundField DataField="id_pengajuan_service" HeaderText="Id" SortExpression="id_service" />
                                    <asp:BoundField DataField="judul_pengajuan_service" HeaderText="Judul" />
                                    <asp:BoundField DataField="catatan_kabag" HeaderText="Catatan Pengaju" />
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
							
                       
                      </div>
                </div><!-- /.box-body -->
              
              </div><!-- /.box -->
    </section>
    </form>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="JsContent" Runat="Server">
<script type="text/javascript">

    $('ul li').removeClass('active');
    $('#m_kabag_service').addClass('active');
    $('#m_kabag_service_telah').addClass('active');

</script>
</asp:Content>


