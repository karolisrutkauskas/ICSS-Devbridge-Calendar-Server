using DevBridgeAPI.Controllers;
using DevBridgeAPI.Repository.Dao;
using DevBridgeAPI.UseCases;
using DevBridgeAPI.UseCases.UserLogicN;
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
                c => new AssignmentsController(c.Resolve<IAssignmentLogic>())
            );

            container.RegisterFactory<GoalsController>(
                c => new GoalsController(c.Resolve<IGoalsLogic>())
            );

            container.RegisterFactory<TopicsController>(
                c => new TopicsController(c.Resolve<ITopicsDao>())
            );

            container.RegisterFactory<UsersController>(
                c => new UsersController(c.Resolve<IUserLogic>())
            );

            container.RegisterFactory<IAssignmentLogic>(
                c => new AssignmentLogic(c.Resolve<IAssignmentsDao>(),
                                         c.Resolve<IUsersDao>())
            );

            container.RegisterFactory<IUserLogic>(
                c => new UserLogic(c.Resolve<IUsersDao>(),
                                   c.Resolve<ITeamTreeNodeFactory>(),
                                   c.Resolve<IUserValidator>())
            );
            container.RegisterFactory<IGoalsLogic>(
                c => new GoalsLogic(c.Resolve<IGoalsDao>(),
                                    c.Resolve<IUsersDao>())
                );

            container.RegisterFactory<IGoalsDao>(c => new GoalsDao());

            container.RegisterFactory<IUsersDao>(c => new UsersDao());

            container.RegisterFactory<IAssignmentsDao>(c => new AssignmentsDao());

            container.RegisterFactory<ITopicsDao>(c => new TopicsDao());

            container.RegisterFactory<ITeamTreeNodeFactory>(
                c => new TeamTreeNodeFactory(c.Resolve<IUsersDao>())
            );

            container.RegisterFactory<IUserValidator>(
                c => new UserValidator(c.Resolve<IUsersDao>())
            );

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}