using System;

namespace Scout.Core.Bus
{
    public static class BaseballStatisticCalculation
    {
        /// <summary>
        /// Compute the park-adjusted OPS (On-Base Percentage + Slugging Percentage).
        /// This is the OPS statistic adjusted for park factors and normalized. Above 100 indicates better-than-average performance.
        /// Below 100 indicates worse-than-average performance
        /// </summary>
        /// <param name="playerOnbasePct">The player's on-base percentage</param>
        /// <param name="leagueOnbasePct">The league's (NL & AL) on-base percentage</param>
        /// <param name="playerSluggingPct">The player's slugging percentage</param>
        /// <param name="leagueSluggingPct">The league's slugging percentage</param>
        /// <returns>The park-adjusted OPS</returns>
        public static int GetParkAdjustedOps(decimal playerOnbasePct, decimal leagueOnbasePct, decimal playerSluggingPct, decimal leagueSluggingPct)
        {
            int parkAdjOps = 100 * Convert.ToInt32(((playerOnbasePct / leagueOnbasePct) + (playerSluggingPct / leagueSluggingPct)));

            return parkAdjOps;
        }

        /// <summary>
        /// Computer the BABIP (Batting Average of Balls In Play) for a player.
        /// 
        /// This is to determine a player's batting average of balls that are hit in play which excludes strikeouts and home runs.
        /// </summary>
        /// <param name="ab">At bats (used instead of PA because AB removes BB & SH from equation)</param>
        /// <param name="hits">Recorded hits</param>
        /// <param name="hr">Home runs</param>
        /// <param name="so">Strikeouts</param>
        /// <param name="sf">Sacrifice Flies</param>
        /// <returns>The batting average of balls in play</returns>
        public static decimal GetBattingAvgOfBallsInPlay(int ab, int hits, int hr, int so, int sf)
        {
            decimal divisor = Convert.ToDecimal(ab - so - hr + sf);
            decimal babip = 0M;
            if (divisor > 0)
                babip = (hits - hr) / divisor;
            else if (babip < 0)
                throw new InvalidOperationException("BABIP is never supposed to be a negative number.");

            return babip;
        }

        /// <summary>
        /// Get a statistic ratio or quotient from counting statistics such as AB, H, 2B, etc.
        /// </summary>
        /// <param name="stat">The stat or accumulated stats to divide</param>
        /// <param name="div">The stat or accumulated to divide by</param>
        /// <returns>The quotent in decimal form</returns>
        public static decimal GetRatio(int stat, int div)
        {
            decimal ratio = 0M;
            if (div != 0)
            {
                if (stat < 0 || div < 0)
                    throw new InvalidOperationException("Ratios are never supposed to be negative");

                ratio = stat / Convert.ToDecimal(div);
            }
            return ratio;
        }
    }
}
