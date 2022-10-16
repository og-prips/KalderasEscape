namespace KalderasEscape.GameClasses
{
    public class Room
    {
        public Room? NorthRoom;
        public Room? SouthRoom;
        public Room? WestRoom;
        public Room? EastRoom;

        public Door? NorthDoor;
        public Door? SouthDoor;
        public Door? WestDoor;
        public Door? EastDoor;

        public List<Item>? Items = new List<Item>();
        public List<Room> ConnectedRooms = new List<Room>();

        public string Description;
        public string Name;
        public bool IsEndPoint = false;

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
        public void ConnectTo(Room room, Direction direction, Item key )
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

        public void SetConnectedRooms()
        {
            if (NorthRoom != null) ConnectedRooms.Add(NorthRoom);
            if (SouthRoom != null) ConnectedRooms.Add(SouthRoom);
            if (WestRoom != null) ConnectedRooms.Add(WestRoom);
            if (EastRoom != null) ConnectedRooms.Add(EastRoom);
        }

        public Door GetDoorByRoom(Room room)
        {
            if (room == NorthRoom) return NorthDoor;
            else if (room == SouthRoom) return SouthDoor;
            else if (room == WestRoom) return WestDoor;
            else return EastDoor;
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
