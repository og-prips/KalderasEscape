using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace KalderasEscape.GameClasses
{
    public enum Direction
    {
        North = 'n',
        South = 's',
        West = 'w',
        East = 'e'
    }

    internal class Game
    {
        Player player = new Player();

        Room s = new Room("starting room");
        Room a = new Room("room a");
        Room b = new Room("room b");
        Room c = new Room("room c");

        public Game()
        {
            player.CurrentRoom = s;
            s.ConnectTo(b, Direction.North, false);
            b.ConnectTo(a, Direction.West, false);
            b.ConnectTo(c, Direction.East, false);
        }

        public void InitializeGame()
        {
            Program.WriteLineFalling("welcome!");

            // provide player with information about the game (story, help-commands etc...)
        }

        public void StartGame()
        {
            while (true)
            {
                Program.WriteLineFalling("enter a direction (n, s, w, e)");
                var command = Console.ReadLine().ToLower();

                switch (command)
                {
                    case "exit":
                        Program.Exit();
                        break;

                    case "n":
                        MovePlayer(Direction.North);
                        break;

                    case "s":
                        MovePlayer(Direction.South);
                        break;

                    case "w":
                        MovePlayer(Direction.West);
                        break;

                    case "e":
                        MovePlayer(Direction.East);
                        break;

                    default:
                        Program.WriteLineFalling("i did not quite get that...");
                        break;
                }
            }
        }

        private void MovePlayer(Direction direction)
        {
            switch (direction)
            {
                case Direction.North:
                    if (player.CurrentRoom.NorthRoom != null)
                    {
                        player.CurrentRoom = player.CurrentRoom.NorthRoom;
                        Program.WriteLineFalling(player.CurrentRoom.Description);
                        break;
                    }
                    Program.WriteLineFalling("There seems to be an impenetratable wall here...");
                    break;

                case Direction.South:
                    if (player.CurrentRoom.SouthRoom != null)
                    {
                        player.CurrentRoom = player.CurrentRoom.SouthRoom;
                        Program.WriteLineFalling(player.CurrentRoom.Description);
                        break;
                    }
                    Program.WriteLineFalling("There seems to be an impenetratable wall here...");
                    break;

                case Direction.West:
                    if (player.CurrentRoom.WestRoom != null)
                    {
                        player.CurrentRoom = player.CurrentRoom.WestRoom;
                        Program.WriteLineFalling(player.CurrentRoom.Description);
                        break;
                    }
                    Program.WriteLineFalling("There seems to be an impenetratable wall here...");
                    break;

                case Direction.East:
                    if (player.CurrentRoom.EastRoom != null)
                    {
                        player.CurrentRoom = player.CurrentRoom.EastRoom;
                        Program.WriteLineFalling(player.CurrentRoom.Description);
                        break;
                    }
                    Program.WriteLineFalling("There seems to be an impenetratable wall here...");
                    break;
            }
        }
    }
}
