<%@ Page Title="" Language="C#" MasterPageFile="~/Template.master" AutoEventWireup="true" CodeFile="TambahCatatan.aspx.cs" Inherits="Pemeliharaan_TambahKerusakan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="NavContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" Runat="Server">
    <h1>
          Catat Kerusakan di Ruang <asp:Label ID="LblRuangan" runat="server" Text="" />
            
    </h1>
    <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-user"></i>Pemeliharaan</a></li>
            <li class="">Inventaris</li><li class="active"><asp:Label ID="LblBC" runat="server" text="Pencatatan"/></li>
    </ol>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <form id="Form1" runat="server">
    <section class="content">

        <div class="col-md-6 center-block">
          <div class="box box-primary">
                <div class="box-header">
                  <h3 class="box-title">Form Pencatatan Kerusakan</h3>
                </div><!-- /.box-header -->
                <!-- form start -->
                  <div class="box-body">
                    <div class="form-group">
                      <label >No Pencatatan</label>
                      <asp:HiddenField ID="HiddenIdRuangan" runat="server" Value="0" />
                      <asp:TextBox ID="TbNoPencatatan" Text=""  runat="server" CssClass="form-control" />
                        </div>
                         <div class="form-group">
                          <label >Tanggal Pencatatan</label>
                          <asp:TextBox ID="TbTanggalPencatatan" Text=""  runat="server" CssClass="form-control input-date" />
                        </div>
                         <div class="form-group">
                          <label >Catatan</label>
                          <asp:TextBox ID="TbCatatan" TextMode="MultiLine" Text=""  runat="server" CssClass="form-control" />
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
<asp:Content ID="Content5" ContentPlaceHolderID="JsContent" Runat="Server">
<script type="text/javascript">

    $('ul li').removeClass('active');
    $('#m_pencatat_pemeliharaan').addClass('active');
    $('#m_pencatat_pemeliharaan_lokasi').addClass('active');


</script>
</asp:Content>