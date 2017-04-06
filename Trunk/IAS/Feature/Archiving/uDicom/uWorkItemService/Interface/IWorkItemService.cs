using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using uDicom.WorkQueue.Service.Common;

namespace uDicom.WorkQueue.Service.Interface
{
    public static class McsfNamespace
    {
        public const string Value = "http://www.united-imaging.com/MCSF";
    }

    public static class McsfWorkQueueNamespace
    {
        public const string Value = McsfNamespace.Value + "/WorkQueue";
    }

    [DataContract(Namespace = McsfWorkQueueNamespace.Value)]
    public class WorkItemInsertRequest : DataContractBase
    {
        [DataMember]
        public WorkItemRequest Request { get; set; }

        [DataMember]
        public WorkItemProgress Progress { get; set; }
    }

    [DataContract(Namespace = McsfWorkQueueNamespace.Value)]
    public class WorkItemInsertResponse
    {
        [DataMember]
        public WorkItemData Item { get; set; }
    }

    [DataContract(Namespace = McsfWorkQueueNamespace.Value)]
    public class WorkItemUpdateRequest
    {
        [DataMember(IsRequired = true)]
        public long Identifier { get; set; }

        [DataMember]
        public WorkItemPriorityEnum? Priority { get; set; }

        [DataMember]
        public WorkItemStatusEnum? Status { get; set; }

        [DataMember]
        public DateTime? ProcessTime { get; set; }

        [DataMember]
        public DateTime? ExpirationTime { get; set; }

        [DataMember]
        public bool? Cancel { get; set; }

        [DataMember]
        public bool? Pause { get; set; }

        [DataMember]
        public bool? Delete { get; set; }

    }

    [DataContract(Namespace = McsfWorkQueueNamespace.Value)]
    public class WorkItemUpdateResponse 
    {
        [DataMember]
        public WorkItemData Item { get; set; }
    }

    [DataContract(Namespace = McsfWorkQueueNamespace.Value)]
    public class WorkItemQueryRequest 
    {
        [DataMember]
        public string Type { get; set; }

        [DataMember]
        public WorkItemStatusEnum? Status { get; set; }

        [DataMember]
        public string StudyInstanceUid { get; set; }

        [DataMember]
        public long? Identifier { get; set; }
    }

    [DataContract(Namespace = McsfWorkQueueNamespace.Value)]
    public class WorkItemQueryResponse 
    {
        [DataMember]
        public IEnumerable<WorkItemData> Items { get; set; }
    }

    [DataContract(Namespace = McsfWorkQueueNamespace.Value)]
    public class WorkItemSubscribeRequest : DataContractBase
    {
    }

    [DataContract(Namespace = McsfWorkQueueNamespace.Value)]
    public class WorkItemSubscribeResponse : DataContractBase
    {
    }

    [DataContract(Namespace = McsfWorkQueueNamespace.Value)]
    public class WorkItemUnsubscribeRequest : DataContractBase
    {
    }

    [DataContract(Namespace = McsfWorkQueueNamespace.Value)]
    public class WorkItemUnsubscribeResponse : DataContractBase
    {
    }

    [DataContract(Namespace = McsfWorkQueueNamespace.Value)]
    public class WorkItemRefreshRequest : DataContractBase
    {
    }

    [DataContract(Namespace = McsfWorkQueueNamespace.Value)]
    public class WorkItemRefreshResponse : DataContractBase
    {
    }

    [DataContract(Namespace = McsfWorkQueueNamespace.Value)]
    public class WorkItemPublishRequest : DataContractBase
    {
        // TODO (CR Jun 2012): We can only publish changes to single items, but the callback accepts an array?
        [DataMember]
        public WorkItemData Item { get; set; }
    }

    [DataContract(Namespace = McsfWorkQueueNamespace.Value)]
    public class WorkItemPublishResponse : DataContractBase
    {
    }

    [ServiceContract(SessionMode = SessionMode.Required,
                        CallbackContract = typeof(IWorkItemActivityCallback),
                        ConfigurationName = "IWorkItemActivityMonitorService",
                        Namespace = McsfWorkQueueNamespace.Value)]
    [ServiceKnownType("GetKnownTypes", typeof(WorkItemRequestTypeProvider))]
    public interface IWorkItemActivityMonitorService
    {
        [OperationContract]
        WorkItemSubscribeResponse Subscribe(WorkItemSubscribeRequest request);

        [OperationContract]
        WorkItemUnsubscribeResponse Unsubscribe(WorkItemUnsubscribeRequest request);

        [OperationContract(IsOneWay = true)]
        void Refresh(WorkItemRefreshRequest request);

        // TODO this should be renamed "PublishWorkedItemChanged". Still wish we could get rid of it.
        [OperationContract]
        WorkItemPublishResponse Publish(WorkItemPublishRequest request);
    }

    /// <summary>
    /// Service for the creation, manipulation, and monitoring of WorkItems.
    /// </summary>
    [ServiceContract(SessionMode = SessionMode.Required,
        ConfigurationName = "IWorkItemService",
        Namespace = McsfWorkQueueNamespace.Value)]
    [ServiceKnownType("GetKnownTypes", typeof(WorkItemRequestTypeProvider))]
    public interface IWorkItemService
    {
        [OperationContract]
        WorkItemInsertResponse Insert(WorkItemInsertRequest request);

        [OperationContract]
        WorkItemUpdateResponse Update(WorkItemUpdateRequest request);

        [OperationContract]
        WorkItemQueryResponse Query(WorkItemQueryRequest request);
    }
}
