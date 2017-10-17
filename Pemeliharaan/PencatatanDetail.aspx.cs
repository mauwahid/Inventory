using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class Pemeliharaan_KerusakanDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            HiddenIdRuangan.Value = Session["id_ruangan"].ToString();
            GvBind();
            BindPencatatan();
        }

    }



    protected void BindPencatatan()
    {
        SqlConnection conn = Common.getConnection();
        string query = "";
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;
        string id = Session["id_pencatatan"].ToString();
        HiddenIdPencatatan.Value = id;

        query = "select * from v_pencatatan where id = " + id + "";
        cmd.CommandText = query;

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();
        while(reader.Read())
        {
            HiddenIdPencatatan.Value = id;
            LblCatatan.Text = reader["catatan"].ToString();
            LblPencatat.Text = reader["username"].ToString();
            LblTanggalPencatatan.Text = reader["tanggal_pencatatan"].ToString();
            LblNoPencatatan.Text = reader["no_pencatatan"].ToString();

        }
        
        conn.Close();

    }

    protected void GvBind()
    {
        // string id = Session["no_pengajuan"].ToString();
        string id = Session["id_pencatatan"].ToString();
        SqlDataSource dataSource = new SqlDataSource();
        dataSource.ConnectionString = ConfigurationManager.ConnectionStrings["InventoryConn"].ToString();
        dataSource.SelectCommand = "select * from v_pencatatan_detail where id_pencatatan = " + id;

        GridView1.DataSource = dataSource;
        GridView1.DataBind();

        GridView1.Columns[4].Visible = false;
        GridView1.Columns[1].Visible = false;
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
            string idInvDetail = Convert.ToString(e.CommandArgument);
            Delete(idInvDetail);
            GvBind();
        }
        else if (e.CommandName == "Ubah")
        {
            SqlConnection conn = Common.getConnection();
            string query = "";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = System.Data.CommandType.Text;
            string id = Convert.ToString(e.CommandArgument);

            query = "select id, id_jenis_inventaris, memo_kabag, qty from t_pengajuan_inventaris_detail where id = " + id;
            cmd.CommandText = query;

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                HiddenIdInventaris.Value = reader["id"].ToString();
                DDLInventaris.SelectedValue = reader["id_jenis_inventaris"].ToString();
                DDLInventaris.Enabled = false;
                TbCatatanInventaris.Text = reader["memo_kabag"].ToString();
                TbQty.Text = reader["qty"].ToString();
            }

            conn.Close();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "LoadModal", "showModalInventaris();", true);

        }
    }

    protected void Delete(string id)
    {
        SqlConnection conn = Common.getConnection();
        string query = "";
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;

        query = "delete t_pengajuan_inventaris_detail where id = " + id + "";
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
        SqlConnection conn = Common.getConnection();
        string query = "";
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;
        string id = GridView1.DataKeys[e.NewEditIndex]["id"].ToString();

        query = "select id, id_jenis_inventaris, memo_kabag, qty from t_pengajuan_inventaris_detail where id_jenis_inventaris = " + id;
        cmd.CommandText = query;

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            HiddenIdInventaris.Value = reader["id_jenis_inventaris"].ToString();
            DDLInventaris.SelectedValue = (string)reader["id_jenis_inventaris"];
            DDLInventaris.Enabled = false;
        //    TbMemoKabag.Text = reader["memo_kabag"].ToString();
            TbQty.Text = reader["qty"].ToString();
        }

        conn.Close();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "LoadModal", "showModalInventaris();", true);


    }

    protected void RowDelete_Click(object sender, GridViewDeleteEventArgs e)
    {

        string id = GridView1.DataKeys[e.RowIndex]["Id"].ToString();
        Delete(id);
        GvBind();


    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        GvBind();
    }

    protected void SimpanInventaris_Click(object sender, EventArgs e)
    {
        SqlConnection conn = Common.getConnection();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        cmd.CommandText = "UPSERT_CATATAN_INVENTARIS_DETAIL";

        cmd.Parameters.AddWithValue("@id_inventaris", DDLInventaris.SelectedValue);
        cmd.Parameters.AddWithValue("@tipe", DDLType.SelectedValue);
        cmd.Parameters.AddWithValue("@id_ruangan", HiddenIdRuangan.Value);
        cmd.Parameters.AddWithValue("@id_catatan", HiddenIdPencatatan.Value);
        cmd.Parameters.AddWithValue("@qty", TbQty.Text);
        cmd.Parameters.AddWithValue("@catatan", TbCatatanInventaris.Text);
        cmd.Connection = conn;
        
        conn.Open();
        cmd.ExecuteNonQuery();

        conn.Close();

        GvBind();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeAddInvModal();", true);

    }

    protected void InitDDLInventaris()
    {
        SqlConnection conn = Common.getConnection();
        string query = "";
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.Parameters.AddWithValue("@id_ruangan", HiddenIdRuangan.Value);

        query = "select id, nama_inventaris from m_inventaris where id in (select id_inventaris from t_inventaris_ruangan where id_ruangan = @id_ruangan)";
        cmd.CommandText = query;

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();

        DDLInventaris.DataSource = reader;
        DDLInventaris.DataValueField = "id";
        DDLInventaris.DataTextField = "nama_inventaris";
        DDLInventaris.DataBind();

        conn.Close();
    }

   

   
    protected void SimpanPencatatan_Click(object sender, EventArgs e)
    {
        string query = "";
        SqlConnection conn = Common.getConnection();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = System.Data.CommandType.Text;
        
        cmd.Parameters.AddWithValue("@id_pencatatan", HiddenIdPencatatan.Value);

        query = "Update t_pencatatan_inventaris set status = 1 where id = @id_pencatatan";

        conn.Open();
        cmd.Connection = conn;
        cmd.CommandText = query;
        int ret = cmd.ExecuteNonQuery();
        if (ret == 1)
        {
            SqlCommand cmd2 = new SqlCommand();
            SqlCommand cmd3 = new SqlCommand(); 
            cmd2.CommandType = System.Data.CommandType.Text;
            cmd2.Connection = conn;
            cmd2.Parameters.AddWithValue("@id_pencatatan", HiddenIdPencatatan.Value);
            cmd2.CommandText = "Update t_pencatatan_inventaris_detail set status_simpan = 1 where id_pencatatan = @id_pencatatan";
            int ret2 = cmd2.ExecuteNonQuery();

            if (ret2 == 1)
            {
                foreach (GridViewRow row in GridView1.Rows)
                {

                    string tipe = row.Cells[4].Text;
                    string qty = row.Cells[3].Text;
                    string idJenis = row.Cells[1].Text;
                    cmd3 = new SqlCommand();
                    cmd3.CommandType = System.Data.CommandType.Text;
                    cmd3.Parameters.AddWithValue("@qty", qty);
                    cmd3.Parameters.AddWithValue("@id_jenis_inventaris", Int64.Parse(idJenis));

                    cmd3.Connection = conn;
                        
                    if (tipe.Trim().Equals("1"))
                    {
                        query = "update t_stock_inventaris set qty_terpakai = qty_terpakai - @qty, qty_rusak = qty_rusak + @qty where id_jenis_inventaris = @id_jenis_inventaris";
                        cmd2.CommandText = query;
                        
            
                    }
                    else if (tipe.Trim().Equals("2"))
                    {
                        query = "update t_stock_inventaris set qty_terpakai = qty_terpakai - @qty, qty_hilang = qty_hilang + @qty where id_jenis_inventaris = @id_jenis_inventaris";
                        cmd2.CommandText = query;
                       
                    }

                    cmd2.ExecuteNonQuery();
                    cmd2.Parameters.Clear();
                }     
            }
            

        }



        conn.Close();

        GvBind();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alertSukses();", true);
        Response.Redirect("Draft.aspx");
    }

    protected void BtnTambah_Click(object sender, EventArgs e)
    {
        InitDDLInventaris();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "LoadModal", "showModalInventaris();", true);

    }
    protected void BtnDeletePencatatan_Click(object sender, EventArgs e)
    {
        string query, query2 = "";
        SqlConnection conn = Common.getConnection();
        SqlCommand cmd = new SqlCommand();
        SqlCommand cmd2 = new SqlCommand();

        cmd.CommandType = System.Data.CommandType.Text;
        cmd.Parameters.AddWithValue("@id_pencatatan", HiddenIdPencatatan.Value);
        query = "delete t_pencatatan_inventaris where id = @id_pencatatan";
        query2 = "delete t_pencatatan_inventaris_detail where id_pencatatan = @id_pencatatan";
        conn.Open();
        cmd.Connection = conn;
        cmd2.Connection = conn;
        cmd.CommandText = query;
        cmd2.CommandText = query2;

        cmd.ExecuteNonQuery();
        cmd2.ExecuteNonQuery();
        
        conn.Close();

        GvBind();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alertSuksesDelete();", true);
        Response.Redirect("Draft.aspx");
    }
}