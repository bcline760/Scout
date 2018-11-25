
namespace Scout.Model.DB
{
    public class LeagueModel : ScoutModel
    {
        
        public string LeagueCode { get; set; }
        
        public string LeagueName { get; set; }
        
        public short YearStarted { get; set; }
        
        public short? YearEnded { get; set; }
    }
}
