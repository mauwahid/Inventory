<%@ Page Title="" Language="C#" MasterPageFile="~/Template.master" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="Penerimaan_Form" %>

<asp:Content ID="Content1" ContentPlaceHolderID="NavContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" Runat="Server">
    <h1>
          <asp:Label ID="LblHeader" runat="server" Text="Form User" />
            
    </h1>
    <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-server"></i>Penerimaan</a></li>
            <li class="active">Tambah Penerimaan</li>
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
                      <label >No Penerimaan</label>
                      <asp:HiddenField ID="HiddenId" runat="server" Value="0" />
                      <asp:TextBox ID="TbNoPengajuan" Text=""  runat="server" CssClass="form-control" />
                      <asp:RequiredFieldValidator runat="server"
                      ValidationGroup="ValGroup"
                      ControlToValidate="TbNoPengajuan"
                      ErrorMessage="No Penerimaan Harus diisi"
                      ForeColor="Red"
                      />
                    </div>
                         <div class="form-group">
                          <label >Judul Penerimaan</label>
                          <asp:TextBox ID="TbJudulPenerimaan" Text=""  runat="server" CssClass="form-control" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                              ValidationGroup="ValGroup"
                              ControlToValidate="TbJudulPenerimaan"
                              ErrorMessage="Judul Penerimaan Harus diisi"
                              ForeColor="Red"
                              />
                        </div>
                         <div class="form-group">
                          <label >Referensi Pengajuan Inventaris (Bila Ada)</label>
                          <asp:DropDownList ID="DDLPengajuan"   runat="server" CssClass="form-control" />
                        </div>
                    
                         <div class="form-group">
                          <label >Catatan Tambahan</label>
                          <asp:TextBox ID="TbMemoKabag" Text="" TextMode="MultiLine"  runat="server" CssClass="form-control" />
                        </div>
                    
                    </div><!-- /.box-body -->

                  <div class="box-footer">
                   <asp:Button ID="BtnSubmit" ValidationGroup="ValGroup" runat="server" CssClass="btn btn-primary" Text="Simpan" onclick="BtnSubmit_Click" 
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
          $('#m_kabag_pengajuan').addClass('active');
          $('#m_kabag_pengajuan_form').addClass('active');

       

    </script>
</asp:Content>

