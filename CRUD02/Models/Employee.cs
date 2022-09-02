using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD02.Models
{
    public class Employee : BaseEntity
    {
        public String Name { get; set; }
        public String Email { get; set; }
        public String Mobile { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public int? EmployeeTypeId { get; set; }
        public EmployeeType EmployeeType { get; set; }


    }
}
