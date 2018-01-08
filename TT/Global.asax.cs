using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using TT.Context;
using TT.Repository;

namespace TT
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //initializes the context everytime the application starts, in order to properly comunicate with the database.
            using (var context = new TTContext())
            {
                context.Database.Initialize(force: true);
            }

            TTContext.SharedTTContext = new TTContext();

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }

        //Disposes of the context when the application ends.
        protected void Application_End(object sender, EventArgs e)
        {
            TTContext.SharedTTContext.Dispose();
        }
    }
}
