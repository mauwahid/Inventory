using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Configuration;
using System.Data.SqlClient;
using System.Data;

public partial class Admin_Inventaris_Form : System.Web.UI.Page
{
    String formType = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        String idInventaris = "";
        formType = Session["inventaris_form_type"].ToString();

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
                TbQty.Enabled = false;
                idInventaris = Session["inventaris_form_id"].ToString();
                GenerateFormChange(idInventaris);
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
        LblBC.Text = "Tambah Inventaris";
        LblHeader.Text = "Tambah Inventaris";
        GenerateDDLDistributor();
        GenerateDDLJenisInventaris();
        GenerateDDLMerk();
       

    }

    private void GenerateFormChange(string id)
    {
        HiddenId.Value = id;
        LblBC.Text = "Ubah Inventaris";
        LblHeader.Text = "Ubah Inventaris";

        SqlConnection conn = Common.getConnection();
        string query = "Select * from m_inventaris where id_inventaris = " + id;
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = query;

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();


        reader.Read();
        HiddenId.Value = reader["id_inventaris"].ToString();
        TbInventaris.Text = reader["nama_inventaris"].ToString();
        TbHarga.Text = reader["harga"].ToString();
        TbKeterangan.Text = reader["keterangan"].ToString();
        TbQty.Text = reader["qty"].ToString();
        GenerateDDLJenisInventaris();
        GenerateDDLMerk();
        GenerateDDLDistributor();

        DDLJenisInventaris.SelectedValue = reader["id_jenis_inventaris"].ToString();
        DDLDistributor.SelectedValue = reader["id_distributor"].ToString();
        DDLMerk.SelectedValue = reader["id_merk"].ToString();
        conn.Close();

    }

    private void TambahData()
    {
        string query = "";
        SqlConnection conn = Common.getConnection();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = query;


        cmd.Parameters.AddWithValue("@inventaris", TbInventaris.Text);
        cmd.Parameters.AddWithValue("@harga", TbHarga.Text);
        cmd.Parameters.AddWithValue("@keterangan", TbKeterangan.Text);
        cmd.Parameters.AddWithValue("@id_jenis_inventaris", DDLJenisInventaris.SelectedValue);
        cmd.Parameters.AddWithValue("@id_merk", DDLMerk.SelectedValue);
        cmd.Parameters.AddWithValue("@id_distributor", DDLDistributor.SelectedValue);
        cmd.Parameters.AddWithValue("@qty", TbQty.Text);

        query = "INSERT INTO m_inventaris (nama_inventaris, harga, keterangan, id_merk, id_distributor, qty, id_jenis_inventaris) " +
            " VALUES(@inventaris, @harga, @keterangan, @id_merk, @id_distributor, @qty, @id_jenis_inventaris)";


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
        cmd.Parameters.AddWithValue("@inventaris", TbInventaris.Text);
        cmd.Parameters.AddWithValue("@harga", TbHarga.Text);
        cmd.Parameters.AddWithValue("@keterangan", TbKeterangan.Text);
        cmd.Parameters.AddWithValue("@id_jenis_inventaris", DDLJenisInventaris.SelectedValue);
        cmd.Parameters.AddWithValue("@id_merk", DDLMerk.SelectedValue);
        cmd.Parameters.AddWithValue("@id_distributor", DDLDistributor.SelectedValue);
        cmd.Parameters.AddWithValue("@qty", TbQty.Text);

        query = "update m_inventaris set nama_inventaris = @inventaris,  "
        +"harga=@harga, keterangan=@keterangan, id_jenis_inventaris = @id_jenis_inventaris, id_merk =@id_merk, id_distributor=@id_distributor, qty=@qty  where id_inventaris = @id";


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


    private void GenerateDDLJenisInventaris()
    {

        SqlConnection conn = Common.getConnection();
        string query = "";
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;

        query = "select id_jenis_inventaris, nama_jenis_inventaris from m_jenis_inventaris ";
        cmd.CommandText = query;

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();

        DDLJenisInventaris.DataSource = reader;
        DDLJenisInventaris.DataValueField = "id_jenis_inventaris";
        DDLJenisInventaris.DataTextField = "nama_jenis_inventaris";
        DDLJenisInventaris.DataBind();

        conn.Close();
    }

    private void GenerateDDLDistributor()
    {

        SqlConnection conn = Common.getConnection();
        string query = "";
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;

        query = "select id_distributor, nama_distributor from m_distributor ";
        cmd.CommandText = query;

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();

        DDLDistributor.DataSource = reader;
        DDLDistributor.DataValueField = "id_distributor";
        DDLDistributor.DataTextField = "nama_distributor";
        DDLDistributor.DataBind();

        conn.Close();
    }


    private void GenerateDDLMerk()
    {

        SqlConnection conn = Common.getConnection();
        string query = "";
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;

        query = "select id_merk, nama_merk from m_merk ";
        cmd.CommandText = query;

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();

        DDLMerk.DataSource = reader;
        DDLMerk.DataValueField = "id_merk";
        DDLMerk.DataTextField = "nama_merk";
        DDLMerk.DataBind();

        conn.Close();
    }
}