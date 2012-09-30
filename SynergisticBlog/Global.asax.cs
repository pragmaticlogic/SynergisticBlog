using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Configuration;

namespace SynergisticBlog
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            
            routes.MapRoute(
                "Write", // Route name
                //"{controller}/{action}/{page}/{update}/{id}", // URL with parameters
                "Write/{page}/{update}/{id}",
                new { controller = "Admin", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "Logon", // Route name                
                "Logon",
                new { controller = "Account", action = "Logon" } 
            );

            routes.MapRoute(
                "Logoff", // Route name                
                "Logoff",
                new { controller = "Account", action = "Logon" }
            );

            routes.MapRoute(
                "DeletePost", // Route name
                "{controller}/{action}/{id}/{page}", // URL with parameters
                //"DeletePost/{id}/{page}",
                new { controller = "Admin", action = "DeletePost" } // Parameter defaults
            );

            routes.MapRoute(
                "Post", // Route name
                //"{controller}/{action}/{id}/{page}", // URL with parameters
                "Post/{id}/{page}",
                new { controller = "Post", action = "Index" } // Parameter defaults
            );            

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{page}", // URL with parameters
                new { controller = "Home", action = "Index", page = UrlParameter.Optional } // Parameter defaults
            );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            // Use LocalDB for Entity Framework by default
#if DEBUG
            Database.DefaultConnectionFactory = new SqlConnectionFactory(@"Data Source=(localdb)\v11.0; Integrated Security=True; MultipleActiveResultSets=True");
#else
            Database.DefaultConnectionFactory = new SqlConnectionFactory(ConfigurationManager.AppSettings["SQLSERVER_CONNECTION_STRING"]);
#endif
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}