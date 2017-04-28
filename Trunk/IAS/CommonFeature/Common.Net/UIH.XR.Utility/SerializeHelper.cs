using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace UIH.XR.Utility
{
    public static class SerializeHelper
    {
        /// <summary>
        /// Serialize Object to Buffer
        /// </summary>
        /// <param name="obj">Source object</param>
        /// <returns>Buffer</returns>
        public static byte[] Serialize<T>(T obj)
        {
            try
            {
                IFormatter formatter = new BinaryFormatter();
                MemoryStream stream = new MemoryStream();
                formatter.Serialize(stream, obj);
                stream.Position = 0;
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                stream.Flush();
                stream.Close();
                return buffer;
            }
            catch (Exception ex)
            {
                throw new Exception("Serialize failed, Ex:" + ex.Message);
            }
        }

        /// <summary>
        /// Desrialize Object from Buffer
        /// </summary>
        /// <param name="buffer">Buffer</param>
        /// <returns>Target object</returns>
        public static T Desrialize<T>(byte[] buffer)
        {
            try
            {
                T obj = default(T);
                IFormatter formatter = new BinaryFormatter();
                MemoryStream stream = new MemoryStream(buffer);
                obj = (T)formatter.Deserialize(stream);
                stream.Flush();
                stream.Close();
                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception("Desrialize failed, Ex:" + ex.Message);
            }
        }
    }
}
