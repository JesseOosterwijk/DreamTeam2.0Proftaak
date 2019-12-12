using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Interfaces
{
    public interface IAccountContext
    {
        void AllowInfoSharing(int userId);
        void DisableInfoSharing(int userId);
        bool SharingIsEnabled(int userId);
        void UpdateWeight(int weight, int id);
        void UpdateStatus(int id, bool status);
        void EnableInfoDelete(int userId);
        void DisableInfoDelete(int userId);
        string ChangePassword(int id);
        bool DeleteInfoIsEnabled(int userId);
        void DeleteUser(User user);
    }
}
