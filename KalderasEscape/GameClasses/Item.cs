namespace KalderasEscape.GameClasses
{
    internal class Item
    {
        public Guid ID;
        public string Name;
        public string Description;

        public List<Item> PairableItems;
        public Item MergedItem;

        //public Item MergeWith(Item item)
        //{
        //    if (PairableItems.Contains(item))
        //    {
        //        return MergedItem;
        //    }
        //}
    }
}
