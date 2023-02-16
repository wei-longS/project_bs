using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dto.Contract
{
    public class ContractEdit
    {
        public long Id { get; set; }
        public string Contractid { get; set; }
        public string Contractname { get; set; }
        public string Contractcontent { get; set; }
        public string Contractstart { get; set; }
        public string Contractend { get; set; }
        public string Staffname { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }
    }
}
