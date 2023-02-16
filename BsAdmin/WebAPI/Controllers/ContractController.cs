using Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Dto.Contract;
using Model.Dto.Position;
using Model.Other;
using WebAPI.Config;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class ContractController : ControllerBase
    {
        private readonly IContractService _contract;
        public ContractController(IContractService contract)
        {
            _contract = contract;
        }
        [HttpPost]
        public ApiResult GetContracts(ContractReq req)
        {
            return ResultHelper.Success(_contract.GetContract(req));
        }
        [HttpGet]
        public ApiResult GetContract(long id)
        {
            return ResultHelper.Success(_contract.GetContractById(id));
        }
        [HttpPost]
        public ApiResult Add(ContractAdd req)
        {
            //获取当前登录人信息
            long userId = Convert.ToInt32(HttpContext.User.Claims.ToList()[0].Value);
            return ResultHelper.Success(_contract.Add(req, userId));
        }
        [HttpPost]
        public ApiResult Edit(ContractEdit req)
        {
            //获取当前登录人信息
            long userId = Convert.ToInt32(HttpContext.User.Claims.ToList()[0].Value);
            return ResultHelper.Success(_contract.Edit(req, userId));
        }
        [HttpGet]
        public ApiResult Del(long id)
        {
            //获取当前登录人信息 
            return ResultHelper.Success(_contract.Del(id));
        }
        [HttpGet]
        public ApiResult BatchDel(string ids)
        {
            //获取当前登录人信息 
            return ResultHelper.Success(_contract.BatchDel(ids));
        }
    }
}
