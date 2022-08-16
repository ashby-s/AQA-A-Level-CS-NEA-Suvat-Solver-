using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AQA_A_Level_CS_NEA__Suvat_Solver_.Models;
using AQA_A_Level_CS_NEA__Suvat_Solver_.Data;

namespace AQA_A_Level_CS_NEA__Suvat_Solver_.Pages
{
    [BindProperties(SupportsGet = true)]
    public class Home_LoggedInModel : PageModel
    {
        public String Username { get; set; }
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
            //Fix this to a less 'roundabout' way for removing extra spaces from start
            int UserLength;
            string PlaceHoldUsername;
            string firstchar = Username.Substring(0, 1);
            string curchar;
            while (firstchar == " ")
            {
                UserLength = Username.Length;
                PlaceHoldUsername = "";
                for (int i = 1; i < UserLength; i++)
                {
                    curchar = Username.Substring(i, 1);

                    PlaceHoldUsername = PlaceHoldUsername + curchar;
                }
                Username = PlaceHoldUsername;
                firstchar = Username.Substring(0, 1);
            }
            //To Here
            User User = new User();
            var CorrectUser = _context.User.FirstOrDefault(x => x.UserName == Username);
            CorrectAnsw = CorrectUser.UserCorrectAnsw;
            TotalAnsw = CorrectUser.UserTotalAnsw;

        }

    }
}
