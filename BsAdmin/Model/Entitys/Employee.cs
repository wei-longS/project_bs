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
    /// 员工信息表
    /// </summary>
    public class Employee : IEntity
    {
        /// <summary>
        /// 员工编号
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public string Nameid { get; set; }
        /// <summary>
        /// 员工姓名
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public string Name { get; set; }
        /// <summary>
        /// 员工性别
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public string Sex { get; set; }
        /// <summary>
        /// 员工年龄
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public long Age { get; set; }
        /// <summary>
        /// 所在部门
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public string Department { get; set; }
        /// <summary>
        /// 职位
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public string Position { get; set; }    
        /// <summary>
        /// 联系电话
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public long Tel { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public int Order { get; set; }

        
    }
}
