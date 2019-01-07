using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

using Scout.Core.Contract;
using Scout.Core.Service;
using Scout.Core.Repository;
using Scout.Core.Bus;

namespace Scout.Service
{
    public class PlayerService : ScoutService<Player>, IScoutService<Player>
    {
        private IPlayerRepository _player = null;

        public PlayerService(IPlayerRepository player) : base(player)
        {
            _player = player;
        }

        public async Task<List<Player>> FindPlayers(PlayerSearchRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            List<Player> players = new List<Player>();
            if (!string.IsNullOrEmpty(request.PlayerName))
            {
                var playerEntities = await _player.FindPlayersByNameAsync(request.PlayerName);
                players.AddRange(playerEntities);
            }
            else if (!string.IsNullOrEmpty(request.PlayerCode))
            {
                var ply = await _player.GetPlayerAsync(request.PlayerCode);
                if (ply != null)
                    players.Add(ply);
            }

            foreach (var player in players)
            {
                //Calculate advanced batting metrics
                player.AdvancedBattingStatistics = player.BattingStatistics.Select(a => new PlayerAdvancedBattingStatistics
                {
                    PlayerIdentifier = a.PlayerIdentifier,
                    TeamIdentifier = a.TeamIdentifier,
                    TeamName = a.TeamName,
                    TeamYear = a.TeamYear,
                    BattingAverage = a.BattingAverage,
                    BattingAvgOfBallsInPlay = BaseballStatisticCalculation.GetBattingAvgOfBallsInPlay(a.AtBats, a.Hits, a.Homeruns, a.Strikeouts, a.SacrificeFlies),
                    ExtraBaseHitPercentage = BaseballStatisticCalculation.GetRatio(a.Doubles + a.Triples + a.Homeruns, a.PlateAppearances),
                    HomeRunPercentage = BaseballStatisticCalculation.GetRatio(a.Homeruns, a.PlateAppearances),
                    InPlayPercentage = BaseballStatisticCalculation.GetRatio(a.AtBats - a.Strikeouts - a.Homeruns - a.SacrificeFlies, a.PlateAppearances),
                    OnBasePercentage = BaseballStatisticCalculation.GetRatio(a.Hits + a.Walks + a.HitByPitch, a.AtBats + a.Walks + a.HitByPitch + a.SacrificeFlies),
                    SluggingPercentage = BaseballStatisticCalculation.GetRatio(a.Hits + (2 * a.Doubles) + (3 * a.Triples) + (4 * a.Homeruns), a.AtBats),
                    StrikeoutPercentage = BaseballStatisticCalculation.GetRatio(a.Strikeouts, a.PlateAppearances),
                    WalkPercentage = BaseballStatisticCalculation.GetRatio(a.Walks, a.PlateAppearances)

                }).ToList();
            }
            return players;
        }

        public override async Task<IEnumerable<Player>> GetAllAsync()
        {
            var players = await _player.LoadAllAsync();
            foreach (var player in players)
            {
                //Calculate advanced batting metrics
                player.AdvancedBattingStatistics = player.BattingStatistics.Select(a => new PlayerAdvancedBattingStatistics
                {
                    PlayerIdentifier = a.PlayerIdentifier,
                    TeamIdentifier = a.TeamIdentifier,
                    TeamName = a.TeamName,
                    TeamYear = a.TeamYear,
                    BattingAverage = a.BattingAverage,
                    BattingAvgOfBallsInPlay = BaseballStatisticCalculation.GetBattingAvgOfBallsInPlay(a.AtBats, a.Hits, a.Homeruns, a.Strikeouts, a.SacrificeFlies),
                    ExtraBaseHitPercentage = BaseballStatisticCalculation.GetRatio(a.Doubles + a.Triples + a.Homeruns, a.PlateAppearances),
                    HomeRunPercentage = BaseballStatisticCalculation.GetRatio(a.Homeruns, a.PlateAppearances),
                    InPlayPercentage = BaseballStatisticCalculation.GetRatio(a.AtBats - a.Strikeouts - a.Homeruns - a.SacrificeFlies, a.PlateAppearances),
                    OnBasePercentage = BaseballStatisticCalculation.GetRatio(a.Hits + a.Walks + a.HitByPitch, a.AtBats + a.Walks + a.HitByPitch + a.SacrificeFlies),
                    SluggingPercentage = BaseballStatisticCalculation.GetRatio(a.Hits + (2 * a.Doubles) + (3 * a.Triples) + (4 * a.Homeruns), a.AtBats),
                    StrikeoutPercentage = BaseballStatisticCalculation.GetRatio(a.Strikeouts, a.PlateAppearances),
                    WalkPercentage = BaseballStatisticCalculation.GetRatio(a.Walks, a.PlateAppearances)

                }).ToList();
            }

            return players;
        }

        public override async Task<Player> GetAsync(Guid id)
        {
            var player = await _player.GetAsync(id);

            if (player != null)
            {
                //Calculate advanced batting metrics
                player.AdvancedBattingStatistics = player.BattingStatistics.Select(a => new PlayerAdvancedBattingStatistics
                {
                    PlayerIdentifier = a.PlayerIdentifier,
                    TeamIdentifier = a.TeamIdentifier,
                    TeamName = a.TeamName,
                    TeamYear = a.TeamYear,
                    BattingAverage = a.BattingAverage,
                    BattingAvgOfBallsInPlay = BaseballStatisticCalculation.GetBattingAvgOfBallsInPlay(a.AtBats, a.Hits, a.Homeruns, a.Strikeouts, a.SacrificeFlies),
                    ExtraBaseHitPercentage = BaseballStatisticCalculation.GetRatio(a.Doubles + a.Triples + a.Homeruns, a.PlateAppearances),
                    HomeRunPercentage = BaseballStatisticCalculation.GetRatio(a.Homeruns, a.PlateAppearances),
                    InPlayPercentage = BaseballStatisticCalculation.GetRatio(a.AtBats - a.Strikeouts - a.Homeruns - a.SacrificeFlies, a.PlateAppearances),
                    OnBasePercentage = BaseballStatisticCalculation.GetRatio(a.Hits + a.Walks + a.HitByPitch, a.AtBats + a.Walks + a.HitByPitch + a.SacrificeFlies),
                    SluggingPercentage = BaseballStatisticCalculation.GetRatio(a.Hits + (2 * a.Doubles) + (3 * a.Triples) + (4 * a.Homeruns), a.AtBats),
                    StrikeoutPercentage = BaseballStatisticCalculation.GetRatio(a.Strikeouts, a.PlateAppearances),
                    WalkPercentage = BaseballStatisticCalculation.GetRatio(a.Walks, a.PlateAppearances)

                }).ToList();
            }
            return player;
        }
    }
}
