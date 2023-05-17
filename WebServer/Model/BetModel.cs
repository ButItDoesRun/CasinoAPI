namespace WebServer.Model
{
    public class BetModel
    {
        public int Bid { get; set; }
        public string? PlayerName { get; set; }
        public int Gid { get; set; }
        public double? Amount { get; set; }
        public UrlModel? CreateBetUrl { get; set; }
        public UrlModel? UpdateBetUrl { get; set; }
        public UrlModel? DeleteBetUrl { get; set; }


    }

    public class BetCreateModel
    {
        public int Bid { get; set; }
        public string? PlayerName { get; set; }
        public int Gid { get; set; }
        public double? Amount { get; set; }
    }

    public class BetUpdateModel
    {
        public int Bid { get; set; }
        public double Amount { get; set; }
    }

    public class BetDeleteModel
    {
        public int Bid { get; set; }
    }
}
