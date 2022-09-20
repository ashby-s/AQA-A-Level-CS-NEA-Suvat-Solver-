using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AQA_A_Level_CS_NEA__Suvat_Solver_.Models;
using AQA_A_Level_CS_NEA__Suvat_Solver_.Data;

namespace AQA_A_Level_CS_NEA__Suvat_Solver_.Pages.RevisionQuestions
{
    [BindProperties(SupportsGet = true)]
    public class RevisionQuestionsModel : PageModel
    {
        public int UserId { get; set; }
        public float Answ1 { get; set; }
        public float Answ2 { get; set; }
        public float Answ3 { get; set; }
        public float Answ4 { get; set; }
        public float Answ5 { get; set; }
        public float Answ6 { get; set; }
        public float QuestCorrectAnsw { get; set; }
        public bool AnswValid { get; set; }
        public bool NextQuestion { get; set; } = true;
        public float BtnAnswer { get; set; }
        public string Unit { get; set; }
        public string TempQuestDetails { get; set; }
        public string TempQuestDiff { get; set; }
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
            User user = new User();

            var CorrectUser = _context.User.FirstOrDefault(x => x.UserId == UserId);

            //CorrectAnsw = CorrectUser.UserCorrectAnsw;
            //TotalAnsw = CorrectUser.UserTotalAnsw;

            if (NextQuestion)
            {
                List<UsertoCourses> UsertoCoursesList = new List<UsertoCourses>();
                UsertoCoursesList = _context.UsertoCourses.ToList();
                List<Question> QuestionList = new List<Question>();
                QuestionList = _context.Question.ToList();
                List<QuestiontoCourses> QuestiontoCoursesList = new List<QuestiontoCourses>();
                QuestiontoCoursesList = _context.QuestiontoCourses.ToList();
                

                Random rnd = new Random();
                bool AQAMaths = false;
                bool AQAPhys = false;
                bool ValidQuest = false;

                if (UsertoCoursesList.Any(x => x.UserId == UserId && x.CourseId == 1))
                {
                    AQAMaths = true;
                }
                if (UsertoCoursesList.Any(x => x.UserId == UserId && x.CourseId == 2))
                {
                    AQAPhys = true;
                }
                while (!ValidQuest)
                {
                    //ADD QUESTION BEFOREHAND

                    TempQuestId = rnd.Next(1,QuestionList.Count+1);

                    if (QuestiontoCoursesList.Any(x => x.QuestionId == TempQuestId && x.CourseId == 1) && AQAMaths)
                    {
                        ValidQuest = true;
                    }
                    else if (QuestiontoCoursesList.Any(x => x.QuestionId == TempQuestId && x.CourseId == 2) && AQAPhys)
                    {
                        ValidQuest = true;
                    }
                }
                var QuestValues = _context.Question.FirstOrDefault(x => x.QuestionId == TempQuestId);
                QuestCorrectAnsw = QuestValues.AnswTrue;
                TempQuestDiff = QuestValues.Difficulty;
                TempQuestDetails = QuestValues.QuestDetails;
                if (QuestValues.AnswType == 'S')
                {
                    Unit = "m";
                }
                else if (QuestValues.AnswType == 'U' || QuestValues.AnswType == 'V')
                {
                    Unit = "m/s";
                }
                else if (QuestValues.AnswType == 'A')
                {
                    Unit = "m/s²";
                }
                else if (QuestValues.AnswType == 'T')
                {
                    Unit = "s";
                }
                int BtnPlacement = rnd.Next(1, 7);

                float[] UsedNums = new float[6];

                for(int i = 0; i < 6; i++)
                {
                    bool UniNum = false;
                    while (!UniNum)
                    {
                        UniNum=true;
                        float tempval = GenerateFloatVal(QuestCorrectAnsw);
                        if (i == 0 && QuestCorrectAnsw != tempval)
                        {
                                UniNum = true;
                                UsedNums[i] = tempval;
                        }
                        else if(i != 0 && QuestCorrectAnsw != tempval)
                        {
                            for (int j = 0; j < i; j++)
                            {
                                if (UsedNums[j] == tempval)
                                {
                                    UniNum = false;
                                }
                            }
                            if(UniNum)
                            {
                                UsedNums[i] = tempval;
                            }
                        }
                        else
                        {
                            UniNum=false;
                        }
                    }
                }

                Answ1 = UsedNums[0];
                Answ2 = UsedNums[1];
                Answ3 = UsedNums[2];
                Answ4 = UsedNums[3];
                Answ5 = UsedNums[4];
                Answ6 = UsedNums[5];


                switch (BtnPlacement)
                {
                    case 1:
                        Answ1 = QuestCorrectAnsw;
                        break;

                    case 2:
                        Answ2 = QuestCorrectAnsw;
                        break;

                    case 3:
                        Answ3 = QuestCorrectAnsw;
                        break;

                    case 4:
                        Answ4 = QuestCorrectAnsw;
                        break;

                    case 5:
                        Answ5 = QuestCorrectAnsw;
                        break;

                    case 6:
                        Answ6 = QuestCorrectAnsw;
                        break;
                }
            }
            else
            {
                var QuestValues = _context.Question.FirstOrDefault(x => x.QuestionId == TempQuestId);
                QuestCorrectAnsw = QuestValues.AnswTrue;
                TempQuestSolved = QuestValues.QuestSolved;
                if (QuestCorrectAnsw == BtnAnswer)
                {
                    AnswValid = true;
                    CorrectUser.UserCorrectAnsw = CorrectUser.UserCorrectAnsw + 1;
                }
                else
                {
                    AnswValid = false;
                }
                CorrectUser.UserTotalAnsw = CorrectUser.UserTotalAnsw + 1;
                _context.User.Update(CorrectUser);
                _context.SaveChanges();
            }
        }
        public IActionResult OnPost()
        {
            NextQuestion = true;
            return RedirectToPage("/RevisionQuestions/RevisionQuestions", new
            {
                NextQuestion,
                UserId
            });
        }

        public float GenerateFloatVal(float MidNum)
        {
            Random rand = new Random();
            double Min = MidNum-3;
            double Max = MidNum+3;
            double Range = Max - Min;
            float TempVal=0;

                double Sample = rand.NextDouble();
                double Scaled = (Sample * Range) + Min;
                if (MidNum % 1 == 0)
                {
                    TempVal = (float)Math.Round(Scaled, 0);
                }
                else
                {
                    string TempScaled = MidNum.ToString();
                    TempVal = (float)Math.Round(Scaled, TempScaled.Substring(TempScaled.IndexOf(".")).Length-1);
                }

            return TempVal;
        }

    }
}
