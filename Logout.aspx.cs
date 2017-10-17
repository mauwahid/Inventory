using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class Logout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            CheckAuth();
    }

    private void CheckAuth()
    {
        if (this.Page.User.Identity.IsAuthenticated)
        {
            


            if (Request.Cookies["UserSettings"] != null)
            {
                HttpCookie myCookie = new HttpCookie("UserSettings");
                myCookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(myCookie);
            }

            FormsAuthentication.SignOut();
           // FormsAuthentication.RedirectToLoginPage();
            Response.Redirect("~/Login.aspx");
            

        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }
}