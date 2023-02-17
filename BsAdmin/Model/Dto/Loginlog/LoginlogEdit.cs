using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dto.Loginlog
{
    public class LoginlogEdit
    {
        public long Id { get; set; }
        public string Loginlogname { get; set; }
        public string Loginlogcontent { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
