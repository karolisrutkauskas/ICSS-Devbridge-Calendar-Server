using DevBridgeAPI.Models.Misc;

namespace DevBridgeAPI.UseCases.UserLogicN
{
    public interface IUserValidator
    {
        ValidationInfo ValidataManagerReassignment(int newManagerId, int userId);
    }
}