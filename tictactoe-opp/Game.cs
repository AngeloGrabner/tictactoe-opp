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
    internal class Display
    {
        private static char[] f; //fild
        /*
         u2588 is the block 
        
        112233445566778899██112233445566778899██112233445566778899
        112233445566778899██112233445566778899██112233445566778899
        112233445566778899██112233445566778899██112233445566778899
        112233445566778899██112233445566778899██112233445566778899
        112233445566778899██112233445566778899██112233445566778899
        112233445566778899██112233445566778899██112233445566778899
        112233445566778899██112233445566778899██112233445566778899
        112233445566778899██112233445566778899██112233445566778899
        112233445566778899██112233445566778899██112233445566778899
        ██████████████████████████████████████████████████████████
        112233445566778899██112233445566778899██112233445566778899
        112233445566778899██112233445566778899██112233445566778899
        112233445566778899██112233445566778899██112233445566778899
        112233445566778899██112233445566778899██112233445566778899
        112233445566778899██112233445566778899██112233445566778899
        112233445566778899██112233445566778899██112233445566778899
        112233445566778899██112233445566778899██112233445566778899
        112233445566778899██112233445566778899██112233445566778899
        112233445566778899██112233445566778899██112233445566778899
        ██████████████████████████████████████████████████████████
        112233445566778899██112233445566778899██112233445566778899
        112233445566778899██112233445566778899██112233445566778899
        112233445566778899██112233445566778899██112233445566778899
        112233445566778899██112233445566778899██112233445566778899
        112233445566778899██112233445566778899██112233445566778899
        112233445566778899██112233445566778899██112233445566778899
        112233445566778899██112233445566778899██112233445566778899
        112233445566778899██112233445566778899██112233445566778899
        112233445566778899██112233445566778899██112233445566778899

        112233445566778899
        112233██████778899
        1122██445566██8899
        11██3344556677██99
        11██3344556677██99
        11██3344556677██99
        1122██445566██8899
        112233██████778899
        112233445566778899

        112233445566778899
        11██3344556677██99
        1122██445566██8899
        112233██55██778899
        11223344██66778899
        112233██55██778899
        1122██445566██8899
        11██3344556677██99
        112233445566778899

         */
        static Display()
        {
            Console.CursorVisible = false;
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            f = new char[59 * 58];
        }
        public static void update(int[] GameState, bool playerTurn)
        {
            Console.Clear();
            if (!playerTurn) // should be right
            {
                Console.Write("It's the turn of player ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("One!\n");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.Write("It's the turn of player ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Two!\n");
                Console.ForegroundColor = ConsoleColor.White;
            }
            draw();
            Console.WriteLine(f);
        }
        private static int Convert(int x, int y) // makes a 2d coordinate into a 1d one 
        {
            return y * 58 + x; // 58 is the max lenght of x
        }

        private static void draw()
        {
            for (int i = 0; i < f.Length; i++)
                f[i] = ' ';
            for (int i = 58; i < f.Length; i += Convert(0, 1))
                f[i] = '\n';

        }
    }
    internal class Bot
    {
        public Bot()
        {

        }
        public void setState(int[] state)
        {

        }
        public int getTurn()
        {
            return 0;
        }
    }
}