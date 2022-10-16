using KalderasEscape.GameClasses;

namespace KalderasEscape
{
    public class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();

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