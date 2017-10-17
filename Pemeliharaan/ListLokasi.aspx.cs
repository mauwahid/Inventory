using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class Pemeliharaan_ListLokasi : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GvBind();
            GenerateDDLGedung();
           // GenerateDDLLantai();
        }

    }

    protected void btnTambah_Click(object sender, EventArgs e)
    {
        Session["lokasi_form_type"] = "add";
        Response.Redirect("Form.aspx");
    }


    protected void GvBind()
    {
        SqlDataSource dataSource = new SqlDataSource();
        dataSource.ConnectionString = ConfigurationManager.ConnectionStrings["InventoryConn"].ToString();
        dataSource.SelectCommand = "select * from v_ruangan";
        GridView1.DataSource = dataSource;
        GridView1.DataBind();


    }

    private void GenerateDDLGedung()
    {

        SqlConnection conn = Common.getConnection();
        string query = "";
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;

        query = "select id_gedung, nama_gedung from m_gedung ";
        cmd.CommandText = query;

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();

        DDLGedung.DataSource = reader;
        DDLGedung.DataValueField = "id_gedung";
        DDLGedung.DataTextField = "nama_gedung";
        DDLGedung.DataBind();

        ListItem item = new ListItem();
        item.Value = "0";
        item.Text = "-Semua-";
        DDLGedung.Items.Insert(0, item);

        conn.Close();



    }

    private void GenerateDDLLantai()
    {
        ListItem item = new ListItem();
        item.Value = "0";
        item.Text = "-Semua-";

        if (!DDLGedung.SelectedValue.Equals("0"))
        {

            SqlConnection conn = Common.getConnection();
            string query = "";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = System.Data.CommandType.Text;

            query = "select id_lantai, lokasi_lantai from m_lantai where id_gedung = "+DDLGedung.SelectedValue+"";
            cmd.CommandText = query;

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            DDLLantai.DataSource = reader;
            DDLLantai.DataValueField = "id_lantai";
            DDLLantai.DataTextField = "lokasi_lantai";
            DDLLantai.DataBind();

            
            DDLLantai.Items.Insert(0, item);

            conn.Close();


        }
        else
        {
            DDLLantai.Items.Insert(0, item);

        }
        

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
            string idLokasi = Convert.ToString(e.CommandArgument);
            ShowRuangan(idLokasi);
         //   GvBind();
        }
    }

    protected void ShowRuangan(string id)
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

        string idGedung = DDLGedung.SelectedValue;
        string idLantai = DDLLantai.SelectedValue;

        if (idGedung.Equals("0"))
        {
            query = "select * from v_ruangan";
            
        }
        else
        {
            if (idLantai.Equals("0"))
            {
                query = "select * from v_ruangan where id_gedung = "+DDLGedung.SelectedValue+"";
            }
            else
            {
                query = "select * from v_ruangan where id_lantai = " + DDLLantai.SelectedValue + " and id_gedung = " + DDLGedung.SelectedValue + "";
            }
        }

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
    protected void DDLGedung_SelectedIndexChanged(object sender, EventArgs e)
    {
        GenerateDDLLantai();
    }
}