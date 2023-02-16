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
            return pageInfo;
        }
    }
}
