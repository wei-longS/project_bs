using AutoMapper;
using Model.Dto.Client;
using Model.Dto.Contract;
using Model.Dto.Department;
using Model.Dto.Emaillog;
using Model.Dto.Employee;
using Model.Dto.Loginlog;
using Model.Dto.Menu;
using Model.Dto.Position;
using Model.Dto.Role;
using Model.Dto.User;
using Model.Entitys;

namespace WebAPI.Config
{
    /// <summary>
    /// Dto的映射配置
    /// </summary>
    public class AutoMapperConfigs: Profile
    {
        public AutoMapperConfigs()
        {
            //角色
            CreateMap<Role, RoleRes>();
            CreateMap<RoleAdd, Role>();
            CreateMap<RoleEdit, Role>();
            //用户
            CreateMap<Users, UserRes>();
            CreateMap<UserAdd, Users>();
            CreateMap<UserEdit, Users>();
            //菜单
            CreateMap<Menu, MenuRes>();
            CreateMap<MenuAdd, Menu>();
            CreateMap<MenuEdit, Menu>();
            //员工
            CreateMap<Employee, EmployeeRes>();
            CreateMap<EmployeeAdd, Employee>();
            CreateMap<EmployeeEdit, Employee>();
            //部门
            CreateMap<Department, DepartmentRes>();
            CreateMap<DepartmentAdd, Department>();
            CreateMap<DepartmentEdit, Department>();
            //职务
            CreateMap<Position, PositionRes>();
            CreateMap<PositionAdd, Position>();
            CreateMap<PositionEdit, Position>();
            //合同
            CreateMap<Contract, ContractRes>();
            CreateMap<ContractAdd, Contract>();
            CreateMap<ContractEdit, Contract>();
            //客户
            CreateMap<Client, ClientRes>();
            CreateMap<ClientAdd, Client>();
            CreateMap<ClientEdit, Client>();
            //邮箱日志
            CreateMap<Emaillog, EmaillogRes>();
            CreateMap<EmaillogAdd, Emaillog>();
            CreateMap<EmaillogEdit, Emaillog>();
            //登录日志
            CreateMap<Loginlog, LoginlogRes>();
            CreateMap<LoginlogAdd, Loginlog>();
            CreateMap<LoginlogEdit, Loginlog>();
        }
    }
}
