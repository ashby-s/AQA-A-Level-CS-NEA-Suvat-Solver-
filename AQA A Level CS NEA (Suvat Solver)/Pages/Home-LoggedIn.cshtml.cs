using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AQA_A_Level_CS_NEA__Suvat_Solver_.Models;
using AQA_A_Level_CS_NEA__Suvat_Solver_.Data;

namespace AQA_A_Level_CS_NEA__Suvat_Solver_.Pages
{
    [BindProperties(SupportsGet = true)]
    public class Home_LoggedInModel : PageModel
    {
        public int UserId { get; set; }
        public string TempUsername { get; set; }
        //Username will already be assigned by passing a value using a form from a previous page
        public int CorrectAnsw { get; set; }
        public int TotalAnsw { get; set; }

        private readonly ApplicationDbContext _context;
        public Home_LoggedInModel(ApplicationDbContext context)
        {
            _context = context;
        }

        
        public void OnGet()
        {
            //User user = new User();
            var CorrectUser = _context.User.FirstOrDefault(x => x.UserId == UserId);
            TempUsername = CorrectUser.UserName;
            CorrectAnsw = CorrectUser.UserCorrectAnsw;
            TotalAnsw = CorrectUser.UserTotalAnsw;

        }

    }
}
