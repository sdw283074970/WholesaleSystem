using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace WholesaleSystem.Manager
{
    public class XmlManager
    {
        public XDocument GenerateXml(object js, string token, string key, string service)
        {
            XNamespace se = "http://schemas.xmlsoap.org/soap/envelope/";
            XNamespace ns = "http://www.example.org/Ec/";
            return new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"), 
                new XElement(se + "Envelope", new XAttribute(XNamespace.Xmlns + "SOAP-ENV", se.NamespaceName), new XAttribute(XNamespace.Xmlns + "ns1", ns.NamespaceName),
                    new XElement(se + "Body",
                        new XElement(ns + "callService", 
                            new XElement("paramsJson", js),
                            new XElement("appToken", token),
                            new XElement("appKey", key),
                            new XElement("service", service)
                        )
                    )
                )
            );
        }

        public string ToXml<T>(T obj)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                StringWriter writer = new StringWriter(CultureInfo.InvariantCulture);
                serializer.Serialize(writer, obj);

                string xml = writer.ToString();
                writer.Close();
                writer.Dispose();
                return xml;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
    }
}
