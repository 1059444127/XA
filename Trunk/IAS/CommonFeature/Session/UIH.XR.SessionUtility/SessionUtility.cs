using System.Text;
using UIH.Mcsf.Pipeline.Data;
using UIH.Mcsf.Core;
using UIH.XR.Basis.Proto;

namespace UIH.XR.SessionManager
{
    public class SessionUtility
    {
        /// <summary>
        /// Serialize dataHead to string
        /// </summary>
        /// <param name="dataHead">dataHead object</param>
        /// <returns>serializeDataHead</returns>
        public static string SerializeDataHead(DicomAttributeCollection dataHead)
        {
            byte[] bValue = null;
            bool result = dataHead.Serialize(out bValue);
            return result ? Encoding.Default.GetString(bValue) : null;
        }

        /// <summary>
        /// Deserialize string to dataHead
        /// </summary>
        /// <param name="serializedDataHead">a serialized dataHead</param>
        /// <returns>dataHead object</returns>
        public static DicomAttributeCollection DeserializeDataHead(string serializedDataHead)
        {
            return DicomAttributeCollection.Deserialize(Encoding.Default.GetBytes(serializedDataHead));
        }

        /// <summary>
        /// Create commandContext
        /// </summary>
        /// <param name="receiver">receiver of communication</param>
        /// <param name="commandID">commandID of communication</param>
        /// <param name="serializeObject">message needs to send</param>
        /// <returns>CommandContext object</returns>
        public static CommandContext CreateCommandContext(string receiver, int commandID, byte[] serializeObject)
        {
            CommandContext context = new CommandContext();
            context.sReceiver = receiver;
            context.iCommandId = commandID;
            context.sSerializeObject = serializeObject;
            return context;
        }

        /// <summary>
        /// Create a protobuffer,type is byte[]
        /// </summary>
        /// <param name="commandID">cmd</param>
        /// <param name="parArray">parameter array</param>
        /// <returns>protobuffer</returns>
        public static byte[] RequestCommandProtoBuffer(int commandID, string[] parArray)
        {
            SessionRequestCommand.Builder builder = new SessionRequestCommand.Builder();
            builder.SetCommandId(commandID);
            builder.AddRangeParameters(parArray);
            return builder.Build().ToByteArray();
        }
    }
}
