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
            HiddenId.Value = Session["id_ruangan"].ToString();
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
      //  cmd.Parameters.AddWithValue("@qty_tersedia", TbJmlInventaris.Text);
        //  cmd.Parameters.AddWithValue("@qty_total", TbJmlTotal.Text);
        //  cmd.Parameters.AddWithValue("@qty_rusak", TbJmlInvRusak.Text);
        // cmd.Parameters.AddWithValue("@qty_hilang", TbJmlInvHilang.Text);
        cmd.Parameters.AddWithValue("@qty_terpakai", 0);
         cmd.Parameters.AddWithValue("@qty_total",0);
          cmd.Parameters.AddWithValue("@qty_rusak",0);
         cmd.Parameters.AddWithValue("@qty_hilang", 0);
        cmd.Parameters.AddWithValue("@id_ruangan", HiddenId.Value);
       // cmd.Parameters.AddWithValue("@id_user", Request.Cookies["UserSettings"]["id_user"]);
        cmd.Parameters.AddWithValue("@id_user", "1");
        cmd.Parameters.AddWithValue("@created_date", DateTime.Now.ToString("yyyy-MM-dd HH:MM:ss"));

       
        query = "INSERT INTO t_inventaris_ruangan (id_inventaris,id_ruangan, qty_total, qty_terpakai, qty_rusak, qty_hilang, creator, created_date ) " +
            " VALUES(@id_inventaris,@id_ruangan, @qty_total, @qty_terpakai, @qty_rusak, @qty_hilang, @id_user, @created_date)";


        conn.Open();
        cmd.Connection = conn;
        cmd.CommandText = query;
        cmd.ExecuteNonQuery();

        conn.Close();

        Response.Redirect("ListInventaris.aspx");
    }


    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("ListInventaris.aspx");
    }


    private void GenerateDDLInventaris()
    {

        SqlConnection conn = Common.getConnection();
        string query = "";
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.Parameters.AddWithValue("@IdRuangan", HiddenId.Value);

        query = "select id_inventaris, nama_inventaris from m_inventaris where id_inventaris not in(select id_inventaris from t_inventaris_ruangan where id_ruangan = @IdRuangan)";
        cmd.CommandText = query;

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();

        DDLInventaris.DataSource = reader;
        DDLInventaris.DataValueField = "id_inventaris";
        DDLInventaris.DataTextField = "nama_inventaris";
        DDLInventaris.DataBind();

        conn.Close();
    }

 

 }