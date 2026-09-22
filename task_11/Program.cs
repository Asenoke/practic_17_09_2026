namespace task_11
{
    /// <summary>
    /// Класс Address
    /// </summary>
    public class Address
    {
        // Поля
        private string city;
        private string street;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="city">Город</param>
        /// <param name="street">Адрес</param>
        public Address(string city, string street)
        {
            this.city = city;
            this.street = street;
        }

        // Метод получения города
        public string GetCity() => city;

        // Метод получения улицы
        public string GetStreet() => street;
    }

    /// <summary>
    /// Класс Department
    /// </summary>
    public class Department
    {
        // Поля
        private string name;
        private Address location;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="name">Название</param>
        /// <param name="location">Локация (Адресс)</param>
        public Department(string name, Address location)
        {
            this.name = name;
            this.location = location;
        }

        // Метод получения имени
        public string GetName() => name;

        // Метод получения города из локации
        public string GetCity() => location.GetCity();

        // Метод получения улицы из локации
        public string GetStreet() => location.GetStreet();

    }

    /// <summary>
    /// Класс Employee
    /// </summary>
    public class Employee
    {
        // Поля
        private string fullName;
        private Department department;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="fullName">Полное имя</param>
        /// <param name="department">Отдел</param>
        public Employee(string fullName, Department department)
        {
            this.fullName = fullName;
            this.department = department;
        }

        // Метод получения полного имени
        public string GetFullName() => fullName;

        // Метод получения города
        public string GetCity() => department.GetCity();

        // Метод получения улицы
        public string GetStreet() => department.GetStreet();
    }

    /// <summary>
    /// Класс ReportGenerator
    /// </summary>
    public class ReportGenerator
    {
        // Метод получения отчёта о городе
        public void PrintEmployeeCity(Employee emp)
        {
            Console.WriteLine($"Сотрудник {emp.GetFullName()} работает в городе {emp.GetCity()}");
        }

        // Метод получения отчёта об улице
        public void PrintEmployeeStreet(Employee emp)
        {
            Console.WriteLine($"Сотрудник {emp.GetFullName()} работает на улице {emp.GetStreet()}");
        }
    }

    internal class Program
    {
        static void Main()
        {
            Address address = new Address("Москва", "Тверская");
            Department department = new Department("IT", address);
            Employee employee = new Employee("Иван Иванов", department);

            ReportGenerator report = new ReportGenerator();
            report.PrintEmployeeCity(employee);
            report.PrintEmployeeStreet(employee);
        }
    }
}