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
    /// 客户信息管理
    /// </summary>
    public class Client : IEntity
    {
        /// <summary>
        /// 客户姓名
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public string Clientname { get; set; }
        /// <summary>
        /// 客户电话
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public string Clienttel { get; set; }
        /// <summary>
        /// 客户地址
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public string Clientaddress { get; set; }
        /// <summary>
        /// 客户邮箱
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public string Clientemail { get; set; }

    }
}
