using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Web.Security;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
       checkLogin();
        
    }

    private void checkLogin()
    {
        
        string query = "";
        SqlConnection conn = Common.getConnection();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = System.Data.CommandType.Text;
        
        cmd.Parameters.AddWithValue("@username", TbUsername.Text);
        cmd.Parameters.AddWithValue("@password", hashSHA256(TbPassword.Text));
       
        query = "Select * from m_user where username like @username and password like @password";
        cmd.CommandText = query;
        cmd.Connection = conn;
        conn.Open();
        String username = "";
        String role = "";
        String id = "";
        SqlDataReader reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            username = reader["username"].ToString();
            role = reader["id_role"].ToString();
            id = reader["id_user"].ToString();

            HttpCookie myCookie = new HttpCookie("UserSettings");
            myCookie["role"] = role;
            myCookie["id_user"] = id;
           
            myCookie.Expires = DateTime.Now.AddDays(1d);
            Response.Cookies.Add(myCookie);

            // FormsAuthentication.SetAuthCookie(username,false);
            conn.Close();

            Response.Write("SUKSES " + username);
            FormsAuthentication.RedirectFromLoginPage(username, false);
            
           // Response.Redirect("Dashboard.aspx");


        }
        else
        {
            LblInfo.Visible = true;
            conn.Close();
        }


       
        
    }

    public static string hashSHA256(string unhashedValue)
    {
        SHA256Managed shaM = new SHA256Managed();
        byte[] hash =
         shaM.ComputeHash(Encoding.ASCII.GetBytes(unhashedValue));

        StringBuilder stringBuilder = new StringBuilder();
        foreach (byte b in hash)
        {
            stringBuilder.AppendFormat("{0:x1}", b);
        }
        return stringBuilder.ToString();
    }

    
}