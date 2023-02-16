using Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Dto.Department;
using Model.Dto.Employee;
using Model.Other;
using WebAPI.Config;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _department;
        public DepartmentController(IDepartmentService department)
        {
            _department = department;
        }
        [HttpPost]
        public ApiResult GetDepartments(DepartmentReq req)
        {
            return ResultHelper.Success(_department.GetDepartment(req));
        }
        [HttpGet]
        public ApiResult GetDepartment(long id)
        {
            return ResultHelper.Success(_department.GetDepartmentById(id));
        }
        [HttpPost]
        public ApiResult Add(DepartmentAdd req)
        {
            //获取当前登录人信息
            long userId = Convert.ToInt32(HttpContext.User.Claims.ToList()[0].Value);
            return ResultHelper.Success(_department.Add(req, userId));
        }
        [HttpPost]
        public ApiResult Edit(DepartmentEdit req)
        {
            //获取当前登录人信息
            long userId = Convert.ToInt32(HttpContext.User.Claims.ToList()[0].Value);
            return ResultHelper.Success(_department.Edit(req, userId));
        }
        [HttpGet]
        public ApiResult Del(long id)
        {
            //获取当前登录人信息 
            return ResultHelper.Success(_department.Del(id));
        }
        [HttpGet]
        public ApiResult BatchDel(string ids)
        {
            //获取当前登录人信息 
            return ResultHelper.Success(_department.BatchDel(ids));
        }

    }
}
