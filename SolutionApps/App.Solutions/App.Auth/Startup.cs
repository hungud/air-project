using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(App.Auth.Startup))]
namespace App.Auth
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
