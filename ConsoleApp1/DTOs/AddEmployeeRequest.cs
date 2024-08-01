using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ConsoleApp1.DTOs
{
    public record AddEmployeeRequest
    {
        #region Props

        [Required(ErrorMessage = $"Поле {nameof(FirstName)} Обязательно")]
        [MinLength(1, ErrorMessage = $"Поле {nameof(FirstName)} Не дожно быть пустым")]
        public string? FirstName { get; set; } = String.Empty;

        [Required(ErrorMessage = $"Поле {nameof(LastName)} Обязательно")]
        [MinLength(1, ErrorMessage = $"Поле {nameof(LastName)} Не дожно быть пустым")]
        public string? LastName { get; set; } = String.Empty;

        [Required(ErrorMessage = $"Поле {nameof(SalaryPerHour)} Обязательно")]
        [Range(0.01, double.MaxValue, ErrorMessage = $"Значение {nameof(SalaryPerHour)} должно быть больше 0")]
        public decimal? SalaryPerHour { get; set; } = null;

        #endregion
    }
}
