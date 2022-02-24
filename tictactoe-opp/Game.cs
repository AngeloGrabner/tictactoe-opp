using System;

namespace TicTacToe
{
    internal class Game
    {

        #region Konstruktoren
        internal Game() : this(true, false, null) { }                           //Konstruktor wenn nichts übergeben wird
                                                                                //'this' übergibt drei Werte an den drei Parameter-Konstruktor  
        internal Game(bool playMod) : this(playMod, false, null) { }            //Konstruktor wenn nur der playMod übergeben wird übergeben wird
        internal Game(bool playMod, bool startTurnSet) : this(playMod, startTurnSet, null) { }
        internal Game(bool playMod, bool startTurnSet, bool? playerFirstTurn)   //Konstruktor wenn alle Felder übergeben werden. 
        {
            sTS = startTurnSet;
            pFT = playerFirstTurn;

            singlePlayer = playMod;
            gameStatus = 0;
            gameState = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };



            if (sTS && pFT != null)
            {
                turn = pFT.Value;
            }
            else
            {
                Random rand = new Random();

                if (rand.Next(2) == 0)
                {
                    turn = false;
                }
                else
                {
                    turn = true;
                }

            }
            bot = new Bot();
        }
        #endregion

        #region Klassenfelder
        internal static readonly string info = "use number key 1 to 9 (left to right, top to bottom)";
        #endregion

        #region Insatnzfelder

        internal bool? pFT { get; set; }
        internal bool sTS { get; set; }
        internal bool singlePlayer { get; set; }
        internal Bot bot { get; set; }
        internal bool turn { get; set; }
        internal int[] gameState { get;private set; } //Die Sichtbarkeit von 'set' ist auf die Anwendung beschränkt
        internal int gameStatus { get; set; }

        private int action = -1;


        #endregion

        #region Methoden der Instanz

        public void run()
        {
            bool legality;
            while (gameStatus == 0) //gameloop
            {
                turn = !turn;
                getInput();
                legality = checkLegality();
                if (!legality) // resets the turn 
                {
                    //Console.WriteLine("not legal");
                    turn = !turn;
                    continue;
                }
                setMove();
                checkWin();
                Display.update(gameState, turn);
            }
            endOfMatch();
        }
        private void getInput()
        {
            bool validKeyState = false;
            while (!validKeyState)
            {
                if (turn || (!turn && !singlePlayer))
                {
                    ConsoleKeyInfo key = Console.ReadKey();
                    string temp = ""; //not the elegant way to solve that i guess
                    temp += key.KeyChar;
                    if (int.TryParse(temp, out action))
                    {
                        action--;
                        if (action >= 0 && action < 9)
                        {
                            validKeyState = true;
                            Thread.Sleep(300);
                        }
                    }
                }
                else if (singlePlayer && !turn)
                {
                    bot.setState(gameState);
                    action = bot.getTurn();
                    if (action >= 0 && action < 9)
                    {
                        validKeyState = true;
                    }
                }
                else
                {
                    Console.WriteLine("error on get input");
                    return;
                }
            }
        }
        private void checkWin() //updates gameStatus
        {
            if //player 1 wins
            (
                (gameState[0] == 1 && gameState[1] == 1 && gameState[2] == 1) ||
                (gameState[3] == 1 && gameState[4] == 1 && gameState[5] == 1) ||
                (gameState[6] == 1 && gameState[7] == 1 && gameState[8] == 1) ||

                (gameState[0] == 1 && gameState[3] == 1 && gameState[6] == 1) ||
                (gameState[1] == 1 && gameState[4] == 1 && gameState[7] == 1) ||
                (gameState[2] == 1 && gameState[5] == 1 && gameState[8] == 1) ||

                (gameState[0] == 1 && gameState[4] == 1 && gameState[8] == 1) ||
                (gameState[2] == 1 && gameState[4] == 1 && gameState[6] == 1)
            )
            {
                gameStatus = 1;
            }
            else if //palyer 2 wins
            (
                (gameState[0] == 2 && gameState[1] == 2 && gameState[2] == 2) ||
                (gameState[3] == 2 && gameState[4] == 2 && gameState[5] == 2) ||
                (gameState[6] == 2 && gameState[7] == 2 && gameState[8] == 2) ||

                (gameState[0] == 2 && gameState[3] == 2 && gameState[6] == 2) ||
                (gameState[1] == 2 && gameState[4] == 2 && gameState[7] == 2) ||
                (gameState[2] == 2 && gameState[5] == 2 && gameState[8] == 2) ||

                (gameState[0] == 2 && gameState[4] == 2 && gameState[8] == 2) ||
                (gameState[2] == 2 && gameState[4] == 2 && gameState[6] == 2)
            )
            {
                gameStatus = 2;
            }
            else if //checks for draw 
            (
                gameState[0] != 0 &&
                gameState[1] != 0 &&
                gameState[2] != 0 &&
                gameState[3] != 0 &&
                gameState[5] != 0 &&
                gameState[6] != 0 &&
                gameState[7] != 0 &&
                gameState[8] != 0
            )
            {
                gameStatus = 3;
            }
        }
        private bool checkLegality()
        {
            if (gameState[action] == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void setMove()
        {
            if (turn)
            {
                gameState[action] = 1;
            }
            else
            {
                gameState[action] = 2;
            }
        }
        private void endOfMatch()
        {
            //Console.Clear();
            if (gameStatus < 3)
            {
                Console.WriteLine($"player {gameStatus} has won");
                for (int i = 0; i < 60; i++)
                {
                    Console.WriteLine();
                    Thread.Sleep(200);
                }
            }
            else
            {
                Console.WriteLine("it's a draw");
                for (int i = 0; i < 60; i++)
                {
                    Console.WriteLine();
                    Thread.Sleep(200);
                }
            }

            Console.ReadLine();
        }

        //Info-Methode wurde entfernt
        #endregion

    }

}