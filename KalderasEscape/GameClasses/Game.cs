using Sharprompt;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

namespace KalderasEscape.GameClasses
{
    public enum Direction
    {
        [Display(Name = "North")]
        North,
        [Display(Name = "South")]
        South,
        [Display(Name = "West")]
        West,
        [Display(Name = "East")]
        East
    }

    internal class Game
    {
        Room startingRoom;
        Room room1;
        Room room2;
        Room room3;
        Room room4;
        Room room5;
        Room endingRoom;

        Player player;

        Item endingRoomKey;

        public Game()
        {
            startingRoom = new Room("start");
            room1 = new Room("room1");
            room2 = new Room("room2");
            room3 = new Room("room3");
            room4 = new Room("room4");
            room5 = new Room("room5");
            endingRoom = new Room("ending");

            player = new Player(startingRoom);
            player.Inventory = new List<Item>();

            endingRoomKey = new Item();
            endingRoomKey.Name = "Mystical key";

            startingRoom.ConnectTo(room1, Direction.North);
            room1.ConnectTo(room2, Direction.West);
            room1.ConnectTo(room4, Direction.East);
            room1.ConnectTo(endingRoom, Direction.North, endingRoomKey);
            room2.ConnectTo(room3, Direction.South);
            room4.ConnectTo(room5, Direction.South);

            startingRoom.Items.Add(endingRoomKey);
        }

        public void Start()
        {
            Program.WriteLineFalling("welcome!");
            // provide player with information about the game (story, help-commands etc...)

            MainMenu();
        }

        private void MainMenu()
        {
            var options = new string[]
            {
                "Look",
                "Go",
                "Inventory",
                "Exit"
            };

            var action = Prompt.Select("What do I want to do?", options);

            switch (action)
            {
                case "Look":
                    //if (player.CurrentRoom.Items != null)
                    //{
                    //    player.CurrentRoom.Items.ForEach(item => Program.WriteLineFalling(item.Name));
                    //}
                    Look();
                    MainMenu();
                    break;

                case "Go":
                    Go();
                    MainMenu();
                    break;

                case "Inventory":
                    player.Inventory.Add(endingRoomKey);
                    MainMenu();
                    break;

                case "Exit":
                    if (Prompt.Confirm("Are you sure?")) Program.Exit();
                    else MainMenu();
                    break;
            }
        }

        private void Look()
        {
            var options = new string[player.CurrentRoom.Items.Count + 1];
            options[player.CurrentRoom.Items.Count + 1] = "Go back";

            for (int i = 0; i < player.CurrentRoom.Items.Count; i++)
            {
                options[i] = player.CurrentRoom.Items[i].Name;
            }

            var action = Prompt.Select("You see...", options);

            if (action == "Go back")
            {
                MainMenu();
            }
            else
            {
                var item = player.CurrentRoom.Items.Where(item => item.Name == action).FirstOrDefault();
            }
        }

        private void Go()
        {
            var direction = Prompt.Select<Direction>("Where do I want to go?");
            player.Navigate(direction);
        }
    }
}
