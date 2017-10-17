using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Configuration;
using System.Data.SqlClient;
using System.Data;

public partial class ServiceKeluar_Form : System.Web.UI.Page
{
    string formType = "add";

    protected void Page_Load(object sender, EventArgs e)
    {
        String idServiceKeluar = "";
        if (Session["service_keluar_form_type"] != null)
            formType = Session["service_keluar_form_type"].ToString();

        if (!IsPostBack)
        {

            if (formType.Equals("add"))
            {
                GenerateFormTambah();
                InitDDLPengajuanService();
            }
            else if (formType.Equals("edit"))
            {
                idServiceKeluar = Session["service_keluar_form_id"].ToString();
                InitDDLPengajuanService();
                GenerateFormChange(idServiceKeluar);
            }

        }
    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        string textId = HiddenIDServiceKeluar.Value;
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
        LblBC.Text = "Tambah Service Keluar";
        LblHeader.Text = "Tambah Service Keluar";

    }

    private void GenerateFormChange(string id)
    {
        HiddenIDServiceKeluar.Value = id;
        LblBC.Text = "Ubah Service Keluar";
        LblHeader.Text = "Ubah Service Keluar";

        SqlConnection conn = Common.getConnection();
        string query = "Select * from t_service_keluar where id = " + id;
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = query;

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();

        reader.Read();
        HiddenIDServiceKeluar.Value = reader["id"].ToString();

        conn.Close();

    }

    private void TambahData()
    {
        string query = "";
        SqlConnection conn = Common.getConnection();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = System.Data.CommandType.Text;

        cmd.Parameters.AddWithValue("@judul", TbJudulServiceKeluar.Text);
        cmd.Parameters.AddWithValue("@tanggal", TbTanggalServiceKeluar.Text);
        cmd.Parameters.AddWithValue("@keterangan", TbKeterangan.Text);
        cmd.Parameters.AddWithValue("@ref_id", DDLReferensiPengajuan.SelectedValue);
        String id = "SVO" + DateTime.Now.ToString("yyyyMMddHHmmss");
        cmd.Parameters.AddWithValue("@id_service", id);
        cmd.Parameters.AddWithValue("@id_user", Request.Cookies["UserSettings"]["id_user"]);



        query = "INSERT INTO t_service_keluar (id_service_keluar,  judul, id_pengajuan_service, tanggal_service, keterangan, id_user) " +
                " VALUES(@id_service, @judul, @ref_id,  @tanggal, @keterangan,@id_user)";


        conn.Open();
        cmd.Connection = conn;
        cmd.CommandText = query;
        int res = cmd.ExecuteNonQuery();
        if (res>0)
        {
           // reader.Read();
            Session["id_service_keluar"] = id;
        }

        conn.Close();
        // ViewState["no_service_keluar"] = TbNoService Keluar.Text;
        Response.Redirect("List.aspx");
    }


    private void UpdateData()
    {
        string query = "";
        SqlConnection conn = Common.getConnection();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = query;


        cmd.Parameters.AddWithValue("@id", HiddenIDServiceKeluar.Value);
        cmd.Parameters.AddWithValue("@judul", TbJudulServiceKeluar.Text);
        cmd.Parameters.AddWithValue("@tanggal", TbTanggalServiceKeluar.Text);
        cmd.Parameters.AddWithValue("@keterangan", TbKeterangan.Text);
        cmd.Parameters.AddWithValue("@ref_id", DDLReferensiPengajuan.SelectedValue);

        query = "UPDATE t_service_keluar judul = @judul, id_pengajuan = @ref_id, tanggal = @tanggal, keterangan = @keterangan " +
            " where id = @id";


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

    private void InitDDLPengajuanBarang()
    {
        SqlConnection conn = Common.getConnection();
        string query = "";
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;

        query = "select id, no_pengajuan from t_pengajuan_inventaris where status_approval = 1  order by id desc";
        cmd.CommandText = query;

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();

        DDLReferensiPengajuan.DataSource = reader;
        DDLReferensiPengajuan.DataValueField = "id";
        DDLReferensiPengajuan.DataTextField = "no_pengajuan";
        DDLReferensiPengajuan.DataBind();

        ListItem item = new ListItem();
        DDLReferensiPengajuan.Items.Insert(0, item);


        conn.Close();
    }

    private void InitDDLPengajuanService()
    {
        SqlConnection conn = Common.getConnection();
        string query = "";
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;

        query = "select id_pengajuan_service, no_pengajuan_service from t_pengajuan_service where status_konfirmasi = 1  order by id_pengajuan_service desc";
        cmd.CommandText = query;

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();

        DDLReferensiPengajuan.DataSource = reader;
        DDLReferensiPengajuan.DataValueField = "id_pengajuan_service";
        DDLReferensiPengajuan.DataTextField = "id_pengajuan_service";
        DDLReferensiPengajuan.DataBind();

        ListItem item = new ListItem();
        DDLReferensiPengajuan.Items.Insert(0, item);


        conn.Close();
    }

}