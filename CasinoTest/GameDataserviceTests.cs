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
            Assert.Equal("TestGame", game?.Name);
            Assert.Equal(1, game?.MinBet);
            Assert.Equal(10, game?.MaxBet);
            Assert.Null(game?.PotAmount);

            //cleaning
            service.DeleteGame(game.Gid);
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
            service.DeleteGame(game.Gid);

        }

        [Fact]
        public void UpdateGameUpdatesAllParametersAndReturnsFullRecord()
        {
            var service = new DataserviceGame();
            var game = service.CreateGame("TestGame", 1, 10);
            Assert.NotNull(game);
            var updatedgame = service.UpdateGame(game.Gid, "TheTestGame", 10, 100);
            Assert.NotNull(updatedgame);
            Assert.Contains("TheTestGame", updatedgame.Name);
            Assert.Equal(10, updatedgame.MinBet);
            Assert.Equal(100, updatedgame.MaxBet);
            Assert.Null(updatedgame.PotAmount);

            //cleaning
            service.DeleteGame(game.Gid);

        }

        [Fact]
        public void UpdateNonExistingGameReturnsNull()
        {
            var service = new DataserviceGame();
            var game = service.CreateGame("TestGame", 1, 10);
            Assert.NotNull(game);
            service.DeleteGame(game.Gid);

            var updatedgame = service.UpdateGame(game.Gid, "TheTestGame", 10, 100);
            Assert.Null(updatedgame);
            
        }



        [Fact]
        public void DeleteExistingGameReturnsTrue()
        {
            var service = new DataserviceGame();
            var game = service.CreateGame("TestGame", 1, 10, 100);
            Assert.NotNull(game);

            var result = service.DeleteGame(game.Gid);
            Assert.True(result);

        }

        [Fact]
        public void DeleteNotExistingGameReturnsFalse()
        {
            var service = new DataserviceGame();
            var result = service.DeleteGame(0);
            Assert.False(result);

        }


    }
}
