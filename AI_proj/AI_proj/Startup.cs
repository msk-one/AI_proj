using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AI_proj.Startup))]
namespace AI_proj
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
