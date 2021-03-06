﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Template.master" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="Admin_Ruangan_Form" %>

<asp:Content ID="Content1" ContentPlaceHolderID="NavContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" Runat="Server">
    <h1>
          <asp:Label ID="LblHeader" runat="server" Text="Form User" />
            
    </h1>
    <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-user"></i>Administrasi</a></li>
            <li class="">Ruangan</li><li class="active"><asp:Label ID="LblBC" runat="server" text="Form Ruangan"/></li>
    </ol>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <form id="Form1" runat="server" i>
    <section class="content">

        <div class="col-md-6 center-block">
          <div class="box box-primary">
                <div class="box-header">
                  <h3 class="box-title">Form Ruangan</h3>
                </div><!-- /.box-header -->
                <!-- form start -->
                  <div class="box-body">
                    <div class="form-group">
                      <label for="exampleInputEmail1">Nama Ruangan</label>
                      <asp:HiddenField ID="HiddenId" runat="server" Value="0" />
                      <asp:TextBox ID="TbRuangan" Text="" MaxLength="50" runat="server" CssClass="form-control" />
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="Nama Ruangan harus diisi"
                        ForeColor="Red"
                        ValidationGroup="ValGroup"
                        ControlToValidate="TbRuangan"
                        runat="server" />
                    </div>
                     <div class="form-group">
                      <label for="exampleInputEmail1">Gedung</label>
                     <asp:DropDownList ID="DDLGedung"  CssClass="form-control" runat="server" 
                             onselectedindexchanged="DDLGedung_SelectedIndexChanged" AutoPostBack="true"/>
                    </div>
                     <div class="form-group">
                      <label for="exampleInputEmail1">Lokasi Ruangan</label>
                       <asp:DropDownList ID="DDLLokasiRuangan"  CssClass="form-control" runat="server"/>
                    </div>
                    </div><!-- /.box-body -->

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
          $('#m_admin_master_lokasi').addClass('active');
          $('#m_admin_ruangan').addClass('active');
       

    </script>
</asp:Content>

