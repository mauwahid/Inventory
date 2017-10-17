using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class Kabag_ListTersetujui : System.Web.UI.Page
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
        if (DDLType.SelectedValue == "0")
            dataSource.SelectCommand = "select id_pengajuan, no_pengajuan, judul_pengajuan, catatan_kabag, kabag, REPLACE(CONVERT(VARCHAR(9), created_date, 6), ' ', '-') AS [created_date],  "+
                "REPLACE(CONVERT(VARCHAR(9), approval_date, 6), ' ', '-') AS [approval_date], "
                                 +"  pimpinan,  catatan_pimpinan, status_konfirmasi,"
                                 +" case when "
                                +" status_konfirmasi = '1' then 'Disetujui' "
                               +"   when status_konfirmasi = '2' then 'Ditolak'"
                                +"  end as status_approval_ket"
                               +" from v_pengajuan where status_konfirmasi is not NULL and status_konfirmasi !=0"
                               +"  order by id_pengajuan desc";
        else
        {
            dataSource.SelectCommand = "select id_pengajuan, no_pengajuan, judul_pengajuan, catatan_kabag, kabag, created_date,  approval_date, "
                                   +" pimpinan, catatan_pimpinan, status_konfirmasi,"
                                 +"  case when "
                                 +" status_konfirmasi = '1' then 'Disetujui' "
                                 +"   when status_konfirmasi = '2' then 'Ditolak'"
                                 +"   end as status_approval_ket"
                                 +"   from v_pengajuan where status_konfirmasi is not NULL and status_konfirmasi !=0"
                                 +" and status_konfirmasi = "+DDLType.SelectedValue+" "
                                 +"  order by id_pengajuan desc";
        }


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
     //   string param = TbCari.Text;

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


/*
    protected void btnCari_Click(object sender, EventArgs e)
    {
        SqlConnection conn = Common.getConnection();
        string query = "";
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;
        string param = TbCari.Text;

        query = "select * from v_pengajuan_inventaris where id like '%" + param + "%' or no_pengajuan like '%" + param + "%'"
        + "or keterangan like '%" + param + "%' or memo_pimpinan like '%" + param + "%' or pimpinan like '%" + param + "%' "
        + "or judul_pengajuan like '%" + param + "%' or memo_kabag like '%" + param + "%' or created_date like '%" + param + "%' "
        + "or status_approval like '%" + param + "%' or approval_date like '%" + param + "%' and status_pengajuan is not NULL ";
        cmd.CommandText = query;

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();

        DataTable dt = new DataTable();
        dt.Columns.Add("Id", typeof(string));
        dt.Columns.Add("NoPengajuan", typeof(string));
        dt.Columns.Add("JudulPengajuan", typeof(string));
        dt.Columns.Add("Keterangan", typeof(string));
        dt.Columns.Add("MemoKabag", typeof(string));
        dt.Columns.Add("Kabag", typeof(string));
        dt.Columns.Add("CreatedDate", typeof(string));
        dt.Columns.Add("StatusApproval", typeof(string));
        dt.Columns.Add("ApprovalDate", typeof(string));
        dt.Columns.Add("Pimpinan", typeof(string));
        dt.Columns.Add("MemoPimpinan", typeof(string));


        DataRow dr = null;
        while (reader.Read())
        {
            dr = dt.NewRow();
            dr["Id"] = reader["id"].ToString();
            dr["NoPengajuan"] = reader["no_pengajuan"].ToString();
            dr["JudulPengajuan"] = reader["judul_pengajuan"].ToString();
            dr["Keterangan"] = reader["keterangan"].ToString();
            dr["MemoKabag"] = reader["memo_kabag"].ToString();
            dr["CreatedDate"] = reader["created_date"].ToString();
            dr["StatusApproval"] = reader["status_approval"].ToString();
            dr["ApprovalDate"] = reader["approval_date"].ToString();
            dr["Pimpinan"] = reader["pimpinan"].ToString();
            dr["MemoPimpinan"] = reader["memo_pimpinan"].ToString();
            dt.Rows.Add(dr);


        }


        GridView1.DataSource = dt;
        GridView1.DataBind();

        conn.Close();

    }*/

    protected void btnCari_Click(object sender, EventArgs e)
    {
        GvBind();
    }


    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        DDLType.SelectedIndex = 0;
        GvBind();
    }
}