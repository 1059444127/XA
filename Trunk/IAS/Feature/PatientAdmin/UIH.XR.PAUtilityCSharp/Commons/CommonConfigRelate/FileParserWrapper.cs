using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIH.Mcsf.Core;

namespace UIH.XA.PAUtilityCSharp.Commons.CommonConfigRelate
{
    public static class FileParserWrapper
    {
        static FileParserWrapper()
        {
            _fileParser = ConfigParserFactory.Instance().CreateCSharpFileParser();
            _fileParser.Initialize();
        }


        private static IFileParserCSharp _fileParser;

        /// <summary>
        /// Use IFileParserCSharp to Save content to filepath
        /// </summary>
        /// <param name="content">Content String</param>
        /// <param name="filePath">File Path need to Save</param>
        public static void Save(string content, string filePath)
        {
            _fileParser.ParseByString(content);
            _fileParser.SaveDocument(filePath);
        }

        public static T GetUserSetting<T>(string relativePath)
        {
            return _fileParser.GetObjFromUserSettingsDir<T>(relativePath);
        }

        public static bool SaveUserSetting<T>(string relativePath, T t)
        {
            return _fileParser.SaveObjToUserSettingsDir(relativePath, t, 1);
        }
    }
}
