using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dto.Client
{
    public class ClientRes
    {
        public long Id { get; set; }
        public string Clientname { get; set; }
        public string Clienttel { get; set; }
        public string Clientaddress { get; set; }
        public string Clientemail { get; set; }
        public string Description { get; set; }
    }
}
