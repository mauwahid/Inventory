using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;


public partial class Admin_User_UserList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GvBind();
        }

    }

    protected void btnTambah_Click(object sender, EventArgs e)
    {
        Session["user_form_type"] = "add";
        Response.Redirect("FormUser.aspx");
    }


    protected void GvBind()
    {
        SqlConnection conn = Common.getConnection();
        string query = "Select * from m_user";
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = query;

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();

        DataTable dt = new DataTable();
        dt.Columns.Add("Id", typeof(string));
        dt.Columns.Add("Username", typeof(string));
        dt.Columns.Add("NamaLengkap", typeof(string));
        dt.Columns.Add("NoTelp", typeof(string));
        dt.Columns.Add("Email", typeof(string));
        dt.Columns.Add("Deskripsi", typeof(string));
        dt.Columns.Add("Role", typeof(string));


        DataRow dr = null;
        string sRole = "";

        while (reader.Read())
        {
            dr = dt.NewRow();
            dr["Id"] = reader["id_user"].ToString();
            dr["Username"] = reader["username"].ToString();
            dr["NamaLengkap"] = reader["nama_lengkap"].ToString();
            dr["NoTelp"] = reader["no_telp"].ToString();
            dr["Email"] = reader["email"].ToString();
            dr["Deskripsi"] = reader["deskripsi"].ToString();
            
            sRole = reader["id_role"].ToString().Trim();
            switch (sRole)
            {
                case "1" :
                    dr["Role"] = "Administrator";
                    break;
                case "2":
                    dr["Role"] = "Ketua Divisi";
                    break;
                case "3":
                    dr["Role"] = "Koordinator BPRTIK";
                    break;
                case "4":
                    dr["Role"] = "Bagian Pencatatan";
                    break;
                case "5":
                    dr["Role"] = "Teknisi";
                    break;
                
            }

            
            dt.Rows.Add(dr);


        }

        GridView1.DataSource = dt;
        GridView1.DataBind();

        conn.Close();

    }

    private void blankTable()
    {
        GridView1.DataSource = new List<String>();
        GridView1.DataBind();
    }

    protected void RowCommand_Click(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Hapus")
        {
            string idUser = Convert.ToString(e.CommandArgument);
            Delete(idUser);
            GvBind();
        }
    }

    protected void Delete(string id)
    {
        SqlConnection conn = Common.getConnection();
        string query = "";
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;
        string param = TbCari.Text;

        query = "delete m_user where id = "+id;
        cmd.CommandText = query;

        conn.Open();
        int reader = cmd.ExecuteNonQuery();

        conn.Close();

    }

    protected void GView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GvBind();
    }

    protected void RowEdit_Click(object sender,GridViewEditEventArgs e)
    {

        string id = GridView1.DataKeys[e.NewEditIndex]["Id"].ToString();
        Session["user_form_type"] = "edit";
        Session["user_form_id"] = id;
        Response.Redirect("FormUser.aspx");


    }

    protected void RowDelete_Click(object sender, GridViewDeleteEventArgs e)
    {

        string id = GridView1.DataKeys[e.RowIndex]["Id"].ToString();
        Delete(id);
        GvBind();


    }
    protected void btnCari_Click(object sender, EventArgs e)
    {
        SqlConnection conn = Common.getConnection();
        string query = "";
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;
        string param = TbCari.Text;

        query = "select * from m_user where username like '%" + param + "%' or nama_lengkap like '%" + param + "%' or email like '%" + param + "%'";
       cmd.CommandText = query;

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();

        DataTable dt = new DataTable();
        dt.Columns.Add("Id", typeof(string));
        dt.Columns.Add("Username", typeof(string));
        dt.Columns.Add("NamaLengkap", typeof(string));
        dt.Columns.Add("NoTelp", typeof(string));
        dt.Columns.Add("Email", typeof(string));
        dt.Columns.Add("Deskripsi", typeof(string));
        dt.Columns.Add("Role", typeof(string));


        DataRow dr = null;
        string sRole = "";

        while (reader.Read())
        {
            dr = dt.NewRow();
            dr["Id"] = reader["id_user"].ToString();
            dr["Username"] = reader["username"].ToString();
            dr["NamaLengkap"] = reader["nama_lengkap"].ToString();
            dr["NoTelp"] = reader["no_telp"].ToString();
            dr["Email"] = reader["email"].ToString();
            dr["Deskripsi"] = reader["deskripsi"].ToString();

            sRole = reader["id_role"].ToString().Trim();
            switch (sRole)
            {
                case "1":
                    dr["Role"] = "Administrator";
                    break;
                case "2":
                    dr["Role"] = "Kepala Bagian";
                    break;
                case "3":
                    dr["Role"] = "Pimpinan";
                    break;
                case "4":
                    dr["Role"] = "Bagian Pencatatan";
                    break;
                case "5":
                    dr["Role"] = "Teknisi";
                    break;

            }


            dt.Rows.Add(dr);


        }

        GridView1.DataSource = dt;
        GridView1.DataBind();

        conn.Close();

    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        GvBind();
    }
}