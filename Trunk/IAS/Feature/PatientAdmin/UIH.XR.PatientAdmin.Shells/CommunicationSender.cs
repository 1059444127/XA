
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIH.Mcsf.Core;
using UIH.Mcsf.Database.Proxy;
using UIH.XA.PAUtilityCSharp.Communication;
using UIH.XA.PAUtilityCSharp.Enumeration;
using UIH.XA.PAUtilityCSharp.Global;


namespace UIH.XA.PatientAdmin.Shells
{
    public class CommunicationSender : ICommunicationMap
    {
        public CommunicationSender()
        {
            AttachCommand();
        }

        #region Events
        public Action<IMcsfDBInfoModal> AsynDbCommandHandler;

        public Action MiniPAServiceDateUpdateHandler;

        public Action PermissionUserLogIn;
        #endregion

        /// <summary>
        /// RegisterCommand
        /// </summary>
        public void RegisterCommand()
        {
            GlobalDefinition.LoggerWrapper.LogTraceInfo("Now we enter RegisterCommand method");
            if (null == ContaineeUtility._feCommProxy)
            {
                GlobalDefinition.LoggerWrapper.LogTraceInfo("param ContaineeUtility._feCommProxy is null");
                return;
            }

            FECommandHandler cmdHandler = null;

            //Register Command
            cmdHandler = new FECommandHandler();
            ContaineeUtility._feCommProxy.RegisterCommandHandler((int)CommandID.COMMAND_DATABASE_EVENT, cmdHandler);

            FEEventHandler eventHandler = null;
            eventHandler = new FEEventHandler();
            ContaineeUtility._feCommProxy.RegisterEventHandler(GlobalDefinition.ServicePageChangedID,
                (int)EventID.SystemCommandEventIDSyncTime, eventHandler);
            ContaineeUtility._feCommProxy.SubscribeEvent(GlobalDefinition.ServicePageChangedID,
                (int)EventID.SystemCommandEventIDSyncTime);
            ContaineeUtility._feEvtHandlerList.Add(eventHandler);

            RegisteredDBNotification();
        }

