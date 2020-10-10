using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeClass
{
    public class Maze
    {
        private int[,] maze; // store maze in 2D array of int (0 = way, 1 = wall, 2 = solution)

        public Maze(int[,] maze)
        {
            this.maze = maze;
        }

        public void printMaze()
        {
            // print col index label
            string str1 = "   ";
            for (int i = 0; i < maze.GetLength(0); i++)
            {
                str1 += String.Format(" {0} ", i);
            }
            Console.WriteLine(str1);

            // print data
            for (int row = 0; row < maze.GetLength(0); row++)
            {
                // print row index label
                string str = String.Format(" {0} ", row);
                for (int col = 0; col < maze.GetLength(1); col++)
                {
                    switch (maze[row, col])
                    {
                        case 0:
                            str += "   "; // way
                            break;
                        case 1:
                            str += "###"; // wall
                            break;
                        case 2:
                            str += " o "; // solution
                            break;
                    }

                }
                Console.WriteLine(str);
            }
        }

        public string Run(int[] start, int[] end, bool anime)
        {
            // queue to store all possible path
            // the first path to reach end is the shortest
            Queue<string> q = new Queue<string>();

            // initialise 4 possible move (up, right, down, left)
            q.Enqueue("u");
            q.Enqueue("r");
            q.Enqueue("d");
            q.Enqueue("l");

            // loop through all possible move set to reach end
            while (true)
            {
                string move = q.Dequeue();
                int row = start[0];
                int col = start[1];
                for (int i = 0; i < move.Length; i++)
                {
                    // change the index according to the move set
                    switch (move[i])
                    {
                        case 'u':
                            row -= 1;
                            break;
                        case 'r':
                            col += 1;
                            break;
                        case 'd':
                            row += 1;
                            break;
                        case 'l':
                            col -= 1;
                            break;
                    }
                }

                // check if reach end
                if (row == end[0] && col == end[1]) return move;

                // check if the cell is valid
                // the next cell need to be inside the grid
                // the next cell cannot be a wall
                if (row >= 0 && row < maze.GetLength(0) && col >= 0 && col < maze.GetLength(1) && maze[row, col] != 1)
                {
                    // create more move
                    q.Enqueue(move + "u");
                    q.Enqueue(move + "r");
                    q.Enqueue(move + "d");
                    q.Enqueue(move + "l");
                }

                // the cell is not valid then just skip it
                // automatically discard invalid move set
                // continue to next move set


            }

        }

  

        public void drawSolution(int[] start,string solution)
        {
            // make a copy of the maze
            int[,] cpym = maze.Clone() as int[,];
            Maze cpy = new Maze(cpym);

            // put starting point
            cpy.setValue(start[0],start[1],2);

            int row = start[0];
            int col = start[1];

            // put solution path into the maze
            for (int i = 0; i < solution.Length; i++)
            {
                switch (solution[i])
                {
                    case 'u':
                        row -= 1;
                        break;
                    case 'r':
                        col += 1;
                        break;
                    case 'd':
                        row += 1;
                        break;
                    case 'l':
                        col -= 1;
                        break;
                }

                cpy.setValue(row, col, 2);

            }

            // call print function
            cpy.printMaze();
        }

        public void setValue(int row, int col, int value)
        {
            maze[row, col] = value;
        }
    }
}
