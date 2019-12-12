using Data.Interfaces;
using Logic.Interface;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public class AccountLogic : IAccountLogic
    {
        private readonly IAccountContext _context;

        public AccountLogic(IAccountContext context)
        {
            _context = context;
        }

        public void AllowInfoSharing(int userId)
        {
            _context.AllowInfoSharing(userId);
        }

        public void DisableInfoSharing(int userId)
        {
            _context.DisableInfoSharing(userId);
        }

        public bool SharingIsEnabled(int userId)
        {
            if (_context.SharingIsEnabled(userId))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void UpdateWeight(int weight, int id)
        {
            _context.UpdateWeight(weight, id);
        }

        public void UpdateStatus(int id, bool status)
        {
            _context.UpdateStatus(id, status);
        }

        public string ChangePassword(int id)
        {
            return _context.ChangePassword(id);
        }

        public void EnableInfoDelete(int userId)
        {
            _context.EnableInfoDelete(userId);
        }

        public void DisableInfoDelete(int userId)
        {
            _context.DisableInfoDelete(userId);
        }

        public bool DeleteInfoIsEnabled(int userId)
        {
            if (_context.DeleteInfoIsEnabled(userId))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void DeleteUser(User user)
        {
            if (DeleteInfoIsEnabled(user.UserId))
            {
                _context.DeleteUser(user);
            }
        }
    }
}
