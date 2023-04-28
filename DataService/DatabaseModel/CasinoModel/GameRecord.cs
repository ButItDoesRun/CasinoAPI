namespace DataLayer.DatabaseModel.CasinoModel;

public class GameRecord
{
    public int Gid { get; set; }
    public string? Name { get; set; }
    public double? MinBet { get; set; }
    public double? MaxBet { get; set; }
    public int? Pid { get; set; }
    public double? Amount { get; set; }
}