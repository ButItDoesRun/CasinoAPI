namespace WebServer.Model
{
    public class PlayersModel
    {
        public string? PlayerName { get; set; }
        public double? Balance { get; set; }
        public string? Url { get; set; }
    }

    public class PlayerCreateModel
    {
        public string? PlayerName { get; set; }
        public string? Password { get; set; }
        public string? BirthDate { get; set; }
    }
}
