using DevBridgeAPI.Models.Post;

namespace DevBridgeAPI.UseCases.Integrations
{
    public interface IUserIntegrations
    {
        void CreateInvitation(User invitedUser);
    }
}