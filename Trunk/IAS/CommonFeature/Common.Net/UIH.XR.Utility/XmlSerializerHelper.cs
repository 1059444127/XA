/// ------------------------------------------------------------------------------
/// <copyright company="United-Imaging">
/// Copyright (c) United-Imaging. All rights reserved.
/// </copyright>
/// <description>
/// </description>
/// ------------------------------------------------------------------------------


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using UIH.Mcsf.Core;
using System.Xml;
using System.IO;

namespace UIH.XR.Utility
{
    public static class XmlSerializerHelper
    {
        /// <summary>
        /// Desrialize
        /// </summary>
        /// <typeparam name="T">Target object type</typeparam>
        /// <param name="fullPathName">Full file path</param>
        /// <returns>Target object</returns>
        public static T Desrialize<T>(string fullPathName)
        {
            try
            {
                T obj = default(T);

                //Load xmlstring from xml file by FileParser
                IFileParserCSharp filePaser = ConfigParserFactory.Instance().CreateCSharpFileParser();
                filePaser.Initialize();
                filePaser.ParseByURI(fullPathName);
                string xmlString = filePaser.WriteToString();
                filePaser.Terminate();

                //Deserialize xmlstring to target object
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                obj = (T)(xmlSerializer.Deserialize(new StringReader(xmlString)));

                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception("Desrialize failed, Ex:" + ex.Message);
            }
        }
    }
}
