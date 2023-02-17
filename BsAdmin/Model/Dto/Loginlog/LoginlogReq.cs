using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dto.Loginlog
{
    public class LoginlogReq
    {
        public string Loginlogname { get; set; }
        public string Loginlogcontent { get; set; }
        public DateTime CreateDate { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
