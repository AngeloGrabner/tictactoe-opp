using System;

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
internal static class Display
{
    private static CHAR_INFO[] f; //field
    private static int[] _gameState = new int[0];
    private const int _width = 59, _height = 29;
    private static ushort _xColor = (ushort)ConsoleColor.Red, _oColor = (ushort)ConsoleColor.Blue, _bordColor = (ushort)ConsoleColor.White;
    
    static Display()
    {
        if (Console.LargestWindowHeight < _height)
        {
            #pragma warning disable CA1416 // only works on windows
            Console.WindowHeight = Console.LargestWindowHeight;

        }
        else
        {
            Console.WindowHeight = _height+1;
        }
        if (Console.LargestWindowWidth < _width)
        {
            Console.WindowWidth = Console.LargestWindowWidth;
        }
        else
        {
            Console.WindowWidth = _width;
            #pragma warning restore CA1416 
        }
        Console.CursorVisible = false;
        //Console.OutputEncoding = System.Text.Encoding.Unicode;
        f = new CHAR_INFO[_width * _height];
        ColorSupport.setup(_width,_height);
    }
    public static void update(int[] gameState, bool playerTurn) // colering is coming soon
    {
        _gameState = gameState;
        //clearing the console is not needed, becourse we directly write to the console screen buffer
        draw();
        ColorSupport.displayFrame(f);
        
    }
    private static int convert(int x, int y) // makes a 2d coordinate into a 1d one 
    {
        return y * 59 + x; // 59 is the max lenght of x
    }
    private static void draw()
    {
        //for (int i = 0; i < 58; i++) //for mesurements 
        //    Console.Write(i % 10);
        //Console.WriteLine();
        for (int i = 0; i < f.Length; i++) // clearing buffer
            f[i].UnicodeChar = ' ';

        for (int i = _width-1; i < f.Length; i += _width)
            f[i].UnicodeChar = '\n';
        verticalLine(18); // left line 
        verticalLine(19);
        verticalLine(38); // right line
        verticalLine(39);

        horizontalLine(9); // top line
        horizontalLine(19); // bottom line 
        for (int i = 0; i < _gameState.Length; i++)
        {
            if (_gameState[i] == 0)
            {
                continue;
            }
            else if (_gameState[i] == 1)
            {
                draw_X(i%3,i/3);
            }
            else if (_gameState[i] == 2)
            {
                draw_O(i%3,i/3);
            }
        }
    }
    private static void verticalLine(int x)
    {
        for (int i = x; i < f.Length;i+=_width)
        {
            f[i].UnicodeChar = '\u2588';
            f[i].Attributes = _bordColor;
        }
    }
    private static void horizontalLine(int y)
    {
        for (int i = convert(0,y); i < convert(_width-1,y) ; i++)
        {
            f[i].UnicodeChar = '\u2588';
            f[i].Attributes = _bordColor;
        }
    }
    private static void draw_O(int x, int y) // yes it is hard coded and hopfully works
    {
        char c = '\u2588';
        for (int i = 6; i < 12; i++)
        {
            f[convert(x * 20 + i, y * 10 + 1)].UnicodeChar = c; // upper line 
            f[convert(x * 20 + i, y * 10 + 7)].UnicodeChar = c; //lower line

            f[convert(x * 20 + i, y * 10 + 1)].Attributes = _oColor;
            f[convert(x * 20 + i, y * 10 + 7)].Attributes = _oColor;

        }
        f[convert(x * 20 + 4, y * 10 + 2)].UnicodeChar = c; // upper left 
        f[convert(x * 20 + 5, y * 10 + 2)].UnicodeChar = c;

        f[convert(x * 20 + 4, y * 10 + 2)].Attributes = _oColor;
        f[convert(x * 20 + 5, y * 10 + 2)].Attributes = _oColor;


        f[convert(x * 20 + 12, y * 10 + 2)].UnicodeChar = c; //upper right 
        f[convert(x * 20 + 13, y * 10 + 2)].UnicodeChar = c;

        f[convert(x * 20 + 12, y * 10 + 2)].Attributes = _oColor;
        f[convert(x * 20 + 13, y * 10 + 2)].Attributes = _oColor;
        for (int i = 3; i < 6; i++) // side lines 
        {
            f[convert(x * 20 + 2, y * 10 + i)].UnicodeChar = c;
            f[convert(x * 20 + 3, y * 10 + i)].UnicodeChar = c;

            f[convert(x * 20 + 2, y * 10 + i)].Attributes = _oColor;
            f[convert(x * 20 + 3, y * 10 + i)].Attributes = _oColor;


            f[convert(x * 20 + 14, y * 10 + i)].UnicodeChar = c;
            f[convert(x * 20 + 15, y * 10 + i)].UnicodeChar = c;

            f[convert(x * 20 + 14, y * 10 + i)].Attributes = _oColor;
            f[convert(x * 20 + 15, y * 10 + i)].Attributes = _oColor;
        }

        f[convert(x * 20 + 4, y * 10 + 6)].UnicodeChar = c; // lower left 
        f[convert(x * 20 + 5, y * 10 + 6)].UnicodeChar = c;

        f[convert(x * 20 + 4, y * 10 + 6)].Attributes = _oColor;
        f[convert(x * 20 + 5, y * 10 + 6)].Attributes = _oColor;


        f[convert(x * 20 + 12, y * 10 + 6)].UnicodeChar = c; // lower right 
        f[convert(x * 20 + 13, y * 10 + 6)].UnicodeChar = c;

        f[convert(x * 20 + 12, y * 10 + 6)].Attributes = _oColor;
        f[convert(x * 20 + 13, y * 10 + 6)].Attributes = _oColor;
    }
    private static void draw_X(int x, int y) // this one as well
    {
        char c = '\u2588';
        for (int i = 1, j = 2; i < 8; i++, j+=2) // left top to right bottom 
        {
            f[convert(x * 20 + j, y * 10 + i)].UnicodeChar = c;
            f[convert(x * 20 + j+1, y * 10 + i)].UnicodeChar = c;

            f[convert(x * 20 + j, y * 10 + i)].Attributes = _xColor;
            f[convert(x * 20 + j + 1, y * 10 + i)].Attributes = _xColor;
        }

        for (int i = 7, j = 2; i >= 1; i--, j+=2) // left bottom to right top
        {
            f[convert(x * 20 + j, y * 10 + i)].UnicodeChar = c;
            f[convert(x * 20 + j+1, y * 10 + i)].UnicodeChar = c;

            f[convert(x * 20 + j, y * 10 + i)].Attributes = _xColor;
            f[convert(x * 20 + j + 1, y * 10 + i)].Attributes = _xColor;
        }
    }
}
