using System;
using System.Runtime.InteropServices;

//pinvoke magic happening below
//if you don't know what that all is and where it comes from. see http://pinvoke.net and https://docs.microsoft.com/en-us/windows/console/writeconsoleoutput
public struct SMALL_RECT
{
    public SMALL_RECT(short Left, short Top, short Right, short Bottom)
    {
        this.Left = Left;
        this.Top = Top; 
        this.Right = Right;
        this.Bottom = Bottom;
    }
    public short Left;
    public short Top;
    public short Right;
    public short Bottom;

}
[StructLayout(LayoutKind.Sequential)]
public struct COORD
{
    public short X;
    public short Y;

    public COORD(short X, short Y)
    {
        this.X = X;
        this.Y = Y;
    }
}
[StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)] //encoding seems to make problems
public struct CHAR_INFO
{
    [FieldOffset(0)]
    public char UnicodeChar;
    [FieldOffset(0)]
    public char AsciiChar;
    [FieldOffset(2)] //2 bytes seems to work properly
    public UInt16 Attributes;
}
internal static class ColorSupport
{
    private static SMALL_RECT _small_rect;
    private static int _width = 0, _height = 0;
    private const int outputHandle = -11;
    private static IntPtr handle;
    static ColorSupport()
    {
        handle = GetStdHandle(outputHandle);
    }
    public static void setup(int width, int height) // width and height of the buffer thats going to be displaied
    {
        _width = width;
        _height = height;
        if (!(width > 0 && height > 0))
        {
            throw new ArgumentException("width and/or height must me grater 0");
        }
        _small_rect =  new SMALL_RECT(0, 0, (short)_width, (short)_height);
    }
    public static void displayFrame(CHAR_INFO[] input)
    {
        if (!WriteConsoleOutput(handle, input, new COORD((short)_width, (short)_height),new COORD(0,0),ref _small_rect))
        {
            Console.WriteLine("Latest Win32Error: " + Marshal.GetLastWin32Error());
        }
    }
    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr GetStdHandle(int nStdHandle);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool WriteConsoleOutput(
    IntPtr hConsoleOutput,
    CHAR_INFO[] lpBuffer,
    COORD dwBufferSize,
    COORD dwBufferCoord,
    ref SMALL_RECT lpWriteRegion
    );
}
