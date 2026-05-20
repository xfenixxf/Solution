using static OOP.Program;

namespace OOP
{
    internal class Program
    {   
        public class Product
        {   
            protected string product;
            protected string producer;
            protected double price;
            protected DateTime expiration_date;
            protected DateTime production_date;

            public Product (string product, string producer, double price, DateTime expiration_date, DateTime production_date)
            {
                if (string.IsNullOrWhiteSpace(product))
                    throw new ArgumentException("Наименование не может быть пустым");

                if (string.IsNullOrWhiteSpace(producer))
                    throw new ArgumentException("Производитель не может быть пустым");

                if (price < 0)
                    throw new ArgumentException("Цена не может быть отрицательной");

                if (production_date > DateTime.Now)
                    throw new ArgumentException("Дата производства не может быть в будущем");

                if (expiration_date <= production_date)
                    throw new ArgumentException("Срок годности должен быть позже даты производства");
                this.product = product;
                this.producer = producer;
                this.price = price;
                this.expiration_date = expiration_date;
                this.production_date = production_date;
            }

            public virtual string ToString()
            {
                return ($"Продукт: {product}, Производитель: {producer}, Цена: {price}, Срок годности: {expiration_date.ToShortDateString()}, Дата производства: {production_date.ToShortDateString()}");
            }

        }

        public class DiscountedProduct : Product
        {

            private int discount;
            private double discounted_price;

            public DiscountedProduct(string product, string producer, double price,DateTime expiration_date, DateTime production_date,int discount) :base(product, producer, price, expiration_date, production_date)
            {

                if (discount < 0 || discount > 100)
                    throw new ArgumentException("Скидка не может быть отрицательной или =>100%");
                this.discount = discount;
                this.discounted_price = price * (1 - discount / 100.0);
            }
            public override string ToString()
            {
                return ($"Продукт: {product}, Производитель: {producer}, Цена: {discounted_price} со скидкой {discount}%, Срок годности: {expiration_date.ToShortDateString()}, Дата производства: {production_date.ToShortDateString()}");
            }
            public int Discount
            {
                get { return discount; }
                set
                {
                    if (value < 0 || value > 100)
                        throw new ArgumentException("Скидка должна быть от 0 до 100%");
                    discount = value;
                    discounted_price = price * (1 - discount / 100.0);
                }
            }
            public double DiscountedPrice
            {
                get { return discounted_price; }
            }
        }
        static void Main(string[] args)
        {
            Console.Write("Наименование: ");
            string name = Console.ReadLine();
            Console.Write("Производитель: ");
            string producer = Console.ReadLine();
            Console.Write("Цена: ");
            double price = double.Parse(Console.ReadLine());
            Console.Write("Дата производства (гггг-мм-дд): ");
            DateTime production_date = DateTime.Parse(Console.ReadLine());
            Console.Write("Срок годности (гггг-мм-дд): ");
            DateTime expiration_date = DateTime.Parse(Console.ReadLine());
            Console.Write("Скидка (%): ");
            int discount = int.Parse(Console.ReadLine());
            Product product = new Product(name, producer, price, expiration_date, production_date);
            DiscountedProduct discounted = new DiscountedProduct(name, producer, price, expiration_date, production_date, discount);
            Console.WriteLine("Обычный товар");
            Console.WriteLine(product.ToString());
            Console.WriteLine("Товар со скидкой");
            Console.WriteLine(discounted.ToString());
        }
    }
}
