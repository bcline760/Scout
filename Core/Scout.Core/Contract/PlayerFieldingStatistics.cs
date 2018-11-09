using System;
namespace Scout.Core.Contract
{
    public class PlayerFieldingStatistics
    {
        public PlayerFieldingStatistics()
        {
        }

        public short Year { get; set; }
        public short Stint { get; set; }
        public string Position { get; set; }
        public short Games { get; set; }
        public short GamesStarted { get; set; }
        public short InningOuts { get; set; }
        public short PutOuts { get; set; }
        public short Assists { get; set; }
        public short Errors { get; set; }
        public short DoublePlays { get; set; }
        public short WildPitches { get; set; }
        public short StolenBases { get; set; }
        public short CaughtStealing { get; set; }
        public short ZoneRating { get; set; }
    }
}
