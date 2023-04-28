using DataLayer;

namespace CasinoAPI.CasinoTest
{
    public class GameDataserviceTests
    {
        [Fact]
        public void CreateNewGameWithoutPotReturnsFullRecordWithNullPot()
        {
            var service = new DataserviceGame();
            var game = service.CreateGame("TestGame", 1, 10);
            Assert.NotNull(game);
            Assert.Contains("TestGame", game.Name);
            Assert.Equal(1, game.MinBet);
            Assert.Equal(10, game.MaxBet);
            Assert.Null(game.PotAmount);

            //cleaning
            //service.DeleteGame(game.Gid);
        }

        [Fact]
        public void CreateNewGameWithPotReturnsFullRecord()
        {
            var service = new DataserviceGame();
            var game = service.CreateGame("TestGame", 1, 10, 100);
             Assert.NotNull(game);
            Assert.Contains("TestGame", game.Name);
            Assert.Equal(1, game.MinBet);
            Assert.Equal(10, game.MaxBet);
            Assert.Equal(100, game.PotAmount);

            //cleaning
            //service.DeleteGame(game.Gid);

        }

    }
}
