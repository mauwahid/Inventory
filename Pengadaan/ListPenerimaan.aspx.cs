using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class Pengadaan_ListPenerimaan : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GvBind();
        }

    }

    protected void btnTambah_Click(object sender, EventArgs e)
    {
        Session["pengajuan_form_type"] = "add";
        Response.Redirect("Form.aspx");
    }


    protected void GvBind()
    {
        SqlDataSource dataSource = new SqlDataSource();
        dataSource.ConnectionString = ConfigurationManager.ConnectionStrings["InventoryConn"].ToString();

        string query = "select a.id_penerimaan, a.no_penerimaan, a.judul_penerimaan, REPLACE(CONVERT(VARCHAR(9), a.tanggal_penerimaan, 6), ' ', '-') AS [tanggal_penerimaan] ,a.creator, a.id_user,"
                                 + "   a.id_pembelian, a.id_pembelian as ref_pengajuan, b.created_date, c.username, a.status_penerimaan,"
                                 + "   a.keterangan from t_penerimaan a left join  t_pengajuan_inventaris b on a.id_penerimaan = b.id_pengajuan"
                                 + "    left join m_user c on a.id_user = c.id_user where tipe_ref = 1";
                                

        query = query + " union all ";
        query = query + "select a.id_penerimaan, a.no_penerimaan, a.judul_penerimaan, REPLACE(CONVERT(VARCHAR(9), a.tanggal_penerimaan, 6), ' ', '-') AS [tanggal_penerimaan],a.creator, a.id_user,"
                                 + "   a.id_pembelian, a.id_service_keluar as ref_pengajuan, b.created_date, c.username, a.status_penerimaan,"
                                 + "   a.keterangan from t_penerimaan a left join  t_pengajuan_inventaris b on a.id_penerimaan = b.id_pengajuan"
                                 + "    left join m_user c on a.id_user = c.id_user where tipe_ref = 2";
                               //  + "    order by a.id_penerimaan desc";

        query = query + " union all ";
        query = query + "select a.id_penerimaan, a.no_penerimaan, a.judul_penerimaan, REPLACE(CONVERT(VARCHAR(9), a.tanggal_penerimaan, 6), ' ', '-') AS [tanggal_penerimaan],a.creator, a.id_user,"
                                 + "   a.id_pembelian, a.id_service_keluar as ref_pengajuan, null, c.username, a.status_penerimaan,"
                                 + "   a.keterangan from t_penerimaan a "
                                  + "    left join m_user c on a.id_user = c.id_user where a.tipe_ref = 0 "
                                   + "    order by a.id_penerimaan desc";

        System.Diagnostics.Debug.WriteLine(query);
                        

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
            string idPenerimaan = Convert.ToString(e.CommandArgument);
            Session["id_penerimaan"] = idPenerimaan;
         //   Response.Write("ID PEnerimaan : " + Session["id_penerimaan"].ToString());
            Response.Redirect("PenerimaanDetailOK.aspx");

        }
    }

    protected void Delete(string id)
    {
        SqlConnection conn = Common.getConnection();
        string query = "";
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;
        string param = TbCari.Text;

        query = "delete m_pengajuan where id = " + id + "and status_approval = 0";
        cmd.CommandText = query;

        conn.Open();
        int reader = cmd.ExecuteNonQuery();

        

        conn.Close();

    }

    protected void GView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GvBind();
    }



    protected void btnCari_Click(object sender, EventArgs e)
    {
        string param = TbCari.Text;
        SqlDataSource dataSource = new SqlDataSource();
        dataSource.ConnectionString = ConfigurationManager.ConnectionStrings["InventoryConn"].ToString();
        dataSource.SelectCommand = "select a.id_penerimaan, a.no_penerimaan, a.judul_penerimaan, a.tanggal_penerimaan,a.creator, a.id_user,"
                                    + "a.id_pengajuan as ref_pengajuan, b.no_pengajuan, b.created_date, c.username, a.status_penerimaan,"
                                    + "a.keterangan from t_penerimaan a left join  t_pengajuan_inventaris b on a.id_pengajuan = b.id_pengajuan"
                                    + " left join m_user c on a.id_user = c.id_user"
                                    + " where a.judul_penerimaan like '%"+param+"%' "
                                     + "   order by a.id_penerimaan desc";


        GridView1.DataSource = dataSource;
        GridView1.DataBind();

    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        GvBind();
    }
}