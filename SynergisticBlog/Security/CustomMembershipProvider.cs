using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Configuration;

namespace SynergisticBlog.Security
{
    public class CustomMembershipProvider : SqlMembershipProvider
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