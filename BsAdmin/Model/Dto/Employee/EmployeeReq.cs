using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dto.Employee
{
    public class EmployeeReq
    {
        public string Nameid { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public long Age { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
        public long Tel { get; set; }
        public string Description { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
