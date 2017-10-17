using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;


public partial class Pimpinan_Approval : System.Web.UI.Page
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

        query = "select * from v_service where id_pengajuan_service like '" + noPengajuan + "'";
        cmd.CommandText = query;

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();
        reader.Read();
        HiddenIDPengajuan.Value = reader["id_pengajuan_service"].ToString();
        LblJudulPengajuan.Text = reader["judul_pengajuan_service"].ToString().Trim();
        LblKabag.Text = reader["kabag"].ToString();
         LblMemoKabag.Text = reader["catatan_kabag"].ToString();
        LblIDPengajuan.Text = reader["id_pengajuan_service"].ToString();

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
     /*   dataSource.SelectCommand = "select p.id, p.id_pengajuan, p.id_inventaris, p.qty, p.memo_kabag, p.gambar,p.created_date, " +
            " pin.no_pengajuan, inv.nama_inventaris from t_service_inventaris_detail p inner join m_inventaris inv on p.id_inventaris = inv.id " +
            " inner join t_service_inventaris pin on p.id_pengajuan = pin.id " +
            " where pin.id like '" + id + "'";
        */
        dataSource.SelectCommand = "select p.id_pengajuan_service_detail, p.id_pengajuan_service, p.id_inventaris, p.qty, p.catatan_kabag, p.created_date, p.biaya_service," +
           " pin.no_pengajuan_service, inv.nama_inventaris from t_pengajuan_service_detail p inner join m_inventaris inv on p.id_inventaris = inv.id_inventaris " +
           " inner join t_pengajuan_service pin on p.id_pengajuan_service = pin.id_pengajuan_service " +
           " where pin.id_pengajuan_service = '" + id + "'";
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
        Response.Redirect("PengajuanBaru.aspx");
    }
    protected void BtnSetuju_Click(object sender, EventArgs e)
    {
        string query = "";
        SqlConnection conn = Common.getConnection();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = query;
       

        cmd.Parameters.AddWithValue("@id_pengajuan", HiddenIDPengajuan.Value);
        cmd.Parameters.AddWithValue("@memo", TbCatatan.Text);
        cmd.Parameters.AddWithValue("@id_user_pimpinan", Request.Cookies["UserSettings"]["id_user"]);
      //  cmd.Parameters.AddWithValue("@id_user_pimpinan", "1");
        cmd.Parameters.AddWithValue("@approval_date", DateTime.Now.ToString("yyyy-MM-dd HH:MM:ss"));


        query = "Update t_pengajuan_service set status_konfirmasi = 1, catatan_pimpinan = @memo, id_user_pimpinan = @id_user_pimpinan, approval_date = @approval_date where id_pengajuan_service = @id_pengajuan";


        conn.Open();
        cmd.Connection = conn;
        cmd.CommandText = query;
        cmd.ExecuteNonQuery();

        conn.Close();

        GvBind();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alertSukses();", true);
        Response.Redirect("ClosedList.aspx");
    }
    protected void BtnRejected_Click(object sender, EventArgs e)
    {
        string query = "";
        SqlConnection conn = Common.getConnection();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = query;

        cmd.Parameters.AddWithValue("@id_pengajuan", HiddenIDPengajuan.Value);
        cmd.Parameters.AddWithValue("@memo", TbCatatan.Text);
      //  cmd.Parameters.AddWithValue("@id_user_pimpinan", Request.Cookies["UserSettings"]["id_user"]);
        cmd.Parameters.AddWithValue("@id_user_pimpinan", "1");
        cmd.Parameters.AddWithValue("@approval_date", DateTime.Now.ToString("yyyy-MM-dd HH:MM:ss"));

        query = "Update t_pengajuan_service set status_konfirmasi = 2, catatan_pimpinan = @memo, id_user_pimpinan = @id_user_pimpinan, approval_date = @approval_date where id_pengajuan_service = @id_pengajuan";


        conn.Open();
        cmd.Connection = conn;
        cmd.CommandText = query;
        cmd.ExecuteNonQuery();

        conn.Close();

        GvBind();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alertSukses();", true);
        Response.Redirect("ClosedList.aspx");
    }
}