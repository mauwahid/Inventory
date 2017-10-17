using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Configuration;
using System.Data.SqlClient;
using System.Data;

public partial class Pemeliharaan_AddAdjustment : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadID();
        }
    }


    private void LoadID()
    {
        string idJenis = Session["id_inventaris"].ToString();
        //string idJenis = "2";
        HiddenIdJenis.Value = idJenis;
        SqlConnection conn = Common.getConnection();
        string query = "Select nama_inventaris from m_inventaris where id = " + idJenis;
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = query;

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
            TbJenisInventaris.Text = reader["nama_inventaris"].ToString();

        conn.Close();

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
        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        cmd.CommandText = "UPSERT_ADJUSTMENT_INVENTARIS";

        cmd.Parameters.AddWithValue("@id_inventaris", HiddenIdJenis.Value);
        cmd.Parameters.AddWithValue("@jenis", DDLJenis.SelectedValue);
        cmd.Parameters.AddWithValue("@qty",TbQty.Text);


        conn.Open();
        cmd.Connection = conn;
        cmd.ExecuteNonQuery();

        conn.Close();
        Session["id_inventaris"] = HiddenIdJenis.Value;
        Response.Redirect("Adjustment.aspx");
    }

   protected void BtnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Adjustment.aspx");
    }
}