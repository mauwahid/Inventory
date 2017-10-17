<%@ Page Title="" Language="C#" MasterPageFile="~/Template.master" AutoEventWireup="true" CodeFile="PenerimaanBarang.aspx.cs" Inherits="Pengadaan_PenerimaanBarang" %>


<asp:Content ID="Content1" ContentPlaceHolderID="NavContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" Runat="Server">
    <h1>
          <asp:Label ID="LblHeader" runat="server" Text="Form User" />
            
    </h1>
    <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-user"></i>Administrasi</a></li>
            <li class="">Penerimaan Inventaris</li><li class="active"><asp:Label ID="LblBC" runat="server" text="Form Pengajuan Barang"/></li>
    </ol>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
        <form id="Form1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
       <ContentTemplate>
            <asp:ScriptManager ID="ScriptManager1" runat="server" />

    <section class="content">
        <div class="col-md-6 center-block">
          <div class="box box-primary">
                <div class="box-header">
                  <h3 class="box-title">Form Penerimaan Inventaris</h3>
                </div><!-- /.box-header -->
                <!-- form start -->
                  <div class="box-body">
                          <asp:HiddenField ID="HiddenId" runat="server" Value="0" />
                        
                        <div class="form-group">
                          <label >Judul Penerimaan</label>
                          <asp:TextBox ID="TbJudulPenerimaan" Text=""  runat="server" CssClass="form-control" />
                          <asp:RequiredFieldValidator ID="RFJudul" runat="server" 
                                ControlToValidate="TbJudulPenerimaan"
                                ErrorMessage="Judul harus diisi"
                                ValidationGroup="ValPembelian"
                                ForeColor="Red">
                         </asp:RequiredFieldValidator>
                        </div>
                         <div class="form-group">
                          <label >Tanggal Penerimaan</label>
                          <asp:TextBox ID="TbTanggalPenerimaan" Text=""  runat="server" CssClass="form-control input-date" />
                        </div>
                         <div class="form-group">
                          <label >Tipe </label>
                          <asp:DropDownList ID="DDLTipePengajuan" runat="server" CssClass="form-control" 
                                 onselectedindexchanged="DDLTipePengajuan_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem Value="0" Text="Tanpa Pengajuan" />
                            <asp:ListItem Value="1" Text="Pembelian Barang" />
                            <asp:ListItem Value="2" Text="Service Keluar" />
                          </asp:DropDownList>
                        </div>
                         <div class="form-group">
                          <label runat="server" id="LblRef" visible="false" ></label>
                          <asp:DropDownList ID="DDLReferensiPengajuan" Visible="false"  runat="server" CssClass="form-control chzn-select" />
                        </div>
                         <div class="form-group">
                          <label >Keterangan</label>
                          <asp:TextBox ID="TbKeterangan" TextMode="MultiLine" Text=""  runat="server" CssClass="form-control" />
                        </div>
                        
                    
                    </div><!-- /.box-body -->

                  <div class="box-footer">
                   <asp:Button ID="BtnSubmit" runat="server" CssClass="btn btn-primary" Text="Simpan" onclick="BtnSubmit_Click" />
                    <asp:Button ID="BtnCancel" runat="server" CssClass="btn btn-delete" 
                          Text="Batalkan" onclick="BtnCancel_Click"  
                           />
                  </div>
              </div><!-- /.box -->
              </div>
   
     </section>
     </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="JsContent" Runat="Server">
<script type="text/javascript">

    $('ul li').removeClass('active');
    $('#m_kabag_penerimaan').addClass('active');
    $('#m_kabag_penerimaan_barang').addClass('active');

    $(".chzn-select").chosen();
    $(".chzn-select-deselect").chosen({ allow_single_deselect: true });

    function pageLoad() {
        $(".chzn-select").chosen();
        $(".chzn-select-deselect").chosen({ allow_single_deselect: true });

    }

</script>
</asp:Content>

