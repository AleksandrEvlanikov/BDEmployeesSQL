using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDEmployeesSQL
{
    internal class FileReportItemFiveEmployees
    {
        private string filePath = "C:\\Users\\aleks\\OneDrive\\Рабочий стол\\C#pog\\InterviewTask\\BDEmployeesSQL\\BDEmployeesSQL\\ReportTimeItemFive.txt";
        public void WriteToFile(string content)
        {
            using (StreamWriter sw = new StreamWriter(filePath, true, Encoding.UTF8))
            {
                sw.WriteLine(content);
            }
        }
    }
}
