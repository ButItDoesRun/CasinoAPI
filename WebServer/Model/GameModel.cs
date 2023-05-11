namespace WebServer.Model
{
    public class GameModel
    {
        //game
        public string? Name { get; set; }
        public double? MinBet { get; set; }
        public double? MaxBet { get; set; }
        public UrlModel? UpdateGameURL { get; set; }
        public UrlModel? DeleteGameURL { get; set; }

        //pot
        public double? PotAmount { get; set; }

    }

    public class GameUpdateModel
    {
        public string? Name { get; set; }
        public double? MinBet { get; set; }
        public double? MaxBet { get; set; }
    }
}
