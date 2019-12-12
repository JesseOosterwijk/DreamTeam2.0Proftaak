<<<<<<< HEAD
﻿namespace Logic.Interface
=======
﻿using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Interface
>>>>>>> f0195e281c00f3af562f5d470aad4a487a638e44
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
