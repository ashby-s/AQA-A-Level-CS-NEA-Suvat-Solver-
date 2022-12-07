using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AQA_A_Level_CS_NEA__Suvat_Solver_.Models;
using AQA_A_Level_CS_NEA__Suvat_Solver_.Data;
using System.Security.Cryptography;
using System.Text;

namespace AQA_A_Level_CS_NEA__Suvat_Solver_.Pages.UserLogin
{
    [BindProperties(SupportsGet = true)]
    public class RegisterPageModel : PageModel
    {

        public string TempUsername { get; set; }
        public string TempPassword { get; set; }
        public string TempPassword2 { get; set; }
        public bool HasPassword { get; set; } = true;
        public bool HasUsername { get; set; } = true;
        public bool ValidUsername { get; set; } = true;
        public bool ValidPassword { get; set; } = true;
        public bool RegisterApproved { get; set; } = false;
        public bool AQAPhys { get; set; } = false;
        public bool AQAMaths { get; set; } = false;
        public bool SubjectChosen { get; set; } = true;

        private readonly ApplicationDbContext _context;
        public RegisterPageModel(ApplicationDbContext context)
        {
            _context = context;
            //gathers values from database to be accessible by program
        }
        public List<User> UserList = new List<User>();

        public IActionResult OnPost()
        {
            User User = new User();
            UsertoCourses UsertoCourses = new UsertoCourses();
            HasPassword = true;
            HasUsername = true;
            ValidUsername = true;
            ValidPassword = true;
            SubjectChosen = true;
            UserList = _context.User.ToList();

            //Checks if no subject has been selected
            if (!AQAMaths && !AQAPhys)
            {
                SubjectChosen = false;
            }
            //Checks if Password is valid
            if (string.IsNullOrWhiteSpace(TempPassword) || TempPassword.Length < 4)
            {
                HasPassword = false;
            }
            //checks if username is valid
            if (string.IsNullOrWhiteSpace(TempUsername) || TempUsername.Length < 4)
            {
                HasUsername = false;
            }
            //checks is both entered passwords are equal
            if (TempPassword != TempPassword2)
            {
                ValidPassword = false;
            }
            //checks if username has already been used
            if (UserList.Any(x => x.UserName == TempUsername))
            {
                ValidUsername = false;
            }
            //if any of these values are not valid, refresh page with passing variables to be shown on refreshed page
            if (!HasPassword || !HasUsername || !ValidUsername || !SubjectChosen)
            {
                return RedirectToPage("/UserLogin/RegisterPage", new { HasPassword, HasUsername, ValidUsername, ValidPassword, SubjectChosen });
            }
            else
            {
                //Hashing the password for encryption
                string HashedPassword = HashPassword(TempPassword);

                RegisterApproved = true;

                //Adds a new user to the User table
                User.UserName = TempUsername;
                User.UserPass = HashedPassword;
                User.UserCorrectAnsw = 0;
                User.UserTotalAnsw = 0;
                _context.User.Add(User);
                _context.SaveChanges();

                //adds a new relation and corresponding subjects to UsertoCourses table
                if (AQAMaths)
                {
                    var CorrectUser = _context.User.FirstOrDefault(x => x.UserName == TempUsername);
                    UsertoCourses.UserId = CorrectUser.UserId;
                    UsertoCourses.CourseId = 1;
                    _context.UsertoCourses.Add(UsertoCourses);
                    _context.SaveChanges();

                }
                if (AQAPhys)
                {
                    var CorrectUser = _context.User.FirstOrDefault(x => x.UserName == TempUsername);
                    UsertoCourses.UserId = CorrectUser.UserId;
                    UsertoCourses.CourseId = 2;
                    _context.UsertoCourses.Add(UsertoCourses);
                    _context.SaveChanges();
                }

                //redirects to login page, showing that a user has just registered
                return RedirectToPage("/UserLogin/LoginPage", new { RegisterApproved });
            }
        }

        public string HashPassword(string Password)
        {
            char CurrentChar;
            byte CharVal;
            byte NewCharVal = 0;
            char NewChar;
            string HashedPassword = "";
            int iRepeat = 0;

            for (int i = 0; i < 32; i++)
            {
                //gets each character of password and converts it to a byte value
                if (i >= Password.Length)
                {
                    //ensures that hashed value is 32 chars long, so goes through chars again
                    if (iRepeat >= Password.Length)
                        iRepeat = 0;
                    CurrentChar = Password[iRepeat];
                    iRepeat++;
                }
                else
                {
                    CurrentChar = Password[i];
                    iRepeat++;
                }

                //updates the generated byte value
                CharVal = (byte)CurrentChar;
                CharVal = (byte)(CharVal - i + (3 * iRepeat));
                //gets a new byte value, with adding or subtracting from old value
                switch (CharVal % 10)
                {
                    case 0:
                        NewCharVal = (byte)(CharVal - 17);
                        break;
                    case 1:
                        NewCharVal = (byte)(CharVal - 4);
                        break;
                    case 2:
                        NewCharVal = (byte)(CharVal - 15);
                        break;
                    case 3:
                        NewCharVal = (byte)(CharVal - 13);
                        break;
                    case 4:
                        NewCharVal = (byte)(CharVal - 9);
                        break;
                    case 5:
                        NewCharVal = (byte)(CharVal - 8);
                        break;
                    case 6:
                        NewCharVal = (byte)(CharVal - 10);
                        break;
                    case 7:
                        NewCharVal = (byte)(CharVal - 12);
                        break;
                    case 8:
                        NewCharVal = (byte)(CharVal - 16);
                        break;
                    default:
                        NewCharVal = (byte)(CharVal + 3);
                        break;
                }
                //converts value back into a character, and to a string
                NewChar = ((char)NewCharVal);
                HashedPassword = HashedPassword + NewChar;
            }
            //sends hashed password back
            return HashedPassword;
        }
    }
}
