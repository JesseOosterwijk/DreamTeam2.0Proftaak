using Models;

namespace Logic.Interface
{
    public interface IAccountLogic
    {
        void AllowInfoSharing(int userId);
        void DisableInfoSharing(int userId);
        bool SharingIsEnabled(int userId);
        void UpdateWeight(int weight, int id);
        void UpdateStatus(int id, bool status);
        string ChangePassword(int id);
        void EnableInfoDelete(int userId);
        void DisableInfoDelete(int userId);
        bool DeleteInfoIsEnabled(int userId);
        void DeleteUser(User user);
    }
}
