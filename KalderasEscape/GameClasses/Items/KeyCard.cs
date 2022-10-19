using Sharprompt;

namespace KalderasEscape.GameClasses.Items
{
    public class KeyCard : Item
    {
        public KeyCard(string name, string description) : base(name, description)
        {
        }

        //public override string Name { get; set; } = "Blood stained keycard";
        //public override string Description { get; set; } = "This could be useful for getting out of here";

        public override void PerformAction(Player player, string action)
        {
            base.PerformAction(player, action);

            switch (action)
            {
                case "Use":
                    Use(player.CurrentRoom);
                    break;
            }
        }

        public override List<string> GetOptions(Player player)
        {
            var currentActions = base.GetOptions(player);

            if (player.Inventory.Contains(this))
            {
                currentActions.Add("Use");
            }

            return currentActions;
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
    }
}
