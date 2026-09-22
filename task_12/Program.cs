namespace task_12
{
    /// <summary>
    /// Статичный класс PayrollConstants
    /// </summary>
    public static class PayrollConstants
    {
        public const int StandardHours = 160;
        public const decimal OvertimeMultiplier = 1.5m;
        public const decimal ManagerMultiplier = 1.2m;
        public const decimal InternMultiplier = 0.5m;
    }

    /// <summary>
    /// Интерфейс ITaxCalculator
    /// </summary>
    public interface ITaxCalculator
    {
        /// <summary>
        /// Метод расчёта зарплаты
        /// </summary>
        /// <param name="gross">Сумма</param>
        /// <returns></returns>
        decimal CalculateTax(decimal gross);
    }

    /// <summary>
    /// Класс RussiaTaxCalculator (наследуется от ITaxCalculator)
    /// </summary>
    public class RussiaTaxCalculator : ITaxCalculator
    {
        // Поля
        private const decimal Rate = 0.13m;

        public decimal CalculateTax(decimal gross) => gross * Rate;
    }


    /// <summary>
    /// Класс UsaTaxCalculator (наследуется от ITaxCalculator)
    /// </summary>
    public class UsaTaxCalculator : ITaxCalculator
    {
        // Поля
        private const decimal Rate = 0.20m;

        public decimal CalculateTax(decimal gross) => gross * Rate;
    }

    /// <summary>
    /// Класс NoTaxCalculator (наследуется от ITaxCalculator)
    /// </summary>
    public class NoTaxCalculator : ITaxCalculator
    {
        public decimal CalculateTax(decimal gross) => 0m;
    }

    /// <summary>
    /// Абстрактный класс Employee
    /// </summary>
    public abstract class Employee
    {
        // Автосвойства
        public string Name { get; private set; }
        public decimal BaseSalary { get; private set; }


        /// <summary>
        /// Контсруктор
        /// </summary>
        /// <param name="name">Имя</param>
        /// <param name="baseSalary">Базовая зарплата</param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public Employee(string name, decimal baseSalary)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Имя не может быть пустым.", nameof(name));
            }

            if (baseSalary < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(baseSalary), "Оклад не может быть отрицательным.");
            }


            Name = name;
            BaseSalary = baseSalary;
        }

        /// <summary>
        /// Абстрактный метод расчёта
        /// </summary>
        /// <param name="hoursWorked">Рабочие часы</param>
        /// <returns></returns>
        public abstract decimal CalculateGross(int hoursWorked);
    }


    /// <summary>
    /// Класс Developer (наследуется от Employee)
    /// </summary>
    public class Developer : Employee
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="name">Имя</param>
        /// <param name="baseSalary">Базовая зарплата</param>
        public Developer(string name, decimal baseSalary) : base(name, baseSalary) { }


        public override decimal CalculateGross(int hoursWorked)
        {
            if (hoursWorked < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(hoursWorked));
            }

            decimal result = BaseSalary * hoursWorked;

            if (hoursWorked > PayrollConstants.StandardHours)
            {
                int overtimeHours = hoursWorked - PayrollConstants.StandardHours;
                result += overtimeHours * BaseSalary * PayrollConstants.OvertimeMultiplier;
            }

            return result;
        }
    }


    /// <summary>
    /// Класс Manager (наследуется от Employee)
    /// </summary>
    public class Manager : Employee
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="name">Имя</param>
        /// <param name="baseSalary">Базовая зарплатаы</param>
        public Manager(string name, decimal baseSalary) : base(name, baseSalary) { }

        public override decimal CalculateGross(int hoursWorked)
        {
            if (hoursWorked < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(hoursWorked));
            }

            return BaseSalary * hoursWorked * PayrollConstants.ManagerMultiplier;
        }
    }

    /// <summary>
    /// Класс Intern (наследуется от Employee)
    /// </summary>
    public class Intern : Employee
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="name">Имя</param>
        /// <param name="baseSalary">Базовая зарплатаы</param>
        public Intern(string name, decimal baseSalary) : base(name, baseSalary) { }

        public override decimal CalculateGross(int hoursWorked)
        {
            if (hoursWorked < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(hoursWorked));
            }

            return BaseSalary * hoursWorked * PayrollConstants.InternMultiplier;
        }
    }

    /// <summary>
    /// Класс Payroll
    /// </summary>
    public class Payroll
    {
        // Поля
        private readonly ITaxCalculator taxCalculator;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="taxCalculator">Тип расчёта</param>
        /// <exception cref="ArgumentNullException"></exception>
        public Payroll(ITaxCalculator taxCalculator)
        {
            this.taxCalculator = taxCalculator;
        }

        /// <summary>
        /// Метод расчёта
        /// </summary>
        /// <param name="employee">Сотрудник</param>
        /// <param name="hoursWorked">Рабочие часы</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public decimal Calculate(Employee employee, int hoursWorked)
        {
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }

            decimal gross = employee.CalculateGross(hoursWorked);
            decimal tax = taxCalculator.CalculateTax(gross);
            decimal net = gross - tax;

            return net < 0 ? 0 : net;
        }
    }

    internal class Program
    {
        static void Main()
        {
            Payroll payroll = new Payroll(new RussiaTaxCalculator());

            Employee dev = new Developer("Иван", 1000m);
            Employee manager = new Manager("Пётр", 1000m);
            Employee intern = new Intern("Олег", 1000m);

            Console.WriteLine($"Разработчик (170 ч): {payroll.Calculate(dev, 170)}");
            Console.WriteLine($"Менеджер (160 ч):    {payroll.Calculate(manager, 160)}");
            Console.WriteLine($"Стажёр (160 ч):      {payroll.Calculate(intern, 160)}");

            Payroll usaPayroll = new Payroll(new UsaTaxCalculator());
            Console.WriteLine($"Разработчик (США):   {usaPayroll.Calculate(dev, 170)}");
        }
    }
}