﻿using KalderasEscape.GameClasses;

namespace KalderasEscape
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();

            game.InitializeGame();
            game.StartGame();
        }

        public static void WriteLineFalling(string value)
        {
            foreach (char c in value)
            {
                Console.Write(c);
                Thread.Sleep(20);
            }
            Console.WriteLine();
        }

        public static void Exit()
        {
            // save to files then close
            WriteLineFalling("Shutting down...");
            Environment.Exit(0);
        }
    }
}