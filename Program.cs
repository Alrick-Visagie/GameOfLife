using System;
using System.ComponentModel;
using System.Numerics;
using System.Reflection;
using System.Threading;

namespace GameOfLife
{
    class Program
    {

        static int cols = 70;
        static int rows = 30;
        static int[,] cells = new int[cols, rows];
        static void Main(string[] args)
        {
            Setup();
        }


        static void Setup()
        {

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetWindowSize(cols, rows);
            Console.SetBufferSize(cols, rows);
            Console.CursorSize = cols;

            
            var random = new Random();

            //Adding values to array
            for (int i = 0; i < cols; i++)
                for (int j = 0; j < rows; j++)
                    cells[i, j] = random.Next(0, 2);

            while (true)
            {
                RenderOnScreen();
            }
        }

        static void RenderOnScreen()
        {
            int[,] cells2 = new int[cols, rows];
           
            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    if (cells[i, j] == 1)
                    {
                        Console.Write("r");
                    }
                    if (cells[i, j] == 0)
                    {
                        Console.Write("0");
                    }
                }
            }

            

            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    int state = cells[i, j];
                   
                        int neighbors = CountNeighbors(cells, i, j);

                        if (state == 0 && neighbors == 3)
                        {
                            cells2[i, j] = 1;
                        }
                        else if (state == 1 && (neighbors < 2 || neighbors > 3))
                        {
                            cells2[i, j] = 0;
                        }
                        else
                        {
                            cells2[i, j] = state;
                        }
                    
                }
            }
            cells = cells2;
            Thread.Sleep(150);
        }

        static int CountNeighbors(int[,] cells, int x, int y)
        {
            int sum = 0;
            for (int i = -1; i < 2; i++)
                for (int j = -1; j < 2; j++)
                {
                    int col = (x + i + cols) % cols;
                    int row = (y + j + rows) % rows;
                    sum += cells[col, row];
                }

            sum -= cells[x, y];

            return sum;
        }
    }
}
