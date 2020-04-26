using DevBridgeAPI.Models;
using DevBridgeAPI.Models.Complex;

namespace DevBridgeAPI.UseCases.UserCasesN
{
    public interface ITeamTreeNodeFactory
    {
        TeamTreeNode ConstructFromRoot(Models.User rootUser);
    }
}