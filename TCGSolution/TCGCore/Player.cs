using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace TCGCore
{
    public class Player
    {
        public int Health { get; set; } = 30;
        public int Mana { get; set; } = 0;
        public int ManaSlots { get; set; } = 0;
        public string Name { get; set; }
        public List<Card> Deck { get; set; } = new List<Card> { 0, 0, 1, 1, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 5, 5, 6, 6, 7, 8 };
        public List<Card> Hand { get; set; } = new List<Card>();

        public Player(string name)
        {
            this.Name = name;
        }

        public int TakeDamage(int damage) => this.Health -= damage;

        public int Heal(int health) => this.Health = Math.Min(this.Health += health, 30);

        public bool CanPlayHand() => this.Mana >= this.Hand.Min().Value;

        public void DrawCard()
        {
            if (this.Deck.Count == 0)
                this.TakeDamage(1);
            else
            {
                var index = new Random().Next(this.Deck.Count);
                var card = this.Deck[index];
                this.Deck.RemoveAt(index);

                if (this.Hand.Count < 5)
                    this.Hand.Add(card);
            }
        }
    }
}
