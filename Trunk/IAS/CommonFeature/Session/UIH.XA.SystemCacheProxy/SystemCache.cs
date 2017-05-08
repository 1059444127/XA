using UIH.Mcsf.Core;
using UIH.Mcsf.Pipeline.Data;
using UIH.Mcsf.Pipeline.Dictionary;
using UIH.XA.GlobalParameter;
using UIH.Mcsf.Log;
using Google.ProtocolBuffers;

namespace UIH.XA.SessionManager
{
    public class SystemCacheProxy
    {
        private ICommunicationProxy _systemCommunicationProxy;
        private DicomAttributeCollection _dataHead;//session模块的tag应该需要申请，防止tag号相同

        /// <summary>
        /// send command to session to set the dataHead
        /// </summary>
        /// <returns></returns>
        public bool Commit()
        {
            byte[] aa=SessionUtility.SerializeDataHead(_dataHead);
            byte[] buf = SessionUtility.RequestCommandProtoBuffer(SessionManagerFuncID.StoreSessionDataID, new ByteString[] { ByteString.CopyFrom(aa) });
            CommandContext context = SessionUtility.CreateCommandContext(CommunicationNode.SystemSessionManager, CommunicationCommandID.XA_SYSTEMSESSION_DATA_CACHE, buf);
            ISyncResult result = _systemCommunicationProxy.SyncSendCommand(context);
            return (0 == result.GetCallResult()) ? true : false;
        }

        /// <summary>
        /// send command to sessionmanager to get the dataHead
        /// </summary>
        /// <returns></returns>
        public bool Refresh()
        {
            byte[] buf = SessionUtility.RequestCommandProtoBuffer(SessionManagerFuncID.QuerySessionDataID, new ByteString[] { });
            CommandContext context = SessionUtility.CreateCommandContext(CommunicationNode.SystemSessionManager, CommunicationCommandID.XA_SYSTEMSESSION_DATA_CACHE, buf);
            ISyncResult result = _systemCommunicationProxy.SyncSendCommand(context);
            CLRLogger.GetInstance().LogDevInfo(string.Format("Commit result:{0},proxyName:{1}", result.GetCallResult().ToString(), _systemCommunicationProxy.GetName()));
            _dataHead = SessionUtility.DeserializeDataHead(result.GetSerializedObject());
            return (0 == result.GetCallResult()) ? true : false;
        }

        /// <summary>
        /// Init proxy
        /// </summary>
        /// <param name="processCommunicationProxy">proxy object</param>
        /// <returns></returns>
        public bool Initialize(ICommunicationProxy proxy)
        {
            if (null == proxy)
            {
                CLRLogger.GetInstance().LogDevError("proxy is null");
                return false;
            }
            _systemCommunicationProxy = proxy;
            PreExecute();
            return true;
        }

        private void PreExecute()
        {
            byte[] bvalue;
            string strValue;
            int iValue;
            uint uValue;
            ushort ushortValue;
            short sValue;
            float fValue;
            SetBytes(0X0000001, new byte[] { 0x01 });
            GetBytes(0X0000001, out bvalue);
            SetString16(0X0000001, "1");
            SetString64(0X0000001, "1");
            GetString(0X0000001, out strValue);
            SetInt32(0X0000001, 1);
            GetInt32(0X0000001, out iValue);
            SetUInt32(0X0000001, 1);
            GetUInt32(0X0000001, out uValue);
            SetUInt16(0X0000001,1);
            GetUInt16(0X0000001, out ushortValue);
            SetInt16(0X0000001, 1);
            GetInt16(0X0000001, out sValue);
            SetFloat(0X0000001, 1);
            GetFloat32(0X0000001,out fValue);
            Commit();
            Refresh();
        }

