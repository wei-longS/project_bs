using Ansely.Email;
using AutoMapper;
using Interface;
using Model.Dto.Client;
using Model.Dto.Emaillog;
using Model.Entitys;
using Model.Other;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class EmaillogService :IEmaillogService
    {
        private readonly IMapper _mapper;
        private ISqlSugarClient _db { get; set; }
        public EmaillogService(IMapper mapper, ISqlSugarClient db)
        {
            _mapper = mapper;
            _db = db;
        }
        public bool Add(EmaillogAdd req, long userId)
        {
            Emaillog info = _mapper.Map<Emaillog>(req);
            info.CreateUserId = userId;
            info.CreateDate = DateTime.Now;
            info.IsDeleted = 0;
            //发送邮件
            var emaillogaddr = req.Emaillogaddr;  //邮箱地址
            var emaillogname = req.Emaillogname; //邮件昵称
            var emaillogtitle = req.Emaillogtitle;   //邮箱标题
            var emaillogcontent = req.Emaillogcontent; //邮件内容
            var options = new EmailSenderOptions()
            {
                FromAddr = "1835868573@qq.com",     //     邮件发送者的邮箱地址。
                Secret = "kfzajpilduqocfcg",//授权码
                Host = "smtp.qq.com"
            };
            IEmailSender emailSender = new EmailSender(options);
            var addrs = new List<string>
            {
                emaillogaddr, 
            };
            //   var tmp = new EmailTemplate("wl", "标题", "测试1");
            var tmp = new EmailTemplate(emaillogname, emaillogtitle, emaillogcontent);

                emailSender.Send(tmp, addrs); //发送

            

            return _db.Insertable(info).ExecuteCommand() > 0;
        }
        public bool Del(long id)
        {
            var info = _db.Queryable<Emaillog>().First(p => p.Id == id);
            return _db.Deleteable(info).ExecuteCommand() > 0;
        }
        public bool BatchDel(string ids)
        {
            return _db.Ado.ExecuteCommand($"DELETE Emaillog WHERE Id IN({ids})") > 0;
        }
        public bool Edit(EmaillogEdit req, long userId)
        {
            var role = _db.Queryable<Emaillog>().First(p => p.Id == req.Id);
            _mapper.Map(req, role);
            role.ModifyUserId = userId;
            role.ModifyDate = DateTime.Now;
            return _db.Updateable(role).ExecuteCommand() > 0;
        }

        public EmaillogRes GetEmaillogById(long id)
        {
            var info = _db.Queryable<Emaillog>().First(p => p.Id == id);
            return _mapper.Map<EmaillogRes>(info);
        }

        public PageInfo GetEmaillog(EmaillogReq req)
        {
            PageInfo pageInfo = new PageInfo();
            var exp = _db.Queryable<Emaillog>().WhereIF(!string.IsNullOrEmpty(req.Emaillogtitle), p => p.Emaillogtitle.Contains(req.Emaillogtitle));
            var res = exp
                .OrderBy(p => p.Id)
                .Skip((req.PageIndex - 1) * req.PageSize)
                .Take(req.PageSize)
                .ToList();
            pageInfo.Data = _mapper.Map<List<EmaillogRes>>(res);
            pageInfo.Total = exp.Count();
            return  pageInfo;
        }
    }
}
