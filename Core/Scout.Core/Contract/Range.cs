using System;
using System.Runtime.Serialization;

namespace Scout.Core.Contract
{
    /// <summary>
    /// A range of values
    /// </summary>
    [DataContract]
    public sealed class Range
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Scout.Core.Contract.Range"/> class.
        /// </summary>
        public Range() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Scout.Core.Contract.Range"/> class.
        /// </summary>
        /// <param name="min">The lower bounds of the range</param>
        /// <param name="max">The upper bounds of the range</param>
        public Range(int min, int max)
        {
            Minimum = min;
            Maximum = max;
        }

        /// <summary>
        /// Get or set the lower bounds of the range
        /// </summary>
        /// <value>The minimum.</value>
        [DataMember]
        public int Minimum { get; set; }
        /// <summary>
        /// Gets or sets the upper bounds of the range.
        /// </summary>
        /// <value>The maximum.</value>
        [DataMember]
        public int Maximum { get; set; }
    }
}
