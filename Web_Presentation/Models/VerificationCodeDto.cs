using Core.Entities;

namespace Web_Presentation.Models
{
    public class VerificationCodeDto : IDto
    {
        public int UserId { get; set; }
        public string? Email { get; set; }
        public string? Code { get; set; }
    }
}
