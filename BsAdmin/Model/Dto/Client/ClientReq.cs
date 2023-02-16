using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dto.Client
{
    public class ClientReq
    {
        public string Clientname { get; set; }
        public string Clienttel { get; set; }
        public string Clientaddress { get; set; }
        public string Clientemail { get; set; }
        public string Description { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
