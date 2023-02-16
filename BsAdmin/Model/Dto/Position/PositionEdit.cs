using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dto.Position
{
    public class PositionEdit
    {
        public long Id { get; set; }
        public string Positionid { get; set; }
        public string Positionname { get; set; }
        public string Description { get; set; }
    }
}
