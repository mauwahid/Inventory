<%@ Page Title="" Language="C#" MasterPageFile="~/Template.master" AutoEventWireup="true" CodeFile="OnProgressTeknisiDetail.aspx.cs" Inherits="Penugasan_OnProgressTeknisiDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="NavContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" Runat="Server">
    <h1>
            Detail Penugasan (Sedang Dikerjakan)
            
    </h1>
    <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-user"></i>Penugasan</a></li>
            <li class="active">Detail Penugasan Baru</li>
    </ol>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
     <form id="Form1" runat="server"  >
     <asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
       
    <section class="content">
       
                     <div class="box">
                <div class="box-header">
                  <h3 class="box-title"></h3>
                </div><!-- /.box-header -->
                <div class="box-body no-padding">
                  <table class="table table-striped">
                    <tr>
                      <td>Tanggal Penugasan</td>
                      <td><asp:HiddenField ID="HiddenIDPenugasan" runat="server" />
                      <asp:HiddenField ID="HiddenIdInventaris" runat="server" />
                       <asp:HiddenField ID="HiddenIdItem" runat="server" />
                     
                      <asp:Label runat="server" ID="LblTglPenugasan" /></td>
                       <td>Pemberi Tugas</td>
                      <td><asp:Label runat="server" ID="LblPemberiTugas" /></td>
                       </tr>
                    <tr>
                      <td>Judul</td>
                      <td><asp:Label runat="server" ID="LblJudul" /></td>
                      </tr>
                   <tr>
                    <td>
                         <asp:Label ID="LblStatus" runat="server" Text="SELESAI" CssClass="label pull-right bg-green" />
                    </td>
                    <td></td>
                    <td>
                    </td>
                   </tr>
                  </table>
                </div><!-- /.box-body -->
              </div><!-- /.box -->
       
              <div class="box box-primary">
                <div class="box-header">
                  <h3 class="box-title"></h3>
                </div>
                <div class="box-body">
                            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                                   DataKeyNames="Id"   AllowPaging="true"
                                   ShowHeaderWhenEmpty="true"
                                   OnRowCommand="RowCommand_Click"
                                   CssClass="table table-bordered" 
                                   OnPageIndexChanging="GView2_PageIndexChanging">
                                <PagerStyle CssClass="gridview" />
                                <Columns>
                                    <asp:BoundField DataField="Id" HeaderText="Id" Visible="true" />
                                    <asp:BoundField DataField="jenis"  HeaderText="Jenis" />
                                    <asp:BoundField DataField="no_ref"  HeaderText="No Referensi" />
                                    <asp:BoundField DataField="judul_ref"  HeaderText="Judul" />
                                    <asp:BoundField DataField="summary"  HeaderText="Catatan" />
                                    <asp:BoundField DataField="status_desc" HeaderText="Status" Visible="true" />
                                    <asp:BoundField DataField="updated_date" HeaderText="Upd Date" Visible="true" />
                      
                                 
                                    <asp:TemplateField HeaderText="Ubah Status">
                                         <ItemTemplate>
                                           <asp:LinkButton ID="LBUbahStatus" 
                                             CommandArgument='<%# Eval("no_ref") + ";"+ Eval("jenis") %>'
                                             CommandName="UbahStatus" runat="server">
                                             Pilih</asp:LinkButton>
                                         </ItemTemplate>
                                    </asp:TemplateField>
                                 
                                 </Columns>
                                </asp:GridView>
                </div><!-- /.box-body -->
              
              </div><!-- /.box -->
    </section>
                    <!-- Modal Cari Data Inventaris -->
 
                     <!-- Modal Cari Data Inventaris -->
  
     <div class="modal" id="modalLain" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"  aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="H1">
                        <asp:Label runat="server" ID="LblForm" Text=""/> Tugas</h4>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label >Inventaris</label>
                        <asp:HiddenField ID="HIddenIdTugas" Value="0" runat="server" />
                       <asp:TextBox ID="DDLInventaris" Enabled="false" runat="server"  CssClass="form-control chzn-select" />
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                              ValidationGroup="ValGroup"
                                              ControlToValidate="DDLInventaris"
                                              ErrorMessage="Inventaris Harus diisi"
                                              ForeColor="Red"
                                              />
                                  
                   </div>
                  
                <!--    <div class="form-group">
                        <label >Qty</label>
                        <asp:TextBox ID="TbTugasQty" Text="" TextMode="Number"  runat="server" CssClass="form-control" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                    ValidationGroup="ValGroup"
                                    ControlToValidate="TbTugasQty"
                                    ErrorMessage="Jumlah Harus diisi"
                                    ForeColor="Red"
                                    />
                   </div> -->
                    <div class="form-group">
                        <label >Catatan</label>
                        <asp:TextBox ID="TbCatatan" Enabled="false" Text="" TextMode="MultiLine"  runat="server" CssClass="form-control" />
                     </div>
                      <div class="form-group">
                        <label >Status Pekerjaan</label>
                        <asp:DropDownList ID="DDLStatInventaris"  runat="server" CssClass="form-control" >
                            <asp:ListItem Text="Belum Dikerjakan" Value="0" />
                            <asp:ListItem Text="Dalam Proses" Value="1" />
                            <asp:ListItem Text="Sudah Selesai" Value="2" />
                            <asp:ListItem Text="Tidak Selesai" Value="3" />
                        </asp:DropDownList>
                     </div>
                     
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        Close</button>
                    <asp:Button ID="BtnSimpanJudul" ValidationGroup="ValGroup" onclick="SimpanTugasDetail_Click" runat="server" CssClass="btn btn-primary" Text="Simpan" />

                    <%--<button type="button"  class="btn btn-primary">
                        Save changes</button>--%>
                    </div>
            </div>
        </div>
    </div>
         
     
     <div class="modal" id="modalBeli" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"  aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="H2">
                        <asp:Label runat="server" ID="Label1" Text=""/> Pembelian</h4>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label >ID Pembelian</label>
                        <asp:HiddenField ID="HiddenField1" Value="0" runat="server" />
                       <asp:TextBox ID="DDLPembelian" Enabled="false" runat="server"  CssClass="form-control chzn-select" />
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                              ValidationGroup="BeliGroup"
                                              ControlToValidate="DDLPembelian"
                                              ErrorMessage="Pembelian Harus dipilih"
                                              ForeColor="Red"/>
                   </div>
                   
                    <div class="form-group">
                        <label >Catatan</label>
                        <asp:TextBox ID="TbCatatanBeli" Enabled="false"  Text="" TextMode="MultiLine"  runat="server" CssClass="form-control" />
                     </div>
                      <div class="form-group">
                        <label >Status Pekerjaan</label>
                        <asp:DropDownList ID="DDLStatusBeli"  runat="server" CssClass="form-control" >
                            <asp:ListItem Text="Belum Dikerjakan" Value="0" />
                            <asp:ListItem Text="Dalam Proses" Value="1" />
                            <asp:ListItem Text="Sudah Selesai" Value="2" />
                            <asp:ListItem Text="Tidak Selesai" Value="3" />
                        </asp:DropDownList>
                     </div>
                     
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        Close</button>
                    <asp:Button ID="Button3" ValidationGroup="BeliGroup" onclick="SimpanBeli_Click" runat="server" CssClass="btn btn-primary" Text="Simpan" />

                    <%--<button type="button"  class="btn btn-primary">
                        Save changes</button>--%>
                    </div>
            </div>
        </div>
    </div>
                
   
          <div class="modal" id="modalService" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"  aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="H3">
                        <asp:Label runat="server" ID="Label2" Text=""/> Tugas Service</h4>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label >ID Service-Judul</label>
                        <asp:HiddenField ID="HiddenField2" Value="0" runat="server" />
                       <asp:TextBox ID="DDLService" Enabled="false" runat="server"  CssClass="form-control chzn-select" />
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                              ValidationGroup="ServiceGroup"
                                              ControlToValidate="DDLService"
                                              ErrorMessage="Pembelian Harus dipilih"
                                              ForeColor="Red"/>
                   </div>
                   
                    <div class="form-group">
                        <label >Catatan</label>
                        <asp:TextBox ID="TbCatatanService" Enabled="false" Text="" TextMode="MultiLine"  runat="server" CssClass="form-control" />
                     </div>
                      <div class="form-group">
                        <label >Status Pekerjaan</label>
                        <asp:DropDownList ID="DDLStatusService"  runat="server" CssClass="form-control" >
                            <asp:ListItem Text="Belum Dikerjakan" Value="0" />
                            <asp:ListItem Text="Dalam Proses" Value="1" />
                            <asp:ListItem Text="Sudah Selesai" Value="2" />
                            <asp:ListItem Text="Tidak Selesai" Value="3" />
                        </asp:DropDownList>
                     </div>
                     
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        Close</button>
                    <asp:Button ID="Button4" ValidationGroup="BeliGroup" onclick="SimpanService_Click" runat="server" CssClass="btn btn-primary" Text="Simpan" />

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
    $('#li_tab1').addClass('active');


    function closeAddInvModal() {
        //  alert("Hallo");
        $('.modal-backdrop').remove();
        $('#myModal').modal('hide');
    }

    function alertSukses() {
        alert("Data Sukses Diajukan");
    }

    function alertSuksesDelete() {
        alert("Pengajuan Sukses Dihapus");
    }

    function showModalInventaris() {
        $(".modal-backdrop").remove();
        $("#myModal").modal("show");
        //   initFunction();
    }

    function hideModalInventaris() {
        $(".modal-backdrop").remove();
        $("#myModal").modal("hide");
        //   initFunction();
    }

    function showModalTugasLain() {
        $(".modal-backdrop").remove();
        $("#modalLain").modal("show");
        //   initFunction();
    }

    function hideModalTugasLain() {
        $(".modal-backdrop").remove();
        $("#modalLain").modal("hide");
        activeTab2();
        //   initFunction();
    }

    function showModalBeli() {
        $(".modal-backdrop").remove();
        $("#modalBeli").modal("show");
        //   initFunction();
    }

    function hideModalBeli() {
        $(".modal-backdrop").remove();
        $("#modalBeli").modal("hide");
        activeTab2();
        //   initFunction();
    }

    function showModalService() {
        $(".modal-backdrop").remove();
        $("#modalService").modal("show");
        //   initFunction();
    }

    function hideModalService() {
        $(".modal-backdrop").remove();
        $("#modalService").modal("hide");
        activeTab2();
        //   initFunction();
    }

    function activeTab1() {
        $('#li_tab2').removeClass('active');
        $('#li_tab1').removeClass('active');
        $('#li_tab1').addClass('active');

    }

    function activeTab2() {
        $('#li_tab1').removeClass('active');
        $('#li_tab2').removeClass('active');
        $('#li_tab2').addClass('active');

    }
    function noAddInventaris() {
        alert("Tidak ada data inventaris yang dapat ditambahkan");
    }


</script>
</asp:Content>

