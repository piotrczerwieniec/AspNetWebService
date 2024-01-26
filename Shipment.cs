using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Data.SqlClient;
using System.Configuration;

namespace AspNetWebServices
{
    //https://learn.microsoft.com/en-us/dotnet/standard/serialization/examples-of-xml-serialization instrukcja wykonania
    [XmlRoot("ShipmentDetails", Namespace = "http://yrdyuqopytsvxrteormorv/com/",
    IsNullable = false)] //isnullable pokazuje lub ukywa wezel/tag true- pokazuje false-chowa
    public class Shipment
    {
        [XmlAttribute]
        public String Name;
        public string Color;
        public Details LogisticParameters;


    }
    public class Details
    {
        //[XmlElement(IsNullable = true)]
        public Decimal? Weight; 
    }
  
  
}