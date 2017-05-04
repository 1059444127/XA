using UIH.Mcsf.Pipeline.Data;
using UIH.Mcsf.Core;
using UIH.XR.Basis.Proto;
using Google.ProtocolBuffers;

namespace UIH.XA.SessionManager
{
    public class SessionUtility
    {
        /// <summary>
        /// Serialize dataHead
        /// </summary>
        public static byte[] SerializeDataHead(DicomAttributeCollection dataHead)
        {
            byte[] bValue = null;
            bool result = dataHead.Serialize(out bValue);
            return bValue;
        }

        /// <summary>
        /// Deserialize DataHead
        /// </summary>
        public static DicomAttributeCollection DeserializeDataHead(byte[] serializedDataHead)
        {
            return DicomAttributeCollection.Deserialize(serializedDataHead);
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
        /// Create a protobuffer object
        /// </summary>
        /// <param name="commandID">cmd</param>
        /// <param name="parArray">parameter array</param>
        /// <returns>protobuffer</returns>
        public static byte[] RequestCommandProtoBuffer(int commandID,ByteString[] parArray)
        {
            SessionRequestCommand.Builder builder = new SessionRequestCommand.Builder();
            builder.SetCommandId(commandID);
            builder.AddRangeParameters(parArray);
            return builder.Build().ToByteArray();
        }
    }
}
