using System.Collections.Generic;
using System.Linq;
using TCGCore;
using Xunit;

namespace TCGTests
{
    public class PlayerTests
    {
        [Fact]
        public void ShouldStartWithRequiredHealth()
        {
            var player = new Player("Dogac");
            int expected = 30;

            Assert.Equal(expected, player.Health);
        }

        [Fact]
        public void ShouldStartWithRequiredMana()
        {
            var player = new Player("Dogac");

            Assert.Equal(0, player.Mana);
        }

        [Fact]
        public void ShouldStartWithRequiredManaSlots()
        {
            var player = new Player("Dogac");

            Assert.Equal(0, player.ManaSlots);
        }

        [Fact]
        public void ShouldStartWithRequiredDeck()
        {
            var player = new Player("Dogac");
            var cards = new List<Card> {0, 0, 1, 1, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 5, 5, 6, 6, 7, 8};

            Assert.Equal(cards, player.Deck);
        }

        [Fact]
        public void ShouldHaveNoCardsInHand()
        {
            var player = new Player("Dogac");
            var expected = new List<Card>();

            Assert.Equal(expected, player.Hand);
        }

        [Fact]
        public void ShouldCapAtThirty()
        {
            var player = new Player("Dogac");

            player.Heal(1);
            player.Heal(3);
            player.Heal(6);

            Assert.Equal(30, player.Health);
        }

        [Fact]
        public void ShouldReceiveOneDamageWhenNoCardInHand()
        {
            var player = new Player("Dogac");
            var deckLen = player.Deck.Count;

            for (int i = 0; i < deckLen; i++)
                player.DrawCard();

            int initialHealth = player.Health;

            player.DrawCard();

            Assert.Equal(initialHealth - 1, player.Health);
        }

        [Fact]
        public void ShouldDrawCard()
        {
            var player = new Player("Dogac");
            var handLen1 = player.Hand.Count;
            var deckLen1 = player.Deck.Count;
            
            player.DrawCard();

            var handLen2 = player.Hand.Count;
            var deckLen2 = player.Deck.Count;
            
            Assert.Equal(handLen1 + 1, handLen2);
            Assert.Equal(deckLen1 - 1, deckLen2);
        }

        [Fact]
        public void ShouldDiscardWhenThereAreFiveCardsInHand()
        {
            var player = new Player("Dogac");
            var initialDeckLen = player.Deck.Count;

            player.DrawCard();
            player.DrawCard();
            player.DrawCard();
            player.DrawCard();
            player.DrawCard();
            player.DrawCard();
            player.DrawCard();

            Assert.Equal(5, player.Hand.Count);
            Assert.Equal(initialDeckLen - 7, player.Deck.Count);
        }

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
