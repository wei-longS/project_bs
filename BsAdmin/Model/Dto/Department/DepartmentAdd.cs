using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dto.Department
{
    public class DepartmentAdd
    {
        public string Departmentnameid { get; set; }
        public string Departmentname { get; set; }
        public string Count { get; set; }
        public string Description { get; set; }
    }
}
