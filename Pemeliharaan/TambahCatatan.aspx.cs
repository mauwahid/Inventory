using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class Pemeliharaan_TambahKerusakan : System.Web.UI.Page
{
    String formType = "add";

    protected void Page_Load(object sender, EventArgs e)
    {
        String idRuangan = "";
        
        
        if (!IsPostBack)
        {

            idRuangan = Session["id_ruangan"].ToString();

            LblRuangan.Text = idRuangan;
            HiddenIdRuangan.Value = idRuangan;

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

        cmd.Parameters.AddWithValue("@id_ruangan", HiddenIdRuangan.Value);
        cmd.Parameters.AddWithValue("@no_catatan", TbNoPencatatan.Text);
        cmd.Parameters.AddWithValue("@tanggal_catatan", TbTanggalPencatatan.Text);
        cmd.Parameters.AddWithValue("@catatan", TbCatatan.Text);
        cmd.Parameters.AddWithValue("@id_user", 1);

        query = "INSERT INTO t_pencatatan_inventaris (no_pencatatan, tanggal_pencatatan, id_ruangan, catatan, id_pencatat) " +
            " Output Inserted.Id VALUES(@no_catatan, @tanggal_catatan, @id_ruangan, @catatan, @id_user)";


        conn.Open();
        cmd.Connection = conn;
        cmd.CommandText = query;
        string id = "";
        SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
            id = reader["id"].ToString();

        conn.Close();

        Session["id_pencatatan"] = id;
        Session["id_ruangan"] = HiddenIdRuangan.Value;
        // ViewState["no_pengajuan"] = TbNoPengajuan.Text;
        Response.Redirect("PencatatanDetail.aspx");
    }


    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("List.aspx");
    }
}