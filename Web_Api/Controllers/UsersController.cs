using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Result.Abstract;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IResult = Core.Utilities.Result.Abstract.IResult;

namespace Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserServices _userServices;

        public UsersController(IUserServices userServices)
        {
            _userServices = userServices;
        }
        [HttpGet("getall")]
        public ActionResult GetAll()
        {
            IDataResult<List<User>> result = _userServices.GetAll();
            return result.Success ? Ok(result) : BadRequest();
        }
        [HttpGet("getalldto")]
        public ActionResult GetAllDto()
        {
            IDataResult<List<UserDto>> result = _userServices.GetAllDto();
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpGet("getbyid")]
        public IActionResult GetByID(int id)
        {
            IDataResult<UserDto> result=_userServices.GetUserDtoById(id);
            return result.Success ? Ok(result) : BadRequest();
        }
        [HttpPost("add")]
        public IActionResult Add(User user)
        {
            IResult result = _userServices.Add(user);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("delete")]
        public IActionResult Delete([FromForm] int id)
        {
            IResult result = _userServices.DeleteByID(id);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(UserDto user)
        {
            IResult result = _userServices.UpdateByDto(user);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
