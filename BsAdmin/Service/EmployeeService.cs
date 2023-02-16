using AutoMapper;
using Interface;
using Model.Dto.Employee;
using Model.Dto.Role;
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
    public class EmployeeService :IEmployeeService
    {
        private readonly IMapper _mapper;
        private ISqlSugarClient _db { get; set; }
        public EmployeeService(IMapper mapper, ISqlSugarClient db)
        {
            _mapper = mapper;
            _db = db;
        }
        public bool Add(EmployeeAdd req, long userId)
        {
            Employee info = _mapper.Map<Employee>(req);
            info.CreateUserId = userId;
            info.Nameid = Service.Helper.employee();
            info.CreateDate = DateTime.Now;
            info.IsDeleted = 0;
            return _db.Insertable(info).ExecuteCommand() > 0;
        }

        public bool Del(long id)
        {
            var info = _db.Queryable<Employee>().First(p => p.Id == id);
            return _db.Deleteable(info).ExecuteCommand() > 0;
        }
        public bool BatchDel(string ids)
        {
            return _db.Ado.ExecuteCommand($"DELETE Employee WHERE Id IN({ids})") > 0;
        }

        public bool Edit(EmployeeEdit req, long userId)
        {
            var role = _db.Queryable<Employee>().First(p => p.Id == req.Id);
            _mapper.Map(req, role);
            role.ModifyUserId = userId;
            role.ModifyDate = DateTime.Now;
            return _db.Updateable(role).ExecuteCommand() > 0;
        }

        public EmployeeRes GetEmployeeById(long id)
        {
            var info = _db.Queryable<Employee>().First(p => p.Id == id);
            return _mapper.Map<EmployeeRes>(info);
        }

        public PageInfo GetEmployee(EmployeeReq req)
        {
            
        //    Console.WriteLine(req);
            PageInfo pageInfo = new PageInfo();
            var exp = _db.Queryable<Employee>()
               .WhereIF(!string.IsNullOrEmpty(req.Name), p => p.Name.Contains(req.Name))
            .WhereIF(!string.IsNullOrEmpty(req.Department), p => p.Department.Contains(req.Department));
            //.WhereIF(!string.IsNullOrEmpty(req.Description), p => p.Name.Contains(req.Description));
            var res = exp
                .OrderBy(p => p.Order)
                .Skip((req.PageIndex - 1) * req.PageSize)
                .Take(req.PageSize)
                .ToList();

            pageInfo.Data = _mapper.Map<List<EmployeeRes>>(res);
       
            pageInfo.Total = exp.Count();
            return pageInfo;
        }

        public PageInfo GetEmployeeCount(EmployeeReq req)
        {
            PageInfo pageInfo = new PageInfo();
            var exp = _db.Queryable<Employee>().WhereIF(!string.IsNullOrEmpty(req.Department), p => p.Department.Contains(req.Department));
            var res = exp
                .OrderBy(p => p.Order)
                .Skip((req.PageIndex - 1) * req.PageSize)
                .Take(req.PageSize)
                .ToList();
            pageInfo.Data = _mapper.Map<List<EmployeeRes>>(res);
            pageInfo.Total = exp.Count();
            return pageInfo;
        }


    }
}
