namespace KalderasEscape.GameClasses.Items
{
    internal class PartialSpaceSuit : Item
    {
        public PartialSpaceSuit(string name, string description) : base(name, description)
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
