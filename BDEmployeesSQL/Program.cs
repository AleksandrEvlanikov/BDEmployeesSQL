using System.Diagnostics;

namespace BDEmployeesSQL
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string _connectionString = "Server=localhost;Port=3306;Database=employeesbd;User ID=root;Password=123456789Sasha;";

            EmployeesRepositoryBD employeesRepositoryBD = new EmployeesRepositoryBD(_connectionString);
            CreateRandomValuesEmployees createRandomValuesEmployees = new CreateRandomValuesEmployees();
            FileReportItemFiveEmployees fileReportItemFiveEmployees = new FileReportItemFiveEmployees();
            Stopwatch stopwatch = new Stopwatch();

            int mode;
            if(args.Length > 0 )
            {
                if (!int.TryParse(args[0], out mode))
                {
                    Console.WriteLine("Неверный режим работы программы.");
                    return;
                }
            }
            else
            {
                Console.Write("Введите режим работы программы: ");
                string input = Console.ReadLine();

                if (!int.TryParse(input, out mode))
                {
                    Console.WriteLine("Некорректный режим работы программы.");
                    return;
                }
            }

            switch (mode)
            {
                case 0:
                    employeesRepositoryBD.DropTableEmployees();
                    Console.WriteLine("Таблица в БД, удалена.");
                    break;
                case 1:
                    employeesRepositoryBD.CreateEmployeesTable();
                    Console.WriteLine("Таблица в БД, создана.");
                    break;
                case 2:
                    Console.Write("Введите ФИО, дату рождения и пол через пробел: ");
                    string input = Console.ReadLine();

                    string[] inputArgs = input.Split(' ');

                    if (inputArgs.Length >= 4)
                    {
                        Employees newEmployees = new Employees
                        {
                            Surname = inputArgs[0],
                            Name = inputArgs[1],
                            Patronymic = inputArgs[2],
                            BirthDate = DateTime.Parse(inputArgs[3]),
                            Gender = inputArgs[4]
                        };
                        int age = newEmployees.CalculateAgeEmployees(DateTime.Parse(inputArgs[3]));
                        Console.WriteLine($"{inputArgs[0]} => {age}лет.");

                        employeesRepositoryBD.CreateEmployeesInBD(newEmployees);
                    }
                    break;
                case 3:
                    employeesRepositoryBD.GetAllInfoEmployees();
                    break;
                case 4:
                    Console.WriteLine("Генерируем сотрудников........");
                    int count = 1000000;
                    int countWithFEmployees = 100;
                    stopwatch.Start();
                    List<Employees> randomWithFSurnameListEmployees = createRandomValuesEmployees.GenerateRandomEmployeesMaleWithFSurname(countWithFEmployees);
                    List<Employees> randomEmployeesList = createRandomValuesEmployees.GenerateRandomEmployees(count);
                    randomEmployeesList.AddRange(randomWithFSurnameListEmployees);
                    employeesRepositoryBD.MassCreateEmployeesInBD(randomEmployeesList);
                    stopwatch.Stop();
                    string reportContentGeneration = $"Выполнение c оптимизации.\n" +
                        $"{count + countWithFEmployees} сотрудников добавлено в БД за {stopwatch.Elapsed.TotalSeconds} секунд.";
                    fileReportItemFiveEmployees.WriteToFile(reportContentGeneration);
                    Console.WriteLine(reportContentGeneration);
                    break;
                case 5:
                    stopwatch.Start();
                    List<Employees> maleEmployeesWithFSurname = employeesRepositoryBD.GetMaleEmployeesWithFSurname();
                    stopwatch.Stop();
                    string reportContentWithFSurname = $"Сотрудников мужчин с первой буквой в фамилии 'F' => {maleEmployeesWithFSurname.Count}шт. Найдены за {stopwatch.Elapsed.TotalSeconds} секунд.";

                    fileReportItemFiveEmployees.WriteToFile(reportContentWithFSurname);  
                    Console.WriteLine(reportContentWithFSurname);
                    break;
                default:
                    Console.WriteLine("Некорректный режим работы программы.");
                    break;

            }


        }
    }
}
