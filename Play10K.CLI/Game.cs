using Play10K.Base;
using System;
using System.Collections.Generic;

namespace Play10K.CLI
{
    public class Game
    {
        public List<Player> Players { get; } = new List<Player>();
        private readonly GameDriver _gameDriver = new GameDriver();

        public Game(List<Player> players)
        {
            Players = players;
        }

        public void Play()
        {
            _gameDriver.ShowPlayerOrder(Players);

            bool isLastRound = false;
            while (isLastRound == false)
            {
                foreach (var player in Players)
                {
                    TakeTurn(player);
                    if (player.Score >= 10000)
                    {
                        isLastRound = true;
                        _gameDriver.MessageLastRound(player);
                    }
                }

                _gameDriver.ShowScoreAfterRound(Players);
                _gameDriver.WaitForAnyKeyInput();
            }
        }

        // Main logic, here the game is actually played.
        // Todo: Move this out. Maybe create a CLIPlayer class that implements this TakeTurn and has a base Player?
        private void TakeTurn(Player player)
        {
            _gameDriver.MessageStartOfTurn(player);
            player.StartTurn();
            bool addToScore = true;

            while (true)
            {
                player.ReconcileHand();
                player.Roll();
                _gameDriver.ShowHand(player);

                if (player.CanDoAnything == false)
                {
                    _gameDriver.MessageNoValidDice();
                    addToScore = false;
                    break;
                }
                else
                {
                    var collectedDice = _gameDriver.GetSpecifiedDice(player);
                    player.CollectDice(collectedDice);

                    var response = _gameDriver.ContinueOrEndTurn(player);
                    if (response == 'e')
                    {
                        break;
                    }
                }
            }

            var turnScore = player.EndTurn(addToScore);
            _gameDriver.MessageEndOfTurn(player, turnScore);

            _gameDriver.WaitForAnyKeyInput();
        }
    }
}
