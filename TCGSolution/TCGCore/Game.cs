using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading;

namespace TCGCore
{
    public class Game
    {
        public Player ActivePlayer { get; set; }
        public Player OpponentPlayer { get; set; }
        public Player Winner { get; set; }

        public Game(string name1, string name2)
        {
            var player1 = new Player(name1);
            var player2 = new Player(name2);
            var rnd = new Random().Next(2);
            this.ActivePlayer = rnd == 0 ? player1 : player2;
            this.OpponentPlayer = this.ActivePlayer == player1 ? player2 : player1;
        }

        public void Start()
        {
            for (int i = 0; i < 3; i++)
            {
                this.ActivePlayer.DrawCard();
                this.OpponentPlayer.DrawCard();
            }

            while (this.Winner == null)
            {
                this.ActivePlayer.ReceiveManaSlot();
                this.ActivePlayer.RefillManaSlots();
                this.ActivePlayer.DrawCard();

                PlayTurn();
                if (SetWinner())
                    break;

                SwitchPlayers();
            }

            Console.WriteLine($"Player { ActivePlayer }\nis the Winner!");
        }

        private bool SetWinner()
        {
            if (this.OpponentPlayer.Health <= 0)
            {
                this.Winner = ActivePlayer;
                return true;
            }
            if (this.ActivePlayer.Health <= 0)
            {
                this.Winner = OpponentPlayer;
                return true;
            }

            return false;
        }

        private void PlayTurn()
        {
            while (ActivePlayer.CanPlayHand())
            {
                Console.WriteLine($"Player:\n{ ActivePlayer }\nIt is your turn! Choose a card from your hand.");
                Console.WriteLine("For healing you should enter H before the card number.");

                var choice = Console.ReadLine();

                if (choice == null)
                {
                    Console.WriteLine("Please enter a choice.");
                    continue;
                }

                var choiceWithoutH = choice.Replace("H", "");

                if (!int.TryParse(choiceWithoutH, out var selectedCard))
                {
                    Console.WriteLine($"The entry { choice } is not a valid choice.");
                    continue;
                }

                if (!ActivePlayer.Hand.Contains(selectedCard))
                {
                    Console.WriteLine("Choice number is not present in hand.");
                    continue;
                }

                if (ActivePlayer.Mana < selectedCard)
                {
                    Console.WriteLine("Insufficient mana to play that hand.");
                    continue;
                }

                var isHealing = choice.Contains("H");
                ActivePlayer.Hand.Remove(selectedCard);
                ActivePlayer.DecreaseMana(selectedCard);

                if (isHealing)
                    ActivePlayer.Heal(selectedCard);
                else
                    OpponentPlayer.TakeDamage(selectedCard);

                Console.WriteLine();
            }
        }

        private void SwitchPlayers() => (ActivePlayer, OpponentPlayer) = (OpponentPlayer, ActivePlayer);
    }
}
