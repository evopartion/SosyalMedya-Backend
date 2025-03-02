using Core.Entities;

namespace Web_Presentation.Models
{
    public class VerificationCode 
    {
        public int UserId { get; set; }
        public string? Email { get; set; }
        public string? Code { get; set; }
    }
}
