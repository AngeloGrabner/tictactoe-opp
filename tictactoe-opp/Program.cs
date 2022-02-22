using System;

namespace TicTacToe
{
    public class Program
    {
        static void Main()
        {
            bool playstyle;
            ConsoleKeyInfo input;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("multiplayer or singleplayer? press 's' or 'm'");
                input = Console.ReadKey();
                if (input.KeyChar == 's')
                {
                    playstyle = true;
                    break;
                }
                else if (input.KeyChar == 'm')
                {
                    playstyle = false;
                    break;
                }
            }
            while (true)
            {
                Console.Clear();
                Console.WriteLine("do you know how the game works? press 'y' or 'n'");
                input = Console.ReadKey();
                if (input.KeyChar == 'y')
                {
                    break;
                }
                else if (input.KeyChar == 'n')
                {
                    Console.WriteLine(Game.getGameInfo());
                    Console.ReadKey();
                    break;
                }
            }
            Console.Clear();
            Console.WriteLine("press any key to start.");
            Console.ReadKey();
            Game game = new Game(playstyle);
            game.run();
        }
    }
}