using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AQA_A_Level_CS_NEA__Suvat_Solver_.Models;

namespace AQA_A_Level_CS_NEA__Suvat_Solver_.Pages.QuestionSolver
{
    [BindProperties(SupportsGet = true)]
    public class EnterRecieveValsModel : PageModel
    {
        public int UserId { get; set; }
        public SUVATValuesSelect SUVATValuesSelect { get; set; }
        public SUVATValues SUVATValues { get; set; }
        public bool EnteredVals { get; set; } = false;
        public bool CorrectVals { get; set; } = true;
        public SUVATMethods SUVATMethods { get; set; }
        public bool QuadraticCalc { get; set; } = true;
        public float SecondTVal { get; set; }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            float SVal = SUVATValues.SVal;
            float UVal = SUVATValues.UVal;
            float VVal = SUVATValues.VVal;
            float AVal = SUVATValues.AVal;
            float TVal = SUVATValues.TVal;
            CorrectVals = true;
            QuadraticCalc = false;
            if ((SVal == 0 && SUVATValuesSelect.SValSelect == true) || (TVal == 0 && SUVATValuesSelect.TValSelect == true)
                || ((VVal == 0 && SUVATValuesSelect.VValSelect == true) && (UVal == 0 && SUVATValuesSelect.UValSelect == true)))
            {
                CorrectVals = false;
                return RedirectToPage("/QuestionSolver/EnterRecieveVals", new
                {
                    SUVATValuesSelect.SValSelect,
                    SUVATValuesSelect.UValSelect,
                    SUVATValuesSelect.VValSelect,
                    SUVATValuesSelect.AValSelect,
                    SUVATValuesSelect.TValSelect,
                    EnteredVals,
                    CorrectVals,
                    UserId
                });
            }
            EnteredVals = true;
            if (SUVATValuesSelect.SValSelect && SUVATValuesSelect.UValSelect && SUVATValuesSelect.VValSelect)
            {
                SUVATValues.AVal = (float)Math.Round(((VVal * VVal) - (UVal * UVal)) / (2 * SVal), 3);
                //AVal = SUVATValues.AVal;
                //SUVATMethods.AMethod = "Rearrange v² = u² +2as to a = (v² - u²) / 2s. Input the values to give" +
                //    " (" + VVal + "² - " + UVal + "²) / 2*" + SVal + ". This equals to " + AVal + " m/s².";
                SUVATValues.TVal = (float)Math.Round((2 * SVal) / (UVal + VVal), 3);
            }
            else if (SUVATValuesSelect.SValSelect && SUVATValuesSelect.UValSelect && SUVATValuesSelect.AValSelect)
            {
                SUVATValues.VVal = (float)Math.Round((Math.Sqrt((UVal * UVal) + (2 * AVal * SVal))), 3);
                QuadraticCalc = true;
                SUVATValues.TVal = (float)((-UVal + Math.Sqrt((UVal * UVal) + (2 * AVal * SVal))) / AVal);
                SecondTVal = (float)Math.Round(((-UVal - Math.Sqrt((UVal * UVal) + (2 * AVal * SVal))) / AVal), 3);
            }
            else if (SUVATValuesSelect.SValSelect && SUVATValuesSelect.UValSelect && SUVATValuesSelect.TValSelect)
            {
                SUVATValues.VVal = (float)Math.Round(((2 * SVal) / TVal) - UVal, 3);
                SUVATValues.AVal = (float)Math.Round(((SVal - (UVal * TVal)) / (0.5 * TVal * TVal)), 3);
            }
            else if (SUVATValuesSelect.SValSelect && SUVATValuesSelect.VValSelect && SUVATValuesSelect.AValSelect)
            {
                SUVATValues.UVal = (float)Math.Round((Math.Sqrt((VVal * VVal) - (2 * AVal * SVal))), 3);
                QuadraticCalc = true;
                SUVATValues.TVal = (float)Math.Round(((-VVal + Math.Sqrt((VVal * VVal) - (2 * AVal * SVal))) / (0 - AVal)), 3);
                SecondTVal = (float)Math.Round(((-VVal - Math.Sqrt((VVal * VVal) - (2 * AVal * SVal))) / (0 - AVal)), 3);
            }
            else if (SUVATValuesSelect.SValSelect && SUVATValuesSelect.VValSelect && SUVATValuesSelect.TValSelect)
            {
                SUVATValues.UVal = (float)Math.Round(((2 * SVal) / TVal) - VVal, 3);
                SUVATValues.AVal = (float)Math.Round((((VVal * TVal) - SVal) / (0.5 * TVal * TVal)), 3);
            }
            else if (SUVATValuesSelect.SValSelect && SUVATValuesSelect.AValSelect && SUVATValuesSelect.TValSelect)
            {
                SUVATValues.VVal = (float)Math.Round(((SVal + (0.5 * AVal * (TVal * TVal))) / (TVal)), 3);
                SUVATValues.UVal = (float)Math.Round(((SVal - (0.5 * AVal * (TVal * TVal))) / (TVal)), 3);
            }
            else if (SUVATValuesSelect.UValSelect && SUVATValuesSelect.VValSelect && SUVATValuesSelect.AValSelect)
            {
                SUVATValues.SVal = (float)Math.Round((((VVal * VVal) - (UVal * UVal)) / (2 * AVal)), 3);
                SUVATValues.TVal = (float)Math.Round((VVal - UVal) / AVal, 3);
            }
            else if (SUVATValuesSelect.UValSelect && SUVATValuesSelect.VValSelect && SUVATValuesSelect.TValSelect)
            {
                SUVATValues.SVal = (float)Math.Round((TVal / 2) * (UVal + VVal), 3);
                SUVATValues.AVal = (float)Math.Round(((VVal - UVal) / TVal), 3);
            }
            else if (SUVATValuesSelect.UValSelect && SUVATValuesSelect.AValSelect && SUVATValuesSelect.TValSelect)
            {
                //incorrect decimal places (Aim for 3 dp)
                SUVATValues.SVal = (float)Math.Round(((UVal * TVal) + (0.5 * (AVal * TVal * TVal))), 3);
                SUVATValues.VVal = (float)Math.Round((UVal + AVal * TVal), 3);
            }
            else if (SUVATValuesSelect.VValSelect && SUVATValuesSelect.AValSelect && SUVATValuesSelect.TValSelect)
            {
                SUVATValues.SVal = (float)Math.Round(((VVal * TVal) - (0.5 * (AVal * TVal * TVal))), 3);
                SUVATValues.UVal = (float)Math.Round((VVal - (AVal * TVal)), 3);
            }


            return RedirectToPage("/QuestionSolver/EnterRecieveVals", new
            {
                SUVATValuesSelect.SValSelect,
                SUVATValuesSelect.UValSelect,
                SUVATValuesSelect.VValSelect,
                SUVATValuesSelect.AValSelect,
                SUVATValuesSelect.TValSelect,
                SUVATValues.SVal,
                SUVATValues.UVal,
                SUVATValues.VVal,
                SUVATValues.AVal,
                SUVATValues.TVal,
                SecondTVal,
                //SUVATMethods.SMethod,
                //SUVATMethods.UMethod,
                //SUVATMethods.VMethod,
                //SUVATMethods.AMethod,
                //SUVATMethods.TMethod,
                QuadraticCalc,
                EnteredVals,
                UserId
            });
        }
    }
}
