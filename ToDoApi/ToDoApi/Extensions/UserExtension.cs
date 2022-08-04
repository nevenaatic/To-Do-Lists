using System.Security.Claims;

namespace ToDoApi.Extensions
{
    public static class UserExtension
    {

        public static string GetEmail(this ClaimsPrincipal user)
        {
            return user.Claims.First(x => x.Type.Equals("preferred_username")).Value;
        }
    }
}
