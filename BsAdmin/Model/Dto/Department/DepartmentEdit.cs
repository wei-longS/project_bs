using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dto.Department
{
    public class DepartmentEdit
    {
        public long Id { get; set; }
        public string Departmentnameid { get; set; }
        public string Departmentname { get; set; }
        public string Count { get; set; }
        public string Description { get; set; }
    }
}
