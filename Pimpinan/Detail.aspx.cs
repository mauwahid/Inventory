using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class Pimpinan_Detail : System.Web.UI.Page
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
        string noPengajuan = (string)Session["pengajuan_id"];

        query = "select * from v_pengajuan where id_pengajuan like '" + noPengajuan + "'";
        cmd.CommandText = query;

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();
        reader.Read();
        HiddenIDPengajuan.Value = reader["id_pengajuan"].ToString();
        LblJudulPengajuan.Text = reader["judul_pengajuan"].ToString().Trim();
        LblKabag.Text = reader["kabag"].ToString();
      //  LblKeterangan.Text = reader["keterangan"].ToString();
        LblMemoKabag.Text = reader["catatan_kabag"].ToString();
        LblIDPengajuan.Text = reader["id_pengajuan"].ToString();
        try
        {
            LblTglPengajuan.Text = String.Format("{0:dd-MMM-yyyy}", (DateTime)reader["created_date"]);

        }
        catch
        {
            LblTglPengajuan.Text = reader["created_date"].ToString();
        }
        
        conn.Close();



    }

    protected void GvBind()
    {
        // string id = Session["no_pengajuan"].ToString();
        string id = (string)Session["pengajuan_id"];
        SqlDataSource dataSource = new SqlDataSource();
        dataSource.ConnectionString = ConfigurationManager.ConnectionStrings["InventoryConn"].ToString();
        dataSource.SelectCommand = "select p.id_pengajuan_detail, p.id_pengajuan_inventaris,inv.harga as harga_satuan,p.harga_total, p.id_inventaris, p.qty, p.catatan_kabag, p.created_date, " +
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
        Response.Redirect("ClosedList.aspx");    
    }
}