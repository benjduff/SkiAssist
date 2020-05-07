using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SkiAssistWebsite.Startup))]
namespace SkiAssistWebsite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
