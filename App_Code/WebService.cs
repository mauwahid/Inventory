using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Web.Script.Services;
using System.Web.UI.WebControls;
/// <summary>
/// Summary description for WebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
 [System.Web.Script.Services.ScriptService]
public class WebService : System.Web.Services.WebService {

    public WebService () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public Notif GetNotif()
    {
        Notif notif = new Notif();
        SqlConnection conn = Common.getConnection();
        string query = "";
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;

        query = "SELECT count(*) as notif_pimpinan FROM v_notif_pimpinan";
        cmd.CommandText = query;

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            notif.Pimpinan =  reader["notif_pimpinan"].ToString();
        }
        conn.Close();

        query = "SELECT count(*) as notif_teknisi FROM t_penugasan_inventaris where status=0 ";
        cmd.CommandText = query;

        conn.Open();
        reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            notif.Teknisi = reader["notif_teknisi"].ToString();
        }
        conn.Close();

        return notif;
    }

    
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public Notifikasi GetNotifikasi()
    {
        Notifikasi notif = new Notifikasi();
        List<NotifPimpinan> notifPimpinan = new List<NotifPimpinan>();
        
        SqlConnection conn = Common.getConnection();
        string query = "";
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = System.Data.CommandType.Text;

        query = "SELECT count(*) as jml_notif, tipe FROM v_notif_pimpinan group by tipe";
        cmd.CommandText = query;

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            NotifPimpinan pimpinan = new NotifPimpinan();
            pimpinan.JmlNotif = reader["jml_notif"].ToString();
            pimpinan.Tipe = reader["tipe"].ToString();

            notifPimpinan.Add(pimpinan);
        }
        conn.Close();

        notif.Pimpinans = notifPimpinan;

        query = "SELECT count(*) as notif_teknisi FROM t_penugasan_inventaris where is_start=0 and status!=6 ";
        cmd.CommandText = query;

        conn.Open();
        reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            notif.notifTeknisi = reader["notif_teknisi"].ToString();
        }
        conn.Close();

        return notif;
    }

    public class Notif
    {
        public string Pimpinan { get; set; }
        public string Teknisi { get; set; }
    }

    public class Notifikasi
    {
        public List<NotifPimpinan> Pimpinans { get; set; }
        public string notifTeknisi { get; set; }
    }

    public class NotifPimpinan
    {
        public string JmlNotif { get; set; }
        public string Tipe { get; set; }
    }
}
