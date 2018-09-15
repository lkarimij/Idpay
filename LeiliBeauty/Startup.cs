using LeiliBeauty.Models;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LeiliBeauty.Startup))]
namespace LeiliBeauty
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
       
    }
}
