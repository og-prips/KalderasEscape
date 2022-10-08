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

        public string Description;
        public string Name;

        public Room(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Connects two rooms
        /// </summary>
        /// <param name="room">The room to be connected</param>
        /// <param name="direction">The direction of where the room will be</param>
        /// <param name="doorLocked">Specifies wether the door should be locked or open</param>
        public void ConnectTo(Room room, Direction direction, bool doorLocked)
        {
            switch (direction)
            {
                case Direction.North:
                    NorthRoom = room;
                    room.SouthRoom = this;
                    CreateNorthSouthDoor(room, this, doorLocked);
                    break;

                case Direction.South:
                    SouthRoom = room;
                    room.NorthRoom = this;
                    CreateNorthSouthDoor(this, room, doorLocked);
                    break;

                case Direction.West:
                    WestRoom = room;
                    room.EastRoom = this;
                    CreateWestEastDoor(room, this, doorLocked);
                    break;

                case Direction.East:
                    EastRoom = room;
                    room.WestRoom = this;
                    CreateWestEastDoor(this, room, doorLocked);
                    break;
            }
        }

        private void CreateNorthSouthDoor(Room northRoom, Room southRoom, bool isLocked)
        {
            var door = new Door();
            door.IsLocked = isLocked;

            northRoom.SouthDoor = door;
            southRoom.NorthDoor = door;
        }

        private void CreateWestEastDoor(Room westRoom, Room eastRoom, bool isLocked)
        {
            var door = new Door();
            door.IsLocked = isLocked;

            westRoom.EastDoor = door;
            eastRoom.WestDoor = door;
        }
    }
}
