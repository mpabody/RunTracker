using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunTracker.Services
{
    public class ShoeService
    {
        private readonly Guid _userID;
        
        public ShoeService(Guid userID)
        {
            _userID = userID;
        }
    }
}
