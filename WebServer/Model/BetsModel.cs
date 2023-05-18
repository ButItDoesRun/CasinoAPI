namespace WebServer.Model
{
    public class BetsModel
    {
        public int? Bid { get; set; }
        public string? PlayerName { get; set; }
        public double? Amount { get; set; }
        public string? Url { get; set; }
    }

    public class GetBetsPlayerGameModel
    {
        public int Gid { get; set; }
        public string PlayerName { get; set; }
    }
}
