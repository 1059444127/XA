using UIH.Mcsf.Pipeline.Dictionary;
using UIH.Mcsf.Pipeline.Data;
using System;

namespace UIH.XA.SessionManager
{
    public class SerializableBase
    {
        public DicomAttributeCollection _dataHead { get; private set; }

        public SerializableBase()
        {
            _dataHead = new DicomAttributeCollection();
        }
        public virtual void CacheStatus()
        {
        }

        public virtual string QueryStatus()
        {
            return "";
        }

        public void SetImageHead(DicomAttributeCollection dataHead)
        {
            _dataHead = dataHead;
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

        protected string QueryStringStatus(uint tagCode)
        {
            try
            {
                string value = null;
                if (_dataHead.Contains(tagCode))
                {

                    _dataHead[tagCode].GetString(0, out value);
                }
                else
                {
                    Console.WriteLine("QueryStringStatus not contains tag:" + tagCode);
                }
                return value;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            
        }
    }
}
