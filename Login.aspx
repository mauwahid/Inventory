<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<html>
  <head>
    <meta charset="UTF-8">
    <title>Inventory System</title>
    <meta content='width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no' name='viewport'>
    <!-- Bootstrap 3.3.4 -->
    <link href="Styles/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css"  runat="server"/>
    <!-- Font Awesome Icons -->
    <link href="Styles/plugins/font-awesome/font-awesome.min.css" rel="stylesheet" type="text/css" />    
   <!-- Theme style -->
     <link id="Link1" href="Styles/dist/css/AdminLTE.css" rel="stylesheet" type="text/css"  runat="server" />
   <!-- iCheck -->
    <link href="Styles/plugins/iCheck/square/blue.css" rel="stylesheet" type="text/css"  runat="server" />
   
    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js"></script>
        <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
    <![endif]-->
  </head>
  <body class="login-page" >

    <div class="login-box">
      <div class="login-logo">
        <a href=""><b>Login Sistem Informasi Inventaris</b></a>
      </div><!-- /.login-logo -->
      <div class="login-box-body">
        <p class="login-box-msg"></p>
        <form runat="server">
          <div class="form-group has-feedback">
            <asp:TextBox runat="server" ID="TbUsername"  CssClass="form-control" placeholder="Username"/>
            <span class="glyphicon glyphicon-user form-control-feedback"></span>
          </div>
          <div class="form-group has-feedback">
            <asp:TextBox  runat="server" ID="TbPassword" TextMode="Password" CssClass="form-control" placeholder="Password"/>
            <span class="glyphicon glyphicon-lock form-control-feedback"></span>
          </div>
          <div class="row">
            <div class="col-xs-8">    
                <div class="icheck">
                    <asp:Label ID="LblInfo" Text="Username atau Password Salah" runat="server" ForeColor="Red" Visible="false" />
                </div>             
            </div><!-- /.col -->
            <div class="col-xs-4">
              <asp:Button runat="server" ID="BtnSubmit" 
                    CssClass="btn btn-primary btn-block btn-flat" Text="Submit" 
                    onclick="BtnSubmit_Click" />
            </div><!-- /.col -->
          </div>
        </form>

        
      </div><!-- /.login-box-body -->
    </div><!-- /.login-box -->

     
  </body>
</html>