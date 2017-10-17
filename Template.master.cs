using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Template : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
              CheckAuth();
    }

    private void CheckAuth()
    {
        if (this.Page.User.Identity.IsAuthenticated)
       // if(true)
        {
            LblUsername.Text = Page.User.Identity.Name;
            LblUserSidebar.Text = Page.User.Identity.Name;

            if (Request.Cookies["UserSettings"] == null)
            {
               // Response.Write("Cookie kosong");
                Response.Redirect("~/Logout.aspx");
            }

            string role = Request.Cookies["UserSettings"]["role"];
 
 //           string role = "1";
            switch (role)
            {
                case "1" :
                    PanelAdmin.Visible = true;
                    TypeUser.Text = "Admin";
                    break;
                case "2" :
                    PanelKabag.Visible = true;
                    TypeUser.Text = "Kabag";
                    break;
                case "3" :
                    PanelPimpinan.Visible = true;
                    notifPimpinan.Visible = true;
                    TypeUser.Text = "Pimpinan";
                    break;
                case "4" :
                    PanelPencatatan.Visible = true;
                    TypeUser.Text = "Pencatatan";
                    break;
                case "5" : 
                    PanelTeknisi.Visible = true;
                    NotifTeknisi.Visible = true;
                    TypeUser.Text = "Teknisi";
                    break;

            }
            
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }
}
