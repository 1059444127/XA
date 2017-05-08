using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using UIH.XA.PAUtilityCSharp.Commons;
using UIH.XA.PAUtilityCSharp.Commons.CommonConfigRelate;
using UIH.XA.PAUtilityCSharp.Enumeration;
using UIH.XA.PAUtilityCSharp.Global;

namespace UIH.XA.PAUtilityCSharp.Models.ConfigRelate
{
    [XmlRoot("PhysicianOptionConfig")]
    public sealed class UserDataPhysicianOption
    {
        [XmlElement]
        public SerializableDictionary<string, PhysicianOption> PhysicianOptions { get; set; }

        /// <summary>
        /// Load
        /// </summary>
        /// <returns>PhysicianOptionConfig</returns>
        public static UserDataPhysicianOption Load()
        {
            GlobalDefinition.LoggerWrapper.LogTraceInfo("Enter UserDataPhysicianOption.Load");
            try
            {
                return ConfigManager.Load<UserDataPhysicianOption>(PaConfigType.PhysicianOptionConfig);
            }
            catch (Exception ex)
            {
                GlobalDefinition.LoggerWrapper.LogDevError("Parse UserDataPhysicianOption fail： " + ex.Message);
                GlobalDefinition.LoggerWrapper.LogDevError(ex.StackTrace);
            }
            GlobalDefinition.LoggerWrapper.LogTraceInfo("Exit UserDataPhysicianOption.Load");
            return new UserDataPhysicianOption { PhysicianOptions = new SerializableDictionary<string, PhysicianOption>() };
        }

        /// <summary>
        /// 构建可选医生列表的可序列化字典
        /// </summary>
        /// <param name="key">医生类型</param>
        /// <param name="physicianName">已选中的医生</param>
        /// <param name="physicianOption">可选医生列表</param>
        /// <param name="dict">可选医生列表的可序列化字典</param>
        private void ConstructPhysicianOptionDictionary(
            string key,
            string physicianName,
            ObservableCollection<string> physicianOption,
            ref SerializableDictionary<string, PhysicianOption> dict)
        {
            GlobalDefinition.LoggerWrapper.LogTraceInfo("Enter function ConstructPhysicianOptionDictionary");
            ObservableCollection<string> names = new ObservableCollection<string>();
            if (!string.IsNullOrEmpty(physicianName))
            {
                names.Add(physicianName);
                int i = 0;
                if (physicianOption.Contains(physicianName))
                {
                    foreach (string name in physicianOption)
                    {
                        if (!name.Equals(physicianName))
                        {
                            names.Add(name);
                            i++;
                            if (i == 4)
                            {
                                break;
                            }
                        }
                    }
                }
                else
                {
                    foreach (string name in physicianOption)
                    {
                        names.Add(name);
                        i++;
                        if (i == 4)
                        {
                            break;
                        }
                    }
                }
            }
            else
            {
                foreach (var item in physicianOption)
                {
                    names.Add(item);
                }
            }
            dict.Add(key, new PhysicianOption { Name = names });
            GlobalDefinition.LoggerWrapper.LogTraceInfo("Exit function ConstructPhysicianOptionDictionary");
        }

        private void ConstructOperatorPhysicianOptionDictionary(
            string key,
            string physicianName,
            List<string> physicianOption,
            ref SerializableDictionary<string, PhysicianOption> dict)
        {
            ObservableCollection<string> names = new ObservableCollection<string>();
            names.Add(physicianName);
            int i = 0;
            if (physicianOption.Contains(physicianName))
            {
                foreach (string name in physicianOption)
                {
                    if (!name.Equals(physicianName) && !string.IsNullOrEmpty(name))
                    {
                        names.Add(name);
                        i++;
                        if (i == 4)
                        {
                            break;
                        }
                    }
                }
            }
            else
            {
                foreach (string name in physicianOption)
                {
                    if (!string.IsNullOrEmpty(name))
                    {
                        names.Add(name);
                        i++;
                        if (i == 4)
                        {
                            break;
                        }
                    }
                }
            }
            dict.Add(key, new PhysicianOption { Name = names });
            GlobalDefinition.LoggerWrapper.LogTraceInfo("Exit function ConstructPhysicianOptionDictionary");
        }

