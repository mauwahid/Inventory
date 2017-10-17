<%@ Page Title="" Language="C#" MasterPageFile="~/Template.master" AutoEventWireup="true" CodeFile="LapInvTotal.aspx.cs" Inherits="LaporanView_LapInvTotal" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="NavContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" Runat="Server">
    <h1>
            Laporan Inventaris
            
    </h1>
    <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-user"></i>Laporan</a></li>
            <li class="active">Inventaris Total</li>
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
                                <asp:Label ID="Label1" Text="Gedung : " runat="server" />
                             </div>
                             <div class="form-group">
                                <asp:DropDownList ID="DDLGedung" CssClass="form-control" runat="server" 
                                   AutoPostBack="true"
                                     onselectedindexchanged="DDLGedung_SelectedIndexChanged"/>
                             </div>
                             <div class="form-group">
                                 <asp:Label ID="LblLantai" Text="Lantai : " runat="server" />
                             </div>
                              <div class="form-group">
                                <asp:DropDownList ID="DDLLantai" CssClass="form-control" runat="server" 
                                      onselectedindexchanged="DDLLantai_SelectedIndexChanged" AutoPostBack="true" />
                             </div>
                             <div class="form-group">
                                 <asp:Label ID="Label2" Text="Ruangan : " runat="server" />
                             </div>
                              <div class="form-group">
                                <asp:DropDownList ID="DDLRuangan" CssClass="form-control" runat="server" />
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
/*
    $(function() {
        alert('hai');
        if ($.browser.mozilla) {
            try {
                var ControlName = 'ReportViewer1';
                var innerScript = '<scr' + 'ipt type="text/javascript">document.getElementById("' + ControlName + '_print").Controller = new ReportViewerHoverButton("' + ControlName + '_print", false, "", "", "", "#ECE9D8", "#DDEEF7", "#99BBE2", "1px #ECE9D8 Solid", "1px #336699 Solid", "1px #336699 Solid");</scr' + 'ipt>';
                var innerTbody = '<tbody><tr><td><input type="image" style="border-width: 0px; padding: 2px; height: 16px; width: 16px;" alt="Print" src="/Reserved.ReportViewerWebControl.axd?OpType=Resource&amp;Version=9.0.30729.1&amp;Name=Microsoft.Reporting.WebForms.Icons.Print.gif" title="Print"></td></tr></tbody>';
                var innerTable = '<table title="Print" onmouseout="this.Controller.OnNormal();" onmouseover="this.Controller.OnHover();" onclick="PrintFunc(\'' + ControlName + '\'); return false;" id="' + ControlName + '_print" style="border: 1px solid rgb(236, 233, 216); background-color: rgb(236, 233, 216); cursor: default;">' + innerScript + innerTbody + '</table>'
                var outerScript = '<scr' + 'ipt type="text/javascript">document.getElementById("' + ControlName + '_print").Controller.OnNormal();</scr' + 'ipt>';
                var outerDiv = '<div style="display: inline; font-size: 8pt; height: 30px;" class=" "><table cellspacing="0" cellpadding="0" style="display: inline;"><tbody><tr><td height="28px">' + innerTable + outerScript + '</td></tr></tbody></table></div>';

                $("#" + ControlName + " > div > div").append(outerDiv);

            }
            catch (e) { alert(e); }
        }
    });

    function PrintFunc(ControlName) {
        setTimeout('ReportFrame' + ControlName + '.print();', 100);
    }
    */

</script>
</asp:Content>