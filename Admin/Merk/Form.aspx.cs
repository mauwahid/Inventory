using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Configuration;
using System.Data.SqlClient;
using System.Data;

public partial class Admin_Merk_Form : System.Web.UI.Page
{
    String formType = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        String idMerk = "";
        formType = Session["merk_form_type"].ToString();

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
                idMerk = Session["merk_form_id"].ToString();
                GenerateFormChange(idMerk);
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
        LblBC.Text = "Tambah Merk";
        LblHeader.Text = "Tambah Merk";

    }

    private void GenerateFormChange(string id)
    {
        HiddenId.Value = id;
        LblBC.Text = "Ubah Merk";
        LblHeader.Text = "Ubah Merk";

        SqlConnection conn = Common.getConnection();
        string query = "Select * from m_merk where id_merk = " + id;
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = query;

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();

        string sRole = "";

        reader.Read();
        HiddenId.Value = reader["id_merk"].ToString();
        TbMerk.Text = reader["nama_merk"].ToString();
        conn.Close();

    }

    private void TambahData()
    {
        string query = "";
        SqlConnection conn = Common.getConnection();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = query;

        
        cmd.Parameters.AddWithValue("@merkname", TbMerk.Text);
       


        query = "INSERT INTO m_merk (nama_merk) " +
            " VALUES(@merkname)";


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
        cmd.Parameters.AddWithValue("@merkname", TbMerk.Text);

        query = "update m_merk set nama_merk = @merkname where id_merk = @id";

        
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