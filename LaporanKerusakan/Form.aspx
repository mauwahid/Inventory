<%@ Page Title="" Language="C#" MasterPageFile="~/Template.master" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="LaporanKerusakan_Form" %>

<asp:Content ID="Content1" ContentPlaceHolderID="NavContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" Runat="Server">
    <h1>
          <asp:Label ID="LblHeader" runat="server" Text="Form Distributor" />
            
    </h1>
    <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-support"></i>Laporan Kerusakan</a></li>
            <li class="">Distributor</li><li class="active"><asp:Label ID="LblBC" runat="server" text="Form Distributor"/></li>
    </ol>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <form id="Form1" runat="server" i>
    <section class="content">

        <div class="col-md-6 center-block">
          <div class="box box-primary">
                <div class="box-header">
                  <h3 class="box-title">Form Laporan</h3>
                </div><!-- /.box-header -->
                <!-- form start -->
                  <div class="box-body">
                    <div class="form-group">
                      <label for="exampleInputEmail1">Nama Laporan</label>
                      <asp:HiddenField ID="HiddenId" runat="server" Value="0" />
                      <asp:TextBox ID="TbNamaLaporan" Text=""  runat="server" CssClass="form-control" />
                    </div>
                     <div class="form-group">
                      <label for="exampleInputEmail1">Surat</label>
                      <div class="col-sm-4">
                           <asp:FileUpload ID="FileUpload1" runat="server" />
                            <asp:Button ID="btnUpload" Text="Contoh File" runat="server" CssClass="btn btn-primary"/>
                      </div>
                      
                    </div>
                     <div class="form-group">
                      <label for="exampleInputEmail1">Lampiran</label>
                      <asp:Button ID="BtnLampiran" Text="Lihat Lampiran" CssClass="btn btn-info" runat="server" />
                    </div>
                    
                    </div><!-- /.box-body -->

                  <div class="box-footer">
                   <asp:Button ID="BtnSubmit" runat="server" CssClass="btn btn-primary" Text="Kirim Laporan" 
                          onclick="BtnSubmit_Click" OnClientClick="return confirm('Sudah yakin kirim laporan ke pimpinan?');" />
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
          $('#m_admin_tree_master').addClass('active');
          $('#m_admin_distributor').addClass('active');

       

    </script>
</asp:Content>

