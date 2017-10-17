using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Configuration;
using System.Data.SqlClient;
using System.Data;


public partial class Pemeliharaan_Stock : System.Web.UI.Page
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
        
        string query = "Select stock.*, j_i.nama_inventaris from t_stock_inventaris stock inner join m_inventaris j_i "
        +" on stock.id_inventaris = j_i.id ";

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

        }else if (e.CommandName == "StockHistory")
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


    protected void btnCari_Click(object sender, EventArgs e)
    {
        SqlConnection conn = Common.getConnection();
        string query = "";
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;
        string param = TbCari.Text;
        string id = HiddenId.Value;
        query = "select * from v_ruangan_pemeliharaan where nama_ruangan like '%" + param + "%' and id_lokasi = " + id;
        cmd.CommandText = query;

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();

        DataTable dt = new DataTable();
        dt.Columns.Add("Id", typeof(string));
        dt.Columns.Add("NamaRuangan", typeof(string));
        dt.Columns.Add("JenisRuangan", typeof(string));
        dt.Columns.Add("QtyTersedia", typeof(string));
        dt.Columns.Add("QtyHilang", typeof(string));
        dt.Columns.Add("QtyRusak", typeof(string));


        DataRow dr = null;
        while (reader.Read())
        {
            dr = dt.NewRow();
            dr["Id"] = reader["id"].ToString();
            dr["NamaRuangan"] = reader["nama_ruangan"].ToString();
            dr["JenisRuangan"] = reader["jenis_ruangan"].ToString();
            dr["QtyTersedia"] = reader["qty_tersedia"].ToString();
            dr["QtyHilang"] = reader["qty_hilang"].ToString();
            dr["QtyRusak"] = reader["qty_rusak"].ToString();
            dt.Rows.Add(dr);
        }

        GridView1.DataSource = dt;
        GridView1.DataBind();

        conn.Close();

    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        GvBind();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("StockAdd.aspx");
    }
}