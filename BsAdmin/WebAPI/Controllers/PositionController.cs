using Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Dto.Department;
using Model.Dto.Position;
using Model.Other;
using WebAPI.Config;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class PositionController : ControllerBase
    {
        private readonly IPositionService _position;
        public PositionController(IPositionService position)
        {
            _position = position;
        }
        [HttpPost]
        public ApiResult GetPositions(PositionReq req)
        {
            return ResultHelper.Success(_position.GetPosition(req));
        }
        [HttpGet]
        public ApiResult GetPosition(long id)
        {
            return ResultHelper.Success(_position.GetPositionById(id));
        }
        [HttpPost]
        public ApiResult Add(PositionAdd req)
        {
            //获取当前登录人信息
            long userId = Convert.ToInt32(HttpContext.User.Claims.ToList()[0].Value);
            return ResultHelper.Success(_position.Add(req, userId));
        }
        [HttpPost]
        public ApiResult Edit(PositionEdit req)
        {
            //获取当前登录人信息
            long userId = Convert.ToInt32(HttpContext.User.Claims.ToList()[0].Value);
            return ResultHelper.Success(_position.Edit(req, userId));
        }
        [HttpGet]
        public ApiResult Del(long id)
        {
            //获取当前登录人信息 
            return ResultHelper.Success(_position.Del(id));
        }
        [HttpGet]
        public ApiResult BatchDel(string ids)
        {
            //获取当前登录人信息 
            return ResultHelper.Success(_position.BatchDel(ids));
        }

    }
}
