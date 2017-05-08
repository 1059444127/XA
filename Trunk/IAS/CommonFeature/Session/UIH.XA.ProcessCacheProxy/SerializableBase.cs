using UIH.Mcsf.Pipeline.Dictionary;
using UIH.Mcsf.Pipeline.Data;

namespace UIH.XA.SessionManager
{
    public class SerializableBase
    {
        public DicomAttributeCollection _dataHead { get; private set; }

        public SerializableBase()
        {
            _dataHead = new DicomAttributeCollection();
            CacheStatus(0X00000001,"0");
        }

        public virtual void CacheStatus()
        {
        }

        public virtual void QueryStatus()
        {
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
            }

            DicomVR TagVR = new DicomVR(VR.CS);
            DicomTag tag = new DicomTag(tagCode);
            tag.TagVR = TagVR;
            DicomAttribute dicomAttr = DicomAttribute.CreateAttribute(tag);
            dicomAttr.SetString(0, value);
            _dataHead.AddDicomAttribute(dicomAttr);
        }

        protected bool QueryStatus(uint tagCode,out string value)
        {
            value = null;
            return _dataHead.Contains(tagCode) ? _dataHead[tagCode].GetString(0, out value) : false;
        }

        protected void CacheArrayStatus(uint tagCode, string []value)
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
        }

        protected bool QueryArrayStatus(uint tagCode,out string[] valueArray)
        {
            if (!_dataHead.Contains(tagCode))
            {
                valueArray = null;
                return false;
            }
                
            valueArray = new string[_dataHead[tagCode].ValueCount];
            for (int i = 0; i < _dataHead[tagCode].ValueCount; i++)
            {
                _dataHead[tagCode].GetString(i, out valueArray[i]);
            }
            return true;
        }
    }
}
