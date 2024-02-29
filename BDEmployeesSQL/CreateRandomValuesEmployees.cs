using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDEmployeesSQL
{
    internal class CreateRandomValuesEmployees
    {
        public List<Employees> GenerateRandomEmployees(int count)
        {
            List<Employees> employeesList = new List<Employees>();
            Random random = new Random();

            string[] maleName = { "Ivan", "Petr", "Sergei", "Foma", "Jonh", "Sasha", "Egor", "Pavel", "Davis", "Brown", "Miller", "Taylor" };
            string[] femaleNames = { "Anna", "Maria", "Olga", "Elena", "Irina", "Natalia", "Olivia", "Ava", "Sophia", "Isabella", "Mia", };
            string[] surname = { "Ivanov", "Petrov", "Sidorov", "Alexandrov", "Nikolaev", "Andreev", "Dmitriev", "Vladimirov", "Mikhaylov", "Yakovlev", "Ivanenko", "Petrovsky", "Sidorovich", "Ivanovich", "Romanov", "Kuznetsov", };
            string[] patronymic = { "Ivanov", "Petrov", "Sidorov", "Fomin", "Ivanov", "Petrov", "Sidorov", "Fomin", "Alexandrov", "Nikolaev", "Andreev", "Dmitriev", "Vladimirov", "Mikhaylov", "Yakovlev", "Sidorov", "Ivanov", "Romanov", "Kuznetsov", "Popov", "Smirnov", "Kozlov", "Volkov", "Morozov", "Sokolov", "Orlov", "Kuzmina" };

            for(int i = 0; i < count; i++)
            {
                Employees newEmployees = new Employees();
                if (random.Next(2) == 0)
                {
                    newEmployees.Name = maleName[random.Next(maleName.Length)];
                    newEmployees.Gender = "Male";
                    newEmployees.Patronymic = patronymic[random.Next(patronymic.Length )] + "ich";
                    newEmployees.Surname = surname[random.Next(surname.Length)];

                }
                else
                {
                    newEmployees.Name = femaleNames[random.Next(femaleNames.Length)];
                    newEmployees.Gender = "Female";
                    newEmployees.Patronymic = patronymic[random.Next(patronymic.Length)] + "a";
                    newEmployees.Surname = surname[random.Next(surname.Length)] + "a";

                }

                DateTime dateTimeStart = new DateTime(1990, 01, 01);
                DateTime dateTimeEnd = new DateTime(2010, 01, 01);
                int rangeDate = (dateTimeEnd - dateTimeStart).Days;
                newEmployees.BirthDate = dateTimeStart.AddDays(random.Next(rangeDate));

                employeesList.Add(newEmployees);
            } 
            return employeesList;
        }

        public List<Employees> GenerateRandomEmployeesMaleWithFSurname(int count)
        {
            List<Employees> employeesList = new List<Employees>();
            Random random = new Random();

            string[] maleName = { "Ivan", "Petr", "Sergei", "Foma", "Jonh", "Sasha", "Egor", "Pavel", "Davis", "Brown", "Miller", "Taylor" };
            string[] surname = { "Filatov", "Frolov", "Fomin", "Fomin", "Fedotov" };
            string[] patronymic = { "Ivanov", "Petrov", "Sidorov", "Fomin", "Ivanov", "Petrov", "Sidorov", "Fomin", "Alexandrov", "Nikolaev", "Andreev", "Dmitriev", "Vladimirov", "Mikhaylov", "Yakovlev", "Sidorov", "Ivanov", "Romanov", "Kuznetsov", "Popov", "Smirnov", "Kozlov", "Volkov", "Morozov", "Sokolov", "Orlov"};

            for (int i = 0; i < count; i++)
            {
                Employees newEmployees = new Employees();
                newEmployees.Name = maleName[random.Next(maleName.Length)];
                newEmployees.Gender = "Male";
                newEmployees.Patronymic = patronymic[random.Next(patronymic.Length)] + "ich";
                newEmployees.Surname = surname[random.Next(surname.Length)];
                DateTime dateTimeStart = new DateTime(1990, 01, 01);
                DateTime dateTimeEnd = new DateTime(2010, 01, 01);
                int rangeDate = (dateTimeEnd - dateTimeStart).Days;
                newEmployees.BirthDate = dateTimeStart.AddDays(random.Next(rangeDate));

                employeesList.Add(newEmployees);
            }
            return employeesList;
        }

    }
}
