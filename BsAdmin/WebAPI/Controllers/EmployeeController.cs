using Ansely.Email;
using Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Dto.Employee;
using Model.Dto.Role;
using Model.Other;
using Org.BouncyCastle.Ocsp;
using WebAPI.Config;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employee;
        public EmployeeController(IEmployeeService employee)
        {
            _employee = employee;
        }

        //private readonly IEmailSender emailSender;

        //public EmployeeController(IEmailSender emailSender)
        //{
        //    this.emailSender = emailSender;
        //}



        [HttpPost]
        public ApiResult GetEmployees(EmployeeReq req)
        {
           
            return ResultHelper.Success(_employee.GetEmployee(req)) ;
        }
        [HttpGet]
        public ApiResult GetEmployee(long id)
        {
            return ResultHelper.Success(_employee.GetEmployeeById(id));
        }
        [HttpGet]
        public ApiResult GetEmployeeCount(EmployeeReq req)
        {
            return ResultHelper.Success(_employee.GetEmployee(req));
        }

        [HttpPost]
        public ApiResult Add(EmployeeAdd req)
        {
            //获取当前登录人信息
            long userId = Convert.ToInt32(HttpContext.User.Claims.ToList()[0].Value);
            return ResultHelper.Success(_employee.Add(req, userId));
        }
        [HttpPost]
        public ApiResult Edit(EmployeeEdit req)
        {
            //获取当前登录人信息
            long userId = Convert.ToInt32(HttpContext.User.Claims.ToList()[0].Value);
            return ResultHelper.Success(_employee.Edit(req, userId));
        }
        [HttpGet]
        public ApiResult Del(long id)
        {
            //获取当前登录人信息 
            return ResultHelper.Success(_employee.Del(id));
        }
        [HttpGet]
        public ApiResult BatchDel(string ids)
        {
            //获取当前登录人信息 
            return ResultHelper.Success(_employee.BatchDel(ids));
        }
    }
}
