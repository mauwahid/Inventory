using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Configuration;
using System.Data.SqlClient;
using System.Data;

public partial class Admin_Distributor_Form : System.Web.UI.Page
{
    String formType = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        String idDistributor = "";
        formType = Session["distributor_form_type"].ToString();

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
                idDistributor = Session["distributor_form_id"].ToString();
                GenerateFormChange(idDistributor);
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
        LblBC.Text = "Tambah Distributor";
        LblHeader.Text = "Tambah Distributor";

    }

    private void GenerateFormChange(string id)
    {
        HiddenId.Value = id;
        LblBC.Text = "Ubah Distributor";
        LblHeader.Text = "Ubah Distributor";

        SqlConnection conn = Common.getConnection();
        string query = "Select * from m_distributor where id_distributor = " + id;
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = query;

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();

        reader.Read();
        HiddenId.Value = reader["id_distributor"].ToString();
        TbDistributor.Text = reader["nama_distributor"].ToString();
        TbAlamat.Text = reader["alamat"].ToString();
        TbNoTelp.Text = reader["no_telp"].ToString();
        TbEmail.Text = reader["email"].ToString();
        TbWebsite.Text = reader["website"].ToString();

        conn.Close();

    }

    private void TambahData()
    {
        string query = "";
        SqlConnection conn = Common.getConnection();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = query;

        cmd.Parameters.AddWithValue("@distributor", TbDistributor.Text);
        cmd.Parameters.AddWithValue("@alamat", TbAlamat.Text);
        cmd.Parameters.AddWithValue("@email", TbEmail.Text);
        cmd.Parameters.AddWithValue("@notelp", TbNoTelp.Text);
        cmd.Parameters.AddWithValue("@website", TbWebsite.Text);

        query = "INSERT INTO m_distributor (nama_distributor, alamat, email, no_telp, website) " +
            " VALUES(@distributor, @alamat, @email, @notelp, @website)";


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
        cmd.Parameters.AddWithValue("@distributorname", TbDistributor.Text);
        cmd.Parameters.AddWithValue("@alamat", TbAlamat.Text);
        cmd.Parameters.AddWithValue("@email", TbEmail.Text);
        cmd.Parameters.AddWithValue("@notelp", TbNoTelp.Text);
        cmd.Parameters.AddWithValue("@website", TbWebsite.Text);


        query = "update m_distributor set nama_distributor = @distributorname, alamat=@alamat, email=@email, no_telp=@notelp, website=@website where id_distributor = @id";


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