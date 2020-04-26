using DevBridgeAPI.Controllers;
using DevBridgeAPI.Repository.Dao;
using DevBridgeAPI.UseCases;
using DevBridgeAPI.UseCases.UserCasesN;
using System.Web.Http;
using Unity;
using Unity.Lifetime;
using Unity.WebApi;

namespace DevBridgeAPI
{
    public static class UnityConfig
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "Disposed at inner workings")]
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterFactory<AssignmentsController>(
                c => new AssignmentsController(c.Resolve<AssignmentLogic>())
            );

            container.RegisterFactory<GoalsController>(
                c => new GoalsController(c.Resolve<GoalsDao>())
            );

            container.RegisterFactory<TopicsController>(
                c => new TopicsController(c.Resolve<TopicsDao>())
            );

            container.RegisterFactory<UsersController>(
                c => new UsersController(c.Resolve<UsersDao>(),
                                         c.Resolve<UserLogic>())
            );

            container.RegisterFactory<AssignmentLogic>(
                c => new AssignmentLogic(c.Resolve<AssignmentsDao>(),
                                         c.Resolve<UsersDao>())
            );

            container.RegisterFactory<UserLogic>(
                c => new UserLogic(c.Resolve<UsersDao>(),
                                   c.Resolve<TeamTreeNodeFactory>())
            );

            container.RegisterFactory<TeamTreeNodeFactory>(
                c => new TeamTreeNodeFactory(c.Resolve<UsersDao>())
            );

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}