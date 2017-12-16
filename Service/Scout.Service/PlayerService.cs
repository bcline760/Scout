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
        private IPlayerRepository _repo = null;

        public PlayerService(IPlayerRepository repository)
        {
            _repo = repository;
        }

        public async Task<ObjectModifyResult<int>> AddStatistics(PlayerBattingStatistics batting, PlayerPitchingStatistics pitching)
        {
            ObjectModifyResult<int> result = new ObjectModifyResult<int>();
            int totalRecords = 0;
            if (batting != null)
            {
                var entity = MapBattingStatEntity(batting);

                int recordsModified = await _repo.CreatePlayerBattingStatistics(entity);
                totalRecords += recordsModified;
            }

            if (pitching != null)
            {
                var entity = MapPitchingStatEntity(pitching);

                int recordsModified = await _repo.CreatePlayerPitchingStatistics(entity);
                totalRecords += recordsModified;
            }

            result.RecordsModified = totalRecords;
            return result;
        }

        public async Task<ObjectModifyResult<int>> CreatePlayer(Player player)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));

            DB.Player playerEntity = MapPlayerEntity(player);
            int playerId = -1;

            ObjectModifyResult<int> result = new ObjectModifyResult<int>();
            playerId = await _repo.CreatePlayer(playerEntity);

            if (playerId > 0)
            {
                result.PrimaryIdentifier = playerId;

                if (player.BattingStatistics.Count > 0)
                {
                    DB.PlayerBattingStatistics pbsEntity = null;
                    foreach (var battingStat in player.BattingStatistics)
                    {
                        pbsEntity = MapBattingStatEntity(battingStat);
                        playerEntity.PlayerBattingStatistics.Add(pbsEntity);
                    }
                }

                if (player.PitchingStatistics.Count > 0)
                {
                    DB.PlayerPitchingStatistics ppsEntity = null;
                    foreach (var pitching in player.PitchingStatistics)
                    {
                        ppsEntity = MapPitchingStatEntity(pitching);
                        
                        playerEntity.PlayerPitchingStatistics.Add(ppsEntity);
                    }
                }
            }

            return result;
        }

        public async Task<List<Player>> FindPlayers(PlayerSearchRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            List<Player> players = new List<Player>();
            if (!string.IsNullOrEmpty(request.PlayerName))
            {
                var playerEntities = await _repo.FindPlayersByName(request.PlayerName);
                players.AddRange(playerEntities.Select(MapPlayerData).ToList());
            }
            else if (!string.IsNullOrEmpty(request.PlayerCode))
            {
                var ply = await _repo.GetPlayer(request.PlayerCode);
                players.Add(MapPlayerData(ply));
            }

            foreach (var player in players)
            {
                //Ignore if player doesn't have batting stats (such as AL pitchers)
                if (player.BattingStatistics.Count < 1)
                    continue;

                //Calculate advanced batting metrics
                player.AdvancedBattingStatistics = player.BattingStatistics.Select(a => new PlayerAdvancedBattingStatistics
                {
                    BattingAverage = a.BattingAverage,
                    BattingAvgOfBallsInPlay = BaseballStatisticCalculation.GetBattingAvgOfBallsInPlay(a.AtBats, a.Hits, a.Homeruns, a.Strikeouts, a.SacrificeFlies),
                    ExtraBaseHitPercentage = BaseballStatisticCalculation.GetRatio(a.Doubles + a.Triples + a.Homeruns, a.PlateAppearances),
                    HomeRunPercentage = BaseballStatisticCalculation.GetRatio(a.Homeruns, a.PlateAppearances),
                    InPlayPercentage = BaseballStatisticCalculation.GetRatio(a.AtBats - a.Strikeouts - a.Homeruns - a.SacrificeFlies, a.PlateAppearances),
                    OnBasePercentage = BaseballStatisticCalculation.GetRatio(a.Hits + a.Walks + a.HitByPitch, a.AtBats + a.Walks + a.HitByPitch + a.SacrificeFlies),
                    PlayerId = a.PlayerId,
                    SluggingPercentage = BaseballStatisticCalculation.GetRatio(a.Hits + (2 * a.Doubles) + (3 * a.Triples) + (4 * a.Homeruns), a.AtBats),
                    StrikeoutPercentage = BaseballStatisticCalculation.GetRatio(a.Strikeouts, a.PlateAppearances),
                    WalkPercentage = BaseballStatisticCalculation.GetRatio(a.Walks, a.PlateAppearances)

                }).ToList();
            }
            return players;
        }

        public async Task<ObjectModifyResult<int>> UpdatePlayer(Player player)
        {
            throw new NotImplementedException();
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
                DraftTeamId = p.DraftTeamId,
                DraftYear = p.DraftYear,
                FirstName = p.FirstName,
                Height = p.Height,
                LastName = p.LastName,
                MajorLeagueDebut = p.MajorLeagueDebut,
                PlayerId = p.PlayerId,
                PlayerIdentifier = p.PlayerIdentifier,
                PrimaryPosition = p.PrimaryPosition,
                RetrosheetId = p.RetrosheetId,
                Throws = p.Throws,
                Weight = p.Weight
            };

            if (p.PlayerBattingStatistics != null && p.PlayerBattingStatistics.Count > 0)
                pData.BattingStatistics.AddRange(p.PlayerBattingStatistics.Select(MapBattingStatistics));
            if (p.PlayerPitchingStatistics != null && p.PlayerPitchingStatistics.Count > 0)
                pData.PitchingStatistics.AddRange(p.PlayerPitchingStatistics.Select(MapPitchingStatistics));

            return pData;
        }

        private PlayerBattingStatistics MapBattingStatistics(DB.PlayerBattingStatistics battingStats)
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
                PlayerId = battingStats.PlayerId,
                RunsBattedIn = battingStats.RunsBattedIn,
                SacrificeFlies = battingStats.SacrificeFlies,
                SacrificeHits = battingStats.SacrificeHits,
                SluggingPercentage = battingStats.SluggingPercentage,
                StolenBases = battingStats.StolenBases,
                Strikeouts = battingStats.Strikeouts,
                TeamId = battingStats.TeamId,
                Triples = battingStats.Triples,
                Walks = battingStats.Walks,
                PlayerBattingStatisticsId = battingStats.PlayerBattingStatisticsId
            };
        }

        private PlayerPitchingStatistics MapPitchingStatistics(DB.PlayerPitchingStatistics pitchingStats)
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
                PlayerId = pitchingStats.PlayerId,
                Runs = pitchingStats.Runs,
                Shutouts = pitchingStats.Shutouts,
                Strikeouts = pitchingStats.Strikeouts,
                TeamId = pitchingStats.TeamId,
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
                PlayerId = pitching.PlayerId,
                Runs = pitching.Runs,
                Shutouts = pitching.Shutouts,
                Strikeouts = pitching.Strikeouts,
                TeamId = pitching.TeamId,
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
                PlayerId = battingStat.PlayerId,
                RunsBattedIn = battingStat.RunsBattedIn,
                SacrificeFlies = battingStat.SacrificeFlies,
                SacrificeHits = battingStat.SacrificeHits,
                SluggingPercentage = battingStat.SluggingPercentage,
                StolenBases = battingStat.StolenBases,
                Strikeouts = battingStat.Strikeouts,
                TeamId = battingStat.TeamId,
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
                DraftTeamId = player.DraftTeamId,
                DraftYear = player.DraftYear,
                FirstName = player.FirstName,
                Height = player.Height,
                LastName = player.LastName,
                MajorLeagueDebut = player.MajorLeagueDebut,
                PlayerId = player.PlayerId,
                PlayerIdentifier = player.PlayerIdentifier,
                PrimaryPosition = player.PrimaryPosition,
                RetrosheetId = player.RetrosheetId,
                Throws = player.Throws,
                Weight = player.Weight
            };
        }
    }
}
