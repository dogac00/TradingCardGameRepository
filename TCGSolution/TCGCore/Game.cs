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
        public InputOutputBase InputOutputHelper { get; set; }

        public Game(string name1, string name2, InputOutputBase ioHelper)
        {
            AssignInitialPlayers(name1, name2);
            DealStartingHands();
            this.InputOutputHelper = ioHelper;
        }

        private void AssignInitialPlayers(string name1, string name2)
        {
            var player1 = new Player(name1);
            var player2 = new Player(name2);
            var rnd = new Random().Next(2);
            this.ActivePlayer = rnd == 0 ? player1 : player2;
            this.OpponentPlayer = this.ActivePlayer == player1 ? player2 : player1;
        }

        private void DealStartingHands()
        {
            for (int i = 0; i < 3; i++)
            {
                this.ActivePlayer.DrawCard();
                this.OpponentPlayer.DrawCard();
            }
        }

        public void Start()
        {
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

            InputOutputHelper.WriteOutput("Game Over!");
            InputOutputHelper.WriteOutput($"Player { ActivePlayer.Name }\nis the Winner!");
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
                InputOutputHelper.WriteOutput($"Player:\n{ ActivePlayer }\nIt is your turn! Choose a card from your hand.");
                InputOutputHelper.WriteOutput("For healing you should enter H before the card number.");

                var choice = InputOutputHelper.GetInput();

                if (choice == null)
                {
                    InputOutputHelper.WriteOutput("Please enter a choice.");
                    continue;
                }

                var choiceWithoutH = choice.Replace("H", "");

                if (!int.TryParse(choiceWithoutH, out var selectedCard))
                {
                    InputOutputHelper.WriteOutput($"The entry { choice } is not a valid choice.");
                    continue;
                }

                if (!ActivePlayer.Hand.Contains(selectedCard))
                {
                    InputOutputHelper.WriteOutput("Choice number is not present in hand.");
                    continue;
                }

                if (ActivePlayer.Mana < selectedCard)
                {
                    InputOutputHelper.WriteOutput("Insufficient mana to play that hand.");
                    continue;
                }

                var isHealing = choice.Contains("H");
                
                ActivePlayer.PlayCard(selectedCard, OpponentPlayer, isHealing);

                InputOutputHelper.WriteOutput("\n");
            }
        }

        private void SwitchPlayers() => (ActivePlayer, OpponentPlayer) = (OpponentPlayer, ActivePlayer);
    }
}