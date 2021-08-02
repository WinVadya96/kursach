using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(kursach.Startup))]
namespace kursach
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
