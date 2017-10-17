<%@ Page Title="" Language="C#" MasterPageFile="~/Template.master" AutoEventWireup="true" CodeFile="PenerimaanBarang.aspx.cs" Inherits="Pengadaan_PenerimaanBarang" %>


<asp:Content ID="Content1" ContentPlaceHolderID="NavContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" Runat="Server">
    <h1>
          <asp:Label ID="LblHeader" runat="server" Text="Form User" />
            
    </h1>
    <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-user"></i>Administrasi</a></li>
            <li class="">Penerimaan Barang</li><li class="active"><asp:Label ID="LblBC" runat="server" text="Form Pengajuan Barang"/></li>
    </ol>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <form id="Form1" runat="server">
    <section class="content">

        <div class="col-md-6 center-block">
          <div class="box box-primary">
                <div class="box-header">
                  <h3 class="box-title">Form Penerimaan Barang</h3>
                </div><!-- /.box-header -->
                <!-- form start -->
                  <div class="box-body">
                        <div class="form-group">
                          <label >No Penerimaan</label>
                          <asp:HiddenField ID="HiddenId" runat="server" Value="0" />
                          <asp:TextBox ID="TbNoPenerimaan" Text=""  runat="server" CssClass="form-control" />
                        </div>
                        <div class="form-group">
                          <label >Judul Penerimaan</label>
                          <asp:TextBox ID="TbJudulPenerimaan" Text=""  runat="server" CssClass="form-control" />
                        </div>
                         <div class="form-group">
                          <label >Tanggal Penerimaan</label>
                          <asp:TextBox ID="TbTanggalPenerimaan" Text=""  runat="server" CssClass="form-control" />
                        </div>
                         <div class="form-group">
                          <label >Referensi Pengajuan</label>
                          <asp:DropDownList ID="DDLReferensiPengajuan"  runat="server" CssClass="form-control" />
                        </div>
                         <div class="form-group">
                          <label >Keterangan</label>
                          <asp:TextBox ID="TbKeterangan" Text=""  runat="server" CssClass="form-control" />
                        </div>
                        
                    
                    </div><!-- /.box-body -->

                  <div class="box-footer">
                   <asp:Button ID="BtnSubmit" runat="server" CssClass="btn btn-primary" Text="Simpan" onclick="BtnSubmit_Click" 
                           />
                    <asp:Button ID="BtnCancel" runat="server" CssClass="btn btn-delete" 
                          Text="Batalkan" onclick="BtnCancel_Click"  
                           />
                  </div>
              </div><!-- /.box -->
              </div>
   
     </section>
    </form>


</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="JsContent" Runat="Server">
<script type="text/javascript">

    $('ul li').removeClass('active');
    $('#m_kabag_penerimaan').addClass('active');
    $('#m_kabag_penerimaan_barang').addClass('active');


</script>
</asp:Content>

