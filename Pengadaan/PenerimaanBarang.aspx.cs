using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Configuration;
using System.Data.SqlClient;
using System.Data;


public partial class Pengadaan_PenerimaanBarang : System.Web.UI.Page
{
    String formType = "add";

    protected void Page_Load(object sender, EventArgs e)
    {
        String idPenerimaan = "";
        if (Session["penerimaan_form_type"] != null)
            formType = Session["penerimaan_form_type"].ToString();

        if (!IsPostBack)
        {

            if (formType.Equals("add"))
            {
                GenerateFormTambah();
            }
            else if (formType.Equals("edit"))
            {
                idPenerimaan = Session["penerimaan_form_id"].ToString();
                GenerateFormChange(idPenerimaan);
            }

        }
    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
      //  System.Diagnostics.Debug.WriteLine("HIII");
        string textId = HiddenId.Value;
        if (textId.Equals("0"))
        {
        //    System.Diagnostics.Debug.WriteLine("huu");
            TambahData();
        }
        else
        {
            UpdateData();
        }
    }

    private void GenerateFormTambah()
    {
        LblBC.Text = "Tambah Penerimaan";
        LblHeader.Text = "Tambah Penerimaan";

    }

    private void GenerateFormChange(string id)
    {
        HiddenId.Value = id;
        LblBC.Text = "Ubah Penerimaan";
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
        
        conn.Close();

    }

    private void TambahData()
    {
        System.Diagnostics.Debug.WriteLine("Tambah");
        string query = "";
        string tipe = "no";
        SqlConnection conn = Common.getConnection();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = System.Data.CommandType.Text;

        String id = "REC" + DateTime.Now.ToString("yyyyMMddHHmmss");
        System.Diagnostics.Debug.WriteLine(id);
      //  System.Diagnostics.Debug.WriteLine(TbNoPenerimaan.Text);
        System.Diagnostics.Debug.WriteLine(TbJudulPenerimaan.Text);
        System.Diagnostics.Debug.WriteLine(TbTanggalPenerimaan.Text);
       
        cmd.Parameters.AddWithValue("@id_penerimaan", id);
       // cmd.Parameters.AddWithValue("@no_penerimaan", TbNoPenerimaan.Text);
        cmd.Parameters.AddWithValue("@judul_penerimaan", TbJudulPenerimaan.Text);
        cmd.Parameters.AddWithValue("@tanggal_penerimaan", TbTanggalPenerimaan.Text);
        cmd.Parameters.AddWithValue("@keterangan", TbKeterangan.Text);
        cmd.Parameters.AddWithValue("@creator", Page.User.Identity.Name);

        

        switch (DDLTipePengajuan.SelectedIndex)
        {
            case 0 :
               cmd.Parameters.AddWithValue("@tipe", DDLTipePengajuan.SelectedIndex);
               query = "INSERT INTO t_penerimaan (id_penerimaan,  judul_penerimaan, tanggal_penerimaan, keterangan,tipe_ref,creator) " +
           " VALUES(@id_penerimaan, @judul_penerimaan,  @tanggal_penerimaan, @keterangan,@tipe,@creator)";
                break;
            case 1 :
                cmd.Parameters.AddWithValue("@tipe", DDLTipePengajuan.SelectedIndex);
                cmd.Parameters.AddWithValue("@ref_id", DDLReferensiPengajuan.SelectedValue);
                query = "INSERT INTO t_penerimaan (id_penerimaan, judul_penerimaan, id_pembelian, tanggal_penerimaan, keterangan, tipe_ref,creator) " +
                " VALUES(@id_penerimaan, @judul_penerimaan, @ref_id,  @tanggal_penerimaan, @keterangan, @tipe,@creator)";
                break;
            case 2:
                cmd.Parameters.AddWithValue("@tipe", DDLTipePengajuan.SelectedIndex);
                cmd.Parameters.AddWithValue("@ref_id", DDLReferensiPengajuan.SelectedValue);
                query = "INSERT INTO t_penerimaan (id_penerimaan, judul_penerimaan, id_service_keluar, tanggal_penerimaan, keterangan, tipe_ref,creator) " +
                " VALUES(@id_penerimaan, @judul_penerimaan, @ref_id,  @tanggal_penerimaan, @keterangan,@tipe,@creator)";
                break;
        }

       

        conn.Open();
        cmd.Connection = conn;
        cmd.CommandText = query;
        int res = cmd.ExecuteNonQuery();
        if (res>0)
        {
            Session["id_penerimaan"] = id;
            Session["tipe"] = tipe;

        }

        conn.Close();
        // ViewState["no_penerimaan"] = TbNoPenerimaan.Text;
        Response.Redirect("PenerimaanDetail.aspx");
      //  TbKeterangan.Text = Session["id_penerimaan"].ToString();
      //  System.Diagnostics.Debug.WriteLine("TEST");
       // System.Diagnostics.Debug.WriteLine("ID PEnerimaan: " + Session["id_penerimaan"].ToString());
    }


