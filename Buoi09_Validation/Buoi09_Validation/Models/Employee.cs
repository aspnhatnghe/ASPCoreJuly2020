using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Buoi09_Validation.Models
{
    public enum Gender
    {
        Nam, Nữ
    }
    public class Employee
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 5)]
        public string EmployeeNo { get; set; }

        [Required]
        public string FullName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string Phone { get; set; }

        [Url]
        public string Website { get; set; }

        [DataType(DataType.Date)]
        [BirthDateCheck]
        public string NgaySinh { get; set; }

        public Gender GioiTinh { get; set; }

        [Range(0, double.MaxValue)]
        public double? Salary { get; set; }

        public bool IsPartTime { get; set; }

        [CreditCard]
        public string CreditCard { get; set; }

        [MaxLength(255)]
        [DataType(DataType.MultilineText)]
        public string MoreInfo { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
