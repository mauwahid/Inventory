<%@ Page Title="" Language="C#" MasterPageFile="~/Template.master" AutoEventWireup="true" CodeFile="AddPenugasan.aspx.cs" Inherits="Penugasan_AddPenugasan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="NavContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" Runat="Server">
    <h1>
          Form Penugasan
            
    </h1>
    <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-user"></i>Penugasan</a></li>
            <li class="">Tambah Penugasan</li>
    </ol>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <form id="Form1" runat="server">
    <section class="content">

        <div class="col-md-6 center-block">
          <div class="box box-primary">
                <div class="box-header">
                  <h3 class="box-title">Form Penugasan Inventaris</h3>
                </div><!-- /.box-header -->
                <!-- form start -->
                  <div class="box-body">
                        
                         <div class="form-group">
                          <label>Tanggal Penugasan</label>
                          <asp:HiddenField ID="HiddenId" runat="server" Value="0" />
                          <asp:TextBox ID="TbTanggalPenugasan" Text=""  runat="server" CssClass="form-control input-date" />
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                              ValidationGroup="ValGroup"
                              ControlToValidate="TbTanggalPenugasan"
                              ErrorMessage="Tanggal Penugasan Harus diisi"
                              ForeColor="Red"
                              />
                        </div>
                        
                         <div class="form-group">
                          <label >Judul</label>
                          <asp:TextBox ID="TbJudul" Text="" runat="server" CssClass="form-control" />
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                              ValidationGroup="ValGroup"
                              ControlToValidate="TbJudul"
                              ErrorMessage="Judul Penugasan Harus diisi"
                              ForeColor="Red"
                              />
                        </div>
                        
                    
                    </div><!-- /.box-body -->

                  <div class="box-footer">
                   <asp:Button ID="BtnSubmit" runat="server" ValidationGroup="ValGroup" CssClass="btn btn-primary" Text="Simpan" onclick="BtnSubmit_Click" 
                           />
                    <asp:Button ID="BtnCancel" runat="server" CssClass="btn btn-delete" 
                          Text="Batalkan" onclick="BtnCancel_Click"  UseSubmitBehavior="false"
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
    $('#m_kabag_penugasan').addClass('active');
    $('#m_kabag_penugasan_add').addClass('active');


</script>
</asp:Content>

