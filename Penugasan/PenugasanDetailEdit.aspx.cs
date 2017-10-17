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
           GvBindPenugasanInv();
          //  HiddenIDPenugasan.Value = Session["
           
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

        query = "SELECT t.id_penugasan, t.tanggal_penugasan, t.Judul, u.username "
                +" from t_penugasan_inventaris t left join "
                +" m_user u on t.id_user = u.id_user where t.id_penugasan = '" + idPenugasan + "'";
        System.Diagnostics.Debug.WriteLine("SQL " + idPenugasan);
        cmd.CommandText = query;

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            HiddenIDPenugasan.Value = reader["id_penugasan"].ToString();
            LblPemberiTugas.Text = reader["username"].ToString();
            LblTglPenugasan.Text = String.Format("{0:dd-MMM-yyyy}", (DateTime)reader["tanggal_penugasan"]);
            LblJudul.Text = reader["Judul"].ToString();

        }
        
        conn.Close();

        

    }

   
    protected void GvBindPenugasanInv()
    {
       // string id = Session["id_penugasan"].ToString();
        // string id = (string)Session["no_pengajuan"];
        string query = "select *, case when status_pekerjaan = 0 then 'Belum Dikerjakan' when status_pekerjaan = 1 then 'Dalam Pengerjaan' " +
            " when status_pekerjaan = 2 then 'Selesai' when status_pekerjaan = 3 then 'Tidak Selesai' end as status_desc from t_pengajuan_ref_type " +
            " where id_penugasan = '" + HiddenIDPenugasan.Value+"'";
        SqlDataSource dataSource = new SqlDataSource();
        dataSource.ConnectionString = ConfigurationManager.ConnectionStrings["InventoryConn"].ToString();
        dataSource.SelectCommand = query;
        GridView2.DataSource = dataSource;
        GridView2.DataBind();

       
    }

  
  
    protected void SimpanBeli_Click(object sender, EventArgs e)
    {
       // String idTugas = HIddenIdTugas.Value;

        string query = "";
        SqlConnection conn = Common.getConnection();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = query;

        int stripIDX = DDLPembelian.SelectedItem.Text.IndexOf('-');
        String judul = DDLPembelian.SelectedItem.Text.Substring(stripIDX + 2);

        cmd.Parameters.AddWithValue("@no_ref",DDLPembelian.SelectedValue);
        cmd.Parameters.AddWithValue("@judul_ref", judul);
        cmd.Parameters.AddWithValue("@summary", TbCatatanBeli.Text);
        cmd.Parameters.AddWithValue("@jenis", "PEMBELIAN");
        cmd.Parameters.AddWithValue("@status", 0);
        cmd.Parameters.AddWithValue("@id_penugasan", HiddenIDPenugasan.Value);

        if (HiddenStatusUbah.Value == "Tambah")
        {
            query = "insert into t_pengajuan_ref_type(no_ref,judul_ref,id_penugasan,jenis,summary,status_pekerjaan,updated_date)" +
                " values(@no_ref,@judul_ref,@id_penugasan,@jenis,@summary,@status,getdate())";
        }
        else
        {
            cmd.Parameters.AddWithValue("@idItem", HiddenIdItem.Value);
            query = "update t_pengajuan_ref_type set no_ref=@no_ref,judul_ref=@judul_ref,summary=@summary,updated_date=getdate() " +
                " where id =@idItem ";
        }
        

        conn.Open();
        cmd.Connection = conn;
        cmd.CommandText = query;
        cmd.ExecuteNonQuery();

        conn.Close();

        GvBindPenugasanInv();
   //     HIddenIdTugas.Value = "0";

        ScriptManager.RegisterStartupScript(this, this.GetType(), "HideLain", "alert('Sukses');hideModalBeli();", true);

    }

    protected void SimpanService_Click(object sender, EventArgs e)
    {
        // String idTugas = HIddenIdTugas.Value;

        string query = "";
        SqlConnection conn = Common.getConnection();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = query;

        int stripIDX = DDLService.SelectedItem.Text.IndexOf('-');
        String judul = DDLService.SelectedItem.Text.Substring(stripIDX + 2);

        cmd.Parameters.AddWithValue("@no_ref", DDLService.SelectedValue);
        cmd.Parameters.AddWithValue("@judul_ref", judul);
        cmd.Parameters.AddWithValue("@summary", TbCatatanService.Text);
        cmd.Parameters.AddWithValue("@jenis", "SERVICE");
        cmd.Parameters.AddWithValue("@status", 0);
        cmd.Parameters.AddWithValue("@id_penugasan", HiddenIDPenugasan.Value);

        if (HiddenStatusUbah.Value == "Tambah")
        {
            query = "insert into t_pengajuan_ref_type(no_ref,judul_ref,id_penugasan,jenis,summary,status_pekerjaan,updated_date)" +
                " values(@no_ref,@judul_ref,@id_penugasan,@jenis,@summary,@status,getdate())";
        }
        else
        {
            cmd.Parameters.AddWithValue("@idItem", HiddenIdItem.Value);
            query = "update t_pengajuan_ref_type set no_ref = @no_ref, judul_ref=@judul_ref, summary=@summary, updated_date=getdate() " +
                " where id=@idItem";
        }

        

        conn.Open();
        cmd.Connection = conn;
        cmd.CommandText = query;
        cmd.ExecuteNonQuery();

        conn.Close();

        GvBindPenugasanInv();
      //  HIddenIdTugas.Value = "0";

        ScriptManager.RegisterStartupScript(this, this.GetType(), "HideLain", "alert('Sukses');hideModalBeli();", true);

    }

    protected void SimpanTugasDetail_Click(object sender, EventArgs e)
    {
        String idTugasLain = HIddenIdTugas.Value;

        string query = "";
        SqlConnection conn = Common.getConnection();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = query;

        cmd.Parameters.AddWithValue("@idInventaris", DDLInventaris.SelectedValue);
        cmd.Parameters.AddWithValue("@summary", TbCatatan.Text);
        cmd.Parameters.AddWithValue("@jenis", DDLJenisTugasInv.SelectedValue);
        cmd.Parameters.AddWithValue("@namaInventaris", DDLInventaris.SelectedItem.Text);
      //  cmd.Parameters.AddWithValue("@idPenugasan", HiddenIDPenugasan.Val);
     //   cmd.Parameters.AddWithValue("@qty", TbTugasQty.Text);

        if (HiddenStatusUbah.Value=="Tambah")
        {
            cmd.Parameters.AddWithValue("@id_penugasan", HiddenIDPenugasan.Value);
         //   query = "Insert into t_penugasan_inventaris(id_penugasan,id_inventaris,catatan,qty) values "
           //   + " (@id_penugasan, @id_inventaris, @catatan,@qty)";
            query = "Insert into [t_pengajuan_ref_type](id_ref, no_ref, judul_ref, id_penugasan, jenis,summary,status_pekerjaan,updated_date)" +
                " values(@idInventaris,@idInventaris,@namaInventaris,@id_penugasan,@jenis,@summary,0,getdate())";
            
        }
        else if(HiddenStatusUbah.Value == "Ubah")
        {
            cmd.Parameters.AddWithValue("@idItem", HiddenIdItem.Value);
            query = "update t_pengajuan_ref_type set no_ref = @idInventaris, id_ref =@idInventaris, judul_ref=@namaInventaris, "+
                " summary=@summary, jenis=@jenis, updated_date=getdate() where "
                + " id = @idItem";
        }


        conn.Open();
        cmd.Connection = conn;
        cmd.CommandText = query;
        cmd.ExecuteNonQuery();

        conn.Close();

        GvBindPenugasanInv();
      //  HIddenIdTugas.Value = "0";

        ScriptManager.RegisterStartupScript(this, this.GetType(), "HideLain", "alert('Sukses');hideModalTugasLain();", true);

    }

   
   
    
    protected void DeletePenugasanInv(string id)
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
       // System.Diagnostics.Debug.WriteLine("DADUT");

        HiddenStatusUbah.Value = "Ubah";
        if (e.CommandName == "Ubah")
        {
            System.Diagnostics.Debug.WriteLine("DADUT");
               
            string[] cmdArg = e.CommandArgument.ToString().Split(';');

            string id = cmdArg[0];
            string jenis = cmdArg[1];

            if (jenis.Equals("PEMBELIAN"))
            {
                loadEditPembelian(id);
            }
            else if (jenis.Equals("SERVICE"))
            {
                loadEditService(id);
            }
            else
            {
               // System.Diagnostics.Debug.WriteLine("DADUT");
                loadEditInventaris(id);
            }

            
        }
        if (e.CommandName == "Hapus")
        {
            String id = e.CommandArgument.ToString();
            deleteRow(id);
        }
    }

    private void deleteRow(string id)
    {
        SqlCommand cmd = new SqlCommand();
        SqlConnection conn = Common.getConnection();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "delete [t_pengajuan_ref_type] where id = " + id;

        cmd.Connection = conn;
        conn.Open();



        int result = cmd.ExecuteNonQuery();


        conn.Close();

        if (result == 1)
        {
            GvBindPenugasanInv();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('hapus sukses');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('hapus gagal');", true);
        }


    }

   

    private void loadEditPembelian(string id)
    {
        HIddenIdTugas.Value = "0";
        HiddenStatusUbah.Value = "Ubah";
        HiddenIdItem.Value = id;
        InitDDLPembelian();

        SqlCommand cmd = new SqlCommand();
        SqlConnection conn = Common.getConnection();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "select * from [t_pengajuan_ref_type] where id = " + id;

        cmd.Connection = conn;
        conn.Open();
        string idInv = "0";
        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            idInv = reader["id_ref"].ToString();
            DDLPembelian.SelectedValue = reader["no_ref"].ToString();
            TbCatatanBeli.Text = reader["summary"].ToString();
        }

        conn.Close();

        ScriptManager.RegisterStartupScript(this, this.GetType(), "LoadModal", "showModalBeli();", true);

    }

    private void loadEditService(string id)
    {
        HIddenIdTugas.Value = "0";
        HiddenStatusUbah.Value = "Ubah";
        HiddenIdItem.Value = id;
        InitDDLService();

        SqlCommand cmd = new SqlCommand();
        SqlConnection conn = Common.getConnection();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "select * from [t_pengajuan_ref_type] where id = " + id;

        cmd.Connection = conn;
        conn.Open();

        string idInv = "0";



        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            idInv = reader["id_ref"].ToString();
            DDLService.SelectedValue = reader["no_ref"].ToString();
            TbCatatanService.Text = reader["summary"].ToString();
        }

        conn.Close();

        ScriptManager.RegisterStartupScript(this, this.GetType(), "LoadModal", "showModalService();", true);

    }

    private void loadEditInventaris(string id)
    {
        HIddenIdTugas.Value = "0";
        HiddenStatusUbah.Value = "Ubah";
        HiddenIdItem.Value = id;
        InitDDLPenugasanInv();

        SqlCommand cmd = new SqlCommand();
        SqlConnection conn = Common.getConnection();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "select * from [t_pengajuan_ref_type] where id = " + id;

        cmd.Connection = conn;
        conn.Open();

        string idInv = "0";

        System.Diagnostics.Debug.WriteLine("Edit Inventaris");


        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            idInv = reader["id_ref"].ToString();
            DDLJenisTugasInv.SelectedValue = reader["jenis"].ToString();
            TbCatatan.Text = reader["summary"].ToString();
        }

        conn.Close();

        ScriptManager.RegisterStartupScript(this, this.GetType(), "LoadModal", "showModalTugasLain();", true);

    }

  
    protected void GView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
        GvBindPenugasanInv();
       
    }

   

   
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        GvBindPenugasanInv();
    }

 
    
  
    protected void BtnTambah_Click(object sender, EventArgs e)
    {

        ScriptManager.RegisterStartupScript(this, this.GetType(), "LoadModal", "showModalInventaris();", true);

    }

    protected void BtnTambahTugas_Click(object sender, EventArgs e)
    {
        HIddenIdTugas.Value = "0";
        HiddenStatusUbah.Value = "Tambah";
        TbCatatan.Text = "";
        InitDDLPenugasanInv();
         ScriptManager.RegisterStartupScript(this, this.GetType(), "LoadModal", "showModalTugasLain();", true);

    }

    protected void BtnTambahBeli_Click(object sender, EventArgs e)
    {
        HiddenStatusUbah.Value = "Tambah";
        InitDDLPembelian();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "LoadModal", "showModalBeli();", true);

    }


    protected void BtnTambahService_Click(object sender, EventArgs e)
    {
        HiddenStatusUbah.Value = "Tambah";
        InitDDLService();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "LoadModal", "showModalService();", true);

    }

    private string GetNamaInventaris(String id)
    {
        string namaInventaris = "";
        SqlCommand cmd = new SqlCommand();
        SqlConnection conn = Common.getConnection();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "select nama_inventaris from m_inventaris where id = "+id;

        cmd.Connection = conn;
        conn.Open();

        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            namaInventaris = reader["nama_inventaris"].ToString();
        }
       
        conn.Close();
        return namaInventaris;
    }


    private void InitDDLPenugasanInv()
    {
        SqlCommand cmd = new SqlCommand();
        SqlConnection conn = Common.getConnection();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "select distinct id_inventaris, nama_inventaris from m_inventaris order by nama_inventaris";

        cmd.Connection = conn;
        conn.Open();

        DDLInventaris.DataSource = cmd.ExecuteReader();
        DDLInventaris.DataValueField = "id_inventaris";
        DDLInventaris.DataTextField = "nama_inventaris";
        DDLInventaris.DataBind();

        conn.Close();
    }


    private void InitDDLPembelian()
    {
        SqlCommand cmd = new SqlCommand();
        SqlConnection conn = Common.getConnection();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "select distinct id_pembelian, (id_pembelian + ' - '+no_pembelian) as title from t_pembelian";

        cmd.Connection = conn;
        conn.Open();

        DDLPembelian.DataSource = cmd.ExecuteReader();
        DDLPembelian.DataValueField = "id_pembelian";
        DDLPembelian.DataTextField = "title";
        DDLPembelian.DataBind();

        conn.Close();
    }

    private void InitDDLService()
    {
        SqlCommand cmd = new SqlCommand();
        SqlConnection conn = Common.getConnection();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "select distinct id_service_keluar, (id_service_keluar + ' - '+judul) as title from t_service_keluar";

        cmd.Connection = conn;
        conn.Open();

        DDLService.DataSource = cmd.ExecuteReader();
        DDLService.DataValueField = "id_service_keluar";
        DDLService.DataTextField = "title";
        DDLService.DataBind();

        conn.Close();
    }

    protected void EditPenugasanInv(string id)
    {
        SqlConnection conn = Common.getConnection();
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;
        HIddenIdTugas.Value = id;
        string query = "Select id, id_inventaris, qty, catatan from t_penugasan_inventaris where id = "+id+"";
        cmd.CommandText = query;
        conn.Open();
        System.Diagnostics.Debug.Write("ID Tugas Lain : " + id);

        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            HIddenIdTugas.Value= reader["id"].ToString();
            DDLInventaris.SelectedValue = reader["id_inventaris"].ToString();
            TbTugasQty.Text = reader["qty"].ToString();
            TbCatatan.Text = reader["catatan"].ToString();
        }
        conn.Close();
       
    }



    protected void BtnSimpan_Click(object sender, EventArgs e)
    {

        string query = "";
        SqlConnection conn = Common.getConnection();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = System.Data.CommandType.Text;
        query = "update t_penugasan_inventaris set status = 0 where id_penugasan = '" + HiddenIDPenugasan.Value + "'";
        cmd.Connection = conn;
        cmd.CommandText = query;
        conn.Open();
        cmd.ExecuteNonQuery();

   //     string userId = Request.Cookies["UserSettings"]["id_user"];

        /*        SqlCommand cmd2;


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


    protected void BtnDraft_Click(object sender, EventArgs e)
    {
        
        Response.Redirect("Draft.aspx");
    }


    protected void BtnDeletePengajuan_Click(object sender, EventArgs e)
    {
        string query = "";
        SqlConnection conn = Common.getConnection();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = query;

        cmd.Parameters.AddWithValue("@id_pengajuan", HiddenIDPenugasan.Value);

        query = "delete t_pengajuan_inventaris where id_pengajuan = @id_pengajuan";


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

        query = "delete  t_pengajuan_inventaris_detail where id_pengajuan_inventaris = @id_pengajuan";


        conn.Open();
        cmd.Connection = conn;
        cmd.CommandText = query;
        cmd.ExecuteNonQuery();

        conn.Close();

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alertSuksesDelete();", true);
        Response.Redirect("Draft.aspx");
    }

  
}