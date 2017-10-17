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

public partial class ServiceKeluar_List : System.Web.UI.Page
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
        dataSource.SelectCommand = " SELECT *, REPLACE(CONVERT(VARCHAR(9), tanggal_service, 6), ' ', '-') AS [dt_svc], "
                                   + " status as status_desc"
                                  +" from v_service_keluar";


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
            string idService = Convert.ToString(e.CommandArgument);
            Session["id_service"] = idService;
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
            DDLStatus.SelectedValue = GetServiceItem(TbNoItem.Text);
            /*   }
               catch
               {
               }*/
            ScriptManager.RegisterStartupScript(this, this.GetType(), "LoadModal", "showModalEditInv();", true);


        }
    }

    protected string GetServiceItem(string id)
    {
        string data = "";
        SqlConnection conn = Common.getConnection();
        string query = "";
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;
        string param = TbCari.Text;

        query = "select status from t_service_keluar where id_service = '" + id + "'";
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

        string query = "SELECT * from v_service_keluar where id_service like '%"+param+"%' or "
            + "judul like '%"+param+"%' or no_pengajuan like '%"+param+"%' ";

        SqlDataSource dataSource = new SqlDataSource();
        dataSource.ConnectionString = ConfigurationManager.ConnectionStrings["InventoryConn"].ToString();
        dataSource.SelectCommand = query;


        GridView1.DataSource = dataSource;
        GridView1.DataBind();

    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        GvBind();
    }

    protected void Save_Click(object sender, EventArgs e)
    {
        SqlConnection conn = Common.getConnection();
        string query = "";
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.Parameters.AddWithValue("@Id", TbNoItem.Text);
        cmd.Parameters.AddWithValue("@status", DDLStatus.SelectedValue);


        query = "UPDATE t_service_keluar set status = @status " +
        " where id_service_keluar = @Id";

        conn.Open();
        cmd.CommandText = query;
        cmd.ExecuteNonQuery();

        conn.Close();
        GvBind();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "HideLain", "alert('Sukses');hideModal();", true);


    }

}