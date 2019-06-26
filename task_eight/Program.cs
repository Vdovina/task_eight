using System;
using System.Collections.Generic;
using System.Linq;

namespace task_eight
{
    class Program
    {
        const int opt = 7;
        static Random rand = new Random();
        public static HashSet<string> Bridges = new HashSet<string>();

        static void Main(string[] args)
        {
            Console.WriteLine("Программа проверяет, является ли граф деревом.\n");
            int[,] matrix = new int[1, 1];

            Console.WriteLine(@"Выберите номер матрицы от 1 до " + opt);
            int option = MenuOption();
            switch (option)
            {
                case 1: matrix = matrix1; break;
                case 2: matrix = matrix2; break;
                case 3: matrix = matrix3; break;
                case 4: matrix = matrix4; break;
                case 5: matrix = matrix5; break;
                case 6: matrix = matrix6; break;
                case 7: matrix = matrix7; break;
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Ваша матрица смежности:");
            Console.ResetColor();
            ShowMatrix(matrix);
            
            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 0; j < matrix.GetLength(1); j++)
                     if (i < j && matrix[i, j] == 1) Bridges.Add((i + 1).ToString() + (j + 1).ToString());

            Graph oe = new Graph(matrix.GetLength(0), matrix);
            
            Console.WriteLine("\n" + oe.ToString());
            Console.Write("\nРёбра:   ");
            foreach (string s in Bridges) Console.Write(s + "    ");
            Console.WriteLine();

            int k;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(!oe.DeapthSearch(0, 0, new HashSet<string>(), Bridges, false, out k) ? "\nГраф является деревом" : "\nГраф не является деревом");
            Console.ResetColor();
            //Bridges = oe.RemoveCycleRibs(k, k, Bridges, out k);

            //Console.WriteLine("\nМосты:   ");
            //foreach (string s in Bridges) Console.Write(s + "    ");
            Console.ReadKey();

        }

        static int[,] MakeMatrix(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (i == j) matrix[i, j] = 0;
                    else if (j > i)
                    {
                        int k = rand.Next(1, 10) % 2;
                        matrix[i, j] = k;
                        matrix[j, i] = k;
                        if (k == 1) Bridges.Add((i + 1).ToString() + (j + 1).ToString());
                    }
                }
            return matrix;
        }

        static void ShowMatrix(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                    Console.Write(matrix[i, j] + "  ");
                Console.WriteLine();
            }
        }

        static int MenuOption()
        {
            int option = 0;
            bool alright = false;
            do
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("Пункт:    ");
                Console.ResetColor();
                try
                {
                    option = Convert.ToInt32(Console.ReadLine());
                    if (option < 1 || option > opt) ErrorInMenu();
                    else alright = true;
                }
                catch (FormatException)
                {
                    ErrorInMenu();
                    alright = false;
                }
            } while (!alright);

            return option;
        }

        static void ErrorInMenu()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Выберите пункт меню!");
            Console.ResetColor();
        }

        static int[,] matrix1 = { { 0, 1, 0, 0 }, { 1, 0, 0, 1 }, { 0, 1, 0, 1 }, { 0, 0, 1, 0 } };
        static int[,] matrix2 = { { 0, 1, 0, 0 }, { 1, 0, 1, 1 }, { 0, 1, 0, 1 }, { 0, 1, 1, 0 } };
        static int[,] matrix3 = { { 0, 0, 0, 1 }, { 0, 0, 0, 1 }, { 0, 1, 0, 1 }, { 1, 0, 1, 0 } };
        static int[,] matrix4 = { { 0, 1, 0, 0, 1, 0}, { 1, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 1 }, { 0, 0, 0, 0, 0, 1}, { 1, 0, 0, 0, 0, 1}, { 0, 0, 1, 1, 1, 0} };
        static int[,] matrix5 = { { 0, 1, 0, 0, 0, 1 }, { 1, 0, 0, 1, 1, 0 }, { 0, 0, 0, 0, 1, 0 }, { 0, 1, 0, 0, 0, 0 }, { 1, 1, 1, 0, 0, 1 }, { 0, 0, 0, 0, 1, 0 } };
        static int[,] matrix6 = { { 0, 0, 0, 0, 0, 1 }, { 0, 0, 0, 1, 1, 0 }, { 0, 0, 0, 0, 1, 0 }, { 0, 1, 0, 0, 0, 0 }, { 1, 1, 1, 0, 0, 1 }, { 0, 0, 0, 0, 1, 0 } };
        static int[,] matrix7 = { { 0, 0, 1, 1, 1 }, { 0, 0, 1, 0, 0 }, { 1, 1, 0, 0, 0 }, { 1, 0, 0, 0, 1 }, { 1, 0, 0, 1, 0 } };

    }
}