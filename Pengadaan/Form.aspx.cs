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
        LblHeader.Text = "Tambah Pengajuan";

    }

    private void GenerateFormChange(string id)
    {
        HiddenId.Value = id;
        LblHeader.Text = "Ubah Pengajuan";

        SqlConnection conn = Common.getConnection();
        string query = "Select * from t_pengajuan_inventaris where id_pengajuan = " + id;
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = query;

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();

        reader.Read();
        HiddenId.Value = reader["id_pengajuan"].ToString();
      //  TbNoPengajuan.Text = reader["no_pengajuan"].ToString();
        TbJudulPengajuan.Text = reader["judul_pengajuan"].ToString();
        
        TbMemoKabag.Text = reader["catatan_kabag"].ToString();
        
        conn.Close();

    }

    private void TambahData()
    {
        string query = "";
        SqlConnection conn = Common.getConnection();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = query;

       // TbMemoKabag.
        String id = "INV"+ DateTime.Now.ToString("yyyyMMddHHmmss");
        cmd.Parameters.AddWithValue("@id_pengajuan", id);
        cmd.Parameters.AddWithValue("@judul_pengajuan", TbJudulPengajuan.Text);
     //   cmd.Parameters.AddWithValue("@no_pengajuan", TbNoPengajuan.Text);
        cmd.Parameters.AddWithValue("@memo_kabag", TbMemoKabag.Text);
        cmd.Parameters.AddWithValue("@id_user_kabag", Request.Cookies["UserSettings"]["id_user"]);
       // cmd.Parameters.AddWithValue("@id_user_kabag", "1");
        cmd.Parameters.AddWithValue("@status_prioritas", DDLPrioritas.SelectedValue);

        query = "INSERT INTO t_pengajuan_inventaris (id_pengajuan,judul_pengajuan, catatan_kabag, id_user,status_prioritas) " +
            " VALUES(@id_pengajuan,@judul_pengajuan, @memo_kabag, @id_user_kabag,@status_prioritas)";
     //   query = "INSERT INTO t_pengajuan_inventaris (id_pengajuan,no_pengajuan, judul_pengajuan, catatan_kabag, status_prioritas) " +
     //    " VALUES(@id_pengajuan,@no_pengajuan, @judul_pengajuan, @memo_kabag, @status_prioritas)";


        conn.Open();
        cmd.Connection = conn;
        cmd.CommandText = query;
        int result = cmd.ExecuteNonQuery();

        if (result > 0)
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
      //  cmd.Parameters.AddWithValue("@no_pengajuan", TbNoPengajuan.Text);
        cmd.Parameters.AddWithValue("@memo_kabag", TbMemoKabag.Text);
        cmd.Parameters.AddWithValue("@id_user_kabag", Request.Cookies["UserSettings"]["id_user"]);
    //    cmd.Parameters.AddWithValue("@id_user_kabag", "dar);
        cmd.Parameters.AddWithValue("@status_prioritas", DDLPrioritas.SelectedValue);

        query = "update t_pengajuan_inventaris set judul_pengajuan = @judul_pengajuan, memo_kabag=@memo_kabag,status_prioritas=@status_prioritas where id = @id";


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