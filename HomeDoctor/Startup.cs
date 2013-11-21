using System.Data.Entity;
using Microsoft.Owin;
using Owin;
using DataRepository.DataAccess;

[assembly: OwinStartupAttribute(typeof(HomeDoctor.Startup))]
namespace HomeDoctor
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            Database.SetInitializer(new HomeDoctorDbInitializer());
        }
    }
}
