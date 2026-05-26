using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Remoting.Messaging;

namespace WorkWithBD
{
    internal class Program
    {
        static string connStr = "data source=DESKTOP-IA3GG02\\MSSQLSERVER1;initial catalog=bookshop;integrated security=True;trustservercertificate=True";
        /// <summary>
        /// Добавление книги
        /// </summary>
        static void CreateBook()
        {
            string title = OutText("Название: ");
            string author = OutText("Автор: ");
            double price = double.Parse(OutText("Цена: "));
            string publishedDateStr = OutText("Дата публикации (гггг-мм-дд, оставьте пустым если не указана): ");

            DateTime? publishedDate = null;
            if (!string.IsNullOrWhiteSpace(publishedDateStr))
            {
                publishedDate = DateTime.Parse(publishedDateStr);
            }

            const string sql = "INSERT INTO Books (ID_books, Title, Author, Price, Published_date) VALUES (NEWID(), @t, @a, @p, @pd)";

            using (var connection = new SqlConnection(connStr))
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add("@t", SqlDbType.NVarChar, 50).Value = title;
                command.Parameters.Add("@a", SqlDbType.NVarChar, 50).Value = author;
                command.Parameters.Add("@p", SqlDbType.Decimal).Value = price;
                command.Parameters.Add("@pd", SqlDbType.DateTime).Value = (object)publishedDate ?? DBNull.Value;
                connection.Open();
                command.ExecuteNonQuery();
            }
            Console.WriteLine("Книга добавлена");
        }
        /// <summary>
        /// Считывание и вывод всех книг
        /// </summary>
        static void ReadBooks()
        {
            const string sql = "SELECT ID_books, Title, Author, Price, Published_date FROM Books";

            using (var connection = new SqlConnection(connStr))
            using (var command = new SqlCommand(sql, connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    Console.WriteLine("Книги:");
                    while (reader.Read())
                    {
                        Console.WriteLine($"ID: {reader["ID_books"]}");
                        Console.WriteLine($"Название: {reader["Title"]}");
                        Console.WriteLine($"Автор: {reader["Author"]}");
                        Console.WriteLine($"Цена: {reader["Price"]}");
                        if (reader["Published_date"] != DBNull.Value)
                            Console.WriteLine($"Дата публикации: {Convert.ToDateTime(reader["Published_date"]).ToShortDateString()}");
                        Console.WriteLine("-------------------");
                    }
                }
            }
        }
        /// <summary>
        /// Изменение записи о книге по ID
        /// </summary>
        static void UpdateBook()
        {
            ReadBooks();
            Guid id = Guid.Parse(OutText("Введите ID книги для обновления: "));
            string title = OutText("Новое название: ");
            string author = OutText("Новый автор: ");
            double price = double.Parse(OutText("Новая цена: "));
            string publishedDateStr = OutText("Новая дата публикации (гггг-мм-дд, оставьте пустым если не указана): ");

            DateTime? publishedDate = null;
            if (!string.IsNullOrWhiteSpace(publishedDateStr))
            {
                publishedDate = DateTime.Parse(publishedDateStr);
            }

            const string sql = "UPDATE Books SET Title = @t, Author = @a, Price = @p, Published_date = @pd WHERE ID_books = @id";

            using (var connection = new SqlConnection(connStr))
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add("@t", SqlDbType.NVarChar, 50).Value = title;
                command.Parameters.Add("@a", SqlDbType.NVarChar, 50).Value = author;
                command.Parameters.Add("@p", SqlDbType.Decimal).Value = price;
                command.Parameters.Add("@pd", SqlDbType.DateTime).Value = (object)publishedDate ?? DBNull.Value;
                command.Parameters.Add("@id", SqlDbType.UniqueIdentifier).Value = id;

                connection.Open();
                int rows = command.ExecuteNonQuery();

                if (rows > 0) Console.WriteLine("Книга обновлена");
                else Console.WriteLine("Книга не найдена");
            }
        }
        /// <summary>
        /// Удаление записи о книге по ID
        /// </summary>
        static void DeleteBook()
        {
            ReadBooks();
            Guid id = Guid.Parse(OutText("Enter Book ID to delete: "));

            const string sql = "DELETE FROM Books WHERE ID_books = @id";

            using (var connection = new SqlConnection(connStr))
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add("@id", SqlDbType.UniqueIdentifier).Value = id;

                connection.Open();
                int rows = command.ExecuteNonQuery();

                if (rows > 0) Console.WriteLine("Book deleted successfully!");
                else Console.WriteLine("Book not found!");
            }
        }

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("1. Добавить книгу");
                Console.WriteLine("2. Показать все книги");
                Console.WriteLine("3. Изменить запись о книге");
                Console.WriteLine("4. Удалить запись о книге");
                Console.WriteLine("5. Выход");
                var choice = OutText("Выберите опцию: ");

                switch (choice)
                {
                    case "1":
                        CreateBook();
                        break;
                    case "2":
                        ReadBooks();
                        break;
                    case "3":
                        UpdateBook();
                        break;
                    case "4":
                        DeleteBook();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Неверная опция");
                        break;
                }
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