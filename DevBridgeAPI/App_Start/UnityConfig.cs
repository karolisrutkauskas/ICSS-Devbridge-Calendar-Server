using DevBridgeAPI.Controllers;
using DevBridgeAPI.Repository.Dao;
using DevBridgeAPI.UseCases;
using DevBridgeAPI.UseCases.Integrations;
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
                c => new AssignmentsController(c.Resolve<IAssignmentLogic>()),
                new HierarchicalLifetimeManager()
            );

            container.RegisterFactory<GoalsController>(
                c => new GoalsController(c.Resolve<IGoalsLogic>()),
                new HierarchicalLifetimeManager()
            );

            container.RegisterFactory<TopicsController>(
                c => new TopicsController(c.Resolve<ITopicLogic>()),
                new HierarchicalLifetimeManager()
            );

            container.RegisterFactory<UsersController>(
                c => new UsersController(c.Resolve<IUserLogic>()),
                new HierarchicalLifetimeManager()
            );

            container.RegisterFactory<IAssignmentLogic>(
                c => new AssignmentLogic(c.Resolve<IAssignmentsDao>(),
                                         c.Resolve<IUsersDao>()),
                new HierarchicalLifetimeManager()
            );

            //TODO try decorator pattern for validations and/or authorizations
            container.RegisterFactory<IUserLogic>(
                c => new UserLogic(c.Resolve<IUsersDao>(),
                                   c.Resolve<ITeamTreeNodeFactory>(),
                                   c.Resolve<IUserValidator>(),
                                   c.Resolve<IUserIntegrations>()),
                new HierarchicalLifetimeManager()
            );
            container.RegisterFactory<IGoalsLogic>(
                c => new GoalsLogic(c.Resolve<IGoalsDao>(),
                                    c.Resolve<IUsersDao>()),
                new HierarchicalLifetimeManager()
                );

            container.RegisterFactory<ITopicLogic>(
                c => new TopicLogic(c.Resolve<IUsersDao>(),
                                    c.Resolve<ITopicsDao>(),
                                    c.Resolve<IAssignmentsDao>()),
                new HierarchicalLifetimeManager()
                );

            container.RegisterFactory<IGoalsDao>(c => new GoalsDao(),
                new HierarchicalLifetimeManager());

            container.RegisterFactory<IUsersDao>(c => new UsersDao(),
                new HierarchicalLifetimeManager());

            container.RegisterFactory<IAssignmentsDao>(c => new AssignmentsDao(),
                new HierarchicalLifetimeManager()
            );

            container.RegisterFactory<ITopicsDao>(c => new TopicsDao(),
                new HierarchicalLifetimeManager()
            );

            container.RegisterFactory<ITeamTreeNodeFactory>(
                c => new TeamTreeNodeFactory(c.Resolve<IUsersDao>()),
                new HierarchicalLifetimeManager()
            );

            container.RegisterFactory<IUserValidator>(
                c => new UserValidator(c.Resolve<IUsersDao>()),
                new HierarchicalLifetimeManager()
            );

            container.RegisterFactory<IUserIntegrations>(c => new UserIntegrations(),
                new HierarchicalLifetimeManager());

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}