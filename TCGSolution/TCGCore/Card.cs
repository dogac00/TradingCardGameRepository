using System;

namespace TradingCardGameCore
{
    public class Card : IComparable<Card>
    {
        public int Value { get; }

        public Card(int value)
        {
            this.Value = value;
        }

        public static implicit operator Card(int value) => new Card(value);

        public override bool Equals(object obj)
        {
            var card = obj as Card;

            if (card == null)
                return false;

            return this.Value == card.Value;
        }

        public override int GetHashCode()
        {
            return this.Value;
        }

        public int CompareTo(Card other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return Value.CompareTo(other.Value);
        }
    }
}