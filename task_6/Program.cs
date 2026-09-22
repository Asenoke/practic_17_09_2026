namespace task_6
{
    public interface IBankAccount
    {
        /// <summary>
        /// Метод пополения баланса
        /// </summary>
        /// <param name="amount">Сумма пополнения</param>
        public void Deposit(decimal amount);
        
        /// <summary>
        /// Метод списание денежных средств
        /// </summary>
        /// <param name="amount">Сумма списания</param>
        public void Withdraw(decimal amount);

    }

    /// <summary>
    /// Класс BankAccount (наследуется от IBankAccount)
    /// </summary>
    public class BankAccount : IBankAccount
    {
        // Поля (Автосвойства)
        public string OwnerName { get; private set; }
        public decimal Balance { get; private set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="ownerName">Имя держателя карты</param>
        /// <param name="balance">Баланс</param>
        public BankAccount(string ownerName, decimal balance = 0)
        {
            OwnerName = ownerName;
            Balance = balance;
        }
       
        public void Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Ошибка пополения. Сумма пополения должна быть положительной");
            }
            {
                Balance += amount;
                Console.WriteLine("Пополнение успешно");
            }
        }

        public void Withdraw(decimal amount)
        {
            if (Balance < amount)
            {
                throw new Exception("Ошибка снятия. На балансе не хватает денежных средств");
            }
            {
                Balance -= amount;
                Console.WriteLine("Выдача наличных успешна");
            }
            
        }

    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите ваше ФИО: ");
            string ownerName = Console.ReadLine();

            Console.Write("Введите сумму для пополения баланса: ");
            decimal amount = Decimal.Parse(Console.ReadLine());

            IBankAccount bankAccount = new BankAccount(ownerName);

            bankAccount.Deposit(amount);

            Console.Write("Введите сумму для снятия: ");
            decimal amount1 = Decimal.Parse(Console.ReadLine());
            
            bankAccount.Withdraw(amount1);

        }
    }
}
