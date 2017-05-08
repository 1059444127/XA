using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIH.XA.PAUtilityCSharp.Enumeration;

namespace UIH.XA.PAUtilityCSharp.Commons.CommonConfigRelate
{
    public class ConfigurePath
    {
        public const string PaConfigure = @"config\patientadmin\PA_Configure.xml";
        public const string PrCfg = @"config\patientadmin\PRConfig.xml";
        public const string WorkListCfg = @"config\patientadmin\WorkList_Configure.xml";
        public const string ArchivingimportCfg = @"config\patientadmin\Archiving_import_config.xml";
        public const string ArchivingPacsImportCfg = @"config\patientadmin\Archiving_pacs_import_config.xml";
        public const string ArchivingCfg = @"config\patientadmin\McsfArchivingConfig.xml";
        public const string ArchivingServiceClassCfg = @"config\patientadmin\McsfArchivingServiceClassConfig.xml";
        public const string ExportViewModelForImage = @"config\patientadmin\PA_ExportViewModelForImage.xml";
        public const string ExportViewModelForRtss = @"config\patientadmin\PA_ExportViewModelForRTSS.xml";
        public const string ExportViewModelForReport = @"config\patientadmin\PA_ExportViewModelForReport.xml";
        public const string PaTabItemVisibility = @"data\patientadmin\PATabItemVisibility.xml";
        public const string IdGeneratorParam = @"data\patientadmin\IDGeneratorParam.xml";
        public const string PaDataGridConfigure = @"data\patientadmin\PA_DataGrid_Configure.xml";
        public const string PhysicianOptionConfig = @"data\patientadmin\PhysicianOptionConfig.xml";
        public const string PaDataSourceConfigure = @"data\patientadmin\PA_DataSource_Configure.xml";
        public const string PaConfig = @"data\patientadmin\PAConfig.xml";

        public static Dictionary<PaConfigType, string> ConfigPathDic = new Dictionary<PaConfigType, string>
        {
            {PaConfigType.PaConfigure, PaConfigure},
            {PaConfigType.PrCfg, PrCfg},
            {PaConfigType.WorkListCfg, WorkListCfg},
            {PaConfigType.ArchivingimportCfg, ArchivingimportCfg},
            {PaConfigType.ArchivingPacsImportCfg, ArchivingPacsImportCfg},
            {PaConfigType.ArchivingCfg, ArchivingCfg},
            {PaConfigType.ArchivingServiceClassCfg, ArchivingServiceClassCfg},
            {PaConfigType.ExportViewModelForImage, ExportViewModelForImage},
            {PaConfigType.ExportViewModelForRtss, ExportViewModelForRtss},
            {PaConfigType.ExportViewModelForReport, ExportViewModelForReport},
            {PaConfigType.PaTabItemVisibility, PaTabItemVisibility},
            {PaConfigType.IdGeneratorParam, IdGeneratorParam},
            {PaConfigType.PaDataGridConfigure, PaDataGridConfigure},
            {PaConfigType.PhysicianOptionConfig, PhysicianOptionConfig},
            {PaConfigType.PaDataSourceConfigure, PaDataSourceConfigure},
            {PaConfigType.PaConfig, PaConfig},
        };
    }
}
