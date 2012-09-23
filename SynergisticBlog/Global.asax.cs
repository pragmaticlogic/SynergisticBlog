using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Configuration;
using System.Data.SqlClient;
using System.Xml.Linq;
using System.Data.EntityClient;

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
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected virtual void Application_PreRequestHandlerExecute(Object sender, EventArgs e)
        {
            //ChangeConnectionString("DefaultConnection", ConfigurationManager.AppSettings["SQLSERVER_CONNECTION_STRING"]);
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            // Use LocalDB for Entity Framework by default
            //Database.DefaultConnectionFactory = new SqlConnectionFactory(@"Data Source=(localdb)\v11.0; Integrated Security=True; MultipleActiveResultSets=True");
            
            var uriString = ConfigurationManager.AppSettings["SQLSERVER_URI"];
            var uri = new Uri(uriString);
            var connectionString = new SqlConnectionStringBuilder
            {
                DataSource = uri.Host,
                InitialCatalog = uri.AbsolutePath.Trim('/'),
                UserID = uri.UserInfo.Split(':').First(),
                Password = uri.UserInfo.Split(':').Last(),
            }.ConnectionString;

            //Database.DefaultConnectionFactory = new SqlConnectionFactory(ConfigurationManager.AppSettings["SQLSERVER_CONNECTION_STRING"]);            

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }

        private bool ChangeConnectionString(string connStringName, string newValue)
        {
            try
            {
                //CreateXDocument and load configuration file
                XDocument doc = XDocument.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

                //Find all connection strings
                var query1 = from p in doc.Descendants("connectionStrings").Descendants()
                             select p;

                //Go throught each connection string elements find atribute specified by argument and replace its value with newVAlue
                foreach (var child in query1)
                {
                    foreach (var atr in child.Attributes())
                    {
                        if (atr.Name.LocalName == "name" && atr.Value == connStringName)
                            if (atr.NextAttribute != null && atr.NextAttribute.Name == "connectionString")
                                atr.NextAttribute.Value = newValue;
                    }
                }

                doc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}