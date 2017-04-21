using System;
using System.Collections.Generic;

public static class MazeGeneratorDepthFirst
{

    public static Cell[,] _maze;
    private static Random r = new Random(Guid.NewGuid().GetHashCode());

    private static int _size;
    private static Stack<Cell> _stack = new Stack<Cell>();
    private static int visitedCells = 0;
    static Cell curCell;

    public static Cell GenerateMaze(int size)
    {
        _size = size;
        _maze = new Cell[size, size];

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                _maze[i, j] = new Cell(i, j);
            }
        }


        _maze[0, 0] = new Cell(0, 0);
        curCell = _maze[0, 0];
        curCell.IsVisited = true;

        //while(visitedCells < _size)
        //{
        //    var ne = GetUnvisitedNeighbours(curCell);

        //    if(ne.Count > 0)
        //    {
        //        _stack.Push(curCell);
        //        Cell chosen = ne[r.Next(0, ne.Count)];
        //        chosen.Walls[GetDirection(curCell, chosen)] = 1;
        //        chosen.IsVisited = true;
        //        curCell = chosen;
        //        visitedCells++;
        //    }
        //    else
        //    {
        //        if(_stack.Count > 0)
        //        {
        //            curCell = _stack.Pop();
        //        }
        //    }
        //}

        return curCell;
    }

    public static Cell GenerateNext()
    {
        if (visitedCells <= _size * _size)
        {
            var ne = GetUnvisitedNeighbours(curCell);

            if (ne.Count > 0)
            {
                _stack.Push(curCell);
                Cell chosen = ne[r.Next(0, ne.Count)];
                chosen.Walls[GetDirection(curCell, chosen)] = 1;
                curCell.Walls[GetDirection(chosen, curCell)] = 1;
                chosen.IsVisited = true;
                curCell = chosen;
                visitedCells++;
                return curCell;
            }
            else
            {
                if (_stack.Count > 0)
                {
                    curCell = _stack.Pop();
                    return curCell;
                }
            }
        }
        return null;
    }

    private static int GetDirection(Cell currCell, Cell newCell)
    {
        if (newCell.X < currCell.X) return 2;
        if (newCell.Y < currCell.Y) return 3;
        if (newCell.X > currCell.X) return 0;

        return 1;
    }

    private static List<Cell> GetUnvisitedNeighbours(Cell c)
    {
        List<Cell> unvisited = new List<Cell>();
        for (int i = 0; i < 4; i++)
        {
            switch (i)
            {
                case 0:
                    if (c.X > 0 && !_maze[c.X - 1, c.Y].IsVisited) unvisited.Add(_maze[c.X - 1, c.Y]);
                    break;
                case 1:
                    if (c.Y > 0 && !_maze[c.X, c.Y - 1].IsVisited) unvisited.Add(_maze[c.X, c.Y - 1]);
                    break;
                case 2:
                    if (c.X < _size - 1 && !_maze[c.X + 1, c.Y].IsVisited) unvisited.Add(_maze[c.X + 1, c.Y]);
                    break;
                case 3:
                    if (c.Y < _size - 1 && !_maze[c.X, c.Y + 1].IsVisited) unvisited.Add(_maze[c.X, c.Y + 1]);
                    break;
            }
        }
        return unvisited;
    }


    public class Cell
    {
        /*
         * 0 left
         * 1 up
         * 2 right
         * 3 down
         */
        public int[] Walls = new int[4];
        public bool IsVisited = false;
        public int X, Y;

        public Cell(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
