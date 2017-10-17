using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Configuration;
using System.Data.SqlClient;
using System.Data;

public partial class Pengadaan_PenerimaanDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

           string idPengajuan = BindPenerimaan();
           // HiddenIDPenerimaan.Value = Session["id_penerimaan"].ToString();
            GvBind(idPengajuan);
        }

    }



    protected string BindPenerimaan()
    {
        SqlConnection conn = Common.getConnection();
        string query = "";
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;
        HiddenIDPenerimaan.Value = (string)Session["id_penerimaan"];
        string idPenerimaan = "1";
        string idPengajuan = "";


        query = "select a.id, a.no_penerimaan, a.judul_penerimaan, a.keterangan, "
                +" a.id_pengajuan, a.tanggal_penerimaan, b.no_pengajuan, a.id_user_kabag, c.username" 
                +"     from t_penerimaan_inventaris a "
                 +"    left join t_pengajuan_inventaris" 
                 +"    b on a.id_pengajuan = b.id left join"
                 +"    m_user c on a.id_user_kabag = c.id where a.id = "+HiddenIDPenerimaan.Value+" ";
        cmd.CommandText = query;

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();
        reader.Read();
        HiddenIDPenerimaan.Value = idPenerimaan;
        LblJudulPenerimaan.Text = reader["judul_penerimaan"].ToString().Trim();
        LblKeterangan.Text = reader["keterangan"].ToString();
        LblNoPengajuan.Text = reader["no_pengajuan"].ToString();
        LblPenerima.Text = reader["username"].ToString();
        LblTglPenerimaan.Text = reader["tanggal_penerimaan"].ToString();
        idPengajuan = reader["id_pengajuan"].ToString();
        

        conn.Close();

        HiddenIDPengajuan.Value = idPengajuan;

        return idPengajuan;



    }

    protected void GvBind(string idPengajuan)
    {
        SqlDataSource dataSource = new SqlDataSource();
        dataSource.ConnectionString = ConfigurationManager.ConnectionStrings["InventoryConn"].ToString();
        dataSource.SelectCommand = "select p.id, p.id_pengajuan, p.id_inventaris, p.qty, p.memo_kabag, p.gambar,p.created_date, " +
            " pin.no_pengajuan, inv.nama_inventaris from t_pengajuan_inventaris_detail p inner join m_inventaris inv on p.id_inventaris = inv.id " +
            " inner join t_pengajuan_inventaris pin on p.id_pengajuan = pin.id " +
            " where pin.id ="+idPengajuan;

        GridView1.DataSource = dataSource;
        GridView1.DataBind();
        GridView1.Columns[3].Visible = false;

    }

    private void blankTable()
    {
        GridView1.DataSource = new List<String>();
        GridView1.DataBind();
    }

  
    protected void GView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GvBind(HiddenIDPengajuan.Value);
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
            TbMemoKabag.Text = reader["memo_kabag"].ToString();
            TbQty.Text = reader["qty"].ToString();
        }

        conn.Close();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "LoadModal", "showModalInventaris();", true);


    }

   
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        GvBind(HiddenIDPengajuan.Value);
    }

   
    protected void SimpanPenerimaan_Click(object sender, EventArgs e)
    {
        string query = "";
        SqlConnection conn = Common.getConnection();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = System.Data.CommandType.Text;
        query = "insert into t_penerimaan_inventaris_detail(id_penerimaan,id_pengajuan_inventaris_detail,id_inventaris, qty_diajukan, qty_diterima, catatan) "
            + " values(@id_penerimaan, @id_pengajuan_inventaris_detail, @id_inventaris, @qty_diajukan, @qty_diterima, @catatan)";
        cmd.CommandText = query;
        cmd.Connection = conn;
        conn.Open(); 
        SqlCommand cmd2;
        int res = 0;

        foreach (GridViewRow row in GridView1.Rows)
        {

            
            String qtyDiterima = ((TextBox) row.FindControl("TbQtyDiterima")).Text;
            String ketPenerimaan = ((TextBox)row.FindControl("TbCatatan")).Text;
            String idJenisInventaris = row.Cells[3].Text; 

            cmd.Parameters.AddWithValue("@id_penerimaan",HiddenIDPenerimaan.Value);
            cmd.Parameters.AddWithValue("@id_pengajuan_inventaris_detail", row.Cells[1].Text);
            cmd.Parameters.AddWithValue("@id_inventaris", idJenisInventaris);
            cmd.Parameters.AddWithValue("@qty_diajukan", row.Cells[4].Text);
            cmd.Parameters.AddWithValue("@qty_diterima", qtyDiterima);
            cmd.Parameters.AddWithValue("@catatan", ketPenerimaan);
            
            res= cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            
            if (res == 1)
            {
                cmd2 = new SqlCommand();
                cmd2.CommandType = System.Data.CommandType.StoredProcedure;
                query = "UPSERT_STOCK_INVENTARIS";
                cmd2.CommandText = query;
                cmd2.Parameters.AddWithValue("@id_penerimaan", HiddenIDPenerimaan.Value);
                cmd2.Parameters.AddWithValue("@id_inventaris", Int64.Parse(idJenisInventaris));
                cmd2.Parameters.AddWithValue("@qty_diterima", Int64.Parse(qtyDiterima));
                cmd2.Parameters.AddWithValue("@module", "Penerimaan");
                cmd2.Parameters.AddWithValue("@reason", "Tambah Penerimaan");
                cmd2.Parameters.AddWithValue("@username", Page.User.Identity.Name); 
                cmd2.Connection = conn;
                cmd2.ExecuteNonQuery();
                cmd2.Parameters.Clear();
            

            }
            
            System.Diagnostics.Debug.WriteLine("Res " + res);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Penerimaan", "Alert('Sukses disimpan');", true);

           // Response.Write("ID Inventaris : "+idInventaris+" qty "+dataQty);
           // System.Diagnostics.Debug.WriteLine("ID Inventaris : " + idInventaris + " qty " + dataQty);
        }

        conn.Close();


        if (res == 1)
            Response.Redirect("ListPenerimaan.aspx");

        
    }

    protected void BtnTambah_Click(object sender, EventArgs e)
    {
        bool val = false;
        if (val)
        {
            HiddenIdInventaris.Value = "0";
            TbMemoKabag.Text = "";
            TbQty.Text = "";

            ScriptManager.RegisterStartupScript(this, this.GetType(), "LoadModal", "showModalInventaris();", true);

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "NothingToShow", "noAddInventaris();", true);

        }
    }
    protected void BtnDeletePenerimaan_Click(object sender, EventArgs e)
    {
        string query = "";
        SqlConnection conn = Common.getConnection();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = query;

        cmd.Parameters.AddWithValue("@id_pengajuan", HiddenIDPengajuan.Value);

        query = "delete t_pengajuan_inventaris where id = @id_pengajuan";


        conn.Open();
        cmd.Connection = conn;
        cmd.CommandText = query;
        cmd.ExecuteNonQuery();

        conn.Close();

        conn = Common.getConnection();
        cmd = new SqlCommand();
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = query;

        cmd.Parameters.AddWithValue("@id_pengajuan", HiddenIDPengajuan.Value);

        query = "delete  t_pengajuan_inventaris_detail where id = @id_pengajuan";


        conn.Open();
        cmd.Connection = conn;
        cmd.CommandText = query;
        cmd.ExecuteNonQuery();

        conn.Close();

       // GvBind();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alertSuksesDelete();", true);
        Response.Redirect("Draft.aspx");
    }
}