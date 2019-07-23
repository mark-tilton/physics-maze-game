using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts
{
    public class Maze
    {
        public int CellCountX = 10;
        public int CellCountZ = 10;

        public Cell[,] Cells { get; }
        private readonly Stack<Cell> _cellStack = new Stack<Cell>();
        private readonly HashSet<Cell> _visitedCells = new HashSet<Cell>();
        private static readonly Random _random = new Random();

        public Maze()
        {
            Cells = new Cell[CellCountX, CellCountZ];
            for (var x = 0; x < CellCountX; x++)
            {
                for (var z = 0; z < CellCountZ; z++)
                {
                    Cells[x, z] = new Cell(x, z);
                }
            }
        }

        public void Generate()
        {
            // pick a random cell to start at
            // while the stack has cells on it
                // find the cells adjacent to the current cell
                // if there are unvisited adjacent cells
                    // add the current cell to the stack
                    // pick one at random
                    // break the wall between the current cell and the picked cell
                    // switch the current cell to the randomly picked one
                // if there are not any unvisited adjacent cells
                    // pop a cell off of the stack and make it the new current cell
        }

        private void BreakWall(Cell cell, Direction direction)
        {
            cell.Walls[direction] = false;
            GetAdjacentCell(cell, direction).Walls[GetOppositeDirection(direction)] = false;
        }

        private Direction GetOppositeDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return Direction.Down;
                case Direction.Down:
                    return Direction.Up;
                case Direction.Left:
                    return Direction.Right;
                case Direction.Right:
                    return Direction.Left;
                default:
                    throw new IndexOutOfRangeException();
            }
        }

        private Cell GetRandomCell()
        {
            return Cells[_random.Next(CellCountX), _random.Next(CellCountZ)];
        }
        
        private Cell GetAdjacentCell(Cell cell, Direction direction)
        {
            if (direction == Direction.Up && cell.Z + 1 < CellCountZ)
            {
                return Cells[cell.X, cell.Z + 1];
            }
            if (direction == Direction.Down && cell.Z > 0)
            {
                return Cells[cell.X, cell.Z - 1];
            }
            if (direction == Direction.Left && cell.X > 0)
            {
                return Cells[cell.X - 1, cell.Z];
            }
            if (direction == Direction.Right && cell.X + 1 < CellCountX)
            {
                return Cells[cell.X + 1, cell.Z];
            }
            return null;
        }

        private Dictionary<Direction, Cell> GetValidAdjacentCells(Cell cell)
        {
            var adjacentCells = new Dictionary<Direction, Cell>();
            foreach (var direction in Enum.GetValues(typeof(Direction)).Cast<Direction>())
            {
                var adjacentCell = GetAdjacentCell(cell, direction);
                if (adjacentCell != null && !_visitedCells.Contains(adjacentCell))
                {
                    adjacentCells[direction] = adjacentCell;
                }
            }
            return adjacentCells;        
        }
    }
}