using KalderasEscape.GameClasses;
using Spectre.Console;

namespace KalderasEscape
{
    public class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.Name = "Kalderas Escape";

            AnsiConsole.Write(
            new FigletText(game.Name)
                .Centered()
                .Color(Color.Orange1));

            game.StartGame();
        }

        public static void WriteLineFalling(string value)
        {
            var sleepInterval = 20;

            if (value.Length > 40) sleepInterval = 10;

            foreach (char c in value)
            {
                Console.Write(c);
                Thread.Sleep(sleepInterval);
            }
            Console.WriteLine("\n");
        }

        public static void Exit()
        {
            WriteLineFalling("Shutting down...");
            Environment.Exit(0);
        }
    }
}