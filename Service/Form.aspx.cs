using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Configuration;
using System.Data.SqlClient;
using System.Data;

public partial class Pengadaan_Form : System.Web.UI.Page
{
    String formType = "add";

    protected void Page_Load(object sender, EventArgs e)
    {
        String idPengajuan = "";
        if(Session["pengajuan_form_type"]!=null)
            formType = Session["pengajuan_form_type"].ToString();

        InitDDLLokasiService();

        if (!IsPostBack)
        {

            if (formType.Equals("add"))
            {
                GenerateFormTambah();
            }
            else if (formType.Equals("edit"))
            {
                idPengajuan = Session["pengajuan_form_id"].ToString();
                GenerateFormChange(idPengajuan);
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
        LblHeader.Text = "Tambah Service";

    }

    private void GenerateFormChange(string id)
    {
        HiddenId.Value = id;
        LblHeader.Text = "Ubah Pengajuan";

        SqlConnection conn = Common.getConnection();
        string query = "Select * from t_pengajuan_service where id_service = " + id;
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = query;

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();

        reader.Read();
        HiddenId.Value = reader["id"].ToString();
      //  TbIDPengajuan.Text = reader["no_pengajuan"].ToString();
        TbJudulPengajuan.Text = reader["judul_pengajuan"].ToString();
        try
        {
            DDLLokasiService.SelectedValue = reader["id_lokasi_service"].ToString();
            DDLPrioritas.SelectedValue = reader["status_prioritas"].ToString();
        }
        catch
        {
        }
        TbMemoKabag.Text = reader["memo_kabag"].ToString();
        
        conn.Close();

    }

    private void TambahData()
    {
        string query = "";
        SqlConnection conn = Common.getConnection();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = query;

        cmd.Parameters.AddWithValue("@judul_pengajuan", TbJudulPengajuan.Text);
    //    cmd.Parameters.AddWithValue("@no_pengajuan", TbNoPengajuan.Text);
        cmd.Parameters.AddWithValue("@memo_kabag", TbMemoKabag.Text);
        cmd.Parameters.AddWithValue("@id_lokasi_service", DDLLokasiService.SelectedValue);

       cmd.Parameters.AddWithValue("@id_user_kabag", Request.Cookies["UserSettings"]["id_user"]);
     //   cmd.Parameters.AddWithValue("@id_user_kabag", "1");
        cmd.Parameters.AddWithValue("@status_prioritas", DDLPrioritas.SelectedValue);
        String id = "SVI" + DateTime.Now.ToString("yyyyMMddHHmmss");
        cmd.Parameters.AddWithValue("@id_service", id);
        
        query = "INSERT INTO t_pengajuan_service (id_pengajuan_service, judul_pengajuan_service, catatan_kabag, id_user,  status_prioritas, id_lokasi_service) " +
            "  VALUES(@id_service, @judul_pengajuan, @memo_kabag, @id_user_kabag,@status_prioritas, @id_lokasi_service)";


        conn.Open();
        cmd.Connection = conn;
        cmd.CommandText = query;
        int ret = cmd.ExecuteNonQuery();

        if (ret>0)
        {
            Session["id_pengajuan"] = id;
        }

        conn.Close();

       // ViewState["no_pengajuan"] = TbNoPengajuan.Text;
        Response.Redirect("FormDetailBaru.aspx");
    }


    private void UpdateData()
    {
        string query = "";
        SqlConnection conn = Common.getConnection();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = query;


        cmd.Parameters.AddWithValue("@judul_pengajuan", TbJudulPengajuan.Text);
       // cmd.Parameters.AddWithValue("@no_pengajuan", TbNoPengajuan.Text);
        cmd.Parameters.AddWithValue("@memo_kabag", TbMemoKabag.Text);
        cmd.Parameters.AddWithValue("@id_lokasi_service", DDLLokasiService.SelectedValue);
        cmd.Parameters.AddWithValue("@status_prioritas", DDLPrioritas.SelectedValue);
        //    cmd.Parameters.AddWithValue("@id_user_kabag", TbWebsite.Text);

        query = "update m_pengajuan set nama_pengajuan = @pengajuanname, id_lokasi_service = @id_lokasi_service, status_prioritas=@status_prioritas where id = @id";


        conn.Open();
        cmd.Connection = conn;
        cmd.CommandText = query;
        cmd.ExecuteNonQuery();

        conn.Close();

        Response.Redirect("List.aspx");
    }
    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Draft.aspx");
    }


    protected bool InitDDLLokasiService()
    {
        SqlConnection conn = Common.getConnection();
        string query = "";
        bool val = false;
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;

        query = "select id_lokasi_service, nama_lokasi_service from m_lokasi_service order by nama_lokasi_service asc";
        cmd.CommandText = query;

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();

        if (reader.HasRows)
        {
            val = true;
            DDLLokasiService.DataSource = reader;
            DDLLokasiService.DataValueField = "id_lokasi_service";
            DDLLokasiService.DataTextField = "nama_lokasi_service";
            DDLLokasiService.DataBind();
            //   DDLHarga.DataBind();
        }


        conn.Close();
        return val;
    }
   
}