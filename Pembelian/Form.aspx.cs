using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Configuration;
using System.Data.SqlClient;
using System.Data;

public partial class Pembelian_Form : System.Web.UI.Page
{
    string formType = "add";

    protected void Page_Load(object sender, EventArgs e)
    {
        String idPembelian = "";
        if (Session["pembelian_form_type"] != null)
            formType = Session["pembelian_form_type"].ToString();

        if (!IsPostBack)
        {

            if (formType.Equals("add"))
            {
                GenerateFormTambah();
                InitDDLPengajuanBarang();
            }
            else if (formType.Equals("edit"))
            {
                idPembelian = Session["pembelian_form_id"].ToString();
                GenerateFormChange(idPembelian);
            }

        }
    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        string textId = HiddenIDPembelian.Value;
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
        LblBC.Text = "Tambah Pembelian";
        LblHeader.Text = "Tambah Pembelian";

    }

    private void GenerateFormChange(string id)
    {
        HiddenIDPembelian.Value = id;
        LblBC.Text = "Ubah Pembelian";
        LblHeader.Text = "Ubah Pembelian";

        SqlConnection conn = Common.getConnection();
        string query = "Select * from t_pembelian where id_pembelian = " + id;
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = query;

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();

        reader.Read();
        HiddenIDPembelian.Value = reader["id_pembelian"].ToString();

        conn.Close();

    }

    private void TambahData()
    {
        string query = "";
        SqlConnection conn = Common.getConnection();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = System.Data.CommandType.Text;

       
      //  cmd.Parameters.AddWithValue("@nopembelian", TbNoPembelian.Text);
        cmd.Parameters.AddWithValue("@tanggal", TbTanggalPembelian.Text);
        cmd.Parameters.AddWithValue("@id_pembelian", "BUY"+DateTime.Now.ToString("yyyyMMddHHmmss"));
        cmd.Parameters.AddWithValue("@keterangan", TbKeterangan.Text);
        cmd.Parameters.AddWithValue("@ref_id", DDLReferensiPengajuan.SelectedValue);
                
        
        query = "INSERT INTO t_pembelian (id_pembelian,id_pengajuan_inventaris, tanggal_pembelian, keterangan, harga_total)  " +
                " VALUES(@id_pembelian,  @ref_id,  @tanggal, @keterangan, (select harga_total from t_pengajuan_inventaris where id_pengajuan = @ref_id));";
        query = query + "UPDATE t_pengajuan_inventaris set is_sudah_beli = 1 where id_pengajuan = @ref_id";

        conn.Open();
        cmd.Connection = conn;
        cmd.CommandText = query;
        SqlDataReader reader = cmd.ExecuteReader();
        if (reader.HasRows)
        {
            reader.Read();
            Session["id_pembelian"] = reader["id_pembelian"].ToString();
         }

        conn.Close();
        // ViewState["no_pembelian"] = TbNoPembelian.Text;
        Response.Redirect("List.aspx");
    }


    private void UpdateData()
    {
        string query = "";
        SqlConnection conn = Common.getConnection();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = query;


        cmd.Parameters.AddWithValue("@id", HiddenIDPembelian.Value);
       // cmd.Parameters.AddWithValue("@nopembelian", TbNoPembelian.Text);
        cmd.Parameters.AddWithValue("@tanggal", TbTanggalPembelian.Text);
        cmd.Parameters.AddWithValue("@keterangan", TbKeterangan.Text);
        cmd.Parameters.AddWithValue("@ref_id", DDLReferensiPengajuan.SelectedValue);

        query = "UPDATE t_pembelian no_pembelian = @nopembelian, id_pengajuan = @ref_id, tanggal_pembelian = @tanggal, keterangan = @keterangan " +
            " where id = @id_pembelian";
     

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

        query = "select id_pengajuan, judul_pengajuan, (id_pengajuan + ' - '+judul_pengajuan) as title  from t_pengajuan_inventaris where status_konfirmasi = 1  and is_sudah_beli is null  order by id_pengajuan desc";
        cmd.CommandText = query;

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();

        DDLReferensiPengajuan.DataSource = reader;
        DDLReferensiPengajuan.DataValueField = "id_pengajuan";
        DDLReferensiPengajuan.DataTextField = "title";
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

        query = "select id_pengajuan_service, id_pengajuan from t_pengajuan_service where status_konfirmasi = 1  order by id_pengajuan_service desc";
        cmd.CommandText = query;

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();

        DDLReferensiPengajuan.DataSource = reader;
        DDLReferensiPengajuan.DataValueField = "id_pengajuan_servie";
        DDLReferensiPengajuan.DataTextField = "no_pengajuan_service";
        DDLReferensiPengajuan.DataBind();

        ListItem item = new ListItem();
        DDLReferensiPengajuan.Items.Insert(0, item);


        conn.Close();
    }

  }