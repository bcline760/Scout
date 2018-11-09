using System;
using System.Runtime.Serialization;

namespace Scout.Core.Contract
{
    [DataContract]
    public partial class ApiResponse<TBody>
    {
        [DataMember(Order = 0)]
        public OperationResult Result { get; set; }

        [DataMember(Order = 1)]
        public string Message { get; set; }

        [DataMember(Order = 2)]
        public TBody ResponseBody { get; set; }
    }
}
