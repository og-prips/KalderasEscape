namespace KalderasEscape.GameClasses
{
    internal class Room
    {
        public Room? NorthRoom;
        public Room? SouthRoom;
        public Room? WestRoom;
        public Room? EastRoom;

        public Door? NorthDoor;
        public Door? SouthDoor;
        public Door? WestDoor;
        public Door? EastDoor;

        public List<Item> Items = new List<Item>();

        public string Description;
        public string Name;

        public Room(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Connects two rooms with an unlocked door
        /// </summary>
        /// <param name="room">The room to be connected</param>
        /// <param name="direction">The direction of where the room will be</param>
        public void ConnectTo(Room room, Direction direction)
        {
            switch (direction)
            {
                case Direction.North:
                    NorthRoom = room;
                    room.SouthRoom = this;
                    CreateNorthSouthDoor(room, this);
                    break;

                case Direction.South:
                    SouthRoom = room;
                    room.NorthRoom = this;
                    CreateNorthSouthDoor(this, room);
                    break;

                case Direction.West:
                    WestRoom = room;
                    room.EastRoom = this;
                    CreateWestEastDoor(room, this);
                    break;

                case Direction.East:
                    EastRoom = room;
                    room.WestRoom = this;
                    CreateWestEastDoor(this, room);
                    break;
            }
        }

        /// <summary>
        /// Connects two rooms with a locked door
        /// </summary>
        /// <param name="room">The room to be connected</param>
        /// <param name="direction">The direction of where the room will be</param>
        public void ConnectTo(Room room, Direction direction, Item key)
        {
            switch (direction)
            {
                case Direction.North:
                    NorthRoom = room;
                    room.SouthRoom = this;
                    CreateNorthSouthDoor(room, this, key);
                    break;

                case Direction.South:
                    SouthRoom = room;
                    room.NorthRoom = this;
                    CreateNorthSouthDoor(this, room, key);
                    break;

                case Direction.West:
                    WestRoom = room;
                    room.EastRoom = this;
                    CreateWestEastDoor(room, this, key);
                    break;

                case Direction.East:
                    EastRoom = room;
                    room.WestRoom = this;
                    CreateWestEastDoor(this, room, key);
                    break;
            }
        }

        private void CreateNorthSouthDoor(Room northRoom, Room southRoom)
        {
            var door = new Door();
            northRoom.SouthDoor = door;
            southRoom.NorthDoor = door;
        }

        private void CreateWestEastDoor(Room westRoom, Room eastRoom)
        {
            var door = new Door();
            westRoom.EastDoor = door;
            eastRoom.WestDoor = door;
        }

        private void CreateNorthSouthDoor(Room northRoom, Room southRoom, Item key)
        {
            var door = new Door(key);

            northRoom.SouthDoor = door;
            southRoom.NorthDoor = door;
        }

        private void CreateWestEastDoor(Room westRoom, Room eastRoom, Item key)
        {
            var door = new Door(key);

            westRoom.EastDoor = door;
            eastRoom.WestDoor = door;
        }
    }
}
