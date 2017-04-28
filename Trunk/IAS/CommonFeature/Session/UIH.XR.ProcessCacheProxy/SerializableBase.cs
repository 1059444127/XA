using UIH.Mcsf.Pipeline.Dictionary;
using UIH.Mcsf.Pipeline.Data;

namespace UIH.XR.SessionManager
{
    public class SerializableBase
    {
        private DicomAttributeCollection _dataHead = new DicomAttributeCollection();

        public virtual void CacheStatus()
        {
        }

        public virtual void QueryStatus()
        {
        }

        /// <summary>
        /// 将集成自SerializableBase的类中的datahead merge到参数dataHead中，提供给proxy
        /// </summary>
        /// <param name="dataHead"></param>
        public void SetImageHead(DicomAttributeCollection dataHead)
        {
            foreach (DicomAttribute dicomAttr in _dataHead)
            {
                if (dataHead.Contains(dicomAttr.Tag))
                {
                    string value = null;
                    dicomAttr.GetString(0, out value);
                    dataHead[dicomAttr.Tag.Value].SetString(0, value);
                }
                else
                {
                    dataHead.AddDicomAttribute(dicomAttr);
                }
            }
        }

        protected void CacheStatus(uint tagCode, string value)
        {
            if (_dataHead.Contains(tagCode))
            {
                _dataHead.RemoveDicomAttribute(tagCode);
                // _dataHead[tagCode].SetString(0,value);如何申请tag，那么就不需要remove操作，直接对tag进行赋值
            }
            DicomVR TagVR = new DicomVR(VR.CS);
            DicomTag tag = new DicomTag(tagCode);
            tag.TagVR = TagVR;
            DicomAttribute dicomAttr = DicomAttribute.CreateAttribute(tag);
            dicomAttr.SetString(0, value);
            _dataHead.AddDicomAttribute(dicomAttr);
        }

        //protected void CacheStatus(uint tagCode, ushort value)
        //{
        //    if (_dataHead.Contains(tagCode))
        //    {
        //        _dataHead.RemoveDicomAttribute(tagCode);
        //    }
        //    DicomVR TagVR = new DicomVR(VR.CS);
        //    DicomTag tag = new DicomTag(tagCode);
        //    tag.TagVR = TagVR;
        //    DicomAttribute dicomAttr = DicomAttribute.CreateAttribute(tag);
        //    dicomAttr.SetUInt16(0, value);
        //    _dataHead.AddDicomAttribute(dicomAttr);
        //}

        //protected void CacheStatus(uint tagCode, short value)
        //{
        //    if (_dataHead.Contains(tagCode))
        //    {
        //        _dataHead.RemoveDicomAttribute(tagCode);
        //    }
        //    DicomVR TagVR = new DicomVR(VR.CS);
        //    DicomTag tag = new DicomTag(tagCode);
        //    tag.TagVR = TagVR;
        //    DicomAttribute dicomAttr = DicomAttribute.CreateAttribute(tag);
        //    dicomAttr.SetInt16(0, value);
        //    _dataHead.AddDicomAttribute(dicomAttr);
        //}

        //protected void CacheStatus(uint tagCode, uint value)
        //{
        //    if (_dataHead.Contains(tagCode))
        //    {
        //        _dataHead.RemoveDicomAttribute(tagCode);
        //    }
        //    DicomVR TagVR = new DicomVR(VR.CS);
        //    DicomTag tag = new DicomTag(tagCode);
        //    tag.TagVR = TagVR;
        //    DicomAttribute dicomAttr = DicomAttribute.CreateAttribute(tag);
        //    dicomAttr.SetUInt32(0, value);
        //    _dataHead.AddDicomAttribute(dicomAttr);
        //}

        //protected void CacheStatus(uint tagCode, int value)
        //{
        //    if (_dataHead.Contains(tagCode))
        //    {
        //        _dataHead.RemoveDicomAttribute(tagCode);
        //    }
        //    DicomVR TagVR = new DicomVR(VR.CS);
        //    DicomTag tag = new DicomTag(tagCode);
        //    tag.TagVR = TagVR;
        //    DicomAttribute dicomAttr = DicomAttribute.CreateAttribute(tag);
        //    dicomAttr.SetInt32(0, value);
        //    _dataHead.AddDicomAttribute(dicomAttr);
        //}

        //protected void CacheStatus(uint tagCode, ulong value)
        //{
        //    if (_dataHead.Contains(tagCode))
        //    {
        //        _dataHead.RemoveDicomAttribute(tagCode);
        //    }
        //    DicomVR TagVR = new DicomVR(VR.CS);
        //    DicomTag tag = new DicomTag(tagCode);
        //    tag.TagVR = TagVR;
        //    DicomAttribute dicomAttr = DicomAttribute.CreateAttribute(tag);
        //    dicomAttr.SetUInt64(0, value);
        //    _dataHead.AddDicomAttribute(dicomAttr);
        //}

        //protected void CacheStatus(uint tagCode, long value)
        //{
        //    if (_dataHead.Contains(tagCode))
        //    {
        //        _dataHead.RemoveDicomAttribute(tagCode);
        //    }
        //    DicomVR TagVR = new DicomVR(VR.CS);
        //    DicomTag tag = new DicomTag(tagCode);
        //    tag.TagVR = TagVR;
        //    DicomAttribute dicomAttr = DicomAttribute.CreateAttribute(tag);
        //    dicomAttr.SetInt64(0, value);
        //    _dataHead.AddDicomAttribute(dicomAttr);
        //}

