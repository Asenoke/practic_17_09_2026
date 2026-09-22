using task_5;

namespace task_5
{
    /// <summary>
    /// Интерфейс IMySQLDatabase
    /// </summary>
    public interface IMySQLDatabase
    {
        /// <summary>
        /// Метод сохранения заказа
        /// </summary>
        /// <param name="orderDetails">Детали заказа</param>
        public void SaveOrder(string orderDetails);
    }

    /// <summary>
    /// Интерфейс  ISmsService
    /// </summary>
    public interface ISmsService
    {
        /// <summary>
        /// Метод отправки смс
        /// </summary>
        /// <param name="phone">Номер телефона</param>
        /// <param name="message">Сообщение</param>
        public void SendSms(string phone, string message);
    }

    /// <summary>
    /// Класс MySQLDatabase (наследуется от IMySQLDatabase)
    /// </summary>
    public class MySQLDatabase : IMySQLDatabase
    {
        // Метод сохранения заказа
        public void SaveOrder(string orderDetails) => Console.WriteLine($"[MySQL] Заказ сохранен: {orderDetails}");
    }

    /// <summary>
    /// Класс SmsService (наследуется от ISmsService)
    /// </summary>
    public class SmsService : ISmsService
    {
        // Метод отправки смс
        public void SendSms(string phone, string message) => Console.WriteLine($"[SMS] Отправлено на {phone}: {message}");
    }
    
    public class OrderProcessor
    {
        public void ProcessOrder(string orderDetails, string clientPhone)
        {
            IMySQLDatabase db = new MySQLDatabase();
            db.SaveOrder(orderDetails);
            ISmsService sms = new SmsService();
            sms.SendSms(clientPhone, "Ваш заказ принят!");
        }
    }
    
    internal class Program
    {
        static void Main(string[] args)
        {
            OrderProcessor processor = new OrderProcessor();
            Console.Write("Введите ваш номер телефона: ");
            string phone = Console.ReadLine();

            processor.ProcessOrder("Книга по C#", phone);
        }
    }
}
