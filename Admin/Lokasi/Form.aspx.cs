using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Configuration;
using System.Data.SqlClient;
using System.Data;

public partial class Admin_Lokasi_Form : System.Web.UI.Page
{
    String formType = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        String idLokasi = "";
        formType = Session["lokasi_form_type"].ToString();

        if (!IsPostBack)
        {
            GenerateDDLGedung();

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
                idLokasi = Session["lokasi_form_id"].ToString();
                GenerateFormChange(idLokasi);
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
        LblBC.Text = "Tambah Lantai";
        LblHeader.Text = "Tambah Lantai";
   
    }

    private void GenerateFormChange(string id)
    {
        HiddenId.Value = id;
        LblBC.Text = "Ubah Lantai";
        LblHeader.Text = "Ubah Lantai";

        SqlConnection conn = Common.getConnection();
        string query = "Select a.*, b.nama_gedung from m_lantai a left join m_gedung b on a.id_gedung = b.id_gedung where a.id_lantai = " + id;
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = query;

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();

        string sRole = "";

        reader.Read();
        HiddenId.Value = reader["id_lantai"].ToString();
        TbLokasiLantai.Text = reader["lokasi_lantai"].ToString();
        DDLGedung.SelectedValue = reader["nama_gedung"].ToString();
        conn.Close();

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



    private void TambahData()
    {
        string query = "";
        SqlConnection conn = Common.getConnection();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = query;

        cmd.Parameters.AddWithValue("@lokasi", TbLokasiLantai.Text);
        cmd.Parameters.AddWithValue("@id_gedung", DDLGedung.SelectedValue);
        query = "INSERT INTO m_lantai (lokasi_lantai, id_gedung) " +
            " VALUES(@lokasi, @id_gedung)";


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


        cmd.Parameters.AddWithValue("@id", HiddenId.Value);
        cmd.Parameters.AddWithValue("@lokasi", TbLokasiLantai.Text);
        cmd.Parameters.AddWithValue("@id_gedung",DDLGedung.SelectedValue);
        query = "update m_lantai set lokasi_lantai = @lokasi, id_gedung = @id_gedung where id_lantai = @id";


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
}