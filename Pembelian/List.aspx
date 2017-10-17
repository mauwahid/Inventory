<%@ Page Title="" Language="C#" MasterPageFile="~/Template.master" AutoEventWireup="true" CodeFile="List.aspx.cs" Inherits="Pembelian_List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="NavContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" Runat="Server">
    <h1>
            Pembelian Inventaris
            
    </h1>
    <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-user"></i>Pembelian Inventaris</a></li>
            <li class="active">List Pembelian</li>
    </ol>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
      <form id="Form1" runat="server" defaultbutton="btnCari">
           <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
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
                                   DataKeyNames="Id_pembelian"   AllowPaging="true"
                                   ShowHeaderWhenEmpty="true"
                                   CssClass="table table-bordered" 
                                   OnRowCommand="RowCommand_Click"
                                   OnPageIndexChanging="GView_PageIndexChanging"
                                   >
                                <PagerStyle CssClass="gridview" />
                                <Columns>
                                    <asp:BoundField DataField="id_pembelian" Visible="false" HeaderText="Id" />
                                    <asp:BoundField DataField="id_pembelian" HeaderText="ID" visible="true" />
                                    <asp:BoundField DataField="tanggal" HeaderText="Tanggal" />
                                    <asp:BoundField DataField="id_pengajuan" HeaderText="Referensi Pengajuan" />
                                    <asp:BoundField DataField="harga_total" HeaderText="Biaya Total" />
                                    <asp:BoundField DataField="keterangan" HeaderText="Keterangan" />
                                    <asp:BoundField DataField="status" HeaderText="Status" />
                                     <asp:TemplateField HeaderText="Ubah Status">
                                         <ItemTemplate>
                                           <asp:LinkButton ID="LbUbah" 
                                             CommandArgument='<%# Eval("id_pembelian") %>'
                                             CommandName="UbahStatus" runat="server">
                                             Pilih</asp:LinkButton>
                                         </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Detail">
                                         <ItemTemplate>
                                           <asp:LinkButton ID="LinkButton1" 
                                             CommandArgument='<%# Eval("id_pembelian") %>'
                                             CommandName="Pilih" runat="server">
                                             Pilih</asp:LinkButton>
                                         </ItemTemplate>
                                    </asp:TemplateField>
                                
                                
                                 </Columns>
                                </asp:GridView>
							
                 
                </div><!-- /.box-body -->
              
              </div><!-- /.box -->
    </section>
      <div class="modal" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"  aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="H1">
                        <asp:Label runat="server" ID="LblForm" Text="Ubah Status " /> Pembelian</h4>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label >ID Pembelian</label>
                        <asp:TextBox ID="TbNoItem" Text="" Enabled="false"  runat="server" CssClass="form-control" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="TbNoItem"
                        ErrorMessage="No harus diisi"
                        ValidationGroup="ValGroup"
                        ForeColor="Red"></asp:RequiredFieldValidator>
                   </div>
                   <div class="form-group">
                        <label >Status</label>
                        <asp:DropDownList ID="DDLStatus" runat="server" CssClass="form-control" >
                            <asp:ListItem Value="1" Text="Dalam Proses" />
                            <asp:ListItem Value="2" Text="Selesai" />
                            <asp:ListItem Value="3" Text="Tidak Selesai" />
                        </asp:DropDownList>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                        ControlToValidate="DDLStatus"
                        ErrorMessage="Status harus diisi"
                        ValidationGroup="ValGroup"
                        ForeColor="Red"></asp:RequiredFieldValidator>
                     </div>
                   
                 </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        Close</button>
                    <asp:Button ID="BtnSimpan" onclick="Save_Click" ValidationGroup="ValGroup" runat="server" CssClass="btn btn-primary" Text="Simpan" />

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
    $('#m_kabag_pembelian').addClass('active');
    $('#m_kabag_pembelian_list').addClass('active');

    function showModalEditInv() {
        //  alert("Hallo");
        $('.modal-backdrop').remove();
        $('#myModal').modal('show');
    }



    function hideModal() {
        //  alert("Hallo");
        $('.modal-backdrop').remove();
        $('#myModal').modal('hide');
    }


</script>
</asp:Content>