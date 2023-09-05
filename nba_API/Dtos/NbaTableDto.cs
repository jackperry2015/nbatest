using System.ComponentModel.DataAnnotations;

namespace nba_API.Dtos
{
    public class NbaTableDto
    {
        [Key]
        public int TeamID { get; set; }
        public string Name { get; set; }
        public string Stadium { get; set; }
        public string Logo { get; set; }
        public string URL { get; set; }
        public int GamesPlayedCount { get; set; }
        public int HomeTeamCount { get; set; }
        public int AwayTeamCount { get; set; }
        public int TotalWins { get; set; }
        public int TotalLoss { get; set; }
        public string BiggestWin { get; set; }
        public string BiggestLoss { get; set; }
        public string MostRecentGameStadium { get; set; }
        public string MostRecentGameMVP { get; set; }
        public string MostRecentGameDate { get; set; }




    }
}
