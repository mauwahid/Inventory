using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;


public partial class Penugasan_ListPenugasanTeknisi : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GvBind();
        }

    }

   


    protected void GvBind()
    {
        SqlDataSource dataSource = new SqlDataSource();
        dataSource.ConnectionString = ConfigurationManager.ConnectionStrings["InventoryConn"].ToString();
        dataSource.SelectCommand = "SELECT p.id_penugasan, REPLACE(CONVERT(VARCHAR(9), p.tanggal_penugasan, 6), ' ', '-') AS [tanggal_penugasan], u.username, p.judul "
                                   +" from t_penugasan_inventaris p "
                                   +" inner join m_user u on p.id_user = u.id_user where"
                                   +" p.is_start = 0 and p.status!=6 order by id_penugasan desc ";


        GridView1.DataSource = dataSource;
        GridView1.DataBind();
    }

    private void blankTable()
    {
        GridView1.DataSource = new List<String>();
        GridView1.DataBind();
    }

    protected void RowCommand_Click(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Pilih")
        {
            string idPengajuan = Convert.ToString(e.CommandArgument);
            Session["id_penugasan"] = idPengajuan;
            Response.Redirect("PenugasanTeknisiDetail.aspx");

        }
    }

   

    protected void btnCari_Click(object sender, EventArgs e)
    {
        string param = TbCari.Text;
        SqlDataSource dataSource = new SqlDataSource();
        dataSource.ConnectionString = ConfigurationManager.ConnectionStrings["InventoryConn"].ToString();
        dataSource.SelectCommand = "SELECT p.id, p.tanggal_penugasan, u.username, p.judul "
                                   + " from t_penugasan p "
                                   + " inner join m_user u on p.id_kabag = u.id where"
                                   + " p.status = 0 "
                                   + " and p.judul like '%"+param+"%' order by id desc ";


        GridView1.DataSource = dataSource;
        GridView1.DataBind();

    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        GvBind();
    } 

    protected void GView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GvBind();
    }
}