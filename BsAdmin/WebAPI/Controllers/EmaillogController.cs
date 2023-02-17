using Ansely.Email;
using Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Dto.Client;
using Model.Dto.Emaillog;
using Model.Entitys;
using Model.Other;
using WebAPI.Config;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class EmaillogController : ControllerBase
    {
        private readonly IEmaillogService _emaillog;
        public EmaillogController(IEmaillogService emaillog)
        {
            _emaillog = emaillog;
        }
        [HttpPost]
        public ApiResult GetEmaillogs(EmaillogReq req)
        {
            return ResultHelper.Success(_emaillog.GetEmaillog(req));
        }
        [HttpGet]
        public ApiResult GetEmaillog(long id)
        {
            return ResultHelper.Success(_emaillog.GetEmaillogById(id));
        }
        [HttpPost]
        public  ApiResult  Add(EmaillogAdd req)
        {
            //获取当前登录人信息
            long userId = Convert.ToInt32(HttpContext.User.Claims.ToList()[0].Value);
            //var emaillogaddr = req.Emaillogaddr;  //邮箱地址
            //var emaillogname = req.Emaillogname; //邮件昵称
            //var emaillogtitle = req.Emaillogtitle;   //邮箱标题
            //var emaillogcontent = req.Emaillogcontent; //邮件内容
            //var options = new EmailSenderOptions()
            //{
            //    FromAddr = "1835868573@qq.com",     //     邮件发送者的邮箱地址。
            //    Secret = "kfzajpilduqocfcg",//授权码
            //    Host = "smtp.qq.com"
            //};

            //IEmailSender emailSender = new EmailSender(options);
            //var addrs = new List<string>
            //{
            //  //  "1835868573@qq.com",
            //    emaillogaddr,
            //  // "2528838602@qq.com",
            //   //"2050427292@qq.com",  
            //};
            
            //var tmp = new EmailTemplate(emaillogname, emaillogtitle, emaillogcontent);

            //emailSender.Send(tmp, addrs);


            //   var tmp = new EmailTemplate("wl", "标题", "测试1");
            //if (!res.Successed)
            //{
            //    Console.WriteLine("发送成功");
            //}
            //else
            //{
            //    foreach (var err in res.Errors)
            //    {
            //        Console.WriteLine(err.Description);
            //    }
            //}

            return ResultHelper.Success(_emaillog.Add(req, userId));
        }
        [HttpPost]
        public ApiResult Edit(EmaillogEdit req)
        {
            //获取当前登录人信息
            long userId = Convert.ToInt32(HttpContext.User.Claims.ToList()[0].Value);
            return ResultHelper.Success(_emaillog.Edit(req, userId));
        }
        [HttpGet]
        public ApiResult Del(long id)
        {
            //获取当前登录人信息 
            return ResultHelper.Success(_emaillog.Del(id));
        }
        [HttpGet]
        public ApiResult BatchDel(string ids)
        {
            //获取当前登录人信息 
            return ResultHelper.Success(_emaillog.BatchDel(ids));
        }

    }
}
