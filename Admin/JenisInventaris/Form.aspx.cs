using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Configuration;
using System.Data.SqlClient;
using System.Data;

public partial class Admin_JenisInventori_Form : System.Web.UI.Page
{
    String formType = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        String idJenisInventaris = "";
        formType = Session["jenis_inventaris_form_type"].ToString();

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
                idJenisInventaris = Session["jenis_inventaris_form_id"].ToString();
                GenerateFormChange(idJenisInventaris);
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
        LblBC.Text = "Tambah Jenis Inventaris";
        LblHeader.Text = "Tambah Jenis Inventaris";

    }

    private void GenerateFormChange(string id)
    {
        HiddenId.Value = id;
        LblBC.Text = "Ubah Jenis Inventaris";
        LblHeader.Text = "Ubah Jenis Inventaris";

        SqlConnection conn = Common.getConnection();
        string query = "Select * from m_jenis_inventaris where id_jenis_inventaris = " + id;
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = query;

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();

        
        reader.Read();
        HiddenId.Value = reader["id_jenis_inventaris"].ToString();
        TbJenisInventaris.Text = reader["nama_jenis_inventaris"].ToString();
        conn.Close();

    }

    private void TambahData()
    {
        string query = "";
        SqlConnection conn = Common.getConnection();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = query;


        cmd.Parameters.AddWithValue("@jenis_inventaris", TbJenisInventaris.Text);



        query = "INSERT INTO m_jenis_inventaris (nama_jenis_inventaris) " +
            " VALUES(@jenis_inventaris)";


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
        cmd.Parameters.AddWithValue("@jenis_inventaris", TbJenisInventaris.Text);

        query = "update m_jenis_inventaris set nama_jenis_inventaris = @jenis_inventaris where id_jenis_inventaris = @id";


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