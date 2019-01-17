using System.Linq;
using System.Security.Principal;
namespace App.Base.Security
{
    public class AppPrincipal : IPrincipal
    {
        public IIdentity Identity { get; private set; }
        public bool IsInRole(string role)
        {
            if (roles.Any(r => role.Contains(r)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public AppPrincipal(string Username)
        {
            this.Identity = new GenericIdentity(Username);
        }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string[] roles { get; set; }
    }
}