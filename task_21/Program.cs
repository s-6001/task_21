using System;
using System.Threading;

namespace task_21
{
    class Program
    {
        static int x;   //длина сада
        static int y;   //ширина сада
        static byte[,] field;
        static object locker = new object();

        static void Main()
        {
            Console.Write("Введите длину прямоугольного поля (целое число): ");
            try
            {
                x = Convert.ToInt32(Console.ReadLine());    //считываем длину сада
            }
            catch
            {
                Console.WriteLine("Не удалось считать длину.");
            }
            Console.Write("Введите ширину прямоугольного поля (целое число): ");
            try
            {
                y = Convert.ToInt32(Console.ReadLine());    //считываем ширину сада
            }
            catch
            {
                Console.WriteLine("Не удалось считать ширину.");
            }
            field = new byte[x, y]; //массив заполнен 0
            Thread gardner1 = new Thread(Gardner1); //создаем поток 1
            Thread gardner2 = new Thread(Gardner2); //создаем поток 2
            gardner1.Start();   //запускаем поток 1
            gardner2.Start();   //запускаем поток 2
            gardner1.Join();    //блокируем потоки
            gardner2.Join();
            for (int i = 0; i < x; i++) //выводим массив на консоль
            {
                for (int j = 0; j < y; j++)
                {
                    Console.Write(field[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.ReadLine();
        }
        private static void Gardner1()  //метод садовника 1
        {
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    if (field[i, j] == 0)
                        field[i, j] = 1;
                    Thread.Sleep(1);
                }
            }
        }
        private static void Gardner2()  //метод садовника 2
        {
            for (int j = y - 1; j > 0; j--)
            {
                for (int i = x - 1; i > 0; i--)
                {
                    if (field[i, j] == 0)
                        field[i, j] = 2;
                    Thread.Sleep(1);
                }
            }
        }
    }
}