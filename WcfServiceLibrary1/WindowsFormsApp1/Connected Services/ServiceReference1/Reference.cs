﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WindowsFormsApp1.ServiceReference1 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.ITranslationService")]
    public interface ITranslationService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITranslationService/Translate", ReplyAction="http://tempuri.org/ITranslationService/TranslateResponse")]
        string Translate(string value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITranslationService/Translate", ReplyAction="http://tempuri.org/ITranslationService/TranslateResponse")]
        System.Threading.Tasks.Task<string> TranslateAsync(string value);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ITranslationServiceChannel : WindowsFormsApp1.ServiceReference1.ITranslationService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class TranslationServiceClient : System.ServiceModel.ClientBase<WindowsFormsApp1.ServiceReference1.ITranslationService>, WindowsFormsApp1.ServiceReference1.ITranslationService {
        
        public TranslationServiceClient() {
        }
        
        public TranslationServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public TranslationServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TranslationServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TranslationServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string Translate(string value) {
            return base.Channel.Translate(value);
        }
        
        public System.Threading.Tasks.Task<string> TranslateAsync(string value) {
            return base.Channel.TranslateAsync(value);
        }
    }
}
