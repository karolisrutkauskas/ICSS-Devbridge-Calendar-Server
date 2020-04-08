using DevBridgeAPI.Controllers;
using DevBridgeAPI.Repository.Selector;
using System.Web.Http;
using Unity;
using Unity.Lifetime;
using Unity.WebApi;

namespace DevBridgeAPI
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterFactory<AssignmentsController>(
                c => new AssignmentsController(c.Resolve<AssignmentsSelector>())
            );

            container.RegisterFactory<ConstraintsController>(
                c => new ConstraintsController(c.Resolve<ConstraintsSelector>())    
            );

            container.RegisterFactory<GoalsController>(
                c => new GoalsController(c.Resolve<GoalsSelector>())
            );

            container.RegisterFactory<TeamManagersController>(
                c => new TeamManagersController(c.Resolve<TeamManagersSelector>())
            );

            container.RegisterFactory<TeamsController>(
                c => new TeamsController(c.Resolve<TeamsSelector>())
            );

            container.RegisterFactory<TopicsController>(
                c => new TopicsController(c.Resolve<TopicsSelector>())
            );

            container.RegisterFactory<UsersController>(
                c => new UsersController(c.Resolve<UsersSelector>())
            );

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}