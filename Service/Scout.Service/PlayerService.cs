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
    public class PlayerService : IPlayerService
    {
        private IPlayerRepository _player = null;
        private ITeamRepository _teamRepo = null;

        public PlayerService(IPlayerRepository player,
            ITeamRepository teamRepository)
        {
            _player = player;
            _teamRepo = teamRepository;
        }

        public async Task<ObjectModifyResult<Guid>> CreatePlayer(Player player)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));

            int playerId = -1;
            int recordsModified = 0;

            var result = new ObjectModifyResult<Guid>();
            playerId = await _player.SaveAsync(player);

            result.RecordsModified = recordsModified;

            return result;
        }

        public async Task<List<PlayerListItem>> RetrievePlayers()
        {
            var players = await _player.LoadAllAsync();

            var playerList = players.Select(s => new PlayerListItem
            {
                PlayerCode = s.PlayerIdentifier,
                FirstName = s.FirstName,
                LastName = s.LastName,
                PlayerRetrosheetId = s.RetrosheetId
            }).ToList();

            return playerList;
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

        public async Task<ObjectModifyResult<Guid>> UpdatePlayer(Player player)
        {
            int recordsModified = await _player.SaveAsync(player);
            var result = new ObjectModifyResult<Guid>
            {
                PrimaryIdentifier = player.Id,
                RecordsModified = recordsModified
            };

            return result;
        }
    }
}
