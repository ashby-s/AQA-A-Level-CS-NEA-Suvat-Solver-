namespace AQA_A_Level_CS_NEA__Suvat_Solver_.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; } = string.Empty;

        public List<UsertoCourses> UsertoCourses { get; set; }
        public List<QuestiontoCourses> QuestiontoCourses { get; set; }
    }
}
