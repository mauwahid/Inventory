<%@ Page Title="" Language="C#" MasterPageFile="~/Template.master" AutoEventWireup="true" CodeFile="AddAdjustment.aspx.cs" Inherits="Pemeliharaan_AddAdjustment" %>


<asp:Content ID="Content1" ContentPlaceHolderID="NavContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" Runat="Server">
    <h1>
          Tambah Penyesuaian
            
    </h1>
    <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-user"></i>Pemeliharaan</a></li>
            <li class="">Penyesuaian</li>
            <li class="active">Tambah Penyesuaian</li>
    </ol>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <form id="Form1" runat="server" >
    <section class="content">

        <div class="col-md-6 center-block">
          <div class="box box-primary">
                <div class="box-header">
                  <h3 class="box-title">Form Penyesuaian</h3>
                </div><!-- /.box-header -->
                <!-- form start -->
                  <div class="box-body">
                    <div class="form-group">
                      <label for="TbJenisInventaris">Jenis Inventaris</label>
                      <asp:HiddenField ID="HiddenIdJenis" runat="server" Value="0" />
                      <asp:TextBox ID="TbJenisInventaris" Text="" Enabled="false"  runat="server" CssClass="form-control" />
                    </div>
                     <div class="form-group">
                      <label for="DDLJenis" >Jenis Penyesuaian</label>
                        <asp:DropDownList ID="DDLJenis" runat="server" CssClass="form-control">
                            <asp:ListItem Value="1" Text="Penambahan" />
                            <asp:ListItem Value="2" Text="Pengurangan" />
                        </asp:DropDownList>
                    </div>
                     <div class="form-group">
                      <label for="TbQty">Jumlah</label>
                      <asp:TextBox ID="TbQty" Text=""  runat="server" CssClass="form-control" />
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


</script>
</asp:Content>
