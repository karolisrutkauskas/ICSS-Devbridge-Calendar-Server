using DevBridgeAPI.Models;
using DevBridgeAPI.Models.Complex;

namespace DevBridgeAPI.UseCases.UserLogicN
{
    public interface ITeamTreeNodeFactory
    {
        TeamTreeNode ConstructFromRoot(Models.User rootUser);
    }
}