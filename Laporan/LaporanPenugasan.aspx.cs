using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Drawing.Printing;
using Microsoft.Reporting.WebForms;
public partial class Laporan_LaporanPenugasan : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            GenerateReport();
        }
    }

    private void GenerateReport()
    {
        string id = Request.QueryString["IdPenugasan"];
        //dataTable

        ReportViewer1.Reset();

        DataTable dt = GetData(id);
        DataTable dt2 = GetDetail(id);
        DataTable dt3 = GetDetailLain(id); 
        ReportDataSource dataSource = new ReportDataSource("DSPenugasan", dt);
        ReportDataSource dataSource2 = new ReportDataSource("DSPerbaikanDetail", dt2);
        ReportDataSource dataSource3 = new ReportDataSource("DSPenugasanLain", dt3);

        ReportViewer1.LocalReport.DataSources.Add(dataSource);
        ReportViewer1.LocalReport.DataSources.Add(dataSource2);
        ReportViewer1.LocalReport.DataSources.Add(dataSource3);
        ReportViewer1.LocalReport.ReportPath = "Laporan/PerbaikanBarang.rdlc";

        PageSettings pg = new System.Drawing.Printing.PageSettings();
        pg.Margins.Top = 0;
        pg.Margins.Bottom = 0;
        pg.Margins.Left = 0;
        pg.Margins.Right = 0;

        PrinterSettings ps = new System.Drawing.Printing.PrinterSettings();
        ps.PrinterName = "Microsoft XPS Document Writer";

        PaperSize size = new PaperSize();
        size.RawKind = (int)PaperKind.A4;
        pg.PaperSize = size;
        pg.PrinterSettings = ps;
        ReportViewer1.SetPageSettings(pg);

        ReportViewer1.LocalReport.Refresh();

    }

    private DataTable GetData(string id)
    {
        DataTable dt = new DataTable();

        SqlConnection conn = Common.getConnection();
        string query = "";

        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.Connection = conn;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.AddWithValue("@id", id);

            query = "SELECT * FROM t_penugasan where id = @id";
            cmd.CommandText = query;
            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
            dataAdapter.Fill(dt);
        }
        return dt;


    }

    private DataTable GetDetail(string id)
    {
        DataTable dt = new DataTable();

        SqlConnection conn = Common.getConnection();
        string query = "";

        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.Connection = conn;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.AddWithValue("@id", id);

            query = "SELECT * FROM v_perbaikan_detail where id_penugasan = @id";
            cmd.CommandText = query;
            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
            dataAdapter.Fill(dt);
        }
        return dt;
    }


    private DataTable GetDetailLain(string id)
    {
        DataTable dt = new DataTable();

        SqlConnection conn = Common.getConnection();
        string query = "";

        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.Connection = conn;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.AddWithValue("@id", id);

            query = "SELECT * FROM t_penugasan_lain_detail where id_penugasan = @id";
            cmd.CommandText = query;
            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
            dataAdapter.Fill(dt);
        }
        return dt;
    }

}