using TCGCore;
using Xunit;

namespace TCGTests
{
    public class GameTests
    {
        [Fact]
        public void ShouldHaveTwoPlayers()
        {
            var game = new Game("Nuri", "Dogac", null);

            Assert.NotNull(game.ActivePlayer);
            Assert.NotNull(game.OpponentPlayer);
        }

        [Fact]
        public void ShouldNotHaveWinnerAtStart()
        {
            var game = new Game("Nuri", "Dogac", null);

            Assert.Null(game.Winner);
        }

        [Fact]
        public void ShouldHaveThreeCardsInHand()
        {
            var game = new Game("Nuri", "Dogac", null);

            Assert.Equal(3, game.ActivePlayer.Hand.Count);
            Assert.Equal(3, game.OpponentPlayer.Hand.Count);
        }
    }
}
