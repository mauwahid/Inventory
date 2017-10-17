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


using System.IO;
using System.Data;
using System.Text;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Collections.Generic;
using Microsoft.Reporting.WinForms;
using System.Drawing;

public partial class LaporanView_PengajuanDetail : System.Web.UI.Page
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
        ReportViewer1.LocalReport.ReportPath = "Laporan/PengajuanInvDetail.rdlc";
        
        PengajuanDomain peDomain = GeneratePengajuan(TbIDNoPengajuan.Text);

        ReportParameter[] parameters = new ReportParameter[6];
        parameters[0] = new ReportParameter("No", peDomain.No);
        parameters[1] = new ReportParameter("TglPengajuan", peDomain.TglPengajuan);
        parameters[2] = new ReportParameter("Hal", peDomain.Hal);
        parameters[3] = new ReportParameter("Keterangan", peDomain.Keterangan);
        parameters[4] = new ReportParameter("Prioritas", peDomain.Prioritas);
        parameters[5] = new ReportParameter("Pengaju", peDomain.Pengaju);
        ReportViewer1.LocalReport.SetParameters(parameters);
        
        DataTable dt = GetData(peDomain.id);
        ReportDataSource dataSource = new ReportDataSource("DS_V_Pengajuan_report_detail", dt);
        ReportViewer1.LocalReport.DataSources.Add(dataSource);
        ReportViewer1.ShowPrintButton = true;



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

            string cond = "where id_pengajuan="+id;

            query = "SELECT * FROM v_pengajuan_report_detail " + cond;
            cmd.CommandText = query;
            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
            dataAdapter.Fill(dt);
        }
        return dt;
    }

    protected void Generate_Click(object sender, EventArgs e)
    {
        GenerateReport();
    }

    protected void Print_Click(object sender, EventArgs e)
    {
        RunPrint();

        //  ReportPrintDocument rp = new ReportPrintDocument(ReportViewer1.ServerReport);
        // rp.Print(); 
    }


    private PengajuanDomain GeneratePengajuan(string noId)
    {
        SqlConnection conn = Common.getConnection();
        string query = "";
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;
        PengajuanDomain pDomain = new PengajuanDomain();
        

        query = "select * from v_pengajuan where id_pengajuan ='" + noId + "' or no_pengajuan ='"+noId+"'";
        cmd.CommandText = query;

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();
        reader.Read();
        pDomain.id = reader["id"].ToString();
        pDomain.Hal = reader["judul_pengajuan"].ToString().Trim();
        pDomain.Pengaju = reader["kabag"].ToString();
        pDomain.Keterangan = reader["keterangan"].ToString();
        pDomain.Keterangan = reader["memo_kabag"].ToString();
        pDomain.No = reader["no_pengajuan"].ToString();
        pDomain.TglPengajuan = reader["created_date"].ToString();

        string prioritas = reader["status_prioritas"].ToString();
        if (prioritas.Equals("1"))
        {
            pDomain.Prioritas = "URGENT";
        }
        else if (prioritas.Equals("2"))
        {
            pDomain.Prioritas = "PENTING";
        }
        else
        {
            pDomain.Prioritas = "NORMAL";
        }
       
        conn.Close();

        return pDomain;


    }




    private int m_currentPageIndex;
    private IList<Stream> m_streams;

    private DataTable LoadSalesData()
    {
        // Create a new DataSet and read sales data file 
        //    data.xml into the first DataTable.
        DataSet dataSet = new DataSet();
        dataSet.ReadXml(@"..\..\data.xml");
        return dataSet.Tables[0];
    }
    // Routine to provide to the report renderer, in order to
    //    save an image for each page of the report.
    private Stream CreateStream(string name,
      string fileNameExtension, Encoding encoding,
      string mimeType, bool willSeek)
    {
        Stream stream = new MemoryStream();
        m_streams.Add(stream);
        return stream;
    }
    // Export the given report as an EMF (Enhanced Metafile) file.
    private void Export(LocalReport report)
    {
        string deviceInfo =
          @"<DeviceInfo>
                <OutputFormat>EMF</OutputFormat>
                <PageWidth>8.5in</PageWidth>
                <PageHeight>11in</PageHeight>
                <MarginTop>0.25in</MarginTop>
                <MarginLeft>0.25in</MarginLeft>
                <MarginRight>0.25in</MarginRight>
                <MarginBottom>0.25in</MarginBottom>
            </DeviceInfo>";
        Warning[] warnings;
        m_streams = new List<Stream>();
        report.Render("Image", deviceInfo, CreateStream,
           out warnings);
        foreach (Stream stream in m_streams)
            stream.Position = 0;
    }
    // Handler for PrintPageEvents
    private void PrintPage(object sender, PrintPageEventArgs ev)
    {
        Metafile pageImage = new
           Metafile(m_streams[m_currentPageIndex]);

        // Adjust rectangular area with printer margins.
        Rectangle adjustedRect = new Rectangle(
            ev.PageBounds.Left - (int)ev.PageSettings.HardMarginX,
            ev.PageBounds.Top - (int)ev.PageSettings.HardMarginY,
            ev.PageBounds.Width,
            ev.PageBounds.Height);

        // Draw a white background for the report
        ev.Graphics.FillRectangle(Brushes.White, adjustedRect);

        // Draw the report content
        ev.Graphics.DrawImage(pageImage, adjustedRect);

        // Prepare for the next page. Make sure we haven't hit the end.
        m_currentPageIndex++;
        ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
    }

    private void Print()
    {
        if (m_streams == null || m_streams.Count == 0)
            throw new Exception("Error: no stream to print.");

        PrintDocument printDoc = new PrintDocument();
        // printDoc.PrinterSettings = 
        PrinterSettings ps = new System.Drawing.Printing.PrinterSettings();
        ps.PrinterName = "Microsoft XPS Document Writer";

        if (!printDoc.PrinterSettings.IsValid)
        {
            throw new Exception("Error: cannot find the default printer.");
        }
        else
        {
            printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
            m_currentPageIndex = 0;
            printDoc.Print();
        }
    }
    // Create a local report for Report.rdlc, load the data,
    //    export the report to an .emf file, and print it.
    private void RunPrint()
    {
        LocalReport report = new LocalReport();
        report.ReportPath = "Laporan/PengajuanInvDetail.rdlc";
       

        PengajuanDomain peDomain = GeneratePengajuan(TbIDNoPengajuan.Text);

        ReportParameter[] parameters = new ReportParameter[6];
        parameters[0] = new ReportParameter("No", peDomain.No);
        parameters[1] = new ReportParameter("TglPengajuan", peDomain.TglPengajuan);
        parameters[2] = new ReportParameter("Hal", peDomain.Hal);
        parameters[3] = new ReportParameter("Keterangan", peDomain.Keterangan);
        parameters[4] = new ReportParameter("Prioritas", peDomain.Prioritas);
        parameters[5] = new ReportParameter("Pengaju", peDomain.Pengaju);
        report.SetParameters(parameters);

        DataTable dt = GetData(peDomain.id);
        
        report.DataSources.Add(new ReportDataSource("DS_V_Pengajuan_report_detail", dt));
  


        Export(report);
        Print();
    }

    class PengajuanDomain
    {
        public string id { get; set; }
        public string No { get; set; }
        public string TglPengajuan { get; set; }
        public string Hal { get; set; }
        public string Keterangan { get; set; }
        public string NB { get; set; }
        public string Prioritas { get; set; }
        public string Pengaju { get; set; }
    }
}