using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalderasEscape.GameClasses
{
    public enum Direction
    {
        North,
        South,
        West,
        East
    }

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
            if (!isLocked)
            {
                var door = new Door();

                northRoom.SouthDoor = door;
                southRoom.NorthDoor = door;
            }
            else
            {
                // create locked door
            }
        }

        private void CreateWestEastDoor(Room westRoom, Room eastRoom, bool isLocked)
        {
            if (!isLocked)
            {
                var door = new Door();

                westRoom.EastDoor= door;
                eastRoom.WestDoor = door;
            }
            else
            {
                // create locked door
            }
        }
    }
}
