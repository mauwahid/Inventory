using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;


public partial class Pengadaan_Detail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GvBind();
            BindPengajuan();
        }

    }

    protected void BindPengajuan()
    {
        SqlConnection conn = Common.getConnection();
        string query = "";
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;
        String idPengajuan = Session["id_pengajuan"].ToString();

        query = "select * from v_pengajuan where id_pengajuan like '"  +idPengajuan+ "'";
        cmd.CommandText = query;

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();
        reader.Read();
        HiddenIDPengajuan.Value = reader["id_pengajuan"].ToString();
        LblJudulPengajuan.Text = reader["judul_pengajuan"].ToString().Trim();
        LblKabag.Text = reader["kabag"].ToString();
    //    LblKeterangan.Text = reader["keterangan"].ToString();
        LblMemoKabag.Text = reader["catatan_kabag"].ToString();
     //   LblNoPengajuan.Text = reader["no_pengajuan"].ToString();
        LblTglPengajuan.Text = reader["created_date"].ToString();
        LblID.Text = reader["id_pengajuan"].ToString();
        LblTglApproval.Text = reader["approval_date"].ToString();
        LblCatatanPimpinan.Text = reader["catatan_pimpinan"].ToString();
        
        string prioritas = reader["status_prioritas"].ToString();
        string status = reader["status_konfirmasi"].ToString();

        if (prioritas.Equals("1"))
        {
            LblPrioritas.Text = "URGENT";
        }
        else if (prioritas.Equals("2"))
        {
            LblPrioritas.Text = "PENTING";
        }
        else
        {
            LblPrioritas.Text = "NORMAL";
        }

        if (status.Equals("1"))
        {
            LblStatusPengajuan.Text = "Disetujui";
        }
        else if (status.Equals("2"))
        {
            LblStatusPengajuan.Text = "Ditolak";
        }
        else
        {
            LblStatusPengajuan.Text = "Belum Direspon";
        }

        conn.Close();



    }

    protected void GvBind()
    {
        // string id = Session["no_pengajuan"].ToString();
        string id = (string)Session["id_pengajuan"];
        SqlDataSource dataSource = new SqlDataSource();
        dataSource.ConnectionString = ConfigurationManager.ConnectionStrings["InventoryConn"].ToString();
     /*   dataSource.SelectCommand = "select p.id, p.id_pengajuan, p.id_jenis_inventaris, p.qty, p.memo_kabag, p.gambar,p.created_date, " +
            " pin.no_pengajuan, inv.nama_jenis from t_pengajuan_inventaris_detail p inner join m_jenis_inventaris inv on p.id_jenis_inventaris = inv.id " +
            " inner join t_pengajuan_inventaris pin on p.id_pengajuan = pin.id " +
            " where pin.id_pengajuan like '" + id + "'";
        */
        dataSource.SelectCommand = "select p.id_pengajuan_detail, p.id_pengajuan_inventaris, p.id_inventaris, p.qty, p.catatan_kabag,p.created_date, p.harga_satuan, p.harga_total," +
            " pin.no_pengajuan, inv.nama_inventaris from t_pengajuan_inventaris_detail p inner join m_inventaris inv on p.id_inventaris = inv.id_inventaris " +
            " inner join t_pengajuan_inventaris pin on p.id_pengajuan_inventaris = pin.id_pengajuan " +
            " where pin.id_pengajuan = '" + id + "'";

        GridView1.DataSource = dataSource;
        GridView1.DataBind();

    }

    private void blankTable()
    {
        GridView1.DataSource = new List<String>();
        GridView1.DataBind();
    }

    protected void GView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GvBind();
    }

   
    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("TelahDiajukan.aspx");    
    }
}