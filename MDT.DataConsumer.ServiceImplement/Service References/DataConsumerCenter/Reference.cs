﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.296
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace MDT.DataConsumer.ServiceImplement.DataConsumerCenter {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Source", Namespace="http://schemas.datacontract.org/2004/07/MDT.ManageCenter.DataContract")]
    [System.SerializableAttribute()]
    public partial class Source : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private MDT.DataConsumer.ServiceImplement.DataConsumerCenter.TaskUnit[] MainTasksField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private MDT.DataConsumer.ServiceImplement.DataConsumerCenter.TaskUnit[] PostTasksField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private MDT.DataConsumer.ServiceImplement.DataConsumerCenter.Result[] ResultsField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public MDT.DataConsumer.ServiceImplement.DataConsumerCenter.TaskUnit[] MainTasks {
            get {
                return this.MainTasksField;
            }
            set {
                if ((object.ReferenceEquals(this.MainTasksField, value) != true)) {
                    this.MainTasksField = value;
                    this.RaisePropertyChanged("MainTasks");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public MDT.DataConsumer.ServiceImplement.DataConsumerCenter.TaskUnit[] PostTasks {
            get {
                return this.PostTasksField;
            }
            set {
                if ((object.ReferenceEquals(this.PostTasksField, value) != true)) {
                    this.PostTasksField = value;
                    this.RaisePropertyChanged("PostTasks");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public MDT.DataConsumer.ServiceImplement.DataConsumerCenter.Result[] Results {
            get {
                return this.ResultsField;
            }
            set {
                if ((object.ReferenceEquals(this.ResultsField, value) != true)) {
                    this.ResultsField = value;
                    this.RaisePropertyChanged("Results");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="TaskUnit", Namespace="http://schemas.datacontract.org/2004/07/MDT.ManageCenter.DataContract")]
    [System.SerializableAttribute()]
    public partial class TaskUnit : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private MDT.DataConsumer.ServiceImplement.DataConsumerCenter.ECommand[] CommandsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool HasTraceLogField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool HasTransactionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public MDT.DataConsumer.ServiceImplement.DataConsumerCenter.ECommand[] Commands {
            get {
                return this.CommandsField;
            }
            set {
                if ((object.ReferenceEquals(this.CommandsField, value) != true)) {
                    this.CommandsField = value;
                    this.RaisePropertyChanged("Commands");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool HasTraceLog {
            get {
                return this.HasTraceLogField;
            }
            set {
                if ((this.HasTraceLogField.Equals(value) != true)) {
                    this.HasTraceLogField = value;
                    this.RaisePropertyChanged("HasTraceLog");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool HasTransaction {
            get {
                return this.HasTransactionField;
            }
            set {
                if ((this.HasTransactionField.Equals(value) != true)) {
                    this.HasTransactionField = value;
                    this.RaisePropertyChanged("HasTransaction");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Result", Namespace="http://schemas.datacontract.org/2004/07/MDT.ManageCenter.DataContract")]
    [System.SerializableAttribute()]
    public partial class Result : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CommandNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string XmlPathField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ValueFromFieldField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SubXmlPathField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PrimaryKeyField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CommandName {
            get {
                return this.CommandNameField;
            }
            set {
                if ((object.ReferenceEquals(this.CommandNameField, value) != true)) {
                    this.CommandNameField = value;
                    this.RaisePropertyChanged("CommandName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string XmlPath {
            get {
                return this.XmlPathField;
            }
            set {
                if ((object.ReferenceEquals(this.XmlPathField, value) != true)) {
                    this.XmlPathField = value;
                    this.RaisePropertyChanged("XmlPath");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=2)]
        public string ValueFromField {
            get {
                return this.ValueFromFieldField;
            }
            set {
                if ((object.ReferenceEquals(this.ValueFromFieldField, value) != true)) {
                    this.ValueFromFieldField = value;
                    this.RaisePropertyChanged("ValueFromField");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=3)]
        public string SubXmlPath {
            get {
                return this.SubXmlPathField;
            }
            set {
                if ((object.ReferenceEquals(this.SubXmlPathField, value) != true)) {
                    this.SubXmlPathField = value;
                    this.RaisePropertyChanged("SubXmlPath");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=4)]
        public string PrimaryKey {
            get {
                return this.PrimaryKeyField;
            }
            set {
                if ((object.ReferenceEquals(this.PrimaryKeyField, value) != true)) {
                    this.PrimaryKeyField = value;
                    this.RaisePropertyChanged("PrimaryKey");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ECommand", Namespace="http://schemas.datacontract.org/2004/07/MDT.ManageCenter.DataContract")]
    [System.SerializableAttribute()]
    public partial class ECommand : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CommandNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CommandTextField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private MDT.DataConsumer.ServiceImplement.DataConsumerCenter.ECommandType CommandTypeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool HasResultField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ParameterValueField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ParameterValueFromField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ParameterValueOjbectNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private MDT.DataConsumer.ServiceImplement.DataConsumerCenter.EParameter[] ParametersField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SourceLinkField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private MDT.DataConsumer.ServiceImplement.DataConsumerCenter.SourceType SourceTypeField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CommandName {
            get {
                return this.CommandNameField;
            }
            set {
                if ((object.ReferenceEquals(this.CommandNameField, value) != true)) {
                    this.CommandNameField = value;
                    this.RaisePropertyChanged("CommandName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CommandText {
            get {
                return this.CommandTextField;
            }
            set {
                if ((object.ReferenceEquals(this.CommandTextField, value) != true)) {
                    this.CommandTextField = value;
                    this.RaisePropertyChanged("CommandText");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public MDT.DataConsumer.ServiceImplement.DataConsumerCenter.ECommandType CommandType {
            get {
                return this.CommandTypeField;
            }
            set {
                if ((this.CommandTypeField.Equals(value) != true)) {
                    this.CommandTypeField = value;
                    this.RaisePropertyChanged("CommandType");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool HasResult {
            get {
                return this.HasResultField;
            }
            set {
                if ((this.HasResultField.Equals(value) != true)) {
                    this.HasResultField = value;
                    this.RaisePropertyChanged("HasResult");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ParameterValue {
            get {
                return this.ParameterValueField;
            }
            set {
                if ((object.ReferenceEquals(this.ParameterValueField, value) != true)) {
                    this.ParameterValueField = value;
                    this.RaisePropertyChanged("ParameterValue");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ParameterValueFrom {
            get {
                return this.ParameterValueFromField;
            }
            set {
                if ((object.ReferenceEquals(this.ParameterValueFromField, value) != true)) {
                    this.ParameterValueFromField = value;
                    this.RaisePropertyChanged("ParameterValueFrom");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ParameterValueOjbectName {
            get {
                return this.ParameterValueOjbectNameField;
            }
            set {
                if ((object.ReferenceEquals(this.ParameterValueOjbectNameField, value) != true)) {
                    this.ParameterValueOjbectNameField = value;
                    this.RaisePropertyChanged("ParameterValueOjbectName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public MDT.DataConsumer.ServiceImplement.DataConsumerCenter.EParameter[] Parameters {
            get {
                return this.ParametersField;
            }
            set {
                if ((object.ReferenceEquals(this.ParametersField, value) != true)) {
                    this.ParametersField = value;
                    this.RaisePropertyChanged("Parameters");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string SourceLink {
            get {
                return this.SourceLinkField;
            }
            set {
                if ((object.ReferenceEquals(this.SourceLinkField, value) != true)) {
                    this.SourceLinkField = value;
                    this.RaisePropertyChanged("SourceLink");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public MDT.DataConsumer.ServiceImplement.DataConsumerCenter.SourceType SourceType {
            get {
                return this.SourceTypeField;
            }
            set {
                if ((this.SourceTypeField.Equals(value) != true)) {
                    this.SourceTypeField = value;
                    this.RaisePropertyChanged("SourceType");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ECommandType", Namespace="http://schemas.datacontract.org/2004/07/MDT.ManageCenter.DataContract")]
    public enum ECommandType : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Text = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        TableDirect = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        StoredProcedure = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Get = 3,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Post = 4,
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="EParameter", Namespace="http://schemas.datacontract.org/2004/07/MDT.ManageCenter.DataContract")]
    [System.SerializableAttribute()]
    public partial class EParameter : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TypeField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Type {
            get {
                return this.TypeField;
            }
            set {
                if ((object.ReferenceEquals(this.TypeField, value) != true)) {
                    this.TypeField = value;
                    this.RaisePropertyChanged("Type");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="SourceType", Namespace="http://schemas.datacontract.org/2004/07/MDT.ManageCenter.DataContract")]
    public enum SourceType : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        SqlServer = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Oracle = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        MySql = 3,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        DB2 = 4,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Sybase = 5,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Http = 6,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Tcp = 7,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        DLL = 8,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="DataConsumerCenter.IDataConsumerCenterService")]
    public interface IDataConsumerCenterService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataConsumerCenterService/GetSourceInfo", ReplyAction="http://tempuri.org/IDataConsumerCenterService/GetSourceInfoResponse")]
        MDT.DataConsumer.ServiceImplement.DataConsumerCenter.Source GetSourceInfo(int taskId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataConsumerCenterService/GetSchema", ReplyAction="http://tempuri.org/IDataConsumerCenterService/GetSchemaResponse")]
        string GetSchema(int taskId);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IDataConsumerCenterServiceChannel : MDT.DataConsumer.ServiceImplement.DataConsumerCenter.IDataConsumerCenterService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class DataConsumerCenterServiceClient : System.ServiceModel.ClientBase<MDT.DataConsumer.ServiceImplement.DataConsumerCenter.IDataConsumerCenterService>, MDT.DataConsumer.ServiceImplement.DataConsumerCenter.IDataConsumerCenterService {
        
        public DataConsumerCenterServiceClient() {
        }
        
        public DataConsumerCenterServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public DataConsumerCenterServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DataConsumerCenterServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DataConsumerCenterServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public MDT.DataConsumer.ServiceImplement.DataConsumerCenter.Source GetSourceInfo(int taskId) {
            return base.Channel.GetSourceInfo(taskId);
        }
        
        public string GetSchema(int taskId) {
            return base.Channel.GetSchema(taskId);
        }
    }
}