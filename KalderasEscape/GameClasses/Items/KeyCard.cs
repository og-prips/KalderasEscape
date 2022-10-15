using Sharprompt;

namespace KalderasEscape.GameClasses.Items
{
    public class KeyCard : Item
    {
        public override string Name { get; set; } = "Blood stained keycard";
        public override string Description { get; set; } = "This could be useful for getting out of here";
        public override List<string> Actions { get; set; }

        //public KeyCard()
        //{
        //    Actions.Add("Inspect");
        //    Actions.Add("Pick up");
        //}

        public override void PerformAction(Player player, string action)
        {
            switch (action)
            {
                case "Inspect":
                    Inspect();
                    break;

                case "Use":
                    Use(player.CurrentRoom);
                    break;

                case "Pick up":
                    player.PickUp(this);
                    break;

                case "Drop":
                    player.Drop(this);
                    break;
            }
        }

        private void Use(Room room)
        {
            var roomToUnlock = Prompt.Select($"Use '{Name}' to unlock", room.ConnectedRooms
                .Where(room => room != null).Select(room => room.Name)
                .ToArray());

            var door = room.GetDoorByRoom(room.ConnectedRooms.Where(room => room.Name == roomToUnlock).Single());

            if (door.IsLocked && this == door.Key)
            {
                door.IsLocked = false;
                Program.WriteLineFalling($"You unlock {roomToUnlock}");
            }
            else if (door.IsLocked && this != door.Key)
            {
                Program.WriteLineFalling($"{Name} does not match {roomToUnlock}");
            }
            else
            {
                Program.WriteLineFalling("That door is already unlocked");
            }
        }

        private void Inspect()
        {
            Program.WriteLineFalling(Description);
        }
    }
}
