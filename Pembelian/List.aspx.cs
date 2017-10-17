using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System;



public partial class Pembelian_List : System.Web.UI.Page
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
        dataSource.SelectCommand = " SELECT a.id_pembelian, a.harga_total, a.no_pembelian, b.id_pengajuan, b.no_pengajuan, REPLACE(CONVERT(VARCHAR(9), tanggal_pembelian, 6), ' ', '-') AS [tanggal], a.keterangan, b.no_pengajuan, " +
                                    " case when status = 0 then 'Belum Dibeli' when status = 1 then 'Dalam Proses' when status = 2 then 'Sudah Dibeli' when status = 3 then 'Tidak dibeli' end as status from "+
                                   " t_pembelian a left join t_pengajuan_inventaris b "+
                                   " on a.id_pengajuan_inventaris = b.id_pengajuan";

        GridView1.DataSource = dataSource;
        GridView1.DataBind();
    }

    private void blankTable()
    {
        GridView1.DataSource = new List<String>();
        GridView1.DataBind();
    }

    protected void Save_Click(object sender, EventArgs e)
    {
        SqlConnection conn = Common.getConnection();
        string query = "";
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.Parameters.AddWithValue("@IdPembelian", TbNoItem.Text);
        cmd.Parameters.AddWithValue("@status", DDLStatus.SelectedValue);


        query = "UPDATE t_pembelian set status = @status " +
        " where id_pembelian = @IdPembelian";

        conn.Open();
        cmd.CommandText = query;
        cmd.ExecuteNonQuery();
        
        conn.Close();
        GvBind();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "HideLain", "alert('Sukses');hideModal();", true);


    }


    protected void RowCommand_Click(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Pilih")
        {
            string idPembelian = Convert.ToString(e.CommandArgument);
            Session["id_pembelian"] = idPembelian;
            Response.Redirect("Detail.aspx");

        }
        if (e.CommandName == "UbahStatus")
        {
            string idPembelian = Convert.ToString(e.CommandArgument);
           // Session["id_pembelian"] = idPembelian;
           // Response.Redirect("Detail.aspx");
           // string idInventaris = Convert.ToString(e.CommandArgument);
            TbNoItem.Text = idPembelian;
         //   try
         //   {
                DDLStatus.SelectedValue = GetBeliItem(TbNoItem.Text);
         /*   }
            catch
            {
            }*/
            ScriptManager.RegisterStartupScript(this, this.GetType(), "LoadModal", "showModalEditInv();", true);


        }
    }
    protected string GetBeliItem(string id)
    {
        string data = "";
        SqlConnection conn = Common.getConnection();
        string query = "";
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;
        string param = TbCari.Text;

        query = "select status from t_pembelian where id_pembelian = '" + id+"'";
        cmd.CommandText = query;

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            data = reader["status"].ToString();
        }

        conn.Close();

        return data;

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
        dataSource.SelectCommand = " SELECT a.id_pembelian, a.id_pembelian, a.harga_total, CONVERT(date, a.tanggal_pembelian) as tanggal, a.keterangan, b.id_pengajuan, "+
                                    " case when status = 0 then 'Belum Dibeli' when status = 1 then 'Dalam Proses' when status = 2 then 'Sudah Dibeli' when status = 3 then 'Tidak dibeli' end as status from " +
                                   " t_pembelian a left join t_pengajuan_inventaris b " +
                                   " on a.id_pengajuan_inventaris = b.id_pengajuan where a.id_pembelian like '%" + param + "%' or " +
                                   " b.id_pengajuan like '%" + param + "%'";
       

        GridView1.DataSource = dataSource;
        GridView1.DataBind();

    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        GvBind();
    }
}