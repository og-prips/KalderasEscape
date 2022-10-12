namespace KalderasEscape.GameClasses
{
    public abstract class Item
    {
        public abstract string Name { get; set; }
        public abstract string Description { get; set; }
        public virtual string[]? Actions { get; set; } = { "Inspect", "Pick up", "Drop" };
        public virtual List<Item> PairableItems { get; set; }
        public virtual Item MergedItem { get; set; }

        public virtual void PerformAction(Player player, string action)
        {
            switch (action)
            {
                case "Inspect":
                    Inspect();
                    break;
            }
        }

        private void Inspect()
        {
            Program.WriteLineFalling(Description);
        }

        //public Item MergeWith(Item item)
        //{
        //    if (PairableItems.Contains(item))
        //    {
        //        return MergedItem;
        //    }
        //}
    }
}
