using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class Admin_Inventaris_List : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GvBind();
        }

    }

    protected void btnTambah_Click(object sender, EventArgs e)
    {
        Session["inventaris_form_type"] = "add";
        Response.Redirect("Form.aspx");
    }


    protected void GvBind()
    {
        SqlDataSource dataSource = new SqlDataSource();
        dataSource.ConnectionString = ConfigurationManager.ConnectionStrings["InventoryConn"].ToString();
        dataSource.SelectCommand = "Select * from v_inventaris";


        GridView1.DataSource = dataSource;
        GridView1.DataBind();



    }

    protected void GvBindOld()
    {
        SqlConnection conn = Common.getConnection();
        string query = "Select * from v_inventaris";
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = query;

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();

        DataTable dt = new DataTable();
        dt.Columns.Add("Id", typeof(string));
        dt.Columns.Add("NamaInventaris", typeof(string));
        dt.Columns.Add("JenisInventaris", typeof(string));
        dt.Columns.Add("Harga", typeof(string));
        dt.Columns.Add("Keterangan", typeof(string));
        dt.Columns.Add("Merk", typeof(string));
        dt.Columns.Add("Distributor", typeof(string));
        dt.Columns.Add("Gambar", typeof(string));
        dt.Columns.Add("Qty", typeof(string));


        DataRow dr = null;
        while (reader.Read())
        {
            dr = dt.NewRow();
            dr["Id"] = reader["id_inventaris"].ToString();
            dr["NamaInventaris"] = reader["nama_inventaris"].ToString();
            dr["JenisInventaris"] = reader["jenis_inventaris"].ToString();
            dr["Harga"] = reader["harga"].ToString();
            dr["Keterangan"] = reader["keterangan"].ToString();
            dr["Merk"] = reader["merk"].ToString();
            dr["Distributor"] = reader["distributor"].ToString();
            dr["Qty"] = reader["qty"].ToString();
            dr["Gambar"] = "";
            dt.Rows.Add(dr);


        }

        GridView1.DataSource = dt;
        GridView1.DataBind();

        conn.Close();

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
            string idInventaris = Convert.ToString(e.CommandArgument);
            Delete(idInventaris);
            GvBind();
        }
        if (e.CommandName == "UbahQty")
        {
            string idInventaris = Convert.ToString(e.CommandArgument);
         //   HiddenEditStatus.Value = "Ubah";
          //  HiddenItemID.Value = idInventaris;
            TbJumlah.Text = "0";
            System.Diagnostics.Debug.WriteLine("ID INV : " + idInventaris);
            EditInventaris(idInventaris);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "LoadModal", "showModalEditInv();", true);


        }
    }

    protected void EditInventaris(string id)
    {
        SqlConnection conn = Common.getConnection();
        string query = "";
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.Parameters.AddWithValue("@IdInv", id);
        //  cmd.Parameters.AddWithValue("@IdRuangan", HiddenIdInvRuangan.Value);
        string param = TbCari.Text;
        //string id = HiddenIdInvRuangan.Value;
        query = "select nama_inventaris from m_inventaris where id_inventaris=@IdInv";
        cmd.CommandText = query;

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            HiddenItemID.Value = id;
            TbNama.Text = reader["nama_inventaris"].ToString();
          //  DDLStatusItem.SelectedValue = reader["status"].ToString();
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
        string param = TbCari.Text;

        query = "delete m_inventaris where id_inventaris = " + id;
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

        string id = GridView1.DataKeys[e.NewEditIndex]["id_inventaris"].ToString();
        Session["inventaris_form_type"] = "edit";
        Session["inventaris_form_id"] = id;
        Response.Redirect("Form.aspx");


    }

    protected void RowDelete_Click(object sender, GridViewDeleteEventArgs e)
    {

        string id = GridView1.DataKeys[e.RowIndex]["Id"].ToString();
        Delete(id);
        GvBind();


    }
    protected void btnCari_Click(object sender, EventArgs e)
    {
        SqlConnection conn = Common.getConnection();
        string query = "";
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;
        string param = TbCari.Text;

        query = "select * from v_inventaris where nama_inventaris like '%" + param + "%' ";
        cmd.CommandText = query;

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();

        DataTable dt = new DataTable();
        dt.Columns.Add("Id", typeof(string));
        dt.Columns.Add("NamaInventaris", typeof(string));
        dt.Columns.Add("JenisInventaris", typeof(string));
        dt.Columns.Add("Harga", typeof(string));
        dt.Columns.Add("Keterangan", typeof(string));
        dt.Columns.Add("Merk", typeof(string));
        dt.Columns.Add("Distributor", typeof(string));
        dt.Columns.Add("Gambar", typeof(string));
        dt.Columns.Add("Qty", typeof(string));


        DataRow dr = null;
        while (reader.Read())
        {
            dr = dt.NewRow();
            dr["Id"] = reader["id_inventaris"].ToString();
            dr["NamaInventaris"] = reader["nama_inventaris"].ToString();
            dr["JenisInventaris"] = reader["jenis_inventaris"].ToString();
            dr["Harga"] = reader["harga"].ToString();
            dr["Keterangan"] = reader["keterangan"].ToString();
            dr["Merk"] = reader["merk"].ToString();
            dr["Distributor"] = reader["distributor"].ToString();
            dr["Qty"] = reader["qty"].ToString();
            dr["Gambar"] = "";
            dt.Rows.Add(dr);


        }

        GridView1.DataSource = dt;
        GridView1.DataBind();

        conn.Close();

    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        GvBind();
    }

    protected void Tambah_Click(object sender, EventArgs e)
    {
        int hasil = Tambah();
        if (hasil > 0)
        {
            GvBind();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Al", "alert('SUKSES');hideModal();", true);
          
        }
        else
        {
            
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Al", "alert('GAGAL INSERT');hideModal();", true);

        }
    }

    protected void Kurangi_Click(object sender, EventArgs e)
    {
        int hasil = Kurangi();
        if (hasil > 0)
        {
            GvBind();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Al", "alert('SUKSES');hideModal();", true);

        }
        else if(hasil == 0)
        {

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Al", "alert('GAGAL INSERT');hideModal();", true);

        }
        else if (hasil < 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Al", "alert('Jumlah tidak boleh minus (Jumlah dikurangi lebih besar dari Jml belum terpakai)');hideModal();", true);

        }
    }

    protected int Tambah()
    {
         int qtySebelum = 0;
         int qtySesudah = 0;
      //  String keterangan = "";

        SqlConnection conn = Common.getConnection();
        string query = "";
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.Parameters.AddWithValue("@IdInv", HiddenItemID.Value);
        //  cmd.Parameters.AddWithValue("@IdRuangan", HiddenIdInvRuangan.Value);
        string param = TbCari.Text;
        //string id = HiddenIdInvRuangan.Value;
        query = "select qty from m_inventaris where id_inventaris=@IdInv";
        cmd.CommandText = query;

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();

        
        if (reader.Read())
        {
          //  HiddenItemID.Value = id;
            try
            {

                qtySebelum = (int)reader["qty"];
            }
            catch (Exception ex)
            {
            }
            //  DDLStatusItem.SelectedValue = reader["status"].ToString();
        }

        conn.Close();

        qtySesudah = Int32.Parse(TbJumlah.Text) + qtySebelum;


        int result = 0;
        conn = Common.getConnection();
        SqlCommand cmd2 = new SqlCommand();
        cmd2.Connection = conn;
        cmd2.CommandType = System.Data.CommandType.Text;
        cmd2.Parameters.AddWithValue("@IdInv", HiddenItemID.Value);
        cmd2.Parameters.AddWithValue("@qty", qtySesudah);
        //  cmd.Parameters.AddWithValue("@IdRuangan", HiddenIdInvRuangan.Value);
        //string id = HiddenIdInvRuangan.Value;
        query = "update m_inventaris set qty = @qty where id_inventaris=@IdInv";
        cmd2.CommandText = query;

        conn.Open();
        result = cmd2.ExecuteNonQuery();


        conn.Close();

        return UpdateInventaris(HiddenItemID.Value);


    }


    protected int Kurangi()
    {
        int qtySebelum = 0;
        int qtySesudah = 0;
        int result = 0;
        
        //  String keterangan = "";

        SqlConnection conn = Common.getConnection();
        string query = "";
      SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.Parameters.AddWithValue("@IdInv", HiddenItemID.Value);
        //  cmd.Parameters.AddWithValue("@IdRuangan", HiddenIdInvRuangan.Value);
        string param = TbCari.Text;
        //string id = HiddenIdInvRuangan.Value;
        query = "select qty_belum_terpakai from m_inventaris where id_inventaris=@IdInv";
        cmd.CommandText = query;

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();


        if (reader.Read())
        {
            //  HiddenItemID.Value = id;
            try
            {

                qtySebelum = (int)reader["qty_belum_terpakai"];
            }
            catch (Exception ex)
            {
            }
            //  DDLStatusItem.SelectedValue = reader["status"].ToString();
        }

        conn.Close();

        int qtyKurangi = Int32.Parse(TbJumlah.Text);

        if (qtyKurangi > qtySebelum)
        {
            return -1;
        }
        else
        {

            qtySesudah = qtyKurangi - qtySebelum;
            conn = Common.getConnection();
            SqlCommand cmd2 = new SqlCommand();
            cmd2.Connection = conn;
            cmd2.CommandType = System.Data.CommandType.Text;
            cmd2.Parameters.AddWithValue("@IdInv", HiddenItemID.Value);
            cmd2.Parameters.AddWithValue("@qty", qtySesudah);
            //  cmd.Parameters.AddWithValue("@IdRuangan", HiddenIdInvRuangan.Value);
            //string id = HiddenIdInvRuangan.Value;
            query = "update m_inventaris set qty = @qty where id_inventaris=@IdInv";
            cmd2.CommandText = query;

            conn.Open();
            result = cmd2.ExecuteNonQuery();


            conn.Close();

            return UpdateInventaris(HiddenItemID.Value);
        }


    }

    protected int UpdateInventaris(string idInv)
    {
        int qtySebelumRuangan = 0;
        int qtySesudahBukanRuangan = 0;
        int qtyTotal = 0;
        int result = 0;

        //  String keterangan = "";

        SqlConnection conn = Common.getConnection();
        string query = "";
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.Parameters.AddWithValue("@IdInv", idInv);
        //  cmd.Parameters.AddWithValue("@IdRuangan", HiddenIdInvRuangan.Value);
        string param = TbCari.Text;
        //string id = HiddenIdInvRuangan.Value;
        query = "select sum(qty_total) as jml_inv from t_inventaris_ruangan "+
                "where id_inventaris = @IdInv"+
                "  group by id_inventaris";
        cmd.CommandText = query;

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();


        if (reader.Read())
        {
            //  HiddenItemID.Value = id;
            try
            {

                qtySebelumRuangan = (int)reader["jml_inv"];
            }
            catch (Exception ex)
            {
            }
            //  DDLStatusItem.SelectedValue = reader["status"].ToString();
        }

        conn.Close();

        conn = Common.getConnection();
        SqlCommand cmd2 = new SqlCommand();
        cmd2.Connection = conn;
        cmd2.CommandType = System.Data.CommandType.Text;
        cmd2.Parameters.AddWithValue("@IdInv",idInv);
        //  cmd.Parameters.AddWithValue("@IdRuangan", HiddenIdInvRuangan.Value);
        //string id = HiddenIdInvRuangan.Value;
        query = "select qty from m_inventaris where id_inventaris=@IdInv";
        cmd2.CommandText = query;

        conn.Open();
        SqlDataReader reader2= cmd2.ExecuteReader();

        while (reader2.Read())
        {
            qtyTotal = (int)reader2["qty"];
        }


        conn.Close();

        qtySesudahBukanRuangan = qtyTotal - qtySebelumRuangan;

        conn = Common.getConnection();
        query = "";
        SqlCommand cmd3 = new SqlCommand();
        cmd3.Connection = conn;
        cmd3.CommandType = System.Data.CommandType.Text;
        cmd3.Parameters.AddWithValue("@IdInv", HiddenItemID.Value);
        cmd3.Parameters.AddWithValue("@qty", qtySesudahBukanRuangan);
        cmd3.Parameters.AddWithValue("@qty_dalam_ruangan", qtySebelumRuangan);
        //  cmd.Parameters.AddWithValue("@IdRuangan", HiddenIdInvRuangan.Value);
        //string id = HiddenIdInvRuangan.Value;
        query = "update m_inventaris set qty_belum_Terpakai = @qty, qty_dalam_ruangan = @qty_dalam_ruangan where id_inventaris=@IdInv";
        cmd3.CommandText = query;

        conn.Open();
        result = cmd3.ExecuteNonQuery();


        conn.Close();

        return result;

    }
}