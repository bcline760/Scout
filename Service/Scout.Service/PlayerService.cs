using Scout.Service.Contract;
using System;
using Scout.Core.Contract;
using Scout.Model.Contract;
using System.Collections.Generic;
using System.Threading.Tasks;
using Scout.Model.DB.Repository;
using DB = Scout.Model.DB;

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
                var entity = new DB.PlayerBattingStatistics
                {
                    AtBats = batting.AtBats,
                    BattingAverage = batting.BattingAverage,
                    CaughtStealing = batting.CaughtStealing,
                    Doubles = batting.Doubles,
                    GroundedIntoDoublePlay = batting.GroundedIntoDoublePlay,
                    HitByPitch = batting.HitByPitch,
                    Hits = batting.Hits,
                    Homeruns = batting.Homeruns,
                    IntentionalWalks = batting.IntentionalWalks,
                    OnBasePercentage = batting.OnBasePercentage,
                    OnBasePlusSlugging = batting.OnBasePlusSlugging,
                    OnBasePlusSluggingAdj = batting.OnBasePlusSluggingAdj,
                    PlateAppearances = batting.PlateAppearances,
                    PlayerId = batting.PlayerId,
                    RunsBattedIn = batting.RunsBattedIn,
                    SacrificeFlies = batting.SacrificeFlies,
                    SacrificeHits = batting.SacrificeHits,
                    SluggingPercentage = batting.SluggingPercentage,
                    StolenBases = batting.StolenBases,
                    Strikeouts = batting.Strikeouts,
                    TeamId = batting.TeamId,
                    Triples = batting.Triples,
                    Walks = batting.Walks
                };

                int recordsModified = await _repo.CreatePlayerBattingStatistics(entity);
                totalRecords += recordsModified;
            }

            if (pitching != null)
            {
                var entity = new DB.PlayerPitchingStatistics
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

            DB.Player playerEntity = _mapper.Map<Player>(player);
            int playerId = -1;


            playerId = await _repo.CreatePlayer(playerEntity);

            if (player.BattingStatistics.Count > 0)
            {
                PlayerBattingStatistics pbsEntity = null;
                foreach (var battingStat in player.BattingStatistics)
                {
                    pbsEntity = _mapper.Map<PlayerBattingStatistics>(battingStat);
                    playerEntity.PlayerBattingStatistics.Add(pbsEntity);
                }
            }

            if (player.PitchingStatistics.Count > 0)
            {
                PlayerPitchingStatistics ppsEntity = null;
                foreach (var pitching in player.PitchingStatistics)
                {
                    ppsEntity = Mapper.Map<PlayerPitchingStatistics>(pitching);
                    playerEntity.PlayerPitchingStatistics.Add(ppsEntity);
                }
            }

            return playerId;
        }

        public async Task<List<Player>> FindPlayers(PlayerSearchRequest request)
        {

        }

        public async Task<ObjectModifyResult<int>> UpdatePlayer(Player player)
        {
            throw new NotImplementedException();
        }
    }
}
