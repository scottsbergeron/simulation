using System;

namespace CitySimGame
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new CitySimGame())
                game.Run();
        }
    }
}
