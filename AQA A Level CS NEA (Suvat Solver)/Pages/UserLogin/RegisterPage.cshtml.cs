using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AQA_A_Level_CS_NEA__Suvat_Solver_.Models;
using AQA_A_Level_CS_NEA__Suvat_Solver_.Data;

namespace AQA_A_Level_CS_NEA__Suvat_Solver_.Pages.UserLogin
{
    [BindProperties(SupportsGet = true)]
    public class RegisterPageModel : PageModel
    {

        public string TempUsername { get; set; }
        public string TempPassword { get; set; }
        public bool HasPassword { get; set; } = true;
        public bool HasUsername { get; set; } = true;
        public bool ValidUsername { get; set; } = true;
        public bool RegisterApproved { get; set; } = false;
        public bool AQAPhys { get; set; } = false;
        public bool AQAMaths { get; set; } = false;
        public bool SubjectChosen { get; set; } = true;

        private readonly ApplicationDbContext _context;
        public RegisterPageModel(ApplicationDbContext context)
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
            UsertoCourses UsertoCourses = new UsertoCourses();
            HasPassword = true;
            HasUsername = true;
            ValidUsername = true;
            SubjectChosen = true;
            UserList = _context.User.ToList();
            int UserLength;

            //Temp fix
            if(TempUsername == null)
            { UserLength = 0; 
            }
            else
            {
              UserLength = TempUsername.Length;
            }

            for (int i = 2; i < UserLength; i++)
            {
                string curchar = TempUsername.Substring(i, 1);

                if(curchar==" ")
                {
                    ValidUsername = false;
                }
            }

            if (!AQAMaths && !AQAPhys)
            {
                SubjectChosen = false;
            }
            if (string.IsNullOrWhiteSpace(TempPassword) || TempPassword.Length < 4)
            {
                HasPassword = false;
            }
            if (string.IsNullOrWhiteSpace(TempUsername) || TempUsername.Length < 4)
            {
                HasUsername = false;
            }
            if (UserList.Any(x => x.UserName == TempUsername))
            {
                ValidUsername = false;
            }
            if (!HasPassword || !HasUsername || !ValidUsername || !SubjectChosen)
            {
                return RedirectToPage("/UserLogin/RegisterPage", new { HasPassword, HasUsername, ValidUsername, SubjectChosen });
            }
            else
            {
                RegisterApproved = true;
                User.UserName = TempUsername;
                User.UserPass = TempPassword;
                User.UserCorrectAnsw = 0;
                User.UserTotalAnsw = 0;
                _context.User.Add(User);
                _context.SaveChanges();

                if (AQAMaths)
                {
                    var CorrectUser = _context.User.FirstOrDefault(x => x.UserName == TempUsername);
                    UsertoCourses.UserId = CorrectUser.UserId;
                    UsertoCourses.CourseId = 1;
                    _context.UsertoCourses.Add(UsertoCourses);
                    _context.SaveChanges();

                }
                if (AQAPhys)
                {
                    var CorrectUser = _context.User.FirstOrDefault(x => x.UserName == TempUsername);
                    UsertoCourses.UserId = CorrectUser.UserId;
                    UsertoCourses.CourseId = 2;
                    _context.UsertoCourses.Add(UsertoCourses);
                    _context.SaveChanges();
                }

                return RedirectToPage("/UserLogin/LoginPage", new { RegisterApproved });
            }
        }
    }
}
