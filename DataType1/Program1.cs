using System;
using System.Collections.Generic;

namespace DateType1
{
    internal class Program1
    {
        /// <summary>
        /// Функция для расчёта сложных процентов по годам
        /// </summary>
        /// <param name="InitialDeposit">Начальный депозит</param>
        /// <param name="Years">Количество лет</param>
        /// <param name="InterestRate">Процентная ставка</param>
        /// <returns>Список значений по годам</returns>
        public static List<double> CalculatePercent(double InitialDeposit, int Years, int InterestRate)
        {
            List<double> Results = new List<double>();
            double CurrentDeposit = InitialDeposit;

            for (int i = 0; i < Years; i++)
            {
                CurrentDeposit = CurrentDeposit + ((CurrentDeposit * InterestRate) / 100);
                Results.Add(CurrentDeposit);
            }

            return Results;
        }

        static void Main(string[] args)
        {
            double InitialDeposit = 0;
            int Years = 0;
            int InterestRate = 0;

            InitialDeposit = Convert.ToDouble(OutText("Введите начальный вклад"));
            if (InitialDeposit < 0)
            {
                Console.WriteLine("Неверное число");
                return;
            }

            Years = Convert.ToInt32(OutText("Введите количество лет"));
            if (Years < 0)
            {
                Console.WriteLine("Неверное число");
                return;
            }

            InterestRate = Convert.ToInt32(OutText("Введите годовую процентную ставку"));
            if (InterestRate < 0)
            {
                Console.WriteLine("Неверное число");
                return;
            }

            List<double> Results = CalculatePercent(InitialDeposit, Years, InterestRate);

            for (int i = 0; i < Results.Count; i++)
            {
                Console.WriteLine($"Год{i + 1}: {Results[i]:F2} руб.");
            }
        }
        /// <summary>
        /// Функция для вывода сообщения и считывания строки ввода
        /// </summary>
        /// <param name="Message">сообщение которое выводится</param>
        /// <returns></returns>
        public static string OutText(string Message)
        {
            Console.WriteLine(Message);
            return Console.ReadLine();
        }
    }
}