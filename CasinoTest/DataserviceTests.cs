using DataLayer;
using DataLayer.DatabaseModel.CasinoModel;

namespace CasinoTest
{
    public class DataserviceTests
    {
        //Testing whether our repository methods return expected values

        [Fact]
        public void CreatePlayerReturnsTrue()
        {
            var service = new DataservicePlayer();
            DateOnly birthdate = new DateOnly(1998,04,24);
            var player = service.CreatePlayer("TestPlayer", "C1B5435157DB2669B81DE9CA2A87CB2CBCCD4667319A08A724AD4F505B823485", birthdate, 4533.32, "7ECB6B87B2408AC9");
            Assert.True(player);
        }
    }
}