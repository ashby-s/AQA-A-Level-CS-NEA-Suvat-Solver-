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
        public bool enteredvals { get; set; } = false;
        public bool correctvals { get; set; } = true;
        public SUVATMethods SUVATMethods { get; set; }
        public bool quadraticcalc { get; set; } = true;
        public float SecondTVal { get; set; }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            float S = SUVATValues.SVal;
            float U = SUVATValues.UVal;
            float V = SUVATValues.VVal;
            float A = SUVATValues.AVal;
            float T = SUVATValues.TVal;
            correctvals = true;
            quadraticcalc = false;
            if ((S == 0 && SUVATValuesSelect.SValSelect == true) || (T == 0 && SUVATValuesSelect.TValSelect == true)
                || ((V==0 && SUVATValuesSelect.VValSelect == true) && (U==0 && SUVATValuesSelect.UValSelect == true)))
            {
                correctvals = false;
                return RedirectToPage("/QuestionSolver/EnterRecieveVals", new
                {
                    SUVATValuesSelect.SValSelect,
                    SUVATValuesSelect.UValSelect,
                    SUVATValuesSelect.VValSelect,
                    SUVATValuesSelect.AValSelect,
                    SUVATValuesSelect.TValSelect,
                    enteredvals,
                    correctvals,
                    UserId
                });
            }
            enteredvals = true;
            if (SUVATValuesSelect.SValSelect && SUVATValuesSelect.UValSelect && SUVATValuesSelect.VValSelect)
            {
                SUVATValues.AVal = ((V * V) - (U * U)) / (2 * S);
                //A = SUVATValues.AVal;
                //SUVATMethods.AMethod = "Rearrange v² = u² +2as to a = (v² - u²) / 2s. Input the values to give" +
                //    " (" + V + "² - " + U + "²) / 2*" + S + ". This equals to " + A + " m/s².";
                SUVATValues.TVal = (2 * S) / (U + V);
            }
            else if (SUVATValuesSelect.SValSelect && SUVATValuesSelect.UValSelect && SUVATValuesSelect.AValSelect)
            {
                SUVATValues.VVal = (float)(Math.Sqrt((U * U) + (2 * A * S)));
                quadraticcalc = true;
                SUVATValues.TVal = (float)((- U + Math.Sqrt((U * U) + (2 * A * S))) / A);
                SecondTVal = (float)((- U - Math.Sqrt((U * U) + (2 * A * S))) / A);
            }
            else if (SUVATValuesSelect.SValSelect && SUVATValuesSelect.UValSelect && SUVATValuesSelect.TValSelect)
            {
                SUVATValues.VVal = ((2 * S) / T) - U;
                SUVATValues.AVal = (float)((S-(U*T))/(0.5*T*T));
            }
            else if (SUVATValuesSelect.SValSelect && SUVATValuesSelect.VValSelect && SUVATValuesSelect.AValSelect)
            {
                SUVATValues.UVal = (float)(Math.Sqrt((V * V)-(2 * A * S)));
                quadraticcalc=true;
                SUVATValues.TVal = (float)((- V + Math.Sqrt((V * V) - (2 * A * S))) / (0 - A));
                SecondTVal = (float)((- V - Math.Sqrt((V * V) - (2 * A * S))) / (0 - A));
            }
            else if (SUVATValuesSelect.SValSelect && SUVATValuesSelect.VValSelect && SUVATValuesSelect.TValSelect)
            {
                SUVATValues.UVal = ((2 * S) / T) - V;
                SUVATValues.AVal = (float)(((V * T)-S) / (0.5 * T * T));
            }
            else if (SUVATValuesSelect.SValSelect && SUVATValuesSelect.AValSelect && SUVATValuesSelect.TValSelect)
            {
                SUVATValues.VVal = (float)((S + (0.5 * A * (T * T))) / (T));
                SUVATValues.UVal = (float)((S - (0.5 * A * (T * T))) / (T));
            }
            else if (SUVATValuesSelect.UValSelect && SUVATValuesSelect.VValSelect && SUVATValuesSelect.AValSelect)
            {
                SUVATValues.SVal = (((V * V) - (U * U)) / (2 * A));
                SUVATValues.TVal = (V-U)/A;
            }
            else if (SUVATValuesSelect.UValSelect && SUVATValuesSelect.VValSelect && SUVATValuesSelect.TValSelect)
            {
                SUVATValues.SVal = (T / 2) * (U + V);
                SUVATValues.AVal = ((V-U)/T);
            }
            else if (SUVATValuesSelect.UValSelect && SUVATValuesSelect.AValSelect && SUVATValuesSelect.TValSelect)
            {
                SUVATValues.SVal = (float)((U * T) + (0.5 * (A * T * T)));
                SUVATValues.VVal = (U+A*T);
            }
            else if (SUVATValuesSelect.VValSelect && SUVATValuesSelect.AValSelect && SUVATValuesSelect.TValSelect)
            {
                SUVATValues.SVal = (float)((V * T) - (0.5 * (A * T * T)));
                SUVATValues.UVal = (V-(A*T));
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
                quadraticcalc,
                enteredvals,
                UserId
            });
        }
    }
}
