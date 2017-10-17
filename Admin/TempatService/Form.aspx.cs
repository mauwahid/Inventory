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
        formType = Session["lokasi_service_form_type"].ToString();

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
                idDistributor = Session["lokasi_service_form_id"].ToString();
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
        LblBC.Text = "Tambah Lokasi Service";
        LblHeader.Text = "Tambah Lokasi Service";

    }

    private void GenerateFormChange(string id)
    {
        HiddenId.Value = id;
        LblBC.Text = "Ubah Lokasi Service";
        LblHeader.Text = "Ubah Lokasi Service";

        SqlConnection conn = Common.getConnection();
        string query = "Select * from m_lokasi_service where id_lokasi_service = " + id;
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = query;

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();

        reader.Read();
        HiddenId.Value = reader["id_lokasi_service"].ToString();
        TbLokasiService.Text = reader["nama_lokasi_service"].ToString();
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

        cmd.Parameters.AddWithValue("@lokasiService", TbLokasiService.Text);
        cmd.Parameters.AddWithValue("@alamat", TbAlamat.Text);
        cmd.Parameters.AddWithValue("@email", TbEmail.Text);
        cmd.Parameters.AddWithValue("@notelp", TbNoTelp.Text);
        cmd.Parameters.AddWithValue("@website", TbWebsite.Text);

        query = "INSERT INTO m_lokasi_service (nama_lokasi_service, alamat, email, no_telp, website) " +
            " VALUES(@lokasiService, @alamat, @email, @notelp, @website)";


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
        cmd.Parameters.AddWithValue("@lokasiService", TbLokasiService.Text);
        cmd.Parameters.AddWithValue("@alamat", TbAlamat.Text);
        cmd.Parameters.AddWithValue("@email", TbEmail.Text);
        cmd.Parameters.AddWithValue("@notelp", TbNoTelp.Text);
        cmd.Parameters.AddWithValue("@website", TbWebsite.Text);

        query = "INSERT INTO m_lokasi_service (nama_lokasi_service, alamat, email, no_telp, website) " +
            " VALUES(@lokasiService, @alamat, @email, @notelp, @website)";

        query = "update m_lokasi_service set nama_lokasi_service = @lokasiService, alamat=@alamat, email=@email, no_telp=@notelp, website=@website where id_lokasi_service = @id";


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