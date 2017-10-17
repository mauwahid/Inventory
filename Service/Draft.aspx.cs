using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class Pengadaan_ListPengadaan : System.Web.UI.Page
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
        dataSource.SelectCommand = "SELECT id_pengajuan_service, no_pengajuan_service, judul_pengajuan_service, "
                                  + "  catatan_kabag, kabag, REPLACE(CONVERT(VARCHAR(9), created_date, 6), ' ', '-') AS [created_date]"
                                  +"  From v_service where status_konfirmasi = 0 order by id_pengajuan_service desc";

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
        if (e.CommandName == "Hapus")
        {
            string idPengajuan = Convert.ToString(e.CommandArgument);
            Delete(idPengajuan);
            GvBind();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('sukses dihapus');", true);
       
        }
    }

    protected void Delete(string id)
    {
        SqlConnection conn = Common.getConnection();
        string query = "";
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;
       
        query = "delete t_pengajuan_service where id_pengajuan_service = '" + id + "'";
        cmd.CommandText = query;

        conn.Open();
        cmd.ExecuteNonQuery();

        conn.Close();

        conn = Common.getConnection();
        cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;

        query = "delete t_pengajuan_service_detail where id_pengajuan_service = '" + id + "'";
        cmd.CommandText = query;

        conn.Open();
        cmd.ExecuteNonQuery();

        conn.Close();
        
    }

    protected void GView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GvBind();
    }

    protected void RowEdit_Click(object sender, GridViewEditEventArgs e)
    {
        SqlConnection conn = Common.getConnection();
        string query = "";
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;
        string id = GridView1.DataKeys[e.NewEditIndex]["Id_pengajuan_service"].ToString();
        
        
        Session["id_pengajuan"] = id;
        Response.Redirect("FormDetailEdit.aspx");
    }

    protected void RowDelete_Click(object sender, GridViewDeleteEventArgs e)
    {

        string id = GridView1.DataKeys[e.RowIndex]["Id"].ToString();
        Delete(id);
        GvBind();


    }
    protected void btnCari_Click(object sender, EventArgs e)
    {
         string param = TbCari.Text;

         string query = "select * from v_service where id_pengajuan_service like '%" + param + "%' or no_pengajuan_service like '%" + param + "%'"
         + "or catatan_kabag like '%" + param + "%' or catatan_pimpinan like '%" + param + "%' or pimpinan like '%" + param + "%' "
         + " or created_date like '%" + param + "%' ";
      
     
        SqlDataSource dataSource = new SqlDataSource();
        dataSource.ConnectionString = ConfigurationManager.ConnectionStrings["InventoryConn"].ToString();
        dataSource.SelectCommand = query;
        GridView1.DataSource = dataSource;
        GridView1.DataBind();

    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        TbCari.Text = "";
        GvBind();
    }
}