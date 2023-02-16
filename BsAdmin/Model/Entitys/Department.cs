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
    /// 部门信息表
    /// </summary>
    public class Department : IEntity
    {
        /// <summary>
        /// 部门编号
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public string Departmentnameid { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public string Departmentname { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public int Order { get; set; }
        /// <summary>
        /// 部门人数
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public string Count { get; set; }

    }
}
