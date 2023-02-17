using Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Dto.Emaillog;
using Model.Dto.Loginlog;
using Model.Other;
using WebAPI.Config;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginlogController : ControllerBase
    {
        private readonly ILoginlogService _loginlog;
        public LoginlogController(ILoginlogService loginlog)
        {
            _loginlog = loginlog;
        }
        [HttpPost]
        public ApiResult GetLoginlogs(LoginlogReq req)
        {
            return ResultHelper.Success(_loginlog.GetLoginlog(req));
        }
        [HttpGet]
        public ApiResult GetLoginlog(long id)
        {
            return ResultHelper.Success(_loginlog.GetLoginlogById(id));
        }
        [HttpPost]
        public ApiResult Add(LoginlogAdd req)
        {
            //获取当前登录人信息
            long userId = Convert.ToInt32(HttpContext.User.Claims.ToList()[0].Value);


            return ResultHelper.Success(_loginlog.Add(req, userId));
        }
        [HttpPost]
        public ApiResult Edit(LoginlogEdit req)
        {
            //获取当前登录人信息
            long userId = Convert.ToInt32(HttpContext.User.Claims.ToList()[0].Value);
            return ResultHelper.Success(_loginlog.Edit(req, userId));
        }
        [HttpGet]
        public ApiResult Del(long id)
        {
            //获取当前登录人信息 
            return ResultHelper.Success(_loginlog.Del(id));
        }
        [HttpGet]
        public ApiResult BatchDel(string ids)
        {
            //获取当前登录人信息 
            return ResultHelper.Success(_loginlog.BatchDel(ids));
        }

    }
}
