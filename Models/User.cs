namespace MyTasks01.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string UserConfirmPassword { get; set; }
        public string UserHashedPassword { get; set; }
        public string? UserToken { get; set; }
        public string UserEmail { get; set; }
        public string? PasswordResetToken { get; set; }
        public DateTime? ResetTokenExpires { get; set; }


    }
}
