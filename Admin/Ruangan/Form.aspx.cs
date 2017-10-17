using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Configuration;
using System.Data.SqlClient;
using System.Data;

public partial class Admin_Ruangan_Form : System.Web.UI.Page
{
    String formType = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        String idRuangan = "";
        formType = Session["ruangan_form_type"].ToString();

        if (!IsPostBack)
        {

            if (formType == null)
            {
                Response.Redirect("List.aspx");
            }
            else if (formType.Equals("add"))
            {
                GenerateFormTambah();
            }
            else if (formType.Equals("edit"))
            {
                idRuangan = Session["ruangan_form_id"].ToString();
                GenerateFormChange(idRuangan);
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
        LblBC.Text = "Tambah Ruangan";
        LblHeader.Text = "Tambah Ruangan";
        GenerateDDLGedung();
        GenerateDDLLokasi();


    }

    private void GenerateFormChange(string id)
    {
        HiddenId.Value = id;
        LblBC.Text = "Ubah Ruangan";
        LblHeader.Text = "Ubah Ruangan";

        SqlConnection conn = Common.getConnection();
        string query = "Select * from v_ruangan where id_ruangan = " + id;
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = query;

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();

       
        reader.Read();
        HiddenId.Value = reader["id_ruangan"].ToString();
        TbRuangan.Text = reader["nama_ruangan"].ToString();

        GenerateDDLGedung();
        GenerateDDLLokasi();

        DDLGedung.SelectedValue = reader["id_gedung"].ToString();
        DDLLokasiRuangan.SelectedValue = reader["id_lantai"].ToString();
        conn.Close();

    }

    private void TambahData()
    {
        string query = "";
        SqlConnection conn = Common.getConnection();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = query;


        cmd.Parameters.AddWithValue("@ruangan", TbRuangan.Text);
        cmd.Parameters.AddWithValue("@id_lantai", DDLLokasiRuangan.SelectedValue);
      //  cmd.Parameters.AddWithValue("@id_gedung", DDLGedung.SelectedValue);

        query = "INSERT INTO m_ruangan (nama_ruangan, id_lantai) " +
            " VALUES(@ruangan, @id_lantai)";


        conn.Open();
        cmd.Connection = conn;
        cmd.CommandText = query;
        cmd.ExecuteNonQuery();

        conn.Close();

        Response.Redirect("List.aspx");
    }


    private void UpdateData()
    {
        string query = "";
        SqlConnection conn = Common.getConnection();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = query;

        string selectIdJenisRuangan = DDLGedung.SelectedValue;
        string selectidLokasi = DDLLokasiRuangan.SelectedValue;

        cmd.Parameters.AddWithValue("@id", HiddenId.Value);
        cmd.Parameters.AddWithValue("@ruangan", TbRuangan.Text);
     //   cmd.Parameters.AddWithValue("@id_gedung", selectIdJenisRuangan);
        cmd.Parameters.AddWithValue("@id_lantai", selectidLokasi);

        query = "update m_ruangan set nama_ruangan = @ruangan, id_lantai=@id_lantai  where id_ruangan = @id";


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


    private void GenerateDDLGedung()
    {

        SqlConnection conn = Common.getConnection();
        string query = "";
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;
        
        query = "select id_gedung, nama_gedung from m_gedung ";
        cmd.CommandText = query;

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();

        DDLGedung.DataSource = reader;
        DDLGedung.DataValueField = "id_gedung";
        DDLGedung.DataTextField = "nama_gedung";
        DDLGedung.DataBind();

        conn.Close();
    }

    private void GenerateDDLLokasi()
    {

        SqlConnection conn = Common.getConnection();
        string query = "";
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;

        query = "select id_lantai, lokasi_lantai from m_lantai where id_gedung = "+DDLGedung.SelectedValue;
        cmd.CommandText = query;

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();

        DDLLokasiRuangan.DataSource = reader;
        DDLLokasiRuangan.DataValueField = "id_lantai";
        DDLLokasiRuangan.DataTextField = "lokasi_lantai";
        DDLLokasiRuangan.DataBind();

        conn.Close();
    }
    protected void DDLGedung_SelectedIndexChanged(object sender, EventArgs e)
    {
        GenerateDDLLokasi();
    }
}