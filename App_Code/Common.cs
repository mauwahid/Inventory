using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;

using System.Configuration;
using System.Data.SqlClient;
using System.Data;

using System.Configuration; 
/// <summary>
/// Summary description for Common
/// </summary>
public class Common
{
    public static SqlConnection getConnection()
    {
        return new SqlConnection(ConfigurationManager.ConnectionStrings["InventoryConn"].ConnectionString);
        
    }

    public static string hashSHA256(string unhashedValue)
    {
        SHA256Managed shaM = new SHA256Managed();
        byte[] hash =
         shaM.ComputeHash(Encoding.ASCII.GetBytes(unhashedValue));

        StringBuilder stringBuilder = new StringBuilder();
        foreach (byte b in hash)
        {
            stringBuilder.AppendFormat("{0:x1}", b);
        }
        return stringBuilder.ToString();
    }
}