using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Configuration;
using System.Data.SqlClient;
using System.Data;

public partial class LaporanKerusakan_Form : System.Web.UI.Page
{
    String formType = "";

    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack)
        {

            

        }
    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        TambahData();
    }

 private void TambahData()
    {
        if (!FileUpload1.HasFile)
        {
        }
        else
        {
            String fileSave = "SRusak_" + FileUpload1.FileName;
            FileUpload1.SaveAs(Server.MapPath("~/Files/LaporanKerusakan/Surat") + "//" + fileSave);
            string query = "";
            SqlConnection conn = Common.getConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = query;



            cmd.Parameters.AddWithValue("@nama_laporan", TbNamaLaporan.Text);
            cmd.Parameters.AddWithValue("@path_surat", fileSave);
            cmd.Parameters.AddWithValue("@path_lampiran", fileSave);

            query = "INSERT INTO t_laporan_kerusakan (nama_laporan, path_surat, path_lampiran) " +
                " VALUES(@laporan, @path)surat, @email, @notelp, @website)";


            conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = query;
            cmd.ExecuteNonQuery();

            conn.Close();

            Response.Redirect("List.aspx");
        }
        
     
     
       
    }


    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("List.aspx");
    }
}