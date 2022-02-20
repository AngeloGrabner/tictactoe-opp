using System;

namespace TicTacToe
{
    public class Program
    {
        static void Main()
        {
            Game game = new Game(false);
            game.run();
        }
    }
}