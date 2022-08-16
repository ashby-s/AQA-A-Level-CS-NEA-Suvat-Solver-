namespace AQA_A_Level_CS_NEA__Suvat_Solver_.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string UserPass { get; set; } = string.Empty;
        public int UserCorrectAnsw { get; set; } = 0;
        public int UserTotalAnsw { get; set; } = 0;

        public List<UsertoCourses> UsertoCourses { get; set; }


    }
}
