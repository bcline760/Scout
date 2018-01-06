﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Scout.Model.Contract
{
    public abstract class PlayerStatistics
    {
        [DataMember]
        public string PlayerIdentifier { get; set; }
        [DataMember]
        public string TeamIdentifier { get; set; }
        [DataMember]
        public string TeamName { get; set; }
        [DataMember]
        public short TeamYear { get; set; }
    }
}