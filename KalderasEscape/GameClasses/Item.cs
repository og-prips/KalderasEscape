namespace KalderasEscape.GameClasses
{
    internal class Item
    {
        public Guid ID { get; set; }
        public string Name { get; set; }

        public List<Item> PairableItems { get; set; }

        //public Item Merge(Item a)
        //{
        //    if (PairableItems.Contains(a))
        //    {
        //    }
        //}
    }
}
