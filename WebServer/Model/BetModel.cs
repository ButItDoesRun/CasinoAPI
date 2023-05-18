namespace WebServer.Model
{
    public class BetModel
    {
        public int bid { get; set; }
        public int gid { get; set; }
        public string? PlayerName { get; set; }
        public double? Amount { get; set; }
        public DateTime date { get; set; }
        public UrlModel? CreateBetUrl { get; set; }
        public UrlModel? UpdateBetUrl { get; set; }
        public UrlModel? DeleteBetUrl { get; set; }


    }

    public class BetCreateModel
    {
        public string? PlayerName { get; set; }
        public double? Amount { get; set; }
    }

    public class BetUpdateModel
    {
        public double Amount { get; set; }
    }

    public class BetDeleteModel
    {
    }
}
