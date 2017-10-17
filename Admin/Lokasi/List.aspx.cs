using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class Admin_Lokasi_List : System.Web.UI.Page
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
        Session["lokasi_form_type"] = "add";
        Response.Redirect("Form.aspx");
    }

   

    protected void GvBind()
    {
        SqlConnection conn = Common.getConnection();
        string query = "Select a.*, b.nama_gedung from m_lantai a left join m_gedung b on a.id_gedung = b.id_gedung";
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = query;

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();

        DataTable dt = new DataTable();
        dt.Columns.Add("Id", typeof(string));
        dt.Columns.Add("Lokasi", typeof(string));
        dt.Columns.Add("NamaGedung", typeof(string));


        DataRow dr = null;
        while (reader.Read())
        {
            dr = dt.NewRow();
            dr["Id"] = reader["id_lantai"].ToString();
            dr["Lokasi"] = reader["lokasi_lantai"].ToString();
            dr["NamaGedung"] = reader["nama_gedung"].ToString();
            dt.Rows.Add(dr);


        }

        GridView1.DataSource = dt;
        GridView1.DataBind();

        conn.Close();

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
            string idLokasi = Convert.ToString(e.CommandArgument);
            Delete(idLokasi);
            GvBind();
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

        query = "delete m_lantai where id_lantai = " + id;
        System.Diagnostics.Debug.WriteLine("Q : " + query);

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

    protected void RowEdit_Click(object sender, GridViewEditEventArgs e)
    {

        string id = GridView1.DataKeys[e.NewEditIndex]["Id"].ToString();
        Session["lokasi_form_type"] = "edit";
        Session["lokasi_form_id"] = id;
        Response.Redirect("Form.aspx");


    }

    protected void RowDelete_Click(object sender, GridViewDeleteEventArgs e)
    {

        string id = GridView1.DataKeys[e.RowIndex]["Id"].ToString();
        Delete(id);
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

        query = "Select a.*, b.nama_gedung from m_lantai a left join m_gedung b on a.id_gedung = b.id_gedung "+
            " where a.lokasi_lantai like '%" + param + "%'";
       // query = "select * from m_lokasi where lokasi_lantai like '%" + param + "%'";
        cmd.CommandText = query;

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();

        DataTable dt = new DataTable();
        dt.Columns.Add("Id", typeof(string));
        dt.Columns.Add("Lokasi", typeof(string));
        dt.Columns.Add("NamaGedung", typeof(string));

        DataRow dr = null;

        while (reader.Read())
        {
            dr = dt.NewRow();
            dr["Id"] = reader["id_lantai"].ToString();
            dr["Lokasi"] = reader["lokasi_lantai"].ToString();
            dr["NamaGedung"] = reader["nama_gedung"].ToString();
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
}