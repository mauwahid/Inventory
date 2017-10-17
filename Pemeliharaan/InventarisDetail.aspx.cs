using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class Pemeliharaan_InventarisDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string idInvetnaris = Session["id_inventaris"].ToString();
            string idInvRuangan = Session["id_inv_ruangan"].ToString();

            System.Diagnostics.Debug.WriteLine("ID INV RUANGAN "+idInvRuangan);
            // string id_ruangan = "1";
            string idRuangan = Session["id_ruangan"].ToString();
            HiddenIdInventaris.Value = idInvetnaris;
            HiddenIdInvRuangan.Value = idInvRuangan;
            HiddenRuangan.Value = idRuangan;
            InitNamaInv();
            GvBind();
      //      Response.Write("ID : " + idRuangan);
        }

    }

    protected void InitNamaInv()
    {
        SqlConnection conn = Common.getConnection();
        string query = "";
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.Parameters.AddWithValue("@IdInvDetail", HiddenIdInventaris.Value);
        string param = TbCari.Text;
     //   query = "select i.nama_inventaris from t_inventaris_ruangan t "+
       //         "    inner join m_inventaris i "+
         //       "   on i.id = t.id_inventaris where t.id = @IdInvDetail";
        query = "select nama_inventaris from m_inventaris where id_inventaris = @IdInvDetail";
        cmd.CommandText = query;

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            LblNamaInv.Text = reader["nama_inventaris"].ToString();
        }

        conn.Close();

    }


    protected void btnTambah_Click(object sender, EventArgs e)
    {
        HiddenEditStatus.Value = "Tambah";
        LblForm.Text = "Tambah";
        TbNoItem.Text = "";
        int qty = CheckInv(HiddenIdInventaris.Value);
        if(qty>0)
            ScriptManager.RegisterStartupScript(this, this.GetType(), "LoadModal", "showModalEditInv();", true);
        else
            ScriptManager.RegisterStartupScript(this, this.GetType(), "LoadModal", "alert('Item tidak bisa ditambahkan karena barang tidak tersedia (habis)');", true);
       
    }


    protected void btnCatat_Click(object sender, EventArgs e)
    {
        Session["id_ruangan"] = HiddenIdInvRuangan.Value;
        Response.Redirect("TambahCatatan.aspx");
    }


    protected void GvBind()
    {
        SqlDataSource dataSource = new SqlDataSource();
        dataSource.ConnectionString = ConfigurationManager.ConnectionStrings["InventoryConn"].ToString();
        string query = "select id,  no_inventaris, case  when status = 1 then 'Terpakai' when status = 2 then 'Rusak' "+
        " when status = 3 then 'Hilang' end as status_available from t_pencatatan_inv_detail_list where id_inv_ruangan = " + HiddenIdInvRuangan.Value;
        dataSource.SelectCommand = query;
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
       
        if (e.CommandName == "Ubah")
        {
            string idInventaris = Convert.ToString(e.CommandArgument);
            HiddenEditStatus.Value = "Ubah";
            HiddenItemID.Value = idInventaris;
            EditInventaris(idInventaris);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "LoadModal", "showModalEditInv();", true);


        }
        if (e.CommandName == "Hapus")
        {
            string idInventaris = Convert.ToString(e.CommandArgument);
            HapusInv(idInventaris);
        
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
        query = "select item.id, item.no_inventaris, item.status from t_pencatatan_inv_detail_list item where item.id=@IdInv";
         cmd.CommandText = query;

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            HiddenItemID.Value = id;
            TbNoItem.Text = reader["no_inventaris"].ToString();
            DDLStatusItem.SelectedValue = reader["status"].ToString();
         }

        conn.Close();

    }

    protected void HapusInv(string id)
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
        query = "delete t_pencatatan_inv_detail_list where id=@IdInv";
        cmd.CommandText = query;

        conn.Open();
        int result = cmd.ExecuteNonQuery();

        if (result > 0)
        {
            UpdateInvRuangan(HiddenIdInvRuangan.Value);
        
            GvBind();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertSuc", "alert('Data berhasil dihapus');", true);

        }
        else
        {
            UpdateInvRuangan(HiddenIdInvRuangan.Value);
        
            GvBind();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertFail", "alert('Data gagal dihapus');", true);

        }

        conn.Close();

    }

   

  
    protected void GView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GvBind();
    }


    protected void Save_Click(object sender, EventArgs e)
    {
        SqlConnection conn = Common.getConnection();
        string query = "";
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.Parameters.AddWithValue("@IdInv", HiddenIdInvRuangan.Value);
        cmd.Parameters.AddWithValue("@noInventaris", TbNoItem.Text);
        cmd.Parameters.AddWithValue("@status", DDLStatusItem.SelectedValue);
        bool result = false;

        if (HiddenEditStatus.Value == "Tambah")
        {
            query = "INSERT t_pencatatan_inv_detail_list(no_inventaris,status,id_inv_ruangan) OUTPUT Inserted.id " +
                " values(@noInventaris,@status,@IdInv)";
            conn.Open();
            cmd.CommandText = query;
            try
            {
                string id = cmd.ExecuteScalar().ToString();
                HiddenItemID.Value = id;

                result = true;
            }
            catch (Exception ex)
            {

            }
           

        }
        else if (HiddenEditStatus.Value == "Ubah")
        {
            cmd.Parameters.AddWithValue("@idItem", HiddenItemID.Value);
            cmd.Parameters.AddWithValue("@id", HiddenItemID.Value);

            query = "UPDATE t_pencatatan_inv_detail_list set no_inventaris = @noInventaris,status = @status " +
            " where id = @id";

            conn.Open();
            cmd.CommandText = query;

            try
            {
                cmd.ExecuteNonQuery();
                result = true;
            }
            catch
            {
            }
        }
        conn.Close();
        System.Diagnostics.Debug.WriteLine("HIDDEN IV RUANGAN : " + HiddenIdInvRuangan.Value);
        GvBind();

        if (result)
        {
            UpdateInvRuangan(HiddenIdInvRuangan.Value);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "HideLain", "alert('Sukses');hideModal();", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "HideLain", "alert('Data gagal disimpan dikarenakan no inventaris sudah tersedia');hideModal();", true);
        }
        

    }


    protected void btnCari_Click(object sender, EventArgs e)
    {
        SqlConnection conn = Common.getConnection();
        string query = "";
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;
        string param = TbCari.Text;
        string id = HiddenIdInvRuangan.Value;
        query = "select * from v_inventaris_ruangan where nama_inventaris like '%" + param + "%' or keterangan like '%" + param + "%'  and id_ruangan = " + id;
        cmd.CommandText = query;

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();

        DataTable dt = new DataTable();
        dt.Columns.Add("Id", typeof(string));
        dt.Columns.Add("NamaInventaris", typeof(string));
        dt.Columns.Add("JenisInventaris", typeof(string));
        dt.Columns.Add("QtyTersedia", typeof(string));
        dt.Columns.Add("QtyHilang", typeof(string));
        dt.Columns.Add("QtyRusak", typeof(string));
        dt.Columns.Add("Keterangan", typeof(string));


        DataRow dr = null;
        while (reader.Read())
        {
            dr = dt.NewRow();
            dr["Id"] = reader["id"].ToString();
            dr["NamaInventaris"] = reader["nama_inventaris"].ToString();
            dr["JenisInventaris"] = reader["jenis_inventaris"].ToString();
            dr["QtyTersedia"] = reader["qty_tersedia"].ToString();
            dr["QtyHilang"] = reader["qty_hilang"].ToString();
            dr["QtyRusak"] = reader["qty_rusak"].ToString();
            dr["Keterangan"] = reader["keterangan"].ToString();
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

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Session["id_ruangan"] = HiddenRuangan.Value;
        Response.Redirect("ListInventaris.aspx");
    }

    private void UpdateInvRuangan(string idInvRuangan)
    {
        SqlConnection conn = Common.getConnection();
       // string query = "";
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.Parameters.AddWithValue("@IdInv", HiddenIdInvRuangan.Value);
        
        string query = "select sum(terpakai) as terpakai, sum(rusak) as rusak, sum(hilang) as hilang from"+
                "(select id_inv_ruangan, case when status = 1 then 1 end as terpakai,  case when status = 2 then 1 end as rusak,"+
                "case when status = 3 then 1 end as hilang from t_pencatatan_inv_detail_list) as t "+
                "where t.id_inv_ruangan = @IdInv";

        int terpakai = 0;
        int rusak = 0;
        int hilang = 0;
        int total = 0;

        conn.Open();
        cmd.CommandText = query;
        SqlDataReader reader =  cmd.ExecuteReader();
        while (reader.Read())
        {
            terpakai = reader["terpakai"]==DBNull.Value?0:(Int32)reader["terpakai"];
            hilang = reader["hilang"]==DBNull.Value?0:(Int32)reader["hilang"];
            rusak = reader["rusak"] == DBNull.Value? 0 : (Int32)reader["rusak"];
            total = terpakai + hilang + rusak;
        }

        conn.Close();

        SqlConnection conn2 = Common.getConnection();
        // string query = "";
        SqlCommand cmd2 = new SqlCommand();
        cmd2.Connection = conn2;
        cmd2.CommandType = System.Data.CommandType.Text;
        cmd2.Parameters.AddWithValue("@IdInv2", HiddenIdInvRuangan.Value);
        cmd2.Parameters.AddWithValue("@Total", total);
        cmd2.Parameters.AddWithValue("@Hilang", hilang);
        cmd2.Parameters.AddWithValue("@Rusak", rusak);
        cmd2.Parameters.AddWithValue("@Terpakai", terpakai);

        string query2 = "update t_inventaris_ruangan set qty_total = @Total, qty_terpakai=@Terpakai, qty_hilang = @Hilang,qty_rusak=@Rusak where id_inventaris_ruangan = @IdInv2";

        cmd2.CommandText = query2;
        conn2.Open();
        cmd2.ExecuteNonQuery();
        conn2.Close();
       
        UpdateInventaris(HiddenIdInventaris.Value);
    }

    protected int UpdateInventaris(string idInv)
    {
      //  System.Diagnostics.Debug.WriteLine(" ID " + idInv);

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
        query = "select sum(qty_total) as jml_inv from t_inventaris_ruangan " +
                "where id_inventaris = @IdInv" +
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
        cmd2.Parameters.AddWithValue("@IdInv", idInv);
        //  cmd.Parameters.AddWithValue("@IdRuangan", HiddenIdInvRuangan.Value);
        //string id = HiddenIdInvRuangan.Value;
        query = "select qty from m_inventaris where id_inventaris=@IdInv";
        cmd2.CommandText = query;

        conn.Open();
        SqlDataReader reader2 = cmd2.ExecuteReader();

        while (reader2.Read())
        {
            qtyTotal = (int)reader2["qty"];
        }


        conn.Close();

        qtySesudahBukanRuangan = qtyTotal - qtySebelumRuangan;

       // System.Diagnostics.Debug.WriteLine("QTY SBLM RUANGAN : " + qtySebelumRuangan);

        conn = Common.getConnection();
        query = "";
        SqlCommand cmd3 = new SqlCommand();
        cmd3.Connection = conn;
        cmd3.CommandType = System.Data.CommandType.Text;
        cmd3.Parameters.AddWithValue("@IdInv", idInv);
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

    protected int CheckInv(string idInv)
    {
        //  System.Diagnostics.Debug.WriteLine(" ID " + idInv);

        //  String keterangan = "";
        UpdateInventaris(HiddenIdInventaris.Value);

        int result = 0;
        SqlConnection conn = Common.getConnection();
        string query = "";
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.Parameters.AddWithValue("@IdInv", idInv);
        //  cmd.Parameters.AddWithValue("@IdRuangan", HiddenIdInvRuangan.Value);
        string param = TbCari.Text;
        //string id = HiddenIdInvRuangan.Value;
        query = "select qty_belum_Terpakai from m_inventaris where id_inventaris = @IdInv";
        cmd.CommandText = query;

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();


        if (reader.Read())
        {
            //  HiddenItemID.Value = id;
            try
            {

                result = (int)reader["qty_belum_terpakai"];
            }
            catch (Exception ex)
            {
            }
            //  DDLStatusItem.SelectedValue = reader["status"].ToString();
        }

        conn.Close();

        return result;

    }

}