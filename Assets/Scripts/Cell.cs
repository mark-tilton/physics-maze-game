using System.Collections.Generic;

namespace Assets.Scripts
{
    public class Cell
    {
        public int X { get; }
        public int Z { get; }
        public Dictionary<Direction, bool> Walls { get; }
        public FloorType FloorType { get; set; }

        public Cell(int x, int y)
        {
            X = x;
            Z = y;
            Walls = new Dictionary<Direction, bool>
            {
                [Direction.Up] = true,
                [Direction.Down] = true,
                [Direction.Left] = true,
                [Direction.Right] = true,
            };
        }
    }
}
