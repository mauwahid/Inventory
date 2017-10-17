using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class Pengadaan_TelahDiajukan : System.Web.UI.Page
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
        dataSource.SelectCommand = "select id_pengajuan_service, no_pengajuan_service, judul_pengajuan_service, catatan_kabag, kabag, REPLACE(CONVERT(VARCHAR(9), created_date, 6), ' ', '-') AS [created_date], REPLACE(CONVERT(VARCHAR(9), created_date, 6), ' ', '-') AS [created_date], REPLACE(CONVERT(VARCHAR(9), approval_date, 6), ' ', '-') AS [approval_date], "
                                 +"    pimpinan, catatan_pimpinan, status_konfirmasi,"
                                  +"  case when "
                                 + "   status_konfirmasi = '0' then 'Belum direspon' "
                                 +"   when status_konfirmasi = '1' then 'Disetujui' "
                                 + "   when status_konfirmasi = '2' then 'Ditolak'"
                                 +"   end as status_approval_ket"
                                 +"   from v_service where status_konfirmasi is not NULL"
                                 +"   order by id_pengajuan_service desc";


        GridView1.DataSource = dataSource;
        GridView1.DataBind();
    }

    protected void GvBindSort()
    {
        SqlDataSource dataSource = new SqlDataSource();
        dataSource.ConnectionString = ConfigurationManager.ConnectionStrings["InventoryConn"].ToString();
        dataSource.SelectCommand = "select id, no_pengajuan, judul_pengajuan, memo_kabag, kabag, created_date,  approval_date, "
                                 +"    pimpinan, memo_pimpinan, status_approval,"
                                  +"  case when "
                                 +"   status_approval = '0' then 'Belum direspon' "
                                 +"   when status_approval = '1' then 'Disetujui' "
                                 +"   when status_approval = '2' then 'Ditolak'"
                                 +"   end as status_approval_ket"
                                 +"   from v_service where status_approval is not NULL"
                                 +"   order by  "+DDLUrutkan.SelectedValue+" "+DDLASCDESC.SelectedValue+"";


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
            Session["pengajuan_id"] = idPengajuan;
            Response.Redirect("Detail.aspx");

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
        dataSource.SelectCommand = "select id,id_service, no_pengajuan, judul_pengajuan, memo_kabag, kabag, created_date,  approval_date, "
                                 + "    pimpinan, memo_pimpinan, status_approval,"
                                  + "  case when "
                                 + "   status_approval = '0' then 'Belum direspon' "
                                 + "   when status_approval = '1' then 'Disetujui' "
                                 + "   when status_approval = '2' then 'Ditolak'"
                                 + "   end as status_approval_ket"
                                 + "   from v_service where status_approval is not NULL"
                                 + "  and (id_service like '%"+param+"%' or no_pengajuan like '%"+param+"%' "
                                 + " or judul_pengajuan like '%" + param + "%' or memo_kabag like '%" + param + "%') "
                                 + "   order by id desc";


        GridView1.DataSource = dataSource;
        GridView1.DataBind();


    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        GvBind();
    }
    protected void DDLUrutkan_SelectedIndexChanged(object sender, EventArgs e)
    {
       GvBindSort();

    }
    protected void DDLASCDESC_SelectedIndexChanged(object sender, EventArgs e)
    {
        GvBindSort();
    }
}