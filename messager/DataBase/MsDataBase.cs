using Messenger.Domain;
using Microsoft.Data.SqlClient;

namespace Messenger.DataBase
{
    public static class MsDataBase
    {
        private static string connectionString = "";

        public static bool CreateUser(User user)
        {
            //Создание нового пользователя

            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            SqlCommand command_users = new SqlCommand(
                $"INSERT INTO users (name, password)" +
                $"VALUES ('{user.Name}', '{user.Password}');",
                connection);

            int res = command_users.ExecuteNonQuery();

            if (res > 0) { return true; }
            else { return false; }
        }

        public static int? LoginUser(User user) {
            //Логирование

            Random rnd = new Random();
            int session = rnd.Next(0, 100000);

            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            SqlCommand command_users = new SqlCommand(
                $"UPDATE users SET session = {session} WHERE name = '{user.Name}' AND password = '{user.Password}';",
                connection);

            int res = command_users.ExecuteNonQuery();

            if (res > 0) { return session; }
            else { return null; }
        }
        public static string? GetUserName(string session)
        {
            //Возвращение имени пользователя по сессии

            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            SqlCommand command_users = new SqlCommand(
                $"SELECT name FROM users WHERE session = {session};",
                connection);

            SqlDataReader res = command_users.ExecuteReader();

            res.Read();
            string? name = res.GetString(0);

            return name;
        }
        public static bool UniqueName(string name)
        {
            //Проверка на уникальное имя

            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            SqlCommand command_users = new SqlCommand(
                $"SELECT * FROM users WHERE name = '{name}'"
                , connection);

            int res = command_users.ExecuteNonQuery();

            if (res > 0) { return false; }
            else { return true; }
        }
        public static bool CreateNewMessage(Messang messang)
        {
            //Создание нового сообщения

            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            SqlCommand command_users = new SqlCommand(
                $"INSERT INTO messages (name_user, date, message) " +
                $"VALUES ('{messang.Name}', '{messang.time.ToString("yyy-MM-dd HH:mm:ss")}', '{messang.Text}');"
                , connection);

            int res = command_users.ExecuteNonQuery();

            if (res > 0) { return true; }
            else { return false; }
        }
        public static List<Messang> GetMessage(int number) 
        {
            //Возвращение последних number сообщений
            
            List<Messang> messages = new List<Messang>();

            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            SqlCommand command_users = new SqlCommand(
                $"SELECT TOP {number} name_user, date, message FROM messages ORDER BY id DESC;",
                connection);

            SqlDataReader res = command_users.ExecuteReader();

            while (res.Read())
            {
                Messang mes = new Messang(res.GetString(0), res.GetDateTime(1), res.GetString(2));

                messages.Add(mes);
            }

            return messages;
        }
        public static List<Messang> GetMessage(DateTime time)
        {
            //Возвращение сообещний новее time

            List<Messang> messages = new List<Messang>();

            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            SqlCommand command_users = new SqlCommand(
                $"SELECT name_user, date, message FROM messages WHERE date > '{time.ToString("yyy-MM-dd HH:mm:ss")}';",
                connection);

            SqlDataReader res = command_users.ExecuteReader();

            while (res.Read())
            {
                Messang mes = new Messang(res.GetString(0), res.GetDateTime(1), res.GetString(2));

                messages.Add(mes);
            }

            return messages;
        }
    }
}
