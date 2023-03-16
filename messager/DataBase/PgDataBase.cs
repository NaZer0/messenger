using Messenger.Domain;
using Npgsql;

namespace Messenger.DataBase
{
    internal class PgDataBase
    {
        public static bool CreateUser(User user)
        {
            //Создание нового пользователя

            //Подключение к БД
            NpgsqlConnection sqlConnection = new NpgsqlConnection("");
            sqlConnection.Open();

            //Выполненеие комманды
            NpgsqlCommand sqlCommand = new NpgsqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = $"INSERT INTO users (name, password) " +
                                    $"VALUES ('{user.Name}', '{user.Password}');";

            //Выполнение команды
            int changeLine = sqlCommand.ExecuteNonQuery();

            //Отключение от БД
            sqlConnection.Close();

            //Проверка на изменение в БД
            if (changeLine > 0) return true;
            else return false;
        }

        public static int? LoginUser(User user)
        {
            //Проверка на наличие пользователя
            //В случае, если логин и пароль верный создаётся и возвращается session
            Random rnd = new Random();
            int session = rnd.Next(0, 100000);

            //Подключение к БД
            NpgsqlConnection sqlConnection = new NpgsqlConnection("");
            sqlConnection.Open();

            //Выполненеие комманды
            NpgsqlCommand sqlCommand = new NpgsqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = $"UPDATE users SET \"session\" = {session} WHERE \"name\" = '{user.Name}' AND \"password\" = '{user.Password}';";

            //Выполнение команды
            int changeLine = sqlCommand.ExecuteNonQuery();

            //Отключение от БД
            sqlConnection.Close();

            if (changeLine > 0) { return session; }
            else { return null; }

        }

        public static string? GetUserName(string session)
        {
            //Возвращения имени пользователя по session

            //Подключение к БД
            NpgsqlConnection sqlConnection = new NpgsqlConnection("");
            sqlConnection.Open();

            //Выполненеие комманды
            NpgsqlCommand sqlCommand = new NpgsqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = $"SELECT name FROM users WHERE session={session};";

            //Чтение ответа из БД
            var reader = sqlCommand.ExecuteReader();

            if (reader.Read())
            {
                string name = reader.GetString(0);
                return name;
            }
            else
            {
                return null;
            }
        }

        public static bool UniqueName(string name)
        {
            //Проврка уникальности имени

            //Подключение к БД
            NpgsqlConnection sqlConnection = new NpgsqlConnection("");
            sqlConnection.Open();

            //Выполненеие комманды
            NpgsqlCommand sqlCommand = new NpgsqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = $"SELECT name FROM users WHERE name = '{name}'";

            //Чтение ответа из БД
            var reader = sqlCommand.ExecuteReader();

            bool HasRows = reader.HasRows;

            //Отключение от БД
            sqlConnection.Close();

            //Проверка на наличие строк в результате
            if (HasRows) return false;
            return true;
        }
    }
}
