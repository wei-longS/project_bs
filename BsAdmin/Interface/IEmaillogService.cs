using Model.Dto.Client;
using Model.Dto.Emaillog;
using Model.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{
    /// <summary>
    /// 邮箱日志
    /// </summary>
    public interface IEmaillogService
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="role"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        bool Add(EmaillogAdd role, long userId);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="role"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        bool Edit(EmaillogEdit role, long userId);
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
        PageInfo GetEmaillog(EmaillogReq req);

        EmaillogRes GetEmaillogById(long id);
    }
}
