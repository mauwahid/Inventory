<%@ Page Title="" Language="C#" MasterPageFile="~/Template.master" AutoEventWireup="true" CodeFile="InventarisAdd.aspx.cs" Inherits="Pemeliharaan_StockAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="NavContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" Runat="Server">
    <h1>
          <asp:Label ID="LblHeader" runat="server" Text="Tambah Inventaris" />
            
    </h1>
    <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-support"></i>Ruangan</a></li>
            <li class="active">Tambah Inventaris</li>
    </ol>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <form id="Form1" runat="server">
    <section class="content">

        <div class="col-md-6 center-block">
          <div class="box box-primary">
               
                <!-- form start -->
                  <div class="box-body">
                    <div class="form-group">
                      <label for="MainContent_DDLInventaris">Nama Inventaris</label>
                      <asp:HiddenField ID="HiddenId" runat="server" Value="0" />
                      <asp:DropDownList ID="DDLInventaris" runat="server" CssClass="form-control chzn-select" >
                     
                      </asp:DropDownList>
                    </div>
                    <!--
                    <div class="form-group">
                      <label for="MainContent_TbJmlInventaris">Jumlah Inventaris Total</label>
                      <asp:TextBox ID="TbJmlTotal" Text="" TextMode="Number"  runat="server" CssClass="form-control" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                        ControlToValidate="TbJmlTotal"
                        ErrorMessage="Jumlah Inventaris  Total harus diisi"
                        ValidationGroup="ValGroup"
                        ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group">
                      <label for="MainContent_TbJmlInventaris">Jumlah Inventaris Terpakai</label>
                      <asp:TextBox ID="TbJmlInventaris" Text="" TextMode="Number" Enabled="false" runat="server" CssClass="form-control" />
                        <asp:RequiredFieldValidator runat="server" 
                        ControlToValidate="TbJmlInventaris"
                        ErrorMessage="Jumlah Inventaris harus diisi"
                        ValidationGroup="ValGroup"
                        ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>
                     <div class="form-group">
                      <label for="MainContent_TbJmlInventaris">Jumlah Inventaris Rusak</label>
                      <asp:TextBox ID="TbJmlInvRusak" Text="" TextMode="Number"  runat="server" CssClass="form-control" />
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="TbJmlInvRusak"
                        ErrorMessage="Jumlah Inventaris Rusak harus diisi"
                        ValidationGroup="ValGroup"
                        ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>
                     <div class="form-group">
                      <label for="MainContent_TbJmlInventaris">Jumlah Inventaris Hilang</label>
                      <asp:TextBox ID="TbJmlInvHilang" Text="" TextMode="Number"  runat="server" CssClass="form-control" />
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ControlToValidate="TbJmlInvHilang"
                        ErrorMessage="Jumlah Inventaris Hilang harus diisi"
                        ValidationGroup="ValGroup"
                        ForeColor="Red"
                        ></asp:RequiredFieldValidator>
                    </div>
                  </div>
                  -->
                  <div class="box-footer">
                   <asp:Button ID="BtnSubmit" runat="server" ValidationGroup="ValGroup" CssClass="btn btn-primary" Text="Simpan" 
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

         
          
        //  function pageLoad() {

          $("#MainContent_TbJmlTotal").keyup(function () {
              hitungTotal();
          })

              $("#MainContent_TbJmlInvRusak").keyup(function () {
                  hitungTotal();
              })

              $("#MainContent_TbJmlInvHilang").keyup(function () {
                  hitungTotal();
              })
       //   }

          function hitungTotal() {
          //    var terpakai = $("#MainContent_TbJmlInventaris").val();
              var rusak = $("#MainContent_TbJmlInvRusak").val();
              var hilang = $("#MainContent_TbJmlInvHilang").val();
              //var total = 0;
              var total = $("#MainContent_TbJmlTotal").val();
              
              if (total == null)
                  total = 0;

              if (rusak == null)
                  rusak = 0;

              if (hilang == null)
                  hilang = 0;


              var terpakai = parseInt(total) - (parseInt(rusak) + parseInt(hilang));

              $("#MainContent_TbJmlInventaris").val(terpakai);


          }

          
    </script>
</asp:Content>
