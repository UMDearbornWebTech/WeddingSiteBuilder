using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WeddingSiteBuilder.Startup))]
namespace WeddingSiteBuilder
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
