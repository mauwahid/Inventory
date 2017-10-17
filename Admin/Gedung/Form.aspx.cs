using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Configuration;
using System.Data.SqlClient;
using System.Data;

public partial class Admin_JenisRuangan_Form : System.Web.UI.Page
{
    String formType = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        String idJenisRuangan = "";
        formType = Session["gedung_form_type"].ToString();

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
                idJenisRuangan = Session["gedung_form_id"].ToString();
                GenerateFormChange(idJenisRuangan);
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
        LblBC.Text = "Tambah Gedung";
        LblHeader.Text = "Tambah Gedung";

    }

    private void GenerateFormChange(string id)
    {
        HiddenId.Value = id;
        LblBC.Text = "Ubah Gedung";
        LblHeader.Text = "Ubah Gedung";

        SqlConnection conn = Common.getConnection();
        string query = "Select * from m_gedung where id_gedung = " + id;
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = query;

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();

        reader.Read();
        HiddenId.Value = reader["id_gedung"].ToString();
        TbGedung.Text = reader["nama_gedung"].ToString();
        conn.Close();

    }

    private void TambahData()
    {
        string query = "";
        SqlConnection conn = Common.getConnection();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = query;


        cmd.Parameters.AddWithValue("@gedung", TbGedung.Text);



        query = "INSERT INTO m_gedung (nama_gedung) " +
            " VALUES(@gedung)";


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
        cmd.Parameters.AddWithValue("@gedung", TbGedung.Text);

        query = "update m_gedung set nama_gedung = @gedung where id_gedung = @id";


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