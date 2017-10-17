using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class Pemeliharaan_ListInventaris : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string id_ruangan = Session["id_ruangan"].ToString();
           // string id_ruangan = "1";
           HiddenId.Value = id_ruangan;

           InitNamaRuangan();
            GvBind();
        }

    }

    protected void InitNamaRuangan()
    {
        SqlConnection conn = Common.getConnection();
        string query = "";
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.Parameters.AddWithValue("@IdRuangan", HiddenId.Value);
        string param = TbCari.Text;
        string id = HiddenId.Value;
        query = "select nama_ruangan from m_ruangan where id_ruangan = @IdRuangan";
        cmd.CommandText = query;

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            LblNamaRuangan.Text = reader["nama_ruangan"].ToString();   
        }

        conn.Close();

    }


    protected void btnTambah_Click(object sender, EventArgs e)
    {
        Session["id_ruangan"] = HiddenId.Value;
        Response.Redirect("InventarisAdd.aspx");
    }


    protected void btnCatat_Click(object sender, EventArgs e)
    {
        Session["id_ruangan"] = HiddenId.Value;
        Response.Redirect("TambahCatatan.aspx");
    }


    protected void GvBind()
    {
        SqlDataSource dataSource = new SqlDataSource();
        dataSource.ConnectionString = ConfigurationManager.ConnectionStrings["InventoryConn"].ToString();
        string query = "select ir.*, i.nama_inventaris, ji.nama_jenis_inventaris from t_inventaris_ruangan ir inner join m_inventaris i "
           + " on ir.id_inventaris = i.id_inventaris left join m_jenis_inventaris ji on ji.id_jenis_inventaris = i.id_jenis_inventaris "
        +" where ir.id_ruangan = '" + HiddenId.Value + "'";
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
        if (e.CommandName == "Pilih")
        {
            string idInventaris = Convert.ToString(e.CommandArgument);
            ShowHistoryInventaris(idInventaris);

        }
        if (e.CommandName == "Ubah")
        {
            string idInventaris = Convert.ToString(e.CommandArgument);
            EditInventaris(idInventaris);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "LoadModal", "showModalEditInv();", true);


        }
        if (e.CommandName == "Detail")
        {
            string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ';' });

            string idRuangan = commandArgs[0];
            string idInventarisRuangan = commandArgs[1];
            string idInventaris = commandArgs[2];

            System.Diagnostics.Debug.WriteLine("ID INV " + idInventarisRuangan + " ID INV RUANGAN " + idInventaris);

            Session["id_inventaris"] = idInventaris;
            Session["id_inv_ruangan"] = idInventarisRuangan;
            Session["id_ruangan"] = HiddenId.Value;


            Response.Redirect("InventarisDetail.aspx");

        }
        if (e.CommandName == "Hapus")
        {
            string idInventaris = Convert.ToString(e.CommandArgument);
            HapusInventaris(idInventaris);

        }
    }

    protected void EditInventaris(string idInv)
    {
        SqlConnection conn = Common.getConnection();
        string query = "";
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.Parameters.AddWithValue("@IdInv",idInv);
        cmd.Parameters.AddWithValue("@IdRuangan", HiddenId.Value);
        string param = TbCari.Text;
        string id = HiddenId.Value;
        query = "select ir.*, i.nama_inventaris from t_inventaris_ruangan ir inner join m_inventaris i "
        +" on ir.id_inventaris = i.id where ir.id_inventaris = @idInv and id_ruangan = @IdRuangan";
        cmd.CommandText = query;

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            HiddenIdInventaris.Value = idInv;
            TbInventaris.Text = reader["nama_inventaris"].ToString();
            TbJmlInventarisHilang.Text = reader["qty_hilang"].ToString();
            TbJmlInventarisRusak.Text = reader["qty_rusak"].ToString();
            TbJmlInventarisTersedia.Text = reader["qty_tersedia"].ToString();
            TbInvKeterangan.Text = reader["keterangan"].ToString();
        }
        
        conn.Close();
        
    }

    protected void HapusInventaris(string idInv)
    {
        SqlConnection conn = Common.getConnection();
        string query = "";
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.Parameters.AddWithValue("@IdInv", idInv);
        cmd.Parameters.AddWithValue("@IdRuangan", HiddenId.Value);

        string param = TbCari.Text;
        string id = HiddenId.Value;
        query = "delete t_inventaris_ruangan where id_inventaris = @IdInv and id_ruangan = @IdRuangan";
        cmd.CommandText = query;

        conn.Open();
         cmd.ExecuteNonQuery();
        conn.Close();
        GvBind();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowModal", "alert('Sukses dihapus');", true);

    }

   

    protected void ShowHistoryInventaris(string id)
    {
        Session["id_inventaris"] = id;
        Response.Redirect("ListHistoryInventaris.aspx");
    }

    protected void GView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GvBind();
    }


    protected void Update_Click(object sender, EventArgs e)
    {
        SqlConnection conn = Common.getConnection();
        string query = "";
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.Parameters.AddWithValue("@IdInv", HiddenIdInventaris.Value);
        cmd.Parameters.AddWithValue("@qtyTerpakai", TbJmlInventarisTersedia.Text);
        cmd.Parameters.AddWithValue("@qtyRusak", TbJmlInventarisRusak.Text);
        cmd.Parameters.AddWithValue("@qtyHilang", TbJmlInventarisHilang.Text);
        cmd.Parameters.AddWithValue("@keterangan", TbInvKeterangan.Text);
        cmd.Parameters.AddWithValue("@qtyTotal", TbInvTotal.Text);
        cmd.Parameters.AddWithValue("@IdRuangan", HiddenId.Value);

        string param = TbCari.Text;
        string id = HiddenId.Value;
        query = "update t_inventaris_ruangan set qty_total = @qtyTotal, qty_rusak = @qtyRusak, qty_tersedia=@qtyTerpakai, qty_hilang = @qtyHilang, "
            +" keterangan = @keterangan where id_inventaris = @IdInv and id_ruangan = @IdRuangan";
        cmd.CommandText = query;

        conn.Open();
        cmd.ExecuteNonQuery();
        conn.Close();
        GvBind();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "HideLain", "alert('Sukses');hideModal();", true);


    }
   

    protected void btnCari_Click(object sender, EventArgs e)
    {
        string param = TbCari.Text;
        SqlDataSource dataSource = new SqlDataSource();
        dataSource.ConnectionString = ConfigurationManager.ConnectionStrings["InventoryConn"].ToString();
        string query = "select ir.*, i.nama_inventaris, ji.nama_jenis_inventaris from t_inventaris_ruangan ir inner join m_inventaris i "
           + " on ir.id_inventaris = i.id_inventaris left join m_jenis_inventaris ji on ji.id_jenis_inventaris = i.id_jenis_inventaris "
        +" where ir.id_ruangan = '" + HiddenId.Value + "' and i.nama_inventaris like '%"+param+"%'";
        System.Diagnostics.Debug.WriteLine("Q " + query);
        dataSource.SelectCommand = query;
        GridView1.DataSource = dataSource;
        GridView1.DataBind();

    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        GvBind();
    }
}