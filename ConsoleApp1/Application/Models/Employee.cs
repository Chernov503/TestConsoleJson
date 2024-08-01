using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Application.Models
{
    public class Employee
    {
        #region Props
        public int Id { get; set; } = int.MinValue;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public decimal SalaryPerHour { get; set; } = decimal.MinValue;

        #endregion
    }
}
