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
        static string aliveCells = "";
        static string deadCells = "";
        static bool start = false;
        static int[,] current = new int[cols, rows];
        static int[,] comming = new int[cols, rows];
        static Random random = new Random();

        static void Main(string[] args)
        {
            Console.Write("Enter character for alive cells: ");
            aliveCells = Console.ReadLine();
            Console.Write("Enter character for dead cells: ");
            deadCells = Console.ReadLine();
            Console.Write("Press Enter To Start: ");
            start = Console.ReadKey().Key == ConsoleKey.Enter;

            Setup();
        }


        static void Setup()
        {
            Console.SetWindowSize(cols, rows);
            Console.SetBufferSize(cols * 2 , rows * 2);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.CursorVisible = false;

            //Adding values to array
            for (int i = 0; i < cols; i++)
                for (int j = 0; j < rows; j++)
                    current[i, j] = random.Next(0, 2);
           
            while (start)
            {
                RenderOnScreen();
            }
        }

        static void RenderOnScreen()
        {          
            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    if (current[i, j] == 1)
                    {
                        Console.SetCursorPosition(i, j);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(aliveCells);
                    }
                    if (current[i, j] == 0)
                    {
                        Console.SetCursorPosition(i, j);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(deadCells);
                    }
                }
            }


            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    int state = current[i, j];

                    int neighbors = CountNeighbors(current, i, j);

                    if (state == 0 && neighbors == 3)
                    {
                        comming[i, j] = 1;
                    }
                    else if (state == 1 && (neighbors < 2 || neighbors > 3))
                    {
                        comming[i, j] = 0;
                    }
                    else
                    {
                        comming[i, j] = state;
                    }

                }
            }

            Array.Copy(comming, current, cols * rows);
            Thread.Sleep(50);
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
