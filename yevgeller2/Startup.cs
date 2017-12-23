using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(yevgeller2.Startup))]
namespace yevgeller2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
