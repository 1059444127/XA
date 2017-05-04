using System;
using System.Linq;
using System.Xml.Linq;

namespace VieweToolKit.Utility
{
    public static class XElementExtension
    {
        public static string GetAttribute(this XElement element, string attributeName)
        {
            var attributes = element.Attributes();
            var attribute = attributes.FirstOrDefault(a => a.Name.LocalName == attributeName);
            return attribute == null ? string.Empty : attribute.Value;
        }

        public static bool GetBoolAttribute(this XElement element, string attributeName)
        {
            var attribute = element.GetAttribute(attributeName);
            bool boolAttribute = false;
            Boolean.TryParse(attribute, out boolAttribute);
            return boolAttribute;
        }
    }
}