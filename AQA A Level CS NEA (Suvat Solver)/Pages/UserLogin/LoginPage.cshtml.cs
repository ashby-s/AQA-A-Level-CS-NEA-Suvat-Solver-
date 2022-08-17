using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AQA_A_Level_CS_NEA__Suvat_Solver_.Models;
using AQA_A_Level_CS_NEA__Suvat_Solver_.Data;

namespace AQA_A_Level_CS_NEA__Suvat_Solver_.Pages.UserLogin
{
    [BindProperties(SupportsGet = true)]
    public class LoginPageModel : PageModel
    {
        public string TempUsername { get; set; }
        public string TempPassword { get; set; }
        public bool HasPassword { get; set; } = true;
        public bool HasUsername { get; set; } = true;
        public bool IncorUsername { get; set; } = false;
        public bool LoginApproved { get; set; }

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
            IncorUsername = false;

            if (string.IsNullOrWhiteSpace(TempPassword))
            {
                HasPassword = false;
            }
            if (string.IsNullOrWhiteSpace(TempUsername))
            {
                HasUsername = false;
            }

            var foundUser = _context.User.FirstOrDefault(x => x.UserName == TempUsername && x.UserPass == TempPassword);

            if (foundUser != null)
            {
                LoginApproved = true;
            }

            //if (foundUser != null && foundUser.UserPass == TempUser.Password)
            //{
            //    LoginApproved = true;
            //}

            else
            {
                IncorUsername = true;
                HasPassword = false;
            }
            if (!HasPassword || !HasUsername || foundUser == null)
            {
                return RedirectToPage("/UserLogin/LoginPage", new { HasPassword, HasUsername, IncorUsername });
            }
            else
            {
                return RedirectToPage("/Home-LoggedIn", new { foundUser.UserId });
            };
        }
    }
}
