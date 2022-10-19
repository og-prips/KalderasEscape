namespace KalderasEscape.GameClasses.Items
{
    internal class SpaceHelmet : Item
    {
        public SpaceHelmet(string name, string description) : base(name, description)
        {
        }

        public override List<string> GetOptions(Player player)
        {
            var currentActions = base.GetOptions(player);

            if (player.Inventory.Contains(this))
            {
                currentActions.Add("Combine");
            }

            return currentActions;
        }
    }
}
