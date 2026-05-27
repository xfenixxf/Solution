using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace DummyApiExample
{
    public class Employee
    {
        public int id { get; set; }
        public string employee_name { get; set; }
        public string employee_salary { get; set; }  // Изменено с int на string
        public string employee_age { get; set; }      // Изменено с int на string
        public string profile_image { get; set; }
    }

    public class CreateEmployeeRequest
    {
        public string name { get; set; }
        public string salary { get; set; }  // Изменено с int на string
        public string age { get; set; }      // Изменено с int на string
    }

    public class ApiResponse<T>
    {
        public string status { get; set; }
        public T data { get; set; }
        public string message { get; set; }
    }

    class Program
    {
        private static readonly HttpClient client = new HttpClient();
        private const string BASE_URL = "https://dummy.restapiexample.com/api/v1";

        static async Task Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            await GetEmployees();

            await CreateEmployee("Иван Петров", "85000", "30");
            await CreateEmployee("Елена Смирнова", "92000", "28");
            await CreateEmployee("Алексей Иванов", "110000", "35");
;
            await GetEmployees();

            Console.WriteLine("Нажмите любую клавишу для выхода...");
            Console.ReadKey();
        }

        static async Task GetEmployees()
        {
            try
            {
                string url = $"{BASE_URL}/employees";
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string jsonResponse = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var apiResult = JsonSerializer.Deserialize<ApiResponse<List<Employee>>>(jsonResponse, options);

                if (apiResult?.status == "success" && apiResult.data != null)
                {
                    Console.WriteLine($"Получено {apiResult.data.Count} сотрудников:\n");
                    Console.WriteLine($"{"ID",-5} {"Имя",-25} {"Зарплата",-15} {"Возраст",-8}");
                    Console.WriteLine(new string('-', 60));

                    foreach (var emp in apiResult.data)
                    {
                        string formattedSalary = FormatSalary(emp.employee_salary);
                        Console.WriteLine($"{emp.id,-5} {emp.employee_name,-25} {formattedSalary,-15} {emp.employee_age,-8}");
                    }
                }
                else
                {
                    Console.WriteLine("❌ Не удалось получить список сотрудников");
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"❌ Ошибка HTTP: {ex.Message}");
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"❌ Ошибка парсинга JSON: {ex.Message}");
            }
        }

        static async Task CreateEmployee(string name, string salary, string age)
        {
            try
            {
                string url = $"{BASE_URL}/create";

                var newEmployee = new CreateEmployeeRequest
                {
                    name = name,
                    salary = salary,
                    age = age
                };

                string jsonBody = JsonSerializer.Serialize(newEmployee);
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(url, content);
                string jsonResponse = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var apiResult = JsonSerializer.Deserialize<ApiResponse<Dictionary<string, object>>>(jsonResponse, options);

                if (apiResult?.status == "success")
                {
                    Console.WriteLine($"Создан: {name} (Зарплата: {FormatSalary(salary)}, Возраст: {age})");
                    if (apiResult.data != null && apiResult.data.ContainsKey("id"))
                    {
                        Console.WriteLine($"Сгенерированный ID: {apiResult.data["id"]}");
                    }
                    Console.WriteLine("Внимание: API не сохраняет данные реально!");
                }
                else
                {
                    Console.WriteLine($"Ошибка при создании {name}: {apiResult?.message ?? "Неизвестная ошибка"}");
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP ошибка при создании {name}: {ex.Message}");
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Ошибка JSON при создании {name}: {ex.Message}");
            }
        }

        static string FormatSalary(string salary)
        {
            if (long.TryParse(salary, out long numericSalary))
            {
                return $"{numericSalary:N0}";  
            }
            return salary;
        }
    }
}