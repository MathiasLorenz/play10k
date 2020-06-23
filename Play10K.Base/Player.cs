using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Play10K.Base
{
    public class Player
    {
        public string Name { get; }
        public int Score { get; private set; } = 0;

        public Player(string name)
        {
            Name = name;
        }

        // Todo: Not tested at allllll.
        public void PlayTurn()
        {
            Console.WriteLine($"It is now your turn, {Name}.");
            Console.WriteLine($"Your score is currently: {Score}");
            var hand = new Hand();

            while (true)
            {
                hand.ReconcileHand();
                hand.Roll();
                if (hand.IsAnyCombinationValid() == false)
                {
                    hand.Clear();
                    break;
                }
                Console.WriteLine($"This turn you have collected {hand.Score} points.");
                Console.WriteLine($"Your dice right now are:");
                hand.Show();

                hand.CollectDiceFromUser();

                var response = hand.ContinueOrEndTurn();
                if (response == 'e')
                {
                    break;
                }
            }

            Score += hand.Score;
            Console.WriteLine($"Your turn is now over, {Name}.");
            Console.WriteLine($"You collected {hand.Score} points this turn and now have {Score} points in total.");
        }

    }
}
