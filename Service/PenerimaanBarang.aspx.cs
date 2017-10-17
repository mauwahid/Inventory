using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Configuration;
using System.Data.SqlClient;
using System.Data;


public partial class Pengadaan_PenerimaanBarang : System.Web.UI.Page
{
    String formType = "add";

    protected void Page_Load(object sender, EventArgs e)
    {
        String idPenerimaan = "";
        if (Session["penerimaan_form_type"] != null)
            formType = Session["penerimaan_form_type"].ToString();

        if (!IsPostBack)
        {

            if (formType.Equals("add"))
            {
                GenerateFormTambah();
                InitDDLPengajuan();
            }
            else if (formType.Equals("edit"))
            {
                idPenerimaan = Session["penerimaan_form_id"].ToString();
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
            UpdateData();
        }
    }

    private void GenerateFormTambah()
    {
        LblBC.Text = "Tambah Penerimaan";
        LblHeader.Text = "Tambah Penerimaan";

    }

    private void GenerateFormChange(string id)
    {
        HiddenId.Value = id;
        LblBC.Text = "Ubah Penerimaan";
        LblHeader.Text = "Ubah Penerimaan";

        SqlConnection conn = Common.getConnection();
        string query = "Select * from t_penerimaan_inventaris where id = " + id;
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

        cmd.Parameters.AddWithValue("@no_penerimaan", TbNoPenerimaan.Text);
        cmd.Parameters.AddWithValue("@judul_penerimaan", TbJudulPenerimaan.Text);
        cmd.Parameters.AddWithValue("@tanggal_penerimaan", TbTanggalPenerimaan.Text);
        cmd.Parameters.AddWithValue("@ref_id", DDLReferensiPengajuan.SelectedValue);
        cmd.Parameters.AddWithValue("@keterangan", TbKeterangan.Text);

        query = "INSERT INTO t_penerimaan_inventaris (no_penerimaan, judul_penerimaan, id_pengajuan, tanggal_penerimaan, keterangan)  OUTPUT Inserted.id" +
            " VALUES(@no_penerimaan, @judul_penerimaan, @ref_id, @tanggal_penerimaan, @keterangan)";


        conn.Open();
        cmd.Connection = conn;
        cmd.CommandText = query;
        SqlDataReader reader = cmd.ExecuteReader();
        if (reader.HasRows)
        {
            reader.Read();
            Session["id_penerimaan"] = reader["id"].ToString();
        }

        conn.Close();

        // ViewState["no_penerimaan"] = TbNoPenerimaan.Text;
        Response.Redirect("PenerimaanDetail.aspx");
    }


    private void UpdateData()
    {
        string query = "";
        SqlConnection conn = Common.getConnection();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = query;


        cmd.Parameters.AddWithValue("@no_penerimaan", TbNoPenerimaan.Text);
        cmd.Parameters.AddWithValue("@judul_penerimaan", TbJudulPenerimaan.Text);
        cmd.Parameters.AddWithValue("@tanggal_penerimaan", TbTanggalPenerimaan.Text);
        cmd.Parameters.AddWithValue("@ref_id", DDLReferensiPengajuan.SelectedValue);
        cmd.Parameters.AddWithValue("@keterangan", TbKeterangan.Text);

        query = "INSERT INTO t_penerimaan_inventaris (no_penerimaan, judul_penerimaan, id_pengajuan, tanggal_penerimaan, keterangan)  ouput Insert.id" +
            " VALUES(@no_penerimaan, @judul_penerimaan, @ref_id, @tanggal_penerimaan, @keterangan)";



        conn.Open();
        cmd.Connection = conn;
        cmd.CommandText = query;
        cmd.ExecuteNonQuery();

        conn.Close();

        Response.Redirect("List.aspx");
    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("List.aspx");
    }

    private void InitDDLPengajuan()
    {
        SqlConnection conn = Common.getConnection();
        string query = "";
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;

        query = "select id, no_pengajuan from t_service_inventaris where status_approval = 1  order by id desc";
        cmd.CommandText = query;

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();

        DDLReferensiPengajuan.DataSource = reader;
        DDLReferensiPengajuan.DataValueField = "id";
        DDLReferensiPengajuan.DataTextField = "no_pengajuan";
        DDLReferensiPengajuan.DataBind();

        conn.Close();
    }
}