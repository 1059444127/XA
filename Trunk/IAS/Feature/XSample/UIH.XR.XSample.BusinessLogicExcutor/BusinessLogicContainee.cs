using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIH.Mcsf.Core;
using System.Diagnostics;
using UIH.XR.XSample.BusinessLogicInterface;

namespace UIH.XR.XSample.BusinessLogicExcutor
{
    public class BusinessLogicContainee : CLRContaineeBase
    {

        private PatientManager _patientManager;

        public override void DoWork()
        {
            //Debugger.Launch();
            _patientManager = new PatientManager();


            CLRCommunicationProxy proxy = GetCommunicationProxy() as CLRCommunicationProxy;

            proxy.RegisterCommandHandler(90001, new PatientCommandHandler(_patientManager));


            Console.WriteLine("Do work!");
        }

        public override void Startup()
        {
            base.Startup();
            Console.WriteLine("Do work!");
        }
    }


    public class PatientCommandHandler : ICLRCommandHandler
    {
        PatientManager _patientManager;

        public PatientCommandHandler(PatientManager patientManager)
        {
            _patientManager = patientManager;
        }


        private static object lockObj = new object();

        public override int HandleCommand(CommandContext context, ISyncResult result)
        {
            //return base.HandleCommand(context, result);

            lock (lockObj)
            {

            Console.WriteLine("HandleCommand");

            PatientRemoteObject rObject = SerializeObj.Desrialize<PatientRemoteObject>(context.sSerializeObject);

            Console.WriteLine("InvokeTag:\t" + rObject.InvokeTag);

            switch (rObject.InvokeTag)
            {
                case "NewPatient":
                    _patientManager.NewPatient(rObject.Paramters.First() as Patient);
                    result.SetSerializedObject(SerializeObj.Serialize(true));
                    break;
                case "DeletePatient":
                    _patientManager.DeletePatient((ulong)(rObject.Paramters.First()));
                    result.SetSerializedObject(SerializeObj.Serialize(true));
                    break;
                case "ModifyPatient":
                    _patientManager.ModifyPatient(rObject.Paramters.First() as Patient);
                    result.SetSerializedObject(SerializeObj.Serialize(true));
                    break;
                case "GetPatients":
                    IList<Patient> patientList = _patientManager.GetPatients();
                    result.SetSerializedObject(SerializeObj.Serialize<IList<Patient>>(patientList));
                    break;
                default:
                    break;
            }

            }

            return 0;


        }

    }
}
