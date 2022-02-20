using System;

namespace TicTacToe
{
    internal class Game
    {
        private readonly bool _singelPlayer;
        private Bot bot;
        private int action = -1;
        private bool _turn; // true = player1 false = player2
        private int[] _gameState; // 0 = empty, 1 = player1, 2 = player2
        private int _gameStatus; // 0 = no winner, 1 = player1 wins, 2 = player2 wins, 3 = draw
        public Game(bool singelPlayer = true, bool startTurnSet = false, bool? playerFirstTrun = null)
        {
            _gameStatus = 0;
            _gameState = new int[] {0,0,0,0,0,0,0,0,0};
            if (startTurnSet && playerFirstTrun != null)
            {
                _turn = playerFirstTrun.Value;
            }
            else
            {
                Random rand = new Random();
                if (rand.Next(2) == 0)
                {
                    _turn = false;
                }
                else
                {
                    _turn = true;
                }
            }
            _singelPlayer = singelPlayer;
            bot = new Bot();
        }
        public void run()
        {
            bool legality;
            while (_gameStatus == 0) //gameloop
            {
                _turn = !_turn; 
                getInput();
                legality = checkLegality();
                if (!legality) // resets the turn 
                {
                    _turn = !_turn;
                    continue;
                }
                checkWin();
                Display.update(_gameState, _turn);
            }       
        }
        private void getInput()
        {
            bool validKeyState = false;
            while (!validKeyState)
            {
                if (_turn || (!_turn && !_singelPlayer))
                {
                    ConsoleKeyInfo key = Console.ReadKey();
                    string temp = ""; //not the elegant way to solve that i guess
                    temp += key.KeyChar;
                    if (int.TryParse(temp, out action))
                    {
                        validKeyState = true;
                    }
                }
                else if (_singelPlayer && !_turn)
                {
                    bot.setState(_gameState);
                    action = bot.getTurn();
                }
                else
                {
                    Console.WriteLine("error on get input");
                    return;
                }
            }
        }
        private void checkWin() //updates _gameStatus
        {
            if //player 1 wins
            (
                (_gameState[0] == 1 && _gameState[1] == 1 && _gameState[2] == 1) ||
                (_gameState[3] == 1 && _gameState[4] == 1 && _gameState[5] == 1) ||
                (_gameState[6] == 1 && _gameState[7] == 1 && _gameState[8] == 1) ||

                (_gameState[0] == 1 && _gameState[3] == 1 && _gameState[6] == 1) ||
                (_gameState[1] == 1 && _gameState[4] == 1 && _gameState[7] == 1) ||
                (_gameState[2] == 1 && _gameState[5] == 1 && _gameState[8] == 1) ||

                (_gameState[0] == 1 && _gameState[4] == 1 && _gameState[8] == 1) ||
                (_gameState[2] == 1 && _gameState[4] == 1 && _gameState[6] == 1) 
            )
            {
                _gameStatus = 1;
            }
            else if //palyer 2 wins
            (
                (_gameState[0] == 2 && _gameState[1] == 2 && _gameState[2] == 2) ||
                (_gameState[3] == 2 && _gameState[4] == 2 && _gameState[5] == 2) ||
                (_gameState[6] == 2 && _gameState[7] == 2 && _gameState[8] == 2) ||

                (_gameState[0] == 2 && _gameState[3] == 2 && _gameState[6] == 2) ||
                (_gameState[1] == 2 && _gameState[4] == 2 && _gameState[7] == 2) ||
                (_gameState[2] == 2 && _gameState[5] == 2 && _gameState[8] == 2) ||

                (_gameState[0] == 2 && _gameState[4] == 2 && _gameState[8] == 2) ||
                (_gameState[2] == 2 && _gameState[4] == 2 && _gameState[6] == 2)
            )
            {
                _gameStatus = 2;
            }
            else if //checks for draw 
            (
                _gameState[0] != 0 &&
                _gameState[1] != 0 &&
                _gameState[2] != 0 && 
                _gameState[3] != 0 && 
                _gameState[5] != 0 &&
                _gameState[6] != 0 &&
                _gameState[7] != 0 &&
                _gameState[8] != 0
            )
            {
                _gameStatus = 3;
            }
        }
        private bool checkLegality()
        {
            if (_gameState[action] == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

}