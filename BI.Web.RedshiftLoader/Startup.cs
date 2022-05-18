using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BI.Web.RedshiftLoader.Startup))]
namespace BI.Web.RedshiftLoader
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
