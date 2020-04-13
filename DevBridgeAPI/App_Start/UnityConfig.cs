using DevBridgeAPI.Controllers;
using DevBridgeAPI.Repository.Selector;
using DevBridgeAPI.UseCases;
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
                c => new GoalsController(c.Resolve<GoalsSelector>())
            );

            container.RegisterFactory<TopicsController>(
                c => new TopicsController(c.Resolve<TopicsSelector>())
            );

            container.RegisterFactory<UsersController>(
                c => new UsersController(c.Resolve<UsersSelector>())
            );

            container.RegisterFactory<AssignmentLogic>(
                c => new AssignmentLogic(c.Resolve<AssignmentsSelector>(),
                                         c.Resolve<UsersSelector>())
            );

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}