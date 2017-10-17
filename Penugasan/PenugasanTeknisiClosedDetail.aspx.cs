using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
public partial class Penugasan_OnProgressTeknisiDetail : System.Web.UI.Page
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

        query = "SELECT t.id, t.tanggal_penugasan, t.catatan, u.username "
                + " from t_penugasan t inner join "
                + " m_user u on t.id_kabag = u.id where t.id = " + idPenugasan + "";
        cmd.CommandText = query;

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            HiddenIDPenugasan.Value = reader["id"].ToString();
            LblPemberiTugas.Text = reader["username"].ToString();
            LblTglPenugasan.Text = String.Format("{0:dd-MMM-yyyy}", (DateTime)reader["tanggal_penugasan"]);
            LblCatatan.Text = reader["catatan"].ToString();

        }

        conn.Close();



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
        dataSource.SelectCommand = " select id, id_penugasan, judul, qty, catatan, "
        + "case when status = 0 then 'BARU'" 
        + "when status = 1 then 'SEDANG DIKERJAKAN' when status = 2 then 'SELESAI' "
        + "when status = 3 then 'TIDAK DAPAT DIKERJAKAN' end as Status, Catatan_Teknisi from t_penugasan_lain_detail where id_penugasan = " + id + "";

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
        string query = "Select id, judul, qty, catatan, status, catatan_teknisi from t_penugasan_lain_detail where id = " + id + "";
        cmd.CommandText = query;
        conn.Open();
        System.Diagnostics.Debug.Write("ID Tugas Lain : " + id);

        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            TbJudul.Text = reader["judul"].ToString();
            TbCatatan.Text = reader["catatan"].ToString();
            TbPopQty.Text = reader["qty"]!=DBNull.Value?reader["qty"].ToString():"";
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





    protected void BtnSelesai_Click(object sender, EventArgs e)
    {
       
        Response.Redirect("PenugasanTeknisiClosed.aspx");

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