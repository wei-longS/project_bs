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
    /// 登录日志
    /// </summary>
    public class Loginlog:IEntity
    {
        /// <summary>
        /// 账户名
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public string Loginlogname { get; set; }
        /// <summary>
        /// 日志内容
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public string Loginlogcontent { get; set; }
    }
}
