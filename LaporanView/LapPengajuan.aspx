<%@ Page Title="" Language="C#" MasterPageFile="~/Template.master" AutoEventWireup="true" CodeFile="LapPengajuan.aspx.cs" Inherits="LaporanView_LapPengajuan" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="NavContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" Runat="Server">
    <h1>
            Laporan Pengajuan
            
    </h1>
    <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-user"></i>Laporan</a></li>
            <li class="active">Pengajuan</li>
    </ol>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
      <form id="Form1" runat="server" defaultbutton="btnCari">
        <asp:ScriptManager ID="ScriptManager1" runat="server" >
                    </asp:ScriptManager>
                      <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                   
    <section class="content">
              <div class="box box-primary">
                <div class="box-header">
                  <h3 class="box-title"></h3>
                </div>
                <div class="box-body">
                    <div class="row">
                  
                        
                        <div class="col-md-10 col-md-offset-2">
                            <div class="form-inline">
                            
                            <div class="form-group">
                                <asp:Label ID="Label1" Text="Tgl Pengajuan : " runat="server" />
                             </div>
                             <div class="form-group">
                                <asp:TextBox ID="TbTanggalDari" Text=""  runat="server" CssClass="form-control input-date" />
                            </div>
                             <div class="form-group">
                                 <asp:Label ID="LblLantai" Text="-" runat="server" />
                             </div>
                              <div class="form-group">
                                <asp:TextBox ID="TbTanggalKe" Text=""  runat="server" CssClass="form-control input-date" />
                             </div>
                           
                             <div class="form-group">
                                 <asp:TextBox style="display:none;" CssClass="form-control" ID="TbCari" runat="server" />
                                 <asp:Button ID="btnCari" CssClass="btn btn-info btn-flat" runat="server" OnClick="Generate_Click"  Text="Generate Report !" />
                                <asp:Button ID="Button1" CssClass="btn btn-info btn-flat" runat="server" OnClick="Print_Click"  Text="Print !" />
                             </div>
                       
                        </div>
                
                        </div>
                    
                    </div>
                   
                </div>
                  <br />
                  <!-- /.box-body -->
                </div><!-- /.box -->
                    
              <div class="box box-primary">
                <div class="box-header">
                  <h3 class="box-title"></h3>
                </div>
                <div class="box-body">
                  
         <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
            Font-Size="8pt" InteractiveDeviceInfos="(Collection)" 
            WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="1000px">
          
        </rsweb:ReportViewer>
      
      </ContentTemplate>
    
    </asp:UpdatePanel>
     
                </div><!-- /.box-body -->
              
              </div><!-- /.box -->
    </section>
    </form>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="JsContent" Runat="Server">
<script type="text/javascript">



    $('ul li').removeClass('active');
    $('#m_kabag_laporan').addClass('active');
    $('#m_kabag_laporan_total').addClass('active');
    $('#m_pimpinan_laporan').addClass('active');
    $('#m_pimpinan_laporan_total').addClass('active');
    $('#m_pencatat_laporan').addClass('active');
    $('#m_pencatat_laporan_total').addClass('active');


    function doPrint() {
        var prtContent = document.getElementById('<%= ReportViewer1.ClientID %>');
        prtContent.border = 0; //set no border here
        var WinPrint = window.open('', '', 'left=150,top=100,width=1000,height=1000,toolbar=0,scrollbars=1,status=0,resizable=1');
        WinPrint.document.write(prtContent.outerHTML);
        WinPrint.document.close();
        WinPrint.focus();
        WinPrint.print();
        WinPrint.close();
    }
   

</script>
</asp:Content>