using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;


public partial class Penugasan_ListPenugasan : System.Web.UI.Page
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
        string query = "SELECT p.id_penugasan, REPLACE(CONVERT(VARCHAR(9), p.tanggal_penugasan, 6), ' ', '-') AS [tanggal_penugasan], u.username, p.judul , case when p.status = 6 then 'Draft' end as status "+  
                                  " from t_penugasan_inventaris p "+
                                  "  inner join m_user u on p.id_user = u.id_user where "+
                                  " p.status = 6 order by id_penugasan desc ";

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
        if (e.CommandName == "Pilih")
        {
            string idPenugasan = Convert.ToString(e.CommandArgument);
            Session["id_penugasan"] = idPenugasan;
            Response.Redirect("PenugasanDetailEdit.aspx");

        }
        if (e.CommandName == "Hapus")
        {
            string idPenugasan = Convert.ToString(e.CommandArgument);
            string query = "";
            SqlConnection conn = Common.getConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = query;

            cmd.Parameters.AddWithValue("@id",idPenugasan);

            query = "delete t_penugasan_inventaris where id_penugasan = '"+idPenugasan+"'";
            System.Diagnostics.Debug.WriteLine("query : " + query);


            conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = query;
            cmd.ExecuteNonQuery();

            conn.Close();

            conn = Common.getConnection();
            cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = query;

            cmd.Parameters.AddWithValue("@id_pengajuan",idPenugasan);

            query = "delete t_penugasan_inventaris_detail where id_penugasan = @id_pengajuan";


            conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = query;
            cmd.ExecuteNonQuery();

            conn.Close();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alertSuksesDelete();", true);
            GvBind();
        }
    }



    protected void btnCari_Click(object sender, EventArgs e)
    {
        string param = TbCari.Text;
        SqlDataSource dataSource = new SqlDataSource();
        dataSource.ConnectionString = ConfigurationManager.ConnectionStrings["InventoryConn"].ToString();
        dataSource.SelectCommand = "SELECT p.id, p.tanggal_penugasan, u.username, p.judul , case when p.status = 6 then 'Draft' end as status   "
                                   + " from t_penugasan p "
                                   + " inner join m_user u on p.id_kabag = u.id where"
                                   + " p.status = 6 "
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