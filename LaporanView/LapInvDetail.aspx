<%@ Page Title="" Language="C#" MasterPageFile="~/Template.master" AutoEventWireup="true" CodeFile="LapInvDetail.aspx.cs" Inherits="LaporanView_LapInvTotal" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="NavContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" Runat="Server">
    <h1>
            Daftar Satuan Inventaris</h1>
    <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-user"></i>Laporan</a></li>
            <li class="active">Satuan Inventaris</li>
    </ol>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
      <form id="Form1" runat="server" >
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
                      <asp:Button ID="Button1" CssClass="btn btn-info btn-flat" runat="server" OnClick="Print_Click"  Text="Print !" />
                             
                   <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
                  Font-Size="8pt" InteractiveDeviceInfos="(Collection)" 
                  WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="1000px">
                       <localreport reportpath="Laporan\ReportInventarisDetail.rdlc">
                           <datasources>
                               <rsweb:ReportDataSource DataSourceId="SqlDataSource2" Name="DataSet1" />
                           </datasources>
                       </localreport>
        </rsweb:ReportViewer>
        
      
             
        
      
              <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                  ConnectionString="<%$ ConnectionStrings:InventoryConn %>" 
                  SelectCommand="SELECT * FROM [v_inventaris]"></asp:SqlDataSource>
              <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                  ConnectionString="<%$ ConnectionStrings:InventoryConn %>" 
                  SelectCommand="SELECT * FROM [m_distributor]"></asp:SqlDataSource>
        
      
             
        
      
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
    $('#m_kabag_laporan_detail').addClass('active');
    $('#m_pimpinan_laporan').addClass('active');
    $('#m_pimpinan_laporan_detail').addClass('active');
    $('#m_pencatat_laporan').addClass('active');
    $('#m_pencatat_laporan_detail').addClass('active');


</script>
</asp:Content>