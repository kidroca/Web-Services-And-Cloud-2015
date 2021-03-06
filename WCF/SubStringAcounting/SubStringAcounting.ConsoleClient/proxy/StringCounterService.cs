﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------



[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.ServiceContractAttribute(ConfigurationName="IStringCounterService")]
public interface IStringCounterService
{
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStringCounterService/CountSubstringOccurence", ReplyAction="http://tempuri.org/IStringCounterService/CountSubstringOccurenceResponse")]
    int CountSubstringOccurence(string subString, string mainString);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStringCounterService/CountSubstringOccurence", ReplyAction="http://tempuri.org/IStringCounterService/CountSubstringOccurenceResponse")]
    System.Threading.Tasks.Task<int> CountSubstringOccurenceAsync(string subString, string mainString);
}

[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
public interface IStringCounterServiceChannel : IStringCounterService, System.ServiceModel.IClientChannel
{
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
public partial class StringCounterServiceClient : System.ServiceModel.ClientBase<IStringCounterService>, IStringCounterService
{
    
    public StringCounterServiceClient()
    {
    }
    
    public StringCounterServiceClient(string endpointConfigurationName) : 
            base(endpointConfigurationName)
    {
    }
    
    public StringCounterServiceClient(string endpointConfigurationName, string remoteAddress) : 
            base(endpointConfigurationName, remoteAddress)
    {
    }
    
    public StringCounterServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
            base(endpointConfigurationName, remoteAddress)
    {
    }
    
    public StringCounterServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
            base(binding, remoteAddress)
    {
    }
    
    public int CountSubstringOccurence(string subString, string mainString)
    {
        return base.Channel.CountSubstringOccurence(subString, mainString);
    }
    
    public System.Threading.Tasks.Task<int> CountSubstringOccurenceAsync(string subString, string mainString)
    {
        return base.Channel.CountSubstringOccurenceAsync(subString, mainString);
    }
}
