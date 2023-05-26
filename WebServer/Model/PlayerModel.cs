namespace WebServer.Model
{
    public class PlayerModel
    {
        public string? PlayerName { get; set; }
        public string? Password { get; set; }
        public double? Balance { get; set; }
        public DateOnly? BirthDate { get; set; }
        public bool IsDeveloper { get; set; }

        public UrlModel? UpdatePlayerUrl { get; set; }
        public UrlModel? UpdatePlayerBalanceUrl { get; set; }
        public UrlModel? DeletePlayerUrl { get; set; }
    }

    public class PlayerCreateModel
    {
        public string? PlayerName { get; set; }
        public string? Password { get; set; }
        public string? BirthDate { get; set; }
    }

    public class PlayerUpdateModel
    {
        public string? Password { get; set; }
        public string? BirthDate { get; set; }
    }

    public class PlayerBalanceUpdateModel
    {
        public double? Balance { get; set; }
    }


    public class PlayerGamesModel
    {
        //game
        public string? Name { get; set; }
        public double? MinBet { get; set; }
        public double? MaxBet { get; set; }
        public double? PotAmount { get; set; }
        public IList<BetModel>? Bets { get; set; }

    }


    public class PlayerLoginModel
    {
        public string? PlayerName { get; set; }
        public string? Password { get; set; }

    }


}
