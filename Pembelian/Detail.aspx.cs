using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;


public partial class Pembelian_Detail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindPembelian();
           // GvBind();
           
        }

    }

    protected void BindPembelian()
    {
        SqlConnection conn = Common.getConnection();
        string query = "";
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;
        String idPembelian = Session["id_pembelian"].ToString();


        query = "select beli.*, aju.no_pengajuan, aju.id_pengajuan from t_pembelian beli inner join t_pengajuan_inventaris aju " +
        "on beli.id_pengajuan_inventaris = aju.id_pengajuan where beli.id_pembelian = '"+idPembelian+"'";
        
        cmd.CommandText = query;

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();
        reader.Read();
        HiddenIDPengajuan.Value = reader["id_pengajuan_inventaris"].ToString();
        LblID.Text = reader["id_pembelian"].ToString().Trim();

        try
        {
            LblTglPembelian.Text = String.Format("{0:dd-MMM-yyyy}", (DateTime)reader["tanggal_pembelian"]);
        }
        catch
        {
            LblTglPembelian.Text = reader["tanggal_pembelian"].ToString();
        }
        

        LblKeterangan.Text = reader["keterangan"].ToString();
        LblIDPengajuan.Text = reader["id_pengajuan"].ToString();
     //   LblNoPembelian.Text = reader["no_pembelian"].ToString();
    //    LblJudul.Text = reader["judul_pembelian"].ToString();
        LblHargaTotal.Text = reader["harga_total"].ToString();
      //  string idPengajuan = reader["id_pengajuan"].ToString();
        
       
        conn.Close();

        GvBind(HiddenIDPengajuan.Value);


    }

    protected void GvBind(string idPengajuan)
    {
        // string id = Session["no_pengajuan"].ToString();
        string id = HiddenIDPengajuan.Value;
        SqlDataSource dataSource = new SqlDataSource();
        dataSource.ConnectionString = ConfigurationManager.ConnectionStrings["InventoryConn"].ToString();
        /*   dataSource.SelectCommand = "select p.id, p.id_pengajuan, p.id_jenis_inventaris, p.qty, p.memo_kabag, p.gambar,p.created_date, " +
               " pin.no_pengajuan, inv.nama_jenis from t_pengajuan_inventaris_detail p inner join m_jenis_inventaris inv on p.id_jenis_inventaris = inv.id " +
               " inner join t_pengajuan_inventaris pin on p.id_pengajuan = pin.id " +
               " where pin.id_pengajuan like '" + id + "'";
           */
        string query = "select p.id_pengajuan_detail, p.id_pengajuan_inventaris, p.id_inventaris, p.qty, p.catatan_kabag, p.created_date, p.harga_satuan, p.harga_total," +
            " pin.no_pengajuan, inv.nama_inventaris from t_pengajuan_inventaris_detail p inner join m_inventaris inv on p.id_inventaris = inv.id_inventaris " +
            " inner join t_pengajuan_inventaris pin on p.id_pengajuan_inventaris = pin.id_pengajuan " +
            " where pin.id_pengajuan = '" + id + "'";

        dataSource.SelectCommand = query;

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
        GvBind(HiddenIDPengajuan.Value);
    }


    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("List.aspx");
    }
}