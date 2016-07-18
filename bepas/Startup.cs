using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(bepas.Startup))]
namespace bepas
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
