using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Configuration;
using System.Data.SqlClient;
using System.Data;

public partial class Admin_User_FormUser : System.Web.UI.Page
{
    String formType = "add";
        
    protected void Page_Load(object sender, EventArgs e)
    {
        String idUser = "";


        if (Session["user_form_type"] != null)
            formType = Session["user_form_type"].ToString();
        if (Request.QueryString["q"] != null)
            if (Request.QueryString["q"] == "add")
                formType = "add";

        if (!IsPostBack)
        {

            if (formType == null)
            {
                Response.Redirect("UserList.aspx");
            }else if (formType.Equals("add"))
            {
                GenerateFormTambah();
            }
            else if (formType.Equals("edit"))
            {
                idUser = Session["user_form_id"].ToString();
                GenerateFormChange(idUser);
            }

        }
    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        string textId = HiddenId.Value;
        if (textId.Equals("0"))
        {
            TambahData();
        }
        else 
        {
            UpdateData();
        }
    }

    private void GenerateFormTambah()
    {
        LblBC.Text = "Tambah User";
        LblHeader.Text = "Tambah User";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "LoadModal", "loadTambahUser();", true);
 
        
    }

    private void GenerateFormChange(string id)
    {
        HiddenId.Value = id;
        LblBC.Text = "Ubah User";
        LblHeader.Text = "Ubah User";

        SqlConnection conn = Common.getConnection();
        string query = "Select * from m_user where id_user = "+id;
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = query;

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();

        string sRole = "";

        reader.Read();
        HiddenId.Value = reader["id_user"].ToString();
        TbUsername.Text = reader["username"].ToString();
        TbNamaLengkap.Text =  reader["nama_lengkap"].ToString();
        TbNoTelp.Text = reader["no_telp"].ToString();
        TbEmail.Text = reader["email"].ToString();
        TbDeskripsi.Text = reader["deskripsi"].ToString();
        TbPassword.Text = reader["password"].ToString();

            sRole = reader["id_role"].ToString().Trim();
            switch (sRole)
            {
                case "1":
                    DDLRole.SelectedIndex = 0;
                    break;
                case "2":
                    DDLRole.SelectedIndex = 1;
                    break;
                case "3":
                    DDLRole.SelectedIndex = 2;
                    break;
                case "4":
                    DDLRole.SelectedIndex = 3;
                    break;
                case "5":
                    DDLRole.SelectedIndex = 4;
                    break;

            }
       
        conn.Close();
        
    }

    private void TambahData()
    {
        string query = "";
        SqlConnection conn = Common.getConnection();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = query;

        String password = Common.hashSHA256(TbPassword.Text);

        cmd.Parameters.AddWithValue("@username", TbUsername.Text);
        cmd.Parameters.AddWithValue("@password", password);
        cmd.Parameters.AddWithValue("@namaLengkap", TbNamaLengkap.Text);
        cmd.Parameters.AddWithValue("@noTelp", TbNoTelp.Text);
        cmd.Parameters.AddWithValue("@email", TbEmail.Text);
        cmd.Parameters.AddWithValue("@deskripsi", TbDeskripsi.Text);
        cmd.Parameters.AddWithValue("@role", DDLRole.SelectedValue);
 


        query = "INSERT INTO m_user (username, password, nama_lengkap, no_telp, email, deskripsi, id_role) " +
            " VALUES(@username, @password, @namaLengkap, @noTelp, @email, @deskripsi, @role)";


        conn.Open();
        cmd.Connection = conn;
        cmd.CommandText = query;
        cmd.ExecuteNonQuery();

        conn.Close();

        Response.Redirect("UserList.aspx");
    }


    private void UpdateData()
    {
        string query = "";
        SqlConnection conn = Common.getConnection();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = query;

        String password = Common.hashSHA256(TbPassword.Text);

        cmd.Parameters.AddWithValue("@id",HiddenId.Value);
        cmd.Parameters.AddWithValue("@username", TbUsername.Text);
        cmd.Parameters.AddWithValue("@password", password);
        cmd.Parameters.AddWithValue("@namaLengkap", TbNamaLengkap.Text);
        cmd.Parameters.AddWithValue("@noTelp", TbNoTelp.Text);
        cmd.Parameters.AddWithValue("@email", TbEmail.Text);
        cmd.Parameters.AddWithValue("@deskripsi", TbDeskripsi.Text);
        cmd.Parameters.AddWithValue("@role", DDLRole.SelectedValue);

        string tempPassword = TbPassword.Text;

        if (tempPassword.Equals("12345678910"))
        {
            query = "update m_user set username = @username, nama_lengkap = @namaLengkap, " +
       "no_telp = @noTelp, email = @email, deskripsi = @deskripsi, id_role = @role where id_user = @id";

        }
        else
        {
            query = "update m_user set username = @username, password = @password, nama_lengkap = @namaLengkap, " +
       "no_telp = @noTelp, email = @email, deskripsi = @deskripsi, id_role = @role where id_user = @id";

        }
        conn.Open();
        cmd.Connection = conn;
        cmd.CommandText = query;
        cmd.ExecuteNonQuery();

        conn.Close();

        Response.Redirect("UserList.aspx");
    }
    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("UserList.aspx");
    }
}