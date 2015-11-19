namespace SubStringAcounting.WcfServiceLibrary
{
    using System.ServiceModel;

    [ServiceContract]
    public interface IStringCounterService
    {
        [OperationContract]
        int CountSubstringOccurence(string subString, string mainString);
    }
}
