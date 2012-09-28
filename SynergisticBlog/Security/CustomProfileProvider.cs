using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Profile;
using System.Configuration;
using System.Reflection;

namespace SynergisticBlog.Security
{
    public class CustomProfileProvider : SqlProfileProvider
    {
        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {
            base.Initialize(name, config);
#if !DEBUG
            string connectionString = ConfigurationManager.AppSettings["SQLSERVER_CONNECTION_STRING"];

            // Update the private connection string field in the base class.
            var connectionStringField = GetType().BaseType.GetField("_sqlConnectionString", BindingFlags.Instance | BindingFlags.NonPublic);
            connectionStringField.SetValue(this, connectionString);           
#endif
        }
    }
}