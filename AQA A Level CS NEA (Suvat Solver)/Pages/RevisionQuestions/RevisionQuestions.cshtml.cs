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
        public float Answ1 { get; set; } = 7;
        public float Answ2 { get; set; }
        public float Answ3 { get; set; }
        public float Answ4 { get; set; }
        public float Answ5 { get; set; }
        public float Answ6 { get; set; }
        public float QuestCorrectAnsw { get; set; }
        //public int CorrectAnsw { get; set; }
        //public int TotalAnsw { get; set; }
        public bool AnswValid { get; set; }
        public bool nextquestion { get; set; } = true;
        public float BtnAnswer { get; set; }
        public string Unit { get; set; }
        //To show the solutions:
        public int TempQuestId { get; set; }
        public string TempQuestSolved { get; set; }

        private readonly ApplicationDbContext _context;
        public RevisionQuestionsModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
            User User = new User();
            var CorrectUser = _context.User.FirstOrDefault(x => x.UserName == Username);
            //CorrectAnsw = CorrectUser.UserCorrectAnsw;
            //TotalAnsw = CorrectUser.UserTotalAnsw;
            if (nextquestion)
            {
                
            }
            else
            {
                if(QuestCorrectAnsw == BtnAnswer)
                {
                    AnswValid = true;
                }
            }
        }
        public IActionResult OnPost()
        {
            nextquestion = true;
            return RedirectToPage("/RevisionQuestions/RevisionQuestions", new
            {
                nextquestion,
                Username
            });
        }

    }
}
