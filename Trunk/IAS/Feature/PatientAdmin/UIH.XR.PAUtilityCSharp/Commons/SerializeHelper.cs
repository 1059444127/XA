using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using UIH.XA.PAUtilityCSharp.Global;

namespace UIH.XA.PAUtilityCSharp.Commons
{
    static public class SerializeHelper
    {
        public static T DeserializeXML<T>(string xmlData)
            where T : new()
        {
            if (string.IsNullOrEmpty(xmlData))
                return default(T);

            T DocItms;
            XmlSerializer xms = new XmlSerializer(typeof(T));
            using (var tr = new StringReader(xmlData))
            {
                DocItms = (T)xms.Deserialize(tr);
            }

            return DocItms == null ? default(T) : DocItms;
        }

        /// <summary>
        /// used for droved object to base class.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlObject"></param>
        /// <returns></returns>
        public static string SeralizeObjectToXML<T>(T xmlObject)
        {
            if (null == xmlObject)
            {
                return "";
            }

            StringBuilder sbTR = new StringBuilder();

            XmlSerializer xmsTR = new XmlSerializer(xmlObject.GetType());

            XmlWriterSettings xwsTR = new XmlWriterSettings();

            XmlWriter xmwTR = XmlWriter.Create(sbTR, xwsTR);

            xmsTR.Serialize(xmwTR, xmlObject);

            return sbTR.ToString();
        }

        /// <summary>
        /// used for dictionary class. (private property ignored)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objClone"></param>
        /// <returns></returns>
        public static T CloneObject<T>(T objClone)
            where T : new()
        {
            string GetString = SerializeHelper.SeralizeObjectToXML<T>(objClone);

            return SerializeHelper.DeserializeXML<T>(GetString);

            //string GetString = JsonConvert.SerializeObject(objClone);
            //return JsonConvert.DeserializeObject<T>(GetString);
            
        }

        public static T CloneObjectByXML<T>(T objClone)
            where T : new()
        {
            string GetString = SerializeHelper.SeralizeObjectToXML<T>(objClone);

            return SerializeHelper.DeserializeXML<T>(GetString);
        }

        /// <summary>
        /// used for deep cloning an object quickly
        /// </summary>
        /// <typeparam name="T">an object that should add [Serializable] tag</typeparam>
        /// <param name="obj">result</param>
        /// <returns>the deep clone object</returns>
        public static T DeepClone<T>(T obj)
        {
            using (var ms = new System.IO.MemoryStream())
            {
                var formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                try
                {
                    formatter.Serialize(ms, obj);
                    ms.Position = 0;

                    return (T)formatter.Deserialize(ms);
                }
                catch (Exception ex)
                {
                    GlobalDefinition.LoggerWrapper.LogDevError(ex.Message);
                    return default(T);
                }
            }
        }
    }
}
