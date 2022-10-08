namespace KalderasEscape.GameClasses
{
    internal class Door
    {
        public Key? MatchingKey { get; set; }
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
        /// <param name="matchingKey"></param>
        public Door(Key matchingKey)
        {
            MatchingKey = matchingKey;
            IsLocked = true;
        }
    }
}
