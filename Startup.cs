using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GFR.Startup))]
namespace GFR
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
