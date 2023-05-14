﻿namespace WebServer.Model
{
    public class BetModel
    {
        public int Bid { get; set; }
        public string? PlayerName { get; set; }
        public int Gid { get; set; }
        public double? Amount { get; set; }
        public DateTime Date { get; set; }
        public string? Url { get; set; }


    }

    public class BetCreateModel
    {
        public int Bid { get; set; }
        public string? PlayerName { get; set; }
        public int Gid { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
    }

    public class BetUpdateModel
    {
        public int Bid { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
