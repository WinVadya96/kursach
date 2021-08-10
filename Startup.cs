using Microsoft.Owin;
using Owin;
using System.Web;

[assembly: OwinStartupAttribute(typeof(kursach.Startup))]
namespace kursach
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }

        //public void ConfigureServices(IServiceCollection services)
        //{

        //}
        
    }

}
