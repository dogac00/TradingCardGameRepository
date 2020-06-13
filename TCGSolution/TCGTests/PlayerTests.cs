using TCGCore;
using Xunit;

namespace TCGTests
{
    public class PlayerTests
    {
        [Theory]
        [InlineData(5, 25)]
        [InlineData(2, 28)]
        [InlineData(4, 26)]
        public void ShouldTakeDamage(int damage, int expectedHealth)
        {
            var player = new Player("Dogac");

            player.TakeDamage(damage);

            Assert.Equal(player.Health, expectedHealth);
        }

        [Theory]
        [InlineData(5, 30)]
        [InlineData(2, 30)]
        [InlineData(4, 30)]
        public void ShouldHeal(int heal, int expectedHealth)
        {
            var player = new Player("Dogac");

            player.Heal(heal);

            Assert.Equal(player.Health, expectedHealth);
        }

        [Theory]
        [InlineData(5, 2, 27)]
        [InlineData(2, 5, 30)]
        [InlineData(4, 2, 28)]
        public void ShouldTakeDamageAndHeal(int damage, int heal, int expectedHealth)
        {
            var player = new Player("Dogac");

            player.TakeDamage(damage);
            player.Heal(heal);

            Assert.Equal(player.Health, expectedHealth);
        }
    }
}
