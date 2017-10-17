<%@ Page Title="" Language="C#" MasterPageFile="~/Template.master" AutoEventWireup="true" CodeFile="AddInventarisDetail.aspx.cs" Inherits="Pemeliharaan_AddInventarisDetail" %>


<asp:Content ID="Content1" ContentPlaceHolderID="NavContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" Runat="Server">
    <h1>
          Tambah Inventaris
            
    </h1>
    <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-user"></i>Administrasi</a></li>
            <li class="">Inventaris</li><li class="active"><asp:Label ID="LblBC" runat="server" text="Form Inventaris"/></li>
    </ol>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <form id="Form1" runat="server" i>
    <section class="content">

        <div class="col-md-6 center-block">
          <div class="box box-primary">
                <div class="box-header">
                  <h3 class="box-title">Tambah Inventaris</h3>
                </div><!-- /.box-header -->
                <!-- form start -->
                  <div class="box-body">
                    <div class="form-group">
                      <label for="exampleInputEmail1">Nama Inventaris</label>
                      <asp:HiddenField ID="HiddenIdRuangan" runat="server" Value="0" />
                      <asp:HiddenField ID="HiddenIdInventaris" runat="server" Value="0" />
                      <asp:TextBox ID="TbInventaris" Text=""  Enabled="false" runat="server" CssClass="form-control" />
                    </div>
                     <div class="form-group">
                      <label for="exampleInputEmail1">Jenis Inventaris</label>
                     <asp:TextBox ID="TbJenisInventaris"  Enabled="false" CssClass="form-control" runat="server"/>
                    </div>
                    <div class="form-group">
                      <label for="exampleInputEmail1">Jumlah Tersedia (MAX)</label>
                      <asp:TextBox ID="TbJumlahTersedia" Enabled="false" Text=""  runat="server" CssClass="form-control" />
                    </div>
                    <div class="form-group">
                      <label for="exampleInputEmail1">Inventaris Yang ditambahkan :</label>
                       <asp:TextBox ID="TbJmlInventaris" TextMode="Number" max="12"  CssClass="form-control" runat="server"/>
                    </div>
                    

                    </div><!-- /.box-body -->

                  <div class="box-footer">
                   <asp:Button ID="BtnSubmit" runat="server" CssClass="btn btn-primary" Text="Simpan" 
                          onclick="BtnSubmit_Click" />
                    <asp:Button ID="BtnCancel" runat="server" CssClass="btn btn-delete" 
                          Text="Batalkan" onclick="BtnCancel_Click" 
                           />
                  </div>
              </div><!-- /.box -->
              </div>
   
     </section>
    </form>


</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="JsContent" Runat="Server">
<script type="text/javascript">

    $('ul li').removeClass('active');
    $('#m_pencatat_pemeliharaan').addClass('active');
    $('#m_pencatat_pemeliharaan_stock').addClass('active');

    $('#MainContent_TbJmlInventaris').prop('max', $('#MainContent_TbJumlahTersedia').val());

</script>
</asp:Content>
