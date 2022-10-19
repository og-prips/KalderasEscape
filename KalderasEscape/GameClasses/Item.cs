namespace KalderasEscape.GameClasses
{
    public class Item
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual Item CombineItem { get; set; }
        public virtual Item CompleteItem { get; set; }

        public Item(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public virtual void PerformAction(Player player, string action)
        {
            switch (action)
            {
                case "Inspect":
                    Inspect();
                    break;

                case "Pick up":
                    player.PickUp(this);
                    break;

                case "Drop":
                    player.Drop(this);
                    break;
            }
        }

        public virtual List<string> GetOptions(Player player)
        {
            var currentOptions = new List<string>();

            currentOptions.Add("Inspect");

            if (player.Inventory.Contains(this))
            {
                currentOptions.Add("Drop");
            }
            else
            {
                currentOptions.Add("Pick up");
            }

            return currentOptions;
        }

        public void SetCombineItem(Item combineItem, Item completeItem)
        {
            CombineItem = combineItem;
            combineItem.CombineItem = this;

            CompleteItem = completeItem;
            combineItem.CompleteItem = completeItem;
        }

        public Item? CombineWith(Item item)
        {
            if (item.CombineItem == this)
            {
                return CompleteItem;
            }
            else
            {
                return null;
            }
        }

        public void Inspect()
        {
            Program.WriteLineFalling(Description);
        }
    }
}
