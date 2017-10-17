<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LaporanKerusakanHarian.aspx.cs" Inherits="Laporan_LaporanKerusakanHarian" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
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
</html>
