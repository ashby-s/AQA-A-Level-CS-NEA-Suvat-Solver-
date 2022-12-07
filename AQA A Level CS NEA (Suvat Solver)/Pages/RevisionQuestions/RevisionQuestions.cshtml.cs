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
            //gathers details for the user that is using the application
            var CorrectUser = _context.User.FirstOrDefault(x => x.UserId == UserId);

            if (NextQuestion)
            {
                //Used to determine what questions to use, gathers details depending
                //on the user using the web application
                List<UsertoCourses> UsertoCoursesList = new List<UsertoCourses>();
                UsertoCoursesList = _context.UsertoCourses.ToList();
                //Gathers details for all the questions into a list
                List<Question> QuestionList = new List<Question>();
                QuestionList = _context.Question.ToList();
                //gathers all the the links between each question to courses
                List<QuestiontoCourses> QuestiontoCoursesList = new List<QuestiontoCourses>();
                QuestiontoCoursesList = _context.QuestiontoCourses.ToList();
                

                Random rnd = new Random();
                bool AQAMaths = false;
                bool AQAPhys = false;
                bool ValidQuest = false;

                //Determines whether the user does Maths course, Physics course or both
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
                    //Randomises the next question to be asked
                    //Makes sure question is linked to courses that user does
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
                //Gathers details for that question from what was previously gathered
                var QuestValues = _context.Question.FirstOrDefault(x => x.QuestionId == TempQuestId);
                QuestCorrectAnsw = QuestValues.AnswTrue;
                TempQuestDiff = QuestValues.Difficulty;
                TempQuestDetails = QuestValues.QuestDetails;
                //Determines what unit that the question uses
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

                //Randomises the location that the answer will be in, button-wise
                int BtnPlacement = rnd.Next(1, 7);

                float[] UsedNums = new float[6];

                //Ensures that each randomised value for what the other buttons should display
                // is of the same decimal places by calling a subroutine
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

                //Places these values into variables for buttons
                Answ1 = UsedNums[0];
                Answ2 = UsedNums[1];
                Answ3 = UsedNums[2];
                Answ4 = UsedNums[3];
                Answ5 = UsedNums[4];
                Answ6 = UsedNums[5];

                //Places the correct answer in the correct button
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
                //Checks whether the answer given was correct
                var QuestValues = _context.Question.FirstOrDefault(x => x.QuestionId == TempQuestId);
                QuestCorrectAnsw = QuestValues.AnswTrue;
                TempQuestSolved = QuestValues.QuestSolved;
                if (QuestCorrectAnsw == BtnAnswer)
                {
                    //If correct, adds one to user total answ + shows answer was correct
                    AnswValid = true;
                    CorrectUser.UserCorrectAnsw = CorrectUser.UserCorrectAnsw + 1;
                }
                else
                {
                    //Shows answer was incorrect
                    AnswValid = false;
                }
                CorrectUser.UserTotalAnsw = CorrectUser.UserTotalAnsw + 1;
                _context.User.Update(CorrectUser);
                _context.SaveChanges();
            }
        }
        public IActionResult OnPost()
        {
            //reloads page, showing the next question
            NextQuestion = true;
            return RedirectToPage("/RevisionQuestions/RevisionQuestions", new
            {
                NextQuestion,
                UserId
            });
        }

        public float GenerateFloatVal(float MidNum)
        {
            //This subroutine ensures that all the
            //values are of the same decimal places
            Random rand = new Random();
            double Min = MidNum-3;
            double Max = MidNum+3;
            double Range = Max - Min;
            float TempVal=0;

                double Sample = rand.NextDouble();
                double Scaled = (Sample * Range) + Min;
                if (MidNum % 1 == 0)
                {
                //If to 0 decimal places, generates whole numbers
                    TempVal = (float)Math.Round(Scaled, 0);
                }
                else
                {
                //if not 0 decimal places, generates numbers to required decimal places
                    string TempScaled = MidNum.ToString();
                    TempVal = (float)Math.Round(Scaled, TempScaled.Substring(TempScaled.IndexOf(".")).Length-1);
                }

            return TempVal;
        }

    }
}
