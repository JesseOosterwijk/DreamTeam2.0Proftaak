using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
