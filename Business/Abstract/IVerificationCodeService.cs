using Core.Utilities.Result.Abstract;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IVerificationCodeService
    {
        IResult SendVerifyCode(VerificationCodeDto verificationCode);
        IResult CheckVerifyCode(VerificationCodeDto verificationCode);
        IResult DeleteVerifyCode(int userId);
    }
}
