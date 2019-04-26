using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(foodcorner.Startup))]
namespace foodcorner
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
