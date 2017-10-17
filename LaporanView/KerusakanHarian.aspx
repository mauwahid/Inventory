<%@ Page Title="" Language="C#" MasterPageFile="~/Template.master" AutoEventWireup="true" CodeFile="KerusakanHarian.aspx.cs" Inherits="LaporanView_KerusakanHarian" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="NavContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" Runat="Server">
<h1>
            Laporan
            
    </h1>
    <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-file"></i>Laporan</a></li>
            <li class="active">Kerusakan Harian</li>
    </ol>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
<form id="form1" runat="server">
    Laporan Kerusakan Harian<br />
     <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <asp:TextBox ID="TbTanggal" runat="server"></asp:TextBox>
    <asp:ImageButton ID="btnFrom" ImageAlign="Bottom" OnClientClick="return false;" ImageUrl="~/Styles/Img/calendar.png"  runat="server"/>
                      <cc1:CalendarExtender  ID="calen" Format="yyyy-MM-dd" TargetControlID="TbTanggal" runat="server" PopupButtonID="btnFrom">
                    </cc1:CalendarExtender>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            ErrorMessage="Tanggal Harus diisi"
                            ControlToValidate ="TbTanggal"
                            ValidationGroup="DefaultForm">
                    </asp:RequiredFieldValidator> 
                    <br />
                     
    <asp:Button ID="Button1" runat="server" Text="Lihat Laporan" 
            onclick="Button1_Click" />
            
    <div>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="965px">
        </rsweb:ReportViewer>
    </div>
      </ContentTemplate>
    
    </asp:UpdatePanel>
 
    </form>
</body>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="JsContent" Runat="Server">
<script type="text/javascript">

    $('ul li').removeClass('active');
    $('#m_pimpinan_laporan').addClass('active');
    $('#m_pimpinan_laporan_kerusakan_harian').addClass('active');
    $('#m_kabag_laporan').addClass('active');
    $('#m_kabag_laporan_harian').addClass('active');


</script>
</asp:Content>
