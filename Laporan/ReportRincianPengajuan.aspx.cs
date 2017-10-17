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

public partial class Laporan_ReportRincianPengajuan : System.Web.UI.Page
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
        string idPengajuan = Request.QueryString["IdPengajuan"];
        //dataTable

        ReportViewer1.Reset();

        DataTable dt = GetData(idPengajuan);
        DataTable dt2 = GetDetail(idPengajuan);
        ReportDataSource dataSource = new ReportDataSource("DataSetPengajuan", dt);

        ReportDataSource dataSource2 = new ReportDataSource("DataSetPengajuanDetail", dt2);


        ReportViewer1.LocalReport.DataSources.Add(dataSource);
        ReportViewer1.LocalReport.DataSources.Add(dataSource2); 
        ReportViewer1.LocalReport.ReportPath = "Laporan/RincianPengajuanBelanja.rdlc";

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

    private DataTable GetData(string idPengajuan)
    {
        DataTable dt = new DataTable();

        SqlConnection conn = Common.getConnection();
        string query = "";

        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.Connection = conn;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.AddWithValue("@idPengajuan", idPengajuan);

            query = "SELECT * FROM v_pengajuan where id = @idPengajuan";
            cmd.CommandText = query;
            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
            dataAdapter.Fill(dt);
        }
        return dt;

       
    }

    private DataTable GetDetail(string idPengajuan)
    {
        DataTable dt = new DataTable();

        SqlConnection conn = Common.getConnection();
        string query = "";

        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.Connection = conn;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.AddWithValue("@idPengajuan", idPengajuan);

            query = "SELECT * FROM v_pengajuan_inventaris_detail where id_pengajuan = @idPengajuan";
            cmd.CommandText = query;
            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
            dataAdapter.Fill(dt);
        }
        return dt;
    }

}