//////////////////////////////////////////////////////////////////////////
/// Copyright, (c) Shanghai United Imaging Healthcare Inc., 2011
/// All rights reserved.
///
/// \author Jinyang Li     jinyang.li@united-imaging.com
//
/// \file XAppIoC.cs
///
/// \brief  This Interface Defines Get Instance From Mef Container,that if the Object is Generate by Mef Container,
/// you can use the Interface,other not
///
/// \version 1
/// \date Apr  , 2017
/////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;

namespace UIH.XR.Core
{
    [Export(typeof(IIoC<CompositionContainer>))]
    public class XAppIoC : IIoC<CompositionContainer>
    {

        #region Implementation of IIoC

        public T Get<T>(Type contractType, string contractName = "")
        {
            var contract = string.IsNullOrEmpty(contractName)
                               ? AttributedModelServices.GetContractName(contractType)
                               : contractName;
            return (T)this.Container.GetExportedValue<T>(contract);
        }

        public Lazy<T> GetLazy<T>(Type contractType, string contractName = "")
        {
            var contract = string.IsNullOrEmpty(contractName)
                    ? AttributedModelServices.GetContractName(contractType)
                    : contractName;
            return this.Container.GetExport<T>(contract);
        }

        #endregion


        #region [----Properties--]

        public CompositionContainer Container { get; set; }

        #endregion

    }
}
