using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDEmployeesSQL
{
    internal class EmployeesRepositoryBD
    {
        private readonly string _connectionString;

        public EmployeesRepositoryBD(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void CreateEmployeesTable()
        {
            using MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();
            using (MySqlCommand mySqlCommand = connection.CreateCommand())
            {
                mySqlCommand.CommandText = "CREATE TABLE IF NOT EXISTS Employees (" +
                                           "Id INT AUTO_INCREMENT PRIMARY KEY, " +
                                           "Surname NVARCHAR(255) NOT NULL, " +
                                           "Name NVARCHAR(255) NOT NULL, " +
                                           "Patronymic NVARCHAR(255) NOT NULL, " +
                                           "BirthDate DATE NOT NULL, " +
                                           "Gender NVARCHAR(10) NOT NULL)";
                mySqlCommand.ExecuteNonQuery();
            }
            CreateIndexesSurname();
            CreateIndexesGender();
        }
        public void CreateIndexesGender()
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                using (MySqlCommand mySqlCommand = connection.CreateCommand())
                {
                    mySqlCommand.CommandText = "CREATE INDEX idx_gender ON Employees (Gender) ";
                    mySqlCommand.ExecuteNonQuery();
                }
            }
        }

        public void CreateIndexesSurname()
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                using (MySqlCommand mySqlCommand = connection.CreateCommand())
                {
                    mySqlCommand.CommandText = "CREATE INDEX idx_surname ON Employees (Surname) ";
                    mySqlCommand.ExecuteNonQuery();
                }
            }
        }


        public void CreateEmployeesInBD(Employees employees)
        {
            using MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();

            using (MySqlCommand mySqlCommand = connection.CreateCommand())
            {
                mySqlCommand.CommandText = "INSERT INTO Employees (Surname, Name, Patronymic, BirthDate, Gender) " +
                                       "VALUES (@Surname, @Name, @Patronymic, @BirthDate, @Gender)";

                mySqlCommand.Parameters.AddWithValue("@Surname", employees.Surname);
                mySqlCommand.Parameters.AddWithValue("@Name", employees.Name);
                mySqlCommand.Parameters.AddWithValue("@Patronymic", employees.Patronymic);
                mySqlCommand.Parameters.AddWithValue("@BirthDate", employees.BirthDate);
                mySqlCommand.Parameters.AddWithValue("@Gender", employees.Gender);

                mySqlCommand.ExecuteNonQuery();
            }
        }

        public void MassCreateEmployeesInBD(List<Employees> employeesList)
        {
            using MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();

            using (MySqlCommand mySqlCommand = connection.CreateCommand())
            {
                mySqlCommand.CommandText = "INSERT INTO Employees (Surname, Name, Patronymic, BirthDate, Gender) " +
                                       "VALUES (@Surname, @Name, @Patronymic, @BirthDate, @Gender)";

                foreach (Employees employees in employeesList)
                {
                    mySqlCommand.Parameters.Clear();
                    mySqlCommand.Parameters.AddWithValue("@Surname", employees.Surname);
                    mySqlCommand.Parameters.AddWithValue("@Name", employees.Name);
                    mySqlCommand.Parameters.AddWithValue("@Patronymic", employees.Patronymic);
                    mySqlCommand.Parameters.AddWithValue("@BirthDate", employees.BirthDate);
                    mySqlCommand.Parameters.AddWithValue("@Gender", employees.Gender);

                    mySqlCommand.ExecuteNonQuery();
                }
            }
        }

        public void GetAllInfoEmployees()
        {
            using MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();

            using (MySqlCommand mySqlCommand = connection.CreateCommand())
            {
                mySqlCommand.CommandText = "SELECT Surname, Name, Patronymic, BirthDate, Gender FROM Employees " +
                                           "GROUP BY Surname, Name, Patronymic, BirthDate, Gender " +
                                           "ORDER BY Surname, Name, Patronymic";
                using (MySqlDataReader reader = mySqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string surname = reader["Surname"].ToString();
                        string name = reader["Name"].ToString();
                        string patronymic = reader["Patronymic"].ToString();
                        DateTime birthDate = DateTime.Parse(reader["BirthDate"].ToString());
                        string gender = reader["Gender"].ToString();

                        Employees employees = new Employees();
                        int age = employees.CalculateAgeEmployees(birthDate);

                        Console.WriteLine($"ФИО: {surname} {name} {patronymic}, Дата рождения: {birthDate.ToShortDateString()}, Пол: {gender}, Возраст: {age} лет");
                    }
                }
            }

        }
        public void DropTableEmployees()
        {
            using MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();

            using (MySqlCommand mySqlCommand = connection.CreateCommand())
            {
                mySqlCommand.CommandText = "DROP TABLE IF EXISTS employees";
                mySqlCommand.ExecuteNonQuery();
            }
        }


        public List<Employees> GetMaleEmployeesWithFSurname()
        {
            List<Employees> resultEmployeesWithFSurnameList = new List<Employees>();

            using MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();

            using (MySqlCommand mySqlCommand = connection.CreateCommand())
            {
                mySqlCommand.CommandText = "SELECT Surname, Name, Patronymic, BirthDate, Gender FROM Employees " +
                                   "WHERE Gender = 'Male' AND Surname LIKE 'F%'";

                using (MySqlDataReader reader = mySqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Employees employee = new Employees
                        {
                            Surname = reader["Surname"].ToString(),
                            Name = reader["Name"].ToString(),
                            Patronymic = reader["Patronymic"].ToString(),
                            BirthDate = DateTime.Parse(reader["BirthDate"].ToString()),
                            Gender = reader["Gender"].ToString()
                        };

                        resultEmployeesWithFSurnameList.Add(employee);
                    }
                }
                return resultEmployeesWithFSurnameList;
            }
        }
    }
}
