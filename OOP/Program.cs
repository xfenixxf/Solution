namespace OOP
{
    internal class Program
    {
        public class Product
        {
            protected string _product;
            protected string _producer;
            protected double _price;
            protected DateTime _expirationDate;
            protected DateTime _productionDate;

            public Product(string product, string producer, double price, DateTime expirationDate, DateTime productionDate)
            {
                if (string.IsNullOrWhiteSpace(product))
                    throw new ArgumentException("Наименование не может быть пустым");

                if (string.IsNullOrWhiteSpace(producer))
                    throw new ArgumentException("Производитель не может быть пустым");

                if (price < 0)
                    throw new ArgumentException("Цена не может быть отрицательной");

                if (productionDate > DateTime.Now)
                    throw new ArgumentException("Дата производства не может быть в будущем");

                if (expirationDate <= productionDate)
                    throw new ArgumentException("Срок годности должен быть позже даты производства");

                _product = product;
                _producer = producer;
                _price = price;
                _expirationDate = expirationDate;
                _productionDate = productionDate;
            }

            public virtual string ToString()
            {
                return $"Продукт: {_product}, Производитель: {_producer}, Цена: {_price}, Срок годности: {_expirationDate.ToShortDateString()}, Дата производства: {_productionDate.ToShortDateString()}";
            }
        }

        public class DiscountedProduct : Product
        {
            private int _discount;
            private double _discountedPrice;

            public DiscountedProduct(string product, string producer, double price, DateTime expirationDate, DateTime productionDate, int discount)
                : base(product, producer, price, expirationDate, productionDate)
            {
                if (discount < 0 || discount > 100)
                    throw new ArgumentException("Скидка должна быть от 0 до 100%");

                _discount = discount;
                _discountedPrice = price * (1 - discount / 100.0);
            }

            public override string ToString()
            {
                return $"Продукт: {_product}, Производитель: {_producer}, Цена: {_discountedPrice:F2} со скидкой {_discount}%, Срок годности: {_expirationDate.ToShortDateString()}, Дата производства: {_productionDate.ToShortDateString()}";
            }

            public int Discount
            {
                get { return _discount; }
                set
                {
                    if (value < 0 || value > 100)
                        throw new ArgumentException("Скидка должна быть от 0 до 100%");
                    _discount = value;
                    _discountedPrice = _price * (1 - _discount / 100.0);
                }
            }

            public double DiscountedPrice
            {
                get { return _discountedPrice; }
            }
        }

        static void Main(string[] args)
        {

            string name = OutText("Наименование: ");
            string producer = OutText("Производитель: ");
            double price = double.Parse(OutText("Цена: "));
            DateTime productionDate = DateTime.Parse(OutText("Дата производства (гггг-мм-дд): "));
            DateTime expirationDate = DateTime.Parse(OutText("Срок годности (гггг-мм-дд): "));
            int discount = int.Parse(OutText("Скидка (%): "));

            Product product = new Product(name, producer, price, expirationDate, productionDate);
            DiscountedProduct discounted = new DiscountedProduct(name, producer, price, expirationDate, productionDate, discount);

            Console.WriteLine("Обычный товар");
            Console.WriteLine(product.ToString());
            Console.WriteLine("Товар со скидкой");
            Console.WriteLine(discounted.ToString());

        }
        /// <summary>
        /// Функция для вывода сообщения и считывания строки ввода
        /// </summary>
        /// <param name="Message">сообщение которое выводится</param>
        /// <returns></returns>
        public static string OutText(string message)
        {
            Console.Write(message);
            return Console.ReadLine();
        }
    }
}