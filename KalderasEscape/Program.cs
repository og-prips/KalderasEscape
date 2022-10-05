using KalderasEscape.GameClasses;

namespace KalderasEscape
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            Room s = new Room();
            Room a = new Room();
            Room b = new Room();
            Room c = new Room();

            s.ConnectTo(b, Direction.North, false);
            b.ConnectTo(a, Direction.West, false);
            b.ConnectTo(c, Direction.East, false);

            List<Room> Map = new List<Room>();
        }
    }
}