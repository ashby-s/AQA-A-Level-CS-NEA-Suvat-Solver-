namespace AQA_A_Level_CS_NEA__Suvat_Solver_.Models
{
    public class Question
    {
        public int QuestionId { get; set; }

        public string Difficulty { get; set; }
        public char AnswType { get; set; }
        public float AnswTrue { get; set; }
        public string QuestDetails { get; set; }
        public string QuestSolved { get; set; }

        public List<QuestiontoCourses> QuestiontoCourses { get; set; }

    }
}
