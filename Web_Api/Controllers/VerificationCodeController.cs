using Business.Abstract;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VerificationCodeController : ControllerBase
    {
        private readonly IVerificationCodeService _verificationCodeService;

        public VerificationCodeController(IVerificationCodeService verificationCodeService)
        {
            _verificationCodeService = verificationCodeService;
        }

        [HttpPost("sendcode")]
        public IActionResult SendVerifyCode(VerificationCodeDto verificationCode)
        {
            var result = _verificationCodeService.SendVerifyCode(verificationCode);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("deletecode")]
        public IActionResult DeleteVerifyCode(int userId)
        {
            var result = _verificationCodeService.DeleteVerifyCode(userId);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("checkcode")]
        public IActionResult CheckVerifyCode(VerificationCodeDto verificationCode)
        {
            var result = _verificationCodeService.CheckVerifyCode(verificationCode);
            return result.Success ? Ok(result) : BadRequest(result);
        }

    }
}
