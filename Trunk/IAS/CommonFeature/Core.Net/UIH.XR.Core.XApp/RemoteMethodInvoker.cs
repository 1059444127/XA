using System;
using System.Collections.Generic;
using System.Linq;
/// ------------------------------------------------------------------------------
/// <copyright company="United-Imaging">
/// Copyright (c) United-Imaging. All rights reserved.
/// </copyright>
/// <description>
/// </description>
/// ------------------------------------------------------------------------------
using System.Text;
using UIH.Mcsf.Core;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.ComponentModel.Composition;
using System.Threading;
using UIH.Mcsf.Log;
using UIH.XR.GlobalParameter;

namespace UIH.XR.Core.XApp
{
    [Export(typeof(IRemoteMethodInvoker))]
    [Export(typeof(IRemoteMethodInvokerClient))]
    [Export(typeof(IRemoteMethodInvokerServer))]
    public class RemoteMethodInvoker : IRemoteMethodInvoker
    {
        const int COMMUNICATION_ID = 23;//

        const int WAIT_TIME = 3000;//ms

        private ICommunicationProxy _communicationProxy;

        private Dictionary<string, object> _serviceObjects = new Dictionary<string, object>();

        private ThreadLocal<string> _currentRemoteInvoker = new ThreadLocal<string>();

        [ImportingConstructor]
        public RemoteMethodInvoker(IAppContext appContext)
        {
            //_communicationProxy = appContext.DefaultCLRCommunicationProxy as CLRCommunicationProxy;
            _communicationProxy = appContext.GetObject<ICommunicationProxy>(AppContextObjectName.DefaultCommunicationProxy);
            if (_communicationProxy != null)
            {
                _communicationProxy.RegisterCommandHandlerEx(COMMUNICATION_ID, HandleRemoteInvoke);
            }
        }

        public void RegisterServiceObject<T>(T obj, string name) where T : class
        {
            if (string.IsNullOrEmpty(name)) name = typeof(T).FullName;
            _serviceObjects.Add(name, obj);
        }

        public object RemoteInvoke(string receiver, params object[] parameters)
        {
            StackFrame stackFrame = new StackFrame(1);
            MethodBase method = stackFrame.GetMethod();
            Type[] interfaces = method.DeclaringType.GetInterfaces();
            if (interfaces != null && interfaces.Length > 0)
            {
                Type type = interfaces.FirstOrDefault(t => t.GetMethod(method.Name) != null);
                if (type != null) method = type.GetMethod(method.Name);
            }
            return RemoteInvoke(receiver, method, parameters);
        }

        public object RemoteInvoke(string receiver, MethodBase method, params object[] parameters)
        {
            return RemoteInvoke(receiver, method.DeclaringType.FullName, method, parameters);
        }

        private void HandleRemoteInvoke(CommandContextEx cmdContext)
        {
            CLRLogger.GetInstance().LogDevInfo("Handle remote invoke, sender is " + cmdContext.sSender);
            byte[] request = cmdContext.sSerializeObject;
            _currentRemoteInvoker.Value = cmdContext.sSender;
            BinaryFormatter formatter = new BinaryFormatter();
            RemoteMethodInvokeArgument invokeArgs = null;
            using (MemoryStream inStream = new MemoryStream(request))
            {
                invokeArgs = formatter.Deserialize(inStream) as RemoteMethodInvokeArgument;
            }
            if (invokeArgs == null)
            {
                throw new Exception("Invalid remote argument");
            }
            object obj = NativeInvoke(invokeArgs.ObjectName, invokeArgs.Method, invokeArgs.Parameters);
            using (MemoryStream outStream = new MemoryStream())
            {
                formatter.Serialize(outStream, obj);
                byte[] response = outStream.GetBuffer();
                int errorCode = cmdContext.Reply(response);
                if (errorCode != 0)
                {
                    throw new CommunicationException(errorCode, "fail to reply to " + cmdContext.sSender);
                }
            }
        }

        private object NativeInvoke(string serviceObjectName, MethodBase method, object[] parameters)
        {
            Object obj = null;
            if (!_serviceObjects.ContainsKey(serviceObjectName))
            {
                obj = _serviceObjects.Values.FirstOrDefault(item => item.GetType().GetMethod(method.Name) != null);
                if (obj == null)
                {
                    throw new Exception(serviceObjectName + " is not registered and no service object has method " + method.Name);
                }
                CLRLogger.GetInstance().LogDevWarning(serviceObjectName + " is not reigstered, use " + obj.GetType().ToString() + " instead");
            }
            obj = _serviceObjects[serviceObjectName];
            return method.Invoke(obj, parameters);
        }

        public string GetCurrentRemoteInvoker()
        {
            return _currentRemoteInvoker.Value;
        }

        public object RemoteInvoke(string receiver, string serviceObjectName, MethodBase method, params object[] parameters)
        {
            if (receiver == _communicationProxy.GetName())
            {
                return NativeInvoke(serviceObjectName, method, parameters);
            }
            RemoteMethodInvokeArgument args = new RemoteMethodInvokeArgument(serviceObjectName, method, parameters);
            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream outStream = new MemoryStream())
            {
                CommandContext cmdContext = new CommandContext();
                _communicationProxy.GetName();
                formatter.Serialize(outStream, args);
                cmdContext.iCommandId = COMMUNICATION_ID;
                cmdContext.sReceiver = receiver;
                cmdContext.sSender = _communicationProxy.GetName();
                cmdContext.sSerializeObject = outStream.GetBuffer();
                cmdContext.iWaitTime = WAIT_TIME;
                ISyncResult result = _communicationProxy.SyncSendCommand(cmdContext);
                if (result.GetCallResult() != 0)
                {
                    throw new CommunicationException(result.GetCallResult(), "fail to sync send command to " + receiver);
                }
                using (MemoryStream replyStream = new MemoryStream(result.GetSerializedObject()))
                {
                    return formatter.Deserialize(replyStream);
                }
            }
        }//EndMethod
    }
}
