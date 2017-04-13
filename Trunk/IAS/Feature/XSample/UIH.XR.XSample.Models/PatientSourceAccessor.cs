using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIH.XR.XSample.BusinessLogicInterface;
using System.ComponentModel.Composition;
using UIH.XR.Core;
using UIH.Mcsf.Core;

namespace UIH.XR.XSample.Models
{
    [Export(typeof(IPatientSource))]
    public class PatientSourceAccessor : IPatientSource
    {

        private static readonly string BusinessLogicProxyName="BusinessLogic@@";
        private static readonly int BusinessLogicCommand=90001;

        IAppContext _appContext;

        [ImportingConstructor]
        public PatientSourceAccessor([Import(typeof(IAppContext))]IAppContext appContext)
        {
            _appContext = appContext;
        }


        public bool NewPatient(Patient patient)
        {
            byte[] buffer = SendSyncToPatientManager("NewPatient", patient);
            return SerializeObj.Desrialize<bool>(buffer);
        }

        public bool DeletePatient(ulong patientID)
        {
            byte[] buffer = SendSyncToPatientManager("DeletePatient", patientID);
            return SerializeObj.Desrialize<bool>(buffer);
        }

        public bool ModifyPatient(Patient patient)
        {
            byte[] buffer = SendSyncToPatientManager("ModifyPatient", patient);
            return SerializeObj.Desrialize<bool>(buffer);
        }


        public IList<Patient> GetPatients()
        {
            byte[] buffer = SendSyncToPatientManager("GetPatients");
            return SerializeObj.Desrialize<IList<Patient>>(buffer);
        }

        /// <summary>
        /// For local only
        /// </summary>
        //private UIH.XR.XSample.BusinessLogicExcutor.PatientManager TestPatientSource = new BusinessLogicExcutor.PatientManager();



        private byte[] SendSyncToPatientManager(string method, params object[] args)
        {
            CommandContext context = new CommandContext();
                context.iCommandId = BusinessLogicCommand;
                //context.sSender 
                context.sReceiver = BusinessLogicProxyName;
                context.bServiceAsyncDispatch = true;

            PatientRemoteObject rObject=new PatientRemoteObject();
            rObject.InvokeTag=method;
            rObject.Paramters=args;

                context.sSerializeObject = SerializeObj.Serialize(rObject);
                context.iWaitTime = 30000;


            CLRCommunicationProxy proxy=_appContext.DefaultCLRCommunicationProxy as CLRCommunicationProxy;

             ISyncResult result=   proxy.SyncSendCommand(context);

           return  result.GetSerializedObject();

        }





    }
}