        public SystemCacheProxy()
        {
            _dataHead = new DicomAttributeCollection();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tagCode"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool SetBytes(uint tagCode, byte[] value)
        {
            if (_dataHead.Contains(tagCode))
            {
                _dataHead.RemoveDicomAttribute(tagCode);
            }
            DicomVR TagVR = new DicomVR(VR.OB);
            DicomTag tag = new DicomTag(tagCode);
            tag.TagVR = TagVR;
            DicomAttribute dicomAttr = DicomAttribute.CreateAttribute(tag);
            if (!dicomAttr.SetBytes(0, value))
                return false;
            _dataHead.AddDicomAttribute(dicomAttr);
            return true;
        }

        /// <summary>
        /// store value in dataHead
        /// </summary>
        /// <param name="tagCode">tag</param>
        /// <param name="value">the value to store,it's length can not more than 16 </param>
        /// <returns></returns>
        public bool SetString16(uint tagCode, string value)
        {
            if (_dataHead.Contains(tagCode))
            {
                _dataHead.RemoveDicomAttribute(tagCode);
            }
            DicomVR TagVR = new DicomVR(VR.CS);
            DicomTag tag = new DicomTag(tagCode);
            tag.TagVR = TagVR;
            DicomAttribute dicomAttr = DicomAttribute.CreateAttribute(tag);
            if (!dicomAttr.SetString(0, value))
                return false;
            _dataHead.AddDicomAttribute(dicomAttr);
            return true;
        }

        public bool SetString16Array(uint tagCode, string []value)
        {
            if (_dataHead.Contains(tagCode))
            {
                _dataHead.RemoveDicomAttribute(tagCode);
            }
            DicomTag tag = new DicomTag(tagCode, "", VR.CS, true, false);
            DicomAttribute dicomAttr = DicomAttribute.CreateAttribute(tag);
            for (int i = 0; i < value.Length; i++)
            {
                dicomAttr.SetString(i, value[i]);
            }
            _dataHead.AddDicomAttribute(dicomAttr);
            return true;
        }

        /// <summary>
        ///  store value in dataHead
        /// </summary>
        /// <param name="tagCode">tagCode</param>
        /// <param name="value">the value to store,it's length can not more than 64 </param>
        /// <returns></returns>
        public bool SetString64(uint tagCode,string value)
        {
            if (_dataHead.Contains(tagCode))
            {
                _dataHead.RemoveDicomAttribute(tagCode);
            }
            DicomVR TagVR = new DicomVR(VR.LO);
            DicomTag tag = new DicomTag(tagCode);
            tag.TagVR = TagVR;
            DicomAttribute dicomAttr = DicomAttribute.CreateAttribute(tag);
            if (!dicomAttr.SetString(0, value))
                return false;
            _dataHead.AddDicomAttribute(dicomAttr);
            return true;
        }

        public bool SetString64Array(uint tagCode, string[] value)
        {
            if (_dataHead.Contains(tagCode))
            {
                _dataHead.RemoveDicomAttribute(tagCode);
            }
            DicomTag tag = new DicomTag(tagCode, "", VR.LO, true, false);
            DicomAttribute dicomAttr = DicomAttribute.CreateAttribute(tag);
            for (int i = 0; i < value.Length; i++)
            {
                if (!dicomAttr.SetString(i, value[i]))
                    return false;
            }
            _dataHead.AddDicomAttribute(dicomAttr);
            return true;
        }

        public bool SetInt16(uint tagCode, short value)
        {
            if (_dataHead.Contains(tagCode))
            {
                _dataHead.RemoveDicomAttribute(tagCode);
            }
            DicomVR TagVR = new DicomVR(VR.SS);
            DicomTag tag = new DicomTag(tagCode);
            tag.TagVR = TagVR;
            DicomAttribute dicomAttr = DicomAttribute.CreateAttribute(tag);
            if (!dicomAttr.SetInt16(0, value))
                return false;
            _dataHead.AddDicomAttribute(dicomAttr);
            return true;
        }

        public bool SetInt16Array(uint tagCode, short []value)
        {
            if (_dataHead.Contains(tagCode))
            {
                _dataHead.RemoveDicomAttribute(tagCode);
            }
            DicomTag tag = new DicomTag(tagCode, "", VR.SS, true, false);
            DicomAttribute dicomAttr = DicomAttribute.CreateAttribute(tag);
            for (int i = 0; i < value.Length; i++)
            {
                if (!dicomAttr.SetInt16(i, value[i]))
                    return false;
            }
            _dataHead.AddDicomAttribute(dicomAttr);
            return true;
        }

        public bool SetUInt16(uint tagCode, ushort value)
        {
            if (_dataHead.Contains(tagCode))
            {
                _dataHead.RemoveDicomAttribute(tagCode);
            }
            DicomVR TagVR = new DicomVR(VR.US);
            DicomTag tag = new DicomTag(tagCode);
            tag.TagVR = TagVR;
            DicomAttribute dicomAttr = DicomAttribute.CreateAttribute(tag);
            dicomAttr.SetUInt16(0, value);
            _dataHead.AddDicomAttribute(dicomAttr);
            return true;
        }

        public bool SetUInt16Array(uint tagCode, ushort[] value)
        {
            if (_dataHead.Contains(tagCode))
            {
                _dataHead.RemoveDicomAttribute(tagCode);
            }
            DicomTag tag = new DicomTag(tagCode, "", VR.US, true, false);
            DicomAttribute dicomAttr = DicomAttribute.CreateAttribute(tag);
            for (int i = 0; i < value.Length; i++)
            {
                if (!dicomAttr.SetUInt16(i, value[i]))
                    return false;
            }
            _dataHead.AddDicomAttribute(dicomAttr);
            return true;
        }

        public bool SetInt32(uint tagCode, int value)
        {
            if (_dataHead.Contains(tagCode))
            {
                _dataHead.RemoveDicomAttribute(tagCode);
            }
            DicomVR TagVR = new DicomVR(VR.SL);
            DicomTag tag = new DicomTag(tagCode);
            tag.TagVR = TagVR;
            DicomAttribute dicomAttr = DicomAttribute.CreateAttribute(tag);
            if (!dicomAttr.SetInt32(0, value))
                return false;
            _dataHead.AddDicomAttribute(dicomAttr);
            return true;
        }

        public bool SetInt32Array(uint tagCode, int []value)
        {
            if (_dataHead.Contains(tagCode))
            {
                _dataHead.RemoveDicomAttribute(tagCode);
            }
            DicomTag tag = new DicomTag(tagCode, "", VR.SL, true, false);
            DicomAttribute dicomAttr = DicomAttribute.CreateAttribute(tag);
            for (int i = 0; i < value.Length; i++)
            {
                if (!dicomAttr.SetInt32(i, value[i]))
                    return false;
            }
            _dataHead.AddDicomAttribute(dicomAttr);
            return true;
        }

        public bool SetUInt32(uint tagCode, uint value)
        {
            if (_dataHead.Contains(tagCode))
            {
                _dataHead.RemoveDicomAttribute(tagCode);
            }
            DicomVR TagVR = new DicomVR(VR.UL);
            DicomTag tag = new DicomTag(tagCode);
            tag.TagVR = TagVR;
            DicomAttribute dicomAttr = DicomAttribute.CreateAttribute(tag);
            dicomAttr.SetUInt32(0, value);
            _dataHead.AddDicomAttribute(dicomAttr);
            return true;
        }

        public bool SetUInt32Array(uint tagCode, uint []value)
        {
            if (_dataHead.Contains(tagCode))
            {
                _dataHead.RemoveDicomAttribute(tagCode);
            }
            DicomTag tag = new DicomTag(tagCode, "", VR.UL, true, false);
            DicomAttribute dicomAttr = DicomAttribute.CreateAttribute(tag);
            for (int i = 0; i < value.Length; i++)
            {
                if (!dicomAttr.SetUInt32(i, value[i]))
                    return false;
            }
            _dataHead.AddDicomAttribute(dicomAttr);
            return true;
        }

        public bool SetFloat(uint tagCode, float value)
        {
            if (_dataHead.Contains(tagCode))
            {
                _dataHead.RemoveDicomAttribute(tagCode);
            }
            DicomVR TagVR = new DicomVR(VR.FL);
            DicomTag tag = new DicomTag(tagCode);
            tag.TagVR = TagVR;
            DicomAttribute dicomAttr = DicomAttribute.CreateAttribute(tag);
            dicomAttr.SetFloat32(0, value);
            _dataHead.AddDicomAttribute(dicomAttr);
            return true;
        }

        public bool SetFloat(uint tagCode, float [] value)
        {
            if (_dataHead.Contains(tagCode))
            {
                _dataHead.RemoveDicomAttribute(tagCode);
            }
            DicomTag tag = new DicomTag(tagCode, "", VR.FL, true, false);
            DicomAttribute dicomAttr = DicomAttribute.CreateAttribute(tag);
            for (int i = 0; i < value.Length; i++)
            {
                if (!dicomAttr.SetFloat32(i, value[i]))
                    return false;
            }
            _dataHead.AddDicomAttribute(dicomAttr);
            return true;
        }

        public bool GetString(uint tagCode,out string value)
        {
            return _dataHead[tagCode].GetString(0, out value);
        }

        public bool GetStringArray(uint tagCode, out string[] valueArray)
        {
            if (!_dataHead.Contains(tagCode))
            {
                valueArray = null;
                return false;
            }

            valueArray = new string[_dataHead[tagCode].ValueCount];
            for (int i = 0; i < valueArray.Length; i++)
            {
                if(!_dataHead[tagCode].GetString(i, out valueArray[i]))
                    return false;
            }
            return true;
        }

        public bool GetBytes(uint tagCode, out byte[] value)
        {
            return _dataHead[tagCode].GetBytes(0, out value);
        }

        public bool GetInt16(uint tagCode, out short value)
        {
           return _dataHead[tagCode].GetInt16(0, out value);
        }

        public bool GetInt16Array(uint tagCode,out short[] valueArray)
        {
            if (!_dataHead.Contains(tagCode))
            {
                valueArray = null;
                return false;
            }

            valueArray = new short[_dataHead[tagCode].ValueCount];
            for (int i = 0; i < valueArray.Length; i++)
            {
                if (!_dataHead[tagCode].GetInt16(i, out valueArray[i]))
                    return false;
            }
            return true;
        }

        public bool GetUInt16(uint tagCode,out ushort value)
        {
            return _dataHead[tagCode].GetUInt16(0, out value);
        }

        public bool GetUInt16Array(uint tagCode,out ushort[] valueArray)
        {
            if (!_dataHead.Contains(tagCode))
            {
                valueArray = null;
                return false;
            }

            valueArray = new ushort[_dataHead[tagCode].ValueCount];
            for (int i = 0; i < valueArray.Length; i++)
            {
                if (!_dataHead[tagCode].GetUInt16(i, out valueArray[i]))
                    return false;
            }
            return true;
        }

        public bool GetInt32(uint tagCode,out int value)
        {
            return _dataHead[tagCode].GetInt32(0, out value);
        }

        public bool GetInt32Array(uint tagCode, out int[] valueArray)
        {
            if (!_dataHead.Contains(tagCode))
            {
                valueArray = null;
                return false;
            }

            valueArray = new int[_dataHead[tagCode].ValueCount];
            for (int i = 0; i < valueArray.Length; i++)
            {
                if (!_dataHead[tagCode].GetInt32(i, out valueArray[i]))
                    return false;
            }
            return true;
        }

        public bool GetUInt32(uint tagCode,out uint value)
        {
            return _dataHead[tagCode].GetUInt32(0, out value);
        }

        public bool GetUInt32Array(uint tagCode, out uint[] valueArray)
        {
            if (!_dataHead.Contains(tagCode))
            {
                valueArray = null;
                return false;
            }

            valueArray = new uint[_dataHead[tagCode].ValueCount];
            for (int i = 0; i < valueArray.Length; i++)
            {
                if (!_dataHead[tagCode].GetUInt32(i, out valueArray[i]))
                    return false;
            }
            return true;
        }

        public bool GetFloat32(uint tagCode,out float value)
        {
           return _dataHead[tagCode].GetFloat32(0, out value);
        }

        public bool GetFloat32Array(uint tagCode, out float[] valueArray)
        {
            if (!_dataHead.Contains(tagCode))
            {
                valueArray = null;
                return false;
            }

            valueArray = new float[_dataHead[tagCode].ValueCount];
            for (int i = 0; i < valueArray.Length; i++)
            {
                if (!_dataHead[tagCode].GetFloat32(i, out valueArray[i]))
                    return false;
            }
            return true;
        }
    }
}