        private void RegisteredDBNotification()
        {
            //Create DatabaseProxy
            IDatabaseProxy databaseProxy =
                McsfDatabaseProxyFactory.Instance().CreateDatabaseProxy(ContaineeUtility._feCommProxy);

            //setup some register info
            IMcsfDBInfoModal dbInfoModal = new IMcsfDBInfoModal();
            dbInfoModal.EventItems = new List<IMcsfDBInfoModalItem>();

            IMcsfDBInfoModalItem item;

            //-----------------------------------------
            item = new IMcsfDBInfoModalItem();
            item.Type = DBEventType.EventPatientUpdate;
            item.UID = "";
            item.SubType = DBEventSubType.EventAll;
            item.SubUID = "";
            dbInfoModal.EventItems.Add(item);
            //-----------------------------------------
            item = new IMcsfDBInfoModalItem();
            item.Type = DBEventType.EventPatientDelete;
            item.UID = "";
            item.SubType = DBEventSubType.EventAll;
            item.SubUID = "";
            dbInfoModal.EventItems.Add(item);
            //-----------------------------------------
            item = new IMcsfDBInfoModalItem();
            item.Type = DBEventType.EventPatientStudyChange;
            item.UID = "";
            item.SubType = DBEventSubType.EventAll;
            item.SubUID = "";
            dbInfoModal.EventItems.Add(item);
            //-----------------------------------------
            item = new IMcsfDBInfoModalItem();
            item.Type = DBEventType.EventStudyUpdate;
            item.UID = "";
            item.SubType = DBEventSubType.EventAll;
            item.SubUID = "";
            dbInfoModal.EventItems.Add(item);
            //-----------------------------------------
            item = new IMcsfDBInfoModalItem();
            item.Type = DBEventType.EventStudyDelete;
            item.UID = "";
            item.SubType = DBEventSubType.EventAll;
            item.SubUID = "";
            dbInfoModal.EventItems.Add(item);
            //-----------------------------------------
            item = new IMcsfDBInfoModalItem();
            item.Type = DBEventType.EventStudyInsert;
            item.UID = "";
            item.SubType = DBEventSubType.EventAll;
            item.SubUID = "";
            dbInfoModal.EventItems.Add(item);
            //-----------------------------------------
            item = new IMcsfDBInfoModalItem();
            item.Type = DBEventType.EventStudySeriesChange;
            item.UID = "";
            item.SubType = DBEventSubType.EventAll;
            item.SubUID = "";
            dbInfoModal.EventItems.Add(item);
            //-----------------------------------------
            item = new IMcsfDBInfoModalItem();
            item.Type = DBEventType.EventSeriesUpdate;
            item.UID = "";
            item.SubType = DBEventSubType.EventAll;
            item.SubUID = "";
            dbInfoModal.EventItems.Add(item);
            //-----------------------------------------
            item = new IMcsfDBInfoModalItem();
            item.Type = DBEventType.EventSeriesComplete;
            item.UID = "";
            item.SubType = DBEventSubType.EventAll;
            item.SubUID = "";
            dbInfoModal.EventItems.Add(item);
            //-----------------------------------------
            item = new IMcsfDBInfoModalItem();
            item.Type = DBEventType.EventSeriesDelete;
            item.UID = "";
            item.SubType = DBEventSubType.EventAll;
            item.SubUID = "";
            dbInfoModal.EventItems.Add(item);
            //----------------------------------------
            item = new IMcsfDBInfoModalItem();
            item.Type = DBEventType.EventSeriesInsert;
            item.UID = "";
            item.SubType = DBEventSubType.EventAll;
            item.SubUID = "";
            dbInfoModal.EventItems.Add(item);
            //----------------------------------------
            item = new IMcsfDBInfoModalItem
            {
                Type = DBEventType.EventStudyImported,
                UID = "",
                SubType = DBEventSubType.EventAll,
                SubUID = ""
            };
            dbInfoModal.EventItems.Add(item);
            //---------------------------------------
            item = new IMcsfDBInfoModalItem();
            item.Type = DBEventType.EventImageDelete;
            item.UID = "";
            item.SubType = DBEventSubType.EventAll;
            item.SubUID = "";
            dbInfoModal.EventItems.Add(item);
            //---------------------------------------
            item = new IMcsfDBInfoModalItem();
            item.Type = DBEventType.EventStudyWithSubItemsChanged;
            item.UID = "";
            item.SubType = DBEventSubType.EventAll;
            item.SubUID = "";
            dbInfoModal.EventItems.Add(item);
            if (null != databaseProxy)
            {
                GlobalDefinition.LoggerWrapper.LogTraceInfo("param databaseProxy is not null");
                databaseProxy.RegisterEventInfo(dbInfoModal);
            }
            GlobalDefinition.LoggerWrapper.LogTraceInfo("Now we exit RegisteredDBNotification method");
        }

        private void AttachCommand()
        {
            FECommandHandler.AddItemToCommandMap((int)CommandID.COMMAND_DATABASE_EVENT, NoticeEventHandlerFromDatabase);

        }

        private string NoticeEventHandlerFromDatabase(CommandContext param)
        {
            try
            {
                GlobalDefinition.LoggerWrapper.LogDevInfo(
                    "enter function --- CommunicationSender.NoticeEventHandlerFromDatabase(CommandContext param)");

                if (null == param)
                {
                    GlobalDefinition.LoggerWrapper.LogDevWarning("param is null");
                    return string.Empty;
                }

                if (null == param.sSerializeObject)
                {
                    GlobalDefinition.LoggerWrapper.LogDevWarning("param.sSerializeObject is null");
                    return string.Empty;
                }

                IMcsfDBInfoModal dbInfoModel = null;

                var databaseProxy =
                    McsfDatabaseProxyFactory.Instance().CreateDatabaseProxy(ContaineeUtility._feCommProxy);
                dbInfoModel = databaseProxy.ParseFromStringToEventInfo(param.sSerializeObject);

                var handler = AsynDbCommandHandler;
                if (handler != null)
                {
                    handler.BeginInvoke(dbInfoModel, null, null);
                }
            }
            catch (Exception ex)
            {
                GlobalDefinition.LoggerWrapper.LogDevError(ex.Message);
                GlobalDefinition.LoggerWrapper.LogDevError(ex.StackTrace);
            }

            GlobalDefinition.LoggerWrapper.LogDevInfo(
                "exit function --- CommunicationSender.NoticeEventHandlerFromDatabase(CommandContext param)");
            return string.Empty;
        }


    }
}
