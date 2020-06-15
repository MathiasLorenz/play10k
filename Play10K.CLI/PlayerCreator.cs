using Play10K.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Play10K.CLI
{
    public class PlayerCreator
    {
        private readonly UserInputHandler _responseRequester = new UserInputHandler();
        public List<Player> Create()
        {
            List<Player> players = new List<Player>();
            bool createAnotherPlayer = true;

            while (createAnotherPlayer)
            {
                Console.WriteLine("Giver your player a name: ");
                var playerName = Console.ReadLine();
                if (players.Any(x => String.Equals(x.Name, playerName, StringComparison.InvariantCultureIgnoreCase)))
                {
                    Console.WriteLine("A player already has this name, so you need to choose another!");
                    continue;
                }
                players.Add(new Player(playerName));

                Console.WriteLine("Do you wanna add more players? Type: y/n");
                var response = _responseRequester.GetCharResponse(new List<char> { 'y', 'n' });
                if (response == 'n')
                {
                    createAnotherPlayer = false;
                }
            }

            return players;
        }
    }
}
