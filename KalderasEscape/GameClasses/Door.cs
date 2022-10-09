namespace KalderasEscape.GameClasses
{
    internal class Door
    {
        public Item? Key { get; set; }
        public bool IsLocked { get; set; }
        
        /// <summary>
        /// Creates an open door
        /// </summary>
        public Door()
        {
            IsLocked = false;
        }

        /// <summary>
        /// Creates a locked door with a matching key
        /// </summary>
        /// <param name="key"></param>
        public Door(Item key)
        {
            Key = key;
            IsLocked = true;
        }
    }
}