    private void UpdateData()
    {
        string query = "";
        SqlConnection conn = Common.getConnection();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = query;

        String id = "RECV" + DateTime.Now.ToString("yyyyMMddHHmmss");
        cmd.Parameters.AddWithValue("@id_penerimaan", id);
       
     //   cmd.Parameters.AddWithValue("@no_penerimaan", TbNoPenerimaan.Text);
        cmd.Parameters.AddWithValue("@judul_penerimaan", TbJudulPenerimaan.Text);
        cmd.Parameters.AddWithValue("@tanggal_penerimaan", TbTanggalPenerimaan.Text);
       // cmd.Parameters.AddWithValue("@ref_id", DDLReferensiPengajuan.SelectedValue);
        cmd.Parameters.AddWithValue("@keterangan", TbKeterangan.Text);


        query = "INSERT INTO t_penerimaan_inventaris (id_penerimaan,no_penerimaan, judul_penerimaan, id_pengajuan, tanggal_penerimaan, keterangan)  ouput Insert.id" +
            " VALUES(@id_penerimaan, @no_penerimaan, @judul_penerimaan, @ref_id, @tanggal_penerimaan, @keterangan)";



        conn.Open();
        cmd.Connection = conn;
        cmd.CommandText = query;
        cmd.ExecuteNonQuery();

        conn.Close();

        Response.Redirect("List.aspx");
    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("ListPenerimaan.aspx");
    }

    private void InitDDLPengajuanBarang()
    {
        SqlConnection conn = Common.getConnection();
        string query = "";
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;

        query = "select id_pembelian, no_pembelian from t_pembelian where id_pembelian not in(select id_pembelian from t_penerimaan where id_pembelian is not null) order by no_pembelian desc";
        cmd.CommandText = query;

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();

        DDLReferensiPengajuan.DataSource = reader;
        DDLReferensiPengajuan.DataValueField = "id_pembelian";
        DDLReferensiPengajuan.DataTextField = "id_pembelian";
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

//        query = "select id_service_keluar from v_service_keluar where status_konfirmasi = 1 and id_service_keluar not in (select id_service_keluar from t_penerimaan where id_service_keluar is not null)  order by id_service_keluar desc";

        query = "select id_service_keluar from v_service_keluar where status_konfirmasi = 1 order by id_service_keluar desc";
        cmd.CommandText = query;

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();

        DDLReferensiPengajuan.DataSource = reader;
        DDLReferensiPengajuan.DataValueField = "id_service_keluar";
        DDLReferensiPengajuan.DataTextField = "id_service_keluar";
        DDLReferensiPengajuan.DataBind();

        ListItem item = new ListItem();
        DDLReferensiPengajuan.Items.Insert(0, item);


        conn.Close();
    }
    
    protected void DDLTipePengajuan_SelectedIndexChanged(object sender, EventArgs e)
    {
        DDLReferensiPengajuan.Items.Clear();
        switch (DDLTipePengajuan.SelectedIndex)
        {
            case 0 :
                DDLReferensiPengajuan.Visible = false;
                LblRef.Visible = false;
                break;
            case 1 :
                DDLReferensiPengajuan.Visible = true;
                LblRef.Visible = true;
                LblRef.InnerText = "Referensi Pembelian";
                InitDDLPengajuanBarang();
                DDLReferensiPengajuan.SelectedIndex = 0;
                break;
            case 2 :
                DDLReferensiPengajuan.Visible = true;
                LblRef.Visible = true;
                LblRef.InnerText = "Referensi Service Keluar";
                InitDDLPengajuanService();
                DDLReferensiPengajuan.SelectedIndex = 0;
                break;
        }

       
    }
}