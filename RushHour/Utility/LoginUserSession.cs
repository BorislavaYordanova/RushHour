using RushHour.Models;
using System.Web;

namespace RushHour.Utility
{
    public class LoginUserSession
    {
        public const string UserProfile = "UserProfile";
        public int UserId { get; set; }
        public bool IsAdmin { get; set; }
        public string Name { get; set; }

        public static bool IsStateAdmin
        {
            get {
                if (Current != null)
                {
                    return Current.IsAdmin;
                }

                return false;
            }
        }

        public static LoginUserSession Current
        {
            get
            {
                return (LoginUserSession)HttpContext.Current.Session[UserProfile];
            }
        }

        public static void SetSessionAndVote(User user)
        {
            LoginUserSession session = new LoginUserSession();

            session.UserId = user.Id;
            session.IsAdmin = user.IsAdmin;
            session.Name = $"{user.Name}";

            HttpContext.Current.Session[UserProfile] = session;
        }


    }
}