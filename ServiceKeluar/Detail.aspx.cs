using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;


public partial class ServiceKeluar_Detail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
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
        string serviceID = (string)Session["id_service"];

        query = "select * from v_service_keluar where id_service_keluar like '" + serviceID + "'";
        cmd.CommandText = query;

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();
        reader.Read();
        HiddenField1.Value = reader["id_pengajuan_service"].ToString();
        LblID.Text = reader["id_service_keluar"].ToString().Trim();
        LblRefService.Text = reader["id_pengajuan_service"].ToString();
        LblKeterangan.Text = reader["keterangan"].ToString();
        LblJudul.Text = reader["judul"].ToString();
        try
        {
            LblTglServices.Text = String.Format("{0:dd-MMM-yyyy}", (DateTime)reader["tanggal_service"]);
        }
        catch
        {
            LblTglServices.Text = reader["tanggal_service"].ToString();
      
        }
      //  string id_pengajuan_service = reader["id_pengajuan"].ToString();
        GvBind(HiddenField1.Value);
       
        conn.Close();



    }

    protected void GvBind(string id)
    {
        // string id = Session["no_pengajuan"].ToString();
        System.Diagnostics.Debug.WriteLine("ID " + id);
        string query = "select * from v_service_keluar_det_report where id_pengajuan_service = '" + id+"'" ;
        SqlDataSource dataSource = new SqlDataSource();
        dataSource.ConnectionString = ConfigurationManager.ConnectionStrings["InventoryConn"].ToString();
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
        GvBind(HiddenField1.Value);
    }


    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("List.aspx");
    }
}