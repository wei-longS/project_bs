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
    /// 合同信息管理
    /// </summary>
    public class Contract : IEntity
    {
        /// <summary>
        /// 合同编号
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public string Contractid { get; set; }
        /// <summary>
        /// 合同名称
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public string Contractname { get; set; }

        /// <summary>
        /// 合同内容
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public string Contractcontent { get; set; }
        /// <summary>
        /// 合同生效期
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public string Contractstart { get; set; }
        /// <summary>
        /// 合同有效期
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public string Contractend { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public string Staffname { get; set; }
        /// <summary>
        /// 合同金额
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public double Amount { get; set; }
    }
}
