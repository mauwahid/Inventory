﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.IO;

public partial class Penerimaan_Detail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GvBind();
            BindPengajuan();
        }

    }



    protected void BindPengajuan()
    {
        SqlConnection conn = Common.getConnection();
        string query = "";
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;
        string idPengajuan = (string)Session["id_penerimaan"];


        query = "select * from v_penerimaan where id = '" + idPengajuan + "'";
        cmd.CommandText = query;

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            HiddenIDPengajuan.Value = reader["id"].ToString();
            LblJudulPengajuan.Text = reader["judul_pengajuan"].ToString().Trim();
            LblKabag.Text = reader["kabag"].ToString();
            LblMemoKabag.Text = reader["memo_kabag"].ToString();
            LblNoPengajuan.Text = reader["no_pengajuan"].ToString();
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
        dataSource.SelectCommand = "select p.id, p.id_pengajuan, p.id_inventaris, p.qty, p.memo_kabag, p.gambar,p.created_date, p.harga_satuan, p.harga_total," +
            " pin.no_pengajuan, inv.nama_inventaris from t_pengajuan_inventaris_detail p inner join m_inventaris inv on p.id_inventaris = inv.id " +
            " inner join t_pengajuan_inventaris pin on p.id_pengajuan = pin.id " +
            " where pin.id = '" + id + "'";

        GridView1.DataSource = dataSource;
        GridView1.DataBind();

    }

    protected void DDLInv_SelectedIndexChanged(object sender, EventArgs e)
    {
        DDLHarga.SelectedIndex = DDLInventaris.SelectedIndex;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "LoadModal", "showModalInventaris();", true);

    }

    protected void TbQty_TextChanged(object sender, EventArgs e)
    {
        int qty = Int32.Parse(TbQty.Text);
        int harga = Int32.Parse(DDLHarga.SelectedValue);
        int total = qty * harga;
        TbHargaTotal.Text = total + "";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "LoadModal", "showModalInventaris();", true);


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
            InitDDLInventaris();

            query = "select id, id_inventaris, memo_kabag, qty, harga_total, harga_satuan from t_pengajuan_inventaris_detail where id = " + id;
            cmd.CommandText = query;

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                HiddenIdInventaris.Value = reader["id"].ToString();
                DDLInventaris.SelectedValue = reader["id_inventaris"].ToString();
                DDLInventaris.Enabled = false;
                TbMemoKabag.Text = reader["memo_kabag"].ToString();
                TbQty.Text = reader["qty"].ToString();
                TbHargaTotal.Text = reader["harga_total"].ToString();

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
        cmd.Parameters.AddWithValue("@harga_satuan", DDLHarga.SelectedValue);
        cmd.Parameters.AddWithValue("@harga_total", TbHargaTotal.Text);

        if (HiddenIdInventaris.Value == "0")
        {
            cmd.Parameters.AddWithValue("@id_pengajuan", HiddenIDPengajuan.Value);
            cmd.Parameters.AddWithValue("@id_inventaris", DDLInventaris.SelectedValue);

            query = "INSERT INTO t_pengajuan_inventaris_detail (id_pengajuan, qty, id_inventaris, memo_kabag, harga_satuan, harga_total) " +
            " VALUES(@id_pengajuan,  @qty,@id_inventaris, @memo_kabag, @harga_satuan, @harga_total)";
        }
        else
        {
            cmd.Parameters.AddWithValue("@ID", HiddenIdInventaris.Value);
            query = "UPDATE t_pengajuan_inventaris_detail  set qty = @qty, memo_kabag = @memo_kabag" +
            ", harga_satuan = @harga_satuan, harga_total = @harga_total where id = @ID";

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

        query = "select id, nama_inventaris, harga from m_inventaris where id not in(select id_inventaris from "
              + " t_pengajuan_inventaris_detail where id_pengajuan = " + HiddenIDPengajuan.Value + ") order by id asc";
        cmd.CommandText = query;

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();

        if (reader.HasRows)
        {
            val = true;
            DDLInventaris.DataSource = reader;
            DDLInventaris.DataValueField = "id";
            DDLInventaris.DataTextField = "nama_inventaris";
            DDLInventaris.DataBind();
            //   DDLHarga.DataBind();
        }


        conn.Close();


        SqlConnection conn2 = Common.getConnection();
        string query2 = "";
        bool val2 = false;
        SqlCommand cmd2 = new SqlCommand();
        cmd2.Connection = conn2;
        cmd2.CommandType = System.Data.CommandType.Text;

        query2 = "select id,  harga from m_inventaris where id not in(select id_inventaris from "
              + " t_pengajuan_inventaris_detail where id_pengajuan = " + HiddenIDPengajuan.Value + ") order by id asc";
        cmd2.CommandText = query2;

        conn2.Open();
        SqlDataReader reader2 = cmd2.ExecuteReader();

        if (reader2.HasRows)
        {
            val = true;

            DDLHarga.DataSource = reader2;
            DDLHarga.DataValueField = "harga";
            DDLHarga.DataTextField = "harga";

            DDLHarga.DataBind();
        }

        conn2.Close();



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

        query = "select id, nama_inventaris from m_inventaris order by id asc";
        cmd.CommandText = query;

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();

        if (reader.HasRows)
        {
            val = true;
            DDLInventaris.DataSource = reader;
            DDLInventaris.DataValueField = "id";
            DDLInventaris.DataTextField = "nama_inventaris";
            DDLInventaris.DataBind();
            //   DDLHarga.DataBind();
        }


        conn.Close();


        SqlConnection conn2 = Common.getConnection();
        string query2 = "";
        SqlCommand cmd2 = new SqlCommand();
        cmd2.Connection = conn2;
        cmd2.CommandType = System.Data.CommandType.Text;

        query2 = "select id, harga from m_inventaris order by id asc";
        cmd2.CommandText = query2;

        conn2.Open();
        SqlDataReader reader2 = cmd2.ExecuteReader();

        if (reader2.HasRows)
        {
            val = true;

            DDLHarga.DataSource = reader2;
            DDLHarga.DataValueField = "harga";
            DDLHarga.DataTextField = "harga";

            DDLHarga.DataBind();
        }

        conn2.Close();



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

        query = "Update t_pengajuan_inventaris set status_approval = 0 where id = @id_pengajuan";


        conn.Open();
        cmd.Connection = conn;
        cmd.CommandText = query;
        cmd.ExecuteNonQuery();

        conn.Close();

        GvBind();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alertSukses();", true);
        Response.Redirect("TelahDiajukan.aspx");
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

        GvBind();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alertSuksesDelete();", true);
        Response.Redirect("Draft.aspx");
    }
    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Draft.aspx");
    }


    protected void Button2_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            String fileSave = "P_" + HiddenIDPengajuan.Value + "_" + FileUpload1.FileName;
            FileUpload1.SaveAs(Server.MapPath("~/Files") + "//" + fileSave);
            LblSurat.Text = FileUpload1.FileName;
            LblSurat.ForeColor = System.Drawing.Color.Black;
            SaveUploadData(fileSave);

        }
        else
        {
            LblSurat.Text = "Mohon Pilih File yang akan diupload";
            LblSurat.ForeColor = System.Drawing.Color.Red;
        }//  lblMessage.Text = "File Successfully Uploaded";
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

        query = "Update t_pengajuan_inventaris set file_save = @fileSave where id = @id_pengajuan";


        conn.Open();
        cmd.Connection = conn;
        cmd.CommandText = query;
        cmd.ExecuteNonQuery();

        conn.Close();

        GvBind();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Upload Success');", true);

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