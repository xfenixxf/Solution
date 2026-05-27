using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Console_API_Task
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string date = "14/01/2025";

            Console.Write($"Введите дату в формате ДД/ММ/ГГГГ (Enter для {date}): ");
            string userInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(userInput))
            {
                date = userInput;
            }

            Console.WriteLine($"Загрузка курсов валют на {date}...\n");

            await GetCurrencyRates(date);
        }

        static async Task GetCurrencyRates(string date)
        {
            string url = $"https://www.cbr.ru/scripts/XML_daily.asp?date_req={date}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    string xmlContent = await response.Content.ReadAsStringAsync();

                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(xmlContent);

                    XmlNodeList valuteNodes = xmlDoc.SelectNodes("//Valute");

                    if (valuteNodes == null || valuteNodes.Count == 0)
                    {
                        Console.WriteLine("Курсы валют не найдены для указанной даты.");
                        return;
                    }

                    Console.WriteLine($"Найдено {valuteNodes.Count} валют:\n");
                    Console.WriteLine($"{"Код",-6} {"Номинал",-8} {"Валюта",-40} {"Курс",-15}");
                    Console.WriteLine(new string('-', 75));

                    foreach (XmlNode valute in valuteNodes)
                    {
                        string charCode = valute.SelectSingleNode("CharCode")?.InnerText ?? "N/A";
                        string name = valute.SelectSingleNode("Name")?.InnerText ?? "N/A";
                        string nominal = valute.SelectSingleNode("Nominal")?.InnerText ?? "N/A";
                        string value = valute.SelectSingleNode("Value")?.InnerText ?? "N/A";

                        Console.WriteLine($"{charCode,-6} {nominal,-8} {name,-40} {value,-15}");
                    }

                    Console.WriteLine($"{new string('-', 75)}");
                    Console.WriteLine("Курсы основных валют:");

                    XmlNode usdNode = xmlDoc.SelectSingleNode("//Valute[CharCode='USD']");
                    if (usdNode != null)
                    {
                        string usdValue = usdNode.SelectSingleNode("Value")?.InnerText;
                        string usdNominal = usdNode.SelectSingleNode("Nominal")?.InnerText;
                        Console.WriteLine($"USD: {usdNominal} доллар = {usdValue} руб.");
                    }

                    XmlNode eurNode = xmlDoc.SelectSingleNode("//Valute[CharCode='EUR']");
                    if (eurNode != null)
                    {
                        string eurValue = eurNode.SelectSingleNode("Value")?.InnerText;
                        string eurNominal = eurNode.SelectSingleNode("Nominal")?.InnerText;
                        Console.WriteLine($"EUR: {eurNominal} евро = {eurValue} руб.");
                    }

                }
                catch (HttpRequestException httpEx)
                {
                    Console.WriteLine($"Ошибка HTTP-запроса: {httpEx.Message}");
                    Console.WriteLine("Проверьте подключение к интернету и правильность даты.");
                }
                catch (XmlException xmlEx)
                {
                    Console.WriteLine($"Ошибка при парсинге XML: {xmlEx.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Неожиданная ошибка: {ex.Message}");
                }
            }
        }
    }
}
