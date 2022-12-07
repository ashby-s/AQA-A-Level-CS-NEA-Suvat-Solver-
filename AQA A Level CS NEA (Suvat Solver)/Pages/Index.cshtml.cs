using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AQA_A_Level_CS_NEA__Suvat_Solver_.Pages
{
    public class IndexModel : PageModel
    {
        //Is created and displayed when application is opened
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
    }
}