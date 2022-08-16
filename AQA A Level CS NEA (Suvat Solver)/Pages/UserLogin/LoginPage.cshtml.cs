using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AQA_A_Level_CS_NEA__Suvat_Solver_.Models;
using AQA_A_Level_CS_NEA__Suvat_Solver_.Data;

namespace AQA_A_Level_CS_NEA__Suvat_Solver_.Pages.UserLogin
{
    [BindProperties(SupportsGet = true)]
    public class LoginPageModel : PageModel
    {


        public new TempUserLoginModel TempUser { get; set; }
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
        public List<User> UserList = new List<User>();

        public void OnGet()
        {

        }
        public IActionResult OnPost()
        {
            User User = new User();
            HasPassword = true;
            HasUsername = true;
            IncorUsername = false;
            UserList = _context.User.ToList();

            if (string.IsNullOrWhiteSpace(TempUser.Password))
            {
                HasPassword = false;
            }
            if (string.IsNullOrWhiteSpace(TempUser.Username))
            {
                HasUsername = false;
            }

            var foundUser = _context.User.FirstOrDefault(x => x.UserName == TempUser.Username && x.UserPass == TempUser.Password);

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
                return RedirectToPage("/Home-LoggedIn", new { TempUser.Username });
            };
        }
    }
}
