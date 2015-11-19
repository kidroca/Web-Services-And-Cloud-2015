namespace TheDaysInBulgaria.WcfService
{
    using System;
    using System.ServiceModel;

    [ServiceContract]
    public interface IBulgarianDayService
    {
        [OperationContract]
        string GetDayName(DateTime date);
    }
}
