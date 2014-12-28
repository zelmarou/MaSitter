using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MaSitter.Startup))]
namespace MaSitter
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
