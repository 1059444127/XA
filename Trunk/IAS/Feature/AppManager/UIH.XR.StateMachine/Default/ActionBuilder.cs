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
using System.Reflection;

namespace UIH.XR.StateMachine.Default
{
    /// <summary>
    /// Builder of action
    /// </summary>
    public class ActionBuilder
    {
        const int PACKAGE_PART_COUNT = 2;

        private string _namesapce;//namespace contains the action type

        private string _module;//assembly contains the action type

        private Func<IAction, IAction> _initializer;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="packageInfo">packageInfo is combined by namespace and assembly pathes, like "UIH.XR.Test,XTest"</param>
        /// <param name="actionInitializer">the function to initialize action</param>
        public ActionBuilder(string packageInfo, Func<IAction, IAction> actionInitializer)
        {
            if (packageInfo == null) return;
            string[] pkgInfo = packageInfo.Split(',');
            if (pkgInfo.Length >= PACKAGE_PART_COUNT)
            {
                _namesapce = pkgInfo[0];
                _module = pkgInfo[1];
            }
            _initializer = actionInitializer;
        }

        /// <summary>
        /// Build Action by ActionDefinition
        /// </summary>
        /// <param name="def"></param>
        /// <returns></returns>
        public IAction Build(ActionDefinition def)
        {
            string className = def.Class;
            try
            {
                Assembly actionAssembly = null;
                if (_module != null)
                {
                    actionAssembly = Assembly.Load(_module);
                }
                if (_namesapce != null && !className.Contains('.'))
                {
                    className = _namesapce + "." + className;
                }
                if (actionAssembly == null)
                {
                    actionAssembly = Assembly.GetCallingAssembly();
                }
                Type actionType = actionAssembly.GetType(className);
                ConstructorInfo constructor = actionType.GetConstructor(new Type[0]);
                IAction action = constructor.Invoke(new object[0]) as IAction;
                return _initializer(action);
            }
            catch (Exception ex)
            {
                throw new StateMachineException(string.Format("Invalid ActionDefinition {0}", className), ex);
            }
        }
    }
}
