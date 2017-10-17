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


       LoadStatusTugas();
    }

    protected void GvBindPenugasanInv()
    {
        // string id = Session["id_penugasan"].ToString();
        // string id = (string)Session["no_pengajuan"];
        string query = "select *, case when status_pekerjaan = 0 then 'Dalam Proses' when status_pekerjaan = 1 then 'Dalam Pengerjaan' " +
            " when status_pekerjaan = 2 then 'Selesai' when status_pekerjaan = 3 then 'Tidak Selesai' end as status_desc from t_pengajuan_ref_type " +
            " where id_penugasan = '" + HiddenIDPenugasan.Value+"'";
        SqlDataSource dataSource = new SqlDataSource();
        dataSource.ConnectionString = ConfigurationManager.ConnectionStrings["InventoryConn"].ToString();
        dataSource.SelectCommand = query;
        GridView2.DataSource = dataSource;
        GridView2.DataBind();


    }

    private void LoadStatusTugas()
    {
        SqlConnection conn = Common.getConnection();
        string query = "";
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;
        string idPenugasan = (string)Session["id_penugasan"];

        query = "select status_pekerjaan from t_pengajuan_ref_type where id_penugasan = '" + idPenugasan + "' "
            + " order by status_pekerjaan asc";
        cmd.CommandText = query;

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();
        int statusTotal = 0; // 
        int count = 0;
        int total0 = 0;
        int total1 = 0;
        int total2 = 0;
        int total3 = 0;
        // 0 Belum, 1 Masih dikerjakan, 2 Selesai Semua, 3 Selesai Sebagian, 4 Sebagian Tidak Bisa dikerjakan 
        // 5 Tidak bisa dikerjakan semua
        
        while (reader.Read())
        {
            int status = Int32.Parse(reader["status_pekerjaan"].ToString());
            switch (status)
            {
                case 0 :
                    total0 = total0 + 1;
                    break;
                case 1:
                    total1 = total1 + 1;
                    break;
                case 2 :
                    total2 = total2 + 1;
                    break;
                case 3 :
                    total3 = total3 + 1;
                    break;
            }

            count++;


        }
        

        if (total0 == count)
            statusTotal = 0;
        else if (total1 == count)
            statusTotal = 1;
        else if (total2 == count)
            statusTotal = 2;
        else if (total3 == count)
            statusTotal = 5;
        else if (total3 > 0)
            statusTotal = 4;
        else if (total2 > 0)
            statusTotal = 3;
        conn.Close();
        conn = Common.getConnection();
        SqlCommand cmd2 = new SqlCommand();
        conn.Open();
        cmd2.Connection = conn;
        cmd2.CommandType = CommandType.Text;
        cmd2.CommandText = "update t_penugasan_inventaris set status = "+statusTotal+" where id_penugasan ='"+HiddenIDPenugasan.Value+"'";

        cmd2.ExecuteNonQuery();

        conn.Close();
        
        if (statusTotal == 0)
        {
            LblStatus.Text = "Dalam Proses";
            LblStatus.CssClass = "label pull-left bg-black";
        }
        else if(statusTotal == 1)
        {
            LblStatus.Text = "Masih dikerjakan";
            LblStatus.CssClass = "label pull-left bg-green";
        }
        else if (statusTotal == 2)
        {
            LblStatus.Text = "SELESAI";
            LblStatus.CssClass = "label pull-left bg-blue";
        }
        else if (statusTotal == 3)
        {
            LblStatus.Text = "Selesai Sebagian";
            LblStatus.CssClass = "label pull-left bg-green";
        }
        else if (statusTotal == 5)
        {
            LblStatus.Text = "Tidak dapat dikerjakan";
            LblStatus.CssClass = "label pull-left bg-red";
        }
        else if (statusTotal == 4)
        {
            LblStatus.Text = "Sebagian Tidak dapat dikerjakan";
            LblStatus.CssClass = "label pull-left bg-yellow";
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

    protected void GvBindPenugasanLain()
    {
        string id = Session["id_penugasan"].ToString();
        // string id = (string)Session["no_pengajuan"];
        SqlDataSource dataSource = new SqlDataSource();
        dataSource.ConnectionString = ConfigurationManager.ConnectionStrings["InventoryConn"].ToString();
        dataSource.SelectCommand = " select a.id, a.id_penugasan, b.nama_inventaris,a.qty,a.Catatan, "
        + "case when a.status = 0 then 'BARU' "
        + "when a.status = 1 then 'SEDANG DIKERJAKAN' when status = 2 then 'SELESAI' "
        + "when a.status = 3 then 'TIDAK DAPAT DIKERJAKAN' end as Status, a.Catatan_Teknisi from t_penugasan_inventaris a "
        + "left join m_inventaris b on a.id_inventaris = b.id where a.id_penugasan = " + id + "";

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
        // System.Diagnostics.Debug.WriteLine("DADUT");

      //  HiddenStatusUbah.Value = "Ubah";
        if (e.CommandName == "Detail")
        {
            System.Diagnostics.Debug.WriteLine("DADUT");

            string[] cmdArg = e.CommandArgument.ToString().Split(';');

            string id = cmdArg[0];
            string jenis = cmdArg[1];


             if (jenis.Equals("PEMBELIAN"))
            {
                System.Diagnostics.Debug.WriteLine("pembelianid : " + id + ", jenis " + jenis);
           
               // Session["no_pembelian"] = id;
               // Response.Redirect("PembelianDetail.aspx");
                string myURL = "../Laporan/TugasPembelian.aspx?IdPembelian=" + id;
                //  Response.Redirect("~/Laporan/ReportPengajuan.aspx?IdPengajuan="+idPengajuan);
                
              //  Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow2", "window.open('" + myURL + "','_newtab');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenWindow2", "window.open('" + myURL + "','_newtab');", true);

            //    loadEditPembelian(id);
            }
            else if (jenis.Equals("SERVICE"))
            {
              //  Session["id_service"] = id;
              //  Response.Redirect("ServiceDetail.aspx");
                DDLStatusService.Enabled = true;
               
                string myURL = "../Laporan/TugasService.aspx?IdServiceKeluar=" + id;
                //  Response.Redirect("~/Laporan/ReportPengajuan.aspx?IdPengajuan="+idPengajuan);
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow3", "window.open('" + myURL + "','_newtab');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenWindow2", "window.open('" + myURL + "','_newtab');", true);

               

           //     loadEditService(id);
            }
            else
            {
                // System.Diagnostics.Debug.WriteLine("DADUT");
                DDLStatusService.Enabled = false;
                loadEditInventaris(id);
            }


        }
        else if (e.CommandName == "UbahStatus")
        {
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
                DDLStatInventaris.Enabled = true;
                loadEditInventaris(id);
            }
        }
    }


    private void loadEditPembelian(string id)
    {
        HIddenIdTugas.Value = "0";
        
        SqlCommand cmd = new SqlCommand();
        SqlConnection conn = Common.getConnection();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "select * from [t_pengajuan_ref_type] where no_ref = '" + id+"'";

        cmd.Connection = conn;
        conn.Open();
        string idInv = "0";
        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            idInv = reader["id_ref"].ToString();
            DDLPembelian.Text = reader["no_ref"].ToString();
            TbCatatanBeli.Text = reader["summary"].ToString();
            DDLStatusBeli.SelectedValue = reader["status_pekerjaan"].ToString();
            HiddenIdItem.Value = reader["id"].ToString();
        }

        conn.Close();

        ScriptManager.RegisterStartupScript(this, this.GetType(), "LoadModal", "showModalBeli();", true);

    }

    private void loadEditService(string id)
    {
        HIddenIdTugas.Value = "0";
        
        SqlCommand cmd = new SqlCommand();
        SqlConnection conn = Common.getConnection();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "select * from [t_pengajuan_ref_type] where no_ref = '" + id+"'";

        cmd.Connection = conn;
        conn.Open();

        string idInv = "0";
        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            idInv = reader["id_ref"].ToString();
            DDLService.Text = reader["no_ref"].ToString();
            TbCatatanService.Text = reader["summary"].ToString();
            DDLStatusService.SelectedValue = reader["status_pekerjaan"].ToString();
            HiddenIdItem.Value = reader["id"].ToString();
        }

        conn.Close();

        ScriptManager.RegisterStartupScript(this, this.GetType(), "LoadModal", "showModalService();", true);

    }

    private void loadEditInventaris(string id)
    {
        HIddenIdTugas.Value = "0";
       
        SqlCommand cmd = new SqlCommand();
        SqlConnection conn = Common.getConnection();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "select * from [t_pengajuan_ref_type] where no_ref = '" + id+"'";

        cmd.Connection = conn;
        conn.Open();

        string idInv = "0";

        System.Diagnostics.Debug.WriteLine("Edit Inventaris");
        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            idInv = reader["id_ref"].ToString();
         //   DDLJenisTugasInv.SelectedValue = reader["jenis"].ToString();
            TbCatatan.Text = reader["summary"].ToString();
            DDLInventaris.Text = reader["judul_ref"].ToString();
            DDLStatInventaris.SelectedValue = reader["status_pekerjaan"].ToString();
            HiddenIdItem.Value = reader["id"].ToString();
        }

        conn.Close();

        ScriptManager.RegisterStartupScript(this, this.GetType(), "LoadModal", "showModalTugasLain();", true);

    }

  
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        GvBindPenugasanLain();
    }



    protected void SimpanBeli_Click(object sender, EventArgs e)
    {
        // String idTugas = HIddenIdTugas.Value;

        string query = "";
        SqlConnection conn = Common.getConnection();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = query;

        cmd.Parameters.AddWithValue("@Id", HiddenIdItem.Value);
        cmd.Parameters.AddWithValue("@status", DDLStatusBeli.SelectedValue);

        query = "update t_pengajuan_ref_type set status_pekerjaan=@status,updated_date=getdate() " +
                " where id =@Id ";

        conn.Open();
        cmd.Connection = conn;
        cmd.CommandText = query;
        cmd.ExecuteNonQuery();

        conn.Close();

       try
        {
            UpdatePembelian(DDLPembelian.Text);
        }
        catch
        {

        }
        
        GvBindPenugasanInv();
        //     HIddenIdTugas.Value = "0";

        ScriptManager.RegisterStartupScript(this, this.GetType(), "HideLain", "alert('Sukses');hideModalBeli();", true);

    }

    private void UpdatePembelian(string id)
    {
        string query = "";
        SqlConnection conn = Common.getConnection();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = query;

        cmd.Parameters.AddWithValue("@Id", id);
        cmd.Parameters.AddWithValue("@status", DDLStatusBeli.SelectedValue);

        query = "update t_pembelian set status=@status " +
                " where id_pembelian=@Id";



        conn.Open();
        cmd.Connection = conn;
        cmd.CommandText = query;
        cmd.ExecuteNonQuery();

        conn.Close();

    }


    private void UpdateService(string id)
    {
        string query = "";
        SqlConnection conn = Common.getConnection();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = query;

        cmd.Parameters.AddWithValue("@Id", id);
        cmd.Parameters.AddWithValue("@status", DDLStatusService.SelectedValue);

        query = "update t_service set status=@status " +
                " where id_service=@Id";



        conn.Open();
        cmd.Connection = conn;
        cmd.CommandText = query;
        cmd.ExecuteNonQuery();

        conn.Close();

    }
    
    protected void SimpanService_Click(object sender, EventArgs e)
    {
        // String idTugas = HIddenIdTugas.Value;

        string query = "";
        SqlConnection conn = Common.getConnection();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = query;

        cmd.Parameters.AddWithValue("@Id", HiddenIdItem.Value);
        cmd.Parameters.AddWithValue("@status", DDLStatusService.SelectedValue);

        query = "update t_pengajuan_ref_type set status_pekerjaan=@status, updated_date=getdate() " +
                " where id=@Id";



        conn.Open();
        cmd.Connection = conn;
        cmd.CommandText = query;
        cmd.ExecuteNonQuery();

        conn.Close();

        try
        {
            UpdateService(DDLService.Text);
        }
        catch
        {

        }
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

        cmd.Parameters.AddWithValue("@Id", HiddenIdItem.Value);
        cmd.Parameters.AddWithValue("@status", DDLStatInventaris.SelectedValue);

        query = "update t_pengajuan_ref_type set status_pekerjaan=@status, updated_date=getdate() " +
                " where id=@Id";


        conn.Open();
        cmd.Connection = conn;
        cmd.CommandText = query;
        cmd.ExecuteNonQuery();

        conn.Close();

        GvBindPenugasanInv();
        //  HIddenIdTugas.Value = "0";

        ScriptManager.RegisterStartupScript(this, this.GetType(), "HideLain", "alert('Sukses');hideModalTugasLain();", true);

    }

  


    protected void BtnSelesai_Click(object sender, EventArgs e)
    {
        string query = "";
        string userId = Request.Cookies["UserSettings"]["id_user"];


        SqlConnection conn = Common.getConnection();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.Connection = conn;
        query = "update t_penugasan set status = 4, last_updated_date = getdate(), updater = " + userId + " where id = " + HiddenIDPenugasan.Value + "";

        cmd.CommandText = query;

        conn.Open();
        cmd.ExecuteNonQuery();


        ScriptManager.RegisterStartupScript(this, this.GetType(), "Penugasan", "Alert('Penugasan Selesai Sukses');", true);

        conn.Close();
        Response.Redirect("ListClosedTeknisi.aspx");

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