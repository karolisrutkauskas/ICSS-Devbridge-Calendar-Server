using DevBridgeAPI.Models;
using DevBridgeAPI.Models.Misc;
using DevBridgeAPI.Models.Patch;

namespace DevBridgeAPI.UseCases.UserLogicN
{
    public interface IUserValidator
    {
        ValidationInfo ValidataManagerReassignment(int newManagerId, int userId);
        ValidationInfo ValidateFinishReg(User userForUpdate, RegCredentials regCredentials);
        ValidationInfo ValidateRegToken(string registrationToken);
    }
}