namespace KalderasEscape.GameClasses
{
    internal class Player
    {
        public Room CurrentRoom;

        public Player(Room currentRoom)
        {
            CurrentRoom = currentRoom;
        }

        public void Navigate(Direction direction)
        {
            var roomToEnter = direction switch
            {
                Direction.North => CurrentRoom.NorthRoom,
                Direction.South => CurrentRoom.SouthRoom,
                Direction.West => CurrentRoom.WestRoom,
                Direction.East => CurrentRoom.EastRoom
            };

            var doorToEnter = direction switch
            {
                Direction.North => CurrentRoom.NorthDoor,
                Direction.South => CurrentRoom.SouthDoor,
                Direction.West => CurrentRoom.WestDoor,
                Direction.East => CurrentRoom.EastDoor
            };

            if (roomToEnter == null)
            {
                Program.WriteLineFalling("I cannot move there, a wall is in the way...");
            }
            else if (doorToEnter.IsLocked)
            {
                Program.WriteLineFalling("door is locked");
            }
            else
            {
                CurrentRoom = roomToEnter;
            }
        }
    }
}
