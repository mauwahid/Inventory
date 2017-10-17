<%@ Page Title="" Language="C#" MasterPageFile="~/Template.master" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="Admin_Inventaris_Form" %>

<asp:Content ID="Content1" ContentPlaceHolderID="NavContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" Runat="Server">
    <h1>
          <asp:Label ID="LblHeader" runat="server" Text="Form User" />
            
    </h1>
    <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-support"></i>Data Master</a></li>
            <li class="">Inventaris</li><li class="active"><asp:Label ID="LblBC" runat="server" text="Form Inventaris"/></li>
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
                      <label for="exampleInputEmail1">Nama Inventaris</label>
                      <asp:HiddenField ID="HiddenId" runat="server" Value="0" />
                      <asp:TextBox ID="TbInventaris" Text="" MaxLength="30"  runat="server" CssClass="form-control" />
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="Nama Inventaris harus diisi"
                        ForeColor="Red"
                        ValidationGroup="ValGroup"
                        ControlToValidate="TbInventaris"
                        runat="server" />

                    </div>
                    <div class="form-group">
                      <label for="exampleInputEmail1">Keterangan</label>
                      <asp:TextBox ID="TbKeterangan" TextMode="MultiLine" Text=""  runat="server" CssClass="form-control" />
                    </div>
                     <div class="form-group">
                      <label for="exampleInputEmail1">Merk</label>
                       <asp:DropDownList ID="DDLMerk"  CssClass="form-control" runat="server"/>
                    </div>
                    <div class="form-group">
                      <label for="exampleInputEmail1">Jenis Inventaris</label>
                       <asp:DropDownList ID="DDLJenisInventaris"  CssClass="form-control" runat="server"/>
                    </div>
                     <div class="form-group">
                      <label for="exampleInputEmail1">Distributor</label>
                       <asp:DropDownList ID="DDLDistributor"  CssClass="form-control" runat="server"/>
                    </div>
                     <div class="form-group">
                      <label for="exampleInputEmail1">Qty</label>
                      <asp:TextBox ID="TbQty" Text=""  TextMode="Number" runat="server" CssClass="form-control" />
                        
                    </div>
                    <div class="form-group">
                      <label for="exampleInputEmail1">Harga (Rp)</label>
                      <asp:TextBox ID="TbHarga" Text=""  TextMode="Number" runat="server" CssClass="form-control" />
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ErrorMessage="Harga Inventaris harus diisi"
                        ForeColor="Red"
                        ValidationGroup="ValGroup"
                        ControlToValidate="TbHarga"
                        runat="server" />
                    </div>
                     
                    </div><!-- /.box-body -->

                  <div class="box-footer">
                   <asp:Button ID="BtnSubmit" ValidationGroup="ValGroup" runat="server" CssClass="btn btn-primary" Text="Simpan" 
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
          $('#m_admin_tree_master').addClass('active');
          $('#m_admin_inventaris').addClass('active');

       

    </script>
</asp:Content>
