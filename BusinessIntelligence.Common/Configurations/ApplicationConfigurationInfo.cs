using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Linq;
namespace BusinessIntelligence.Configurations
{
    public class ApplicationConfigurationInfo
    {
        public ApplicationConfigurationInfo(string configFileName)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(configFileName);
            XmlElement root = doc.DocumentElement;

        }
        public static string TryGetParameter(string configFileName, string path)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(configFileName);
            return TryGetParameter(doc.DocumentElement, path);
        }
        public static string[] TryGetParameters(string configFileName, string path)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(configFileName);
            return TryGetParameters(doc.DocumentElement, path);
        }
        public static string TryGetParameter(XElement xe, string path)
        {
            return TryGetParameter(ToXmlElement(xe), path);
        }
        public static string[] TryGetParameters(XElement xe, string path)
        {
            return TryGetParameters(ToXmlElement(xe), path);
        }
        public static string TryGetParameter(XmlElement xe, string path)
        {
            string ret;
            try
            {
                ret = xe.SelectNodes(path)[0].InnerText;
                return ret;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        private static XmlElement ToXmlElement(XElement el)
        {
            var doc = new XmlDocument();
            doc.Load(el.CreateReader());
            return doc.DocumentElement;
        }
        public static string[] TryGetParameters(XmlElement xe, string path)
        {
            List<string> ret = new List<string>();
            try
            {
                foreach (XmlNode item in xe.SelectNodes(path))
                {
                    ret.Add(item.InnerXml);
                }

                return ret.ToArray();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static XmlNodeList TryGetNodes(XElement xe, string path)
        {
            return TryGetNodes(ToXmlElement(xe), path);
        }
        public static XmlNodeList TryGetNodes(XmlElement xe, string path)
        {
            try
            {
                return xe.SelectNodes(path);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static XmlNodeList TryGetNodes(string configFileName, string path)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(configFileName);
            return TryGetNodes(doc.DocumentElement, path);
        }
        public static T[] TryGetObjects<T>(string configFileName, string path)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(configFileName);
            return TryGetObjects<T>(doc.DocumentElement, path);
        }
        public static T[] TryGetObjects<T>(XElement xe, string path)
        {
            return TryGetObjects<T>(ToXmlElement(xe), path);
        }
        public static T[] TryGetObjects<T>(XmlElement xe, string path)
        {
            var ret = new List<T>();

            var nodelist = xe.SelectNodes(path);

            if (nodelist != null && nodelist.Count > 0)
            {
                foreach (XmlNode node in nodelist)
                {
                    foreach (XmlNode cn in node.ChildNodes)
                    {
                        T o = (T)Activator.CreateInstance(typeof(T));

                        foreach (XmlAttribute a in cn.Attributes)
                        {
                            PropertyInfo prop = null;
                            foreach (var p in o.GetType().GetProperties())
                            {
                                if (p.Name.ToLower() == a.Name.ToLower() && p.CanWrite)
                                {
                                    prop = p;
                                    break;
                                }
                            }
                            if (prop != null)
                            {
                                prop.SetValue(o, a.Value, null);
                            }
                        }
                        ret.Add(o);
                    }
                }
                return ret.ToArray();

            }
            return null;
        }
    }
}