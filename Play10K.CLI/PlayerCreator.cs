using Play10K.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Play10K.CLI
{
    public static class PlayerCreator
    {
        public static List<Player> Create()
        {
            List<Player> players = new List<Player>();
            bool createAnotherPlayer = true;

            while (createAnotherPlayer)
            {
                Console.WriteLine("Giver your player a name: ");
                var playerName = Console.ReadLine();
                players.Add(new Player(playerName));

                Console.WriteLine("Do you wanna add more players? Type: y/n");
                while (true)
                {
                    var response = Console.ReadLine().Trim();
                    if (response == "Y" || response == "y")
                    {
                        break;
                    }
                    else if (response == "N" || response == "n")
                    {
                        createAnotherPlayer = false;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("That was not a valid input. Use y/n (or Y/N).");
                    }
                }
                
            }

            return players;
        }
    }
}
