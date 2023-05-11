namespace WebServer.Model
{
    public class GameModel
    {
        //game
        public string? Name { get; set; }
        public double? MinBet { get; set; }
        public double? MaxBet { get; set; }
        public UrlModel? UpdateGameUrl { get; set; }
        public UrlModel? DeleteGameUrl { get; set; }

    }

    public class GameCreateModel
    {
        public string? Name { get; set; }
        public double? MinBet { get; set; }
        public double? MaxBet { get; set; }
    }

    public class GameUpdateModel
    {
        public string? Name { get; set; }
        public double? MinBet { get; set; }
        public double? MaxBet { get; set; }
    }
}
