using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FastBookCreator.Startup))]
namespace FastBookCreator
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
