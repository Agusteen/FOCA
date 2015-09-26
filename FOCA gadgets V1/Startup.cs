using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FOCA_gadgets_V1.Startup))]
namespace FOCA_gadgets_V1
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