        //protected void CacheStatus(uint tagCode, float value)
        //{
        //    if (_dataHead.Contains(tagCode))
        //    {
        //        _dataHead.RemoveDicomAttribute(tagCode);
        //    }
        //    DicomVR TagVR = new DicomVR(VR.CS);
        //    DicomTag tag = new DicomTag(tagCode);
        //    tag.TagVR = TagVR;
        //    DicomAttribute dicomAttr = DicomAttribute.CreateAttribute(tag);
        //    dicomAttr.SetFloat32(0, value);
        //    _dataHead.AddDicomAttribute(dicomAttr);
        //}

        //protected void CacheStatus(uint tagCode, float[] value)
        //{
        //    if (_dataHead.Contains(tagCode))
        //    {
        //        _dataHead.RemoveDicomAttribute(tagCode);
        //    }
        //    DicomVR TagVR = new DicomVR(VR.CS);
        //    DicomTag tag = new DicomTag(tagCode);
        //    tag.TagVR = TagVR;
        //    DicomAttribute dicomAttr = DicomAttribute.CreateAttribute(tag);
        //    dicomAttr.SetFloat32S(0, value);
        //    _dataHead.AddDicomAttribute(dicomAttr);
        //}

        //protected void CacheStatus(uint tagCode, double value)
        //{
        //    if (_dataHead.Contains(tagCode))
        //    {
        //        _dataHead.RemoveDicomAttribute(tagCode);
        //    }
        //    DicomVR TagVR = new DicomVR(VR.CS);
        //    DicomTag tag = new DicomTag(tagCode);
        //    tag.TagVR = TagVR;
        //    DicomAttribute dicomAttr = DicomAttribute.CreateAttribute(tag);
        //    dicomAttr.SetFloat64(0, value);
        //    _dataHead.AddDicomAttribute(dicomAttr);
        //}

        //protected void CacheStatus(uint tagCode, byte[] value)
        //{
        //    if (_dataHead.Contains(tagCode))
        //    {
        //        _dataHead.RemoveDicomAttribute(tagCode);
        //    }
        //    DicomVR TagVR = new DicomVR(VR.CS);
        //    DicomTag tag = new DicomTag(tagCode);
        //    tag.TagVR = TagVR;
        //    DicomAttribute dicomAttr = DicomAttribute.CreateAttribute(tag);
        //    dicomAttr.SetBytes(0, value);
        //    _dataHead.AddDicomAttribute(dicomAttr);
        //}

        protected string QueryStringStatus(uint tagCode)
        {
            string value = null;
            if (_dataHead.Contains(tagCode))
            {
                _dataHead[tagCode].GetString(0, out value);
            }
            return value;
        }

        //protected ushort QueryUInt16Status(uint tagCode)
        //{
        //    ushort value = 65535;
        //    if (_dataHead.Contains(tagCode))
        //    {
        //        _dataHead[tagCode].GetUInt16(0, out value);
        //    }
        //    return value;
        //}

        //protected uint QueryUInt32Status(uint tagCode)
        //{
        //    uint value = 4294967295;
        //    if (_dataHead.Contains(tagCode))
        //    {
        //        _dataHead[tagCode].GetUInt32(0, out value);
        //    }
        //    return value;
        //}

        //protected ulong QueryUInt64Status(uint tagCode)
        //{
        //    ulong value = 18446744073709551615;
        //    if (_dataHead.Contains(tagCode))
        //    {
        //        _dataHead[tagCode].GetUInt64(0, out value);
        //    }
        //    return value;
        //}

        //protected long QueryInt64Status(uint tagCode)
        //{
        //    long value = -9223372036854775808;
        //    if (_dataHead.Contains(tagCode))
        //    {
        //        _dataHead[tagCode].GetInt64(0, out value);
        //    }
        //    return value;
        //}

        //protected int QueryInt32Status(uint tagCode)
        //{
        //    int value = -2147483648;
        //    if (_dataHead.Contains(tagCode))
        //    {
        //        _dataHead[tagCode].GetInt32(0, out value);
        //    }
        //    return value;
        //}

        //protected short QueryInt16Status(uint tagCode)
        //{
        //    short value = -32768;
        //    if (_dataHead.Contains(tagCode))
        //    {
        //        _dataHead[tagCode].GetInt16(0, out value);
        //    }
        //    return value;
        //}

        //protected double QueryFloat64Status(uint tagCode)
        //{
        //    double value =0;
        //    if (_dataHead.Contains(tagCode))
        //    {
        //        _dataHead[tagCode].GetFloat64(0, out value);
        //    }
        //    return value;
        //}

        //protected float QueryFloat32Status(uint tagCode)
        //{
        //    float value = 0;
        //    if (_dataHead.Contains(tagCode))
        //    {
        //        _dataHead[tagCode].GetFloat32(0, out value);
        //    }
        //    return value;
        //}

        //protected float[] QueryFloat32SStatus(uint tagCode)
        //{
        //    float[] value ={0};
        //    if (_dataHead.Contains(tagCode))
        //    {
        //        _dataHead[tagCode].GetFloat32S(0, out value);
        //    }
        //    return value;
        //}

        //protected byte[] QueryBytesStatus(uint tagCode)
        //{
        //    byte[] value ={0};
        //    if (_dataHead.Contains(tagCode))
        //    {
        //        _dataHead[tagCode].GetBytes(0, out value);
        //    }
        //    return value;
        //}
    }
}
