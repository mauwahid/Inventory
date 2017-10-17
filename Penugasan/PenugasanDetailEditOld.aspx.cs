using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
public partial class Penugasan_PilihInventarisRusak : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindPenugasan();
            GvBindPenugasanLain();
           
        }

    }



    protected void BindPenugasan()
    {
        SqlConnection conn = Common.getConnection();
        string query = "";
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;
        string idPenugasan = (string)Session["id_penugasan"];

        query = "SELECT t.id, t.tanggal_penugasan, t.judul, u.username "
                +" from t_penugasan t inner join "
                +" m_user u on t.id_kabag = u.id where t.id = " + idPenugasan + "";
        cmd.CommandText = query;

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            HiddenIDPenugasan.Value = reader["id"].ToString();
            LblPemberiTugas.Text = reader["username"].ToString();
            LblTglPenugasan.Text = String.Format("{0:dd-MMM-yyyy}", (DateTime)reader["tanggal_penugasan"]);
            LblJudul.Text = reader["judul"].ToString();

        }
        
        conn.Close();



    }

   
    protected void GvBindPenugasanLain()
    {
        string id = Session["id_penugasan"].ToString();
        // string id = (string)Session["no_pengajuan"];
        SqlDataSource dataSource = new SqlDataSource();
        dataSource.ConnectionString = ConfigurationManager.ConnectionStrings["InventoryConn"].ToString();
        dataSource.SelectCommand = " select a.id,b.nama_inventaris, a.id_penugasan, a.id_inventaris, a.qty, a.catatan from t_penugasan_inventaris a "
            + " inner join m_inventaris b on a.id_inventaris = b.id where a.id_penugasan = " + id + "";

        GridView2.DataSource = dataSource;
        GridView2.DataBind();

       
    }

  
  
    protected void DeletePerbaikanInv(string id)
    {
        SqlConnection conn = Common.getConnection();
        string query = "";
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;

        query = "delete t_penugasan_detail where id = " + id + "";
        cmd.CommandText = query;

        conn.Open();
        int reader = cmd.ExecuteNonQuery();

        conn.Close();

    }

    protected void DeleteLain(string id)
    {
        SqlConnection conn = Common.getConnection();
        string query = "";
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;

        query = "delete t_penugasan_inventaris where id = " + id + "";
        cmd.CommandText = query;

        conn.Open();
        int reader = cmd.ExecuteNonQuery();

        conn.Close();
       

    }

   

    protected void RowCommand_Click(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "UbahLain")
        {
            System.Diagnostics.Debug.Write("Test Ubah Lain");
            string idLain = Convert.ToString(e.CommandArgument);
            EditPenugasanInv(idLain);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "LoadModal", "showModalTugasLain();", true);


        }
       

        if (e.CommandName == "HapusLain")
        {
            string idLain = Convert.ToString(e.CommandArgument);
            DeleteLain(idLain);
            GvBindPenugasanLain();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "LoadModal", "activeTab2();", true);


        }

        

    }

  
    protected void GView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
        GvBindPenugasanLain();
       
    }

   

   
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        GvBindPenugasanLain();
    }

 
    
  
    protected void SimpanPengajuan_Click(object sender, EventArgs e)
    {
        string query = "";
        SqlConnection conn = Common.getConnection();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = query;

        cmd.Parameters.AddWithValue("@id_pengajuan", HiddenIDPenugasan.Value);

        query = "Update t_pengajuan_inventaris set status_approval = 0 where id = @id_pengajuan";


        conn.Open();
        cmd.Connection = conn;
        cmd.CommandText = query;
        cmd.ExecuteNonQuery();

        conn.Close();

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alertSukses();", true);
        Response.Redirect("ListPenugasanTeknisi.aspx");
    }

    protected void BtnTambah_Click(object sender, EventArgs e)
    {

        ScriptManager.RegisterStartupScript(this, this.GetType(), "LoadModal", "showModalInventaris();", true);

    }

    private void InitDDLPenugasanInv()
    {
        SqlCommand cmd = new SqlCommand();
        SqlConnection conn = Common.getConnection();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "select distinct id, nama_inventaris from m_inventaris order by nama_inventaris";

        cmd.Connection = conn;
        conn.Open();

        DDLInventaris.DataSource = cmd.ExecuteReader();
        DDLInventaris.DataValueField = "id";
        DDLInventaris.DataTextField = "nama_inventaris";
        DDLInventaris.DataBind();

        conn.Close();
    }


    protected void BtnTambahTugas_Click(object sender, EventArgs e)
    {
        HIddenIdTugas.Value = "0";
        InitDDLPenugasanInv();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "LoadModal", "showModalTugasLain();", true);

    }


    protected void EditPenugasanInv(string id)
    {
        SqlConnection conn = Common.getConnection();
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;
        HIddenIdTugas.Value = id;
        string query = "Select id, id_inventaris, qty, catatan from t_penugasan_inventaris where id = " + id + "";
        cmd.CommandText = query;
        conn.Open();
        System.Diagnostics.Debug.Write("ID Tugas Lain : " + id);

        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            HIddenIdTugas.Value = reader["id"].ToString();
            DDLInventaris.SelectedValue = reader["id_inventaris"].ToString();
            TbTugasQty.Text = reader["qty"].ToString();
            TbCatatan.Text = reader["catatan"].ToString();
        }
        conn.Close();

    }



    protected void SimpanTugasDetail_Click(object sender, EventArgs e)
    {
        String idTugasLain = HIddenIdTugas.Value;

        string query = "";
        SqlConnection conn = Common.getConnection();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = query;

        cmd.Parameters.AddWithValue("@id_inventaris", DDLInventaris.SelectedValue);
        cmd.Parameters.AddWithValue("@catatan", TbCatatan.Text);
        cmd.Parameters.AddWithValue("@qty", TbTugasQty.Text);

        if (idTugasLain.Equals("0") | idTugasLain.Equals(""))
        {
            cmd.Parameters.AddWithValue("@id_penugasan", HiddenIDPenugasan.Value);
            query = "Insert into t_penugasan_inventaris(id_penugasan,id_inventaris,catatan,qty) values "
              + " (@id_penugasan, @id_inventaris, @catatan,@qty)";

        }
        else
        {
            cmd.Parameters.AddWithValue("@id_penugasan_inventaris", HIddenIdTugas.Value);
            query = "update t_penugasan_inventaris set id_inventaris = @id_inventaris, catatan = @catatan, qty=@qty where "
                + " id = @id_penugasan_inventaris";
        }


        conn.Open();
        cmd.Connection = conn;
        cmd.CommandText = query;
        cmd.ExecuteNonQuery();

        conn.Close();

        GvBindPenugasanLain();
        HIddenIdTugas.Value = "0";

        ScriptManager.RegisterStartupScript(this, this.GetType(), "HideLain", "alert('Sukses');hideModalTugasLain();", true);

    }

  

    protected void BtnSimpan_Click(object sender, EventArgs e)
    {

        string query = "";
        SqlConnection conn = Common.getConnection();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = System.Data.CommandType.Text;
        query = "update t_penugasan set status = 0 where id = " + HiddenIDPenugasan.Value + "";
        cmd.Connection = conn;
        cmd.CommandText = query;
        conn.Open();
        cmd.ExecuteNonQuery();

        string userId = Request.Cookies["UserSettings"]["id_user"];

        SqlCommand cmd2;

/*
        foreach (GridViewRow row in GridView2.Rows)
        {
            string qtyDiperbaiki = ((TextBox)row.FindControl("TbQtyDiperbaiki")).Text;
            string idPenugasan = HiddenIDPenugasan.Value;
            string idInventaris = row.Cells[2].Text;
            
            cmd2 = new SqlCommand();
            cmd2.CommandType = System.Data.CommandType.StoredProcedure;
            cmd2.Connection = conn;
            query = "UPSERT_PENUGASAN_INVENTARIS_DETAIL";
            
            cmd2.CommandText = query;
            cmd2.Parameters.AddWithValue("@id_penugasan",idPenugasan);
            cmd2.Parameters.AddWithValue("@id_inventaris", idInventaris);
            cmd2.Parameters.AddWithValue("@qty_diperbaiki", qtyDiperbaiki);
            cmd2.Parameters.AddWithValue("@updater", userId);

            int res = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();

            cmd2.ExecuteNonQuery();
        
        }*/
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Penerimaan", "Alert('Sukses Dikirim ke petugas');", true);

        conn.Close();
        Response.Redirect("ListPenugasan.aspx");
    }

 


    protected void BtnDeletePengajuan_Click(object sender, EventArgs e)
    {
        string query = "";
        SqlConnection conn = Common.getConnection();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = query;

        cmd.Parameters.AddWithValue("@id", HiddenIDPenugasan.Value);

        query = "delete t_penugasan where id = @id";


        conn.Open();
        cmd.Connection = conn;
        cmd.CommandText = query;
        cmd.ExecuteNonQuery();

        conn.Close();

        conn = Common.getConnection();
        cmd = new SqlCommand();
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = query;

        cmd.Parameters.AddWithValue("@id_pengajuan", HiddenIDPenugasan.Value);

        query = "delete t_penugasan_inventaris where id_penugasan = @id_pengajuan";


        conn.Open();
        cmd.Connection = conn;
        cmd.CommandText = query;
        cmd.ExecuteNonQuery();

        conn.Close();

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alertSuksesDelete();", true);
        Response.Redirect("Draft.aspx");
    }



    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Draft.aspx");
    }

}