using Data.Interfaces;
using Logic.Interface;
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
    }
}
