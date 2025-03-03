using Core.Utilities.Result.Abstract;
using Entities.DTOs;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
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
        IResult SendCodeForgotPassword(ResetPassword resetPassword);
        IResult CheckVerifyCode(VerificationCodeDto verificationCode);
        IResult CheckCodeForgotPassword(ResetPassword resetPassword);
        IResult DeleteVerifyCode(int userId);
    }
}
