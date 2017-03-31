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

namespace UIH.XR.Core
{
    public class CommunicationException : Exception
    {
        public CommunicationException() { }

        public CommunicationException(int errorCode) 
        {
            _errorCode = errorCode;
        }

        public CommunicationException(int errorCode, string msg) : base(msg) 
        {
            _errorCode = errorCode;
        }

        public CommunicationException(int errorCode, string msg, Exception innerException) : base(msg, innerException) 
        {
            _errorCode = errorCode;
        }

        public int ErrorCode { get { return _errorCode; } }

        private int _errorCode;
    }
}
