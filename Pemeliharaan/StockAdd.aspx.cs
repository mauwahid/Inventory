using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Configuration;
using System.Data.SqlClient;
using System.Data;


public partial class Pemeliharaan_StockAdd : System.Web.UI.Page
{
   
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            GenerateDDLInventaris();
        }
    }

    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        TambahData();
    }

   

 
    private void TambahData()
    {
        string query = "";
        SqlConnection conn = Common.getConnection();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = query;


        cmd.Parameters.AddWithValue("@id_inventaris", DDLInventaris.SelectedValue);
        cmd.Parameters.AddWithValue("@qty_tersedia", TbJmlInventaris.Text);
        cmd.Parameters.AddWithValue("@id_user", Request.Cookies["UserSettings"]["id_user"]);
        cmd.Parameters.AddWithValue("@created_date", DateTime.Now.ToString("yyyy-MM-dd HH:MM:ss"));

       
        query = "INSERT INTO t_stock_inventaris (id_inventaris, qty_tersedia, creator, created_date ) " +
            " VALUES(@id_inventaris, @qty_tersedia, @id_user, @created_date)";


        conn.Open();
        cmd.Connection = conn;
        cmd.CommandText = query;
        cmd.ExecuteNonQuery();

        conn.Close();

        Response.Redirect("Stock.aspx");
    }


    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Stock.aspx");
    }


    private void GenerateDDLInventaris()
    {

        SqlConnection conn = Common.getConnection();
        string query = "";
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;

        query = "select id, nama_inventaris from m_inventaris where id not in(select id_inventaris from t_stock_inventaris)";
        cmd.CommandText = query;

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();

        DDLInventaris.DataSource = reader;
        DDLInventaris.DataValueField = "id";
        DDLInventaris.DataTextField = "nama_inventaris";
        DDLInventaris.DataBind();

        conn.Close();
    }

 

 }