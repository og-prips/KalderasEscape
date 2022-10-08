using Sharprompt;

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
        Player player;

        Room startingRoom;
        Room room1;
        Room room2;
        Room room3;
        Room room4;
        Room room5;
        Room endingRoom;

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

            startingRoom.ConnectTo(room1, Direction.North, false);
            room1.ConnectTo(room2, Direction.West, false);
            room1.ConnectTo(room4, Direction.East, false);
            room1.ConnectTo(endingRoom, Direction.North, true);
            room2.ConnectTo(room3, Direction.South, false);
            room4.ConnectTo(room5, Direction.South, false);
        }

        public void InitializeGame()
        {
            
            Program.WriteLineFalling("welcome!");

            // provide player with information about the game (story, help-commands etc...)
        }

        public void PlayGame()
        {
            var options = new string[] { "Look", "Go", "Inventory", "Exit" };
            var action = Prompt.Select("What do I want to do?", options);

            switch (action)
            {
                case "Look":
                    //describe room
                    break;

                case "Go":
                    MovePlayer();
                    PlayGame();
                    break;

                case "Inventory":
                    //display inventory
                    break;

                case "Exit":
                    if (Prompt.Confirm("Are you sure?")) Program.Exit();
                    else PlayGame();
                    break;
            }
        }

        private void MovePlayer()
        {
            var options = new string[] 
            {   
                Direction.North.ToString(),
                Direction.South.ToString(),
                Direction.West.ToString(),
                Direction.East.ToString() 
            };

            Enum.TryParse(Prompt.Select("Where do I want to go?", options), out Direction direction);
            player.Navigate(direction);
        }
    }
}
