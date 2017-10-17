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

public partial class LaporanView_LapPembelianDetail : System.Web.UI.Page
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
        ReportViewer1.LocalReport.ReportPath = "Laporan/PembelianDetail.rdlc";

        PembelianDomain peDomain = GeneratePembelian(TbIDNoPembelian.Text);

        ReportParameter[] parameters = new ReportParameter[6];
        parameters[0] = new ReportParameter("NoPembelian", peDomain.IDPembelian);
        parameters[1] = new ReportParameter("TglBeli", peDomain.Tgl);
       // parameters[2] = new ReportParameter("Judul", peDomain.Hal);
        parameters[2] = new ReportParameter("Keterangan", peDomain.Keterangan);
        parameters[3] = new ReportParameter("RefPengajuan", peDomain.RefPengajuan);
        parameters[4] = new ReportParameter("HargaTotal", peDomain.HargaTotal);
        parameters[5] = new ReportParameter("Status", peDomain.status);
       ReportViewer1.LocalReport.SetParameters(parameters);

        DataTable dt = GetData(peDomain.RefPengajuan);
        ReportDataSource dataSource = new ReportDataSource("DataSet1", dt);
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

    private DataTable GetData(string no)
    {
        DataTable dt = new DataTable();

        SqlConnection conn = Common.getConnection();
        string query = "";

        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.Connection = conn;
            cmd.CommandType = System.Data.CommandType.Text;

            string cond = "where id_pengajuan_inventaris ='" + no+"'";

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


    private PembelianDomain GeneratePembelian(string noId)
    {
        SqlConnection conn = Common.getConnection();
        string query = "";
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;
        PembelianDomain pDomain = new PembelianDomain();


        query = "select * from v_pembelian_report where id_pembelian ='" + noId + "' or no_pembelian ='" + noId + "'";
        cmd.CommandText = query;

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();
        reader.Read();
        pDomain.IDPembelian = reader["id_pembelian"].ToString();
        pDomain.NoPembelian = reader["no_pembelian"].ToString();
        pDomain.Keterangan = reader["keterangan"].ToString();
     //   pDomain.Keterangan = reader["memo_kabag"].ToString();
        pDomain.RefPengajuan = reader["id_pengajuan"].ToString();
        pDomain.HargaTotal = reader["harga_total"].ToString();
        pDomain.Tgl = reader["tanggal"].ToString();
        pDomain.status = reader["status"].ToString();

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
        report.ReportPath = "Laporan/PembelianDetail.rdlc";


        PembelianDomain peDomain = GeneratePembelian(TbIDNoPembelian.Text);

        ReportParameter[] parameters = new ReportParameter[6];
        parameters[0] = new ReportParameter("NoPembelian", peDomain.NoPembelian);
        parameters[1] = new ReportParameter("TglBeli", peDomain.Tgl);
        // parameters[2] = new ReportParameter("Judul", peDomain.Hal);
        parameters[2] = new ReportParameter("Keterangan", peDomain.Keterangan);
        parameters[3] = new ReportParameter("RefPengajuan", peDomain.RefPengajuan);
        parameters[4] = new ReportParameter("HargaTotal", peDomain.HargaTotal);
        parameters[5] = new ReportParameter("Status", peDomain.status);
       
        report.SetParameters(parameters);

        DataTable dt = GetData(peDomain.IDPengajuan);

        report.DataSources.Add(new ReportDataSource("DS_Pembelian_Detail", dt));



        Export(report);
        Print();
    }

    class PembelianDomain
    {
        public string IDPembelian { get; set; }
        public string NoPembelian { get; set; }
        public string Tgl { get; set; }
        public string Keterangan { get; set; }
        public string Pengaju { get; set; }
        public string RefPengajuan { get; set; }
        public string HargaTotal { get; set; }
        public string IDPengajuan { get; set; }
        public string status { get; set; }
    }
}