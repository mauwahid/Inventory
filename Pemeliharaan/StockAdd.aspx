<%@ Page Title="" Language="C#" MasterPageFile="~/Template.master" AutoEventWireup="true" CodeFile="StockAdd.aspx.cs" Inherits="Pemeliharaan_StockAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="NavContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" Runat="Server">
    <h1>
          <asp:Label ID="LblHeader" runat="server" Text="Form User" />
            
    </h1>
    <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-support"></i>Stock & Adjustment</a></li>
            <li class="active">Tambah Stock</li>
    </ol>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <form id="Form1" runat="server" i>
    <section class="content">

        <div class="col-md-6 center-block">
          <div class="box box-primary">
                <div class="box-header">
                  <h3 class="box-title">Form Inventaris</h3>
                </div><!-- /.box-header -->
                <!-- form start -->
                  <div class="box-body">
                    <div class="form-group">
                      <label for="MainContent_DDLInventaris">Nama Inventaris</label>
                      <asp:HiddenField ID="HiddenId" runat="server" Value="0" />
                      <asp:DropDownList ID="DDLInventaris" runat="server" CssClass="form-control" >
                     
                      </asp:DropDownList>
                    </div>
                    <div class="form-group">
                      <label for="MainContent_TbJmlInventaris">Jumlah Inventaris</label>
                      <asp:TextBox ID="TbJmlInventaris" Text="" TextMode="Number"  runat="server" CssClass="form-control" />
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
<asp:Content ID="Content4" ContentPlaceHolderID="JsContent" Runat="Server">
      <script type="text/javascript">

          $('ul li').removeClass('active');
          $('#m_pencatat_pemeliharaan').addClass('active');
          $('#m_pencatat_pemeliharaan_stock').addClass('active');



    </script>
</asp:Content>
