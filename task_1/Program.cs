namespace task_1
{
    /// <summary>
    /// Класс User
    /// </summary>
    public class User
    {
        // Автосвойства
        public string UserName { get; private set; }
        public string Password { get; private set; }
        public string Email {  get; private set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="userName">Имя</param>
        /// <param name="password">Пароль</param>
        /// <param name="email">Почта</param>
        public User(string userName, string password, string email)
        {
            UserName = userName;
            Password = password;
            Email = email;
        }


        // Метод изменения имени
        public void SetUserName(string value) {
            if (string.IsNullOrWhiteSpace(value)) {
                throw new ArgumentNullException("Ошибка ввода имени");
            } else
            {
                UserName = value;
                Console.WriteLine("Имя изменено.");
            }
        }

        // Метод изменения почты
        public void SetEmail(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException("Ошибка ввода почты");
            }
            else
            {
                Email = value;
                Console.WriteLine("Почта изменена.");
            }
        }

        // Метод изменения пароля
        public void SetPassword(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException("Ошибка ввода пароля");
            }
            else
            {
                Password = value;
                Console.WriteLine("Пароль изменён.");
            }
        }

    }

    /// <summary>
    /// Статичный класс HashPassword
    /// </summary>
    public static class HashPassword
    {
        public static string HashedPassword(string password)
        {
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
        }

    }

    /// <summary>
    /// Статичный класс EmailHandler
    /// </summary>
    public static class EmailHandler
    {
        public static void SendMessage(User user)
        {
            Console.WriteLine($"[EMAIL] Отправка письма на {user.Email}: 'Добро пожаловать, {user.UserName}!'");
        }
    }

    /// <summary>
    /// Класс DataBase
    /// </summary>
    public class DataBase
    {
        public void SaveData(User newUser)
        {
            Console.WriteLine($"[DB] Сохранение пользователя {newUser.UserName} с паролем {newUser.Password} в таблицу Users.");
        }
    }

    /// <summary>
    /// Класс UserRegistrationService
    /// </summary>
    public class UserRegistrationService
    {
        // Поля
        private DataBase db = new DataBase();

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="username">Имя пользователя</param>
        /// <param name="password">Пароль</param>
        /// <param name="email">Почта</param>
        public void RegisterUser(string username, string password, string email)
        {
            string hashedPassword = HashPassword.HashedPassword(password);
            User newUser = new User(username, hashedPassword, email);
            db.SaveData(newUser);
            EmailHandler.SendMessage(newUser);

        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {

            UserRegistrationService userRegistrationService = new UserRegistrationService();

            Console.Write("Введите имя пользователя: ");
            string username = Console.ReadLine();

            Console.Write("Введите  вашу электронную почту: ");
            string email = Console.ReadLine();

            Console.Write("Введите пароль: ");
            string password = Console.ReadLine();

            userRegistrationService.RegisterUser(username, password, email);
        }
    }
}
