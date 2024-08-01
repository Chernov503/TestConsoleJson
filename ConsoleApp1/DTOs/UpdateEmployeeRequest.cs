using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.DTOs
{
    public record UpdateEmployeeRequest
    {
        #region Props

        [Required(ErrorMessage = $"Укажите {nameof(Id)}")]
        public int? Id { get; set; } = null;
        public string? FirstName { get; set; } = String.Empty;
        public string? LastName { get; set; } = String.Empty;
        public decimal? SalaryPerHour { get; set; } = null;

        #endregion
    }
}
