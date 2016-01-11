using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GPSystem.Startup))]
namespace GPSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
