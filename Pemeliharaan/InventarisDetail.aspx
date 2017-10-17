<%@ Page Title="" Language="C#" MasterPageFile="~/Template.master" AutoEventWireup="true" CodeFile="InventarisDetail.aspx.cs" Inherits="Pemeliharaan_InventarisDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="NavContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" Runat="Server">
    <h1>
            Detail Inventaris <asp:Label ID="LblNamaInv" runat="server" />
            
    </h1>
    <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-user"></i>Pemeliharaan</a></li>
            <li >Pilih Inventaris</li>
            <li class="active">Detail Inventaris</li>
    </ol>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
      <form id="Form1" runat="server" defaultbutton="btnCari">
      <asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
    <section class="content">
    
              <div class="box box-primary">
                <div class="box-header">
                  <h3 class="box-title"></h3>
                </div>
                <div class="box-body">
                <div class="row">
                 <div class="col-md-8">
                    <asp:Button CssClass="btn btn-primary"  runat="server" Text="Tambah Item" 
                            ID="Button3" onclick="btnTambah_Click"/>
                      <asp:Button CssClass="btn btn-info"  runat="server" Text="Refresh" 
                            ID="btnRefresh" onclick="btnRefresh_Click" />
                      <asp:Button CssClass="btn btn-warning"  runat="server" Text="Kembali" 
                            ID="Button1" onclick="btnBack_Click" />
                      
                  </div>
                    <div class="col-md-4">
                        <div class="input-group input-group-sm ">
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
                <asp:HiddenField ID="HiddenIdInventaris" runat="server" Value="0" />
                <asp:HiddenField ID="HiddenIdInvRuangan" runat="server" Value="0" />
                 <asp:HiddenField ID="HiddenEditStatus" runat="server" Value="Tambah" />
                <asp:HiddenField ID="HiddenRuangan" runat="server" Value="0" />
                 <asp:HiddenField ID="HiddenItemID" runat="server" Value="0" />
                <div class="box-body">
                      <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                                   DataKeyNames="id"   AllowPaging="true"
                                   ShowHeaderWhenEmpty="true"
                                   CssClass="table table-bordered" 
                                   OnRowCommand="RowCommand_Click"
                                   OnPageIndexChanging="GView_PageIndexChanging">
                                <PagerStyle CssClass="gridview" />
                                <Columns>
                                    <asp:BoundField DataField="id" HeaderText="Id" Visible="false" />
                                    <asp:BoundField DataField="no_inventaris" HeaderText="Nomor Inventaris" />
                                    <asp:BoundField DataField="status_available" HeaderText="Status" />
                                    <asp:TemplateField HeaderText="Ubah">
                                         <ItemTemplate>
                                           <asp:LinkButton ID="LbUbh" 
                                             CommandArgument='<%# Eval("id") %>'
                                              CommandName="Ubah" runat="server">
                                             Ubah</asp:LinkButton>
                                         </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Hapus">
                                         <ItemTemplate>
                                           <asp:LinkButton ID="LBHapus1" 
                                             CommandArgument='<%# Eval("id") %>'
                                             OnClientClick="return confirm('Yakin Item dihapus?');"
                                              CommandName="Hapus" runat="server">
                                             Hapus</asp:LinkButton>
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
                        <asp:Label runat="server" ID="LblForm" Text="" /> Item</h4>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label >No Item</label>
                        <asp:TextBox ID="TbNoItem" Text=""   runat="server" CssClass="form-control" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="TbNoItem"
                        ErrorMessage="No harus diisi"
                        ValidationGroup="ValGroup"
                        ForeColor="Red"></asp:RequiredFieldValidator>
                   </div>
                   <div class="form-group">
                        <label >Status</label>
                        <asp:DropDownList ID="DDLStatusItem" runat="server" CssClass="form-control" >
                            <asp:ListItem Value="1" Text="Terpakai" />
                            <asp:ListItem Value="2" Text="Rusak" />
                            <asp:ListItem Value="3" Text="Hilang" />
                        </asp:DropDownList>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                        ControlToValidate="DDLStatusItem"
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
<asp:Content ID="Content5" ContentPlaceHolderID="JsContent" Runat="Server">
<script type="text/javascript">

    $('ul li').removeClass('active');
    $('#m_pencatat_pemeliharaan').addClass('active');
    //  $('#m_pencatat_pemeliharaan_stock').addClass('active');

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

    function hitung() {
        var total = $("#MainContent_TbInvTotal").val();
        var rusak = $("#MainContent_TbJmlInventarisRusak").val();
        var hilang = $("#MainContent_TbJmlInventarisHilang").val();
        var terpakai = total - (hilang + rusak);
        $("#MainContent_TbJmlInventarisTersedia").val(terpakai);
    }

    function pageLoad() {

        $("#MainContent_TbInvTotal").keyup(function () {
            hitung();
        })

        $("#MainContent_TbJmlInventarisRusak").keyup(function () {
            hitung();
        })
        $("#MainContent_TbJmlInventarisHilang").keyup(function () {
            hitung();
        })
    }

</script>
</asp:Content>
