using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Scout.Core.Contract
{
    [DataContract]
    public class PlayerListItem
    {
        [DataMember]
        public string PlayerCode { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string PlayerRetrosheetId { get; set; }
    }
}
