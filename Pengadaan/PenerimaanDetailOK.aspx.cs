﻿using System;
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
           BindPenerimaan();
            
        }

    }



    protected void BindPenerimaan()
    {
        SqlConnection conn = Common.getConnection();
        string query = "";
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;
      //  if(String.IsNullOrEmpty(HiddenIDPenerimaan.Value))
             HiddenIDPenerimaan.Value = (string)Session["id_penerimaan"];
      //  string idPenerimaan = "1";
        string idBeliService = "";

        System.Diagnostics.Debug.Write("ID PENERIMAAN : " + HiddenIDPenerimaan.Value);
     /*   query = "select a.id, a.id_penerimaan, a.no_penerimaan, a.judul_penerimaan, a.keterangan, "
                +" a.id_pengajuan, a.id_service, a.tanggal_penerimaan, b.no_pengajuan, a.id_user_kabag, c.username" 
                +"     from t_penerimaan_inventaris a "
                 +"    left join t_pengajuan_inventaris" 
                 +"    b on a.id_pengajuan = b.id left join"
                 +"    m_user c on a.id_user_kabag = c.id where a.id = "+HiddenIDPenerimaan.Value+" "; */

        query = "select a.*, b.username from t_penerimaan a left join m_user b on a.id_user = b.id_user where a.id_penerimaan = '" + HiddenIDPenerimaan.Value + "'";
        System.Diagnostics.Debug.WriteLine("Query : " + query);
        cmd.CommandText = query;

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();
        if (reader.Read())
        {



            LblID.Text = reader["id_penerimaan"].ToString().Trim();
            LblJudulPenerimaan.Text = reader["judul_penerimaan"].ToString().Trim();
            LblKeterangan.Text = reader["keterangan"].ToString();
            //  LblNoPengajuan.Text = reader["no_pengajuan"].ToString();
            LblPenerima.Text = reader["creator"].ToString();
            try
            {
                LblTglPenerimaan.Text = String.Format("{0:dd-MMM-yyyy}", (DateTime)reader["tanggal_penerimaan"]);

            }
            catch
            {
                LblTglPenerimaan.Text = reader["tanggal_penerimaan"].ToString();
            }
      //      LblNoPenerimaan.Text = reader["no_penerimaan"].ToString();

            string tipe = reader["tipe_ref"].ToString();

            LblRefPengajuan.Text = reader["id_pengajuan"].ToString();
                
            HiddenType.Value = tipe;

            if (tipe.Equals("1"))
                idBeliService = reader["id_pembelian"].ToString();
            else
                idBeliService = reader["id_service_keluar"].ToString();

            GetNoPengajuan(idBeliService, tipe);
                   
            conn.Close();


            LblRefBeliService.Text = idBeliService;

            GvBindBarang(LblID.Text);

            
        }


        HiddenIDPengajuan.Value = idBeliService;

    }

    protected void GvBindBarang(string idPenerimaan)
    {
         string query = "";
         query = "Select i.nama_inventaris, i.id_inventaris, a.qty_diajukan, a.catatan_pengajuan, a.qty_diterima, a.keterangan "+
            " from t_penerimaan_detail a inner join m_inventaris i on a.id_inventaris = i.id_inventaris "+
            " where a.id_penerimaan = '" + idPenerimaan + "'";
        

      
        SqlDataSource dataSource = new SqlDataSource();
        dataSource.ConnectionString = ConfigurationManager.ConnectionStrings["InventoryConn"].ToString();
        dataSource.SelectCommand =  query;
        GridView1.DataSource = dataSource;
        GridView1.DataBind();
       // GridView1.Columns[3].Visible = false;

    }

    protected void GetNoPengajuan(string id, string tipe)
    {
        SqlConnection conn = Common.getConnection();
        string query = "";
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;


        if (tipe.Equals("1"))
        {
            query = "select id_pengajuan_inventaris as id_pengajuan from t_pembelian where id_pembelian = '" + id + "'";
            System.Diagnostics.Debug.WriteLine("Q :" + query);
       
        }
        else if (tipe.Equals("2"))
        {
            query = "select id_pengajuan_service as id_pengajuan from t_service_keluar where id_service_keluar = '" + id + "'";
       
        }
        cmd.CommandText = query;

        conn.Open();
        try
        {
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            LblRefPengajuan.Text = reader["id_pengajuan"].ToString().Trim();
        }
        catch
        {
        }

        conn.Close();
        
        
    }

    protected void GvBindService(string idPengajuan)
    {

        string query = "";
        query = "select p.id_pengajuan_service_detail as id, p.id_pengajuan_service, p.id_inventaris, p.qty, p.catatan_kabag, p.created_date, "+
         " pin.no_pengajuan_service, inv.nama_inventaris from t_pengajuan_service_detail p inner join m_inventaris inv on p.id_inventaris = inv.id_inventaris "+
         "  inner join t_pengajuan_service pin on p.id_pengajuan_service = pin.id_pengajuan_service "+
          " where pin.id_pengajuan_service = '"+LblRefPengajuan.Text + "'";

        System.Diagnostics.Debug.WriteLine("SQL Q : " + query);

        query = query + " union all " +
           " select cast(tp.id as varchar(20)) as id, '0', tp.id_inventaris, 0, '', tp.created_date, " +
            "'', inv.nama_inventaris from t_penerimaan_inventaris_nopengajuan tp inner join m_inventaris inv on tp.id_inventaris = inv.id_inventaris " +
            " where id_penerimaan = '" + HiddenIDPenerimaan.Value+"'";


        SqlDataSource dataSource = new SqlDataSource();
        dataSource.ConnectionString = ConfigurationManager.ConnectionStrings["InventoryConn"].ToString();
        dataSource.SelectCommand = query;
            
            /*"select p.id, p.id_pengajuan, p.id_inventaris, p.qty, p.memo_kabag, p.created_date, " +
            " pin.no_pengajuan, inv.nama_inventaris from t_service_inventaris_detail p inner join m_inventaris inv on p.id_inventaris = inv.id " +
            " inner join t_service_inventaris pin on p.id_pengajuan = pin.id " +
            " where pin.id =" + idPengajuan; */

        GridView1.DataSource = dataSource;
        GridView1.DataBind();
      //  GridView1.Columns[3].Visible = false;

    }


    protected void GvBindNoPengajuan()
    {

        string query = "";
       
        query = 
           " select tp.id, 0 as id_pengajuan, tp.id_inventaris, 0 as qty, '' as memo_kabag, tp.created_date, " +
            "'' as no_pengajuan, inv.nama_inventaris from t_penerimaan_inventaris_nopengajuan tp left join m_inventaris inv on tp.id_inventaris = inv.id_inventaris " +
            " where tp.id_penerimaan = '" + HiddenIDPenerimaan.Value+"'";


        SqlDataSource dataSource = new SqlDataSource();
        dataSource.ConnectionString = ConfigurationManager.ConnectionStrings["InventoryConn"].ToString();
        dataSource.SelectCommand = query;

        GridView1.DataSource = dataSource;
        GridView1.DataBind();
      //  GridView1.Columns[3].Visible = false;
      //  Response.Write("ID Terima :" + HiddenIDPenerimaan.Value);   

    }

    private void blankTable()
    {
        GridView1.DataSource = new List<String>();
        GridView1.DataBind();
    }

  
    protected void GView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        if (Session["tipe"].Equals("barang"))
            GvBindBarang(HiddenIDPengajuan.Value);
        else if (Session["tipe"].Equals("service"))
            GvBindService(HiddenIDPengajuan.Value);

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
        if (Session["tipe"].Equals("barang"))
            GvBindBarang(HiddenIDPengajuan.Value);
        else if (Session["tipe"].Equals("service"))
            GvBindService(HiddenIDPengajuan.Value);
    }

   
    protected void SimpanPenerimaan_Click(object sender, EventArgs e)
    {
        string query = "";
        SqlConnection conn = Common.getConnection();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = System.Data.CommandType.Text;
        query = "insert into t_penerimaan_detail(id_penerimaan,id_pengajuan_inventaris_detail,id_inventaris, qty_diajukan, qty_diterima, keterangan) "
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
        /*    
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
            */
            System.Diagnostics.Debug.WriteLine("Res " + res);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Penerimaan", "Alert('Sukses disimpan');", true);

           // Response.Write("ID Inventaris : "+idInventaris+" qty "+dataQty);
           // System.Diagnostics.Debug.WriteLine("ID Inventaris : " + idInventaris + " qty " + dataQty);
        }

        conn.Close();


        if (res == 1)
            Response.Redirect("ListPenerimaan.aspx");

        
    }

    protected void BtnOK_Click(object sender, EventArgs e)
    {
        Response.Redirect("ListPenerimaan.aspx");
    }

    protected bool InitInsertDDLInventaris()
    {
        SqlConnection conn = Common.getConnection();
        string query = "";
        bool val = false;
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;

        if (HiddenType.Equals("barang"))
        {
            query = "select id_inventaris, nama_inventaris, harga from m_inventaris where id_inventaris not in(select id_inventaris from "
            + " t_pengajuan_inventaris_detail where id_pengajuan = '" + HiddenIDPengajuan.Value + "') order by id_inventaris asc";

        }
        else if (HiddenType.Equals("service"))
        {
            query = "select id_inventaris, nama_inventaris, harga from m_inventaris where id_inventaris not in(select id_inventaris from "
            + " t_service_inventaris_detail where id_pengajuan_service ='" + HiddenIDPengajuan.Value + "') order by id_inventaris asc";

        }
        else
        {
            query = "select id_inventaris, nama_inventaris from m_inventaris";
        }
           
        cmd.CommandText = query;

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();

        if (reader.HasRows)
        {
            val = true;
            DDLInventaris.DataSource = reader;
            DDLInventaris.DataValueField = "id_inventaris";
            DDLInventaris.DataTextField = "nama_inventaris";
            DDLInventaris.DataBind();
            //   DDLHarga.DataBind();
        }


        conn.Close();


        return val;
    }

    protected void SimpanInventaris_Click(object sender, EventArgs e)
    {
        string query = "";
        SqlConnection conn = Common.getConnection();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = query;

        cmd.Parameters.AddWithValue("@qty_diterima", TbQty.Text);
        cmd.Parameters.AddWithValue("@id_penerimaan",HiddenIDPenerimaan.Value);
        cmd.Parameters.AddWithValue("@memo_kabag", TbMemoKabag.Text);
        cmd.Parameters.AddWithValue("@id_inventaris", DDLInventaris.SelectedValue);
      
        query = "INSERT INTO t_penerimaan_inventaris_nopengajuan (id_penerimaan, qty_diterima, id_inventaris, catatan, created_date) " +
        " VALUES(@id_penerimaan,  @qty_diterima, @id_inventaris, @memo_kabag,getdate())";
       

        conn.Open();
        cmd.Connection = conn;
        cmd.CommandText = query;
        cmd.ExecuteNonQuery();

        conn.Close();

        BindPenerimaan();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeAddInvModal();", true);
    }



    protected void BtnDeletePenerimaan_Click(object sender, EventArgs e)
    {
        string query = "";
        SqlConnection conn = Common.getConnection();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = query;

        cmd.Parameters.AddWithValue("@id_penerimaan", HiddenIDPenerimaan.Value);

        query = "delete t_penerimaan_inventaris where id = @id_penerimaan";


        conn.Open();
        cmd.Connection = conn;
        cmd.CommandText = query;
        cmd.ExecuteNonQuery();

        conn.Close();

        conn = Common.getConnection();
        cmd = new SqlCommand();
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = query;

        cmd.Parameters.AddWithValue("@id_penerimaan", HiddenIDPenerimaan.Value);

        query = "delete t_penerimaan_inventaris_detail where id = @id_penerimaan";


        conn.Open();
        cmd.Connection = conn;
        cmd.CommandText = query;
        cmd.ExecuteNonQuery();

        conn.Close();

       // GvBind();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alertSuksesDelete();", true);
        Response.Redirect("ListPenerimaan.aspx");
    }
}