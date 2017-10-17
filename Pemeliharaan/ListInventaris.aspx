<%@ Page Title="" Language="C#" MasterPageFile="~/Template.master" AutoEventWireup="true" CodeFile="ListInventaris.aspx.cs" Inherits="Pemeliharaan_ListInventaris" %>

<asp:Content ID="Content1" ContentPlaceHolderID="NavContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" Runat="Server">
    <h1>
            List Inventaris <asp:Label ID="LblNamaRuangan" runat="server" />
            
    </h1>
    <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-user"></i>Pemeliharaan</a></li>
            <li class="active">Pilih Inventaris</li>
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
                    <asp:Button CssClass="btn btn-primary"  runat="server" Text="Tambah Inventaris" 
                            ID="Button3" onclick="btnTambah_Click"/>
                      <asp:Button CssClass="btn btn-info"  runat="server" Text="Refresh" 
                            ID="btnRefresh" onclick="btnRefresh_Click" />
                      
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
                <asp:HiddenField ID="HiddenId" runat="server" Value="1" />
                <div class="box-body">
                      <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                                   DataKeyNames="id_inventaris"   AllowPaging="true"
                                   ShowHeaderWhenEmpty="true"
                                   CssClass="table table-bordered" 
                                   OnRowCommand="RowCommand_Click"
                                   OnPageIndexChanging="GView_PageIndexChanging">
                                <PagerStyle CssClass="gridview" />
                                <Columns>
                                    <asp:BoundField DataField="id_ruangan" HeaderText="Id" Visible="false"/>
                                    <asp:BoundField DataField="id_inventaris_ruangan" HeaderText="Id Inventaris Ruangan" Visible="false" />
                                    <asp:BoundField DataField="id_inventaris" HeaderText="Id Inventaris" Visible="false" />
                                    <asp:BoundField DataField="nama_inventaris" HeaderText="Nama Inventaris" />
                                    <asp:BoundField DataField="nama_jenis_inventaris" HeaderText="Jenis Inventaris" />
                                    <asp:BoundField DataField="qty_total" HeaderText="Jmh Inventaris Total" />
                                    <asp:BoundField DataField="qty_terpakai" HeaderText="Jmh Inventaris Terpakai" />
                                    <asp:BoundField DataField="qty_hilang" HeaderText="Jml Inventaris Hilang" />
                                    <asp:BoundField DataField="qty_rusak" HeaderText="Jml Inventaris Rusak" />
                                    <asp:TemplateField HeaderText="Detail">
                                         <ItemTemplate>
                                           <asp:LinkButton ID="LbDetail" 
                                             CommandArgument='<%# Eval("id_ruangan")+";"+Eval("id_inventaris_ruangan")+";"+Eval("id_inventaris") %>'
                                              CommandName="Detail" runat="server">
                                             Detail</asp:LinkButton>
                                         </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Hapus">
                                         <ItemTemplate>
                                           <asp:LinkButton ID="LBHapus1" 
                                             CommandArgument='<%# Eval("id_inventaris") %>'
                                             OnClientClick="return confirm('Yakin Inventaris dihapus?');"
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
                        <asp:Label runat="server" ID="LblForm" Text="Ubah" /> Inventaris</h4>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label >Nama Inventaris</label>
                        <asp:HiddenField ID="HiddenIdInventaris" Value="0" runat="server" />
                        <asp:TextBox ID="TbInventaris" Text=""  Enabled="false" runat="server" CssClass="form-control" />
                   </div>
                   <div class="form-group">
                        <label >Jumlah Inventaris Total</label>
                        <asp:TextBox ID="TbInvTotal"  Text="" TextMode="Number" runat="server" CssClass="form-control" />
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                        ControlToValidate="TbJmlInventarisTersedia"
                        ErrorMessage="Jumlah Inventaris Tersedua harus diisi"
                        ValidationGroup="ValGroup"
                        ForeColor="Red"></asp:RequiredFieldValidator>
                     </div>
                    <div class="form-group">
                        <label >Jumlah Inventaris Terpakai</label>
                        <asp:TextBox ID="TbJmlInventarisTersedia" Enabled="false" Text="" TextMode="Number" runat="server" CssClass="form-control" />
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="TbJmlInventarisTersedia"
                        ErrorMessage="Jumlah Inventaris Tersedua harus diisi"
                        ValidationGroup="ValGroup"
                        ForeColor="Red"></asp:RequiredFieldValidator>
                     </div>
                      <div class="form-group">
                        <label >Jumlah Inventaris Hilang</label>
                        <asp:TextBox ID="TbJmlInventarisHilang" Text="" TextMode="Number" runat="server" CssClass="form-control" />
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ControlToValidate="TbJmlInventarisHilang"
                        ErrorMessage="Jumlah Inventaris Hilang harus diisi"
                        ValidationGroup="ValGroup"
                        ForeColor="Red"></asp:RequiredFieldValidator>                  
                     </div>
                      <div class="form-group">
                        <label >Jumlah Inventaris Rusak</label>
                        <asp:TextBox ID="TbJmlInventarisRusak" Text="" TextMode="Number" runat="server" CssClass="form-control" />
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                        ControlToValidate="TbJmlInventarisRusak"
                        ErrorMessage="Jumlah Inventaris Rusak harus diisi"
                        ValidationGroup="ValGroup"
                        ForeColor="Red"></asp:RequiredFieldValidator>                  
                     
                     </div>
                    <div class="form-group">
                        <label >Keterangan</label>
                        <asp:TextBox ID="TbInvKeterangan" Text="" TextMode="MultiLine"  runat="server" CssClass="form-control" />
                     </div>
                     
                
                 </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        Close</button>
                    <asp:Button ID="BtnSimpan" onclick="Update_Click" ValidationGroup="ValGroup" runat="server" CssClass="btn btn-primary" Text="Update" />

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
