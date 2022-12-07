using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AQA_A_Level_CS_NEA__Suvat_Solver_.Models;


namespace AQA_A_Level_CS_NEA__Suvat_Solver_.Pages.QuestionSolver
{
    [BindProperties(SupportsGet = true)]
    public class QuestionSolverModel : PageModel
    {
        
        public int UserId { get; set; }
        public SUVATValuesSelect SUVATValuesSelect { get; set; }
        public bool CorrectNumVal { get; set; } = true;


        public void OnGet()
        {
        }
        
        public IActionResult OnPost()
        {
            //checks how many values have been selected
            int totalvals = 0;
            if (SUVATValuesSelect.SValSelect) totalvals++;
            if (SUVATValuesSelect.UValSelect) totalvals++;
            if (SUVATValuesSelect.VValSelect) totalvals++;
            if (SUVATValuesSelect.AValSelect) totalvals++;
            if (SUVATValuesSelect.TValSelect) totalvals++;

            //checks if not three options where ticked
            if(totalvals != 3)
            {
                //reloads page with warning to only choose 3 options
                CorrectNumVal = false;
                return RedirectToPage("/QuestionSolver/ValsSelect", new {CorrectNumVal, UserId});
            }
            else
            {
                //goes to enterrecievevals page, with whether values have been chosen or not
                return RedirectToPage("EnterRecieveVals", new { SUVATValuesSelect.SValSelect, SUVATValuesSelect.UValSelect,
                    SUVATValuesSelect.VValSelect, SUVATValuesSelect.AValSelect, SUVATValuesSelect.TValSelect, UserId});
            }

        }

    }
}
