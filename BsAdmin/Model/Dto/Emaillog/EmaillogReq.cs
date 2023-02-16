using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dto.Emaillog
{
    public class EmaillogReq
    {
        public string Emaillogaddr { get; set; }
        public string Emaillogname { get; set; }
        public string Emaillogtitle { get; set; }
        public string Emaillogcontent { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
