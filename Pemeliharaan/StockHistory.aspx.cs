using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class Pemeliharaan_StockHistory : System.Web.UI.Page
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
        String idJenis = Session["id_inventaris"].ToString();
        lblIdJenisInv.Text = idJenis;
        SqlDataSource dataSource = new SqlDataSource();
        dataSource.ConnectionString = ConfigurationManager.ConnectionStrings["InventoryConn"].ToString();

        string query = "select * from v_history_stock where id_inventaris = "+idJenis+" order by id desc";

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
        GvBind();
    }
}