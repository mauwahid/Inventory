using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;


public partial class Penugasan_ListPenugasan : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GvBind();
        }

    }




    protected void GvBind()
    {
        SqlDataSource dataSource = new SqlDataSource();
        dataSource.ConnectionString = ConfigurationManager.ConnectionStrings["InventoryConn"].ToString();
        dataSource.SelectCommand = "SELECT p.id_penugasan,REPLACE(CONVERT(VARCHAR(9), p.tanggal_penugasan, 6), ' ', '-') AS [tanggal_penugasan], u.username, p.judul, "
                                   + " case when p.status = 0 then 'Belum Dikerjakan' "
                                   + " when p.status = 1 then 'Masih Dikerjakan'"
                                   + " when p.status = 2 then 'Telah SELESAI'"
                                   + " when p.status = 3 then 'Sebagian Selesai'"
                                   + " when p.status = 4 then 'Sebagian Tidak Bisa Dikerjakan'"
                                   + " when p.status = 5 then 'Tidak Bisa Dikerjakan' end as status"
                                  + " from t_penugasan_inventaris p "
                                  + " inner join m_user u on p.id_user = u.id_user where"
                                  + " p.status != 6 order by id_penugasan desc ";


        GridView1.DataSource = dataSource;
        GridView1.DataBind();



    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        // Check if row is data row, not header, footer etc.
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // Get value of third column. Index is zero based, to 
            // get text of third column we use Cells[2].Text
            string CellValue = e.Row.Cells[4].Text.Trim();

            // If value is greater of 10, change format
            switch (CellValue)
            {
                case "Belum Dikerjakan":
                    e.Row.Cells[4].ForeColor = System.Drawing.Color.Purple;
                    //      e.Row.Cells[4].CssClass = "label pull-left bg-black";
                    break;
                case "Masih Dikerjakan":
                    e.Row.Cells[4].ForeColor = System.Drawing.Color.Orange;
                    break;
                case "Telah SELESAI":
                    e.Row.Cells[4].ForeColor = System.Drawing.Color.Blue;
                    break;
                case "Sebagian Selesai":
                    e.Row.Cells[4].ForeColor = System.Drawing.Color.BlueViolet;
                    break;
                case "Sebagian Tidak Bisa Dikerjakan":
                    e.Row.Cells[4].ForeColor = System.Drawing.Color.Orange;
                    break;
                case "Tidak Bisa Dikerjakan":
                    e.Row.Cells[4].ForeColor = System.Drawing.Color.Red;
                    break;
            }

        }
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
            string idPengajuan = Convert.ToString(e.CommandArgument);
            Session["id_penugasan"] = idPengajuan;
            Response.Redirect("ListPenugasanDetail.aspx");

        }
    }



    protected void btnCari_Click(object sender, EventArgs e)
    {
        string param = TbCari.Text;
        SqlDataSource dataSource = new SqlDataSource();
        dataSource.ConnectionString = ConfigurationManager.ConnectionStrings["InventoryConn"].ToString();
        dataSource.SelectCommand = "SELECT p.id_penugasan, p.tanggal_penugasan, u.username, p.judul, "
                                   + " case when p.status = 0 then 'Belum Dikerjakan' "
                                   + " when p.status = 1 then 'Masih Dikerjakan'"
                                   + " when p.status = 2 then 'Telah SELESAI'"
                                   + " when p.status = 3 then 'Sebagian Selesai'"
                                   + " when p.status = 4 then 'Sebagian Tidak Bisa Dikerjakan'"
                                   + " when p.status = 5 then 'Tidak Bisa Dikerjakan' end as status"
                                  + " from t_penugasan_inventaris p "
                                  + " inner join m_user u on p.id_user = u.id_user where"
                                  + " p.status != 6 "
                                   +" and p.judul like '%"+param+"%' order by id_penugasan desc ";


        GridView1.DataSource = dataSource;
        GridView1.DataBind();


    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        GvBind();
    }

    protected void GView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GvBind();
    }
}