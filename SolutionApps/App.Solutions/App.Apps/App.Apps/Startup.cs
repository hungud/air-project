using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(App.Apps.Startup))]
namespace App.Apps
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
