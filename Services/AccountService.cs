using MyTasks01.Helper;
using MyTasks01.Models;

namespace MyTasks01.Services
{
    public class AccountService
    {
        //private readonly List> _users = new();
        private readonly EmailService _emailService;
        private readonly IConfiguration _configuration;
        private readonly MyTasksDBContext _context;

        public AccountService(EmailService emailService, IConfiguration configuration, MyTasksDBContext context)
        {
            _emailService = emailService;
            _configuration = configuration;
            _context = context;
        }
        public async Task<Models.User> Registration(Models.User user, string password)
        {
            ValidateRegisteration(user);
            user.UserHashedPassword = BCrypt.Net.BCrypt.HashPassword(password, workFactor: 12);
            // Set default values
            user.PasswordResetToken = null;
            user.ResetTokenExpires = null;

            _context.User.Add(user);
            _context.SaveChanges();
            return await Task.FromResult(user);
        }

        public  void ValidateRegisteration(Models.User user)
        {
            if (string.IsNullOrWhiteSpace(user.UserName))
                throw new InvalidDataException("user name is required");
            if (string.IsNullOrWhiteSpace(user.UserEmail))
                throw new InvalidDataException("email is required");
            if (string.IsNullOrWhiteSpace(user.UserPassword))
                throw new InvalidDataException("password is required");
            if (string.IsNullOrWhiteSpace(user.UserConfirmPassword))
                throw new InvalidDataException("confirm the password");
            if (user.UserPassword.Length < 6 )
                throw new InvalidDataException("password must be atleast 6 chars");
            if (_context.User.Any(u => u.UserEmail.ToLower() == user.UserEmail.ToLower()))
                throw new InvalidDataException("Email already exists");
            if (user.UserPassword != user.UserConfirmPassword)
                throw new InvalidDataException("Confirm password must match password");
        }
        public async Task<Models.User> Login(string email, string password)
        {
            ValidateLogin(email, password);
            var LoggedUser = _context?.User.FirstOrDefault(user => user.UserEmail.ToLower() == email.ToLower());


            if (LoggedUser is null || !BCrypt.Net.BCrypt.Verify(password, LoggedUser.UserHashedPassword))
            {
                throw new InvalidDataException("email or password is incorrect");
            }
            return await Task.FromResult(LoggedUser);
        }
        public void ValidateLogin(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new InvalidDataException("email filed is required");
            if (string.IsNullOrWhiteSpace(password))
                throw new InvalidDataException("pass is required");

        }


        public async Task ForgetPassword(string email)
        {
            ValidateForgetPassword(email);

            var user = _context?.User.FirstOrDefault(user => user.UserEmail.ToLower() == email.ToLower());

            if (user != null)
            {
                user.PasswordResetToken = Guid.NewGuid().ToString();

                user.ResetTokenExpires = DateTime.UtcNow.AddMinutes(30);
                var resetLink = $"{GetApplicationBaseUrl()}/resetpassword?token={Uri.EscapeDataString(user.PasswordResetToken)}&email={Uri.EscapeDataString(user.UserEmail)}";
                var subject = "Password Reset Request";
                var body = $"""
                            <p>Click the link below to reset your password:</p>
                            <p><a href="{resetLink}">{resetLink}</a></p>
                            <p>This link will expire in 30 minutes.</p>
                            """;

                await _emailService.SendEmailService(email, subject, body);
            }
            else
            {
                Console.WriteLine("stmp failed");
            }
            await _context.SaveChangesAsync();
        }

        public void ValidateForgetPassword(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new InvalidDataException("Email is required");
            if (!_context.User.Any(u => u.UserEmail == email))
                throw new InvalidDataException("this email dont exist");
        }

        private string GetApplicationBaseUrl()
        {
            return _configuration["AppBaseUrl"] ?? "https://localhost:7233";
        }
        public async Task<bool> ResetPassword(string token, string newpassword, string confirmnewpassword)
        {
            ValidateResetPassword(newpassword, confirmnewpassword);

            var user = _context.User.FirstOrDefault(user => user.PasswordResetToken == token
            && user.ResetTokenExpires >= DateTime.UtcNow);

            if (token == null)
            {
                throw new Exception("token is null");
            }
            if (user == null)
            {
                throw new Exception("user is null");

            }

            if (string.IsNullOrWhiteSpace(newpassword))
            {
                throw new ArgumentException("New password cannot be empty", nameof(newpassword));
            }

            if (newpassword != confirmnewpassword)
            {
                throw new Exception("Passwords do not match.");
            }


            user.UserHashedPassword = BCrypt.Net.BCrypt.HashPassword(newpassword, workFactor : 12);
            user.UserPassword = newpassword;
            user.ResetTokenExpires = null;
            user.PasswordResetToken = null;
            user.UserConfirmPassword = newpassword;

            await _context.SaveChangesAsync();
            return await Task.FromResult(true);
        }

        public void ValidateResetPassword(string newpassword, string confirmnewpassword)
        {
            if (string.IsNullOrWhiteSpace(newpassword))
                throw new InvalidDataException("enter new password");
            if (string.IsNullOrWhiteSpace(confirmnewpassword))
                throw new InvalidDataException("confirm new password");
            if (newpassword != confirmnewpassword)
                throw new InvalidDataException("two passwords must match ");
        }
    }
}
