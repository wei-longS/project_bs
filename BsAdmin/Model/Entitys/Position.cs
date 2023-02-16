using Model.Common;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entitys
{
    /// <summary>
    /// 职务表
    /// </summary>
    public class Position : IEntity
    {
        /// <summary>
        /// 职务编号
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public string Positionid { get; set; }
        /// <summary>
        /// 职务名称
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public string Positionname{ get; set; }
    }
}
