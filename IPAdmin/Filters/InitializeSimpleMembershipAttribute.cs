using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using System.Web.Security;
using IPAdmin.Repository;
using WebMatrix.WebData;
using IPAdmin.Models;

namespace IPAdmin.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class InitializeSimpleMembershipAttribute : ActionFilterAttribute
    {
        private static SimpleMembershipInitializer _initializer;
        private static object _initializerLock = new object();
        private static bool _isInitialized;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Ensure ASP.NET Simple Membership is initialized only once per app start
            LazyInitializer.EnsureInitialized(ref _initializer, ref _isInitialized, ref _initializerLock);
        }

        private class SimpleMembershipInitializer
        {
            public SimpleMembershipInitializer()
            {
                Database.SetInitializer<PatentRepository>(null);

                try
                {
                    using (var context = new PatentRepository())
                    {
                        if (!context.Database.Exists())
                        {
                            // Create the SimpleMembership database without Entity Framework migration schema
                            ((IObjectContextAdapter)context).ObjectContext.CreateDatabase();
                        }
                    }

                    WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "Id", "UserName", autoCreateTables: true);

                    if (!Roles.RoleExists("Admin"))
                        Roles.CreateRole("Admin");

                    if (!Roles.RoleExists("D1"))
                        Roles.CreateRole("D1");

                    if (!Roles.RoleExists("D2"))
                        Roles.CreateRole("D2");

                    if (!Roles.RoleExists("M1"))
                        Roles.CreateRole("M1");

                    if (!Roles.RoleExists("M2"))
                        Roles.CreateRole("M2");

                    if (!Roles.RoleExists("EndUser"))
                        Roles.CreateRole("EndUser");

                    if (!WebSecurity.UserExists("efan"))
                        WebSecurity.CreateUserAndAccount(
                            "efan",
                            "password",
                            new { Email = "Eric.Fan@snsunicorp.com.au"});

                    if (!Roles.GetRolesForUser("efan").Contains("Admin"))
                        Roles.AddUsersToRoles(new[] { "efan" }, new[] { "Admin" });
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("The ASP.NET Simple Membership database could not be initialized. For more information, please see http://go.microsoft.com/fwlink/?LinkId=256588", ex);
                }
            }
        }
    }
}
