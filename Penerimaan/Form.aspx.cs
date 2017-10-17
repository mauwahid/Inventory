using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Configuration;
using System.Data.SqlClient;
using System.Data;

public partial class Penerimaan_Form : System.Web.UI.Page
{
    String formType = "add";

    protected void Page_Load(object sender, EventArgs e)
    {
        String idPengajuan = "";
        if(Session["penerimaan_form_type"]!=null)
            formType = Session["penerimaan_form_type"].ToString();

        if (!IsPostBack)
        {
            InitDDLPengajuan();

            if (formType.Equals("add"))
            {
                GenerateFormTambah();
            }
            else if (formType.Equals("edit"))
            {
                idPengajuan = Session["penerimaan_form_id"].ToString();
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
        LblHeader.Text = "Tambah Penerimaan";

    }

    private void GenerateFormChange(string id)
    {
        HiddenId.Value = id;
        LblHeader.Text = "Ubah Penerimaan";

        SqlConnection conn = Common.getConnection();
        string query = "Select * from t_penerimaan_inventaris where id = " + id;
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = query;

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();

        reader.Read();
        HiddenId.Value = reader["id"].ToString();
        TbNoPengajuan.Text = reader["no_penerimaan"].ToString();
        TbJudulPenerimaan.Text = reader["judul_penerimaan"].ToString();

        string idPengajuan = "";
        idPengajuan = reader["id_pengajuan"].ToString();

        if (!idPengajuan.Equals(""))
        {
            DDLPengajuan.SelectedValue = idPengajuan;
        }
        

        TbMemoKabag.Text = reader["keterangan"].ToString();
        
        conn.Close();

    }

    private void InitDDLPengajuan()
    {
        SqlConnection conn = Common.getConnection();
        string query = "";
        bool val = false;
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;

        query = "select id, no_pengajuan from t_pengajuan_inventaris";
        cmd.CommandText = query;

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();

        if (reader.HasRows)
        {
            val = true;
            DDLPengajuan.DataSource = reader;
            DDLPengajuan.DataValueField = "id";
            DDLPengajuan.DataTextField = "no_pengajuan";
            DDLPengajuan.DataBind();
            //   DDLHarga.DataBind();

            ListItem item = new ListItem();
            DDLPengajuan.Items.Insert(0, item);
            DDLPengajuan.SelectedIndex = 0;
        }

        conn.Close();
    }

    private void TambahData()
    {
        string query = "";
        SqlConnection conn = Common.getConnection();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = query;

        cmd.Parameters.AddWithValue("@judul_penerimaan", TbJudulPenerimaan.Text);
        cmd.Parameters.AddWithValue("@no_penerimaan", TbNoPengajuan.Text);
        cmd.Parameters.AddWithValue("@id_penerimaan", "REC" + DateTime.Now.ToString("yyyyMMddHHmmss"));
        cmd.Parameters.AddWithValue("@keterangan", TbMemoKabag.Text);
        cmd.Parameters.AddWithValue("@id_user_kabag", Request.Cookies["UserSettings"]["id_user"]);
        cmd.Parameters.AddWithValue("@id_pengajuan", DDLPengajuan.SelectedValue);

        query = "INSERT INTO t_penerimaan_inventaris (id_penerimaan,no_penerimaan, judul_penerimaan, keterangan, id_user_kabag,id_pengajuan) " +
            " Output Inserted.id VALUES(@id_penerimaan, @no_penerimaan, @judul_penerimaan, @keterangan, @id_user_kabag,@id_pengajuan)";


        conn.Open();
        cmd.Connection = conn;
        cmd.CommandText = query;
        SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Session["id_penerimaan"] = reader["id"].ToString();
        }

        conn.Close();

       // ViewState["no_penerimaan"] = TbNoPengajuan.Text;
        Response.Redirect("FormDetailBaru.aspx");
    }


    private void UpdateData()
    {
        string query = "";
        SqlConnection conn = Common.getConnection();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = query;


        cmd.Parameters.AddWithValue("@judul_penerimaan", TbJudulPenerimaan.Text);
        cmd.Parameters.AddWithValue("@no_penerimaan", TbNoPengajuan.Text);
        cmd.Parameters.AddWithValue("@keterangan", TbMemoKabag.Text);
        cmd.Parameters.AddWithValue("@id_user_kabag", Request.Cookies["UserSettings"]["id_user"]);
        cmd.Parameters.AddWithValue("@id_pengajuan", DDLPengajuan.SelectedValue);
        query = "update t_penerimaan_inventaris set judul_penerimaan = @judul_penerimaan, no_penerimaan=@no_penerimaan,keterangan=@keterangan,id_pengajuan=@id_pengajuan where id = @id";


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

   
}