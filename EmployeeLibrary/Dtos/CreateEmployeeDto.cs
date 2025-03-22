using System.ComponentModel.DataAnnotations;

namespace EmployeeLibrary.Dtos
{
    public class CreateEmployeeDto
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string EmpName { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "Salary must be positive.")]
        public decimal Salary { get; set; }
    }
}

