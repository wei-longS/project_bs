using AutoMapper;
using Interface;
using Model.Dto.Department;
using Model.Dto.Employee;
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
    public class DepartmentService : IDepartmentService
    {
        private readonly IMapper _mapper;
        private ISqlSugarClient _db { get; set; }
        public DepartmentService(IMapper mapper, ISqlSugarClient db)
        {
            _mapper = mapper;
            _db = db;
        }
        public bool Add(DepartmentAdd req, long userId)
        {
            Department info = _mapper.Map<Department>(req);
            info.Departmentnameid = Service.Helper.department();
            info.CreateUserId = userId;
            info.CreateDate = DateTime.Now;
            info.IsDeleted = 0;
            return _db.Insertable(info).ExecuteCommand() > 0;
        }
        public bool Del(long id)
        {
            var info = _db.Queryable<Department>().First(p => p.Id == id);
            return _db.Deleteable(info).ExecuteCommand() > 0;
        }
        public bool BatchDel(string ids)
        {
            return _db.Ado.ExecuteCommand($"DELETE Department WHERE Id IN({ids})") > 0;
        }
        public bool Edit(DepartmentEdit req, long userId)
        {
            var role = _db.Queryable<Department>().First(p => p.Id == req.Id);
            _mapper.Map(req, role);
            role.ModifyUserId = userId;
            role.ModifyDate = DateTime.Now;
            return _db.Updateable(role).ExecuteCommand() > 0;
        }

        public DepartmentRes GetDepartmentById(long id)
        {
            var info = _db.Queryable<Department>().First(p => p.Id == id);
            return _mapper.Map<DepartmentRes>(info);
        }

        public PageInfo GetDepartment(DepartmentReq req)
        {
            PageInfo pageInfo = new PageInfo();
            var exp = _db.Queryable<Department>().WhereIF(!string.IsNullOrEmpty(req.Departmentname), p => p.Departmentname.Contains(req.Departmentname));
            var res = exp
                .OrderBy(p => p.Order)  
                .Skip((req.PageIndex - 1) * req.PageSize)
                .Take(req.PageSize)
                .ToList();
            pageInfo.Data = _mapper.Map<List<DepartmentRes>>(res);
            pageInfo.Total = exp.Count();
            return pageInfo;
        }



    }
}
