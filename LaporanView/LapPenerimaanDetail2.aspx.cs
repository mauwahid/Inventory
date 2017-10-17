﻿using System;
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
        ReportViewer1.LocalReport.ReportPath = "Laporan/PenerimaanDetail.rdlc";

        PenerimaanDomain peDomain = GeneratePenerimaan(TbID.Text);

        ReportParameter[] parameters = new ReportParameter[5];
        parameters[0] = new ReportParameter("NoPenerimaan", peDomain.No);
        parameters[1] = new ReportParameter("TglPenerimaan", peDomain.TglPenerimaan);
        parameters[2] = new ReportParameter("Hal", peDomain.Hal);
        parameters[3] = new ReportParameter("Keterangan", peDomain.Keterangan);
        /* 
          if (peDomain.Tipe.Equals("1"))
          {
              parameters[4] = new ReportParameter("Tipe", "Dengan Pengajuan Barang");

          }
          else if (peDomain.Tipe.Equals("2"))
          {
              parameters[4] = new ReportParameter("Tipe", "Dengan Pengajuan Service");

          }
          else
          {
              parameters[5] = new ReportParameter("Tipe", "Tanpa Pengajuan");
          }

          SqlConnection conn = Common.getConnection();
          string query = "";
          SqlCommand cmd = new SqlCommand();
          cmd.Connection = conn;
          cmd.CommandType = System.Data.CommandType.Text;


          if (peDomain.Tipe.Equals("1"))
          {
              query = "select no_pengajuan from t_pengajuan_inventaris where id = " + peDomain.id + "";

          }
          else if (peDomain.Tipe.Equals("2"))
          {
              query = "select no_pengajuan from t_pengajuan_service where id = " + peDomain.id + "";

          }
          cmd.CommandText = query;

          conn.Open();
          try
          {
              SqlDataReader reader = cmd.ExecuteReader();
              reader.Read();
              string pengajuan = reader["no_pengajuan"].ToString().Trim();
              parameters[5] = new ReportParameter("RefPengajuan", pengajuan);

          }
          catch
          {
          }

          conn.Close();
  */
        parameters[4] = new ReportParameter("RefPengajuan", peDomain.RefPengajuan);

        ReportViewer1.LocalReport.SetParameters(parameters);

        DataTable dt = GetData(peDomain);
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
        System.Diagnostics.Debug.WriteLine("RPT " + ReportViewer1.Width);

        ReportViewer1.LocalReport.Refresh();

    }

    private DataTable GetData(PenerimaanDomain peDomain)
    {
        DataTable dt = new DataTable();

        SqlConnection conn = Common.getConnection();
        string query = "";

        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.Connection = conn;
            cmd.CommandType = System.Data.CommandType.Text;

            string cond = "where id_penerimaan = '" + peDomain.id + "'";
            query = "select * from v_penerimaan_report_detail " + cond;
            System.Diagnostics.Debug.WriteLine(query);
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


    private PenerimaanDomain GeneratePenerimaan(string noId)
    {
        SqlConnection conn = Common.getConnection();
        string query = "";
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;
        PenerimaanDomain pDomain = new PenerimaanDomain();

        /*
         * select a.*, b.username from t_penerimaan_inventaris a left join m_user b on a.id_user_kabag = b.id where a.id = " + HiddenIDPenerimaan.Value + "";
         */

        query = "select a.*, b.username from t_penerimaan a left join m_user b on a.id_user = b.id_user where a.id_penerimaan ='" + noId + "' or no_penerimaan ='" + noId + "'";
        System.Diagnostics.Debug.WriteLine(query);
        cmd.CommandText = query;

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();
        if (reader.Read())
        {

            pDomain.id = reader["id_penerimaan"].ToString();
            pDomain.Hal = reader["judul_penerimaan"].ToString().Trim();
            //  pDomain.Pengaju = reader["kabag"].ToString();
            pDomain.Keterangan = reader["keterangan"].ToString();
            // pDomain.RefPengajuan = reader["no_pengajuan"].ToString();
            pDomain.Tipe = reader["tipe_ref"].ToString();

            if (pDomain.Tipe.Equals("1"))
            {
                pDomain.RefPengajuan = reader["id_pembelian"].ToString();
            }
            else
            {
                pDomain.RefPengajuan = reader["id_service_keluar"].ToString();
            }
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
        report.ReportPath = "Laporan/PenerimaanDetail.rdlc";
        PenerimaanDomain peDomain = GeneratePenerimaan(TbID.Text);


        ReportParameter[] parameters = new ReportParameter[6];
        parameters[0] = new ReportParameter("No", peDomain.No);
        parameters[1] = new ReportParameter("TglPenerimaan", peDomain.TglPenerimaan);
        parameters[2] = new ReportParameter("Hal", peDomain.Hal);
        parameters[3] = new ReportParameter("Keterangan", peDomain.Keterangan);

        if (peDomain.Tipe.Equals("1"))
        {
            parameters[4] = new ReportParameter("Tipe", "Dengan Pengajuan Barang");

        }
        else if (peDomain.Tipe.Equals("2"))
        {
            parameters[4] = new ReportParameter("Tipe", "Dengan Pengajuan Service");

        }
        else
        {
            parameters[5] = new ReportParameter("Tipe", "Tanpa Pengajuan");
        }
        SqlConnection conn = Common.getConnection();
        string query = "";
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;


        if (peDomain.Tipe.Equals("1"))
        {
            query = "select no_pengajuan from t_pengajuan_inventaris where id = " + peDomain.id + "";

        }
        else if (peDomain.Tipe.Equals("2"))
        {
            query = "select no_pengajuan from t_service_inventaris where id = " + peDomain.id + "";

        }
        cmd.CommandText = query;

        conn.Open();
        try
        {
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            string pengajuan = reader["no_pengajuan"].ToString().Trim();
            parameters[5] = new ReportParameter("RefPengajuan", pengajuan);

        }
        catch
        {
        }

        conn.Close();
        report.SetParameters(parameters);

        DataTable dt = GetData(peDomain);

        report.DataSources.Add(new ReportDataSource("DataSet1", dt));



        Export(report);
        Print();
    }

    class PenerimaanDomain
    {
        public string id { get; set; }
        public string No { get; set; }
        public string TglPenerimaan { get; set; }
        public string Hal { get; set; }
        public string RefPengajuan { get; set; }
        public string Keterangan { get; set; }
        public string Pengaju { get; set; }
        public string Tipe { get; set; }
    }

}