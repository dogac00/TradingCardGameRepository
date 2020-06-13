using System.Linq;
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

            Assert.Equal(expectedHealth, player.Health);
        }

        [Theory]
        [InlineData(5, 30)]
        [InlineData(2, 30)]
        [InlineData(4, 30)]
        public void ShouldHeal(int heal, int expectedHealth)
        {
            var player = new Player("Dogac");

            player.Heal(heal);

            Assert.Equal(expectedHealth, player.Health);
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

            Assert.Equal(expectedHealth, player.Health);
        }

        [Fact]
        public void ShouldReceiveManaSlot()
        {
            var player = new Player("Dogac");

            player.ReceiveManaSlot();
            player.ReceiveManaSlot();
            player.ReceiveManaSlot();

            Assert.Equal(3, player.ManaSlots);
        }

        [Fact]
        public void ShouldRefillManaSlots()
        {
            var player = new Player("Dogac");

            player.ReceiveManaSlot();
            player.ReceiveManaSlot();
            player.ReceiveManaSlot();
            player.ReceiveManaSlot();
            player.RefillManaSlots();

            Assert.Equal(4, player.Mana);
        }

        [Fact]
        public void ShouldPlayHand()
        {
            var player = new Player("Dogac");

            player.ReceiveManaSlot();
            player.ReceiveManaSlot();
            player.ReceiveManaSlot();
            player.ReceiveManaSlot();
            player.RefillManaSlots();

            player.DrawCard();
            player.DrawCard();
            player.DrawCard();
            var min = player.Hand.Min().Value;

            if (min > player.Mana)
                Assert.False(player.CanPlayHand());
            else
                Assert.True(player.CanPlayHand());
        }
    }
}
