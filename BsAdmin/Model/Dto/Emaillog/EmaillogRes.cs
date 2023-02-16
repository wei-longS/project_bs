using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dto.Emaillog
{
    public class EmaillogRes
    {
        public long Id { get; set; }
        public string Emaillogaddr { get; set; }
        public string Emaillogname { get; set; }
        public string Emaillogtitle { get; set; }
        public string Emaillogcontent { get; set; }
    }
}
