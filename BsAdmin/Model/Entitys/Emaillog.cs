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
    /// 邮箱日志
    /// </summary>
    public class Emaillog :IEntity
    {
        /// <summary>
        /// 邮箱地址
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public string Emaillogaddr { get; set; }

        /// <summary>
        /// 邮件昵称
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public string Emaillogname { get; set; }

        /// <summary>
        /// 邮箱标题
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public string Emaillogtitle { get; set; }

        /// <summary>
        /// 邮箱内容
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public string Emaillogcontent { get; set; }

    }
}
