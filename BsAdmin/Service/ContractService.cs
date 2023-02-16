using AutoMapper;
using Interface;
using Model.Dto.Contract;
using Model.Dto.Position;
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
    public class ContractService :IContractService
    {
        private readonly IMapper _mapper;
        private ISqlSugarClient _db { get; set; }
        public ContractService(IMapper mapper, ISqlSugarClient db)
        {
            _mapper = mapper;
            _db = db;
        }
        public bool Add(ContractAdd req, long userId)
        {
            Contract info = _mapper.Map<Contract>(req);
            info.Contractid = Service.Helper.contract();
            info.CreateUserId = userId;
            info.CreateDate = DateTime.Now;
            info.IsDeleted = 0;
            return _db.Insertable(info).ExecuteCommand() > 0;
        }
        public bool Del(long id)
        {
            var info = _db.Queryable<Contract>().First(p => p.Id == id);
            return _db.Deleteable(info).ExecuteCommand() > 0;
        }
        public bool BatchDel(string ids)
        {
            return _db.Ado.ExecuteCommand($"DELETE Contract WHERE Id IN({ids})") > 0;
        }
        public bool Edit(ContractEdit req, long userId)
        {
            var role = _db.Queryable<Contract>().First(p => p.Id == req.Id);
            _mapper.Map(req, role);
            role.ModifyUserId = userId;
            role.ModifyDate = DateTime.Now;
            return _db.Updateable(role).ExecuteCommand() > 0;
        }

        public ContractRes GetContractById(long id)
        {
            var info = _db.Queryable<Contract>().First(p => p.Id == id);
            return _mapper.Map<ContractRes>(info);
        }

        public PageInfo GetContract(ContractReq req)
        {
            PageInfo pageInfo = new PageInfo();
            var exp = _db.Queryable<Contract>().WhereIF(!string.IsNullOrEmpty(req.Contractid), p => p.Contractid.Contains(req.Contractid));
            var res = exp
                .OrderBy(p => p.Id)
                .Skip((req.PageIndex - 1) * req.PageSize)
                .Take(req.PageSize)
                .ToList();
            pageInfo.Data = _mapper.Map<List<ContractRes>>(res);
            pageInfo.Total = exp.Count();
            return pageInfo;
        }
    }
}
