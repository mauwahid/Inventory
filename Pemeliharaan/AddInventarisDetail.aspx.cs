using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Configuration;
using System.Data.SqlClient;
using System.Data;

public partial class Pemeliharaan_AddInventarisDetail : System.Web.UI.Page
{
    string idRuangan = "";
    string idInventaris = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GenerateFormTambah();
        }
    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        TambahData();
    }

    private void GenerateFormTambah()
    {
        idRuangan = Session["id_ruangan"].ToString();
        idInventaris = Session["id_inventaris"].ToString();
        TbJumlahTersedia.Text = Session["qty_tersedia"].ToString();

        HiddenIdRuangan.Value = idRuangan;
        HiddenIdInventaris.Value = idInventaris;

        string query = "select inv.id, inv.nama_inventaris, jinv.nama_jenis from m_inventaris inv "
                    +"left join m_jenis_inventaris jinv on inv.id_jenis_inventaris = jinv.id where inv.id = "+idInventaris+"";
      
        SqlConnection conn = Common.getConnection();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = query;
        conn.Open();
        cmd.Connection = conn;
        
        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
           TbInventaris.Text = reader["nama_inventaris"].ToString();
            TbJenisInventaris.Text = reader["nama_jenis"].ToString();

        }

        conn.Close();




    }

 
    private void TambahData()
    {
        string query = "UPSERT_INVENTARIS_RUANGAN";
        SqlConnection conn = Common.getConnection();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = System.Data.CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@id_ruangan", HiddenIdRuangan.Value);
        cmd.Parameters.AddWithValue("@id_inventaris",HiddenIdInventaris.Value);
        cmd.Parameters.AddWithValue("@qty_ditambah", TbJmlInventaris.Text);
        
       
        conn.Open();
        cmd.Connection = conn;
        cmd.CommandText = query;
        cmd.ExecuteNonQuery();

        conn.Close();
        Session["id_ruangan"] = HiddenIdRuangan.Value;
        Response.Redirect("ListInventaris.aspx");

    }


    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddInventaris.aspx");
    }


}