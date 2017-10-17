using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class Penugasan_ServiceDetail : System.Web.UI.Page
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
        string idService = (string)Session["id_service"];

        query = "select * from v_service where id_service like '" + idService + "'";
        cmd.CommandText = query;

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();
        reader.Read();
        HiddenIDPengajuan.Value = reader["id"].ToString();
        LblJudulPengajuan.Text = reader["judul_pengajuan"].ToString().Trim();
        LblKabag.Text = reader["kabag"].ToString();
        LblKeterangan.Text = reader["keterangan"].ToString();
        LblMemoKabag.Text = reader["memo_kabag"].ToString();
        LblNoPengajuan.Text = reader["no_pengajuan"].ToString();
        LblTglPengajuan.Text = reader["created_date"].ToString();

        conn.Close();



    }

    protected void GvBind()
    {
        // string id = Session["no_pengajuan"].ToString();
        string id = (string)Session["pengajuan_id"];
        SqlDataSource dataSource = new SqlDataSource();
        dataSource.ConnectionString = ConfigurationManager.ConnectionStrings["InventoryConn"].ToString();
        dataSource.SelectCommand = "select p.id, p.id_pengajuan, p.id_inventaris, p.qty, p.memo_kabag, p.gambar,p.created_date, p.harga_satuan, p.harga_total," +
            " pin.no_pengajuan, inv.nama_inventaris from t_service_inventaris_detail p inner join m_inventaris inv on p.id_inventaris = inv.id " +
            " inner join t_service_inventaris pin on p.id_pengajuan = pin.id " +
            " where pin.id = '" + id + "'";

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
        Response.Redirect("OnProgressTeknisiDetail.aspx");
    }
}