        /// <summary>
        /// 界面上删除医生后重构可选医生列表的可序列化字典
        /// </summary>
        /// <param name="key">医生类型</param>
        /// <param name="physicianName">要删除的医生</param>
        /// <param name="dict">可选医生列表的可序列化字典</param>
        private void ConstructPhysicianOptionDictionaryByRemove(
            string key,
            string physicianName,
            ref SerializableDictionary<string, PhysicianOption> dict)
        {
            GlobalDefinition.LoggerWrapper.LogTraceInfo("Enter function ConstructPhysicianOptionDictionaryByRemove");
            ObservableCollection<string> toBeUpdatedList = dict[key].Name;
            if (toBeUpdatedList.Count != 0)
            {
                toBeUpdatedList.Remove(physicianName);
            }
            dict[key].Name = toBeUpdatedList;
            GlobalDefinition.LoggerWrapper.LogTraceInfo("Exit function ConstructPhysicianOptionDictionaryByRemove");
        }

        /// <summary>
        /// 判断可选医生列表是否发生了变化，如果未选择医生或选中的医生为列表中的第一项，则表示医生选项未变化，否则相反
        /// </summary>
        /// <param name="physicianName">已选中的医生</param>
        /// <param name="physicianOption">可选医生列表</param>
        /// <returns></returns>
        private bool IsPhysicianOptionDictionaryChanged(string physicianName,
            ObservableCollection<string> physicianOption)
        {
            GlobalDefinition.LoggerWrapper.LogTraceInfo("Enter function IsPhysicianOptionDictionaryChanged");
            if (string.IsNullOrEmpty(physicianName)
                || (physicianOption.Count > 0 && physicianName.Equals(physicianOption[0]))
                )
            {
                GlobalDefinition.LoggerWrapper.LogTraceInfo("Exit----the PhysicianOptionDictionary is not Changed");
                return false;
            }
            else
            {
                GlobalDefinition.LoggerWrapper.LogTraceInfo("Exit----the PhysicianOptionDictionary is Changed");
                return true;
            }
        }

        private bool IsOperatorPhysicianOptionDictionaryChanged(string physicianName, List<string> physicianOption)
        {
            GlobalDefinition.LoggerWrapper.LogTraceInfo("Enter function IsPhysicianOptionDictionaryChanged");
            if ((physicianOption.Count > 0 && physicianName.Equals(physicianOption[0]))
                )
            {
                GlobalDefinition.LoggerWrapper.LogTraceInfo("Exit----the PhysicianOptionDictionary is not Changed");
                return false;
            }
            else
            {
                GlobalDefinition.LoggerWrapper.LogTraceInfo("Exit----the PhysicianOptionDictionary is Changed");
                return true;
            }
        }

        /// <summary>
        /// 根据physicianType key读取可选医生列表
        /// </summary>
        /// <param name="physicianType">医生类型</param>
        /// <returns></returns>
        private ObservableCollection<string> PreparePhysicianNames(string physicianType)
        {
            GlobalDefinition.LoggerWrapper.LogTraceInfo("Enter function PreparePhysicianNames");
            if (this.PhysicianOptions.ContainsKey(physicianType))
            {
                var option = this.PhysicianOptions[physicianType];
                if (option != null)
                {
                    GlobalDefinition.LoggerWrapper.LogTraceInfo(
                        "Exit function PreparePhysicianNames----Parameter option is not null");
                    return option.Name;
                }
            }
            GlobalDefinition.LoggerWrapper.LogTraceInfo("Exit function PreparePhysicianNames");
            return new ObservableCollection<string>();
        }

