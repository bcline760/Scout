using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Scout.Model.DB
{
    public class GameModel : ScoutModel
    {
        public GameModel()
        {
        }

        public byte Season { get; set; }
        public DateTime GameDate { get; set; }
        public char GameNumberFlag { get; set; }
        public string GameDayOfWeek { get; set; }
        public short VisitingTeamGameNumb { get; set; }
    }
}
