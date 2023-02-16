using Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Dto.Client;
using Model.Dto.Contract;
using Model.Other;
using WebAPI.Config;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _client;
        public ClientController(IClientService client)
        {
            _client = client;
        }
        [HttpPost]
        public ApiResult GetClients(ClientReq req)
        {
            return ResultHelper.Success(_client.GetClient(req));
        }
        [HttpGet]
        public ApiResult GetClient(long id)
        {
            return ResultHelper.Success(_client.GetClientById(id));
        }
        [HttpPost]
        public ApiResult Add(ClientAdd req)
        {
            //获取当前登录人信息
            long userId = Convert.ToInt32(HttpContext.User.Claims.ToList()[0].Value);
            return ResultHelper.Success(_client.Add(req, userId));
        }
        [HttpPost]
        public ApiResult Edit(ClientEdit req)
        {
            //获取当前登录人信息
            long userId = Convert.ToInt32(HttpContext.User.Claims.ToList()[0].Value);
            return ResultHelper.Success(_client.Edit(req, userId));
        }
        [HttpGet]
        public ApiResult Del(long id)
        {
            //获取当前登录人信息 
            return ResultHelper.Success(_client.Del(id));
        }
        [HttpGet]
        public ApiResult BatchDel(string ids)
        {
            //获取当前登录人信息 
            return ResultHelper.Success(_client.BatchDel(ids));
        }
    }
}
