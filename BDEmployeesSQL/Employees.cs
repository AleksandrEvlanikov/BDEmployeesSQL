using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDEmployeesSQL
{
    internal class Employees
    {
        public int Id {  get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }


        public int CalculateAgeEmployees(DateTime BirthDate)
        {
            DateTime today = DateTime.Today;
            int ageEmployees = today.Year - BirthDate.Year;
            return ageEmployees;
        }
    }
}
