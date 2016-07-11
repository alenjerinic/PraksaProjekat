using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Levi9ZrOrderingFood.Startup))]
namespace Levi9ZrOrderingFood
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
