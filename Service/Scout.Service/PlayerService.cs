using Scout.Service.Contract;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

using Scout.Core.Contract;
using Scout.Model.Contract;
using Scout.Model.DB.Repository;
using DB = Scout.Model.DB;
using Scout.Core.Bus;

namespace Scout.Service
{
    public class PlayerService : IPlayerService
    {
        private IPlayerRepository _player = null;
        private IPlayerBattingRepository _batting = null;
        private IPlayerPitchingRepository _pitching = null;
        private ITeamRepository _teamRepo = null;

        public PlayerService(IPlayerRepository player,
            IPlayerBattingRepository batting,
            IPlayerPitchingRepository pitching,
            ITeamRepository teamRepository)
        {
            _player = player;
            _batting = batting;
            _pitching = pitching;
            _teamRepo = teamRepository;
        }

        public async Task<ObjectModifyResult<int>> CreatePlayer(Player player)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));

            DB.Player playerEntity = MapPlayerEntity(player);
            int playerId = -1;
            int recordsModified = 0;

            ObjectModifyResult<int> result = new ObjectModifyResult<int>();
            playerId = await _player.CreatePlayer(playerEntity);

            if (playerId > 0)
            {
                result.PrimaryIdentifier = playerId;
                recordsModified++;

                if (player.BattingStatistics.Count > 0)
                {
                    foreach (var battingStat in player.BattingStatistics)
                    {
                        var battingResult = await AddStatistics(battingStat, null);
                        recordsModified += battingResult.RecordsModified;
                    }
                }

                if (player.PitchingStatistics.Count > 0)
                {
                    foreach (var pitching in player.PitchingStatistics)
                    {
                        var pitchingResult = await AddStatistics(null, pitching);
                        recordsModified += pitchingResult.RecordsModified;
                    }
                }
            }

            result.RecordsModified = recordsModified;

            return result;
        }

        public async Task<List<Player>> FindPlayers(PlayerSearchRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            List<Player> players = new List<Player>();
            if (!string.IsNullOrEmpty(request.PlayerName))
            {
                var playerEntities = await _player.FindPlayersByName(request.PlayerName);
                players.AddRange(playerEntities.Select(MapPlayerData).ToList());
            }
            else if (!string.IsNullOrEmpty(request.PlayerCode))
            {
                var ply = await _player.GetPlayer(request.PlayerCode);
                if (ply != null)
                    players.Add(MapPlayerData(ply));
            }

            foreach (var player in players)
            {
                var battingStats = await _batting.GetStatistics(player.PlayerId);
                var pitchingStats = await _pitching.GetStatistics(player.PlayerId);

                //Calculate advanced batting metrics
                player.AdvancedBattingStatistics = battingStats.Select(a => new PlayerAdvancedBattingStatistics
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

        public async Task<ObjectModifyResult<int>> UpdatePlayer(Player player)
        {
            DB.Player dbPlayer = MapPlayerEntity(player);
            int recordsModified = await _player.UpdatePlayer(dbPlayer);
            ObjectModifyResult<int> result = new ObjectModifyResult<int>
            {
                PrimaryIdentifier = player.PlayerId,
                RecordsModified = recordsModified
            };

            return result;
        }

        private async Task<ObjectModifyResult<int>> AddStatistics(PlayerBattingStatistics batting, PlayerPitchingStatistics pitching)
        {
            if (batting == null && pitching == null)
                throw new ArgumentException("Both pitching and batting statistics cannot be null");

            ObjectModifyResult<int> result = new ObjectModifyResult<int>();
            int totalRecords = 0;
            
            if (batting != null)
            {
                var team = await _teamRepo.GetTeamByYearCode(batting.TeamYear, batting.TeamIdentifier);
                var entity = MapBattingStatEntity(batting);

                entity.TeamId = team.TeamId;
                int recordsModified = await _batting.CreatePlayerBattingStatistics(entity);
                totalRecords += recordsModified;
            }

            if (pitching != null)
            {
                var team = await _teamRepo.GetTeamByYearCode(pitching.TeamYear, pitching.TeamIdentifier);
                var entity = MapPitchingStatEntity(pitching);
                entity.TeamId = team.TeamId;

                int recordsModified = await _pitching.CreatePlayerPitchingStatistics(entity);
                totalRecords += recordsModified;
            }

            result.RecordsModified = totalRecords;
            return result;
        }

        private Player MapPlayerData(DB.Player p)
        {
            Player pData = new Player
            {
                Bats = p.Bats,
                BirthCity = p.BirthCity,
                BirthCountry = p.BirthCountry,
                Birthdate = p.Birthdate,
                BirthStateProvince = p.BirthStateProvince,
                DeathCity = p.DeathCity,
                DeathCountry = p.DeathCountry,
                DeathDate = p.DeathDate,
                DeathStateProvince = p.DeathStateProvince,
                FirstName = p.FirstName,
                Height = p.Height,
                LastName = p.LastName,
                MajorLeagueDebut = p.MajorLeagueDebut,
                PlayerId = p.PlayerId,
                PlayerIdentifier = p.PlayerIdentifier,
                RetrosheetId = p.RetrosheetId,
                Throws = p.Throws,
                Weight = p.Weight
            };

            return pData;
        }

        private PlayerBattingStatistics MapBattingStatistics(DB.PlayerBattingStatisticsView battingStats)
        {
            return new PlayerBattingStatistics
            {
                AtBats = battingStats.AtBats,
                BattingAverage = battingStats.BattingAverage,
                CaughtStealing = battingStats.CaughtStealing,
                Doubles = battingStats.Doubles,
                GroundedIntoDoublePlay = battingStats.GroundedIntoDoublePlay,
                HitByPitch = battingStats.HitByPitch,
                Hits = battingStats.Hits,
                Homeruns = battingStats.Homeruns,
                IntentionalWalks = battingStats.IntentionalWalks,
                OnBasePercentage = battingStats.OnBasePercentage,
                OnBasePlusSlugging = battingStats.OnBasePlusSlugging,
                OnBasePlusSluggingAdj = battingStats.OnBasePlusSluggingAdj,
                PlateAppearances = battingStats.PlateAppearances,
                PlayerIdentifier = battingStats.PlayerIdentifier,
                RunsBattedIn = battingStats.RunsBattedIn,
                SacrificeFlies = battingStats.SacrificeFlies,
                SacrificeHits = battingStats.SacrificeHits,
                SluggingPercentage = battingStats.SluggingPercentage,
                StolenBases = battingStats.StolenBases,
                Strikeouts = battingStats.Strikeouts,
                TeamIdentifier = battingStats.TeamIdentifier,
                TeamName = battingStats.TeamName,
                TeamYear = battingStats.TeamYear,
                Triples = battingStats.Triples,
                Walks = battingStats.Walks,
            };
        }

        private PlayerPitchingStatistics MapPitchingStatistics(DB.PlayerPitchingStatisticsView pitchingStats)
        {
            return new PlayerPitchingStatistics
            {
                Balks = pitchingStats.Balks,
                CompleteGames = pitchingStats.CompleteGames,
                EarnedRunAverage = pitchingStats.EarnedRunAverage,
                EarnedRuns = pitchingStats.EarnedRuns,
                GamesLost = pitchingStats.GamesLost,
                GamesPlayed = pitchingStats.GamesPlayed,
                GamesSaved = pitchingStats.GamesSaved,
                GamesStarted = pitchingStats.GamesStarted,
                GamesWon = pitchingStats.GamesWon,
                HitBatsmen = pitchingStats.HitBatsmen,
                Hits = pitchingStats.Hits,
                Homeruns = pitchingStats.Homeruns,
                InningsPitched = pitchingStats.InningsPitched,
                IntentionalWalks = pitchingStats.IntentionalWalks,
                PitchingStint = pitchingStats.PitchingStint,
                PlayerIdentifier = pitchingStats.PlayerIdentifier,
                Runs = pitchingStats.Runs,
                Shutouts = pitchingStats.Shutouts,
                Strikeouts = pitchingStats.Strikeouts,
                TeamIdentifier = pitchingStats.TeamIdentifier,
                TeamName = pitchingStats.TeamName,
                TeamYear = pitchingStats.TeamYear,
                TimesInducedGidp = pitchingStats.TimesInducedGidp,
                Walks = pitchingStats.Walks,
                WildPitches = pitchingStats.WildPitches
            };
        }

        private DB.PlayerPitchingStatistics MapPitchingStatEntity(PlayerPitchingStatistics pitching)
        {
            return new DB.PlayerPitchingStatistics
            {
                Balks = pitching.Balks,
                CompleteGames = pitching.CompleteGames,
                EarnedRunAverage = pitching.EarnedRunAverage,
                EarnedRuns = pitching.EarnedRuns,
                GamesLost = pitching.GamesLost,
                GamesPlayed = pitching.GamesPlayed,
                GamesSaved = pitching.GamesSaved,
                GamesStarted = pitching.GamesStarted,
                GamesWon = pitching.GamesWon,
                HitBatsmen = pitching.HitBatsmen,
                Hits = pitching.Hits,
                Homeruns = pitching.Homeruns,
                InningsPitched = pitching.InningsPitched,
                IntentionalWalks = pitching.IntentionalWalks,
                PitchingStint = pitching.PitchingStint,
                Runs = pitching.Runs,
                Shutouts = pitching.Shutouts,
                Strikeouts = pitching.Strikeouts,
                TimesInducedGidp = pitching.TimesInducedGidp,
                Walks = pitching.Walks,
                WildPitches = pitching.WildPitches
            };
        }

        private DB.PlayerBattingStatistics MapBattingStatEntity(PlayerBattingStatistics battingStat)
        {
            return new DB.PlayerBattingStatistics
            {
                AtBats = battingStat.AtBats,
                BattingAverage = battingStat.BattingAverage,
                CaughtStealing = battingStat.CaughtStealing,
                Doubles = battingStat.Doubles,
                GroundedIntoDoublePlay = battingStat.GroundedIntoDoublePlay,
                HitByPitch = battingStat.HitByPitch,
                Hits = battingStat.Hits,
                Homeruns = battingStat.Homeruns,
                IntentionalWalks = battingStat.IntentionalWalks,
                OnBasePercentage = battingStat.OnBasePercentage,
                OnBasePlusSlugging = battingStat.OnBasePlusSlugging,
                OnBasePlusSluggingAdj = battingStat.OnBasePlusSluggingAdj,
                PlateAppearances = battingStat.PlateAppearances,
                RunsBattedIn = battingStat.RunsBattedIn,
                SacrificeFlies = battingStat.SacrificeFlies,
                SacrificeHits = battingStat.SacrificeHits,
                SluggingPercentage = battingStat.SluggingPercentage,
                StolenBases = battingStat.StolenBases,
                Strikeouts = battingStat.Strikeouts,
                Triples = battingStat.Triples,
                Walks = battingStat.Walks
            };
        }

        private DB.Player MapPlayerEntity(Player player)
        {
            return new DB.Player
            {
                Bats = player.Bats,
                BirthCity = player.BirthCity,
                BirthCountry = player.BirthCountry,
                Birthdate = player.Birthdate,
                BirthStateProvince = player.BirthStateProvince,
                DeathCity = player.DeathCity,
                DeathCountry = player.DeathCountry,
                DeathDate = player.DeathDate,
                DeathStateProvince = player.DeathStateProvince,
                FirstName = player.FirstName,
                Height = player.Height,
                LastName = player.LastName,
                MajorLeagueDebut = player.MajorLeagueDebut,
                PlayerId = player.PlayerId,
                PlayerIdentifier = player.PlayerIdentifier,
                RetrosheetId = player.RetrosheetId,
                Throws = player.Throws,
                Weight = player.Weight
            };
        }
    }
}
