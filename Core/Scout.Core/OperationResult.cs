using System.Runtime.Serialization;

namespace Scout.Core
{
    [DataContract]
    public enum OperationResult
    {
        [EnumMember]
        Unknown = 0,
        [EnumMember]
        Success = 1,
        [EnumMember]
        Failure = 2,
        [EnumMember]
        Error = 4
    }
}
