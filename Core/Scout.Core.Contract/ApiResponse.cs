using System;
using System.Runtime.Serialization;

namespace Scout.Core.Contract
{
    public class ApiResponse
    {
        [DataContract]
        public partial class Response<TBody>
        {
            [DataMember(Order = 0)]
            public OperationResult Result { get; set; }

            [DataMember(Order = 1)]
            public string Message { get; set; }

            [DataMember(Order = 2)]
            public TBody ResponseBody { get; set; }
        }
    }
}
