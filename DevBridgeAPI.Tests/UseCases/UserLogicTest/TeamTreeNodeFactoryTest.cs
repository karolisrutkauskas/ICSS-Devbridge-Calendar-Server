using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using DevBridgeAPI.Models;
using DevBridgeAPI.Models.Complex;
using DevBridgeAPI.Repository.Dao;
using DevBridgeAPI.Tests.Helpers;
using DevBridgeAPI.UseCases.Exceptions;
using DevBridgeAPI.UseCases.UserLogicN;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PostUser = DevBridgeAPI.Models.Post.User;

namespace DevBridgeAPI.Tests.UseCases.UserLogicTest
{
    [TestClass]
    public class TeamTreeNodeFactoryTest
    {
        IEnumerable<User> _database;

        [TestInitialize]
        public void Init()
        {
            _database = new List<User>()
            {
                new User{ UserId = 1, ManagerId = null },
                new User{ UserId = 2, ManagerId = 11 },
                new User{ UserId = 3, ManagerId = 11 },
                new User{ UserId = 4, ManagerId = 6 },
                new User{ UserId = 5, ManagerId = 6 },
                new User{ UserId = 6, ManagerId = 7 },
                new User{ UserId = 7, ManagerId = 1 },
                new User{ UserId = 8, ManagerId = 6 },
                new User{ UserId = 9, ManagerId = 10 },
                new User{ UserId = 10, ManagerId = 6 },
                new User{ UserId = 11, ManagerId = 1 },
                new User{ UserId = 12, ManagerId = 3 },
            };
        }
        [TestMethod]
        public void ConstructFromRoot_ShouldHaveCorrectStructure()
        {
            var rootUserId = 1;
            var expected = new TeamTreeNode
            {
                This = new User { UserId = 1 },
                Children = new List<TeamTreeNode>() 
                {
                    new TeamTreeNode
                    {
                        This = new User { UserId = 7 },
                        Children = new List<TeamTreeNode>()
                        {
                            new TeamTreeNode
                            {
                                This = new User { UserId = 6 },
                                Children = new List<TeamTreeNode>()
                                {
                                    new TeamTreeNode
                                    {
                                        This = new User { UserId = 4 }
                                    },
                                    new TeamTreeNode
                                    {
                                        This = new User { UserId = 5 }
                                    },
                                    new TeamTreeNode
                                    {
                                        This = new User { UserId = 8 }
                                    },
                                    new TeamTreeNode
                                    {
                                        This = new User { UserId = 10 },
                                        Children = new List<TeamTreeNode>()
                                        {
                                            new TeamTreeNode
                                            {
                                                This = new User { UserId = 9 }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    },
                    new TeamTreeNode
                    {
                        This = new User { UserId = 11 },
                        Children = new List<TeamTreeNode>()
                        {
                            new TeamTreeNode
                            {
                                This = new User { UserId = 2 }
                            },
                            new TeamTreeNode
                            {
                                This = new User { UserId = 3 },
                                Children = new List<TeamTreeNode>()
                                {
                                    new TeamTreeNode
                                    {
                                        This = new User { UserId = 12 }
                                    }
                                }
                            }
                        }
                    }
                }
            };

            var daoMock = new Mock<IUsersDao>(MockBehavior.Strict);
            daoMock.Setup(x => x.SelectByID(It.IsAny<int>()))
                   .Returns<int>(param => GetUserById(param));
            daoMock.Setup(x => x.SelectSubordinates(It.IsAny<int>()))
                   .Returns<int>(param => GetSubordinates(param));

            var sut = new UserLogic(daoMock.Object, new TeamTreeNodeFactory(daoMock.Object));
            var actual = sut.GetTeamTree(rootUserId);

            Assert.IsTrue(TreesAreEqual(expected, actual));
        }

        [TestMethod]
        [SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "<Pending>")]
        public void RegisterNewUser_ShouldCatchSQLException()
        {
            var sqlException = SQLExceptionBuilder.CreateInstance()
                .Message("Some SQL text with UQ_Users_Email keyword")
                .Number(2627).Build();
            var fakeUser = new PostUser { Password = "pass" };
            var daoMock = new Mock<IUsersDao>();
            daoMock.Setup(x => x.InsertNewUser(It.IsAny<PostUser>()))
                .Throws(sqlException);

            var sut = new UserLogic(daoMock.Object, null);

            Assert.ThrowsException<UniqueFieldException>(() => sut.RegisterNewUser(fakeUser));
        }
        private bool TreesAreEqual(TeamTreeNode tree1, TeamTreeNode tree2)
        {
            if (tree1 == tree2) return true;

            if (!UsersAreEqual(tree1.This, tree2.This)) return false;

            if (tree1.Children == null && tree2.Children == null) return true;

            if ((tree1.Children == null && tree2.Children != null)
                || tree1.Children != null && tree2.Children == null) return false;

            if (tree1.Children.Count() != tree2.Children.Count()) return false;

            for (int i = 0; i < tree1.Children.Count(); i++)
            {
                if (!TreesAreEqual(tree1.Children.ElementAt(i), tree2.Children.ElementAt(i)))
                    return false;
            }

            return true;
        }
        private bool UsersAreEqual(User user1, User user2)
        {
            if (user1 == user2) return true;

            if (user1.UserId == user2.UserId) return true;

            return false;
        }
        private IEnumerable<User> GetSubordinates(int managerId)
        {
            return _database.Where(u => u.ManagerId == managerId);
        }

        private User GetUserById(int userId)
        {
            return _database.Where(u => u.UserId == userId).First();
        }
    }
}
