using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AQA_A_Level_CS_NEA__Suvat_Solver_.Models;
using AQA_A_Level_CS_NEA__Suvat_Solver_.Data;

namespace AQA_A_Level_CS_NEA__Suvat_Solver_.Pages.RevisionQuestions
{
    [BindProperties(SupportsGet = true)]
    public class RevisionQuestionsModel : PageModel
    {
        public String Username { get; set; }
        public int CorrectAnsw { get; set; }
        public int TotalAnsw { get; set; }

        private readonly ApplicationDbContext _context;
        public RevisionQuestionsModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
            Username = Username.Remove(0, 1);
            User User = new User();
            var CorrectUser = _context.User.FirstOrDefault(x => x.UserName == Username);
            CorrectAnsw = CorrectUser.UserCorrectAnsw;
            TotalAnsw = CorrectUser.UserTotalAnsw;
        }
    }
}
