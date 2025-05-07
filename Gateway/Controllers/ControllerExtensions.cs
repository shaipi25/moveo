using Excpetions;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace Controllers
{
    public static class ControllerExtensions
    {
        public static string GetUserName(this ControllerBase controller)
        {
            var cognitoUsername = controller.HttpContext.User.FindFirst("username")?.Value;
            if (cognitoUsername == null)
                throw new UnauthorizedException("COuldn ot find user name");

            return cognitoUsername;
        }
    }
}