        /// <summary>
        /// 更新PhysicianOptionConfig配置文件
        /// Mini PR里的医生配置不做同步更新，原因如下：
        /// 医生配置的目的是让使用最频繁的医生显示在医生列表中。Mini PR中修改已完成的检查时，那时所选则的医生不一定是使用最频繁的医生。
        /// 所有不需要覆盖掉医生列表。
        /// </summary>
        /// <param name="study">检查信息</param>
        /// <returns></returns>
        public bool Update(StudyInfoModel study)
        {
            GlobalDefinition.LoggerWrapper.LogTraceInfo("Enter function Update");
            if (study == null)
            {
                GlobalDefinition.LoggerWrapper.LogTraceInfo(
                    "Exit function Update----Parameter study is null return false");
                return false;
            }
            try
            {
                var operatorNames = PreparePhysicianNames("OperatorNames");
                var performingPhysicianNames = PreparePhysicianNames("PerformingPhysicianNames");
                var referencePhysicianNames = PreparePhysicianNames("ReferencePhysicianNames");
                var requestingPhysicianNames = PreparePhysicianNames("RequestingPhysicianNames");

                var isChanged = IsPhysicianOptionDictionaryChanged(study.OperatorName, operatorNames)
                                |
                                IsPhysicianOptionDictionaryChanged(study.PerformingPhysician, performingPhysicianNames)
                                |
                                IsPhysicianOptionDictionaryChanged(study.ReferringPhysicianName, referencePhysicianNames)
                                |
                                IsPhysicianOptionDictionaryChanged(study.RequestingPhysician, requestingPhysicianNames);
                if (isChanged)
                {
                    var physicianOptions =
                        new SerializableDictionary<string, PhysicianOption>();
                    ConstructPhysicianOptionDictionary("OperatorNames", study.OperatorName, operatorNames,
                        ref physicianOptions);
                    ConstructPhysicianOptionDictionary("PerformingPhysicianNames", study.PerformingPhysician,
                        performingPhysicianNames, ref physicianOptions);
                    ConstructPhysicianOptionDictionary("ReferencePhysicianNames", study.ReferringPhysicianName,
                        referencePhysicianNames, ref physicianOptions);
                    ConstructPhysicianOptionDictionary("RequestingPhysicianNames", study.RequestingPhysician,
                        requestingPhysicianNames, ref physicianOptions);
                    this.PhysicianOptions = physicianOptions;
                    ConfigManager.Save(PaConfigType.PhysicianOptionConfig, this);
                    GlobalDefinition.LoggerWrapper.LogTraceInfo("Exit function Update----return true");
                    return true;
                }
            }
            catch (Exception ex)
            {
                GlobalDefinition.LoggerWrapper.LogDevError(ex.Message);
                GlobalDefinition.LoggerWrapper.LogDevError(ex.StackTrace);
            }
            GlobalDefinition.LoggerWrapper.LogTraceInfo("Exit function Update----return false");
            return false;
        }

        /// <summary>
        /// RemovePhysician
        /// </summary>
        /// <param name="physicianName">string</param>
        /// <param name="physicianListName">string</param>
        public void RemovePhysician(string physicianName, string physicianListName)
        {
            GlobalDefinition.LoggerWrapper.LogTraceInfo("Enter function RemovePhysician");
            if (physicianName == null || physicianListName == null)
            {
                GlobalDefinition.LoggerWrapper.LogTraceInfo("Parameter physicianName or physicianListName is null");
                return;
            }

            try
            {
                var physicianOptions = this.PhysicianOptions;
                ConstructPhysicianOptionDictionaryByRemove(physicianListName, physicianName,
                    ref physicianOptions);
                ConfigManager.Save(PaConfigType.PhysicianOptionConfig, this);
            }
            catch (Exception ex)
            {
                GlobalDefinition.LoggerWrapper.LogDevError(ex.Message);
                GlobalDefinition.LoggerWrapper.LogDevError(ex.StackTrace);
            }
            
        }
    }

    public class PhysicianOption
    {
        [XmlElement]
        public ObservableCollection<string> Name { get; set; }
    }
}
