using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OrderingFood.Web.Startup))]
namespace OrderingFood.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
