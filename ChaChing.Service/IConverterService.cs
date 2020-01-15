using System.Runtime.Serialization;
using System.ServiceModel;

namespace ChaChing.Service
{
    [ServiceContract]
    public interface IConverterService
    {
        [OperationContract]
        string NumberToEnglish(string input);
    }
}
