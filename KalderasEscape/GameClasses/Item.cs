namespace KalderasEscape.GameClasses
{
    public abstract class Item
    {
        public abstract string Name { get; set; }
        public abstract string Description { get; set; }
        public virtual List<string> Actions { get; set; }
        public virtual List<Item> PairableItems { get; set; }
        public virtual Item MergedItem { get; set; }

        //public Item()
        //{
        //    Actions.Add("Insect");
        //    Actions.Add("Pick up");
        //}

        public virtual void PerformAction(Player player, string action)
        {
            switch (action)
            {
                case "Inspect":
                    Inspect();
                    break;

                case "Pick up":
                    player.PickUp(this);
                    UpdateActions("Pick up", "Drop");
                    break;

                case "Drop":
                    player.Drop(this);
                    UpdateActions("Drop", "Pick up");
                    break;
            }
        }

        private void Inspect()
        {
            Program.WriteLineFalling(Description);
        }

        private void UpdateActions(string oldValue, string newValue)
        {
            foreach (var value in Actions)
            {
                value.Replace(oldValue, newValue);
            }
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
