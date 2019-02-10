using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(MVC5_Template.Startup))]

namespace MVC5_Template
{
  public partial class Startup
  {
    public void Configuration(IAppBuilder app)
    {
      ConfigureAuth(app);
      ConfigureDependencyInjection();
      app.MapSignalR();
    }
  }
}
