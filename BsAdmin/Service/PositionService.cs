using AutoMapper;
using Interface;
using Model.Dto.Department;
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
    public class PositionService :IPositionService
    {
        private readonly IMapper _mapper;
        private ISqlSugarClient _db { get; set; }
        public PositionService(IMapper mapper, ISqlSugarClient db)
        {
            _mapper = mapper;
            _db = db;
        }
        public bool Add(PositionAdd req, long userId)
        {
            Position info = _mapper.Map<Position>(req);
            info.Positionid = Service.Helper.position();
            info.CreateUserId = userId;
            info.CreateDate = DateTime.Now;
            info.IsDeleted = 0;
            return _db.Insertable(info).ExecuteCommand() > 0;
        }
        public bool Del(long id)
        {
            var info = _db.Queryable<Position>().First(p => p.Id == id);
            return _db.Deleteable(info).ExecuteCommand() > 0;
        }
        public bool BatchDel(string ids)
        {
            return _db.Ado.ExecuteCommand($"DELETE Position WHERE Id IN({ids})") > 0;
        }
        public bool Edit(PositionEdit req, long userId)
        {
            var role = _db.Queryable<Position>().First(p => p.Id == req.Id);
            _mapper.Map(req, role);
            role.ModifyUserId = userId;
            role.ModifyDate = DateTime.Now;
            return _db.Updateable(role).ExecuteCommand() > 0;
        }

        public PositionRes GetPositionById(long id)
        {
            var info = _db.Queryable<Position>().First(p => p.Id == id);
            return _mapper.Map<PositionRes>(info);
        }

        public PageInfo GetPosition(PositionReq req)
        {
            PageInfo pageInfo = new PageInfo();
            var exp = _db.Queryable<Position>().WhereIF(!string.IsNullOrEmpty(req.Positionname), p => p.Positionname.Contains(req.Positionname));
            var res = exp
                .OrderBy(p => p.Id)
                .Skip((req.PageIndex - 1) * req.PageSize)
                .Take(req.PageSize)
                .ToList();
            pageInfo.Data = _mapper.Map<List<PositionRes>>(res);
            pageInfo.Total = exp.Count();
            return pageInfo;
        }

    }
}
