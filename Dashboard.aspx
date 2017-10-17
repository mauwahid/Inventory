<%@ Page Title="" Language="C#" MasterPageFile="~/Template.master" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="Dashboard" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" Runat="Server">
   
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="NavContent" Runat="Server">
         
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
     <form id="form1" runat="server">
     <section class="invoice">
         <div class="row">
                <div class="col-xs-12">
                  <h2 class="page-header">
                  </h2>
                </div><!-- /.col -->
              </div>
              <div class="box box-solid">
                <div class="box-body">
                 <div class="row">
                             <div class="col-lg-6 col-md-offset-3">
                                    
                            </div>
                       </div>
                         <div class="row">
                             <div class="col-lg-6 col-md-offset-4">
                  
                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Files/logo.gif"></asp:Image>
                                  
                                  
                            </div>
                       </div>
                        <br />
                        <br />
                        <div class="row" style="float: none; margin: 0 auto;">
                            <div class="col-lg-10 col-md-offset-3">
                                 <asp:Label ID="Lba"  Text="Sistem Informasi Pengelolaan Inventaris" Font-Size="20" ForeColor="#85C2D6" Font-Bold="true" BorderStyle="Solid" BorderColor="White"  runat="server"  />
                            </div>
                       </div>       
                    </div>
              </div>
              <div class="row invoice-info">
                <div class="col-sm-12 invoice-col">
                 
                  <address>
                    <strong>Developed by :</strong><br/>
                    Tatat Nuraeni (thalumut@gmail.com)<br/>

                  </address>
                </div><!-- /.col -->
              </div><!-- /.row -->
    </section>


     </form>


</asp:Content>

