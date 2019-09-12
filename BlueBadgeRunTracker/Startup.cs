using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BlueBadgeRunTracker.Startup))]
namespace BlueBadgeRunTracker
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
