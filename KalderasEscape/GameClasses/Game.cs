using KalderasEscape.GameClasses.Items;
using Sharprompt;
using System.ComponentModel.DataAnnotations;

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

    public class Game
    {
        Room startingRoom;
        Room room1;
        Room room2;
        Room room3;
        Room room4;
        Room room5;
        Room endingRoom;

        List<Room> map;

        Player player;

        Item endingRoomKeyCard;
        Item spaceHelmet;

        public Game()
        {
            InitializeItems();
            InitializeRooms();
            InitializePlayer();
        }

        public void Start()
        {
            Program.WriteLineFalling("welcome!");
            // provide player with information about the game (story, help-commands etc...)

            MainMenu();
        }

        // Creates and sets parameters
        private void InitializeItems()
        {
            endingRoomKeyCard = new KeyCard();
            spaceHelmet = new SpaceHelmet();
        }

        // Creates rooms and sets parameters. Connects the rooms to build a map
        private void InitializeRooms()
        {
            startingRoom = new Room("start");
            room1 = new Room("room1");
            room2 = new Room("room2");
            room3 = new Room("room3");
            room4 = new Room("room4");
            room5 = new Room("room5");
            endingRoom = new Room("ending");

            startingRoom.Description = 
                "You wake up in a dark room, you feel tired and hurt, not really sure what happened to you.\n\n" +
                "You and your team were on a mission to explore planet Kaldera, a planet found in the nearest solar system which seemed habitable for life\n\n" +
                "The last thing your remember is a violent crash after your ship lost control when entering the atmosphere...";

            room1.Description = "room1";
            room2.Description = "room2";
            room3.Description = "room3";
            room4.Description = "room4";
            room5.Description = "room5";
            endingRoom.Description = "ending room";

            startingRoom.ConnectTo(room1, Direction.North);
            room1.ConnectTo(room2, Direction.West);
            room1.ConnectTo(room4, Direction.East);
            room1.ConnectTo(endingRoom, Direction.North, endingRoomKeyCard); ;
            room2.ConnectTo(room3, Direction.South);
            room4.ConnectTo(room5, Direction.South);

            room1.Items.Add(endingRoomKeyCard);
            startingRoom.Items.Add(spaceHelmet);

            map = new List<Room>();

            map.Add(startingRoom);
            map.Add(room1);
            map.Add(room2);
            map.Add(room3);
            map.Add(room4);
            map.Add(room5);
            map.Add(endingRoom);

            foreach (var room in map)
            {
                room.SetConnectedRooms();
            }
        }

        // Creates a player
        private void InitializePlayer()
        {
            player = new Player(startingRoom);
            player.Inventory = new List<Item>();
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
                    LookAtRoom();
                    break;

                case "Go":
                    MovePlayer();
                    MainMenu();
                    break;

                case "Inventory":
                    player.Inventory.Add(endingRoomKeyCard);
                    MainMenu();
                    break;

                case "Exit":
                    if (Prompt.Confirm("Are you sure?")) Program.Exit();
                    else MainMenu();
                    break;
            }
        }

        private void LookAtRoom()
        {
            var action = "";
            var options = new string[player.CurrentRoom.Items.Count + 1];
            
            for (int i = 0; i < player.CurrentRoom.Items.Count; i++)
            {
                options[i] = player.CurrentRoom.Items[i].Name;
            }
            options[player.CurrentRoom.Items.Count] = "Main menu";

            Program.WriteLineFalling(player.CurrentRoom.Description);
            action = Prompt.Select("Go to...", options);

            if (action == "Main menu")
            {
                MainMenu();
            }
            else
            {
                var item = player.CurrentRoom.Items.Where(item => item.Name == action).FirstOrDefault();
                LookAtItem(item);
            }
        }

        private void LookAtItem(Item item)
        {
            var options = new string[item.Actions.Length + 1];

            for (int i = 0; i < item.Actions.Length; i++)
            {
                options[i] = item.Actions[i];
            }
            options[item.Actions.Length] = "Main menu";

            var action = Prompt.Select($"What do you want to do with '{item.Name}'?", options);

            if (action == "Main menu")
            {
                MainMenu();
            }
            else
            {
                item.PerformAction(player, action);
                LookAtItem(item);
            }
        }

        private void MovePlayer()
        {
            var direction = Prompt.Select<Direction>("Where do I want to go?");
            player.Navigate(direction);
        }

        private void ShowInventory()
        {

        }
    }
}
