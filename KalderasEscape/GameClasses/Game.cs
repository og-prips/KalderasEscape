using KalderasEscape.GameClasses.Items;
using Sharprompt;
using System;
using System.ComponentModel.DataAnnotations;
using static System.Collections.Specialized.BitVector32;

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
        Room commandRoom;
        Room passageRoom;
        Room utilityRoom;
        Room room3;
        Room sleepingRoom;
        Room room5;
        Room endingRoom;

        List<Room> map;

        Player player;

        Item endingRoomKeyCard;
        Item spaceHelmet;
        Item partialSpaceSuit;
        Item spaceSuit;

        public Game()
        {
            InitializeItems();
            InitializeRooms();
            InitializePlayer();
        }

        public void StartGame()
        {
            string startingMessage =
                "Welcome to Kalderas Escape!\nA text-adventure game where you will be able to move around and explore your crashed spaceship in an attempt to survive\n\n";
            Program.WriteLineFalling(startingMessage);

            player.Name = Prompt.Input<string>("Enter a name for your character");

            startingMessage =
                "You and your team were on a mission to explore planet Kaldera, a planet found in the nearest solar system which seemed habitable for life.\n" +
                $"Things turned horribly wrong once you entered the atmosphere resulting in a violent crash where you, {player.Name}, remain the only survivor...";
            Program.WriteLineFalling(startingMessage);

            ShowMainMenu();
        }

        private void EndGame()
        {
            var endingMessage =
                "You head out into Kaldera, a brand new world for you to explore\n\n--- THE END ---";

            Program.WriteLineFalling(endingMessage);

            if (Prompt.Confirm("Do you want to replay the game?"))
            {
                ResetGame();
                StartGame();
            }
            else
            {
                Program.Exit();
            }
        }

        private void ResetGame()
        {
            InitializeItems();
            InitializeRooms();
            InitializePlayer();
        }

        // Creates and sets parameters
        private void InitializeItems()
        {
            endingRoomKeyCard = new KeyCard();
            spaceHelmet = new SpaceHelmet();
            partialSpaceSuit = new PartialSpaceSuit();
            spaceSuit = new SpaceSuit();

            spaceHelmet.SetCombineItem(partialSpaceSuit, spaceSuit);
        }

        // Creates rooms and sets parameters. Connects the rooms to build a map
        private void InitializeRooms()
        {
            commandRoom = new Room("Command Room");
            passageRoom = new Room("Passage Room");
            utilityRoom = new Room("Utility Room");
            sleepingRoom = new Room("Sleeping Room");
            endingRoom = new Room("Kaldera");

            endingRoom.IsEndPoint = true;

            commandRoom.Description =
                "The command room, where you were seated during the crash. It's no pleasant sight...\nYou see a keycard lying on the floor amidst all chaos";
            passageRoom.Description = 
                "This is the room which separates you from the open world, to the west of you is the Utility room and to the east lies the Sleeping room";
            utilityRoom.Description = 
                "In here lies a bunch of equipment, most of which is broken. Though one space suit lies intact, though it's missing a helmet";
            sleepingRoom.Description = 
                "The room where your crew headed for rest. One of your crew members space helmet lies beside one of the beds, it could be useful";

            commandRoom.ConnectTo(passageRoom, Direction.North);
            passageRoom.ConnectTo(utilityRoom, Direction.West);
            passageRoom.ConnectTo(sleepingRoom, Direction.East);
            passageRoom.ConnectTo(endingRoom, Direction.North, endingRoomKeyCard); ;

            commandRoom.Items.Add(endingRoomKeyCard);
            sleepingRoom.Items.Add(spaceHelmet);
            utilityRoom.Items.Add(partialSpaceSuit);

            map = new List<Room>();

            map.Add(commandRoom);
            map.Add(passageRoom);
            map.Add(utilityRoom);
            map.Add(sleepingRoom);
            map.Add(endingRoom);

            foreach (var room in map)
            {
                room.SetConnectedRooms();
            }
        }

        // Creates a player
        private void InitializePlayer()
        {
            player = new Player(commandRoom);
            player.Inventory = new List<Item>();
        }

        private void ShowMainMenu()
        {
            var options = new string[]
            {
                "Look",
                "Go",
                "Inventory",
                "Exit"
            };

            var action = Prompt.Select($"What does {player.Name} want to do?", options);

            switch (action)
            {
                case "Look":
                    LookAtRoom();
                    break;

                case "Go":
                    MovePlayer();
                    break;

                case "Inventory":
                    ShowInventory();
                    break;

                case "Exit":
                    if (Prompt.Confirm("Are you sure?")) Program.Exit();
                    else ShowMainMenu();
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
                ShowMainMenu();
            }
            else
            {
                var item = player.CurrentRoom.Items.Where(item => item.Name == action).FirstOrDefault();
                LookAtItem(item);
            }
        }

        private void LookAtItem(Item selectedItem)
        {
            var options = selectedItem.GetOptions(player);
            options.Add("Main menu");

            var action = Prompt.Select($"What does {player.Name} want to do with '{selectedItem.Name}'?", options);

            if (action == "Main menu")
            {
                ShowMainMenu();
            }
            else if (action == "Combine")
            {
                ShowCombineMenu(selectedItem);
            }
            else
            {
                selectedItem.PerformAction(player, action);
                LookAtItem(selectedItem);
            }
        }

        private void ShowCombineMenu(Item selectedItem)
         {
            var options = new List<string>();

            player.Inventory.ForEach(item => options.Add(item.Name));
            options.Remove(selectedItem.Name);

            if (options.Count.Equals(0))
            {
                Program.WriteLineFalling("You have no other items to combine this with...");
                LookAtItem(selectedItem);
            }
            else
            {
                var action = Prompt.Select("Combine with", options);
                var itemToCombineWith = player.Inventory.First(item => item.Name.Equals(action));

                CombineItems(selectedItem, itemToCombineWith);
            }
        }

        private void CombineItems(Item item1, Item item2)
        {
            var newItem = item1.CombineWith(item2);

            if (newItem != null)
            {
                player.Inventory.Add(newItem);
                player.Inventory.Remove(item1);
                player.Inventory.Remove(item2);

                Program.WriteLineFalling($"You combine '{item1.Name}' with '{item2.Name}' and get '{newItem.Name}'");
                LookAtItem(newItem);
            }
            else
            {
                Program.WriteLineFalling($"It seems '{item1.Name}' cannot be combined with '{item2.Name}'");
                LookAtItem(item1);
            }
        }

        private void MovePlayer()
        {
            var direction = Prompt.Select<Direction>("Where do I want to go?");
            player.Navigate(direction);

            if (player.CurrentRoom.IsEndPoint)
            {
                EndGame();
            }
            else
            {
                ShowMainMenu();
            }
        }

        private void ShowInventory()
        {
            var options = new List<string>();

            player.Inventory.ForEach(item => options.Add(item.Name));
            options.Add("Main menu");

            var action = Prompt.Select($"{player.Name}s inventory", options.ToArray());

            if (action == "Main menu")
            {
                ShowMainMenu();
            }
            else
            {
                var selectedItem = player.Inventory.First(item => item.Name.Equals(action));
                LookAtItem(selectedItem);
            }
        }
    }
}
