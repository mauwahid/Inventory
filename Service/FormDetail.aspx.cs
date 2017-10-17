using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.IO;

public partial class Pengadaan_FormDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GvBind();
            BindPengajuan();
       //    InitDDLInventaris();
        }

    }

   

    protected void BindPengajuan()
    {
        SqlConnection conn = Common.getConnection();
        string query = "";
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;
        string idPengajuan = (string)Session["id_pengajuan"];
        HiddenIDPengajuan.Value = idPengajuan;


        query = "select * from v_service where id_pengajuan_service = '" + idPengajuan + "'";
        cmd.CommandText = query;

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            HiddenIDPengajuan.Value = reader["id_pengajuan_service"].ToString();
            LblJudulPengajuan.Text = reader["judul_pengajuan_service"].ToString().Trim();
            LblKabag.Text = reader["kabag"].ToString();
            LblMemoKabag.Text = reader["catatan_kabag"].ToString();
            LblNoPengajuan.Text = reader["no_pengajuan_service"].ToString();
            LblTglPengajuan.Text = reader["created_date"].ToString();

        }
       
        conn.Close();

       

    }

    protected void GvBind()
    {
        string id = Session["id_pengajuan"].ToString();
       // string id = (string)Session["no_pengajuan"];
        SqlDataSource dataSource = new SqlDataSource();
        dataSource.ConnectionString = ConfigurationManager.ConnectionStrings["InventoryConn"].ToString();
        dataSource.SelectCommand = "select p.id_pengajuan_service_detail, p.id_pengajuan_service, p.id_inventaris, p.qty, p.catatan_kabag, p.created_date, p.biaya_service,"+
           " pin.no_pengajuan_service, inv.nama_inventaris from t_pengajuan_service_detail p inner join m_inventaris inv on p.id_inventaris = inv.id_inventaris"+
          " inner join t_pengajuan_service pin on p.id_pengajuan_service = pin.id_pengajuan_service " +
            " where pin.id_pengajuan_service = '" + id + "'";

        GridView1.DataSource = dataSource;
        GridView1.DataBind();

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
            InitDDLInventaris();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = System.Data.CommandType.Text;
            string id = Convert.ToString(e.CommandArgument);
            
            query = "select id_pengajuan_service_detail, id_inventaris, catatan_kabag, qty, biaya_service from t_pengajuan_service_detail where id_pengajuan_service_detail = " + id;
            
            cmd.CommandText = query;

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                HiddenIdInventaris.Value = reader["id_pengajuan_service_detail"].ToString();
                DDLInventaris.SelectedValue = reader["id_inventaris"].ToString();
                DDLInventaris.Enabled = false;
                TbMemoKabag.Text = reader["catatan_kabag"].ToString();
                TbQty.Text = reader["qty"].ToString();
                TbHargaTotal.Text = reader["biaya_service"].ToString();

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
       
        query = "delete t_pengajuan_service_detail where id_pengajuan_service_detail = " + id + "";
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
        string query = "";
        SqlConnection conn = Common.getConnection();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = query;

        cmd.Parameters.AddWithValue("@qty", TbQty.Text);
        cmd.Parameters.AddWithValue("@memo_kabag", TbMemoKabag.Text);
        cmd.Parameters.AddWithValue("@harga_total", TbHargaTotal.Text);

        if (HiddenIdInventaris.Value == "0")
        {
            cmd.Parameters.AddWithValue("@id_pengajuan", HiddenIDPengajuan.Value);
            cmd.Parameters.AddWithValue("@id_inventaris", DDLInventaris.SelectedValue);
            DateTime myDateTime = DateTime.Now;
            cmd.Parameters.AddWithValue("@createddate", myDateTime.ToString("yyyy-MM-dd HH:mm:ss"));

            query = "INSERT INTO t_pengajuan_service_detail (id_pengajuan_service, qty, id_inventaris, catatan_kabag, biaya_service,created_date) " +
            " VALUES(@id_pengajuan,  @qty,@id_inventaris, @memo_kabag, @harga_total, @createddate)";
        }
        else
        {
            cmd.Parameters.AddWithValue("@ID", HiddenIdInventaris.Value);
            query = "UPDATE t_pengajuan_service_detail  set qty = @qty, catatan_kabag = @memo_kabag" +
            ", biaya_service = @harga_total where id_pengajuan_service_detail = @ID";
       
        }

        

        conn.Open();
        cmd.Connection = conn;
        cmd.CommandText = query;
        cmd.ExecuteNonQuery();

        conn.Close();

        GvBind();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeAddInvModal();", true);

    }



    protected bool InitInsertDDLInventaris()
    {
        SqlConnection conn = Common.getConnection();
        string query = "";
        bool val = false;
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;

        
        query = "select id_inventaris, nama_inventaris, harga from m_inventaris where id_inventaris not in(select id_inventaris from "
              +" t_pengajuan_service_detail where id_pengajuan_service = '"+HiddenIDPengajuan.Value+"') order by nama_inventaris asc";
        System.Diagnostics.Debug.WriteLine("Query " + query);

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


    protected bool InitDDLInventaris()
    {
        SqlConnection conn = Common.getConnection();
        string query = "";
        bool val = false;
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;


        query = "select id_inventaris, nama_inventaris from m_inventaris";
        System.Diagnostics.Debug.WriteLine("Query " + query);

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

    protected void SimpanPengajuan_Click(object sender, EventArgs e)
    {
        string query = "";
        SqlConnection conn = Common.getConnection();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = query;

        cmd.Parameters.AddWithValue("@id_pengajuan", HiddenIDPengajuan.Value);
       
        query = "Update t_pengajuan_service set status_konfirmasi = 0 where id_pengajuan_service = @id_pengajuan";


        conn.Open();
        cmd.Connection = conn;
        cmd.CommandText = query;
        cmd.ExecuteNonQuery();

        conn.Close();

        GvBind();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alertSukses();", true);
        Response.Redirect("Draft.aspx");
    }

    protected void BtnTambah_Click(object sender, EventArgs e)
    {
        bool val = InitInsertDDLInventaris();
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
    protected void BtnDeletePengajuan_Click(object sender, EventArgs e)
    {
        string query = "";
        SqlConnection conn = Common.getConnection();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = query;

        cmd.Parameters.AddWithValue("@id_pengajuan", HiddenIDPengajuan.Value);

        query = "delete t_pengajuan_service where id_pengajuan_service = @id_pengajuan";


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

        query = "delete  t_pengajuan_service_detail where id_pengajuan_service = @id_pengajuan";


        conn.Open();
        cmd.Connection = conn;
        cmd.CommandText = query;
        cmd.ExecuteNonQuery();

        conn.Close();

        GvBind();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alertSuksesDelete();", true);
        Response.Redirect("Draft.aspx");
    }
    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Draft.aspx");
    }

    private void SaveUploadData(string data)
    {
        string query = "";
        SqlConnection conn = Common.getConnection();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = query;

        cmd.Parameters.AddWithValue("@id_pengajuan", HiddenIDPengajuan.Value);
        cmd.Parameters.AddWithValue("@fileSave", data);

        query = "Update t_service_inventaris set file_save = @fileSave where id = @id_pengajuan";


        conn.Open();
        cmd.Connection = conn;
        cmd.CommandText = query;
        cmd.ExecuteNonQuery();

        conn.Close();

        GvBind();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Upload Success');", true);

    }
 

    protected void ButtonUpd_Click(object sender, EventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("MASUK DUT");
        if (FileUploader.HasFile)
        {
            String fileSave = "P_" + HiddenIDPengajuan.Value + "_" + FileUploader.FileName;
            FileUploader.SaveAs(Server.MapPath("~/Files") + "//" + fileSave);
            LblSurat.Text = FileUploader.FileName;
            LblSurat.ForeColor = System.Drawing.Color.Black;
            System.Diagnostics.Debug.WriteLine("MASUK DUT "+fileSave);
       
            SaveUploadData(fileSave);

        }
        else
        {
            LblSurat.Text = "Mohon Pilih File yang akan diupload";
            LblSurat.ForeColor = System.Drawing.Color.Red;
        }//  lblMessage.Text = "File Successfully Uploaded";
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.ContentType = "Application/msword";
        Response.AppendHeader("Content-Disposition", "attachment; filename=LaporanPengajuan.docx");
        //  Response.TransmitFile(Server.MapPath("~/Files/Template/LaporanPengajuanTemplate.docx"));
        // Response.End();

        // Write the file to the Response
        const int bufferLength = 10000;
        byte[] buffer = new Byte[bufferLength];
        int length = 0;
        Stream download = null;
        try
        {
            download = new FileStream(Server.MapPath("~/Files/Template/LaporanPengajuanTemplate.docx"),
                                                           FileMode.Open,
                                                           FileAccess.Read);
            do
            {
                if (Response.IsClientConnected)
                {
                    length = download.Read(buffer, 0, bufferLength);
                    Response.OutputStream.Write(buffer, 0, length);
                    buffer = new Byte[bufferLength];
                }
                else
                {
                    length = -1;
                }
            }
            while (length > 0);
            Response.Flush();
            Response.End();
        }
        finally
        {
            if (download != null)
                download.Close();
        }

    }

  
}