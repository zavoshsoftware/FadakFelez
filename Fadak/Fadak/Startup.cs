using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Fadak.Startup))]
namespace Fadak
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
