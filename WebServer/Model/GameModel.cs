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

        //pot
        public double? Amount { get; set; }
        public UrlModel? CreatePotUrl { get; set; }
        public UrlModel? UpdatePotUrl { get; set; }
        public UrlModel? DeletePotUrl { get; set; }
    }

    public class GameUpdateModel
    {
        public string? Name { get; set; }
        public double? MinBet { get; set; }
        public double? MaxBet { get; set; }
    }
}
