using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class Penugasan_PenugasanTeknisiDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindPenugasan();
          //  GvBindPenugasanLain();

            GvBindPenugasanInv();
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

        query = "SELECT t.id_penugasan, t.tanggal_penugasan, t.judul, u.username "
                + " from t_penugasan_inventaris t inner join "
                + " m_user u on t.id_user = u.id_user where t.id_penugasan = '" + idPenugasan + "'";
        cmd.CommandText = query;

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            HiddenIDPenugasan.Value = reader["id_penugasan"].ToString();
            LblPemberiTugas.Text = reader["username"].ToString();
            LblTglPenugasan.Text = String.Format("{0:dd-MMM-yyyy}", (DateTime)reader["tanggal_penugasan"]);
            LblJudul.Text = reader["judul"].ToString();

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

    protected void GvBindPenugasanLain()
    {
        string id = Session["id_penugasan"].ToString();
        // string id = (string)Session["no_pengajuan"];
        SqlDataSource dataSource = new SqlDataSource();
        dataSource.ConnectionString = ConfigurationManager.ConnectionStrings["InventoryConn"].ToString();
        dataSource.SelectCommand = " select a.id_inventaris, a.id_penugasan, b.nama_inventaris,a.qty,a.Catatan, "
        +"case when a.status = 0 then 'BARU' "
        +"when a.status = 1 then 'SEDANG DIKERJAKAN' when status = 2 then 'SELESAI' "
        +"when a.status = 3 then 'TIDAK DAPAT DIKERJAKAN' end as Status, a.Catatan_Teknisi from t_penugasan_inventaris a "
        +"left join m_inventaris b on a.id_inventaris = b.id where a.id_penugasan = " + id + "";

        GridView2.DataSource = dataSource;
        GridView2.DataBind();


    }

  
    protected void GView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
        GvBindPenugasanLain();

    }


    protected void RowCommand_Click(object sender, GridViewCommandEventArgs e)
    {
        
        if (e.CommandName == "PilihLain")
        {
            System.Diagnostics.Debug.Write("Test Ubah Lain");
            string idLain = Convert.ToString(e.CommandArgument);
            EditLain(idLain);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "LoadModal", "showModalTugasLain();", true);


        }
    }

    protected void EditLain(string id)
    {
        SqlConnection conn = Common.getConnection();
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;
        HIddenIdTugasLain.Value = id;
        string query = "Select id, judul, catatan, status, catatan_teknisi from t_penugasan_lain_detail where id = " + id + "";
        cmd.CommandText = query;
        conn.Open();
        System.Diagnostics.Debug.Write("ID Tugas Lain : " + id);

        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            TbJudul.Text = reader["judul"].ToString();
            TbCatatan.Text = reader["catatan"].ToString();
            DDLStatus.SelectedValue = reader["status"].ToString();
            TbCatatanTeknisi.Text = reader["catatan_teknisi"].ToString();
        }
        conn.Close();

    }

    protected void SimpanTugasLain_Click(object sender, EventArgs e)
    {
        String idTugasLain = HIddenIdTugasLain.Value;

        string query = "";
        SqlConnection conn = Common.getConnection();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = query;

        cmd.Parameters.AddWithValue("@catatan_teknisi", TbCatatanTeknisi.Text);
        cmd.Parameters.AddWithValue("@status", DDLStatus.SelectedValue);
        cmd.Parameters.AddWithValue("@id_penugasan_lain", idTugasLain);
        
        query = "update t_penugasan_lain_detail set status = @status, catatan_teknisi = @catatan_teknisi where "
                + " id = @id_penugasan_lain";
        
        conn.Open();
        cmd.Connection = conn;
        cmd.CommandText = query;
        cmd.ExecuteNonQuery();

        conn.Close();

        GvBindPenugasanLain();
        HIddenIdTugasLain.Value = "0";

        ScriptManager.RegisterStartupScript(this, this.GetType(), "HideLain", "alert('Sukses');hideModalTugasLain();", true);

    }






    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        GvBindPenugasanLain();
    }

  



    protected void BtnProgress_Click(object sender, EventArgs e)
    {
        string query = "";
      //  string userId = Request.Cookies["UserSettings"]["id_user"];
        string userId = "1";


        SqlConnection conn = Common.getConnection();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.Connection = conn;
        query = "update t_penugasan_inventaris set is_start = 1, last_updated_date = getdate(), updater = "+userId+" where id_penugasan = '" + HiddenIDPenugasan.Value + "'";
        
        cmd.CommandText = query;

        conn.Open();
        cmd.ExecuteNonQuery();

        
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Penerimaan", "Alert('Sukses Ubah Status');", true);

        conn.Close();
        Response.Redirect("ListPenugasanTeknisi.aspx");
       
    }

    

   



    protected void BtnDeletePengajuan_Click(object sender, EventArgs e)
    {
        string query = "";
        SqlConnection conn = Common.getConnection();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = query;

        cmd.Parameters.AddWithValue("@id_pengajuan", HiddenIDPenugasan.Value);

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

        cmd.Parameters.AddWithValue("@id_pengajuan", HiddenIDPenugasan.Value);

        query = "delete  t_pengajuan_inventaris_detail where id = @id_pengajuan";


        conn.Open();
        cmd.Connection = conn;
        cmd.CommandText = query;
        cmd.ExecuteNonQuery();

        conn.Close();

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alertSuksesDelete();", true);
        Response.Redirect("Draft.aspx");
    }

}