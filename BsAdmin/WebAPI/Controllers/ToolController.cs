using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Entitys;
using SqlSugar;
using System.Reflection;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ToolController : ControllerBase
    {
        public ISqlSugarClient _db;
        public ToolController(ISqlSugarClient db)
        {
            _db = db;
        }
        [HttpGet]
        public string InitDateBase()
        {
            string res = "OK";
            //如果不存在则创建数据库
            _db.DbMaintenance.CreateDatabase();
            //创建表
            string nspace = "Model.Entitys";
            //通过反射读取我们想要的类
            Type[] ass = Assembly.LoadFrom(AppContext.BaseDirectory + "Model.dll").GetTypes().Where(p => p.Namespace == nspace).ToArray();
            _db.CodeFirst.SetStringDefaultLength(200).InitTables(ass);
            //初始化炒鸡管理员和菜单
            //Users user = new Users()
            //{
            //    Name = "admin",
            //    NickName = "炒鸡管理员",
            //    Password = "123456",
            //    UserType = 0,
            //    IsEnable = true,
            //    Description = "数据库初始化时默认添加的炒鸡管理员",
            //    CreateDate = DateTime.Now,
            //    CreateUserId = 0,
            //    IsDeleted = 0
            //};
            //long userId = _db.Insertable(user).ExecuteReturnBigIdentity();


            //Menu menuRoot = new Menu()
            //{
            //    Name = "菜单管理",
            //    Index = "menumanager",
            //    FilePath = "../view/admin/menu/MenuManager",
            //    ParentId = 0,
            //    Order = 0,
            //    IsEnable = true,
            //    Description = "数据库初始化时默认添加的默认菜单",
            //    CreateDate = DateTime.Now,
            //    CreateUserId = userId,
            //    IsDeleted = 0
            //};
            //_db.Insertable(menuRoot).ExecuteReturnBigIdentity();
            //Menu menuRoot1 = new Menu()
            //{
            //    Name = "用户管理",
            //    Index = "personmanager",
            //    FilePath = "../view/admin/person/PersonManager",
            //    ParentId = 0,
            //    Order = 1,
            //    IsEnable = true,
            //    Description = "数据库初始化时默认添加的默认菜单",
            //    CreateDate = DateTime.Now,
            //    CreateUserId = userId,
            //    IsDeleted = 0
            //};
            //_db.Insertable(menuRoot1).ExecuteReturnBigIdentity();

            //Menu menuRoot3 = new Menu()
            //{
            //    Name = "角色管理",
            //    Index = "rolemanager",
            //    FilePath = "../view/admin/role/RoleManager",
            //    ParentId = 0,
            //    Order = 2,
            //    IsEnable = true,
            //    Description = "数据库初始化时默认添加的默认菜单",
            //    CreateDate = DateTime.Now,
            //    CreateUserId = userId,
            //    IsDeleted = 0
            //};
            //_db.Insertable(menuRoot3).ExecuteReturnBigIdentity();

            //Menu menuRoot4 = new Menu()
            //{
            //    Name = "员工信息管理",
            //    Index = "employeemanager",
            //    FilePath = "../view/admin/employee/EmployeeManager",
            //    ParentId = 0,
            //    Order = 3,
            //    IsEnable = true,
            //    Description = "数据库初始化时默认添加的默认菜单",
            //    CreateDate = DateTime.Now,
            //    CreateUserId = userId,
            //    IsDeleted = 0
            //};
            //_db.Insertable(menuRoot4).ExecuteReturnBigIdentity();

            //Menu menuRoot5 = new Menu()
            //{
            //    Name = "部门信息管理",
            //    Index = "departmentmanager",
            //    FilePath = "../view/admin/department/DepartmentManager",
            //    ParentId = 0,
            //    Order = 4,
            //    IsEnable = true,
            //    Description = "数据库初始化时默认添加的默认菜单",
            //    CreateDate = DateTime.Now,
            //    CreateUserId = userId,
            //    IsDeleted = 0
            //};
            //_db.Insertable(menuRoot5).ExecuteReturnBigIdentity();

            //Menu menuRoot6 = new Menu()
            //{
            //    Name = "职务信息管理",
            //    Index = "position",
            //    FilePath = "../view/admin/position/PositionManager",
            //    ParentId = 0,
            //    Order = 5,
            //    IsEnable = true,
            //    Description = "数据库初始化时默认添加的默认菜单",
            //    CreateDate = DateTime.Now,
            //    CreateUserId = userId,
            //    IsDeleted = 0
            //};
            //_db.Insertable(menuRoot6).ExecuteReturnBigIdentity();

            //Menu menuRoot7 = new Menu()
            //{
            //    Name = "合同信息管理",
            //    Index = "contract",
            //    FilePath = "../view/admin/contract/ContractManager",
            //    ParentId = 0,
            //    Order = 6,
            //    IsEnable = true,
            //    Description = "数据库初始化时默认添加的默认菜单",
            //    CreateDate = DateTime.Now,
            //    CreateUserId = userId,
            //    IsDeleted = 0
            //};
            //_db.Insertable(menuRoot7).ExecuteReturnBigIdentity();

            //Menu menuRoot8 = new Menu()
            //{
            //    Name = "客户信息管理",
            //    Index = "client",
            //    FilePath = "../view/admin/client/ClientManager",
            //    ParentId = 0,
            //    Order = 7,
            //    IsEnable = true,
            //    Description = "数据库初始化时默认添加的默认菜单",
            //    CreateDate = DateTime.Now,
            //    CreateUserId = userId,
            //    IsDeleted = 0
            //};
            //_db.Insertable(menuRoot8).ExecuteReturnBigIdentity();


            return res;
        }

        [HttpGet]
        public string Test()
        {
            return "OK";
        }
    }
}
