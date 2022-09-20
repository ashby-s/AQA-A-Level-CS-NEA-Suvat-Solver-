using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AQA_A_Level_CS_NEA__Suvat_Solver_.Models;
using AQA_A_Level_CS_NEA__Suvat_Solver_.Data;
using System.Security.Cryptography;
using System.Text;

namespace AQA_A_Level_CS_NEA__Suvat_Solver_.Pages.UserLogin
{
    [BindProperties(SupportsGet = true)]
    public class LoginPageModel : PageModel
    {
        public string TempUsername { get; set; }
        public string TempPassword { get; set; }
        public bool HasPassword { get; set; } = true;
        public bool HasUsername { get; set; } = true;
        public bool ValidationFailed { get; set; }

        public bool RegisterApproved { get; set; }

        private readonly ApplicationDbContext _context;
        public LoginPageModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {

        }
        public IActionResult OnPost()
        {
            HasPassword = true;
            HasUsername = true;
            ValidationFailed = false;

            if (string.IsNullOrWhiteSpace(TempUsername))
            {
                HasUsername = false;
            }
            if (string.IsNullOrWhiteSpace(TempPassword))
            {
                HasPassword = false;
            }

            string HashedPassword;
            HashedPassword = HashPassword(TempPassword, TempUsername);

            var foundUser = _context.User.FirstOrDefault(x => x.UserName == TempUsername && x.UserPass == HashedPassword);

            if (foundUser == null)
            {
                ValidationFailed = true;
            }
            if (!HasPassword || !HasUsername || foundUser == null)
            {
                return RedirectToPage("/UserLogin/LoginPage", new { HasPassword, HasUsername, ValidationFailed });
            }
            else
            {
                return RedirectToPage("/Home-LoggedIn", new { foundUser.UserId });
            };
        }
        public string HashPassword(string Password, string Username)
        {
            using var sha = SHA256.Create();
            var AsBytes = Encoding.Default.GetBytes(Password + Username);
            var Hashed = sha.ComputeHash(AsBytes);
            return Convert.ToBase64String(Hashed);
        }
    }
}
