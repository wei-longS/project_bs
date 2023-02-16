using Model.Dto.Department;
using Model.Dto.Employee;
using Model.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{
    /// <summary>
    /// 部门模块接口
    /// </summary>
    public interface IDepartmentService
    {

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="role"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        bool Add(DepartmentAdd role, long userId);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="role"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        bool Edit(DepartmentEdit role, long userId);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Del(long id);
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        bool BatchDel(string ids);
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        PageInfo GetDepartment(DepartmentReq req);

        DepartmentRes GetDepartmentById(long id);
    }
}
