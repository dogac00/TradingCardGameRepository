using System.Collections.Generic;

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
    }
}
