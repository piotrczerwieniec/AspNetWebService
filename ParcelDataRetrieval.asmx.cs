using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using System.Configuration;
//using System.Xml;  //metoda druga XmlDocument
using System.Xml.Serialization;

namespace AspNetWebServices
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://yrdyuqopytsvxrteormorv/com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {
       
        [WebMethod]
        
        public void DaysToManufacture(int ProdId, out String Name, out String Color, out decimal Weight)
        {
            //string connectionString;  po staremu 
            SqlConnection cnn;

            //connectionString = @"Data Source=DESKTOP-DO20ERI\SQLEXPRESS;Initial Catalog=AdventureWorks2012;User ID=AspNetWebService;Password=#dqMryErnDrtbovr";  po staremu
            //cnn = new SqlConnection(connectionString);  po staremu
            string connStr = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;   // moze trzeba zakryptowac https://stackoverflow.com/questions/10168240/encrypting-decrypting-a-string-in-c-sharp
            cnn = new SqlConnection(connStr);
            cnn.Open();
            SqlCommand command;
            SqlDataReader dataReader;
            String sql = "";
            Name = "";
            Color = "";
            Weight = 0;
            //System.Xml.XmlNode Logistic; //= System.Xml.XmlDocument.CreateNode(System.Xml.XmlNodeType.Element, "Logistic", "http://yrdyuqopytsvxrteormorv/com/");

            sql = "exec AspNetWebService " + ProdId;

            command = new SqlCommand(sql, cnn);

            dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                Name = dataReader.IsDBNull(0) ? "" : dataReader.GetString(0);
                Color = dataReader.IsDBNull(1) ? "" : dataReader.GetString(1);
                Weight = dataReader.IsDBNull(2) ? 0 : dataReader.GetDecimal(2);
            }

           
            dataReader.Close();
            command.Dispose();
            cnn.Close();

        }
       
        [WebMethod]
        public Shipment GetShipment(int ProdId)
        {

            XmlSerializer serializer =
            new XmlSerializer(typeof(Shipment));
            Shipment Sh = new Shipment();
            Details ShD = new Details();

            //string connectionString;  po staremu 
            SqlConnection cnn;

            //connectionString = @"Data Source=DESKTOP-DO20ERI\SQLEXPRESS;Initial Catalog=AdventureWorks2012;User ID=AspNetWebService;Password=#dqMryErnDrtbovr";  po staremu
            //cnn = new SqlConnection(connectionString);  po staremu
            string connStr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;   // mozetrzeba zakryptowac https://stackoverflow.com/questions/10168240/encrypting-decrypting-a-string-in-c-sharp
            cnn = new SqlConnection(connStr);
            

            SqlCommand command = new SqlCommand("AspNetWebService", cnn);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            SqlParameter parameter = new SqlParameter("@ProdID", ProdId);
            command.Parameters.Add(parameter);
            cnn.Open();
            SqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                Sh.Name = dataReader.IsDBNull(0) ? "" : dataReader.GetString(0);
                Sh.Color = dataReader.IsDBNull(1) ? "" : dataReader.GetString(1);
                ShD.Weight = dataReader.IsDBNull(2) ? 0 : dataReader.GetDecimal(2);
            }


            dataReader.Close();
            command.Dispose();
            cnn.Close();

            Sh.LogisticParameters = ShD;

            return Sh;
            

        }
    }

}
