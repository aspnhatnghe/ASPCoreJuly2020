using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Buoi10_Validation_Layout.Models
{
    public class Employee
    {
        [Display(Name ="Mã nhân viên")]
        [Required(ErrorMessage = "*")]
        [Remote(action: "CheckEmployeeExist", controller: "Employee", ErrorMessage = "Mã này đã tồn tại")]
        public string EmployeeNo { get; set; }

        [RegularExpression("^0[5789][0-9]{8}$")]
        public string Phone { get; set; }

        [RegularExpression(@"^\w+([.+-]\w+)*@\w{2,}([.]\w{2,4})+$", ErrorMessage ="Chưa đúng định dạng email")]
        public string Email { get; set; }
    }
}
