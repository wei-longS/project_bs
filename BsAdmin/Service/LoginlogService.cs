using Ansely.Email;
using AutoMapper;
using Interface;
using Model.Dto.Emaillog;
using Model.Dto.Loginlog;
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
    public class LoginlogService:ILoginlogService
    {
        private readonly IMapper _mapper;
        private ISqlSugarClient _db { get; set; }
        public LoginlogService(IMapper mapper, ISqlSugarClient db)
        {
            _mapper = mapper;
            _db = db;
        }
        public bool Add(LoginlogAdd req, long userId)
        {
            Loginlog info = _mapper.Map<Loginlog>(req);
            info.CreateUserId = userId;
            info.CreateDate = DateTime.Now;
            info.IsDeleted = 0;

            return _db.Insertable(info).ExecuteCommand() > 0;
        }
        public bool Del(long id)
        {
            var info = _db.Queryable<Loginlog>().First(p => p.Id == id);
            return _db.Deleteable(info).ExecuteCommand() > 0;
        }
        public bool BatchDel(string ids)
        {
            return _db.Ado.ExecuteCommand($"DELETE Loginlog WHERE Id IN({ids})") > 0;
        }
        public bool Edit(LoginlogEdit req, long userId)
        {
            var role = _db.Queryable<Loginlog>().First(p => p.Id == req.Id);
            _mapper.Map(req, role);
            role.ModifyUserId = userId;
            role.ModifyDate = DateTime.Now;
            return _db.Updateable(role).ExecuteCommand() > 0;
        }

        public LoginlogRes GetLoginlogById(long id)
        {
            var info = _db.Queryable<Loginlog>().First(p => p.Id == id);
            return _mapper.Map<LoginlogRes>(info);
        }

        public PageInfo GetLoginlog(LoginlogReq req)
        {
            PageInfo pageInfo = new PageInfo();
            var exp = _db.Queryable<Loginlog>().WhereIF(!string.IsNullOrEmpty(req.Loginlogname), p => p.Loginlogname.Contains(req.Loginlogname));
            var res = exp
                .OrderBy(p => p.Id)
                .Skip((req.PageIndex - 1) * req.PageSize)
                .Take(req.PageSize)
                .ToList();
            pageInfo.Data = _mapper.Map<List<LoginlogRes>>(res);
            pageInfo.Total = exp.Count();
            return pageInfo;
        }
    }
}
