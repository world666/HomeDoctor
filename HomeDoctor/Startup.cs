using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HomeDoctor.Startup))]
namespace HomeDoctor
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
