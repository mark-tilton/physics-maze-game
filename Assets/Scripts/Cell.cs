using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public class Cell
    {
        public int X { get; }
        public int Y { get; }
        public Dictionary<Direction, bool> Walls { get; }

        public Cell(int x, int y)
        {
            X = x;
            Y = y;
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
