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

public partial class Laporan_LaporanKerusakanHarian : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
           
        }
    }

    private void GenerateReport()
    {
        //dataTable

        ReportViewer1.Reset();

        DataTable dt = GetData();
        ReportDataSource dataSource = new ReportDataSource("DSetLapKerusakan", dt);

      

        ReportViewer1.LocalReport.DataSources.Add(dataSource);
         ReportViewer1.LocalReport.ReportPath = "Laporan/KerusakanHarian.rdlc";

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

    private DataTable GetData()
    {
        DataTable dt = new DataTable();

        SqlConnection conn = Common.getConnection();
        string query = "";

        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.Connection = conn;
            cmd.CommandType = System.Data.CommandType.Text;

            query = "SELECT * FROM v_pencatatan_detail where Created_Date = '"+TbTanggal.Text+"' and tipe = 'Rusak'";
            cmd.CommandText = query;
            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
            dataAdapter.Fill(dt);
        }
        return dt;


    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        GenerateReport();
    }
}