namespace KalderasEscape.GameClasses
{
    public class Player
    {
        public string Name;
        public Room CurrentRoom;
        public List<Item> Inventory;

        public Player(Room currentRoom)
        {
            CurrentRoom = currentRoom;
        }

        public void Navigate(Direction direction)
        {
            var roomToEnter = RoomToEnter(direction);

            var doorToEnter = DoorToEnter(direction);

            if (roomToEnter == null)
            {
                Program.WriteLineFalling("I cannot move there, a wall is in the way...");
            }
            else if (doorToEnter.IsLocked)
            {
                Program.WriteLineFalling("The door is locked");
            }
            else
            {
                CurrentRoom = roomToEnter;
                Program.WriteLineFalling($"You go into {CurrentRoom.Name}");
            }
        }

        public Room? RoomToEnter(Direction direction)
        {
            var roomToEnter = direction switch
            {
                Direction.North => CurrentRoom.NorthRoom,
                Direction.South => CurrentRoom.SouthRoom,
                Direction.West => CurrentRoom.WestRoom,
                Direction.East => CurrentRoom.EastRoom
            };

            return roomToEnter;
        }

        public Door DoorToEnter(Direction direction)
        {
            var doorToEnter = direction switch
            {
                Direction.North => CurrentRoom.NorthDoor,
                Direction.South => CurrentRoom.SouthDoor,
                Direction.West => CurrentRoom.WestDoor,
                Direction.East => CurrentRoom.EastDoor
            };

            return doorToEnter;
        }

        public void PickUp(Item item)
        {
            Inventory.Add(item);
            CurrentRoom.Items.Remove(item);

            Program.WriteLineFalling($"You pick up '{item.Name}'");
        }

        public void Drop(Item item)
        {
            Inventory.Remove(item);
            CurrentRoom.Items.Add(item);

            Program.WriteLineFalling($"You drop '{item.Name}'");
        }
    }
}
