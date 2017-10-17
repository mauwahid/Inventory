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

public partial class LaporanView_LapInvTotal : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GenerateDDLGedung();
            GenerateDDLLantai();
            GenerateDDLRuangan();
        }
    }

    private void GenerateDDLGedung()
    {

        SqlConnection conn = Common.getConnection();
        string query = "";
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;

        query = "select id_gedung, nama_gedung from m_gedung ";
        cmd.CommandText = query;

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();

        DDLGedung.DataSource = reader;
        DDLGedung.DataValueField = "id_gedung";
        DDLGedung.DataTextField = "nama_gedung";
        DDLGedung.DataBind();

        ListItem item = new ListItem();
        item.Value = "0";
        item.Text = "-Semua-";
        DDLGedung.Items.Insert(0, item);

        conn.Close();



    }

    private void GenerateDDLLantai()
    {
        ListItem item = new ListItem();
        item.Value = "0";
        item.Text = "-Semua-";

        if (!DDLGedung.SelectedValue.Equals("0"))
        {

            SqlConnection conn = Common.getConnection();
            string query = "";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = System.Data.CommandType.Text;

            query = "select id_lantai, lokasi_lantai from m_lantai where id_gedung = " + DDLGedung.SelectedValue + "";
            cmd.CommandText = query;

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            DDLLantai.DataSource = reader;
            DDLLantai.DataValueField = "id_lantai";
            DDLLantai.DataTextField = "lokasi_lantai";
            DDLLantai.DataBind();


            DDLLantai.Items.Insert(0, item);

            conn.Close();


        }
        else
        {
            DDLLantai.Items.Insert(0, item);

        }


    }

    private void GenerateDDLRuangan()
    {
        ListItem item = new ListItem();
        item.Value = "0";
        item.Text = "-Semua-";

        if (!DDLLantai.SelectedValue.Equals("0"))
        {

            SqlConnection conn = Common.getConnection();
            string query = "";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = System.Data.CommandType.Text;

            query = "select id_ruangan, nama_ruangan from m_ruangan where id_lantai = " + DDLLantai.SelectedValue + "";
            cmd.CommandText = query;

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            DDLRuangan.DataSource = reader;
            DDLRuangan.DataValueField = "id_ruangan";
            DDLRuangan.DataTextField = "nama_ruangan";
            DDLRuangan.DataBind();


            DDLRuangan.Items.Insert(0, item);

            conn.Close();


        }
        else
        {
            DDLRuangan.Items.Insert(0, item);

        }


    }


    private void GenerateReport()
    {
        //dataTable

        ReportViewer1.Reset();

        DataTable dt = GetData();
        ReportDataSource dataSource = new ReportDataSource("DataSet1", dt);


        ReportViewer1.LocalReport.DataSources.Add(dataSource);
        ReportViewer1.LocalReport.ReportPath = "Laporan/InvTotal.rdlc";
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

    private DataTable GetData()
    {
        DataTable dt = new DataTable();

        SqlConnection conn = Common.getConnection();
        string query = "";

        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.Connection = conn;
            cmd.CommandType = System.Data.CommandType.Text;

            string cond = "";
            if (DDLGedung.SelectedIndex != 0)
            {
                cond = " where id_gedung = '" + DDLGedung.SelectedValue + "'";
                if(DDLLantai.SelectedIndex!=0){
                    cond = cond + " and id_lantai = '" + DDLLantai.SelectedValue + "'";

                    if (DDLRuangan.SelectedIndex != 0)
                        cond = cond + " and id_ruangan = '" + DDLRuangan.SelectedValue + "'";
                }

            }

            query = "SELECT * FROM v_inv_total " + cond;
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


   

    protected void DDLGedung_SelectedIndexChanged(object sender, EventArgs e)
    {
        GenerateDDLLantai();
    }
    protected void DDLLantai_SelectedIndexChanged(object sender, EventArgs e)
    {
        GenerateDDLRuangan();
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
         report.ReportPath = "Laporan/InvTotal.rdlc";
         DataTable dt = GetData();
         ReportDataSource dataSource = new ReportDataSource("DataSet1", dt);

        Export(report);
        Print();
    }

}