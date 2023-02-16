using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dto.Department
{
    public class DepartmentReq
    {
        public string Departmentnameid { get; set; }
        public string Departmentname { get; set; }
        public string Count { get; set; }
        public string Description { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
