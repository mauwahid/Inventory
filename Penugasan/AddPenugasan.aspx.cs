using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Configuration;
using System.Data.SqlClient;
using System.Data;

public partial class Penugasan_AddPenugasan : System.Web.UI.Page
{
    String formType = "add";

    protected void Page_Load(object sender, EventArgs e)
    {
        String idPenerimaan = "";
        if (Session["penugasan_form_type"] != null)
            formType = Session["penugasan_form_type"].ToString();

        if (!IsPostBack)
        {

            if (formType.Equals("edit"))
            {
                idPenerimaan = Session["penugasan_form_id"].ToString();
                GenerateFormChange(idPenerimaan);
            }

        }
    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        string textId = HiddenId.Value;
        if (textId.Equals("0"))
        {
            TambahData();
        }
        else
        {
           // UpdateData();
        }
    }

 

    private void GenerateFormChange(string id)
    {
        HiddenId.Value = id;
      
        SqlConnection conn = Common.getConnection();
        string query = "Select * from t_penugasan where id = " + id;
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = query;

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();

        reader.Read();
        HiddenId.Value = reader["id"].ToString();

        conn.Close();

    }

    private void TambahData()
    {
        string query = "";
        SqlConnection conn = Common.getConnection();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = query;

        cmd.Parameters.AddWithValue("@tanggal_penugasan", TbTanggalPenugasan.Text);
        cmd.Parameters.AddWithValue("@Judul", TbJudul.Text);
        cmd.Parameters.AddWithValue("@id_kabag", Request.Cookies["UserSettings"]["id_user"]);
     //   cmd.Parameters.AddWithValue("@id_kabag", "1");
        String id = "ASG" + DateTime.Now.ToString("yyyyMMddHHmmss");
        cmd.Parameters.AddWithValue("@id", id);
        
        query = "INSERT INTO t_penugasan_inventaris (id_penugasan, tanggal_penugasan, Judul, id_user,status)" +
            " VALUES(@id, @tanggal_penugasan, @Judul, @id_kabag, 6)";


        conn.Open();
        cmd.Connection = conn;
        cmd.CommandText = query;
        int reader = cmd.ExecuteNonQuery();
        if (reader>0)
        {
        //    reader.Read();
            Session["id_penugasan"] = id;
        }

        conn.Close();

        // ViewState["no_penugasan"] = TbNoPenerimaan.Text;
        Response.Redirect("PenugasanDetail.aspx");
    }


 /*   private void UpdateData()
    {
        string query = "";
        SqlConnection conn = Common.getConnection();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = query;


        cmd.Parameters.AddWithValue("@no_penugasan", .Text);
        cmd.Parameters.AddWithValue("@judul_penugasan", TbJudulPenerimaan.Text);
        cmd.Parameters.AddWithValue("@tanggal_penugasan", TbTanggalPenerimaan.Text);
        cmd.Parameters.AddWithValue("@ref_id", DDLReferensiPengajuan.SelectedValue);
        cmd.Parameters.AddWithValue("@keterangan", TbKeterangan.Text);

        query = "INSERT INTO t_penugasan_inventaris (no_penugasan, judul_penugasan, id_pengajuan, tanggal_penugasan, keterangan)  ouput Insert.id" +
            " VALUES(@no_penugasan, @judul_penugasan, @ref_id, @tanggal_penugasan, @keterangan)";



        conn.Open();
        cmd.Connection = conn;
        cmd.CommandText = query;
        cmd.ExecuteNonQuery();

        conn.Close();

        Response.Redirect("List.aspx");
    }*/

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Draft.aspx");
    }

   
}