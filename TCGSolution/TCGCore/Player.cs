using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TCGCore
{
    public class Player
    {
        public int Health { get; private set; } = 30;
        public int Mana { get; private set; } = 0;
        public int ManaSlots { get; private set; } = 0;
        public string Name { get; }
        public List<Card> Deck { get; } = new List<Card> { 0, 0, 1, 1, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 5, 5, 6, 6, 7, 8 };
        public List<Card> Hand { get; } = new List<Card>();

        public Player(string name)
        {
            this.Name = name;
        }

        public int TakeDamage(int damage) => this.Health -= damage;

        public int Heal(int health) => this.Health = Math.Min(this.Health += health, 30);

        public bool CanPlayHand() => this.Mana >= this.Hand.Min().Value;

        public int ReceiveManaSlot() => this.ManaSlots = Math.Min(this.ManaSlots + 1, 10);

        public int RefillManaSlots() => this.Mana = this.ManaSlots;

        public Card DrawCard()
        {
            if (this.Deck.Count == 0)
            {
                this.TakeDamage(1);
                return null;
            }
            else
            {
                var index = new Random().Next(this.Deck.Count);
                var card = this.Deck[index];
                this.Deck.RemoveAt(index);

                if (this.Hand.Count < 5)
                    this.Hand.Add(card);

                return card;
            }
        }

        public void PlayCard(Card card, Player opponent, bool isHealing = false)
        {
            this.Hand.Remove(card);
            this.DecreaseMana(card.Value);

            if (isHealing)
                this.Heal(card.Value);
            else
                opponent.TakeDamage(card.Value);
        }

        private string GetHandString()
        {
            var sb = new StringBuilder();

            for (int i = 0; i < this.Hand.Count; i++)
            {
                if (i != 0)
                    sb.Append(", ");

                sb.Append(Hand[i].Value);
            }

            return sb.ToString();
        }

        public override string ToString()
        {
            return $"Name : {this.Name}\n" +
                   $"Health : {this.Health}\n" +
                   $"Mana : {this.Mana}/{this.ManaSlots}\n" +
                   $"Hand : {this.GetHandString()}\n";
        }

        public void DecreaseMana(int selectedCard) => this.Mana -= selectedCard;
    }
}
