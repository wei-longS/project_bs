using AutoMapper;
using Interface;
using Model.Dto.Client;
using Model.Dto.Contract;
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
    public class ClientService :IClientService
    {
        private readonly IMapper _mapper;
        private ISqlSugarClient _db { get; set; }
        public ClientService(IMapper mapper, ISqlSugarClient db)
        {
            _mapper = mapper;
            _db = db;
        }
        public bool Add(ClientAdd req, long userId)
        {
            Client info = _mapper.Map<Client>(req);
            info.CreateUserId = userId;
            info.CreateDate = DateTime.Now;
            info.IsDeleted = 0;
            return _db.Insertable(info).ExecuteCommand() > 0;
        }
        public bool Del(long id)
        {
            var info = _db.Queryable<Client>().First(p => p.Id == id);
            return _db.Deleteable(info).ExecuteCommand() > 0;
        }
        public bool BatchDel(string ids)
        {
            return _db.Ado.ExecuteCommand($"DELETE Client WHERE Id IN({ids})") > 0;
        }
        public bool Edit(ClientEdit req, long userId)
        {
            var role = _db.Queryable<Client>().First(p => p.Id == req.Id);
            _mapper.Map(req, role);
            role.ModifyUserId = userId;
            role.ModifyDate = DateTime.Now;
            return _db.Updateable(role).ExecuteCommand() > 0;
        }

        public ClientRes GetClientById(long id)
        {
            var info = _db.Queryable<Client>().First(p => p.Id == id);
            return _mapper.Map<ClientRes>(info);
        }

        public PageInfo GetClient(ClientReq req)
        {
            PageInfo pageInfo = new PageInfo();
            var exp = _db.Queryable<Client>().WhereIF(!string.IsNullOrEmpty(req.Clientname), p => p.Clientname.Contains(req.Clientname));
            var res = exp
                .OrderBy(p => p.Id)
                .Skip((req.PageIndex - 1) * req.PageSize)
                .Take(req.PageSize)
                .ToList();
            pageInfo.Data = _mapper.Map<List<ClientRes>>(res);
            pageInfo.Total = exp.Count();
            return pageInfo;
        }
    }
}
