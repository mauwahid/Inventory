using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;


public partial class Pemeliharaan_Adjustment : System.Web.UI.Page
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
        string idJenis = Session["id_inventaris"].ToString();
        HiddenId.Value = idJenis;
        SqlDataSource dataSource = new SqlDataSource();
        dataSource.ConnectionString = ConfigurationManager.ConnectionStrings["InventoryConn"].ToString();

        string query = "Select adj.*, case adj.jenis_adjustment when 1 then 'Penambahan' when 2 then 'Pengurangan' end as jenis, j_i.nama_inventaris from t_adjustment adj inner join m_inventaris j_i "
        + " on adj.id_inventaris = j_i.id where adj.id_inventaris = "+idJenis;

        dataSource.SelectCommand = query;

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
        if (e.CommandName == "Adjustment")
        {
            string idInventaris = Convert.ToString(e.CommandArgument);
            Session["id_inventaris"] = idInventaris;
            Response.Redirect("Adjustment.aspx");

        }
        else if (e.CommandName == "StockHistory")
        {
            string idInventaris = Convert.ToString(e.CommandArgument);
            Session["id_inventaris"] = idInventaris;
            Response.Redirect("StockHistory.aspx");

        }
    }

    protected void ShowInventaris(string id)
    {
        Session["id_ruangan"] = id;
        Response.Redirect("ListInventaris.aspx");
    }

    protected void GView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GvBind();
    }


    protected void btnTambah_Click(object sender, EventArgs e)
    {
        Session["id_jenis_inventaris"] = HiddenId.Value;
        Response.Redirect("AddAdjustment.aspx");
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        GvBind();
    }
}