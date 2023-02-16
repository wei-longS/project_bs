using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dto.Contract
{
    public class ContractReq
    {
        public string Contractid { get; set; }
        public string Contractname { get; set; }
        public string Contractcontent { get; set; }
        public string Contractstart { get; set; }
        public string Contractend { get; set; }
        public string Staffname { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
