﻿namespace Web_Presentation.Models
{
    public class ChangePassword
    {
        public string Email { get; set; }
        public string? OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
