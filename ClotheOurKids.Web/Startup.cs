using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ClotheOurKids.Web.Startup))]
namespace ClotheOurKids.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
