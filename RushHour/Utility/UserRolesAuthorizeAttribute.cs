using System.Web.Mvc;

namespace RushHour.Utility
{
    public class UserRolesAuthorizeAttribute:AuthorizeAttribute
    {

        protected ActionResult CreateResult()
        {
            string role = "";

            bool isAuthoruzed = true;
            if (role == "Admin")
            {
                isAuthoruzed = false;
                if (LoginUserSession.Current == null)
                {
                    var viewResult = new ViewResult();
                    isAuthoruzed = false;
                }
                else
                {
                    if (LoginUserSession.Current.IsAdmin == true)
                    {
                        isAuthoruzed = true;
                    }
                }
            }
            if (!isAuthoruzed)
            {
                var viewName = "~/Views/Users/Login.cshtml";

                return new ViewResult
                {
                    ViewName = viewName
                };
            }
            else
            {
                return new ViewResult();
            }
        }
    }
}

