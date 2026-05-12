using System.ComponentModel.DataAnnotations;

namespace DateType1
{
    internal class Program1
    {
        /// <summary>
        /// функция для расчёта сложных процентов по годам
        /// </summary>
        /// <param name="initial_deposit"> Начальный депозит</param>
        /// <param name="years">года</param>
        /// <param name="interest_rate">процент ставки</param>
        /// <returns></returns>
        public static string CalculatePercent(double initial_deposit, int years, int interest_rate)
        {
            string resultString = "";
            for (int i = 0; i < years; i++)
            {
                initial_deposit = initial_deposit + ((initial_deposit * interest_rate) / 100);
                resultString = resultString + $"Год{i + 1}: {initial_deposit:F2} руб.\n";

            }
            return resultString;
        }

        static void Main(string[] args)
        {
            double initial_deposit = 0;
            int years = 0;
            int interest_rate = 0;
            Console.WriteLine($"начальный вклад");
            initial_deposit = Convert.ToDouble(Console.ReadLine());
            if (initial_deposit < 0) { Console.WriteLine($"неверное число"); return; }
            Console.WriteLine($"количество лет");
            years = Convert.ToInt32(Console.ReadLine());
            if (years < 0) { Console.WriteLine($"неверное число"); return; }
            Console.WriteLine($"годовая процентная ставка");
            interest_rate = Convert.ToInt32(Console.ReadLine());
            if (interest_rate < 0) { Console.WriteLine($"неверное число"); return; }
            Console.WriteLine(CalculatePercent(initial_deposit, years, interest_rate));

        }
    }
}