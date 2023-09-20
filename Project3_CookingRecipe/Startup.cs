using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Project3_CookingRecipe.Startup))]
namespace Project3_CookingRecipe
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
