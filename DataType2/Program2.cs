namespace DataType2
{
    internal class Program2
    {
        /// <summary>
        /// Функция для вывода Diamond
        /// </summary>
        /// <param name="x">размер Diamond</param>
        public static void PrintDiamond(int x)
        {
            if (x % 2 == 0)
            {
                Console.WriteLine("нечётное число");
                return;
            }

            int center = x / 2;

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    if (Math.Abs(i - center) + Math.Abs(j - center) == center)
                    {
                        Console.Write("X");
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
        }
        static void Main(string[] args)
        {
            int x = 0;
            Console.WriteLine($"X =");
            x = Convert.ToInt32(Console.ReadLine());
            if (x < 0) { Console.WriteLine($"неверное число"); return; }
            PrintDiamond(x);
        }
    }
}


