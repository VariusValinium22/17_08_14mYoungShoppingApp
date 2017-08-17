using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(_17_08_14mYoungShoppingApp.Startup))]
namespace _17_08_14mYoungShoppingApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
